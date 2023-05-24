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
            this.DirectFilteringBtn = new System.Windows.Forms.ToolStripMenuItem();
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
            this.calFpfh = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标转换 = new System.Windows.Forms.ToolStripDropDownButton();
            this.selectConvertPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.convertAndOutputPly = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnRgbD.Size = new System.Drawing.Size(270, 34);
            this.btnRgbD.Text = "融合色彩和深度";
            this.btnRgbD.Click += new System.EventHandler(this.btnRgbD_Click);
            // 
            // downSamplingBtn
            // 
            this.downSamplingBtn.Name = "downSamplingBtn";
            this.downSamplingBtn.Size = new System.Drawing.Size(270, 34);
            this.downSamplingBtn.Text = "均匀降采样";
            this.downSamplingBtn.Click += new System.EventHandler(this.downSamplingBtn_Click);
            // 
            // 剔除离群点ToolStripMenuItem
            // 
            this.剔除离群点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOutliers,
            this.DirectFilteringBtn,
            this.removeGround,
            this.dBSCAN聚类ToolStripMenuItem});
            this.剔除离群点ToolStripMenuItem.Name = "剔除离群点ToolStripMenuItem";
            this.剔除离群点ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.剔除离群点ToolStripMenuItem.Text = "剔除离群点";
            // 
            // btnOutliers
            // 
            this.btnOutliers.Name = "btnOutliers";
            this.btnOutliers.Size = new System.Drawing.Size(270, 34);
            this.btnOutliers.Text = "基于统计方法";
            this.btnOutliers.Click += new System.EventHandler(this.btnOutliers_Click);
            // 
            // DirectFilteringBtn
            // 
            this.DirectFilteringBtn.Name = "DirectFilteringBtn";
            this.DirectFilteringBtn.Size = new System.Drawing.Size(270, 34);
            this.DirectFilteringBtn.Text = "基于直通滤波方法";
            this.DirectFilteringBtn.Click += new System.EventHandler(this.DirectFilteringBtn_Click);
            // 
            // removeGround
            // 
            this.removeGround.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.迭代次数ToolStripMenuItem,
            this.removeGroundBtn});
            this.removeGround.Name = "removeGround";
            this.removeGround.Size = new System.Drawing.Size(270, 34);
            this.removeGround.Text = "剔除地面";
            // 
            // 迭代次数ToolStripMenuItem
            // 
            this.迭代次数ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GroundFilteringIteration,
            this.GroundFilteringError});
            this.迭代次数ToolStripMenuItem.Name = "迭代次数ToolStripMenuItem";
            this.迭代次数ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
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
            this.GroundFilteringError.Text = "40";
            // 
            // removeGroundBtn
            // 
            this.removeGroundBtn.Name = "removeGroundBtn";
            this.removeGroundBtn.Size = new System.Drawing.Size(270, 34);
            this.removeGroundBtn.Text = "剔除";
            this.removeGroundBtn.Click += new System.EventHandler(this.removeGroundBtn_Click);
            // 
            // dBSCAN聚类ToolStripMenuItem
            // 
            this.dBSCAN聚类ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.DBSCAN_begin_btn});
            this.dBSCAN聚类ToolStripMenuItem.Name = "dBSCAN聚类ToolStripMenuItem";
            this.dBSCAN聚类ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.dBSCAN聚类ToolStripMenuItem.Text = "DBSCAN聚类";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DBSCAN_epsValue,
            this.DBSCAN_minNum});
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
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
            this.DBSCAN_begin_btn.Size = new System.Drawing.Size(270, 34);
            this.DBSCAN_begin_btn.Text = "开始";
            this.DBSCAN_begin_btn.Click += new System.EventHandler(this.DBSCAN_begin_btn_Click);
            // 
            // calFpfh
            // 
            this.calFpfh.Name = "calFpfh";
            this.calFpfh.Size = new System.Drawing.Size(270, 34);
            this.calFpfh.Text = "计算fpfh值";
            this.calFpfh.Click += new System.EventHandler(this.calFpfh_Click);
            // 
            // 坐标转换
            // 
            this.坐标转换.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectConvertPoints,
            this.convertAndOutputPly});
            this.坐标转换.Name = "坐标转换";
            this.坐标转换.Size = new System.Drawing.Size(100, 28);
            this.坐标转换.Text = "坐标转换";
            // 
            // selectConvertPoints
            // 
            this.selectConvertPoints.Name = "selectConvertPoints";
            this.selectConvertPoints.Size = new System.Drawing.Size(270, 34);
            this.selectConvertPoints.Text = "选择待配准点";
            this.selectConvertPoints.Click += new System.EventHandler(this.selectConvertPoints_Click);
            // 
            // convertAndOutputPly
            // 
            this.convertAndOutputPly.Name = "convertAndOutputPly";
            this.convertAndOutputPly.Size = new System.Drawing.Size(270, 34);
            this.convertAndOutputPly.Text = "进行转换并输出ply";
            this.convertAndOutputPly.Click += new System.EventHandler(this.convertAndOutputPly_Click);
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
        private System.Windows.Forms.ToolStripMenuItem DirectFilteringBtn;
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
    }
}