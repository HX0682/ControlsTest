namespace 加载更多数据
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn1 = new System.Windows.Forms.ToolStripButton();
            this.btn2 = new System.Windows.Forms.ToolStripButton();
            this.btn3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn4 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn1,
            this.btn2,
            this.btn3,
            this.btn4});
            this.toolStrip1.Location = new System.Drawing.Point(9, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(625, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn1
            // 
            this.btn1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(125, 24);
            this.btn1.Text = "添加Micaps数据";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(148, 24);
            this.btn2.Text = "添加长时间序列数据";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(152, 24);
            this.btn3.Text = "添加自定义矢量数据 ";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 39);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(776, 399);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 3;
            // 
            // btn4
            // 
            this.btn4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(148, 24);
            this.btn4.Text = "加载自定义栅格数据";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn1;
        private System.Windows.Forms.ToolStripButton btn2;
        private System.Windows.Forms.ToolStripButton btn3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btn4;
    }
}

