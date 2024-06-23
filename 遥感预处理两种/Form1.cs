using DevExpress.Utils.Drawing.Helpers;
using PIE.AxControls;
using PIE.Carto;
using PIE.CommonAlgo;
using PIE.Controls;
using PIE.DataSource;
using PIE.Display;
using PIE.Geometry;
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
            Control.CheckForIllegalCrossThreadCalls = false;
            //1.添加TocControls
            tocCtrl = new TOCControl();
            tocCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(tocCtrl);

            //2.添加MapControls
            mapCtrl = new MapControl();
            mapCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(mapCtrl);

            tocCtrl.SetBuddyControl(mapCtrl);

            //图层树控件事件注册
            tocCtrl.MouseClick += TocCtrl_MouseClick;//图层树控件鼠标点击事件

            //地图范围变化监听事件
            PIE.AxControls.IMapControlEvents mapControlEvent = mapCtrl as PIE.AxControls.IMapControlEvents;
            mapControlEvent.OnExtentUpdated += mapControlEvent_OnExtentUpdated;//地图范围变化事件
            mapControlEvent.OnResolutionUpdated += mapControlEvent_OnResolutionUpdated;//地图分辨率更新事件

            //显示转换事件注册
            ITransformEvents transformEvent = (ITransformEvents)mapCtrl.ActiveView.DisplayTransformation;
            transformEvent.OnVisibleBoundsUpdated += TransformEvent_OnVisibleBoundsUpdated;//可视化范围变化事件


        }
        /// <summary>
        /// 图层树控件鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TocCtrl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//鼠标右键
            {
                PIETOCNodeType nodeType = PIETOCNodeType.Null;//HitTest中的对象
                IMap map = null;
                ILayer layer = null;
                object unk = null;
                object data = null;
                tocCtrl.HitTest(e.X, e.Y, ref nodeType, ref map, ref layer, ref unk, ref data);
                switch (nodeType)
                {
                    case PIETOCNodeType.Map:
                        rghMenuMap.Show(tocCtrl, new System.Drawing.Point(e.X, e.Y));
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
            ////1、调用功能插件中的窗体 
            //var frm = new PIE.Plugin.FrmPIECalibration();
            //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            //// 辐射定标算法实现
            //// 1.创建算法对象
            //var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.CalibrationAlgo");
            //algo.Name = "CalibrationAlgo";
            //algo.Description = "辐射定标算法";
            ////2.设置算法参数
            //if (frm.ExChangeData == null) return;
            //algo.Params = frm.ExChangeData;            
            ////var info = new DataPreCali_Exchange_Info();
            ////info.InputFilePath = @"D:\\Temp\\06-数据基础操作数据\\栅格数据\\01.GF1-tiff\\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-PAN1.tiff";
            ////info.XMLFilePath = @"D:\Temp\06-数据基础操作数据\栅格数据\01.GF1-tiff\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-PAN1.xml";
            ////info.OutputFilePath = @"D:\Temp\fsdb.tif";
            ////info.FileTypeCode = "GTiff";
            ////info.Type = 100;
            ////algo.Params = info;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += AlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += AlgoEvent_OnExecuteCompleted;
            //// 4.执行算法
            //AlgoFactory.Instance().AsynExecuteAlgo(algo);





            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmAtmosphericCorrection();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //大气校正算法实现
            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.AtmosphericCorrectionAlgo");
            algo.Name = "AtmosphericCorrectionAlgo";
            algo.Description = "大气校正算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////2.创建算法参数结构体
            //var info = new DataProcess_AtmCor_Info();
            //info.InputFile = @"F:\工作\教学\备课2023\遥感可视化程序设计2024上\PIE-SDK博客材料\PIE示例数据\栅格数据\01.GF1\
            //GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-MSS1.tiff";
            //info.InputXML = @"F:\工作\教学\备课2023\遥感可视化程序设计2024上\PIE-SDK博客材料\PIE示例数据\栅格数据\01.GF1\
            //GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-MSS1.xml";
            //info.OutputSR = @"F:\工作\教学\备课2023\遥感可视化程序设计2024上\PIE-SDK博客材料\输出数据\dqjz.tif";
            //info.AtmModel = 0;
            //info.AerosolType = 1;
            //info.InitialVIS = 40;
            //info.FileTypeCode = "GTiff";
            //info.DataType = 1;
            //info.AeroRetrieval = 1;
            //algo.Params = info;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;


            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void CAlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
        private int CAlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value = Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }
        /// <summary>
        /// 功能二
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            ////1.调用功能插件中的窗体
            //var frm = new PIE.Plugin.FrmImageClip(0b0);
            //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            ////图像裁剪算法实现
            ////1.创建算法对象
            //var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.ImageClipAlgo");
            //algo.Name = "ImageClipAlgo";
            //algo.Description = "图像裁剪算法";
            ////2.设置算法参数
            //algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;


            ////4.执行算法
            //AlgoFactory.Instance().AsynExecuteAlgo(algo);



            //DataPreImgClip_Exchange_Info info = new DataPreImgClip_Exchange_Info();
            //info.InputFilePath = @"D:\Temp\06-数据基础操作数据\栅格数据\01.GF1-tiff\langfangDEM\廊坊dem.tif";
            //info.OutputFilePath = @"D:\Temp\06-数据基础操作数据\栅格数据\01.GF1-tiff\langfangDEM\廊坊caijian.tif";
            //info.XStart = 1440;
            //info.XEnd = 1770;
            //info.YStart = 160;
            //info.YEnd = 480;

            //info.Type = 0;
            //info.listBands = new List<int>() { 0, 1, 2 };
            //info.FileType = "GTiff";

            GraphicsLayer graphicsLayer = null;//错误
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmImageClip(graphicsLayer);//必须输入一个参数
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //算法创建
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.ImageClipAlgo");
            if (algo == null) return;
            algo.Name = "ImageClipAlgo";
            algo.Description = "图像裁剪算法";

            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            //算法执行
            ISystemAlgoEvents algoEvents = algo as ISystemAlgoEvents;
            algoEvents.OnExecuteCompleted += CjAlgoEvent_OnExecuteCompleted;
            algoEvents.OnProgressChanged += CjAlgoEvent_OnProgressChanged;
            AlgoFactory.Instance().AsynExecuteAlgo(algo);

        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void CjAlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
        private int CjAlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value = Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }
        /// <summary>
        /// 加载栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 加载栅格数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cmd = new PIE.Controls.RasterCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 删除图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cmd = new PIE.Controls.DeleteLayerCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 缩放至图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 缩放至图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cmd = new PIE.Controls.ZoomToLayerCommand();
            cmd.OnCreate(mapCtrl);
            cmd.OnClick();
        }
        /// <summary>
        /// 地图范围发生变化
        /// </summary>
        /// <param name="displayTransformation"></param>
        /// <param name="bVisibleBoundChanged"></param>
        private void TransformEvent_OnVisibleBoundsUpdated(IDisplayTransformation displayTransformation, bool bVisibleBoundChanged)
        {
            //MessageBox.Show("地图范围发生变化");
        }
        /// <summary>
        /// 地图分辨率发生变化
        /// </summary>
        /// <param name="displayTransformation"></param>
        private void mapControlEvent_OnResolutionUpdated(IDisplayTransformation displayTransformation)
        {
            //MessageBox.Show("地图分辨率发生变化");
        }
        /// <summary>
        /// 可视范围发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sizeChanged"></param>
        /// <param name="newEnvelope"></param>
        private void mapControlEvent_OnExtentUpdated(object sender, bool sizeChanged, IEnvelope newEnvelope)
        {
            //MessageBox.Show("可视范围发生变化");
        }

        private void 主成分正变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmPCA();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //主成分正变换算法实现
            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformForwardPCAAlgo");
            algo.Name = "TransformForwardPCAAlgo";
            algo.Description = "主成分正变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += FPAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += FPAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void FPAlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
                var info = algo.Params as ForwardPCA_Exchange_Info;
                if (info == null) return;
                var outFile = info.m_strOutputResultFile;
                var layer = LayerFactory.CreateDefaultLayer(outFile);
                mapCtrl.FocusMap.AddLayer(layer);
                mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
            }
        }
        /// <summary>
        /// 算法进度事件
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int FPAlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value = Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }

        private void 主成分逆变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmPCAInv();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //主成分逆变换算法实现
            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformInversePCAAlgo");
            algo.Name = "TransformForwardPCAAlgo";
            algo.Description = "主成分逆变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 最小噪声正变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmMNF();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            if (algo == null)
            {
                MessageBox.Show("地图分辨率发生变化");
                return;
            }
            algo.Name = "TransformFuncAlgo";
            algo.Description = "最小噪声正变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 最小噪声逆变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmMNFInv();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            if (algo == null) return;
            algo.Name = "TransformFuncAlgo";
            algo.Description = "最小噪声逆变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 小波正变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmWAVELET();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            algo.Name = "TransformFunAlgo";
            algo.Description = "小波正变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 小波逆变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmWAVELETInv();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            algo.Name = "TransformFunAlgo";
            algo.Description = "小波逆变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 傅里叶正变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmFFT();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            algo.Name = "TransformFunAlgo";
            algo.Description = "傅里叶正变换";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 傅里叶逆变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmFFTInv();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            algo.Name = "TransformFunAlgo";
            algo.Description = "傅里叶逆变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 缨帽变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmKT();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformFuncAlgo");
            algo.Name = "TransformFunAlgo";
            algo.Description = "缨帽变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += KTAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += KTAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void KTAlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
                var info = algo.Params as DataTrans_Exchange_Info;
                if (info == null) return;
                var outFile = info.m_strOutputFile;
                var layer = LayerFactory.CreateDefaultLayer(outFile);
                mapCtrl.FocusMap.AddLayer(layer);
                mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
            }
        }
        /// <summary>
        /// 算法进度事件
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int KTAlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value = Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }

        private void 彩色空间正变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmRGB2IHS();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformRGB2IHSAlgo");
            algo.Name = "TransformRGB2IHSAlgo";
            algo.Description = "彩色空间正变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 彩色空间逆变换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmIHS2RGB();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.TransformIHS2RGBAlgo");
            algo.Name = "TransformRGB2IHSAlgo";
            algo.Description = "彩色空间逆变换算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 去相关拉伸ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PIE.CommonAlgo.DeRelationStretch_Exchange info = new PIE.CommonAlgo.DeRelationStretch_Exchange();

            info.m_strInputFile = @"D:\Temp\07-数据基础操作数据\04.World\World.tif";
            info.m_strOutputFile = @"D:\Temp\07-数据基础操作数据\04.World\ip_result12.tif";
            info.m_strFileTypeCode = "GTiff";

            PIE.SystemAlgo.ISystemAlgo algo = PIE.SystemAlgo.AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.DeRelationStretchAlgo");
            if (algo == null) return;

            //2、算法执行
            PIE.SystemAlgo.ISystemAlgoEvents algoEvents = algo as PIE.SystemAlgo.ISystemAlgoEvents;
            algo.Name = " 去相关拉伸";
            algo.Params = info;
            bool result = PIE.SystemAlgo.AlgoFactory.Instance().ExecuteAlgo(algo);

            //3、结果显示
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(@"D:\Temp\07-数据基础操作数据\04.World\ip_result12.tif");
            mapCtrl.ActiveView.FocusMap.AddLayer(layer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 影像格式转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 影像格式转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmRasterConverter();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.RasterConverterAlgo");
            if (algo == null) return;
            algo.Name = "RasterConverterAlgo";
            algo.Description = "影像格式转换";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }
        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void RCAlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
                var info = algo.Params as DataTrans_Exchange_Info;
                if (info == null) return;
                var outFile = info.m_strOutputFile;
                var layer = LayerFactory.CreateDefaultLayer(outFile);
                mapCtrl.FocusMap.AddLayer(layer);
                mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
            }
        }
        /// <summary>
        /// 算法进度事件
        /// </summary>
        /// <param name="complete"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private int RCAlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            var temp = Convert.ToInt32(complete);
            if (temp > 0)
            {
                temp = 100;
            }
            progressbar.Value = temp;
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }
        /// <summary>
        /// 存储格式转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 存储格式转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmUtilityFormatConvert();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.FormatTranAlgo");
            if (algo == null) return;
            algo.Name = "FormatTranAlgo";
            algo.Description = "存储格式转换";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 位深转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////1.调用功能插件中的窗体
            //var frm = new PIE.Plugin.FrmBitDepthTrans();
            //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            ////1.创建算法对象
            //ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BitDepthTransAlgo");
            //if (algo == null) return;
            //algo.Name = "BitDepthTransAlgo";
            //algo.Description = "位深转换";
            ////2.设置算法参数
            //algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            ////4.执行算法
            //AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 波段运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////1.调用功能插件中的窗体
            //var frm = new PIE.AxControls.UtilityOperBandDialog();
            ////if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            ////设置地图，与mapControlMain中的地图绑定
            //frm.SetMap(mapCtrl.FocusMap);
            ////初始化
            //frm.ReInit();

            ////1.创建算法对象
            //ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BandOperAlgo");
            //if (algo == null) return;
            //algo.Name = "BandOperAlgo";
            //algo.Description = "波段运算";
            ////2.设置算法参数
            //algo.Params = frm.GetParams();

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            ////4.执行算法
            //AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 波谱运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////1.调用功能插件中的窗体
            //var frm = new PIE.Plugin.FrmSpecBand();
            //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            ////1.创建算法对象
            //ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BandSpecAlgo");
            //if (algo == null) return;
            //algo.Name = "BandSpecAlgo";
            //algo.Description = "波谱运算";
            ////2.设置算法参数
            //algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            ////4.执行算法
            //AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 波段合成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void 读取ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer layer = mapCtrl.FocusMap.GetLayer(0);
            IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;

            string fileName = featureClass.GetName();


            long featureCount = featureClass.GetFeatureCount();
            string spatia1Reference = featureClass.GetFeatureDataset().SpatialReference.Name;
            string message = string.Format("数据名称:{0} ;\r\n要素的数量:{1} ; \r\n数据空间参考:{2}", fileName, featureCount, spatia1Reference);
            MessageBox.Show(message, string.Format("{0}数据的信息: ", fileName));

            IFeature feature = featureClass.GetFeature(0);
            IFields fields = featureClass.GetFields();
            int fie1dIndex = fields.GetFieldIndex("NAME"); IField field = fields.GetField(fie1dIndex); string fieldValue = field.Name;
            string fieldInfo = "";
            while (feature != null)
                fieldInfo += fieldValue + "字段值为:\r\n" + feature.GetValue(fieldValue) + "\r\n";
            MessageBox.Show(fieldInfo, "要素的信息");
            feature = null;
        }

        private void 绘制面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.定义面
            IPointCollection polygon = new Polygon();
            polygon.AddPoint(120, 30, 0);
            polygon.AddPoint(125, 30, 0);
            polygon.AddPoint(125, 35, 0);
            polygon.AddPoint(120, 35, 0);
            (polygon as Polygon).CloseRings();
            //2.定义面样式
            IFillSymbol symbol = SystemSymbolSetting.Instance.DefaultFillSymbol;
            //3.绘制面
            mapCtrl.DrawShape(polygon as IGeometry, symbol);
        }

        private void 创建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.设置输出路径
            var filepath = @"";
            //2.设置字段
            IField fie1d1 = new Field("Name", FieldType.OFTString, 50, 1);
            fie1d1.Name = "Name";
            fie1d1.AliasName = "名称";
            IField fie1d2 = new Field("Level", FieldType.OFTInteger, 50, 1);
            fie1d2.Name = "Level";
            fie1d2.AliasName = "级别";
            //3.添加字段
            IFields fields = new Fields();
            fields.AddField(fie1d1);
            fields.AddField(fie1d2);
            // 4.创建空间参考信息
            var spaRef = SpatialReferenceFactory.CreateSpatialReference((int)GeoCSType.GeoCSType_WGS1984);
            // 5.创建矢量要素集
            var vecDs = PIE.DataSource.DatasetFactory.CreateFeatureDataset(filepath, fields, GeometryType.GeometryPoint, spaRef, "SHP");
            var layer = LayerFactory.CreateDefaultFeatureLayer(vecDs);
            var vecFeaCls = layer.FeatureClass;
            //6.添加要素
            // A POINT
            IFeature feature = vecFeaCls.CreateNewFeature();
            IPoint point = new PIE.Geometry.Point();
            point.PutCoords(90, 45);
            feature.Geometry = point as IGeometry;
            feature.FID = 02;
            feature.SetValue(0, "A");
            feature.SetValue(1, 2);
            //B POINT
            IFeature feature2 = vecFeaCls.CreateNewFeature();
            IPoint point2 = new PIE.Geometry.Point();
            point2.PutCoords(93, 40);
            feature2.Geometry = point2 as IGeometry;
            feature2.FID = 01;
            feature2.SetValue(0, "B");
            feature2.SetValue(1, 3);
            //7.将要素字段信息添加进矢量数据集里面
            vecFeaCls.AddFeature(feature);
            vecFeaCls.AddFeature(feature2);
            // 8.保存矢量数据集
            vecFeaCls.Save();
            mapCtrl.FocusMap.AddLayer(layer as ILayer);
        }

        private void 查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.首先要获取图层要素
            IMap map = mapCtrl.FocusMap;
            ILayer layer = map.GetLayer(0);
            IFeatureLayer featurelayer = layer as IFeatureLayer;
            //2.设置查询条件
            IQueryFilter queryFilter = new QueryFilter();
            string fi1ter = "NAME='北京'";
            queryFilter.SetAttributeQuery(fi1ter);
            //3.得到查询结果
            IFeatureCursor featureCursor = featurelayer.FeatureClass.Search(queryFilter);
            IFeature feature = null;
            feature = featureCursor.NextFeature();
            //清除之前查询高亮显示的结果
            map.ClearSelectionFeatures();
            //4.地图显示得到的结果
            if (feature != null)
            {
                mapCtrl.FocusMap.SelectFeature(featurelayer as ILayer, feature);
            }
            mapCtrl.PartialRefresh(ViewDrawPhaseType.ViewAll);

        }
        /// <summary>
        /// 属性查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var tool = new AttributeIdentifyTool();//添加Controls库引用
            (tool as ICommand).OnCreate(mapCtrl);
            mapCtrl.CurrentTool = tool;
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.获取图层
            ILayer layer = mapCtrl.FocusMap.GetLayer(0);
            var feaCls = (layer as IFeatureLayer).FeatureClass;

            //2获取要修改的字段索引
            int index = feaCls.GetFields().GetFieldIndex("Name");

            //2.获取要素
            IFeatureCursor feaCursor = feaCls.Search(null);
            IFeature fea = feaCursor.NextFeature();
            while (fea != null)
            {
                //3.修改要素值
                fea.SetValue(index, "C");
                //4.修改后保存
                feaCls.UpdateFeature(fea);

                fea = feaCursor.NextFeature();
            }
            mapCtrl.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
    }
}
