namespace SLC1_N
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CH1IsRun = new System.Windows.Forms.Timer(this.components);
            this.ProBarRun = new System.Windows.Forms.Timer(this.components);
            this.CH2IsRun = new System.Windows.Forms.Timer(this.components);
            this.CH3IsRun = new System.Windows.Forms.Timer(this.components);
            this.CH4IsRun = new System.Windows.Forms.Timer(this.components);
            this.CodePort1 = new System.IO.Ports.SerialPort(this.components);
            this.PLCSignal = new System.Windows.Forms.Timer(this.components);
            this.ADCPort1 = new System.IO.Ports.SerialPort(this.components);
            this.VDCPort1 = new System.IO.Ports.SerialPort(this.components);
            this.CH2ADCPort = new System.IO.Ports.SerialPort(this.components);
            this.CH2VDCPort = new System.IO.Ports.SerialPort(this.components);
            this.CH1ReadElec = new System.Windows.Forms.Timer(this.components);
            this.CH2ReadElec = new System.Windows.Forms.Timer(this.components);
            this.CH1ReaduA = new System.Windows.Forms.Timer(this.components);
            this.CH2ReaduA = new System.Windows.Forms.Timer(this.components);
            this.DayTime = new System.Windows.Forms.Timer(this.components);
            this.UDPOverTime = new System.Windows.Forms.Timer(this.components);
            this.UDPRead = new System.Windows.Forms.Timer(this.components);
            this.CH1LinUP = new System.Windows.Forms.Timer(this.components);
            this.CH2LinUP = new System.Windows.Forms.Timer(this.components);
            this.CH1FlowPort = new System.IO.Ports.SerialPort(this.components);
            this.CH3FlowPort = new System.IO.Ports.SerialPort(this.components);
            this.CH2FlowPort = new System.IO.Ports.SerialPort(this.components);
            this.CH4FlowPort = new System.IO.Ports.SerialPort(this.components);
            this.CH1ReadFlowT = new System.Windows.Forms.Timer(this.components);
            this.CH2ReadFlowT = new System.Windows.Forms.Timer(this.components);
            this.CH3ReadFlowT = new System.Windows.Forms.Timer(this.components);
            this.CH4ReadFlowT = new System.Windows.Forms.Timer(this.components);
            this.CH1ReadPress = new System.Windows.Forms.Timer(this.components);
            this.CH2ReadPress = new System.Windows.Forms.Timer(this.components);
            this.CH3ReadPress = new System.Windows.Forms.Timer(this.components);
            this.CH4ReadPress = new System.Windows.Forms.Timer(this.components);
            this.MachineStart = new System.Windows.Forms.Timer(this.components);
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CH4LeakUnit = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.CH4PressureUnit = new System.Windows.Forms.Label();
            this.right_CH2Tlight = new System.Windows.Forms.Label();
            this.RightCH2Status = new System.Windows.Forms.Label();
            this.CH4progressBar = new Sunny.UI.UIProcessBar();
            this.label78 = new System.Windows.Forms.Label();
            this.RightCH2TCP = new System.Windows.Forms.Label();
            this.CH4ParamIndex = new System.Windows.Forms.Label();
            this.CH2_2FullPress = new System.Windows.Forms.Label();
            this.CH2_2flow = new System.Windows.Forms.Label();
            this.RightCH2SmallLeak = new System.Windows.Forms.Label();
            this.RightCH2BigLeak = new System.Windows.Forms.Label();
            this.RightCH2LeakPress = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.CH2_2presstext = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.CH2LeakUnit = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.CH2PressureUnit = new System.Windows.Forms.Label();
            this.left_CH2Tlight = new System.Windows.Forms.Label();
            this.LeftCH2Status = new System.Windows.Forms.Label();
            this.CH2progressBar = new Sunny.UI.UIProcessBar();
            this.label45 = new System.Windows.Forms.Label();
            this.LeftCH2TCP = new System.Windows.Forms.Label();
            this.CH2ParamIndex = new System.Windows.Forms.Label();
            this.CH1_2flow = new System.Windows.Forms.Label();
            this.LeftCH2SmallLeak = new System.Windows.Forms.Label();
            this.CH1_2FullPress = new System.Windows.Forms.Label();
            this.LeftCH2BigLeak = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.LeakUnit = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.PressureUnit = new System.Windows.Forms.Label();
            this.LeftCH2LeakPress = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.CH1_2presstext = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.uiGroupBox3 = new Sunny.UI.UIGroupBox();
            this.label74 = new System.Windows.Forms.Label();
            this.RightCH1BigLeak = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.CH2_1flow = new System.Windows.Forms.Label();
            this.RightCH1SmallLeak = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.RightCH1TCP = new System.Windows.Forms.Label();
            this.CH3ParamIndex = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.CH2RTVDC = new System.Windows.Forms.Label();
            this.CH2RTElec = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.CH2Tlight = new System.Windows.Forms.Label();
            this.RightCH1Status = new System.Windows.Forms.Label();
            this.CH2_1FullPress = new System.Windows.Forms.Label();
            this.right_CH1Code = new Sunny.UI.UITextBox();
            this.label103 = new System.Windows.Forms.Label();
            this.CH2Status = new System.Windows.Forms.Label();
            this.CH2RTADC = new System.Windows.Forms.Label();
            this.CH3progressBar = new Sunny.UI.UIProcessBar();
            this.RightCH1LeakPress = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.CH2_1presstext = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.CH3LeakUnit = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.CH3PressureUnit = new System.Windows.Forms.Label();
            this.uiGroupBox6 = new Sunny.UI.UIGroupBox();
            this.right_CH1Tlight = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.uiGroupBox5 = new Sunny.UI.UIGroupBox();
            this.LeftCH1TCP = new System.Windows.Forms.Label();
            this.TestStation = new Sunny.UI.UITextBox();
            this.TestType = new Sunny.UI.UITextBox();
            this.ProductionItem = new Sunny.UI.UITextBox();
            this.WorkOrder = new Sunny.UI.UITextBox();
            this.ProductModel = new Sunny.UI.UITextBox();
            this.ProductName = new Sunny.UI.UITextBox();
            this.ProductNum = new Sunny.UI.UITextBox();
            this.Admin = new Sunny.UI.UITextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.left_CH1Tlight = new System.Windows.Forms.Label();
            this.LeftCH1Status = new System.Windows.Forms.Label();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.CH1progressBar = new Sunny.UI.UIProcessBar();
            this.label50 = new System.Windows.Forms.Label();
            this.CH1ParamIndex = new System.Windows.Forms.Label();
            this.CH1_1FullPress = new System.Windows.Forms.Label();
            this.CH1_1flow = new System.Windows.Forms.Label();
            this.LeftCH1SmallLeak = new System.Windows.Forms.Label();
            this.LeftCH1BigLeak = new System.Windows.Forms.Label();
            this.LeftCH1LeakPress = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.CH1_1presstext = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CH1Tlight = new System.Windows.Forms.Label();
            this.left_CH1Code = new Sunny.UI.UITextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.RightReset = new Sunny.UI.UIHeaderButton();
            this.LeftReset = new Sunny.UI.UIHeaderButton();
            this.uiHeaderButton1 = new Sunny.UI.UIHeaderButton();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.button1 = new System.Windows.Forms.Button();
            this.CH2ReceiveText = new System.Windows.Forms.TextBox();
            this.CH1ReceiveText = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PLCControl = new Sunny.UI.UIHeaderButton();
            this.CH3ReceiveText = new System.Windows.Forms.TextBox();
            this.Now = new System.Windows.Forms.Label();
            this.CH4ReceiveText = new System.Windows.Forms.TextBox();
            this.PLCRun = new System.Windows.Forms.PictureBox();
            this.CH1Status = new System.Windows.Forms.Label();
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.CH1RTVDC = new System.Windows.Forms.Label();
            this.CH1RTElec = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.CH1RTADC = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.DataGridView1 = new Sunny.UI.UIDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiTitlePanel3 = new Sunny.UI.UITitlePanel();
            this.CH1ProductNumber = new Sunny.UI.UITextBox();
            this.label233 = new System.Windows.Forms.Label();
            this.label232 = new System.Windows.Forms.Label();
            this.label231 = new System.Windows.Forms.Label();
            this.label230 = new System.Windows.Forms.Label();
            this.label229 = new System.Windows.Forms.Label();
            this.CH1CT = new Sunny.UI.UITextBox();
            this.CH1PassRate = new Sunny.UI.UITextBox();
            this.CH1FailNumber = new Sunny.UI.UITextBox();
            this.CH1PassNumber = new Sunny.UI.UITextBox();
            this.DataGridView2 = new Sunny.UI.UIDataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiTitlePanel1 = new Sunny.UI.UITitlePanel();
            this.label130 = new System.Windows.Forms.Label();
            this.label131 = new System.Windows.Forms.Label();
            this.label132 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.label134 = new System.Windows.Forms.Label();
            this.CH2CT = new Sunny.UI.UITextBox();
            this.CH2PassRate = new Sunny.UI.UITextBox();
            this.CH2FailNumber = new Sunny.UI.UITextBox();
            this.CH2PassNumber = new Sunny.UI.UITextBox();
            this.CH2ProductNumber = new Sunny.UI.UITextBox();
            this.OpenMachineINI = new System.Windows.Forms.OpenFileDialog();
            this.winforclose = new System.Windows.Forms.Timer(this.components);
            this.CodePort2 = new System.IO.Ports.SerialPort(this.components);
            this.CKCH1Port = new System.IO.Ports.SerialPort(this.components);
            this.CKCH2Port = new System.IO.Ports.SerialPort(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uiGroupBox7 = new Sunny.UI.UIGroupBox();
            this.logDisplay1 = new SLC1_N.LogDisplay();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.uiGroupBox2.SuspendLayout();
            this.uiGroupBox3.SuspendLayout();
            this.uiGroupBox6.SuspendLayout();
            this.uiGroupBox5.SuspendLayout();
            this.uiGroupBox4.SuspendLayout();
            this.uiNavBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PLCRun)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.uiTitlePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.uiTitlePanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.uiGroupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // CH1IsRun
            // 
            this.CH1IsRun.Tick += new System.EventHandler(this.IsRun_Tick);
            // 
            // ProBarRun
            // 
            this.ProBarRun.Tick += new System.EventHandler(this.ProgressBarRun_Tick);
            // 
            // CH2IsRun
            // 
            this.CH2IsRun.Tick += new System.EventHandler(this.CH2IsRun_Tick);
            // 
            // CH3IsRun
            // 
            this.CH3IsRun.Tick += new System.EventHandler(this.CH3IsRun_Tick);
            // 
            // CH4IsRun
            // 
            this.CH4IsRun.Tick += new System.EventHandler(this.CH4IsRun_Tick);
            // 
            // CodePort1
            // 
            this.CodePort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CodePort1_DataReceived);
            // 
            // PLCSignal
            // 
            this.PLCSignal.Tick += new System.EventHandler(this.PLCSignal_Tick_1);
            // 
            // ADCPort1
            // 
            this.ADCPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ADCPort1_DataReceived);
            // 
            // VDCPort1
            // 
            this.VDCPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.VDCPort1_DataReceived);
            // 
            // CH2ADCPort
            // 
            this.CH2ADCPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH2ADCPort_DataReceived);
            // 
            // CH2VDCPort
            // 
            this.CH2VDCPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH2VDCPort_DataReceived);
            // 
            // CH1ReadElec
            // 
            this.CH1ReadElec.Tick += new System.EventHandler(this.CH1ReadElec_Tick);
            // 
            // CH2ReadElec
            // 
            this.CH2ReadElec.Tick += new System.EventHandler(this.CH2ReadElec_Tick);
            // 
            // CH1ReaduA
            // 
            this.CH1ReaduA.Tick += new System.EventHandler(this.CH1ReaduA_Tick);
            // 
            // CH2ReaduA
            // 
            this.CH2ReaduA.Tick += new System.EventHandler(this.CH2ReaduA_Tick);
            // 
            // DayTime
            // 
            this.DayTime.Tick += new System.EventHandler(this.DayTime_Tick);
            // 
            // UDPOverTime
            // 
            this.UDPOverTime.Tick += new System.EventHandler(this.UDPOverTime_Tick);
            // 
            // UDPRead
            // 
            this.UDPRead.Tick += new System.EventHandler(this.UDPRead_Tick);
            // 
            // CH1LinUP
            // 
            this.CH1LinUP.Tick += new System.EventHandler(this.CH1LinUP_Tick);
            // 
            // CH2LinUP
            // 
            this.CH2LinUP.Tick += new System.EventHandler(this.CH2LinUP_Tick);
            // 
            // CH1FlowPort
            // 
            this.CH1FlowPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH1FlowPort_DataReceived);
            // 
            // CH3FlowPort
            // 
            this.CH3FlowPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH3FlowPort_DataReceived);
            // 
            // CH2FlowPort
            // 
            this.CH2FlowPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH2FlowPort_DataReceived);
            // 
            // CH4FlowPort
            // 
            this.CH4FlowPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH4FlowPort_DataReceived);
            // 
            // CH1ReadFlowT
            // 
            this.CH1ReadFlowT.Tick += new System.EventHandler(this.CH1ReadFlowT_Tick);
            // 
            // CH2ReadFlowT
            // 
            this.CH2ReadFlowT.Tick += new System.EventHandler(this.CH2ReadFlowT_Tick);
            // 
            // CH3ReadFlowT
            // 
            this.CH3ReadFlowT.Tick += new System.EventHandler(this.CH3ReadFlowT_Tick);
            // 
            // CH4ReadFlowT
            // 
            this.CH4ReadFlowT.Tick += new System.EventHandler(this.CH4ReadFlowT_Tick);
            // 
            // CH1ReadPress
            // 
            this.CH1ReadPress.Tick += new System.EventHandler(this.CH1ReadPress_Tick);
            // 
            // CH2ReadPress
            // 
            this.CH2ReadPress.Tick += new System.EventHandler(this.CH2ReadPress_Tick);
            // 
            // CH3ReadPress
            // 
            this.CH3ReadPress.Tick += new System.EventHandler(this.CH3ReadPress_Tick);
            // 
            // CH4ReadPress
            // 
            this.CH4ReadPress.Tick += new System.EventHandler(this.CH4ReadPress_Tick);
            // 
            // MachineStart
            // 
            this.MachineStart.Tick += new System.EventHandler(this.MachineStart_Tick);
            // 
            // uiGroupBox2
            // 
            resources.ApplyResources(this.uiGroupBox2, "uiGroupBox2");
            this.uiGroupBox2.Controls.Add(this.label7);
            this.uiGroupBox2.Controls.Add(this.label8);
            this.uiGroupBox2.Controls.Add(this.CH4LeakUnit);
            this.uiGroupBox2.Controls.Add(this.label14);
            this.uiGroupBox2.Controls.Add(this.CH4PressureUnit);
            this.uiGroupBox2.Controls.Add(this.right_CH2Tlight);
            this.uiGroupBox2.Controls.Add(this.RightCH2Status);
            this.uiGroupBox2.Controls.Add(this.CH4progressBar);
            this.uiGroupBox2.Controls.Add(this.label78);
            this.uiGroupBox2.Controls.Add(this.RightCH2TCP);
            this.uiGroupBox2.Controls.Add(this.CH4ParamIndex);
            this.uiGroupBox2.Controls.Add(this.CH2_2FullPress);
            this.uiGroupBox2.Controls.Add(this.CH2_2flow);
            this.uiGroupBox2.Controls.Add(this.RightCH2SmallLeak);
            this.uiGroupBox2.Controls.Add(this.RightCH2BigLeak);
            this.uiGroupBox2.Controls.Add(this.RightCH2LeakPress);
            this.uiGroupBox2.Controls.Add(this.label86);
            this.uiGroupBox2.Controls.Add(this.label87);
            this.uiGroupBox2.Controls.Add(this.label88);
            this.uiGroupBox2.Controls.Add(this.CH2_2presstext);
            this.uiGroupBox2.Controls.Add(this.label90);
            this.uiGroupBox2.Controls.Add(this.label91);
            this.uiGroupBox2.Controls.Add(this.label92);
            this.uiGroupBox2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Name = "label8";
            // 
            // CH4LeakUnit
            // 
            resources.ApplyResources(this.CH4LeakUnit, "CH4LeakUnit");
            this.CH4LeakUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH4LeakUnit.Name = "CH4LeakUnit";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Name = "label14";
            // 
            // CH4PressureUnit
            // 
            resources.ApplyResources(this.CH4PressureUnit, "CH4PressureUnit");
            this.CH4PressureUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH4PressureUnit.Name = "CH4PressureUnit";
            // 
            // right_CH2Tlight
            // 
            resources.ApplyResources(this.right_CH2Tlight, "right_CH2Tlight");
            this.right_CH2Tlight.BackColor = System.Drawing.Color.Transparent;
            this.right_CH2Tlight.ForeColor = System.Drawing.Color.Red;
            this.right_CH2Tlight.Name = "right_CH2Tlight";
            // 
            // RightCH2Status
            // 
            resources.ApplyResources(this.RightCH2Status, "RightCH2Status");
            this.RightCH2Status.BackColor = System.Drawing.Color.Transparent;
            this.RightCH2Status.Name = "RightCH2Status";
            // 
            // CH4progressBar
            // 
            resources.ApplyResources(this.CH4progressBar, "CH4progressBar");
            this.CH4progressBar.Name = "CH4progressBar";
            this.CH4progressBar.Style = Sunny.UI.UIStyle.Custom;
            // 
            // label78
            // 
            resources.ApplyResources(this.label78, "label78");
            this.label78.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label78.ForeColor = System.Drawing.Color.Black;
            this.label78.Name = "label78";
            // 
            // RightCH2TCP
            // 
            resources.ApplyResources(this.RightCH2TCP, "RightCH2TCP");
            this.RightCH2TCP.BackColor = System.Drawing.Color.Transparent;
            this.RightCH2TCP.Name = "RightCH2TCP";
            // 
            // CH4ParamIndex
            // 
            resources.ApplyResources(this.CH4ParamIndex, "CH4ParamIndex");
            this.CH4ParamIndex.BackColor = System.Drawing.Color.Transparent;
            this.CH4ParamIndex.Name = "CH4ParamIndex";
            // 
            // CH2_2FullPress
            // 
            resources.ApplyResources(this.CH2_2FullPress, "CH2_2FullPress");
            this.CH2_2FullPress.BackColor = System.Drawing.Color.Transparent;
            this.CH2_2FullPress.Name = "CH2_2FullPress";
            // 
            // CH2_2flow
            // 
            resources.ApplyResources(this.CH2_2flow, "CH2_2flow");
            this.CH2_2flow.BackColor = System.Drawing.Color.Transparent;
            this.CH2_2flow.Name = "CH2_2flow";
            // 
            // RightCH2SmallLeak
            // 
            resources.ApplyResources(this.RightCH2SmallLeak, "RightCH2SmallLeak");
            this.RightCH2SmallLeak.BackColor = System.Drawing.Color.Transparent;
            this.RightCH2SmallLeak.Name = "RightCH2SmallLeak";
            // 
            // RightCH2BigLeak
            // 
            resources.ApplyResources(this.RightCH2BigLeak, "RightCH2BigLeak");
            this.RightCH2BigLeak.BackColor = System.Drawing.Color.Transparent;
            this.RightCH2BigLeak.Name = "RightCH2BigLeak";
            // 
            // RightCH2LeakPress
            // 
            resources.ApplyResources(this.RightCH2LeakPress, "RightCH2LeakPress");
            this.RightCH2LeakPress.BackColor = System.Drawing.Color.Transparent;
            this.RightCH2LeakPress.Name = "RightCH2LeakPress";
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.BackColor = System.Drawing.Color.Transparent;
            this.label86.Name = "label86";
            // 
            // label87
            // 
            resources.ApplyResources(this.label87, "label87");
            this.label87.BackColor = System.Drawing.Color.Transparent;
            this.label87.Name = "label87";
            // 
            // label88
            // 
            resources.ApplyResources(this.label88, "label88");
            this.label88.BackColor = System.Drawing.Color.Transparent;
            this.label88.Name = "label88";
            // 
            // CH2_2presstext
            // 
            resources.ApplyResources(this.CH2_2presstext, "CH2_2presstext");
            this.CH2_2presstext.BackColor = System.Drawing.Color.Transparent;
            this.CH2_2presstext.Name = "CH2_2presstext";
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.BackColor = System.Drawing.Color.Transparent;
            this.label90.Name = "label90";
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.BackColor = System.Drawing.Color.Transparent;
            this.label91.Name = "label91";
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.BackColor = System.Drawing.Color.Transparent;
            this.label92.Name = "label92";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Name = "label16";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Name = "label25";
            // 
            // CH2LeakUnit
            // 
            resources.ApplyResources(this.CH2LeakUnit, "CH2LeakUnit");
            this.CH2LeakUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH2LeakUnit.Name = "CH2LeakUnit";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Name = "label29";
            // 
            // CH2PressureUnit
            // 
            resources.ApplyResources(this.CH2PressureUnit, "CH2PressureUnit");
            this.CH2PressureUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH2PressureUnit.Name = "CH2PressureUnit";
            // 
            // left_CH2Tlight
            // 
            resources.ApplyResources(this.left_CH2Tlight, "left_CH2Tlight");
            this.left_CH2Tlight.BackColor = System.Drawing.Color.Transparent;
            this.left_CH2Tlight.ForeColor = System.Drawing.Color.Red;
            this.left_CH2Tlight.Name = "left_CH2Tlight";
            // 
            // LeftCH2Status
            // 
            resources.ApplyResources(this.LeftCH2Status, "LeftCH2Status");
            this.LeftCH2Status.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH2Status.Name = "LeftCH2Status";
            // 
            // CH2progressBar
            // 
            resources.ApplyResources(this.CH2progressBar, "CH2progressBar");
            this.CH2progressBar.Name = "CH2progressBar";
            this.CH2progressBar.Style = Sunny.UI.UIStyle.Custom;
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label45.ForeColor = System.Drawing.Color.Black;
            this.label45.Name = "label45";
            // 
            // LeftCH2TCP
            // 
            resources.ApplyResources(this.LeftCH2TCP, "LeftCH2TCP");
            this.LeftCH2TCP.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH2TCP.Name = "LeftCH2TCP";
            // 
            // CH2ParamIndex
            // 
            resources.ApplyResources(this.CH2ParamIndex, "CH2ParamIndex");
            this.CH2ParamIndex.BackColor = System.Drawing.Color.Transparent;
            this.CH2ParamIndex.Name = "CH2ParamIndex";
            // 
            // CH1_2flow
            // 
            resources.ApplyResources(this.CH1_2flow, "CH1_2flow");
            this.CH1_2flow.BackColor = System.Drawing.Color.Transparent;
            this.CH1_2flow.Name = "CH1_2flow";
            // 
            // LeftCH2SmallLeak
            // 
            resources.ApplyResources(this.LeftCH2SmallLeak, "LeftCH2SmallLeak");
            this.LeftCH2SmallLeak.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH2SmallLeak.Name = "LeftCH2SmallLeak";
            // 
            // CH1_2FullPress
            // 
            resources.ApplyResources(this.CH1_2FullPress, "CH1_2FullPress");
            this.CH1_2FullPress.BackColor = System.Drawing.Color.Transparent;
            this.CH1_2FullPress.Name = "CH1_2FullPress";
            // 
            // LeftCH2BigLeak
            // 
            resources.ApplyResources(this.LeftCH2BigLeak, "LeftCH2BigLeak");
            this.LeftCH2BigLeak.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH2BigLeak.Name = "LeftCH2BigLeak";
            // 
            // label104
            // 
            resources.ApplyResources(this.label104, "label104");
            this.label104.BackColor = System.Drawing.Color.Transparent;
            this.label104.Name = "label104";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Name = "label28";
            // 
            // LeakUnit
            // 
            resources.ApplyResources(this.LeakUnit, "LeakUnit");
            this.LeakUnit.BackColor = System.Drawing.Color.Transparent;
            this.LeakUnit.Name = "LeakUnit";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Name = "label24";
            // 
            // label73
            // 
            resources.ApplyResources(this.label73, "label73");
            this.label73.BackColor = System.Drawing.Color.Transparent;
            this.label73.Name = "label73";
            // 
            // PressureUnit
            // 
            resources.ApplyResources(this.PressureUnit, "PressureUnit");
            this.PressureUnit.BackColor = System.Drawing.Color.Transparent;
            this.PressureUnit.Name = "PressureUnit";
            // 
            // LeftCH2LeakPress
            // 
            resources.ApplyResources(this.LeftCH2LeakPress, "LeftCH2LeakPress");
            this.LeftCH2LeakPress.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH2LeakPress.Name = "LeftCH2LeakPress";
            // 
            // label67
            // 
            resources.ApplyResources(this.label67, "label67");
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Name = "label67";
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.BackColor = System.Drawing.Color.Transparent;
            this.label68.Name = "label68";
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.BackColor = System.Drawing.Color.Transparent;
            this.label69.Name = "label69";
            // 
            // CH1_2presstext
            // 
            resources.ApplyResources(this.CH1_2presstext, "CH1_2presstext");
            this.CH1_2presstext.BackColor = System.Drawing.Color.Transparent;
            this.CH1_2presstext.Name = "CH1_2presstext";
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.BackColor = System.Drawing.Color.Transparent;
            this.label75.Name = "label75";
            // 
            // uiGroupBox3
            // 
            resources.ApplyResources(this.uiGroupBox3, "uiGroupBox3");
            this.uiGroupBox3.Controls.Add(this.label16);
            this.uiGroupBox3.Controls.Add(this.label25);
            this.uiGroupBox3.Controls.Add(this.CH2LeakUnit);
            this.uiGroupBox3.Controls.Add(this.label29);
            this.uiGroupBox3.Controls.Add(this.CH2PressureUnit);
            this.uiGroupBox3.Controls.Add(this.left_CH2Tlight);
            this.uiGroupBox3.Controls.Add(this.LeftCH2Status);
            this.uiGroupBox3.Controls.Add(this.CH2progressBar);
            this.uiGroupBox3.Controls.Add(this.label45);
            this.uiGroupBox3.Controls.Add(this.LeftCH2TCP);
            this.uiGroupBox3.Controls.Add(this.CH2ParamIndex);
            this.uiGroupBox3.Controls.Add(this.CH1_2FullPress);
            this.uiGroupBox3.Controls.Add(this.CH1_2flow);
            this.uiGroupBox3.Controls.Add(this.LeftCH2SmallLeak);
            this.uiGroupBox3.Controls.Add(this.LeftCH2BigLeak);
            this.uiGroupBox3.Controls.Add(this.LeftCH2LeakPress);
            this.uiGroupBox3.Controls.Add(this.label67);
            this.uiGroupBox3.Controls.Add(this.label68);
            this.uiGroupBox3.Controls.Add(this.label69);
            this.uiGroupBox3.Controls.Add(this.CH1_2presstext);
            this.uiGroupBox3.Controls.Add(this.label73);
            this.uiGroupBox3.Controls.Add(this.label74);
            this.uiGroupBox3.Controls.Add(this.label75);
            this.uiGroupBox3.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox3.Name = "uiGroupBox3";
            this.uiGroupBox3.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label74
            // 
            resources.ApplyResources(this.label74, "label74");
            this.label74.BackColor = System.Drawing.Color.Transparent;
            this.label74.Name = "label74";
            // 
            // RightCH1BigLeak
            // 
            resources.ApplyResources(this.RightCH1BigLeak, "RightCH1BigLeak");
            this.RightCH1BigLeak.BackColor = System.Drawing.Color.Transparent;
            this.RightCH1BigLeak.Name = "RightCH1BigLeak";
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.BackColor = System.Drawing.Color.Transparent;
            this.label93.Name = "label93";
            // 
            // CH2_1flow
            // 
            resources.ApplyResources(this.CH2_1flow, "CH2_1flow");
            this.CH2_1flow.BackColor = System.Drawing.Color.Transparent;
            this.CH2_1flow.Name = "CH2_1flow";
            // 
            // RightCH1SmallLeak
            // 
            resources.ApplyResources(this.RightCH1SmallLeak, "RightCH1SmallLeak");
            this.RightCH1SmallLeak.BackColor = System.Drawing.Color.Transparent;
            this.RightCH1SmallLeak.Name = "RightCH1SmallLeak";
            // 
            // label94
            // 
            resources.ApplyResources(this.label94, "label94");
            this.label94.BackColor = System.Drawing.Color.Transparent;
            this.label94.Name = "label94";
            // 
            // label115
            // 
            resources.ApplyResources(this.label115, "label115");
            this.label115.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label115.ForeColor = System.Drawing.Color.Black;
            this.label115.Name = "label115";
            // 
            // RightCH1TCP
            // 
            resources.ApplyResources(this.RightCH1TCP, "RightCH1TCP");
            this.RightCH1TCP.BackColor = System.Drawing.Color.Transparent;
            this.RightCH1TCP.Name = "RightCH1TCP";
            // 
            // CH3ParamIndex
            // 
            resources.ApplyResources(this.CH3ParamIndex, "CH3ParamIndex");
            this.CH3ParamIndex.BackColor = System.Drawing.Color.Transparent;
            this.CH3ParamIndex.Name = "CH3ParamIndex";
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.label95.BackColor = System.Drawing.Color.Transparent;
            this.label95.Name = "label95";
            // 
            // CH2RTVDC
            // 
            resources.ApplyResources(this.CH2RTVDC, "CH2RTVDC");
            this.CH2RTVDC.BackColor = System.Drawing.Color.Transparent;
            this.CH2RTVDC.Name = "CH2RTVDC";
            // 
            // CH2RTElec
            // 
            resources.ApplyResources(this.CH2RTElec, "CH2RTElec");
            this.CH2RTElec.BackColor = System.Drawing.Color.Transparent;
            this.CH2RTElec.Name = "CH2RTElec";
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.BackColor = System.Drawing.Color.Transparent;
            this.label98.Name = "label98";
            // 
            // label99
            // 
            resources.ApplyResources(this.label99, "label99");
            this.label99.BackColor = System.Drawing.Color.Transparent;
            this.label99.Name = "label99";
            // 
            // label100
            // 
            resources.ApplyResources(this.label100, "label100");
            this.label100.BackColor = System.Drawing.Color.Transparent;
            this.label100.Name = "label100";
            // 
            // CH2Tlight
            // 
            resources.ApplyResources(this.CH2Tlight, "CH2Tlight");
            this.CH2Tlight.BackColor = System.Drawing.Color.Transparent;
            this.CH2Tlight.ForeColor = System.Drawing.Color.Red;
            this.CH2Tlight.Name = "CH2Tlight";
            // 
            // RightCH1Status
            // 
            resources.ApplyResources(this.RightCH1Status, "RightCH1Status");
            this.RightCH1Status.BackColor = System.Drawing.Color.Transparent;
            this.RightCH1Status.Name = "RightCH1Status";
            // 
            // CH2_1FullPress
            // 
            resources.ApplyResources(this.CH2_1FullPress, "CH2_1FullPress");
            this.CH2_1FullPress.BackColor = System.Drawing.Color.Transparent;
            this.CH2_1FullPress.Name = "CH2_1FullPress";
            // 
            // right_CH1Code
            // 
            resources.ApplyResources(this.right_CH1Code, "right_CH1Code");
            this.right_CH1Code.ButtonSymbol = 61761;
            this.right_CH1Code.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.right_CH1Code.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.right_CH1Code.Maximum = 2147483647D;
            this.right_CH1Code.Minimum = -2147483648D;
            this.right_CH1Code.Name = "right_CH1Code";
            this.right_CH1Code.Style = Sunny.UI.UIStyle.Custom;
            this.right_CH1Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.right_CH1Code.TextChanged += new System.EventHandler(this.right_CH1Code_TextChanged);
            // 
            // label103
            // 
            resources.ApplyResources(this.label103, "label103");
            this.label103.BackColor = System.Drawing.Color.Transparent;
            this.label103.Name = "label103";
            // 
            // CH2Status
            // 
            resources.ApplyResources(this.CH2Status, "CH2Status");
            this.CH2Status.BackColor = System.Drawing.Color.Transparent;
            this.CH2Status.Name = "CH2Status";
            // 
            // CH2RTADC
            // 
            resources.ApplyResources(this.CH2RTADC, "CH2RTADC");
            this.CH2RTADC.BackColor = System.Drawing.Color.Transparent;
            this.CH2RTADC.Name = "CH2RTADC";
            // 
            // CH3progressBar
            // 
            resources.ApplyResources(this.CH3progressBar, "CH3progressBar");
            this.CH3progressBar.Name = "CH3progressBar";
            this.CH3progressBar.Style = Sunny.UI.UIStyle.Custom;
            // 
            // RightCH1LeakPress
            // 
            resources.ApplyResources(this.RightCH1LeakPress, "RightCH1LeakPress");
            this.RightCH1LeakPress.BackColor = System.Drawing.Color.Transparent;
            this.RightCH1LeakPress.Name = "RightCH1LeakPress";
            // 
            // label123
            // 
            resources.ApplyResources(this.label123, "label123");
            this.label123.BackColor = System.Drawing.Color.Transparent;
            this.label123.Name = "label123";
            // 
            // label124
            // 
            resources.ApplyResources(this.label124, "label124");
            this.label124.BackColor = System.Drawing.Color.Transparent;
            this.label124.Name = "label124";
            // 
            // label125
            // 
            resources.ApplyResources(this.label125, "label125");
            this.label125.BackColor = System.Drawing.Color.Transparent;
            this.label125.Name = "label125";
            // 
            // CH2_1presstext
            // 
            resources.ApplyResources(this.CH2_1presstext, "CH2_1presstext");
            this.CH2_1presstext.BackColor = System.Drawing.Color.Transparent;
            this.CH2_1presstext.Name = "CH2_1presstext";
            // 
            // label127
            // 
            resources.ApplyResources(this.label127, "label127");
            this.label127.BackColor = System.Drawing.Color.Transparent;
            this.label127.Name = "label127";
            // 
            // label102
            // 
            resources.ApplyResources(this.label102, "label102");
            this.label102.BackColor = System.Drawing.Color.Transparent;
            this.label102.Name = "label102";
            // 
            // label129
            // 
            resources.ApplyResources(this.label129, "label129");
            this.label129.BackColor = System.Drawing.Color.Transparent;
            this.label129.Name = "label129";
            // 
            // label107
            // 
            resources.ApplyResources(this.label107, "label107");
            this.label107.BackColor = System.Drawing.Color.Transparent;
            this.label107.Name = "label107";
            // 
            // label108
            // 
            resources.ApplyResources(this.label108, "label108");
            this.label108.BackColor = System.Drawing.Color.Transparent;
            this.label108.Name = "label108";
            // 
            // label109
            // 
            resources.ApplyResources(this.label109, "label109");
            this.label109.BackColor = System.Drawing.Color.Transparent;
            this.label109.Name = "label109";
            // 
            // CH3LeakUnit
            // 
            resources.ApplyResources(this.CH3LeakUnit, "CH3LeakUnit");
            this.CH3LeakUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH3LeakUnit.Name = "CH3LeakUnit";
            // 
            // label111
            // 
            resources.ApplyResources(this.label111, "label111");
            this.label111.BackColor = System.Drawing.Color.Transparent;
            this.label111.Name = "label111";
            // 
            // CH3PressureUnit
            // 
            resources.ApplyResources(this.CH3PressureUnit, "CH3PressureUnit");
            this.CH3PressureUnit.BackColor = System.Drawing.Color.Transparent;
            this.CH3PressureUnit.Name = "CH3PressureUnit";
            // 
            // uiGroupBox6
            // 
            resources.ApplyResources(this.uiGroupBox6, "uiGroupBox6");
            this.uiGroupBox6.Controls.Add(this.label108);
            this.uiGroupBox6.Controls.Add(this.label109);
            this.uiGroupBox6.Controls.Add(this.CH3LeakUnit);
            this.uiGroupBox6.Controls.Add(this.label111);
            this.uiGroupBox6.Controls.Add(this.CH3PressureUnit);
            this.uiGroupBox6.Controls.Add(this.right_CH1Tlight);
            this.uiGroupBox6.Controls.Add(this.RightCH1Status);
            this.uiGroupBox6.Controls.Add(this.CH3progressBar);
            this.uiGroupBox6.Controls.Add(this.label115);
            this.uiGroupBox6.Controls.Add(this.RightCH1TCP);
            this.uiGroupBox6.Controls.Add(this.CH3ParamIndex);
            this.uiGroupBox6.Controls.Add(this.CH2_1FullPress);
            this.uiGroupBox6.Controls.Add(this.CH2_1flow);
            this.uiGroupBox6.Controls.Add(this.RightCH1SmallLeak);
            this.uiGroupBox6.Controls.Add(this.RightCH1BigLeak);
            this.uiGroupBox6.Controls.Add(this.RightCH1LeakPress);
            this.uiGroupBox6.Controls.Add(this.label123);
            this.uiGroupBox6.Controls.Add(this.label124);
            this.uiGroupBox6.Controls.Add(this.label125);
            this.uiGroupBox6.Controls.Add(this.CH2_1presstext);
            this.uiGroupBox6.Controls.Add(this.label127);
            this.uiGroupBox6.Controls.Add(this.label128);
            this.uiGroupBox6.Controls.Add(this.label129);
            this.uiGroupBox6.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox6.Name = "uiGroupBox6";
            this.uiGroupBox6.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // right_CH1Tlight
            // 
            resources.ApplyResources(this.right_CH1Tlight, "right_CH1Tlight");
            this.right_CH1Tlight.BackColor = System.Drawing.Color.Transparent;
            this.right_CH1Tlight.ForeColor = System.Drawing.Color.Red;
            this.right_CH1Tlight.Name = "right_CH1Tlight";
            // 
            // label128
            // 
            resources.ApplyResources(this.label128, "label128");
            this.label128.BackColor = System.Drawing.Color.Transparent;
            this.label128.Name = "label128";
            // 
            // uiGroupBox5
            // 
            resources.ApplyResources(this.uiGroupBox5, "uiGroupBox5");
            this.uiGroupBox5.Controls.Add(this.label93);
            this.uiGroupBox5.Controls.Add(this.label94);
            this.uiGroupBox5.Controls.Add(this.label95);
            this.uiGroupBox5.Controls.Add(this.CH2RTVDC);
            this.uiGroupBox5.Controls.Add(this.CH2RTElec);
            this.uiGroupBox5.Controls.Add(this.label98);
            this.uiGroupBox5.Controls.Add(this.label99);
            this.uiGroupBox5.Controls.Add(this.label100);
            this.uiGroupBox5.Controls.Add(this.CH2Tlight);
            this.uiGroupBox5.Controls.Add(this.right_CH1Code);
            this.uiGroupBox5.Controls.Add(this.label102);
            this.uiGroupBox5.Controls.Add(this.label103);
            this.uiGroupBox5.Controls.Add(this.CH2Status);
            this.uiGroupBox5.Controls.Add(this.CH2RTADC);
            this.uiGroupBox5.Controls.Add(this.label107);
            this.uiGroupBox5.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox5.Name = "uiGroupBox5";
            this.uiGroupBox5.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LeftCH1TCP
            // 
            resources.ApplyResources(this.LeftCH1TCP, "LeftCH1TCP");
            this.LeftCH1TCP.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH1TCP.Name = "LeftCH1TCP";
            // 
            // TestStation
            // 
            resources.ApplyResources(this.TestStation, "TestStation");
            this.TestStation.ButtonSymbol = 61761;
            this.TestStation.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TestStation.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.TestStation.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.TestStation.Maximum = 2147483647D;
            this.TestStation.Minimum = -2147483648D;
            this.TestStation.Name = "TestStation";
            this.TestStation.ReadOnly = true;
            this.TestStation.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.TestStation.Style = Sunny.UI.UIStyle.Custom;
            this.TestStation.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TestType
            // 
            resources.ApplyResources(this.TestType, "TestType");
            this.TestType.ButtonSymbol = 61761;
            this.TestType.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TestType.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.TestType.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.TestType.Maximum = 2147483647D;
            this.TestType.Minimum = -2147483648D;
            this.TestType.Name = "TestType";
            this.TestType.ReadOnly = true;
            this.TestType.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.TestType.Style = Sunny.UI.UIStyle.Custom;
            this.TestType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductionItem
            // 
            resources.ApplyResources(this.ProductionItem, "ProductionItem");
            this.ProductionItem.ButtonSymbol = 61761;
            this.ProductionItem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProductionItem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.ProductionItem.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ProductionItem.Maximum = 2147483647D;
            this.ProductionItem.Minimum = -2147483648D;
            this.ProductionItem.Name = "ProductionItem";
            this.ProductionItem.ReadOnly = true;
            this.ProductionItem.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.ProductionItem.Style = Sunny.UI.UIStyle.Custom;
            this.ProductionItem.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WorkOrder
            // 
            resources.ApplyResources(this.WorkOrder, "WorkOrder");
            this.WorkOrder.ButtonSymbol = 61761;
            this.WorkOrder.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.WorkOrder.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.WorkOrder.Maximum = 2147483647D;
            this.WorkOrder.Minimum = -2147483648D;
            this.WorkOrder.Name = "WorkOrder";
            this.WorkOrder.ReadOnly = true;
            this.WorkOrder.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.WorkOrder.Style = Sunny.UI.UIStyle.Custom;
            this.WorkOrder.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductModel
            // 
            resources.ApplyResources(this.ProductModel, "ProductModel");
            this.ProductModel.ButtonSymbol = 61761;
            this.ProductModel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProductModel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.ProductModel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ProductModel.Maximum = 2147483647D;
            this.ProductModel.Minimum = -2147483648D;
            this.ProductModel.Name = "ProductModel";
            this.ProductModel.ReadOnly = true;
            this.ProductModel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.ProductModel.Style = Sunny.UI.UIStyle.Custom;
            this.ProductModel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductName
            // 
            resources.ApplyResources(this.ProductName, "ProductName");
            this.ProductName.ButtonSymbol = 61761;
            this.ProductName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProductName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.ProductName.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ProductName.Maximum = 2147483647D;
            this.ProductName.Minimum = -2147483648D;
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.ProductName.Style = Sunny.UI.UIStyle.Custom;
            this.ProductName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ProductNum
            // 
            resources.ApplyResources(this.ProductNum, "ProductNum");
            this.ProductNum.ButtonSymbol = 61761;
            this.ProductNum.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProductNum.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.ProductNum.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ProductNum.Maximum = 2147483647D;
            this.ProductNum.Minimum = -2147483648D;
            this.ProductNum.Name = "ProductNum";
            this.ProductNum.ReadOnly = true;
            this.ProductNum.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.ProductNum.Style = Sunny.UI.UIStyle.Custom;
            this.ProductNum.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Admin
            // 
            resources.ApplyResources(this.Admin, "Admin");
            this.Admin.ButtonSymbol = 61761;
            this.Admin.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Admin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(191)))), ((int)(((byte)(182)))));
            this.Admin.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.Admin.Maximum = 2147483647D;
            this.Admin.Minimum = -2147483648D;
            this.Admin.Name = "Admin";
            this.Admin.ReadOnly = true;
            this.Admin.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.Admin.Style = Sunny.UI.UIStyle.Custom;
            this.Admin.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // left_CH1Tlight
            // 
            resources.ApplyResources(this.left_CH1Tlight, "left_CH1Tlight");
            this.left_CH1Tlight.BackColor = System.Drawing.Color.Transparent;
            this.left_CH1Tlight.ForeColor = System.Drawing.Color.Red;
            this.left_CH1Tlight.Name = "left_CH1Tlight";
            // 
            // LeftCH1Status
            // 
            resources.ApplyResources(this.LeftCH1Status, "LeftCH1Status");
            this.LeftCH1Status.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH1Status.Name = "LeftCH1Status";
            // 
            // uiGroupBox4
            // 
            resources.ApplyResources(this.uiGroupBox4, "uiGroupBox4");
            this.uiGroupBox4.Controls.Add(this.label104);
            this.uiGroupBox4.Controls.Add(this.label28);
            this.uiGroupBox4.Controls.Add(this.LeakUnit);
            this.uiGroupBox4.Controls.Add(this.label24);
            this.uiGroupBox4.Controls.Add(this.PressureUnit);
            this.uiGroupBox4.Controls.Add(this.left_CH1Tlight);
            this.uiGroupBox4.Controls.Add(this.LeftCH1Status);
            this.uiGroupBox4.Controls.Add(this.CH1progressBar);
            this.uiGroupBox4.Controls.Add(this.label50);
            this.uiGroupBox4.Controls.Add(this.LeftCH1TCP);
            this.uiGroupBox4.Controls.Add(this.CH1ParamIndex);
            this.uiGroupBox4.Controls.Add(this.CH1_1FullPress);
            this.uiGroupBox4.Controls.Add(this.CH1_1flow);
            this.uiGroupBox4.Controls.Add(this.LeftCH1SmallLeak);
            this.uiGroupBox4.Controls.Add(this.LeftCH1BigLeak);
            this.uiGroupBox4.Controls.Add(this.LeftCH1LeakPress);
            this.uiGroupBox4.Controls.Add(this.label48);
            this.uiGroupBox4.Controls.Add(this.label59);
            this.uiGroupBox4.Controls.Add(this.label49);
            this.uiGroupBox4.Controls.Add(this.CH1_1presstext);
            this.uiGroupBox4.Controls.Add(this.label47);
            this.uiGroupBox4.Controls.Add(this.label52);
            this.uiGroupBox4.Controls.Add(this.label51);
            this.uiGroupBox4.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CH1progressBar
            // 
            resources.ApplyResources(this.CH1progressBar, "CH1progressBar");
            this.CH1progressBar.Name = "CH1progressBar";
            this.CH1progressBar.Style = Sunny.UI.UIStyle.Custom;
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.label50.ForeColor = System.Drawing.Color.Black;
            this.label50.Name = "label50";
            // 
            // CH1ParamIndex
            // 
            resources.ApplyResources(this.CH1ParamIndex, "CH1ParamIndex");
            this.CH1ParamIndex.BackColor = System.Drawing.Color.Transparent;
            this.CH1ParamIndex.Name = "CH1ParamIndex";
            // 
            // CH1_1FullPress
            // 
            resources.ApplyResources(this.CH1_1FullPress, "CH1_1FullPress");
            this.CH1_1FullPress.BackColor = System.Drawing.Color.Transparent;
            this.CH1_1FullPress.Name = "CH1_1FullPress";
            // 
            // CH1_1flow
            // 
            resources.ApplyResources(this.CH1_1flow, "CH1_1flow");
            this.CH1_1flow.BackColor = System.Drawing.Color.Transparent;
            this.CH1_1flow.Name = "CH1_1flow";
            // 
            // LeftCH1SmallLeak
            // 
            resources.ApplyResources(this.LeftCH1SmallLeak, "LeftCH1SmallLeak");
            this.LeftCH1SmallLeak.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH1SmallLeak.Name = "LeftCH1SmallLeak";
            // 
            // LeftCH1BigLeak
            // 
            resources.ApplyResources(this.LeftCH1BigLeak, "LeftCH1BigLeak");
            this.LeftCH1BigLeak.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH1BigLeak.Name = "LeftCH1BigLeak";
            // 
            // LeftCH1LeakPress
            // 
            resources.ApplyResources(this.LeftCH1LeakPress, "LeftCH1LeakPress");
            this.LeftCH1LeakPress.BackColor = System.Drawing.Color.Transparent;
            this.LeftCH1LeakPress.Name = "LeftCH1LeakPress";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.BackColor = System.Drawing.Color.Transparent;
            this.label48.Name = "label48";
            // 
            // label59
            // 
            resources.ApplyResources(this.label59, "label59");
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Name = "label59";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.BackColor = System.Drawing.Color.Transparent;
            this.label49.Name = "label49";
            // 
            // CH1_1presstext
            // 
            resources.ApplyResources(this.CH1_1presstext, "CH1_1presstext");
            this.CH1_1presstext.BackColor = System.Drawing.Color.Transparent;
            this.CH1_1presstext.Name = "CH1_1presstext";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.BackColor = System.Drawing.Color.Transparent;
            this.label47.Name = "label47";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.BackColor = System.Drawing.Color.Transparent;
            this.label52.Name = "label52";
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.BackColor = System.Drawing.Color.Transparent;
            this.label51.Name = "label51";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // CH1Tlight
            // 
            resources.ApplyResources(this.CH1Tlight, "CH1Tlight");
            this.CH1Tlight.BackColor = System.Drawing.Color.Transparent;
            this.CH1Tlight.ForeColor = System.Drawing.Color.Red;
            this.CH1Tlight.Name = "CH1Tlight";
            // 
            // left_CH1Code
            // 
            resources.ApplyResources(this.left_CH1Code, "left_CH1Code");
            this.left_CH1Code.ButtonSymbol = 61761;
            this.left_CH1Code.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.left_CH1Code.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.left_CH1Code.Maximum = 2147483647D;
            this.left_CH1Code.Minimum = -2147483648D;
            this.left_CH1Code.Name = "left_CH1Code";
            this.left_CH1Code.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.left_CH1Code.TextChanged += new System.EventHandler(this.left_CH1Code_TextChanged);
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Name = "label32";
            // 
            // RightReset
            // 
            resources.ApplyResources(this.RightReset, "RightReset");
            this.RightReset.CircleColor = System.Drawing.Color.Transparent;
            this.RightReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.RightReset.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.RightReset.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.RightReset.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.RightReset.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.RightReset.Name = "RightReset";
            this.RightReset.Radius = 0;
            this.RightReset.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.RightReset.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.RightReset.Style = Sunny.UI.UIStyle.Custom;
            this.RightReset.Symbol = 95;
            this.RightReset.SymbolOffset = new System.Drawing.Point(-1, 2);
            this.RightReset.SymbolSize = 70;
            this.RightReset.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RightReset.Click += new System.EventHandler(this.RightReset_Click);
            // 
            // LeftReset
            // 
            resources.ApplyResources(this.LeftReset, "LeftReset");
            this.LeftReset.CircleColor = System.Drawing.Color.Transparent;
            this.LeftReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.LeftReset.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.LeftReset.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.LeftReset.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.LeftReset.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.LeftReset.Name = "LeftReset";
            this.LeftReset.Radius = 0;
            this.LeftReset.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.LeftReset.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.LeftReset.Style = Sunny.UI.UIStyle.Custom;
            this.LeftReset.Symbol = 95;
            this.LeftReset.SymbolOffset = new System.Drawing.Point(-1, 2);
            this.LeftReset.SymbolSize = 70;
            this.LeftReset.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LeftReset.Click += new System.EventHandler(this.LeftReset_Click);
            // 
            // uiHeaderButton1
            // 
            resources.ApplyResources(this.uiHeaderButton1, "uiHeaderButton1");
            this.uiHeaderButton1.CircleColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.FillColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.FillDisableColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.FillHoverColor = System.Drawing.Color.RosyBrown;
            this.uiHeaderButton1.FillPressColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.FillSelectedColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.ForeColor = System.Drawing.Color.Transparent;
            this.uiHeaderButton1.Name = "uiHeaderButton1";
            this.uiHeaderButton1.Radius = 0;
            this.uiHeaderButton1.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.uiHeaderButton1.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.uiHeaderButton1.Style = Sunny.UI.UIStyle.Custom;
            this.uiHeaderButton1.Symbol = 61579;
            this.uiHeaderButton1.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.uiHeaderButton1.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiHeaderButton1.Click += new System.EventHandler(this.uiHeaderButton1_Click);
            // 
            // uiNavBar1
            // 
            resources.ApplyResources(this.uiNavBar1, "uiNavBar1");
            this.uiNavBar1.BackColor = System.Drawing.Color.LightBlue;
            this.uiNavBar1.Controls.Add(this.button1);
            this.uiNavBar1.Controls.Add(this.CH2ReceiveText);
            this.uiNavBar1.Controls.Add(this.CH1ReceiveText);
            this.uiNavBar1.Controls.Add(this.textBox2);
            this.uiNavBar1.Controls.Add(this.textBox1);
            this.uiNavBar1.Controls.Add(this.PLCControl);
            this.uiNavBar1.Controls.Add(this.CH3ReceiveText);
            this.uiNavBar1.Controls.Add(this.Now);
            this.uiNavBar1.Controls.Add(this.CH4ReceiveText);
            this.uiNavBar1.Controls.Add(this.TestStation);
            this.uiNavBar1.Controls.Add(this.TestType);
            this.uiNavBar1.Controls.Add(this.ProductionItem);
            this.uiNavBar1.Controls.Add(this.WorkOrder);
            this.uiNavBar1.Controls.Add(this.ProductModel);
            this.uiNavBar1.Controls.Add(this.ProductName);
            this.uiNavBar1.Controls.Add(this.ProductNum);
            this.uiNavBar1.Controls.Add(this.Admin);
            this.uiNavBar1.Controls.Add(this.label12);
            this.uiNavBar1.Controls.Add(this.label11);
            this.uiNavBar1.Controls.Add(this.label10);
            this.uiNavBar1.Controls.Add(this.label9);
            this.uiNavBar1.Controls.Add(this.label4);
            this.uiNavBar1.Controls.Add(this.label3);
            this.uiNavBar1.Controls.Add(this.label2);
            this.uiNavBar1.Controls.Add(this.label1);
            this.uiNavBar1.Controls.Add(this.PLCRun);
            this.uiNavBar1.Controls.Add(this.label22);
            this.uiNavBar1.Controls.Add(this.RightReset);
            this.uiNavBar1.Controls.Add(this.LeftReset);
            this.uiNavBar1.Controls.Add(this.uiHeaderButton1);
            this.uiNavBar1.DropMenuFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.ForeColor = System.Drawing.Color.Black;
            this.uiNavBar1.MenuHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(163)))), ((int)(((byte)(133)))));
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(163)))), ((int)(((byte)(133)))));
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.NodeInterval = 0;
            this.uiNavBar1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes1"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes2"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes3"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes4"))),
            ((System.Windows.Forms.TreeNode)(resources.GetObject("uiNavBar1.Nodes5")))});
            this.uiNavBar1.SelectedForeColor = System.Drawing.Color.Black;
            this.uiNavBar1.SelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(164)))), ((int)(((byte)(144)))));
            this.uiNavBar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiNavBar1.MenuItemClick += new Sunny.UI.UINavBar.OnMenuItemClick(this.uiNavBar1_MenuItemClick);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CH2ReceiveText
            // 
            resources.ApplyResources(this.CH2ReceiveText, "CH2ReceiveText");
            this.CH2ReceiveText.Name = "CH2ReceiveText";
            // 
            // CH1ReceiveText
            // 
            resources.ApplyResources(this.CH1ReceiveText, "CH1ReceiveText");
            this.CH1ReceiveText.Name = "CH1ReceiveText";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // PLCControl
            // 
            resources.ApplyResources(this.PLCControl, "PLCControl");
            this.PLCControl.CircleColor = System.Drawing.Color.Transparent;
            this.PLCControl.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.PLCControl.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.PLCControl.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.PLCControl.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.PLCControl.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(215)))), ((int)(((byte)(230)))));
            this.PLCControl.Name = "PLCControl";
            this.PLCControl.Radius = 0;
            this.PLCControl.RadiusSides = Sunny.UI.UICornerRadiusSides.None;
            this.PLCControl.RectSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.None;
            this.PLCControl.Style = Sunny.UI.UIStyle.Custom;
            this.PLCControl.Symbol = 61459;
            this.PLCControl.SymbolOffset = new System.Drawing.Point(-1, 2);
            this.PLCControl.SymbolSize = 70;
            this.PLCControl.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PLCControl.Click += new System.EventHandler(this.PLCControl_Click);
            // 
            // CH3ReceiveText
            // 
            resources.ApplyResources(this.CH3ReceiveText, "CH3ReceiveText");
            this.CH3ReceiveText.Name = "CH3ReceiveText";
            // 
            // Now
            // 
            resources.ApplyResources(this.Now, "Now");
            this.Now.Name = "Now";
            // 
            // CH4ReceiveText
            // 
            resources.ApplyResources(this.CH4ReceiveText, "CH4ReceiveText");
            this.CH4ReceiveText.Name = "CH4ReceiveText";
            // 
            // PLCRun
            // 
            resources.ApplyResources(this.PLCRun, "PLCRun");
            this.PLCRun.Name = "PLCRun";
            this.PLCRun.TabStop = false;
            // 
            // CH1Status
            // 
            resources.ApplyResources(this.CH1Status, "CH1Status");
            this.CH1Status.BackColor = System.Drawing.Color.Transparent;
            this.CH1Status.Name = "CH1Status";
            // 
            // uiGroupBox1
            // 
            resources.ApplyResources(this.uiGroupBox1, "uiGroupBox1");
            this.uiGroupBox1.Controls.Add(this.label30);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.label39);
            this.uiGroupBox1.Controls.Add(this.CH1RTVDC);
            this.uiGroupBox1.Controls.Add(this.CH1RTElec);
            this.uiGroupBox1.Controls.Add(this.label5);
            this.uiGroupBox1.Controls.Add(this.label42);
            this.uiGroupBox1.Controls.Add(this.label35);
            this.uiGroupBox1.Controls.Add(this.CH1Tlight);
            this.uiGroupBox1.Controls.Add(this.left_CH1Code);
            this.uiGroupBox1.Controls.Add(this.label38);
            this.uiGroupBox1.Controls.Add(this.label32);
            this.uiGroupBox1.Controls.Add(this.CH1Status);
            this.uiGroupBox1.Controls.Add(this.CH1RTADC);
            this.uiGroupBox1.Controls.Add(this.label43);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiGroupBox1.Click += new System.EventHandler(this.uiGroupBox1_Click);
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Name = "label30";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Name = "label39";
            // 
            // CH1RTVDC
            // 
            resources.ApplyResources(this.CH1RTVDC, "CH1RTVDC");
            this.CH1RTVDC.BackColor = System.Drawing.Color.Transparent;
            this.CH1RTVDC.Name = "CH1RTVDC";
            this.CH1RTVDC.Click += new System.EventHandler(this.CH1RTVDC_Click);
            // 
            // CH1RTElec
            // 
            resources.ApplyResources(this.CH1RTElec, "CH1RTElec");
            this.CH1RTElec.BackColor = System.Drawing.Color.Transparent;
            this.CH1RTElec.Name = "CH1RTElec";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Name = "label42";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Name = "label35";
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.Name = "label38";
            // 
            // CH1RTADC
            // 
            resources.ApplyResources(this.CH1RTADC, "CH1RTADC");
            this.CH1RTADC.BackColor = System.Drawing.Color.Transparent;
            this.CH1RTADC.Name = "CH1RTADC";
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Name = "label43";
            // 
            // DataGridView1
            // 
            resources.ApplyResources(this.DataGridView1, "DataGridView1");
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.DataGridView1.EnableHeadersVisualStyles = false;
            this.DataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.SelectedIndex = -1;
            this.DataGridView1.ShowGridLine = true;
            this.DataGridView1.StyleCustomMode = true;
            this.DataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGridView1_RowPrePaint);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 40F;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 140F;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 40F;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 25F;
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 46F;
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 46F;
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.FillWeight = 30F;
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // uiTitlePanel3
            // 
            resources.ApplyResources(this.uiTitlePanel3, "uiTitlePanel3");
            this.uiTitlePanel3.Controls.Add(this.CH1ProductNumber);
            this.uiTitlePanel3.Controls.Add(this.label233);
            this.uiTitlePanel3.Controls.Add(this.label232);
            this.uiTitlePanel3.Controls.Add(this.label231);
            this.uiTitlePanel3.Controls.Add(this.label230);
            this.uiTitlePanel3.Controls.Add(this.label229);
            this.uiTitlePanel3.Controls.Add(this.CH1CT);
            this.uiTitlePanel3.Controls.Add(this.CH1PassRate);
            this.uiTitlePanel3.Controls.Add(this.CH1FailNumber);
            this.uiTitlePanel3.Controls.Add(this.CH1PassNumber);
            this.uiTitlePanel3.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiTitlePanel3.Name = "uiTitlePanel3";
            this.uiTitlePanel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CH1ProductNumber
            // 
            resources.ApplyResources(this.CH1ProductNumber, "CH1ProductNumber");
            this.CH1ProductNumber.ButtonSymbol = 61761;
            this.CH1ProductNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH1ProductNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH1ProductNumber.Maximum = 2147483647D;
            this.CH1ProductNumber.Minimum = -2147483648D;
            this.CH1ProductNumber.Name = "CH1ProductNumber";
            this.CH1ProductNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH1ProductNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label233
            // 
            resources.ApplyResources(this.label233, "label233");
            this.label233.BackColor = System.Drawing.Color.Transparent;
            this.label233.Name = "label233";
            // 
            // label232
            // 
            resources.ApplyResources(this.label232, "label232");
            this.label232.BackColor = System.Drawing.Color.Transparent;
            this.label232.Name = "label232";
            // 
            // label231
            // 
            resources.ApplyResources(this.label231, "label231");
            this.label231.BackColor = System.Drawing.Color.Transparent;
            this.label231.Name = "label231";
            // 
            // label230
            // 
            resources.ApplyResources(this.label230, "label230");
            this.label230.BackColor = System.Drawing.Color.Transparent;
            this.label230.Name = "label230";
            // 
            // label229
            // 
            resources.ApplyResources(this.label229, "label229");
            this.label229.BackColor = System.Drawing.Color.Transparent;
            this.label229.Name = "label229";
            // 
            // CH1CT
            // 
            resources.ApplyResources(this.CH1CT, "CH1CT");
            this.CH1CT.ButtonSymbol = 61761;
            this.CH1CT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH1CT.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH1CT.Maximum = 2147483647D;
            this.CH1CT.Minimum = -2147483648D;
            this.CH1CT.Name = "CH1CT";
            this.CH1CT.ReadOnly = true;
            this.CH1CT.Style = Sunny.UI.UIStyle.Custom;
            this.CH1CT.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH1PassRate
            // 
            resources.ApplyResources(this.CH1PassRate, "CH1PassRate");
            this.CH1PassRate.ButtonSymbol = 61761;
            this.CH1PassRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH1PassRate.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH1PassRate.Maximum = 2147483647D;
            this.CH1PassRate.Minimum = -2147483648D;
            this.CH1PassRate.Name = "CH1PassRate";
            this.CH1PassRate.ReadOnly = true;
            this.CH1PassRate.Style = Sunny.UI.UIStyle.Custom;
            this.CH1PassRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH1FailNumber
            // 
            resources.ApplyResources(this.CH1FailNumber, "CH1FailNumber");
            this.CH1FailNumber.ButtonSymbol = 61761;
            this.CH1FailNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH1FailNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH1FailNumber.Maximum = 2147483647D;
            this.CH1FailNumber.Minimum = -2147483648D;
            this.CH1FailNumber.Name = "CH1FailNumber";
            this.CH1FailNumber.ReadOnly = true;
            this.CH1FailNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH1FailNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH1PassNumber
            // 
            resources.ApplyResources(this.CH1PassNumber, "CH1PassNumber");
            this.CH1PassNumber.ButtonSymbol = 61761;
            this.CH1PassNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH1PassNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH1PassNumber.Maximum = 2147483647D;
            this.CH1PassNumber.Minimum = -2147483648D;
            this.CH1PassNumber.Name = "CH1PassNumber";
            this.CH1PassNumber.ReadOnly = true;
            this.CH1PassNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH1PassNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DataGridView2
            // 
            resources.ApplyResources(this.DataGridView2, "DataGridView2");
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.DataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.DataGridView2.EnableHeadersVisualStyles = false;
            this.DataGridView2.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DataGridView2.RowHeadersVisible = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            this.DataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.DataGridView2.RowTemplate.Height = 23;
            this.DataGridView2.SelectedIndex = -1;
            this.DataGridView2.ShowGridLine = true;
            this.DataGridView2.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.DataGridView2_RowPrePaint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 40F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.MaxInputLength = 25000;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 140F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.MaxInputLength = 38000;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 46.26217F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FillWeight = 25F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FillWeight = 46F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 46F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 30F;
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // uiTitlePanel1
            // 
            resources.ApplyResources(this.uiTitlePanel1, "uiTitlePanel1");
            this.uiTitlePanel1.Controls.Add(this.label130);
            this.uiTitlePanel1.Controls.Add(this.label131);
            this.uiTitlePanel1.Controls.Add(this.label132);
            this.uiTitlePanel1.Controls.Add(this.label133);
            this.uiTitlePanel1.Controls.Add(this.label134);
            this.uiTitlePanel1.Controls.Add(this.CH2CT);
            this.uiTitlePanel1.Controls.Add(this.CH2PassRate);
            this.uiTitlePanel1.Controls.Add(this.CH2FailNumber);
            this.uiTitlePanel1.Controls.Add(this.CH2PassNumber);
            this.uiTitlePanel1.Controls.Add(this.CH2ProductNumber);
            this.uiTitlePanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiTitlePanel1.Name = "uiTitlePanel1";
            this.uiTitlePanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTitlePanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label130
            // 
            resources.ApplyResources(this.label130, "label130");
            this.label130.BackColor = System.Drawing.Color.Transparent;
            this.label130.Name = "label130";
            // 
            // label131
            // 
            resources.ApplyResources(this.label131, "label131");
            this.label131.BackColor = System.Drawing.Color.Transparent;
            this.label131.Name = "label131";
            // 
            // label132
            // 
            resources.ApplyResources(this.label132, "label132");
            this.label132.BackColor = System.Drawing.Color.Transparent;
            this.label132.Name = "label132";
            // 
            // label133
            // 
            resources.ApplyResources(this.label133, "label133");
            this.label133.BackColor = System.Drawing.Color.Transparent;
            this.label133.Name = "label133";
            // 
            // label134
            // 
            resources.ApplyResources(this.label134, "label134");
            this.label134.BackColor = System.Drawing.Color.Transparent;
            this.label134.Name = "label134";
            // 
            // CH2CT
            // 
            resources.ApplyResources(this.CH2CT, "CH2CT");
            this.CH2CT.ButtonSymbol = 61761;
            this.CH2CT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH2CT.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH2CT.Maximum = 2147483647D;
            this.CH2CT.Minimum = -2147483648D;
            this.CH2CT.Name = "CH2CT";
            this.CH2CT.ReadOnly = true;
            this.CH2CT.Style = Sunny.UI.UIStyle.Custom;
            this.CH2CT.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH2PassRate
            // 
            resources.ApplyResources(this.CH2PassRate, "CH2PassRate");
            this.CH2PassRate.ButtonSymbol = 61761;
            this.CH2PassRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH2PassRate.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH2PassRate.Maximum = 2147483647D;
            this.CH2PassRate.Minimum = -2147483648D;
            this.CH2PassRate.Name = "CH2PassRate";
            this.CH2PassRate.ReadOnly = true;
            this.CH2PassRate.Style = Sunny.UI.UIStyle.Custom;
            this.CH2PassRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH2FailNumber
            // 
            resources.ApplyResources(this.CH2FailNumber, "CH2FailNumber");
            this.CH2FailNumber.ButtonSymbol = 61761;
            this.CH2FailNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH2FailNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH2FailNumber.Maximum = 2147483647D;
            this.CH2FailNumber.Minimum = -2147483648D;
            this.CH2FailNumber.Name = "CH2FailNumber";
            this.CH2FailNumber.ReadOnly = true;
            this.CH2FailNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH2FailNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH2PassNumber
            // 
            resources.ApplyResources(this.CH2PassNumber, "CH2PassNumber");
            this.CH2PassNumber.ButtonSymbol = 61761;
            this.CH2PassNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH2PassNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH2PassNumber.Maximum = 2147483647D;
            this.CH2PassNumber.Minimum = -2147483648D;
            this.CH2PassNumber.Name = "CH2PassNumber";
            this.CH2PassNumber.ReadOnly = true;
            this.CH2PassNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH2PassNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CH2ProductNumber
            // 
            resources.ApplyResources(this.CH2ProductNumber, "CH2ProductNumber");
            this.CH2ProductNumber.ButtonSymbol = 61761;
            this.CH2ProductNumber.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CH2ProductNumber.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.CH2ProductNumber.Maximum = 2147483647D;
            this.CH2ProductNumber.Minimum = -2147483648D;
            this.CH2ProductNumber.Name = "CH2ProductNumber";
            this.CH2ProductNumber.ReadOnly = true;
            this.CH2ProductNumber.Style = Sunny.UI.UIStyle.Custom;
            this.CH2ProductNumber.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OpenMachineINI
            // 
            this.OpenMachineINI.FileName = "openFileDialog1";
            resources.ApplyResources(this.OpenMachineINI, "OpenMachineINI");
            // 
            // winforclose
            // 
            this.winforclose.Interval = 200;
            this.winforclose.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CodePort2
            // 
            this.CodePort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CodePort2_DataReceived);
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox7, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.uiTitlePanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiTitlePanel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.uiGroupBox3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.DataGridView2, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.DataGridView1, 0, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // uiGroupBox7
            // 
            resources.ApplyResources(this.uiGroupBox7, "uiGroupBox7");
            this.tableLayoutPanel1.SetColumnSpan(this.uiGroupBox7, 2);
            this.uiGroupBox7.Controls.Add(this.logDisplay1);
            this.uiGroupBox7.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.uiGroupBox7.Name = "uiGroupBox7";
            this.uiGroupBox7.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox7.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logDisplay1
            // 
            resources.ApplyResources(this.logDisplay1, "logDisplay1");
            this.logDisplay1.Name = "logDisplay1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.uiNavBar1);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.uiGroupBox3.ResumeLayout(false);
            this.uiGroupBox3.PerformLayout();
            this.uiGroupBox6.ResumeLayout(false);
            this.uiGroupBox6.PerformLayout();
            this.uiGroupBox5.ResumeLayout(false);
            this.uiGroupBox5.PerformLayout();
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox4.PerformLayout();
            this.uiNavBar1.ResumeLayout(false);
            this.uiNavBar1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PLCRun)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.uiTitlePanel3.ResumeLayout(false);
            this.uiTitlePanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.uiTitlePanel1.ResumeLayout(false);
            this.uiTitlePanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.uiGroupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Timer CH1IsRun;
        private System.Windows.Forms.Timer ProBarRun;
        public System.Windows.Forms.Timer CH2IsRun;
        public System.Windows.Forms.Timer CH4IsRun;
        public System.Windows.Forms.Timer CH3IsRun;
        private System.Windows.Forms.Timer PLCSignal;
        private System.Windows.Forms.Timer CH1ReaduA;
        private System.Windows.Forms.Timer CH2ReaduA;
        public System.IO.Ports.SerialPort ADCPort1;
        public System.IO.Ports.SerialPort VDCPort1;
        public System.IO.Ports.SerialPort CH2ADCPort;
        public System.IO.Ports.SerialPort CH2VDCPort;
        public System.Windows.Forms.Timer DayTime;
        private System.Windows.Forms.Timer UDPOverTime;
        private System.Windows.Forms.Timer UDPRead;
        private System.Windows.Forms.Timer CH1LinUP;
        private System.Windows.Forms.Timer CH2LinUP;
        public System.Windows.Forms.Timer CH1ReadElec;
        public System.Windows.Forms.Timer CH2ReadElec;
        public System.IO.Ports.SerialPort CodePort1;
        public System.IO.Ports.SerialPort CH1FlowPort;
        public System.IO.Ports.SerialPort CH3FlowPort;
        public System.IO.Ports.SerialPort CH2FlowPort;
        public System.IO.Ports.SerialPort CH4FlowPort;
        private System.Windows.Forms.Timer CH1ReadFlowT;
        private System.Windows.Forms.Timer CH2ReadFlowT;
        private System.Windows.Forms.Timer CH3ReadFlowT;
        private System.Windows.Forms.Timer CH4ReadFlowT;
        private System.Windows.Forms.Timer CH1ReadPress;
        private System.Windows.Forms.Timer CH2ReadPress;
        private System.Windows.Forms.Timer CH3ReadPress;
        private System.Windows.Forms.Timer CH4ReadPress;
        private System.Windows.Forms.Timer MachineStart;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private System.Windows.Forms.Label right_CH2Tlight;
        private System.Windows.Forms.Label RightCH2Status;
        private Sunny.UI.UIProcessBar CH4progressBar;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label CH4ParamIndex;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label CH2_2presstext;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label left_CH2Tlight;
        private System.Windows.Forms.Label LeftCH2Status;
        private Sunny.UI.UIProcessBar CH2progressBar;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label CH2ParamIndex;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label CH1_2presstext;
        private System.Windows.Forms.Label label75;
        private Sunny.UI.UIGroupBox uiGroupBox3;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label CH3ParamIndex;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label CH2RTVDC;
        private System.Windows.Forms.Label CH2RTElec;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label CH2Tlight;
        private System.Windows.Forms.Label RightCH1Status;
        private Sunny.UI.UITextBox right_CH1Code;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label CH2Status;
        private System.Windows.Forms.Label CH2RTADC;
        private Sunny.UI.UIProcessBar CH3progressBar;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label CH2_1presstext;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label right_CH1Tlight;
        private System.Windows.Forms.Label label128;
        private Sunny.UI.UIGroupBox uiGroupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label left_CH1Tlight;
        private System.Windows.Forms.Label LeftCH1Status;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label CH1ParamIndex;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label CH1_1presstext;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PLCRun;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label CH1Tlight;
        private Sunny.UI.UITextBox left_CH1Code;
        private System.Windows.Forms.Label label32;
        private Sunny.UI.UIHeaderButton RightReset;
        private Sunny.UI.UIHeaderButton LeftReset;
        private Sunny.UI.UIHeaderButton uiHeaderButton1;
        private Sunny.UI.UINavBar uiNavBar1;
        private System.Windows.Forms.Label Now;
        private System.Windows.Forms.Label CH1Status;
        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label CH1RTVDC;
        private System.Windows.Forms.Label CH1RTElec;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label CH1RTADC;
        private System.Windows.Forms.Label label43;
        private Sunny.UI.UIDataGridView DataGridView1;
        private Sunny.UI.UITitlePanel uiTitlePanel3;
        private System.Windows.Forms.Label label233;
        private System.Windows.Forms.Label label232;
        private System.Windows.Forms.Label label231;
        private System.Windows.Forms.Label label230;
        private System.Windows.Forms.Label label229;
        private Sunny.UI.UIDataGridView DataGridView2;
        private Sunny.UI.UITitlePanel uiTitlePanel1;
        private System.Windows.Forms.Label label130;
        private System.Windows.Forms.Label label131;
        private System.Windows.Forms.Label label132;
        private System.Windows.Forms.Label label133;
        private System.Windows.Forms.Label label134;
        private Sunny.UI.UIProcessBar CH1progressBar;
        private Sunny.UI.UIHeaderButton PLCControl;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox CH3ReceiveText;
        private System.Windows.Forms.TextBox CH4ReceiveText;
        private System.Windows.Forms.TextBox CH2ReceiveText;
        private System.Windows.Forms.TextBox CH1ReceiveText;
        public Sunny.UI.UITextBox TestStation;
        public Sunny.UI.UITextBox TestType;
        public Sunny.UI.UITextBox ProductionItem;
        public Sunny.UI.UITextBox WorkOrder;
        public Sunny.UI.UITextBox ProductModel;
        public Sunny.UI.UITextBox ProductName;
        public Sunny.UI.UITextBox ProductNum;
        public Sunny.UI.UITextBox Admin;
        public System.Windows.Forms.Label RightCH2TCP;
        public System.Windows.Forms.Label LeftCH2TCP;
        public System.Windows.Forms.Label RightCH1TCP;
        public System.Windows.Forms.Label LeftCH1TCP;
        private System.Windows.Forms.OpenFileDialog OpenMachineINI;
        public Sunny.UI.UITextBox CH1CT;
        public Sunny.UI.UITextBox CH1PassRate;
        public Sunny.UI.UITextBox CH1FailNumber;
        public Sunny.UI.UITextBox CH1PassNumber;
        public Sunny.UI.UITextBox CH2CT;
        public Sunny.UI.UITextBox CH2PassRate;
        public Sunny.UI.UITextBox CH2FailNumber;
        public Sunny.UI.UITextBox CH2PassNumber;
        public Sunny.UI.UITextBox CH2ProductNumber;
        public Sunny.UI.UITextBox CH1ProductNumber;
        private System.Windows.Forms.Timer winforclose;
        public System.IO.Ports.SerialPort CodePort2;
        public System.IO.Ports.SerialPort CKCH1Port;
        public System.IO.Ports.SerialPort CKCH2Port;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Sunny.UI.UIGroupBox uiGroupBox7;
        private LogDisplay logDisplay1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        public System.Windows.Forms.Label CH2_2FullPress;
        public System.Windows.Forms.Label CH2_2flow;
        public System.Windows.Forms.Label RightCH2SmallLeak;
        public System.Windows.Forms.Label RightCH2BigLeak;
        public System.Windows.Forms.Label RightCH2LeakPress;
        public System.Windows.Forms.Label CH1_2flow;
        public System.Windows.Forms.Label LeftCH2SmallLeak;
        public System.Windows.Forms.Label CH1_2FullPress;
        public System.Windows.Forms.Label LeftCH2BigLeak;
        public System.Windows.Forms.Label LeftCH2LeakPress;
        public System.Windows.Forms.Label RightCH1BigLeak;
        public System.Windows.Forms.Label CH2_1flow;
        public System.Windows.Forms.Label RightCH1SmallLeak;
        public System.Windows.Forms.Label CH2_1FullPress;
        public System.Windows.Forms.Label RightCH1LeakPress;
        public System.Windows.Forms.Label CH1_1FullPress;
        public System.Windows.Forms.Label CH1_1flow;
        public System.Windows.Forms.Label LeftCH1SmallLeak;
        public System.Windows.Forms.Label LeftCH1BigLeak;
        public System.Windows.Forms.Label LeftCH1LeakPress;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label CH4LeakUnit;
        public System.Windows.Forms.Label label14;
        public System.Windows.Forms.Label CH4PressureUnit;
        public System.Windows.Forms.Label label16;
        public System.Windows.Forms.Label label25;
        public System.Windows.Forms.Label CH2LeakUnit;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.Label CH2PressureUnit;
        public System.Windows.Forms.Label label104;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.Label LeakUnit;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.Label PressureUnit;
        public System.Windows.Forms.Label label108;
        public System.Windows.Forms.Label label109;
        public System.Windows.Forms.Label CH3LeakUnit;
        public System.Windows.Forms.Label label111;
        public System.Windows.Forms.Label CH3PressureUnit;
        public Sunny.UI.UIGroupBox uiGroupBox6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

