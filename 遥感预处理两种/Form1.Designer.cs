namespace 遥感预处理两种
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.btnMapZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnMapZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnMapPan = new System.Windows.Forms.ToolStripButton();
            this.btnMapFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnAddRasterData = new System.Windows.Forms.ToolStripButton();
            this.btn1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddRasterData,
            this.btn1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1182, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip3);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 728);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMapZoomIn,
            this.btnMapZoomOut,
            this.btnMapPan,
            this.btnMapFullExtent});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.MinimumSize = new System.Drawing.Size(40, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(40, 728);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripLabel1});
            this.toolStrip3.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip3.Location = new System.Drawing.Point(40, 703);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(896, 25);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(122, 22);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // btnMapZoomIn
            // 
            this.btnMapZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomIn.Image")));
            this.btnMapZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomIn.Name = "btnMapZoomIn";
            this.btnMapZoomIn.Size = new System.Drawing.Size(27, 24);
            this.btnMapZoomIn.Text = "地图放大";
            this.btnMapZoomIn.Click += new System.EventHandler(this.btnMapZoomIn_Click);
            // 
            // btnMapZoomOut
            // 
            this.btnMapZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnMapZoomOut.Image")));
            this.btnMapZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapZoomOut.Name = "btnMapZoomOut";
            this.btnMapZoomOut.Size = new System.Drawing.Size(27, 24);
            this.btnMapZoomOut.Text = "地图缩小";
            this.btnMapZoomOut.Click += new System.EventHandler(this.btnMapZoomOut_Click);
            // 
            // btnMapPan
            // 
            this.btnMapPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapPan.Image = ((System.Drawing.Image)(resources.GetObject("btnMapPan.Image")));
            this.btnMapPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapPan.Name = "btnMapPan";
            this.btnMapPan.Size = new System.Drawing.Size(27, 24);
            this.btnMapPan.Text = "地图平移";
            this.btnMapPan.Click += new System.EventHandler(this.btnMapPan_Click);
            // 
            // btnMapFullExtent
            // 
            this.btnMapFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMapFullExtent.Image = ((System.Drawing.Image)(resources.GetObject("btnMapFullExtent.Image")));
            this.btnMapFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapFullExtent.Name = "btnMapFullExtent";
            this.btnMapFullExtent.Size = new System.Drawing.Size(27, 24);
            this.btnMapFullExtent.Text = "全图显示";
            this.btnMapFullExtent.Click += new System.EventHandler(this.btnMapFullExtent_Click);
            // 
            // btnAddRasterData
            // 
            this.btnAddRasterData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddRasterData.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRasterData.Image")));
            this.btnAddRasterData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRasterData.Name = "btnAddRasterData";
            this.btnAddRasterData.Size = new System.Drawing.Size(103, 22);
            this.btnAddRasterData.Text = "添加栅格数据";
            this.btnAddRasterData.Click += new System.EventHandler(this.btnAddRasterData_Click);
            // 
            // btn1
            // 
            this.btn1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn1.Image = ((System.Drawing.Image)(resources.GetObject("btn1.Image")));
            this.btn1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(52, 22);
            this.btn1.Text = "功能1";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnMapZoomIn;
        private System.Windows.Forms.ToolStripButton btnMapZoomOut;
        private System.Windows.Forms.ToolStripButton btnMapPan;
        private System.Windows.Forms.ToolStripButton btnMapFullExtent;
        private System.Windows.Forms.ToolStripButton btnAddRasterData;
        private System.Windows.Forms.ToolStripButton btn1;
    }
}

