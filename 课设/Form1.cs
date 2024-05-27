using Gif.Components;
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
using System.Windows.Forms;

namespace 课设
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
        /// 辐射定标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //1、调用功能插件中的窗体
            var frm = new PIE.Plugin.FrmPIECalibration();
            if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            // 辐射定标算法实现
            // 1.创建算法对象
            var algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.CalibrationAlgo");
            algo.Name = "CalibrationAlgo";
            algo.Description = "辐射定标算法";
            //2.设置算法参数
            if (frm.ExChangeData == null) return;
            algo.Params = frm.ExChangeData;
            //var info = new DataPreCali_Exchange_Info();
            //info.InputFilePath = @"D:\\Temp\\06-数据基础操作数据\\栅格数据\\01.GF1-tiff\\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-PAN1.tiff";
            //info.XMLFilePath = @"D:\Temp\06-数据基础操作数据\栅格数据\01.GF1-tiff\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600\GF1_PMS1_E116.5_N39.4_20131127_L1A0000117600-PAN1.xml";
            //info.OutputFilePath = @"D:\Temp\fsdb.tif";
            //info.FileTypeCode = "GTiff";
            //info.Type = 100;
            //algo.Params = info;

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += AlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += AlgoEvent_OnExecuteCompleted;
            // 4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        /// <summary>
        /// 算法完成事件
        /// </summary>
        /// <param name="algo"></param>
        private void AlgoEvent_OnExecuteCompleted(ISystemAlgo algo)
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
        private int AlgoEvent_OnProgressChanged(double complete, string mag, ISystemAlgo algo)
        {
            //算法进度
            progressbar.Value = Convert.ToInt32(complete);
            //算法进度消息
            labProMsg.Text = mag;
            return 1;
        }

        /// <summary>
        /// 大气校正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
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
        /// 图像裁剪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            GraphicsLayer graphicsLayer = null;
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
            #region 1、参数设置

            PIE.CommonAlgo.DataBitDepthTrans_Exchange_Info info = new PIE.CommonAlgo.DataBitDepthTrans_Exchange_Info();
            info.InputFile = @"D:\Temp\07-数据基础操作数据\04.World\World.tif";
            info.OutputFile = @"D:\Temp\07-数据基础操作数据\04.World\World2.tif";
            info.MaxIn = 255;
            info.MinIn = 0;
            info.MaxOut = 32768;
            info.MinOut = -32768;
            info.OutDataType = 1;

            PIE.SystemAlgo.ISystemAlgo algo = PIE.SystemAlgo.AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BitDepthTransAlgo");
            if (algo == null) return;

            #endregion 1、参数设置

            //2、算法执行
            PIE.SystemAlgo.ISystemAlgoEvents algoEvents = algo as PIE.SystemAlgo.ISystemAlgoEvents;
            algo.Name = "位深转换";
            algo.Params = info;
            PIE.SystemAlgo.AlgoFactory.Instance().ExecuteAlgo(algo);
            //3、结果显示
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(@"D:\Temp\07-数据基础操作数据\04.World\World2.tif");
            mapCtrl.ActiveView.FocusMap.AddLayer(layer); mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }

        private void 波段运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1.调用功能插件中的窗体
            var frm = new PIE.AxControls.UtilityOperBandDialog();
            //if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            //设置地图，与mapControlMain中的地图绑定
            frm.SetMap(mapCtrl.FocusMap);
            //初始化
            frm.ReInit();

            //1.创建算法对象
            ISystemAlgo algo = AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BandOperAlgo");
            if (algo == null) return;
            algo.Name = "BandOperAlgo";
            algo.Description = "波段运算";
            //2.设置算法参数
            algo.Params = frm.GetParams();

            //3.注册算法事件
            var algoEvent = algo as ISystemAlgoEvents;
            algoEvent.OnProgressChanged += RCAlgoEvent_OnProgressChanged;
            algoEvent.OnExecuteCompleted += RCAlgoEvent_OnExecuteCompleted;

            //4.执行算法
            AlgoFactory.Instance().AsynExecuteAlgo(algo);
        }

        private void 波谱运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 1、参数设置

            PIE.CommonAlgo.BandOper_Exchange_Info info = new PIE.CommonAlgo.BandOper_Exchange_Info();
            info.StrExp = "s1";
            info.SelectFileBands = new List<int> { 1 };
            info.SelectFileNames = new List<string> { @"D:\Temp\07-数据基础操作数据\04.World\World.tif", @"D:\Temp\07-数据基础操作数据\04.World\World.tif" };
            info.OutputFilePath = @"D:\Temp\07-数据基础操作数据\04.World\World4.tif";
            info.FileTypeCode = "GTiff";

            PIE.SystemAlgo.ISystemAlgo algo = PIE.SystemAlgo.AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BandSpecAlgo");
            if (algo == null) return;

            #endregion 1、参数设置

            //2、算法执行
            PIE.SystemAlgo.ISystemAlgoEvents algoEvents = algo as PIE.SystemAlgo.ISystemAlgoEvents;
            algo.Name = "波谱运算";
            algo.Params = info; PIE.SystemAlgo.AlgoFactory.Instance().ExecuteAlgo(algo);
            //3、结果显示
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(@"D:\Temp\07-数据基础操作数据\04.World\World4.tif");
            mapCtrl.ActiveView.FocusMap.AddLayer(layer); mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }

        private void 波段合成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 1、参数设置

            PIE.CommonAlgo.BandCombination_Exchange_Info info = new PIE.CommonAlgo.BandCombination_Exchange_Info();
            string path = @"D:\Temp\07-数据基础操作数据\04.World\World.tif";
            IRasterDataset rDataset = DatasetFactory.OpenDataset(path, OpenMode.ReadOnly) as IRasterDataset;

            info.m_vecFileptr = new List<IRasterDataset> { rDataset, rDataset };
            List<int> list1 = new List<int> { 0, 1, 2 };
            info.bands = new List<List<int>> { list1, list1 };
            info.tstrfile = @"D:\Temp\07-数据基础操作数据\04.World\World5.tif";
            info.m_strFileTypeCode = "GTiff";
            PIE.CommonAlgo.Interestregion interestregion = new PIE.CommonAlgo.Interestregion();
            interestregion.SetRegion(0, 0, rDataset.GetRasterYSize(), rDataset.GetRasterXSize());
            info.regioninfo = new List<PIE.CommonAlgo.Interestregion> { interestregion, interestregion };
            info.m_iOutRangeCrossType = 0;

            PIE.SystemAlgo.ISystemAlgo algo = PIE.SystemAlgo.AlgoFactory.Instance().CreateAlgo("PIE.CommonAlgo.dll", "PIE.CommonAlgo.BandCombinationAlgo");
            if (algo == null) return;

            #endregion 1、参数设置

            //2、算法执行
            PIE.SystemAlgo.ISystemAlgoEvents algoEvents = algo as PIE.SystemAlgo.ISystemAlgoEvents;
            algo.Name = "波段合成";
            algo.Params = info;
            bool result = PIE.SystemAlgo.AlgoFactory.Instance().ExecuteAlgo(algo);
            //3、结果显示
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(@"D:\Temp\07-数据基础操作数据\04.World\World5.tif");
            mapCtrl.ActiveView.FocusMap.AddLayer(layer); mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
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

        /// <summary>
        /// 动画图层
        /// </summary>
        private IAnimationLayer m_AnimationLayer = null;

        /// <summary>
        /// 添加长时间序列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 添加数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //1、获取数据
            IList<string>
            listFile = new List<string>();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = true;
            openDialog.Title = "打开长时间序列数据";
            openDialog.Filter = "SeriesData(*.tiff)|*.tiff;*.tif";
            if (openDialog.ShowDialog() != DialogResult.OK) return;
            //2、加载动画图层
            ILayer tempLayer = null;
            m_AnimationLayer = new AnimationLayer();

            foreach (var fileName in openDialog.FileNames)
            {
                tempLayer = LayerFactory.CreateDefaultLayer(fileName);
                m_AnimationLayer.AddLayer(tempLayer);
            }

            ILayer layer = m_AnimationLayer as ILayer;
            layer.Name = "长时间序列数据";
            mapCtrl.FocusMap.AddLayer(layer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
            m_AnimationLayer.SetInterval(500);//设置时间间隔
            m_AnimationLayer.Start();//开始播放
        }

        private void 继续ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Pause();
        }

        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Pause();
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Stop();
        }

        private void 动画导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //设置导出路径
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "GIF Data|*.gif";
            saveDlg.Title = "请选择动画保存路径";
            if (saveDlg.ShowDialog() != DialogResult.OK)
                return;
            var outFile = saveDlg.FileName;//gif动画文件路径

            m_AnimationLayer.Stop();//动画暂停

            //设置gif动画导出
            AnimatedGifEncoder gifencoder = new AnimatedGifEncoder();
            gifencoder.Start(outFile);//开始导出
            gifencoder.SetDelay(500);//每帧播放时间
            gifencoder.SetRepeat(0);//-1：不重复，0：重复

            //向gifencoder对象中添加单帧图片
            for (int i = 0; i < m_AnimationLayer.LayerCount; i++)
            {
                m_AnimationLayer.SetCurrentFrameIndex(i);//设置当前帧
                mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
                System.Threading.Thread.Sleep(100);
                var image = mapCtrl.GetScreenshot();//获取屏幕截图
                gifencoder.AddFrame(image);//添加截图到gifencoder
            }
            gifencoder.Finish();//结束导出
            MessageBox.Show($"导出成功！{outFile}");
        }

        /// <summary>
        /// 查看图层属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            //1.获取查看图层
            IMap map = mapCtrl.FocusMap;
            ILayer layer = map.GetLayer(0);

            //2.实例化属性查看窗口对象
            PIE.AxControls.LayerPropertyDialog dlg = new PIE.AxControls.LayerPropertyDialog();
            PIETOCNodeTag pieTOCNodeTag = new PIETOCNodeTag();
            pieTOCNodeTag.Map = map;
            pieTOCNodeTag.Layer = layer;

            ILayer layerl = pieTOCNodeTag.Layer;
            dlg.Initial(map, layer);
            dlg.ShowDialog();
        }

        /// <summary>
        /// 查看矢量数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //1.获取查看数据的图层
            IMap map = mapCtrl.FocusMap;
            ILayer layer = map.GetLayer(0);

            //2.实例属性窗口对象
            PIE.AxControls.FeatureLayerAttributeDialog fLayerAttributeDlg = new FeatureLayerAttributeDialog();
            PIETOCNodeTag pieTOCNodeTag = new PIETOCNodeTag();
            pieTOCNodeTag.Map = map;
            pieTOCNodeTag.Layer = layer;
            //m_mapControl.CustomerProperty = pieTOCNodeTag:
            fLayerAttributeDlg.Initial(map, layer);
            fLayerAttributeDlg.ShowDialog();
        }

        /// <summary>
        /// 栅格数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            IRasterDataset rasterDs = null;
            for (int i = 0; i < mapCtrl.FocusMap.LayerCount; i++)
            {
                var layer = mapCtrl.FocusMap.GetLayer(i);
                if (layer is IRasterLayer)
                {
                    rasterDs = (layer as IRasterLayer).Dataset;
                    break;
                }
            }
            if (rasterDs == null)
            {
                MessageBox.Show("没有找到栅格数据集！");
                return;
            }
            var filePath = rasterDs.FullName;
            var rsName = rasterDs.Name;
            var bandCount = rasterDs.GetBandCount();
            var rsWidth = rasterDs.GetRasterXSize();
            var rsHeight = rasterDs.GetRasterYSize();
            var rsSpaRef = rasterDs.SpatialReference;

            MessageBox.Show($"栅格数据集路径：{filePath};\r\n栅格数据集名称：{rsName};\r\n栅格数据集波段数：{bandCount};" + $"\r\n栅格数据集宽度:{rsWidth};\r\n栅格数据集高度:{rsHeight};\r\n空间参考:{rsSpaRef}");
        }

        /// <summary>
        /// 创建栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 创建ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //栅格数据读取
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "RasterFile|*.tif;*.tiff";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "数据保存路径";
            saveFileDialog.Filter = "RasterFile|*.tif";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            IRasterDataset rDataset = DatasetFactory.OpenRasterDataset(openFileDialog.FileName, OpenMode.ReadOnly);
            ILayer layer = LayerFactory.CreateDefaultLayer(openFileDialog.FileName);
            mapCtrl.FocusMap.AddLayer(layer);
            mapCtrl.PartialRefresh(ViewDrawPhaseType.ViewAll);

            //栅格数据集的属性信息
            var filePath = rDataset.FullName;
            var rsName = rDataset.Name;
            var bandCount = rDataset.GetBandCount();
            var rsWidth = rDataset.GetRasterXSize();
            var rsHeight = rDataset.GetRasterYSize();
            var rsSpaRef = rDataset.SpatialReference;

            MessageBox.Show($"栅格数据集路:{filePath};\r\n栅格数据集名称:{rsName};\r\n栅格数据集波段数:{bandCount};" +
                $"\r\n栅格数据集宽度:{rsWidth};\r\n栅格数据集高度:{rsHeight};\r\n空间参考:{rsSpaRef}");
            PixelDataType pixelType = rDataset.GetRasterBand(0).GetRasterDataType();

            Byte[] arr = new Byte[rsWidth * rsHeight * bandCount];
            int[] bandMap = new int[bandCount];
            for (int i = 0; i < bandCount; i++)
            {
                bandMap[i] = i + 1;
            }
            bool ok = rDataset.Read(0, 0, rsWidth, rsHeight, arr, rsWidth, rsHeight, pixelType, bandCount, bandMap);
            IRasterDataset newRDataset = DatasetFactory.CreateRasterDataset(saveFileDialog.FileName, rsWidth, rsHeight, bandCount, pixelType, "GTiff", null);
            newRDataset.Write(0, 0, rsWidth, rsHeight, arr, rsWidth, rsHeight, pixelType, bandCount, bandMap);
            newRDataset.SpatialReference = rDataset.SpatialReference;
            double[] geoTrans = rDataset.GetGeoTransform();
            newRDataset.SetGeoTransform(geoTrans);

            ILayer layer2 = LayerFactory.CreateDefaultRasterLayer(newRDataset) as ILayer;
            mapCtrl.FocusMap.AddLayer(layer2);
            mapCtrl.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }

        /// <summary>
        /// 栅格数据创建金字塔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 创建金字塔ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //对话框选择栅格数据集
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "RasterFile|*.tif;*.tiff";
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                string path = openFileDialog.FileName;

                //打开栅格数据集
                IRasterDataset rasterDataset = DatasetFactory.OpenRasterDataset(path, OpenMode.ReadOnly);
                //读取创建前的栅格数据集金字塔级别
                int count = rasterDataset.GetPyramidLevel();
                MessageBox.Show("金字塔创建之前的级别为【" + count + "】");
                //创建栅格数据集金字塔：重采样比率，数据类别（高斯、最近邻、立方卷积等），两个回调函数
                bool bOk = rasterDataset.BuildPyramid(Convert.ToSingle(0.5), DadaSampleType.CUBIC, null, null);
                //读取创建后的栅格数据集金字塔级别
                count = rasterDataset.GetPyramidLevel();

                if (bOk) MessageBox.Show("金字塔创建成功，金字塔创建后的级别为【" + count + "】");
                else MessageBox.Show("金字塔创建失败！");
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "创建金字塔失败异常");
            }
        }
    }
}