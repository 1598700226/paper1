namespace sift.SocketApp
{
    partial class ClientService
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
            this.IpText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.OpenConnectBtn = new System.Windows.Forms.Button();
            this.CloseConnectBtn = new System.Windows.Forms.Button();
            this.MessageTxtBox = new System.Windows.Forms.RichTextBox();
            this.SendMsgBtnText = new System.Windows.Forms.RichTextBox();
            this.SendMsgBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PortText = new System.Windows.Forms.TextBox();
            this.recvFileBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IpText
            // 
            this.IpText.Location = new System.Drawing.Point(59, 24);
            this.IpText.Name = "IpText";
            this.IpText.Size = new System.Drawing.Size(105, 28);
            this.IpText.TabIndex = 0;
            this.IpText.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ip：";
            // 
            // OpenConnectBtn
            // 
            this.OpenConnectBtn.Location = new System.Drawing.Point(588, 15);
            this.OpenConnectBtn.Name = "OpenConnectBtn";
            this.OpenConnectBtn.Size = new System.Drawing.Size(97, 42);
            this.OpenConnectBtn.TabIndex = 2;
            this.OpenConnectBtn.Text = "建立连接";
            this.OpenConnectBtn.UseVisualStyleBackColor = true;
            this.OpenConnectBtn.Click += new System.EventHandler(this.OpenConnectBtn_Click);
            // 
            // CloseConnectBtn
            // 
            this.CloseConnectBtn.Location = new System.Drawing.Point(691, 15);
            this.CloseConnectBtn.Name = "CloseConnectBtn";
            this.CloseConnectBtn.Size = new System.Drawing.Size(97, 42);
            this.CloseConnectBtn.TabIndex = 2;
            this.CloseConnectBtn.Text = "关闭连接";
            this.CloseConnectBtn.UseVisualStyleBackColor = true;
            this.CloseConnectBtn.Click += new System.EventHandler(this.CloseConnectBtn_Click);
            // 
            // MessageTxtBox
            // 
            this.MessageTxtBox.Location = new System.Drawing.Point(24, 63);
            this.MessageTxtBox.Name = "MessageTxtBox";
            this.MessageTxtBox.Size = new System.Drawing.Size(764, 362);
            this.MessageTxtBox.TabIndex = 3;
            this.MessageTxtBox.Text = "";
            // 
            // SendMsgBtnText
            // 
            this.SendMsgBtnText.Location = new System.Drawing.Point(24, 447);
            this.SendMsgBtnText.Name = "SendMsgBtnText";
            this.SendMsgBtnText.Size = new System.Drawing.Size(630, 43);
            this.SendMsgBtnText.TabIndex = 4;
            this.SendMsgBtnText.Text = "";
            // 
            // SendMsgBtn
            // 
            this.SendMsgBtn.Location = new System.Drawing.Point(673, 448);
            this.SendMsgBtn.Name = "SendMsgBtn";
            this.SendMsgBtn.Size = new System.Drawing.Size(115, 42);
            this.SendMsgBtn.TabIndex = 2;
            this.SendMsgBtn.Text = "发送消息";
            this.SendMsgBtn.UseVisualStyleBackColor = true;
            this.SendMsgBtn.Click += new System.EventHandler(this.SendMsgBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port：";
            // 
            // PortText
            // 
            this.PortText.Location = new System.Drawing.Point(238, 24);
            this.PortText.Name = "PortText";
            this.PortText.Size = new System.Drawing.Size(74, 28);
            this.PortText.TabIndex = 0;
            this.PortText.Text = "8080";
            // 
            // recvFileBtn
            // 
            this.recvFileBtn.Location = new System.Drawing.Point(480, 15);
            this.recvFileBtn.Name = "recvFileBtn";
            this.recvFileBtn.Size = new System.Drawing.Size(102, 42);
            this.recvFileBtn.TabIndex = 5;
            this.recvFileBtn.Text = "接收文件";
            this.recvFileBtn.UseVisualStyleBackColor = true;
            this.recvFileBtn.Click += new System.EventHandler(this.recvFile_Click);
            // 
            // ClientService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 502);
            this.Controls.Add(this.recvFileBtn);
            this.Controls.Add(this.SendMsgBtnText);
            this.Controls.Add(this.MessageTxtBox);
            this.Controls.Add(this.SendMsgBtn);
            this.Controls.Add(this.CloseConnectBtn);
            this.Controls.Add(this.OpenConnectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortText);
            this.Controls.Add(this.IpText);
            this.Name = "ClientService";
            this.Text = "ClientService";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ClientService_FormClosed);
            this.Load += new System.EventHandler(this.ClientService_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IpText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OpenConnectBtn;
        private System.Windows.Forms.Button CloseConnectBtn;
        private System.Windows.Forms.RichTextBox MessageTxtBox;
        private System.Windows.Forms.RichTextBox SendMsgBtnText;
        private System.Windows.Forms.Button SendMsgBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PortText;
        private System.Windows.Forms.Button recvFileBtn;
    }
}