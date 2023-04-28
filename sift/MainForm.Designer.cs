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
            this.可变圆匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.y旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.z旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.采样半径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.与哪一张图比0开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_angle_x = new System.Windows.Forms.ToolStripTextBox();
            this.variableCircle_angle_y = new System.Windows.Forms.ToolStripTextBox();
            this.variableCircle_angle_z = new System.Windows.Forms.ToolStripTextBox();
            this.variableCircle_sample_r = new System.Windows.Forms.ToolStripTextBox();
            this.variableCircle_ref_index = new System.Windows.Forms.ToolStripTextBox();
            this.VariableCircleMatchBegin = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索半径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_search_size = new System.Windows.Forms.ToolStripTextBox();
            this.相关性限制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_limit_r = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.服务端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.kinectToolBtn,
            this.toolStripButton1});
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
            this.roiSelect.Size = new System.Drawing.Size(270, 34);
            this.roiSelect.Text = "ROI选择";
            this.roiSelect.Click += new System.EventHandler(this.roiSelect_Click);
            // 
            // closeROIbtn
            // 
            this.closeROIbtn.Name = "closeROIbtn";
            this.closeROIbtn.Size = new System.Drawing.Size(270, 34);
            this.closeROIbtn.Text = "关闭ROI选择";
            this.closeROIbtn.Click += new System.EventHandler(this.closeROIbtn_Click);
            // 
            // saveLeftPicBtn
            // 
            this.saveLeftPicBtn.Name = "saveLeftPicBtn";
            this.saveLeftPicBtn.Size = new System.Drawing.Size(270, 34);
            this.saveLeftPicBtn.Text = "保存左图";
            this.saveLeftPicBtn.Click += new System.EventHandler(this.saveLeftPicBtn_Click);
            // 
            // saveRightPicBtn
            // 
            this.saveRightPicBtn.Name = "saveRightPicBtn";
            this.saveRightPicBtn.Size = new System.Drawing.Size(270, 34);
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
            this.plyFileOutputBtn,
            this.可变圆匹配ToolStripMenuItem});
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
            // 可变圆匹配ToolStripMenuItem
            // 
            this.可变圆匹配ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.VariableCircleMatchBegin});
            this.可变圆匹配ToolStripMenuItem.Name = "可变圆匹配ToolStripMenuItem";
            this.可变圆匹配ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.可变圆匹配ToolStripMenuItem.Text = "可变圆匹配";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x旋转弧度ToolStripMenuItem,
            this.y旋转弧度ToolStripMenuItem,
            this.z旋转弧度ToolStripMenuItem,
            this.搜索半径ToolStripMenuItem,
            this.采样半径ToolStripMenuItem,
            this.与哪一张图比0开始ToolStripMenuItem,
            this.相关性限制ToolStripMenuItem});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // x旋转弧度ToolStripMenuItem
            // 
            this.x旋转弧度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_angle_x});
            this.x旋转弧度ToolStripMenuItem.Name = "x旋转弧度ToolStripMenuItem";
            this.x旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.x旋转弧度ToolStripMenuItem.Text = "x旋转弧度";
            // 
            // y旋转弧度ToolStripMenuItem
            // 
            this.y旋转弧度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_angle_y});
            this.y旋转弧度ToolStripMenuItem.Name = "y旋转弧度ToolStripMenuItem";
            this.y旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.y旋转弧度ToolStripMenuItem.Text = "y旋转弧度";
            // 
            // z旋转弧度ToolStripMenuItem
            // 
            this.z旋转弧度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_angle_z});
            this.z旋转弧度ToolStripMenuItem.Name = "z旋转弧度ToolStripMenuItem";
            this.z旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.z旋转弧度ToolStripMenuItem.Text = "z旋转弧度";
            // 
            // 采样半径ToolStripMenuItem
            // 
            this.采样半径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_sample_r});
            this.采样半径ToolStripMenuItem.Name = "采样半径ToolStripMenuItem";
            this.采样半径ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.采样半径ToolStripMenuItem.Text = "采样半径";
            // 
            // 与哪一张图比0开始ToolStripMenuItem
            // 
            this.与哪一张图比0开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_ref_index});
            this.与哪一张图比0开始ToolStripMenuItem.Name = "与哪一张图比0开始ToolStripMenuItem";
            this.与哪一张图比0开始ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.与哪一张图比0开始ToolStripMenuItem.Text = "与哪一张图比(0开始)";
            // 
            // variableCircle_angle_x
            // 
            this.variableCircle_angle_x.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_x.Name = "variableCircle_angle_x";
            this.variableCircle_angle_x.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_x.Text = "0";
            // 
            // variableCircle_angle_y
            // 
            this.variableCircle_angle_y.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_y.Name = "variableCircle_angle_y";
            this.variableCircle_angle_y.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_y.Text = "0";
            // 
            // variableCircle_angle_z
            // 
            this.variableCircle_angle_z.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_z.Name = "variableCircle_angle_z";
            this.variableCircle_angle_z.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_z.Text = "0";
            // 
            // variableCircle_sample_r
            // 
            this.variableCircle_sample_r.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_sample_r.Name = "variableCircle_sample_r";
            this.variableCircle_sample_r.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_sample_r.Text = "10";
            // 
            // variableCircle_ref_index
            // 
            this.variableCircle_ref_index.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_ref_index.Name = "variableCircle_ref_index";
            this.variableCircle_ref_index.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_ref_index.Text = "-1";
            // 
            // VariableCircleMatchBegin
            // 
            this.VariableCircleMatchBegin.Name = "VariableCircleMatchBegin";
            this.VariableCircleMatchBegin.Size = new System.Drawing.Size(270, 34);
            this.VariableCircleMatchBegin.Text = "开始";
            this.VariableCircleMatchBegin.Click += new System.EventHandler(this.VariableCircleMatchBegin_Click);
            // 
            // 搜索半径ToolStripMenuItem
            // 
            this.搜索半径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_search_size});
            this.搜索半径ToolStripMenuItem.Name = "搜索半径ToolStripMenuItem";
            this.搜索半径ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.搜索半径ToolStripMenuItem.Text = "搜索半径";
            // 
            // variableCircle_search_size
            // 
            this.variableCircle_search_size.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_search_size.Name = "variableCircle_search_size";
            this.variableCircle_search_size.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_search_size.Text = "10";
            // 
            // 相关性限制ToolStripMenuItem
            // 
            this.相关性限制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_limit_r});
            this.相关性限制ToolStripMenuItem.Name = "相关性限制ToolStripMenuItem";
            this.相关性限制ToolStripMenuItem.Size = new System.Drawing.Size(277, 34);
            this.相关性限制ToolStripMenuItem.Text = "相关性限制";
            // 
            // variableCircle_limit_r
            // 
            this.variableCircle_limit_r.Name = "variableCircle_limit_r";
            this.variableCircle_limit_r.Size = new System.Drawing.Size(270, 34);
            this.variableCircle_limit_r.Text = "0.3";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.服务端ToolStripMenuItem,
            this.客户端ToolStripMenuItem});
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(83, 28);
            this.toolStripButton1.Text = "socket";
            // 
            // 服务端ToolStripMenuItem
            // 
            this.服务端ToolStripMenuItem.Name = "服务端ToolStripMenuItem";
            this.服务端ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.服务端ToolStripMenuItem.Text = "服务端";
            this.服务端ToolStripMenuItem.Click += new System.EventHandler(this.服务端ToolStripMenuItem_Click);
            // 
            // 客户端ToolStripMenuItem
            // 
            this.客户端ToolStripMenuItem.Name = "客户端ToolStripMenuItem";
            this.客户端ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.客户端ToolStripMenuItem.Text = "客户端";
            this.客户端ToolStripMenuItem.Click += new System.EventHandler(this.客户端ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripMenuItem 可变圆匹配ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x旋转弧度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_angle_x;
        private System.Windows.Forms.ToolStripMenuItem y旋转弧度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_angle_y;
        private System.Windows.Forms.ToolStripMenuItem z旋转弧度ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_angle_z;
        private System.Windows.Forms.ToolStripMenuItem 采样半径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_sample_r;
        private System.Windows.Forms.ToolStripMenuItem 与哪一张图比0开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_ref_index;
        private System.Windows.Forms.ToolStripMenuItem VariableCircleMatchBegin;
        private System.Windows.Forms.ToolStripMenuItem 搜索半径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_search_size;
        private System.Windows.Forms.ToolStripMenuItem 相关性限制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem variableCircle_limit_r;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 服务端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户端ToolStripMenuItem;
    }
}

