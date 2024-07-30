
namespace Socket.Server
{
    partial class FrmServer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chBoxIsHeartCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtReceviceData = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCurrentClient = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.btnSendData = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnListeningStop = new System.Windows.Forms.Button();
            this.btnListeningStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvClientView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblClientCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientView)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chBoxIsHeartCheck
            // 
            this.chBoxIsHeartCheck.AutoSize = true;
            this.chBoxIsHeartCheck.Checked = true;
            this.chBoxIsHeartCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxIsHeartCheck.Location = new System.Drawing.Point(141, 159);
            this.chBoxIsHeartCheck.Name = "chBoxIsHeartCheck";
            this.chBoxIsHeartCheck.Size = new System.Drawing.Size(72, 16);
            this.chBoxIsHeartCheck.TabIndex = 0;
            this.chBoxIsHeartCheck.Text = "心跳检测";
            this.chBoxIsHeartCheck.UseVisualStyleBackColor = true;
            this.chBoxIsHeartCheck.CheckedChanged += new System.EventHandler(this.chBoxIsHeartCheck_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口号：";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(141, 46);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(202, 21);
            this.txtServerIp.TabIndex = 3;
            this.txtServerIp.Text = "192.168.1.5";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(141, 101);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(202, 21);
            this.txtServerPort.TabIndex = 4;
            this.txtServerPort.Text = "7001";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(410, 10);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panel2.Size = new System.Drawing.Size(628, 669);
            this.panel2.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtReceviceData);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(10, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已接收数据";
            // 
            // txtReceviceData
            // 
            this.txtReceviceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceviceData.Location = new System.Drawing.Point(3, 47);
            this.txtReceviceData.Name = "txtReceviceData";
            this.txtReceviceData.ReadOnly = true;
            this.txtReceviceData.Size = new System.Drawing.Size(612, 410);
            this.txtReceviceData.TabIndex = 0;
            this.txtReceviceData.Text = "";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblCurrentClient);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 17);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(612, 30);
            this.panel4.TabIndex = 1;
            // 
            // lblCurrentClient
            // 
            this.lblCurrentClient.AutoSize = true;
            this.lblCurrentClient.Location = new System.Drawing.Point(126, 9);
            this.lblCurrentClient.Name = "lblCurrentClient";
            this.lblCurrentClient.Size = new System.Drawing.Size(17, 12);
            this.lblCurrentClient.TabIndex = 1;
            this.lblCurrentClient.Text = "无";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "当前通讯客户端：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtMessage);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(10, 460);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(618, 139);
            this.groupBox5.TabIndex = 70;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "消息";
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Location = new System.Drawing.Point(3, 17);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(612, 119);
            this.txtMessage.TabIndex = 101;
            this.txtMessage.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSendData);
            this.groupBox2.Controls.Add(this.btnSendData);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(10, 599);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 70);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送数据";
            // 
            // txtSendData
            // 
            this.txtSendData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSendData.Location = new System.Drawing.Point(3, 17);
            this.txtSendData.Multiline = true;
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(537, 50);
            this.txtSendData.TabIndex = 0;
            // 
            // btnSendData
            // 
            this.btnSendData.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendData.Location = new System.Drawing.Point(540, 17);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(75, 50);
            this.btnSendData.TabIndex = 1;
            this.btnSendData.Text = "发送数据";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtInterval);
            this.groupBox3.Controls.Add(this.btnListeningStop);
            this.groupBox3.Controls.Add(this.btnListeningStart);
            this.groupBox3.Controls.Add(this.txtServerPort);
            this.groupBox3.Controls.Add(this.chBoxIsHeartCheck);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtServerIp);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 263);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "服务器参数设置";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(308, 157);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(35, 21);
            this.txtInterval.TabIndex = 6;
            this.txtInterval.Text = "500";
            // 
            // btnListeningStop
            // 
            this.btnListeningStop.Location = new System.Drawing.Point(219, 214);
            this.btnListeningStop.Name = "btnListeningStop";
            this.btnListeningStop.Size = new System.Drawing.Size(100, 36);
            this.btnListeningStop.TabIndex = 5;
            this.btnListeningStop.Text = "停止监听";
            this.btnListeningStop.UseVisualStyleBackColor = true;
            this.btnListeningStop.Click += new System.EventHandler(this.btnListeningStop_Click);
            // 
            // btnListeningStart
            // 
            this.btnListeningStart.Location = new System.Drawing.Point(89, 214);
            this.btnListeningStart.Name = "btnListeningStart";
            this.btnListeningStart.Size = new System.Drawing.Size(100, 36);
            this.btnListeningStart.TabIndex = 5;
            this.btnListeningStart.Text = "开启监听";
            this.btnListeningStart.UseVisualStyleBackColor = true;
            this.btnListeningStart.Click += new System.EventHandler(this.btnListeningStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "间隔(毫秒)：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 669);
            this.panel1.TabIndex = 8;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvClientView);
            this.groupBox4.Controls.Add(this.panel3);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 263);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(400, 406);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "客户端列表";
            // 
            // dgvClientView
            // 
            this.dgvClientView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClientView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClientView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvClientView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClientView.Location = new System.Drawing.Point(3, 47);
            this.dgvClientView.Name = "dgvClientView";
            this.dgvClientView.RowHeadersVisible = false;
            this.dgvClientView.RowTemplate.Height = 23;
            this.dgvClientView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientView.Size = new System.Drawing.Size(394, 356);
            this.dgvClientView.TabIndex = 0;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "客户端IP";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "端口号";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "状态";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblClientCount);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(394, 30);
            this.panel3.TabIndex = 1;
            // 
            // lblClientCount
            // 
            this.lblClientCount.AutoSize = true;
            this.lblClientCount.Location = new System.Drawing.Point(102, 9);
            this.lblClientCount.Name = "lblClientCount";
            this.lblClientCount.Size = new System.Drawing.Size(11, 12);
            this.lblClientCount.TabIndex = 1;
            this.lblClientCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "客户端总数：";
            // 
            // FrmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 689);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmServer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "Socket通讯 服务端";
            this.Load += new System.EventHandler(this.FrmServer_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chBoxIsHeartCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnListeningStop;
        private System.Windows.Forms.Button btnListeningStart;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox txtReceviceData;
        private System.Windows.Forms.DataGridView dgvClientView;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblClientCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblCurrentClient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RichTextBox txtMessage;
    }
}

