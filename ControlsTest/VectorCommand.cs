using PIE.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PIE.ControlsTest
{
    /// <summary>
    /// 矢量数据加载
    /// </summary>
    public class VectorCommand : BaseCommand
    {

        ///<summary>
        ///构造函数
        ///</summary>
        public VectorCommand()
        {
            m_Image = ControlsTest.Properties.Resources.DataLoader_Vector;
            this.Caption = "加载矢量数据";
            this.Name = "VectorCommand";
            this.ToolTip = "加载矢量数据(Shape)";
            this.Checked = false;
            this.Enabled = false;

        }

        ///<summary>
        ///创建插件对象
        ///</summary>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            if (!(hook is PIE.Carto.IPmdContents)) return;

            this.Enabled = true;
            m_Hook = hook;
            m_HookHelper.Hook = hook;
        }

        ///<summary>
        ///单击方法
        ///</summary>
        public override void OnClick()
        {
            if (!this.Enabled) return;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "请选择要打开的数据:";
            openFileDialog.Filter = "Shape Files|*.shp;*.000";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            
            PIE.Carto.IActiveView activeVeiw = m_HookHelper.ActiveView;
            PIE.Carto.IMap map = m_HookHelper.FocusMap;
            PIE.Carto.ILayer layer = null;
            string[] files = openFileDialog.FileNames;
            for (int i = 0; i < files.Length; i++)
            {
                layer = PIE.Carto.LayerFactory.CreateDefaultLayer(files[i]);
                map.AddLayer(layer);
            }
            activeVeiw.PartialRefresh(PIE.Carto.ViewDrawPhaseType.ViewAll);
        }
    }
}
