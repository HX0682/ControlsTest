using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PIE.AxControls;
using PIE.Carto;
using PIE.SystemUI;


namespace FormMain
{
    public partial class FormMain : Form
    {
        private MapControl mapCtrl;
        public FormMain()
        {
            InitializeComponent();
            //1.添加TocControls
            var tocCtrl = new TOCControl();
            tocCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(tocCtrl);

            //2.添加MapControls
            var mapCtrl = new MapControl();
            mapCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(mapCtrl);

            tocCtrl.SetBuddyControl(mapCtrl);
        }
        /// <summary>
        /// 添加栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRasterData_Click(object sender, EventArgs e)
        {
            DialogResult xh = MessageBox.Show("未找到地图数据，无法进行比例尺变换操作。", "提示");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "栅格数据(*.tiff) *.tif.*.tiff";
            openFileDialog.Title = "请选择需要打开的栅格数据";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            string filePath = openFileDialog.FileName;
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(filePath);//引用类库Carto
            if (layer == null)
                return;
            mapCtrl.FocusMap.AddLayer(layer);
            mapCtrl.ActiveView.PartialRefresh(PIE.Carto.ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 地图放大事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapZoomIn_Click(object sender, EventArgs e)
        {
            ITool tool = new PIE.Controls.MapZoomInTool();//引用类库SystemUI
            (tool as ICommand).OnCreate(mapCtrl);
            mapCtrl.CurrentTool = tool;
        }
        /// <summary>
        /// 地图缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapZoomOut_Click(object sender, EventArgs e)
        {
            ITool tool = new PIE.Controls.MapZoomOutTool();//引用类库SystemUI
            (tool as ICommand).OnCreate(mapCtrl);
            mapCtrl.CurrentTool = tool;
        }
        /// <summary>
        /// 地图平移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapPan_Click(object sender, EventArgs e)
        {
            ITool tool=new PIE.Controls.PanTool();
            (tool as ICommand).OnCreate(mapCtrl);
            mapCtrl.CurrentTool = tool;
        }
        /// <summary>
        /// 全图显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapFullExtent_Click(object sender, EventArgs e)
        {
            ICommand command=new PIE.Controls.FullExtentCommand();
            command.OnCreate(mapCtrl);
            command.OnClick();
        }
        /// <summary>
        /// 中心放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapCenterZoomIn_Click(object sender, EventArgs e)
        {
            ICommand cmd = new PIE.Controls.CenterZoomInCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 中心缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapCenterZoomOut_Click(object sender, EventArgs e)
        {
            ICommand cmd = new PIE.Controls.CenterZoomOutCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 地图1：1显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapZoomToNative_Click(object sender, EventArgs e)
        {
            ICommand cmd = new PIE.Controls.ZoomToNativeCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 地图显示处截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapOutputlmg_Click(object sender, EventArgs e)
        {
            //1.设置输出路径
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "png图(*.png)|*.png|bmp|*.bmp";
            saveFile.Title = "截图保存路径";
            if (saveFile.ShowDialog() != DialogResult.OK)
                return;
            if (string.IsNullOrEmpty(saveFile.FileName))
            {
                MessageBox.Show("文件名不能为空!");
                return;
            }

            //2.实例化输出png对象，并设置输出属性
            PIE.Display.IDisplayTransformation dT = mapCtrl.ActiveView.DisplayTransformation;
            PIE.Carto.ExportPNG export = new PIE.Carto.ExportPNG();
            export.Width = (int)dT.DeviceFrame.Width;
            export.Height = (int)dT.DeviceFrame.Height;
            export.ExportFileName = saveFile.FileName;
            export.StartExporting();
            mapCtrl.ActiveView.Output(export as PIE.Carto.IExport, 96, dT.DeviceFrame, mapCtrl.ActiveView.Extent, null);
            export.FinishExporting();
        }
        /// <summary>
        /// 地图比例尺
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (mapCtrl == null)
            {
                DialogResult xh = MessageBox.Show("未找到地图数据，无法进行比例尺变换操作。", "提示");
                if (xh == DialogResult.OK) return;
            }
            double beforeMapScale = mapCtrl.ActiveView.DisplayTransformation.MapScale;//当前地图比例尺
            mapCtrl.ActiveView.DisplayTransformation.MapScale = 50000;//转换尺度
            double lastMapScale = mapCtrl.ActiveView.DisplayTransformation.MapScale;//转换后的地图比例尺
            MessageBox.Show(string.Format("修改前地图比例尺为:1:{0};\r\n修改后比例尺为:1:{1}", beforeMapScale, lastMapScale), "提示");//提示框显示内容
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
    }
}
