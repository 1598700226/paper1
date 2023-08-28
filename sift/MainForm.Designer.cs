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
            this.iCPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择两幅ply点云ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.对应点距离约束ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitDistanceText = new System.Windows.Forms.ToolStripTextBox();
            this.初始旋转ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.icp_init_rotation = new System.Windows.Forms.ToolStripTextBox();
            this.初始位移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.icp_init_translation = new System.Windows.Forms.ToolStripTextBox();
            this.iCP配准开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iCP配准开始点对面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.两幅ply点云融合并降采样ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kinectToolBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.openKinectBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FusionBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.dIC设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.子区大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dic_subsize = new System.Windows.Forms.ToolStripTextBox();
            this.dic搜索区域 = new System.Windows.Forms.ToolStripMenuItem();
            this.dic_searchsize = new System.Windows.Forms.ToolStripTextBox();
            this.dic相关性阈值 = new System.Windows.Forms.ToolStripMenuItem();
            this.dic_limitR = new System.Windows.Forms.ToolStripTextBox();
            this.Show3DBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMatchResultBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.calculateCloudRT = new System.Windows.Forms.ToolStripMenuItem();
            this.plyFileOutputBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.可变圆匹配ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_angle_x = new System.Windows.Forms.ToolStripTextBox();
            this.y旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_angle_y = new System.Windows.Forms.ToolStripTextBox();
            this.z旋转弧度ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_angle_z = new System.Windows.Forms.ToolStripTextBox();
            this.isManuallySetRotation = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索半径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_search_size = new System.Windows.Forms.ToolStripTextBox();
            this.采样半径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_sample_r = new System.Windows.Forms.ToolStripTextBox();
            this.与哪一张图比0开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_ref_index = new System.Windows.Forms.ToolStripTextBox();
            this.相关性限制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variableCircle_limit_r = new System.Windows.Forms.ToolStripTextBox();
            this.VariableCircleMatchBegin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.服务端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.客户端ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.打开待配准图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.kinectToolBtn,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1034, 38);
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
            this.operateBtn.Size = new System.Drawing.Size(64, 33);
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
            this.openTkTest,
            this.iCPToolStripMenuItem});
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
            // iCPToolStripMenuItem
            // 
            this.iCPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择两幅ply点云ToolStripMenuItem,
            this.参数设置ToolStripMenuItem1,
            this.iCP配准开始ToolStripMenuItem,
            this.iCP配准开始点对面ToolStripMenuItem,
            this.两幅ply点云融合并降采样ToolStripMenuItem});
            this.iCPToolStripMenuItem.Name = "iCPToolStripMenuItem";
            this.iCPToolStripMenuItem.Size = new System.Drawing.Size(218, 34);
            this.iCPToolStripMenuItem.Text = "ICP";
            // 
            // 选择两幅ply点云ToolStripMenuItem
            // 
            this.选择两幅ply点云ToolStripMenuItem.Name = "选择两幅ply点云ToolStripMenuItem";
            this.选择两幅ply点云ToolStripMenuItem.Size = new System.Drawing.Size(317, 34);
            this.选择两幅ply点云ToolStripMenuItem.Text = "选择两幅ply点云";
            this.选择两幅ply点云ToolStripMenuItem.Click += new System.EventHandler(this.选择两幅ply点云ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem1
            // 
            this.参数设置ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.对应点距离约束ToolStripMenuItem,
            this.初始旋转ToolStripMenuItem,
            this.初始位移ToolStripMenuItem});
            this.参数设置ToolStripMenuItem1.Name = "参数设置ToolStripMenuItem1";
            this.参数设置ToolStripMenuItem1.Size = new System.Drawing.Size(317, 34);
            this.参数设置ToolStripMenuItem1.Text = "参数设置";
            // 
            // 对应点距离约束ToolStripMenuItem
            // 
            this.对应点距离约束ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limitDistanceText});
            this.对应点距离约束ToolStripMenuItem.Name = "对应点距离约束ToolStripMenuItem";
            this.对应点距离约束ToolStripMenuItem.Size = new System.Drawing.Size(236, 34);
            this.对应点距离约束ToolStripMenuItem.Text = "对应点距离约束";
            // 
            // limitDistanceText
            // 
            this.limitDistanceText.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.limitDistanceText.Name = "limitDistanceText";
            this.limitDistanceText.Size = new System.Drawing.Size(270, 30);
            this.limitDistanceText.Text = "25";
            // 
            // 初始旋转ToolStripMenuItem
            // 
            this.初始旋转ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icp_init_rotation});
            this.初始旋转ToolStripMenuItem.Name = "初始旋转ToolStripMenuItem";
            this.初始旋转ToolStripMenuItem.Size = new System.Drawing.Size(236, 34);
            this.初始旋转ToolStripMenuItem.Text = "初始旋转";
            // 
            // icp_init_rotation
            // 
            this.icp_init_rotation.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.icp_init_rotation.Name = "icp_init_rotation";
            this.icp_init_rotation.Size = new System.Drawing.Size(270, 30);
            this.icp_init_rotation.Text = "1 0 0 0 1 0 0 0 1";
            // 
            // 初始位移ToolStripMenuItem
            // 
            this.初始位移ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icp_init_translation});
            this.初始位移ToolStripMenuItem.Name = "初始位移ToolStripMenuItem";
            this.初始位移ToolStripMenuItem.Size = new System.Drawing.Size(236, 34);
            this.初始位移ToolStripMenuItem.Text = "初始位移";
            // 
            // icp_init_translation
            // 
            this.icp_init_translation.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.icp_init_translation.Name = "icp_init_translation";
            this.icp_init_translation.Size = new System.Drawing.Size(100, 30);
            this.icp_init_translation.Text = "0 0 0";
            // 
            // iCP配准开始ToolStripMenuItem
            // 
            this.iCP配准开始ToolStripMenuItem.Name = "iCP配准开始ToolStripMenuItem";
            this.iCP配准开始ToolStripMenuItem.Size = new System.Drawing.Size(317, 34);
            this.iCP配准开始ToolStripMenuItem.Text = "ICP配准开始(点对点)";
            this.iCP配准开始ToolStripMenuItem.Click += new System.EventHandler(this.ICP配准开始ToolStripMenuItem_Click);
            // 
            // iCP配准开始点对面ToolStripMenuItem
            // 
            this.iCP配准开始点对面ToolStripMenuItem.Name = "iCP配准开始点对面ToolStripMenuItem";
            this.iCP配准开始点对面ToolStripMenuItem.Size = new System.Drawing.Size(317, 34);
            this.iCP配准开始点对面ToolStripMenuItem.Text = "ICP配准开始(点对面)";
            this.iCP配准开始点对面ToolStripMenuItem.Click += new System.EventHandler(this.iCP配准开始点对面ToolStripMenuItem_Click);
            // 
            // 两幅ply点云融合并降采样ToolStripMenuItem
            // 
            this.两幅ply点云融合并降采样ToolStripMenuItem.Name = "两幅ply点云融合并降采样ToolStripMenuItem";
            this.两幅ply点云融合并降采样ToolStripMenuItem.Size = new System.Drawing.Size(317, 34);
            this.两幅ply点云融合并降采样ToolStripMenuItem.Text = "两幅ply点云融合并降采样";
            this.两幅ply点云融合并降采样ToolStripMenuItem.Click += new System.EventHandler(this.两幅ply点云融合并降采样ToolStripMenuItem_Click);
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
            this.dIC设置ToolStripMenuItem,
            this.Show3DBtn,
            this.ShowMatchResultBtn});
            this.FusionBtn.Name = "FusionBtn";
            this.FusionBtn.Size = new System.Drawing.Size(270, 34);
            this.FusionBtn.Text = "融合";
            this.FusionBtn.Click += new System.EventHandler(this.FusionBtn_Click);
            // 
            // dIC设置ToolStripMenuItem
            // 
            this.dIC设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.子区大小ToolStripMenuItem,
            this.dic搜索区域,
            this.dic相关性阈值});
            this.dIC设置ToolStripMenuItem.Name = "dIC设置ToolStripMenuItem";
            this.dIC设置ToolStripMenuItem.Size = new System.Drawing.Size(219, 34);
            this.dIC设置ToolStripMenuItem.Text = "DIC设置";
            // 
            // 子区大小ToolStripMenuItem
            // 
            this.子区大小ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dic_subsize});
            this.子区大小ToolStripMenuItem.Name = "子区大小ToolStripMenuItem";
            this.子区大小ToolStripMenuItem.Size = new System.Drawing.Size(200, 34);
            this.子区大小ToolStripMenuItem.Text = "子区大小";
            // 
            // dic_subsize
            // 
            this.dic_subsize.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dic_subsize.Name = "dic_subsize";
            this.dic_subsize.Size = new System.Drawing.Size(270, 30);
            this.dic_subsize.Text = "31";
            this.dic_subsize.TextChanged += new System.EventHandler(this.dic_subsize_TextChanged);
            // 
            // dic搜索区域
            // 
            this.dic搜索区域.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dic_searchsize});
            this.dic搜索区域.Name = "dic搜索区域";
            this.dic搜索区域.Size = new System.Drawing.Size(200, 34);
            this.dic搜索区域.Text = "搜索区域";
            // 
            // dic_searchsize
            // 
            this.dic_searchsize.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dic_searchsize.Name = "dic_searchsize";
            this.dic_searchsize.Size = new System.Drawing.Size(270, 30);
            this.dic_searchsize.Text = "100";
            this.dic_searchsize.TextChanged += new System.EventHandler(this.dic_searchsize_TextChanged);
            // 
            // dic相关性阈值
            // 
            this.dic相关性阈值.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dic_limitR});
            this.dic相关性阈值.Name = "dic相关性阈值";
            this.dic相关性阈值.Size = new System.Drawing.Size(200, 34);
            this.dic相关性阈值.Text = "相关性阈值";
            this.dic相关性阈值.TextChanged += new System.EventHandler(this.dic_limitR_TextChanged);
            // 
            // dic_limitR
            // 
            this.dic_limitR.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.dic_limitR.Name = "dic_limitR";
            this.dic_limitR.Size = new System.Drawing.Size(270, 30);
            this.dic_limitR.Text = "0.2";
            // 
            // Show3DBtn
            // 
            this.Show3DBtn.Name = "Show3DBtn";
            this.Show3DBtn.Size = new System.Drawing.Size(219, 34);
            this.Show3DBtn.Text = "3D(暂时无用)";
            this.Show3DBtn.Click += new System.EventHandler(this.Show3DBtn_Click);
            // 
            // ShowMatchResultBtn
            // 
            this.ShowMatchResultBtn.Name = "ShowMatchResultBtn";
            this.ShowMatchResultBtn.Size = new System.Drawing.Size(219, 34);
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
            this.isManuallySetRotation,
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
            this.x旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.x旋转弧度ToolStripMenuItem.Text = "x旋转弧度";
            // 
            // variableCircle_angle_x
            // 
            this.variableCircle_angle_x.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_x.Name = "variableCircle_angle_x";
            this.variableCircle_angle_x.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_x.Text = "0";
            // 
            // y旋转弧度ToolStripMenuItem
            // 
            this.y旋转弧度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_angle_y});
            this.y旋转弧度ToolStripMenuItem.Name = "y旋转弧度ToolStripMenuItem";
            this.y旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.y旋转弧度ToolStripMenuItem.Text = "y旋转弧度";
            // 
            // variableCircle_angle_y
            // 
            this.variableCircle_angle_y.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_y.Name = "variableCircle_angle_y";
            this.variableCircle_angle_y.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_y.Text = "0";
            // 
            // z旋转弧度ToolStripMenuItem
            // 
            this.z旋转弧度ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_angle_z});
            this.z旋转弧度ToolStripMenuItem.Name = "z旋转弧度ToolStripMenuItem";
            this.z旋转弧度ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.z旋转弧度ToolStripMenuItem.Text = "z旋转弧度";
            // 
            // variableCircle_angle_z
            // 
            this.variableCircle_angle_z.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_angle_z.Name = "variableCircle_angle_z";
            this.variableCircle_angle_z.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_angle_z.Text = "0";
            // 
            // isManuallySetRotation
            // 
            this.isManuallySetRotation.Name = "isManuallySetRotation";
            this.isManuallySetRotation.Size = new System.Drawing.Size(290, 34);
            this.isManuallySetRotation.Text = "是否为手动设置的旋转";
            this.isManuallySetRotation.Click += new System.EventHandler(this.isManuallySetRotation_Click);
            // 
            // 搜索半径ToolStripMenuItem
            // 
            this.搜索半径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_search_size});
            this.搜索半径ToolStripMenuItem.Name = "搜索半径ToolStripMenuItem";
            this.搜索半径ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.搜索半径ToolStripMenuItem.Text = "搜索半径";
            // 
            // variableCircle_search_size
            // 
            this.variableCircle_search_size.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_search_size.Name = "variableCircle_search_size";
            this.variableCircle_search_size.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_search_size.Text = "10";
            // 
            // 采样半径ToolStripMenuItem
            // 
            this.采样半径ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_sample_r});
            this.采样半径ToolStripMenuItem.Name = "采样半径ToolStripMenuItem";
            this.采样半径ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.采样半径ToolStripMenuItem.Text = "采样半径";
            // 
            // variableCircle_sample_r
            // 
            this.variableCircle_sample_r.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_sample_r.Name = "variableCircle_sample_r";
            this.variableCircle_sample_r.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_sample_r.Text = "10";
            // 
            // 与哪一张图比0开始ToolStripMenuItem
            // 
            this.与哪一张图比0开始ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_ref_index});
            this.与哪一张图比0开始ToolStripMenuItem.Name = "与哪一张图比0开始ToolStripMenuItem";
            this.与哪一张图比0开始ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.与哪一张图比0开始ToolStripMenuItem.Text = "与哪一张图比(0开始)";
            // 
            // variableCircle_ref_index
            // 
            this.variableCircle_ref_index.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_ref_index.Name = "variableCircle_ref_index";
            this.variableCircle_ref_index.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_ref_index.Text = "0";
            // 
            // 相关性限制ToolStripMenuItem
            // 
            this.相关性限制ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.variableCircle_limit_r});
            this.相关性限制ToolStripMenuItem.Name = "相关性限制ToolStripMenuItem";
            this.相关性限制ToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.相关性限制ToolStripMenuItem.Text = "相关性限制";
            // 
            // variableCircle_limit_r
            // 
            this.variableCircle_limit_r.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.variableCircle_limit_r.Name = "variableCircle_limit_r";
            this.variableCircle_limit_r.Size = new System.Drawing.Size(270, 30);
            this.variableCircle_limit_r.Text = "0.3";
            // 
            // VariableCircleMatchBegin
            // 
            this.VariableCircleMatchBegin.Name = "VariableCircleMatchBegin";
            this.VariableCircleMatchBegin.Size = new System.Drawing.Size(270, 34);
            this.VariableCircleMatchBegin.Text = "开始";
            this.VariableCircleMatchBegin.Click += new System.EventHandler(this.VariableCircleMatchBegin_Click);
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
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(0, 28);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开待配准图片ToolStripMenuItem});
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(100, 28);
            this.toolStripButton3.Text = "匹配测试";
            // 
            // 打开待配准图片ToolStripMenuItem
            // 
            this.打开待配准图片ToolStripMenuItem.Name = "打开待配准图片ToolStripMenuItem";
            this.打开待配准图片ToolStripMenuItem.Size = new System.Drawing.Size(236, 34);
            this.打开待配准图片ToolStripMenuItem.Text = "打开待配准图片";
            this.打开待配准图片ToolStripMenuItem.Click += new System.EventHandler(this.打开待配准图片ToolStripMenuItem_Click);
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
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem 服务端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 客户端ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox variableCircle_limit_r;
        private System.Windows.Forms.ToolStripMenuItem iCPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择两幅ply点云ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iCP配准开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 对应点距离约束ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox limitDistanceText;
        private System.Windows.Forms.ToolStripMenuItem iCP配准开始点对面ToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripButton2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem 打开待配准图片ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isManuallySetRotation;
        private System.Windows.Forms.ToolStripMenuItem 两幅ply点云融合并降采样ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 初始旋转ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox icp_init_rotation;
        private System.Windows.Forms.ToolStripMenuItem 初始位移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox icp_init_translation;
        private System.Windows.Forms.ToolStripMenuItem dIC设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 子区大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox dic_subsize;
        private System.Windows.Forms.ToolStripMenuItem dic搜索区域;
        private System.Windows.Forms.ToolStripTextBox dic_searchsize;
        private System.Windows.Forms.ToolStripMenuItem dic相关性阈值;
        private System.Windows.Forms.ToolStripTextBox dic_limitR;
    }
}

