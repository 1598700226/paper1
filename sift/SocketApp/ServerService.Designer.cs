namespace sift.SocketApp
{
    partial class ServerService
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.MessageTxtBox = new System.Windows.Forms.RichTextBox();
            this.listenPortText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(665, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "关闭服务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(527, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(123, 44);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开启服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // MessageTxtBox
            // 
            this.MessageTxtBox.Location = new System.Drawing.Point(24, 76);
            this.MessageTxtBox.Name = "MessageTxtBox";
            this.MessageTxtBox.Size = new System.Drawing.Size(764, 431);
            this.MessageTxtBox.TabIndex = 1;
            this.MessageTxtBox.Text = "";
            // 
            // listenPortText
            // 
            this.listenPortText.Location = new System.Drawing.Point(116, 28);
            this.listenPortText.Name = "listenPortText";
            this.listenPortText.Size = new System.Drawing.Size(100, 28);
            this.listenPortText.TabIndex = 2;
            this.listenPortText.Text = "8080";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "监听端口：";
            // 
            // ServerService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 519);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listenPortText);
            this.Controls.Add(this.MessageTxtBox);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.button1);
            this.Name = "ServerService";
            this.Text = "ServerService";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServerService_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.RichTextBox MessageTxtBox;
        private System.Windows.Forms.TextBox listenPortText;
        private System.Windows.Forms.Label label1;
    }
}