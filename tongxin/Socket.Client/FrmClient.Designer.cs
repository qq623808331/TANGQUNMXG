namespace Client
{
    partial class FrmClient
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.chBoxIsHeartCheck = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReceviceData = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "服务器端口：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "服务器IP：";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(121, 94);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(202, 21);
            this.txtServerPort.TabIndex = 1;
            this.txtServerPort.Text = "7001";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(121, 48);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(202, 21);
            this.txtServerIp.TabIndex = 0;
            this.txtServerIp.Text = "192.168.1.5";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(127, 187);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(106, 33);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtInterval);
            this.groupBox3.Controls.Add(this.chBoxIsHeartCheck);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtServerPort);
            this.groupBox3.Controls.Add(this.txtServerIp);
            this.groupBox3.Controls.Add(this.btnConnect);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(360, 258);
            this.groupBox3.TabIndex = 68;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "客户端参数设置";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(288, 147);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(35, 21);
            this.txtInterval.TabIndex = 41;
            this.txtInterval.Text = "40";
            // 
            // chBoxIsHeartCheck
            // 
            this.chBoxIsHeartCheck.AutoSize = true;
            this.chBoxIsHeartCheck.Checked = true;
            this.chBoxIsHeartCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxIsHeartCheck.Location = new System.Drawing.Point(121, 149);
            this.chBoxIsHeartCheck.Name = "chBoxIsHeartCheck";
            this.chBoxIsHeartCheck.Size = new System.Drawing.Size(72, 16);
            this.chBoxIsHeartCheck.TabIndex = 39;
            this.chBoxIsHeartCheck.Text = "心跳检测";
            this.chBoxIsHeartCheck.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(219, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 40;
            this.label3.Text = "间隔(秒)：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(370, 10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(616, 690);
            this.panel1.TabIndex = 73;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReceviceData);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 620);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已接收数据";
            // 
            // txtReceviceData
            // 
            this.txtReceviceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceviceData.Location = new System.Drawing.Point(3, 17);
            this.txtReceviceData.Name = "txtReceviceData";
            this.txtReceviceData.ReadOnly = true;
            this.txtReceviceData.Size = new System.Drawing.Size(600, 600);
            this.txtReceviceData.TabIndex = 100;
            this.txtReceviceData.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSendData);
            this.groupBox2.Controls.Add(this.btnSendData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(10, 620);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(606, 70);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送数据";
            // 
            // txtSendData
            // 
            this.txtSendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSendData.Location = new System.Drawing.Point(3, 17);
            this.txtSendData.Multiline = true;
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(525, 50);
            this.txtSendData.TabIndex = 0;
            // 
            // btnSendData
            // 
            this.btnSendData.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendData.Location = new System.Drawing.Point(528, 17);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(75, 50);
            this.btnSendData.TabIndex = 1;
            this.btnSendData.Text = "发送数据";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(10, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 690);
            this.panel2.TabIndex = 74;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtMessage);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 258);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(360, 432);
            this.groupBox4.TabIndex = 69;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "消息";
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(3, 17);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(354, 412);
            this.txtMessage.TabIndex = 101;
            this.txtMessage.Text = "";
            // 
            // FrmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmClient";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "主窗体";
            this.Load += new System.EventHandler(this.FrmClient_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtReceviceData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox txtMessage;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.CheckBox chBoxIsHeartCheck;
        private System.Windows.Forms.Label label3;
    }
}

