using DevExpress.Utils.Drawing.Helpers;
using PIE.AxControls;
using PIE.Carto;
using PIE.CommonAlgo;
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
            if(e.Button == MouseButtons.Right)//鼠标右键
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
        private int CAlgoEvent_OnProgressChanged(double complete,string mag,ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value=Convert.ToInt32(complete);
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

            GraphicsLayer graphicsLayer=null;//错误
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
        private int FPAlgoEvent_OnProgressChanged(double complete,string mag, ISystemAlgo algo)
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
            //1.调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmDeRelationStretch();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.DeRelationStretch");
            if (algo == null) return;
            algo.Name = "DeRelationStretch";
            algo.Description = "去相关拉伸算法";
            //2.设置算法参数
            algo.Params = frm.ExChangeData;

            ////3.注册算法事件
            //var algoEvent = algo as ISystemAlgoEvents;
            //algoEvent.OnProgressChanged += CAlgoEvent_OnProgressChanged;
            //algoEvent.OnExecuteCompleted += CAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }
    }
}
