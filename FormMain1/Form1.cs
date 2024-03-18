using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PIE.AxControls;
using PIE.Carto;
using PIE.DataSource;
using PIE.Geometry;
using PIE.SystemUI;

namespace FormMain1
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
            var mapCtrl = new MapControl();
            mapCtrl.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(mapCtrl);

            tocCtrl.SetBuddyControl(mapCtrl);
        }
        /// <summary>
        /// 添加矢量数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            // 获得要打开Shape数据的路径
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "矢量数据(*.shp)|*.shp";
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
        /// 添加栅格数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
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
        /// 添加HDF、NC数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            //获得要打开HDF数据的路径
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "HDF、NC数据|*.hdf;*.nc";
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
        /// 加载GDB地理数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            // 打开Persona1 GDB和Dwg
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Personal GDB数据|*.mdb|Dwg数据|*.dwg";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            //创建数据集
            IMultiDataset multiDataset = PIE.DataSource.DatasetFactory.OpenDataset(openFileDialog.FileName, OpenMode.ReadOnly) as IMultiDataset;
            if (multiDataset == null) return;
            //创建图层
            IMultiLayer multiLayer = PIE.Carto.LayerFactory.CreateDefaultMultiLayer(multiDataset);
            if (multiLayer == null) return;
            // 添加图层到地图并刷新
            mapCtrl.FocusMap.AddLayer(multiLayer as ILayer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 加载静止卫星数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "HF数据|*.hdf";
            if (openFile.ShowDialog() != DialogResult.OK) return;

            string channelName = "NOMChanne113";//波段名称
            string tempTif = System.IO.Path.GetDirectoryName(openFile.FileName) + "\\N0MChanne113.tiff";//输出tiff路径
            ISpatialReference spatialReference = new ProjectedCoordinateSystem();//日标空间参考
            spatialReference.ImportFromUserInput("+projgeos +h=35785863 +a=6378137.0 +b=6356752.3 +lon_0=104.7 +no_defs");
            IRasterLayer rasterLayer = OpenStaticData(openFile.FileName, channelName, tempTif, spatialReference);
            if (rasterLayer == null) return;
            mapCtrl.FocusMap.AddLayer(rasterLayer as ILayer);
            mapCtrl.ActiveView.PartialRefresh(ViewDrawPhaseType.ViewAll);
        }
        /// <summary>
        /// 打开风云4A、风云2G等静止卫星数据，读取指定波段数据为tiff
        /// </summary>
        /// <param name="filePath">hdf路径</param>
        /// <param name="channelName">波段通道名称</param>
        /// <param name="tiffPath">生成tiff路径</param>
        /// <param name="spatialReference">空间参考</param>
        /// <returns></returns>
        private IRasterLayer OpenStaticData(string  filePath,string channelName,string tiffPath,ISpatialReference spatialReference)
        {
            IRasterLayer rasteLayer = null;
                //打开MultiDataset
            IMultiDataset hdfDataset=PIE.DataSource.DatasetFactory.OpenDataset(filePath,OpenMode.ReadOnly)as IMultiDataset;
            if(hdfDataset == null) return null;
                //遍历，查找指定通道的Dataset，进行数据格式转换
            for(int i = 0;i<hdfDataset.GetDatasetCount();i++)
            {
                //1、获取操作数据
                IDataset pTempDataset = hdfDataset.GetDataset(i);
                if (pTempDataset.Name != channelName) continue;
                IRasterDataset hdfRasterDatasetBand=pTempDataset as IRasterDataset;
                //2、读写栅格数据形成新的栅格数据集
                int nWidth = hdfRasterDatasetBand.GetRasterXSize();
                int nHeight = hdfRasterDatasetBand.GetRasterYSize();
                PixelDataType pixDataType = hdfRasterDatasetBand.GetRasterBand(0).GetRasterDataType();
                int bandCount = hdfRasterDatasetBand.GetBandCount();
                int[] bandMap = new int[bandCount];
                for (int j = 0; j < bandCount; j++)
                    bandMap[j] = j + 1;
                UInt16[] arr = new UInt16[nWidth * nHeight * bandCount];
                bool IsOk = hdfRasterDatasetBand.Read(0, 0, nWidth, nHeight, arr, nWidth, nHeight, pixDataType, bandCount, bandMap);
                IRasterDataset newRasterDataset = DatasetFactory.CreateRasterDataset(tiffPath, nWidth, nHeight, bandCount, pixDataType, "GTiff", null);
                bool flag = newRasterDataset.Write(0, 0, nWidth, nHeight, arr, nWidth, nHeight, pixDataType, bandCount, bandMap);
                newRasterDataset.SpatialReference = spatialReference;
                newRasterDataset.GetRasterBand(0).SetNoDataValue(65535);
                // 六参数，根据输入坐标的不同需要进行动态设置，本示例代码以风云4 - 4000m的数据作为实验数据
                int beginLineNum = 0;
                int nReslution = 4000;
                double[] geoTransform = new double[6];
                geoTransform[0] = -5496000;
                geoTransform[1] = nReslution;
                geoTransform[2] = 0;
                geoTransform[3] = 5496000 - beginLineNum * nReslution;
                geoTransform[4] = 0;
                geoTransform[5] = -nReslution;
                newRasterDataset.SetGeoTransform(geoTransform);

                (newRasterDataset as IDisposable).Dispose();
                (hdfRasterDatasetBand as IDisposable).Dispose();
                rasteLayer = PIE.Carto.LayerFactory.CreateDefaultLayer(tiffPath) as IRasterLayer;
                break;
            }
            return rasteLayer;
        }
    }
}
