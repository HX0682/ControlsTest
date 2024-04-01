namespace 实验三数据基础操作
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
            this.btn1 = new System.Windows.Forms.ToolStripButton();
            this.Resume = new System.Windows.Forms.ToolStripButton();
            this.Pause = new System.Windows.Forms.ToolStripButton();
            this.Stop = new System.Windows.Forms.ToolStripButton();
            this.btn2 = new System.Windows.Forms.ToolStripButton();
            this.btn3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn4 = new System.Windows.Forms.ToolStripButton();
            this.btn5 = new System.Windows.Forms.ToolStripButton();
            this.btn6 = new System.Windows.Forms.ToolStripButton();
            this.btn7 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn1,
            this.Resume,
            this.Pause,
            this.Stop,
            this.btn2,
            this.btn3,
            this.btn4,
            this.btn5,
            this.btn6,
            this.btn7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1110, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn1
            // 
            this.btn1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn1.Image = ((System.Drawing.Image)(resources.GetObject("btn1.Image")));
            this.btn1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(148, 28);
            this.btn1.Text = "添加长时间序列数据";
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // Resume
            // 
            this.Resume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Resume.Image = ((System.Drawing.Image)(resources.GetObject("Resume.Image")));
            this.Resume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Resume.Name = "Resume";
            this.Resume.Size = new System.Drawing.Size(43, 28);
            this.Resume.Text = "继续";
            this.Resume.Click += new System.EventHandler(this.Resume_Click);
            // 
            // Pause
            // 
            this.Pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Pause.Image = ((System.Drawing.Image)(resources.GetObject("Pause.Image")));
            this.Pause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(43, 28);
            this.Pause.Text = "暂停";
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // Stop
            // 
            this.Stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Stop.Image = ((System.Drawing.Image)(resources.GetObject("Stop.Image")));
            this.Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(43, 28);
            this.Stop.Text = "停止";
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // btn2
            // 
            this.btn2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn2.Image = ((System.Drawing.Image)(resources.GetObject("btn2.Image")));
            this.btn2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(73, 28);
            this.btn2.Text = "动画导出";
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // btn3
            // 
            this.btn3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn3.Image = ((System.Drawing.Image)(resources.GetObject("btn3.Image")));
            this.btn3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(118, 28);
            this.btn3.Text = "打开复合数据集";
            this.btn3.Click += new System.EventHandler(this.btn3_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1110, 612);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn4
            // 
            this.btn4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn4.Image = ((System.Drawing.Image)(resources.GetObject("btn4.Image")));
            this.btn4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(115, 28);
            this.btn4.Text = "Alpha通道渲染";
            this.btn4.Click += new System.EventHandler(this.btn4_Click);
            // 
            // btn5
            // 
            this.btn5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn5.Image = ((System.Drawing.Image)(resources.GetObject("btn5.Image")));
            this.btn5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(73, 28);
            this.btn5.Text = "图层属性";
            this.btn5.Click += new System.EventHandler(this.btn5_Click);
            // 
            // btn6
            // 
            this.btn6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn6.Image = ((System.Drawing.Image)(resources.GetObject("btn6.Image")));
            this.btn6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(88, 28);
            this.btn6.Text = "矢量数据表";
            this.btn6.Click += new System.EventHandler(this.btn6_Click);
            // 
            // btn7
            // 
            this.btn7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btn7.Image = ((System.Drawing.Image)(resources.GetObject("btn7.Image")));
            this.btn7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(43, 28);
            this.btn7.Text = "卷帘";
            this.btn7.Click += new System.EventHandler(this.btn7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1110, 643);
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
        private System.Windows.Forms.ToolStripButton Resume;
        private System.Windows.Forms.ToolStripButton Pause;
        private System.Windows.Forms.ToolStripButton Stop;
        private System.Windows.Forms.ToolStripButton btn2;
        private System.Windows.Forms.ToolStripButton btn3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btn4;
        private System.Windows.Forms.ToolStripButton btn5;
        private System.Windows.Forms.ToolStripButton btn6;
        private System.Windows.Forms.ToolStripButton btn7;
    }
}

