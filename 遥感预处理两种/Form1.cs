using PIE.AxControls;
using PIE.Carto;
using PIE.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 遥感预处理两种
{
    public partial class Form1 : Form
    {
        private MapControl mapCtrl;
        public Form1()
        {
            InitializeComponent();
            //1.添加TocControls
            var tocCtrl = new TOCControl();
            tocCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(tocCtrl);

            //2.添加MapControls
            mapCtrl = new MapControl();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "栅格数据(*.tiff)|*.tif;*.tiff";
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
        /// 地图放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapZoomIn_Click(object sender, EventArgs e)
        {
            ICommand cmd = new PIE.Controls.CenterZoomInCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 地图缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapZoomOut_Click(object sender, EventArgs e)
        {
            ICommand cmd = new PIE.Controls.CenterZoomOutCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 地图平移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapPan_Click(object sender, EventArgs e)
        {
            ITool tool = new PIE.Controls.PanTool();
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
            ICommand command = new PIE.Controls.FullExtentCommand();
            command.OnCreate(mapCtrl);
            command.OnClick();
        }
        /// <summary>
        /// 功能1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            //1、调用功能插件中的窗体 
            var frm = new PIE.Plugin.FrmPIECalibration();


        }
    }
}
