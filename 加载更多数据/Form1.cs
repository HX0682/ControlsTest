using PIE.AxControls;
using PIE.Carto;
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
using PIE.Geometry;
using PIE.DataSource;

namespace 加载更多数据
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
        /// 打开Micaps数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            // 获得要打开Micaps数据的路径
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Micaps数据|*.000";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            // 打开图层
            string filePath = openFileDialog.FileName;
            ILayer layer = PIE.Carto.LayerFactory.CreateDefaultLayer(filePath);
            if (layer == null) return;
            // 添加图层到地图并刷新
            mapCtrl.FocusMap.AddLayer(layer);
            mapCtrl.ActiveView.PartialRefresh(PIE.Carto.ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 动画图层
        /// </summary>
        private IAnimationLayer m_AnimationLayer= null;
        /// <summary>
        /// 打开长时间序列数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
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
        /// 继续播放
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

        private void btn3_Click(object sender, EventArgs e)
        {
            //以解析Micaps1数据为例
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Micaps文件(*.000)|*.000";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IFeatureLayer defineLayer = OpenDefineShp(openFileDialog.FileName);
                if (defineLayer != null)
                {
                    (defineLayer as ILayer).Name = "自定义矢量图层";
                    mapCtrl.FocusMap.AddLayer(defineLayer as ILayer);
                    mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
                }
            }
        }
        /// <summary>
        /// 解析自定义数据，生成矢量
        /// 字段添加站号、经度、纬度、海波高度
        /// </summary>
        /// <param name="filePath">输出SHP文件路径</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IFeatureLayer OpenDefineShp(string filePath)
        {
            IFeatureLayer featureLayer = null;
            int count = 0;//记录读取数据的行数
            string[] lineValues = null;//每一行的数据值
                                       //字段声明
            IField field_id = new Field("PointID", FieldType.OFTInteger, 20, 4);
            field_id.AliasName = "站号";

            IField field_x = new Field("1on", FieldType.OFTInteger, 20, 4);
            field_x.AliasName = "经度";

            IField field_y = new Field("lat", FieldType.OFTInteger, 20, 4);
            field_y.AliasName = "纬度";

            IField field_h = new Field("height", FieldType.OFTInteger, 20, 4);
            field_h.AliasName = "海拔高度";

            IFields fields = new Fields();
            fields.AddField(field_id);
            fields.AddField(field_x);
            fields.AddField(field_y);
            fields.AddField(field_h);
            //建立内存数据集
            IFeatureDataset pDataset = PIE.DataSource.DatasetFactory.CreateFeatureDataset("", fields, GeometryType.GeometryPoint, null, "MEM");
            //解析数据文件，写入数据集中
            string[] valueLines = System.IO.File.ReadAllLines(filePath);
            char[] charSeperate = { '' };

            for(int j = 0;j< valueLines.Length; j++)
            {
                string str = valueLines[j];
                if (count == 0 || count == 1)
                {
                    count++;
                    continue;
                }
                //解析字符串，生成Feature
                IFeature newFeature = pDataset.CreateNewFeature();
                lineValues=str.Split(charSeperate,StringSplitOptions.RemoveEmptyEntries);
                //字段赋值
                newFeature.SetValue(0, Convert.ToInt32(lineValues[0]));
                newFeature.SetValue(1, Convert.ToInt32(lineValues[1]));
                newFeature.SetValue(2, Convert.ToInt32(lineValues[2]));
                newFeature.SetValue(3, Convert.ToInt32(lineValues[3]));
                //生成Geometry
                IPoint point = new PIE.Geometry.Point();
                point.PutCoords(Convert.ToDouble(lineValues[1]), Convert.ToDouble(lineValues[2]));
                newFeature.Geometry=point as IGeometry;
                count++;
                (newFeature as IDisposable).Dispose();

            }
            featureLayer = PIE.Carto.LayerFactory.CreateDefaultFeatureLayer(pDataset);
            return featureLayer;
        }
    }
}
