using Gif.Components;
using PIE.AxControls;
using PIE.Carto;
using PIE.DataSource;
using PIE.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 实验三数据基础操作
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
        /// 动画图层
        /// </summary>
        private IAnimationLayer m_AnimationLayer = null;
        /// <summary>
        /// 打开长时间序列数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
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
        /// <summary>
        /// 继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resume_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Resume();
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pause_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Pause();
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, EventArgs e)
        {
            m_AnimationLayer.Stop();
        }
        /// <summary>
        /// 动画导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
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
        /// 复合图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            //1.获取路径
            IRasterDataset rasterDS_World = PIE.DataSource.DatasetFactory.OpenRasterDataset(@"D:\Temp\07-数据基础操作数据\04.World\World.tif", OpenMode.ReadOnly);
            IRasterDataset resterDS_Yun = PIE.DataSource.DatasetFactory.OpenRasterDataset(@"D:\Temp\07-数据基础操作数据\04.World\CC_2018082615.tif", OpenMode.ReadOnly);
            PIE.DataSource.IComplexRasterDataset dataset = new PIE.DataSource.ComplexRasterDataset();
            dataset.AddRasterBand(rasterDS_World, 0);
            dataset.AddRasterBand(rasterDS_World, 1);
            dataset.AddRasterBand(rasterDS_World, 2);
            dataset.AddRasterBand(resterDS_Yun, 0);
            IRasterLayer rasterLayer = new RasterLayer();
            rasterLayer.Dataset = dataset as IRasterDataset;
            (rasterLayer as ILayer).Name = "组合图层测试";
            mapCtrl.FocusMap.AddLayer(rasterLayer as ILayer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 增强Alpha通道的数据渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            //1、打开数据集
            IRasterLayer rasterLayer = new RasterLayer();

            IRasterDataset rasterDataset = PIE.DataSource.DatasetFactory.OpenRasterDataset(@"D:\Temp\07-数据基础操作数据\04.World\World_Yun.tif", OpenMode.ReadOnly);

            // 2、渲染lpha通道
            IRasterRGBRender rgbRender = new RasterRGBRender();
            //rgbRender.UseRedBand = true;
            //rgbRender.UseGreenBand = true;
            //rgbRender.UseBlueBand = true;
            //指定Alpha通道的索引
            rgbRender.AlphaBandIndex = 3;
            rgbRender.UseAlphaBand = true;
            //拉伸
            IRasterStretch strech = rgbRender as IRasterStretch;
            strech.StretchType = RasterStretchType.RasterStretch_MinimumMaximum;
            //strech.SetMinimumMaximum(0, 255,0);
            //strech.SetMinimumMaximum(0，255，1)
            //strech.SetMinimumMaximum(0, 255,
            strech.SetMinimumMaximum(0, 201, 0);
            rasterLayer.Dataset = rasterDataset;
            rasterLayer.Render = rgbRender as IRasterRender;
            //3、结果显示
            mapCtrl.ActiveView.FocusMap.AddLayer(rasterLayer as ILayer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 查看图层属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
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
        private void btn6_Click(object sender, EventArgs e)
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
        /// 卷帘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn7_Click(object sender, EventArgs e)
        {
            ITool tool = new PIE.Controls.SwipeLayerTool();
            (tool as ICommand).OnCreate(mapCtrl);
            mapCtrl.CurrentTool = tool;
        }
    }
}
