namespace FormMain
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAddRasterData = new System.Windows.Forms.ToolStripButton();
            this.btnMapZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnMapZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnMapPan = new System.Windows.Forms.ToolStripButton();
            this.btnMapFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnMapCenterZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnMapCenterZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnMapZoomToNative = new System.Windows.Forms.ToolStripButton();
            this.btnMapOutputlmg = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnMapScale = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRasterData,
            this.btnMapZoomIn,
            this.btnMapZoomOut,
            this.btnMapPan,
            this.btnMapFullExtent,
            this.btnMapCenterZoomIn,
            this.btnMapCenterZoomOut,
            this.btnMapZoomToNative,
            this.btnMapOutputlmg});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(834, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddRasterData
            // 
            this.btnAddRasterData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRasterData.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRasterData.Image")));
            this.btnAddRasterData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRasterData.Name = "btnAddRasterData";
            this.btnAddRasterData.Size = new System.Drawing.Size(29, 24);
            this.btnAddRasterData.Text = "添加栅格数据";
            this.btnAddRasterData.Click += new System.EventHandler(this.btnAddRasterData_Click_1);
            // 
            // btnMapZoomIn
            // 
            this.btnMapZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomIn.Image")));
            this.btnMapZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomIn.Name = "btnMapZoomIn";
            this.btnMapZoomIn.Size = new System.Drawing.Size(29, 24);
            this.btnMapZoomIn.Text = "地图放大";
            // 
            // btnMapZoomOut
            // 
            this.btnMapZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomOut.Image")));
            this.btnMapZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomOut.Name = "btnMapZoomOut";
            this.btnMapZoomOut.Size = new System.Drawing.Size(29, 24);
            this.btnMapZoomOut.Text = "地图缩小";
            // 
            // btnMapPan
            // 
            this.btnMapPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapPan.Image = ((System.Drawing.Image)(resources.GetObject("btnMapPan.Image")));
            this.btnMapPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapPan.Name = "btnMapPan";
            this.btnMapPan.Size = new System.Drawing.Size(29, 24);
            this.btnMapPan.Text = "地图平移";
            this.btnMapPan.Click += new System.EventHandler(this.btnMapPan_Click);
            // 
            // btnMapFullExtent
            // 
            this.btnMapFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnMapFullExtent.Image")));
            this.btnMapFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapFullExtent.Name = "btnMapFullExtent";
            this.btnMapFullExtent.Size = new System.Drawing.Size(29, 24);
            this.btnMapFullExtent.Text = "全图显示";
            this.btnMapFullExtent.Click += new System.EventHandler(this.btnMapFullExtent_Click);
            // 
            // btnMapCenterZoomIn
            // 
            this.btnMapCenterZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapCenterZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnMapCenterZoomIn.Image")));
            this.btnMapCenterZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapCenterZoomIn.Name = "btnMapCenterZoomIn";
            this.btnMapCenterZoomIn.Size = new System.Drawing.Size(29, 24);
            this.btnMapCenterZoomIn.Text = "中心放大";
            this.btnMapCenterZoomIn.Click += new System.EventHandler(this.btnMapCenterZoomIn_Click);
            // 
            // btnMapCenterZoomOut
            // 
            this.btnMapCenterZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapCenterZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnMapCenterZoomOut.Image")));
            this.btnMapCenterZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapCenterZoomOut.Name = "btnMapCenterZoomOut";
            this.btnMapCenterZoomOut.Size = new System.Drawing.Size(29, 24);
            this.btnMapCenterZoomOut.Text = "中心缩小";
            this.btnMapCenterZoomOut.Click += new System.EventHandler(this.btnMapCenterZoomOut_Click);
            // 
            // btnMapZoomToNative
            // 
            this.btnMapZoomToNative.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomToNative.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomToNative.Image")));
            this.btnMapZoomToNative.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomToNative.Name = "btnMapZoomToNative";
            this.btnMapZoomToNative.Size = new System.Drawing.Size(29, 24);
            this.btnMapZoomToNative.Text = "1:1显示";
            this.btnMapZoomToNative.Click += new System.EventHandler(this.btnMapZoomToNative_Click);
            // 
            // btnMapOutputlmg
            // 
            this.btnMapOutputlmg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMapOutputlmg.Image = ((System.Drawing.Image)(resources.GetObject("btnMapOutputlmg.Image")));
            this.btnMapOutputlmg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapOutputlmg.Name = "btnMapOutputlmg";
            this.btnMapOutputlmg.Size = new System.Drawing.Size(133, 24);
            this.btnMapOutputlmg.Text = "地图显示范围截图";
            this.btnMapOutputlmg.Click += new System.EventHandler(this.btnMapOutputlmg_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(834, 437);
            this.splitContainer1.SplitterDistance = 174;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMapScale,
            this.toolStripButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 410);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(656, 27);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnMapScale
            // 
            this.btnMapScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMapScale.Name = "btnMapScale";
            this.btnMapScale.Size = new System.Drawing.Size(58, 24);
            this.btnMapScale.Text = "比例尺:";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 464);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FormMain";
            this.Text = "PIE演示程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btnAddRasterData;
        private System.Windows.Forms.ToolStripButton btnMapZoomIn;
        private System.Windows.Forms.ToolStripButton btnMapZoomOut;
        private System.Windows.Forms.ToolStripButton btnMapPan;
        private System.Windows.Forms.ToolStripButton btnMapFullExtent;
        private System.Windows.Forms.ToolStripButton btnMapCenterZoomIn;
        private System.Windows.Forms.ToolStripButton btnMapCenterZoomOut;
        private System.Windows.Forms.ToolStripButton btnMapZoomToNative;
        private System.Windows.Forms.ToolStripButton btnMapOutputlmg;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel btnMapScale;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

