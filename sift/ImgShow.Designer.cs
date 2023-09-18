namespace sift
{
    partial class ImgShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImgShow));
            this.picBox = new System.Windows.Forms.PictureBox();
            this.infoLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.operateBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.selectWaitMatchPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.savePicBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.rollBackBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.rollBackOriBpmBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPlyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.输出off文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnRgbD = new System.Windows.Forms.ToolStripMenuItem();
            this.downSamplingBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.剔除离群点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOutliers = new System.Windows.Forms.ToolStripMenuItem();
            this.removeGround = new System.Windows.Forms.ToolStripMenuItem();
            this.迭代次数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GroundFilteringIteration = new System.Windows.Forms.ToolStripTextBox();
            this.GroundFilteringError = new System.Windows.Forms.ToolStripTextBox();
            this.removeGroundBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.dBSCAN聚类ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DBSCAN_epsValue = new System.Windows.Forms.ToolStripTextBox();
            this.DBSCAN_minNum = new System.Windows.Forms.ToolStripTextBox();
            this.DBSCAN_begin_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.直通滤波ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.最大值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passFilterMax = new System.Windows.Forms.ToolStripTextBox();
            this.passFilterMin = new System.Windows.Forms.ToolStripTextBox();
            this.最小值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectFilteringXBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectFilteringYBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectFilteringZBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.calFpfh = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标转换 = new System.Windows.Forms.ToolStripDropDownButton();
            this.selectConvertPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.获取位姿变换矩阵ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.第一个点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationPoint_1 = new System.Windows.Forms.ToolStripTextBox();
            this.第二个点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationPoint_2 = new System.Windows.Forms.ToolStripTextBox();
            this.第三个点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationPoint_3 = new System.Windows.Forms.ToolStripTextBox();
            this.convertAndOutputPly = new System.Windows.Forms.ToolStripMenuItem();
            this.sift特征点选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(12, 74);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(712, 547);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseMove);
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(12, 43);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(53, 18);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "lable";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.operateBtn,
            this.toolStripDropDownButton1,
            this.坐标转换});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(736, 33);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // operateBtn
            // 
            this.operateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.operateBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectWaitMatchPoint,
            this.savePicBtn,
            this.rollBackBtn,
            this.rollBackOriBpmBtn,
            this.btnPlyFile,
            this.输出off文件ToolStripMenuItem});
            this.operateBtn.Image = ((System.Drawing.Image)(resources.GetObject("operateBtn.Image")));
            this.operateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.operateBtn.Name = "operateBtn";
            this.operateBtn.Size = new System.Drawing.Size(64, 28);
            this.operateBtn.Text = "操作";
            this.operateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // selectWaitMatchPoint
            // 
            this.selectWaitMatchPoint.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sift特征点选择ToolStripMenuItem});
            this.selectWaitMatchPoint.Name = "selectWaitMatchPoint";
            this.selectWaitMatchPoint.Size = new System.Drawing.Size(270, 34);
            this.selectWaitMatchPoint.Text = "选择待匹配点";
            this.selectWaitMatchPoint.Click += new System.EventHandler(this.selectWaitMatchPoint_Click);
            // 
            // savePicBtn
            // 
            this.savePicBtn.Name = "savePicBtn";
            this.savePicBtn.Size = new System.Drawing.Size(270, 34);
            this.savePicBtn.Text = "保存图片";
            this.savePicBtn.Click += new System.EventHandler(this.savePicBtn_Click);
            // 
            // rollBackBtn
            // 
            this.rollBackBtn.Name = "rollBackBtn";
            this.rollBackBtn.Size = new System.Drawing.Size(270, 34);
            this.rollBackBtn.Text = "还原";
            this.rollBackBtn.Click += new System.EventHandler(this.rollBackBtn_Click_1);
            // 
            // rollBackOriBpmBtn
            // 
            this.rollBackOriBpmBtn.Name = "rollBackOriBpmBtn";
            this.rollBackOriBpmBtn.Size = new System.Drawing.Size(270, 34);
            this.rollBackOriBpmBtn.Text = "还原原始图片";
            this.rollBackOriBpmBtn.Click += new System.EventHandler(this.rollBackBtn_Click);
            // 
            // btnPlyFile
            // 
            this.btnPlyFile.Name = "btnPlyFile";
            this.btnPlyFile.Size = new System.Drawing.Size(270, 34);
            this.btnPlyFile.Text = "输出ply文件";
            this.btnPlyFile.Click += new System.EventHandler(this.btnPlyFile_Click);
            // 
            // 输出off文件ToolStripMenuItem
            // 
            this.输出off文件ToolStripMenuItem.Name = "输出off文件ToolStripMenuItem";
            this.输出off文件ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.输出off文件ToolStripMenuItem.Text = "输出off文件";
            this.输出off文件ToolStripMenuItem.Click += new System.EventHandler(this.输出off文件ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRgbD,
            this.downSamplingBtn,
            this.剔除离群点ToolStripMenuItem,
            this.calFpfh});
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(64, 28);
            this.toolStripDropDownButton1.Text = "测试";
            // 
            // btnRgbD
            // 
            this.btnRgbD.Name = "btnRgbD";
            this.btnRgbD.Size = new System.Drawing.Size(236, 34);
            this.btnRgbD.Text = "融合色彩和深度";
            this.btnRgbD.Click += new System.EventHandler(this.btnRgbD_Click);
            // 
            // downSamplingBtn
            // 
            this.downSamplingBtn.Name = "downSamplingBtn";
            this.downSamplingBtn.Size = new System.Drawing.Size(236, 34);
            this.downSamplingBtn.Text = "均匀降采样";
            this.downSamplingBtn.Click += new System.EventHandler(this.downSamplingBtn_Click);
            // 
            // 剔除离群点ToolStripMenuItem
            // 
            this.剔除离群点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOutliers,
            this.removeGround,
            this.dBSCAN聚类ToolStripMenuItem,
            this.直通滤波ToolStripMenuItem,
            this.DirectFilteringXBtn,
            this.DirectFilteringYBtn,
            this.DirectFilteringZBtn});
            this.剔除离群点ToolStripMenuItem.Name = "剔除离群点ToolStripMenuItem";
            this.剔除离群点ToolStripMenuItem.Size = new System.Drawing.Size(236, 34);
            this.剔除离群点ToolStripMenuItem.Text = "剔除离群点";
            // 
            // btnOutliers
            // 
            this.btnOutliers.Name = "btnOutliers";
            this.btnOutliers.Size = new System.Drawing.Size(264, 34);
            this.btnOutliers.Text = "基于统计方法";
            this.btnOutliers.Click += new System.EventHandler(this.btnOutliers_Click);
            // 
            // removeGround
            // 
            this.removeGround.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.迭代次数ToolStripMenuItem,
            this.removeGroundBtn});
            this.removeGround.Name = "removeGround";
            this.removeGround.Size = new System.Drawing.Size(264, 34);
            this.removeGround.Text = "剔除地面";
            // 
            // 迭代次数ToolStripMenuItem
            // 
            this.迭代次数ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroundFilteringIteration,
            this.GroundFilteringError});
            this.迭代次数ToolStripMenuItem.Name = "迭代次数ToolStripMenuItem";
            this.迭代次数ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.迭代次数ToolStripMenuItem.Text = "参数设置";
            // 
            // GroundFilteringIteration
            // 
            this.GroundFilteringIteration.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.GroundFilteringIteration.Name = "GroundFilteringIteration";
            this.GroundFilteringIteration.Size = new System.Drawing.Size(270, 30);
            this.GroundFilteringIteration.Text = "1000";
            // 
            // GroundFilteringError
            // 
            this.GroundFilteringError.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.GroundFilteringError.Name = "GroundFilteringError";
            this.GroundFilteringError.Size = new System.Drawing.Size(360, 30);
            this.GroundFilteringError.Text = "5";
            // 
            // removeGroundBtn
            // 
            this.removeGroundBtn.Name = "removeGroundBtn";
            this.removeGroundBtn.Size = new System.Drawing.Size(182, 34);
            this.removeGroundBtn.Text = "剔除";
            this.removeGroundBtn.Click += new System.EventHandler(this.removeGroundBtn_Click);
            // 
            // dBSCAN聚类ToolStripMenuItem
            // 
            this.dBSCAN聚类ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.DBSCAN_begin_btn});
            this.dBSCAN聚类ToolStripMenuItem.Name = "dBSCAN聚类ToolStripMenuItem";
            this.dBSCAN聚类ToolStripMenuItem.Size = new System.Drawing.Size(264, 34);
            this.dBSCAN聚类ToolStripMenuItem.Text = "DBSCAN聚类";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBSCAN_epsValue,
            this.DBSCAN_minNum});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(182, 34);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // DBSCAN_epsValue
            // 
            this.DBSCAN_epsValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.DBSCAN_epsValue.Name = "DBSCAN_epsValue";
            this.DBSCAN_epsValue.Size = new System.Drawing.Size(270, 30);
            this.DBSCAN_epsValue.Text = "10";
            // 
            // DBSCAN_minNum
            // 
            this.DBSCAN_minNum.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.DBSCAN_minNum.Name = "DBSCAN_minNum";
            this.DBSCAN_minNum.Size = new System.Drawing.Size(360, 30);
            this.DBSCAN_minNum.Text = "10";
            // 
            // DBSCAN_begin_btn
            // 
            this.DBSCAN_begin_btn.Name = "DBSCAN_begin_btn";
            this.DBSCAN_begin_btn.Size = new System.Drawing.Size(182, 34);
            this.DBSCAN_begin_btn.Text = "开始";
            this.DBSCAN_begin_btn.Click += new System.EventHandler(this.DBSCAN_begin_btn_Click);
            // 
            // 直通滤波ToolStripMenuItem
            // 
            this.直通滤波ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.最大值ToolStripMenuItem,
            this.最小值ToolStripMenuItem});
            this.直通滤波ToolStripMenuItem.Name = "直通滤波ToolStripMenuItem";
            this.直通滤波ToolStripMenuItem.Size = new System.Drawing.Size(264, 34);
            this.直通滤波ToolStripMenuItem.Text = "直通滤波参数设置";
            // 
            // 最大值ToolStripMenuItem
            // 
            this.最大值ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.passFilterMax,
            this.passFilterMin});
            this.最大值ToolStripMenuItem.Name = "最大值ToolStripMenuItem";
            this.最大值ToolStripMenuItem.Size = new System.Drawing.Size(164, 34);
            this.最大值ToolStripMenuItem.Text = "最大值";
            // 
            // passFilterMax
            // 
            this.passFilterMax.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.passFilterMax.Name = "passFilterMax";
            this.passFilterMax.Size = new System.Drawing.Size(270, 30);
            this.passFilterMax.Text = "1500";
            // 
            // passFilterMin
            // 
            this.passFilterMin.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.passFilterMin.Name = "passFilterMin";
            this.passFilterMin.Size = new System.Drawing.Size(360, 30);
            this.passFilterMin.Text = "0";
            // 
            // 最小值ToolStripMenuItem
            // 
            this.最小值ToolStripMenuItem.Name = "最小值ToolStripMenuItem";
            this.最小值ToolStripMenuItem.Size = new System.Drawing.Size(164, 34);
            this.最小值ToolStripMenuItem.Text = "最小值";
            // 
            // DirectFilteringXBtn
            // 
            this.DirectFilteringXBtn.Name = "DirectFilteringXBtn";
            this.DirectFilteringXBtn.Size = new System.Drawing.Size(264, 34);
            this.DirectFilteringXBtn.Text = "基于x方向直通滤波";
            this.DirectFilteringXBtn.Click += new System.EventHandler(this.DirectFilteringXBtn_Click);
            // 
            // DirectFilteringYBtn
            // 
            this.DirectFilteringYBtn.Name = "DirectFilteringYBtn";
            this.DirectFilteringYBtn.Size = new System.Drawing.Size(264, 34);
            this.DirectFilteringYBtn.Text = "基于y方向直通滤波";
            this.DirectFilteringYBtn.Click += new System.EventHandler(this.DirectFilteringYBtn_Click);
            // 
            // DirectFilteringZBtn
            // 
            this.DirectFilteringZBtn.Name = "DirectFilteringZBtn";
            this.DirectFilteringZBtn.Size = new System.Drawing.Size(264, 34);
            this.DirectFilteringZBtn.Text = "基于z方向直通滤波";
            this.DirectFilteringZBtn.Click += new System.EventHandler(this.DirectFilteringBtn_Click);
            // 
            // calFpfh
            // 
            this.calFpfh.Name = "calFpfh";
            this.calFpfh.Size = new System.Drawing.Size(236, 34);
            this.calFpfh.Text = "计算fpfh值";
            this.calFpfh.Click += new System.EventHandler(this.calFpfh_Click);
            // 
            // 坐标转换
            // 
            this.坐标转换.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectConvertPoints,
            this.获取位姿变换矩阵ToolStripMenuItem,
            this.convertAndOutputPly});
            this.坐标转换.Name = "坐标转换";
            this.坐标转换.Size = new System.Drawing.Size(100, 28);
            this.坐标转换.Text = "坐标转换";
            // 
            // selectConvertPoints
            // 
            this.selectConvertPoints.Name = "selectConvertPoints";
            this.selectConvertPoints.Size = new System.Drawing.Size(283, 34);
            this.selectConvertPoints.Text = "选择3个待转换平面点";
            this.selectConvertPoints.Click += new System.EventHandler(this.selectConvertPoints_Click);
            // 
            // 获取位姿变换矩阵ToolStripMenuItem
            // 
            this.获取位姿变换矩阵ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem1});
            this.获取位姿变换矩阵ToolStripMenuItem.Name = "获取位姿变换矩阵ToolStripMenuItem";
            this.获取位姿变换矩阵ToolStripMenuItem.Size = new System.Drawing.Size(283, 34);
            this.获取位姿变换矩阵ToolStripMenuItem.Text = "获取位姿变换矩阵";
            this.获取位姿变换矩阵ToolStripMenuItem.Click += new System.EventHandler(this.获取位姿变换矩阵ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem1
            // 
            this.参数设置ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.第一个点ToolStripMenuItem,
            this.第二个点ToolStripMenuItem,
            this.第三个点ToolStripMenuItem});
            this.参数设置ToolStripMenuItem1.Name = "参数设置ToolStripMenuItem1";
            this.参数设置ToolStripMenuItem1.Size = new System.Drawing.Size(182, 34);
            this.参数设置ToolStripMenuItem1.Text = "参数设置";
            // 
            // 第一个点ToolStripMenuItem
            // 
            this.第一个点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrationPoint_1});
            this.第一个点ToolStripMenuItem.Name = "第一个点ToolStripMenuItem";
            this.第一个点ToolStripMenuItem.Size = new System.Drawing.Size(271, 34);
            this.第一个点ToolStripMenuItem.Text = "第一个点（单位m）";
            // 
            // calibrationPoint_1
            // 
            this.calibrationPoint_1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.calibrationPoint_1.Name = "calibrationPoint_1";
            this.calibrationPoint_1.Size = new System.Drawing.Size(270, 30);
            this.calibrationPoint_1.Text = "0 0 0";
            // 
            // 第二个点ToolStripMenuItem
            // 
            this.第二个点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrationPoint_2});
            this.第二个点ToolStripMenuItem.Name = "第二个点ToolStripMenuItem";
            this.第二个点ToolStripMenuItem.Size = new System.Drawing.Size(271, 34);
            this.第二个点ToolStripMenuItem.Text = "第二个点";
            // 
            // calibrationPoint_2
            // 
            this.calibrationPoint_2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.calibrationPoint_2.Name = "calibrationPoint_2";
            this.calibrationPoint_2.Size = new System.Drawing.Size(270, 30);
            this.calibrationPoint_2.Text = "0 0 0.15";
            // 
            // 第三个点ToolStripMenuItem
            // 
            this.第三个点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibrationPoint_3});
            this.第三个点ToolStripMenuItem.Name = "第三个点ToolStripMenuItem";
            this.第三个点ToolStripMenuItem.Size = new System.Drawing.Size(271, 34);
            this.第三个点ToolStripMenuItem.Text = "第三个点";
            // 
            // calibrationPoint_3
            // 
            this.calibrationPoint_3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.calibrationPoint_3.Name = "calibrationPoint_3";
            this.calibrationPoint_3.Size = new System.Drawing.Size(270, 30);
            this.calibrationPoint_3.Text = "0.15 0 0.15";
            // 
            // convertAndOutputPly
            // 
            this.convertAndOutputPly.Name = "convertAndOutputPly";
            this.convertAndOutputPly.Size = new System.Drawing.Size(283, 34);
            this.convertAndOutputPly.Text = "进行转换并输出ply";
            this.convertAndOutputPly.Click += new System.EventHandler(this.convertAndOutputPly_Click);
            // 
            // sift特征点选择ToolStripMenuItem
            // 
            this.sift特征点选择ToolStripMenuItem.Name = "sift特征点选择ToolStripMenuItem";
            this.sift特征点选择ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sift特征点选择ToolStripMenuItem.Text = "sift特征点选择";
            this.sift特征点选择ToolStripMenuItem.Click += new System.EventHandler(this.sift特征点选择ToolStripMenuItem_Click);
            // 
            // ImgShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 633);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.picBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImgShow";
            this.Text = "ImgShow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImgShow_FormClosing);
            this.Load += new System.EventHandler(this.ImgShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton operateBtn;
        private System.Windows.Forms.ToolStripMenuItem savePicBtn;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem downSamplingBtn;
        private System.Windows.Forms.ToolStripMenuItem rollBackOriBpmBtn;
        private System.Windows.Forms.ToolStripMenuItem calFpfh;
        private System.Windows.Forms.ToolStripMenuItem 剔除离群点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnOutliers;
        private System.Windows.Forms.ToolStripMenuItem btnRgbD;
        private System.Windows.Forms.ToolStripMenuItem btnPlyFile;
        private System.Windows.Forms.ToolStripMenuItem DirectFilteringZBtn;
        private System.Windows.Forms.ToolStripMenuItem removeGround;
        private System.Windows.Forms.ToolStripMenuItem 迭代次数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox GroundFilteringIteration;
        private System.Windows.Forms.ToolStripTextBox GroundFilteringError;
        private System.Windows.Forms.ToolStripMenuItem removeGroundBtn;
        private System.Windows.Forms.ToolStripMenuItem rollBackBtn;
        private System.Windows.Forms.ToolStripMenuItem selectWaitMatchPoint;
        private System.Windows.Forms.ToolStripMenuItem dBSCAN聚类ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DBSCAN_begin_btn;
        private System.Windows.Forms.ToolStripTextBox DBSCAN_epsValue;
        private System.Windows.Forms.ToolStripTextBox DBSCAN_minNum;
        private System.Windows.Forms.ToolStripMenuItem 输出off文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton 坐标转换;
        private System.Windows.Forms.ToolStripMenuItem selectConvertPoints;
        private System.Windows.Forms.ToolStripMenuItem convertAndOutputPly;
        private System.Windows.Forms.ToolStripMenuItem 直通滤波ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 最大值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox passFilterMax;
        private System.Windows.Forms.ToolStripTextBox passFilterMin;
        private System.Windows.Forms.ToolStripMenuItem 最小值ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DirectFilteringXBtn;
        private System.Windows.Forms.ToolStripMenuItem DirectFilteringYBtn;
        private System.Windows.Forms.ToolStripMenuItem 获取位姿变换矩阵ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 第一个点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox calibrationPoint_1;
        private System.Windows.Forms.ToolStripMenuItem 第二个点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox calibrationPoint_2;
        private System.Windows.Forms.ToolStripMenuItem 第三个点ToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox calibrationPoint_3;
        private System.Windows.Forms.ToolStripMenuItem sift特征点选择ToolStripMenuItem;
    }
}