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
            this.closeROIbtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLeftPicBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRightPicBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.alBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.siftBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.sparkBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.lKFAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTkTest = new System.Windows.Forms.ToolStripMenuItem();
            this.kinectToolBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.openKinectBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FusionBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.Show3DBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMatchResultBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateCloudRT = new System.Windows.Forms.ToolStripMenuItem();
            this.plyFileOutputBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.firstPicOpenBtn = new System.Windows.Forms.Button();
            this.firstPicBox = new System.Windows.Forms.PictureBox();
            this.secondPicBox = new System.Windows.Forms.PictureBox();
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
            this.alBtn,
            this.kinectToolBtn});
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
            this.closeROIbtn,
            this.saveLeftPicBtn,
            this.saveRightPicBtn});
            this.operateBtn.Image = ((System.Drawing.Image)(resources.GetObject("operateBtn.Image")));
            this.operateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.operateBtn.Name = "operateBtn";
            this.operateBtn.Size = new System.Drawing.Size(64, 28);
            this.operateBtn.Text = "操作";
            // 
            // roiSelect
            // 
            this.roiSelect.Name = "roiSelect";
            this.roiSelect.Size = new System.Drawing.Size(214, 34);
            this.roiSelect.Text = "ROI选择";
            this.roiSelect.Click += new System.EventHandler(this.roiSelect_Click);
            // 
            // closeROIbtn
            // 
            this.closeROIbtn.Name = "closeROIbtn";
            this.closeROIbtn.Size = new System.Drawing.Size(214, 34);
            this.closeROIbtn.Text = "关闭ROI选择";
            this.closeROIbtn.Click += new System.EventHandler(this.closeROIbtn_Click);
            // 
            // saveLeftPicBtn
            // 
            this.saveLeftPicBtn.Name = "saveLeftPicBtn";
            this.saveLeftPicBtn.Size = new System.Drawing.Size(214, 34);
            this.saveLeftPicBtn.Text = "保存左图";
            this.saveLeftPicBtn.Click += new System.EventHandler(this.saveLeftPicBtn_Click);
            // 
            // saveRightPicBtn
            // 
            this.saveRightPicBtn.Name = "saveRightPicBtn";
            this.saveRightPicBtn.Size = new System.Drawing.Size(214, 34);
            this.saveRightPicBtn.Text = "保存右图";
            this.saveRightPicBtn.Click += new System.EventHandler(this.saveRightPicBtn_Click);
            // 
            // alBtn
            // 
            this.alBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.alBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.siftBtn,
            this.sparkBtn,
            this.lKFAToolStripMenuItem,
            this.openTkTest});
            this.alBtn.Image = ((System.Drawing.Image)(resources.GetObject("alBtn.Image")));
            this.alBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alBtn.Name = "alBtn";
            this.alBtn.Size = new System.Drawing.Size(100, 28);
            this.alBtn.Text = "算法测试";
            // 
            // siftBtn
            // 
            this.siftBtn.Name = "siftBtn";
            this.siftBtn.Size = new System.Drawing.Size(218, 34);
            this.siftBtn.Text = "sift";
            this.siftBtn.Click += new System.EventHandler(this.siftBtn_Click);
            // 
            // sparkBtn
            // 
            this.sparkBtn.Name = "sparkBtn";
            this.sparkBtn.Size = new System.Drawing.Size(218, 34);
            this.sparkBtn.Text = "模拟散斑生成";
            this.sparkBtn.Click += new System.EventHandler(this.sparkBtn_Click);
            // 
            // lKFAToolStripMenuItem
            // 
            this.lKFAToolStripMenuItem.Name = "lKFAToolStripMenuItem";
            this.lKFAToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.lKFAToolStripMenuItem.Text = "LK-FA";
            this.lKFAToolStripMenuItem.Click += new System.EventHandler(this.lKFAToolStripMenuItem_Click);
            // 
            // openTkTest
            // 
            this.openTkTest.Name = "openTkTest";
            this.openTkTest.Size = new System.Drawing.Size(218, 34);
            this.openTkTest.Text = "OpenTK测试";
            this.openTkTest.Click += new System.EventHandler(this.openTkTest_Click);
            // 
            // kinectToolBtn
            // 
            this.kinectToolBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openKinectBtn,
            this.保存ToolStripMenuItem,
            this.FusionBtn,
            this.calculateCloudRT,
            this.plyFileOutputBtn});
            this.kinectToolBtn.Name = "kinectToolBtn";
            this.kinectToolBtn.Size = new System.Drawing.Size(117, 28);
            this.kinectToolBtn.Text = "Kinect测试";
            // 
            // openKinectBtn
            // 
            this.openKinectBtn.Name = "openKinectBtn";
            this.openKinectBtn.Size = new System.Drawing.Size(270, 34);
            this.openKinectBtn.Text = "打开";
            this.openKinectBtn.Click += new System.EventHandler(this.openKinectBtn_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // FusionBtn
            // 
            this.FusionBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Show3DBtn,
            this.ShowMatchResultBtn});
            this.FusionBtn.Name = "FusionBtn";
            this.FusionBtn.Size = new System.Drawing.Size(270, 34);
            this.FusionBtn.Text = "融合";
            this.FusionBtn.Click += new System.EventHandler(this.FusionBtn_Click);
            // 
            // Show3DBtn
            // 
            this.Show3DBtn.Name = "Show3DBtn";
            this.Show3DBtn.Size = new System.Drawing.Size(270, 34);
            this.Show3DBtn.Text = "3D(暂时无用)";
            this.Show3DBtn.Click += new System.EventHandler(this.Show3DBtn_Click);
            // 
            // ShowMatchResultBtn
            // 
            this.ShowMatchResultBtn.Name = "ShowMatchResultBtn";
            this.ShowMatchResultBtn.Size = new System.Drawing.Size(270, 34);
            this.ShowMatchResultBtn.Text = "显示匹配结果";
            this.ShowMatchResultBtn.Click += new System.EventHandler(this.ShowMatchResultBtn_Click);
            // 
            // calculateCloudRT
            // 
            this.calculateCloudRT.Name = "calculateCloudRT";
            this.calculateCloudRT.Size = new System.Drawing.Size(270, 34);
            this.calculateCloudRT.Text = "点云计算RT";
            this.calculateCloudRT.Click += new System.EventHandler(this.calculateCloudRT_Click);
            // 
            // plyFileOutputBtn
            // 
            this.plyFileOutputBtn.Name = "plyFileOutputBtn";
            this.plyFileOutputBtn.Size = new System.Drawing.Size(270, 34);
            this.plyFileOutputBtn.Text = "输出ply文件";
            this.plyFileOutputBtn.Click += new System.EventHandler(this.plyFileOutputBtn_Click);
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
            this.firstPicBox.Location = new System.Drawing.Point(16, 126);
            this.firstPicBox.Name = "firstPicBox";
            this.firstPicBox.Size = new System.Drawing.Size(500, 400);
            this.firstPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.firstPicBox.TabIndex = 3;
            this.firstPicBox.TabStop = false;
            // 
            // secondPicBox
            // 
            this.secondPicBox.Location = new System.Drawing.Point(522, 126);
            this.secondPicBox.Name = "secondPicBox";
            this.secondPicBox.Size = new System.Drawing.Size(500, 400);
            this.secondPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.secondPicBox.TabIndex = 4;
            this.secondPicBox.TabStop = false;
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
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripDropDownButton kinectToolBtn;
        private System.Windows.Forms.ToolStripMenuItem openKinectBtn;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FusionBtn;
        private System.Windows.Forms.ToolStripMenuItem calculateCloudRT;
        private System.Windows.Forms.ToolStripMenuItem ShowMatchResultBtn;
        private System.Windows.Forms.ToolStripMenuItem closeROIbtn;
        private System.Windows.Forms.ToolStripMenuItem Show3DBtn;
        private System.Windows.Forms.ToolStripMenuItem openTkTest;
        private System.Windows.Forms.ToolStripMenuItem saveRightPicBtn;
        private System.Windows.Forms.ToolStripMenuItem plyFileOutputBtn;
    }
}

