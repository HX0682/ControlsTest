namespace FormMain1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnMapScale = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
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
            this.toolStrip1.Location = new System.Drawing.Point(4, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(378, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAddRasterData
            // 
            this.btnAddRasterData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddRasterData.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRasterData.Image")));
            this.btnAddRasterData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRasterData.Name = "btnAddRasterData";
            this.btnAddRasterData.Size = new System.Drawing.Size(29, 28);
            this.btnAddRasterData.Text = "添加栅格数据";
            this.btnAddRasterData.Click += new System.EventHandler(this.btnAddRasterData_Click);
            // 
            // btnMapZoomIn
            // 
            this.btnMapZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomIn.Image")));
            this.btnMapZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomIn.Name = "btnMapZoomIn";
            this.btnMapZoomIn.Size = new System.Drawing.Size(29, 28);
            this.btnMapZoomIn.Text = "地图放大";
            this.btnMapZoomIn.Click += new System.EventHandler(this.btnMapZoomIn_Click);
            // 
            // btnMapZoomOut
            // 
            this.btnMapZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomOut.Image")));
            this.btnMapZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomOut.Name = "btnMapZoomOut";
            this.btnMapZoomOut.Size = new System.Drawing.Size(29, 28);
            this.btnMapZoomOut.Text = "地图缩小";
            this.btnMapZoomOut.Click += new System.EventHandler(this.btnMapZoomOut_Click);
            // 
            // btnMapPan
            // 
            this.btnMapPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapPan.Image = ((System.Drawing.Image)(resources.GetObject("btnMapPan.Image")));
            this.btnMapPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapPan.Name = "btnMapPan";
            this.btnMapPan.Size = new System.Drawing.Size(29, 28);
            this.btnMapPan.Text = "地图平移";
            // 
            // btnMapFullExtent
            // 
            this.btnMapFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnMapFullExtent.Image")));
            this.btnMapFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapFullExtent.Name = "btnMapFullExtent";
            this.btnMapFullExtent.Size = new System.Drawing.Size(29, 28);
            this.btnMapFullExtent.Text = "全图显示";
            // 
            // btnMapCenterZoomIn
            // 
            this.btnMapCenterZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapCenterZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnMapCenterZoomIn.Image")));
            this.btnMapCenterZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapCenterZoomIn.Name = "btnMapCenterZoomIn";
            this.btnMapCenterZoomIn.Size = new System.Drawing.Size(29, 28);
            this.btnMapCenterZoomIn.Text = "中心放大";
            // 
            // btnMapCenterZoomOut
            // 
            this.btnMapCenterZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapCenterZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnMapCenterZoomOut.Image")));
            this.btnMapCenterZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapCenterZoomOut.Name = "btnMapCenterZoomOut";
            this.btnMapCenterZoomOut.Size = new System.Drawing.Size(29, 28);
            this.btnMapCenterZoomOut.Text = "中心缩小";
            // 
            // btnMapZoomToNative
            // 
            this.btnMapZoomToNative.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomToNative.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomToNative.Image")));
            this.btnMapZoomToNative.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomToNative.Name = "btnMapZoomToNative";
            this.btnMapZoomToNative.Size = new System.Drawing.Size(29, 28);
            this.btnMapZoomToNative.Text = "1:1显示";
            // 
            // btnMapOutputlmg
            // 
            this.btnMapOutputlmg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMapOutputlmg.Image = ((System.Drawing.Image)(resources.GetObject("btnMapOutputlmg.Image")));
            this.btnMapOutputlmg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapOutputlmg.Name = "btnMapOutputlmg";
            this.btnMapOutputlmg.Size = new System.Drawing.Size(133, 28);
            this.btnMapOutputlmg.Text = "地图显示范围截图";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 419);
            this.splitContainer1.SplitterDistance = 179;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.toolStrip2);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer2.Location = new System.Drawing.Point(3, 245);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer2.TabIndex = 4;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 419);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 450);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMapScale,
            this.toolStripButton10});
            this.toolStrip2.Location = new System.Drawing.Point(0, 123);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(150, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnMapScale
            // 
            this.btnMapScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapScale.Name = "btnMapScale";
            this.btnMapScale.Size = new System.Drawing.Size(0, 24);
            this.btnMapScale.Text = "比例尺";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton10.Text = "toolStripButton10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.ContentPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAddRasterData;
        private System.Windows.Forms.ToolStripButton btnMapZoomIn;
        private System.Windows.Forms.ToolStripButton btnMapZoomOut;
        private System.Windows.Forms.ToolStripButton btnMapPan;
        private System.Windows.Forms.ToolStripButton btnMapFullExtent;
        private System.Windows.Forms.ToolStripButton btnMapCenterZoomIn;
        private System.Windows.Forms.ToolStripButton btnMapCenterZoomOut;
        private System.Windows.Forms.ToolStripButton btnMapZoomToNative;
        private System.Windows.Forms.ToolStripButton btnMapOutputlmg;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel btnMapScale;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
    }
}

