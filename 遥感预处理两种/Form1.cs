using PIE.AxControls;
using PIE.Carto;
using PIE.CommonAlgo;
using PIE.SystemAlgo;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 遥感预处理两种
{
    public partial class Form1 : Form
    {
        private MapControl mapCtrl;
        private TOCControl tocCtrl;
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

            //图层树控件事件注册
            tocCtrl.MouseClick += TocCtrl_MouseClick;//图层树控件鼠标点击事件
        }
        /// <summary>
        /// 图层树控件鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TocCtrl_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                PIETOCNodeType nodeType = PIETOCNodeType.Null;//HitTest中的对象
                IMap map = null;
                ILayer layer = null;
                object unk=null;
                object data=null;
                tocCtrl.HitTest(e.X,e.Y,ref nodeType,ref map,ref layer,ref unk,ref data);
                switch(nodeType)
                {
                    case PIETOCNodeType.Map:
                        rghMenuMap.Show(tocCtrl,new System.Drawing.Point(e.X,e.Y));
                        break;
                    case PIETOCNodeType.RasterLayer:
                        rghMenuRaster.Show(tocCtrl, new System.Drawing.Point(e.X, e.Y));
                        break;
                    case PIETOCNodeType.FeatureLayer:
                        rghMenuVector.Show(tocCtrl, new System.Drawing.Point(e.X, e.Y));
                        break;
                }
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRasterData_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "栅格数据(*.tiff,*.tif)|*.tif;*.tiff|矢量数据(*.shp)|*.shp";
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
            if (frm.ShowDialog()!=System.Windows.Forms.DialogResult.OK)return;
            // 辐射定标算法实现
            // 1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonA1go.d11", "PIE.CommonA1go.CalibrationA1go");
            algo.Name = "CalibrationAlgo";
            algo.Description = "辐射定标算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;
            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += AlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += AlgoEvent_OnProgressChanged;
            // 4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);

        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void AlgoEvent_OnProgressChanged(ISystemAlgo algo)
        {
            progressbar.Value = 0;
            labProMsg.Text = "";
            var eCode = -1;
            var eMsg = "";
            algo.GetErrorInfo(ref eCode, ref eMsg);
            if (eCode != 0)
            {
                MessageBox.Show(algo.Name + "执行失败!");
                return;
            }
            else
            {
                var info = algo.Params as DataPreCali_Exchange_Info;
                if (info == null) return;
                var outFile = info.OutputFilePath;
                var layer = LayerFactory.CreateDefaultLayer(outFile);
                mapCtrl.FocusMap.AddLayer(layer);
                mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
            }
        }
        /// <summary>
        /// 算法进度事件
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="mag"></param>
        /// <param name="algo"></param>
        /// <returns></returns>
        private int AlgoEvent_OnProgressChanged(double complete,string mag,ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value=Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }
    }
}
