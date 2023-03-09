namespace sift
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.operateBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.roiSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLeftPicBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.alBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.siftBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.sparkBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.firstPicOpenBtn = new System.Windows.Forms.Button();
            this.firstPicBox = new System.Windows.Forms.PictureBox();
            this.secondPicBox = new System.Windows.Forms.PictureBox();
            this.lKFAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operateBtn,
            this.alBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1034, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // operateBtn
            // 
            this.operateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.operateBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roiSelect,
            this.saveLeftPicBtn});
            this.operateBtn.Image = ((System.Drawing.Image)(resources.GetObject("operateBtn.Image")));
            this.operateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.operateBtn.Name = "operateBtn";
            this.operateBtn.Size = new System.Drawing.Size(64, 28);
            this.operateBtn.Text = "操作";
            // 
            // roiSelect
            // 
            this.roiSelect.Name = "roiSelect";
            this.roiSelect.Size = new System.Drawing.Size(270, 34);
            this.roiSelect.Text = "ROI选择";
            this.roiSelect.Click += new System.EventHandler(this.roiSelect_Click);
            // 
            // saveLeftPicBtn
            // 
            this.saveLeftPicBtn.Name = "saveLeftPicBtn";
            this.saveLeftPicBtn.Size = new System.Drawing.Size(270, 34);
            this.saveLeftPicBtn.Text = "保存左图";
            this.saveLeftPicBtn.Click += new System.EventHandler(this.saveLeftPicBtn_Click);
            // 
            // alBtn
            // 
            this.alBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.alBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siftBtn,
            this.sparkBtn,
            this.lKFAToolStripMenuItem});
            this.alBtn.Image = ((System.Drawing.Image)(resources.GetObject("alBtn.Image")));
            this.alBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alBtn.Name = "alBtn";
            this.alBtn.Size = new System.Drawing.Size(100, 28);
            this.alBtn.Text = "算法测试";
            // 
            // siftBtn
            // 
            this.siftBtn.Name = "siftBtn";
            this.siftBtn.Size = new System.Drawing.Size(270, 34);
            this.siftBtn.Text = "sift";
            this.siftBtn.Click += new System.EventHandler(this.siftBtn_Click);
            // 
            // sparkBtn
            // 
            this.sparkBtn.Name = "sparkBtn";
            this.sparkBtn.Size = new System.Drawing.Size(270, 34);
            this.sparkBtn.Text = "模拟散斑生成";
            this.sparkBtn.Click += new System.EventHandler(this.sparkBtn_Click);
            // 
            // firstPicOpenBtn
            // 
            this.firstPicOpenBtn.Location = new System.Drawing.Point(12, 36);
            this.firstPicOpenBtn.Name = "firstPicOpenBtn";
            this.firstPicOpenBtn.Size = new System.Drawing.Size(75, 31);
            this.firstPicOpenBtn.TabIndex = 1;
            this.firstPicOpenBtn.Text = "open";
            this.firstPicOpenBtn.UseVisualStyleBackColor = true;
            this.firstPicOpenBtn.Click += new System.EventHandler(this.firstPicOpenBtn_Click);
            // 
            // firstPicBox
            // 
            this.firstPicBox.Location = new System.Drawing.Point(12, 73);
            this.firstPicBox.Name = "firstPicBox";
            this.firstPicBox.Size = new System.Drawing.Size(500, 400);
            this.firstPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.firstPicBox.TabIndex = 3;
            this.firstPicBox.TabStop = false;
            // 
            // secondPicBox
            // 
            this.secondPicBox.Location = new System.Drawing.Point(518, 73);
            this.secondPicBox.Name = "secondPicBox";
            this.secondPicBox.Size = new System.Drawing.Size(500, 400);
            this.secondPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.secondPicBox.TabIndex = 4;
            this.secondPicBox.TabStop = false;
            // 
            // lKFAToolStripMenuItem
            // 
            this.lKFAToolStripMenuItem.Name = "lKFAToolStripMenuItem";
            this.lKFAToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.lKFAToolStripMenuItem.Text = "LK-FA";
            this.lKFAToolStripMenuItem.Click += new System.EventHandler(this.lKFAToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1034, 587);
            this.Controls.Add(this.secondPicBox);
            this.Controls.Add(this.firstPicBox);
            this.Controls.Add(this.firstPicOpenBtn);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "主窗口";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button firstPicOpenBtn;
        private System.Windows.Forms.PictureBox firstPicBox;
        private System.Windows.Forms.PictureBox secondPicBox;
        private System.Windows.Forms.ToolStripDropDownButton alBtn;
        private System.Windows.Forms.ToolStripMenuItem siftBtn;
        private System.Windows.Forms.ToolStripMenuItem sparkBtn;
        private System.Windows.Forms.ToolStripDropDownButton operateBtn;
        private System.Windows.Forms.ToolStripMenuItem roiSelect;
        private System.Windows.Forms.ToolStripMenuItem saveLeftPicBtn;
        private System.Windows.Forms.ToolStripMenuItem lKFAToolStripMenuItem;
    }
}

