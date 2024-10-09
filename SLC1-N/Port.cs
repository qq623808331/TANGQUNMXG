using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Port : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        public Port()
        {
            InitializeComponent();
        }

        private void Port_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;

            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH1ADCPort.Items.AddRange(ports);
            CH1VDCPort.Items.AddRange(ports);
            CH2ADCPort.Items.AddRange(ports);
            CH2VDCPort.Items.AddRange(ports);
            CodePort.Items.AddRange(ports);
            CH2CodePort.Items.AddRange(ports);
            CH1FlowPort.Items.AddRange(ports);
            CH2FlowPort.Items.AddRange(ports);
            CH3FlowPort.Items.AddRange(ports);
            CH4FlowPort.Items.AddRange(ports);

            CKCH1Port.Items.AddRange(ports);
            CKCH2Port.Items.AddRange(ports);
            ReadStatus.Interval = 500;
            ReadStatus.Start();
            CH1ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CH1ADCIsComm.ForeColor = Color.Red;
            CH2ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CH2ADCIsComm.ForeColor = Color.Red;
            CH1VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CH1VDCIsComm.ForeColor = Color.Red;
            CH2VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CH2VDCIsComm.ForeColor = Color.Red;
            CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CodeIsComm.ForeColor = Color.Red;
            CH2CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开");
            CH2CodeIsComm.ForeColor = Color.Red;
            ReadPort();
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        /// <summary>
        /// 读取工作电流的串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1ADCCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.ADCPort1.BaudRate = int.Parse(CH1ADCBaud.Text);
                Form1.f1.ADCPort1.PortName = CH1ADCPort.Text;
                Form1.f1.ADCPort1.DataBits = 8;
                Form1.f1.ADCPort1.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.ADCPort1.Parity = System.IO.Ports.Parity.None;
                Form1.f1.ADCPort1.Open();
                if (Form1.f1.ADCPort1.IsOpen)
                {
                    CH1ADCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1ADCIsComm.ForeColor = Color.Green;
                    CH1ADCPort.Enabled = false;
                    CH1ADCCon.Enabled = false;
                    CH1ADCBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI mesconfig = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    mesconfig.IniWriteValue("Port", "CH1ADCPort", CH1ADCPort.Text);
                    mesconfig.IniWriteValue("Port", "CH1ADCBaud", CH1ADCBaud.Text);
                    if (Form1.f1.ADCPort1.IsOpen)
                    {
                        byte[] data = Encoding.Default.GetBytes("ADC\r\n");
                        Form1.f1.ADCPort1.Write(data, 0, data.Length);
                    }
                    if (Form1.f1.VDCPort1.IsOpen)
                    {
                        //计算电压和电流是否超过上下限
                        Form1.f1.CH1ReadElec.Interval = 1200;
                        Form1.f1.CH1ReadElec.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH1ADCRefresh_Click(object sender, EventArgs e)
        {
            CH1ADCPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH1ADCPort.Items.AddRange(ports);
        }

        private void CH1ADCBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1ReadElec.Stop();
            Form1.f1.ADCPort1.Close();
            if (!Form1.f1.ADCPort1.IsOpen)
            {
                CH1ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH1ADCIsComm.ForeColor = Color.Red;
                CH1ADCPort.Enabled = true;
                CH1ADCCon.Enabled = true;
                CH1ADCBaud.Enabled = true;
            }
        }

        /// <summary>
        /// 读取工作电压的串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1VDCCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.VDCPort1.BaudRate = int.Parse(CH1VDCBaud.Text);
                Form1.f1.VDCPort1.PortName = CH1VDCPort.Text;
                Form1.f1.VDCPort1.DataBits = 8;
                Form1.f1.VDCPort1.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.VDCPort1.Parity = System.IO.Ports.Parity.None;
                Form1.f1.VDCPort1.Open();
                if (Form1.f1.VDCPort1.IsOpen)
                {
                    CH1VDCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1VDCIsComm.ForeColor = Color.Green;
                    CH1VDCPort.Enabled = false;
                    CH1VDCCon.Enabled = false;
                    CH1VDCBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH1VDCPort", CH1VDCPort.Text);
                    config.IniWriteValue("Port", "CH1VDCBaud", CH1VDCBaud.Text);
                    if (Form1.f1.ADCPort1.IsOpen)
                    {
                        //计算电压和电流是否超过上下限
                        Form1.f1.CH1ReadElec.Interval = 1200;
                        Form1.f1.CH1ReadElec.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH1VDCRefresh_Click(object sender, EventArgs e)
        {
            CH1VDCPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH1VDCPort.Items.AddRange(ports);
        }

        private void CH1VDCBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1ReadElec.Stop();
            Form1.f1.VDCPort1.Close();
            if (!Form1.f1.VDCPort1.IsOpen)
            {
                CH1VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH1VDCIsComm.ForeColor = Color.Red;
                CH1VDCPort.Enabled = true;
                CH1VDCCon.Enabled = true;
                CH1VDCBaud.Enabled = true;
            }
        }

        /// <summary>
        /// 读取工作电流的串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH2ADCCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.CH2ADCPort.BaudRate = int.Parse(CH2ADCBaud.Text);
                Form1.f1.CH2ADCPort.PortName = CH2ADCPort.Text;
                Form1.f1.CH2ADCPort.DataBits = 8;
                Form1.f1.CH2ADCPort.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.CH2ADCPort.Parity = System.IO.Ports.Parity.None;
                Form1.f1.CH2ADCPort.Open();
                if (Form1.f1.CH2ADCPort.IsOpen)
                {
                    CH2ADCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2ADCIsComm.ForeColor = Color.Green;
                    CH2ADCPort.Enabled = false;
                    CH2ADCCon.Enabled = false;
                    CH2ADCBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH2ADCPort", CH2ADCPort.Text);
                    config.IniWriteValue("Port", "CH2ADCBaud", CH2ADCBaud.Text);
                    if (Form1.f1.CH2ADCPort.IsOpen)
                    {
                        byte[] data = Encoding.Default.GetBytes("ADC\r\n");
                        Form1.f1.CH2ADCPort.Write(data, 0, data.Length);
                    }
                    if (Form1.f1.CH2VDCPort.IsOpen)
                    {
                        //计算电压和电流是否超过上下限
                        Form1.f1.CH2ReadElec.Interval = 1200;
                        Form1.f1.CH2ReadElec.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH2ADCRefresh_Click(object sender, EventArgs e)
        {
            CH2ADCPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH2ADCPort.Items.AddRange(ports);
        }

        private void CH2ADCBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CH2ReadElec.Stop();
            Form1.f1.CH2ADCPort.Close();
            if (!Form1.f1.CH2ADCPort.IsOpen)
            {
                CH2ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH2ADCIsComm.ForeColor = Color.Red;
                CH2ADCPort.Enabled = true;
                CH2ADCCon.Enabled = true;
                CH2ADCBaud.Enabled = true;
            }
        }

        /// <summary>
        /// 读取工作电压的串口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH2VDCCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.CH2VDCPort.BaudRate = int.Parse(CH2VDCBaud.Text);
                Form1.f1.CH2VDCPort.PortName = CH2VDCPort.Text;
                Form1.f1.CH2VDCPort.DataBits = 8;
                Form1.f1.CH2VDCPort.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.CH2VDCPort.Parity = System.IO.Ports.Parity.None;
                Form1.f1.CH2VDCPort.Open();
                if (Form1.f1.CH2VDCPort.IsOpen)
                {
                    CH2VDCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2VDCIsComm.ForeColor = Color.Green;
                    CH2VDCPort.Enabled = false;
                    CH2VDCCon.Enabled = false;
                    CH2VDCBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH2VDCPort", CH2VDCPort.Text);
                    config.IniWriteValue("Port", "CH2VDCBaud", CH2VDCBaud.Text);
                    if (Form1.f1.CH2ADCPort.IsOpen)
                    {
                        //计算电压和电流是否超过上下限
                        Form1.f1.CH2ReadElec.Interval = 1200;
                        Form1.f1.CH2ReadElec.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH2VDCRefresh_Click(object sender, EventArgs e)
        {
            CH2VDCPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH2VDCPort.Items.AddRange(ports);
        }

        private void CH2VDCBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CH2ReadElec.Stop();
            Form1.f1.CH2VDCPort.Close();
            if (!Form1.f1.CH2VDCPort.IsOpen)
            {
                CH2VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH2VDCIsComm.ForeColor = Color.Red;
                CH2VDCPort.Enabled = true;
                CH2VDCCon.Enabled = true;
                CH2VDCBaud.Enabled = true;
            }
        }

        private void ReadPort()
        {
            ReadConfig con = new ReadConfig();
            Setup.Port port = con.ReadPort();
            CH1ADCPort.Text = port.CH1ADCPort;
            CH1VDCPort.Text = port.CH1VDCPort;
            CH1ADCBaud.Text = port.CH1ADCBaud;
            CH1VDCBaud.Text = port.CH1VDCBaud;
            CH2ADCPort.Text = port.CH2ADCPort;
            CH2VDCPort.Text = port.CH2VDCPort;
            CH2ADCBaud.Text = port.CH2ADCBaud;
            CH2VDCBaud.Text = port.CH2VDCBaud;
            CodePort.Text = port.CodePort;
            CodeBaud.Text = port.CodeBaud;
            CH2CodePort.Text = port.CH2CodePort;
            CH2CodeBaud.Text = port.CH2CodeBaud;
            CH1FlowPort.Text = port.CH1FlowPort;
            CH1FlowBaud.Text = port.CH1FlowBaud;
            CH2FlowPort.Text = port.CH2FlowPort;
            CH2FlowBaud.Text = port.CH2FlowBaud;
            CH3FlowPort.Text = port.CH3FlowPort;
            CH3FlowBaud.Text = port.CH3FlowBaud;
            CH4FlowPort.Text = port.CH4FlowPort;
            CH4FlowBaud.Text = port.CH4FlowBaud;
            CKCH1Port.Text = port.CKCH1Port;
            CKCH2Port.Text = port.CKCH2Port;
            CKCH1Baud.Text = port.CKCH1Baud;
            CKCH2Baud.Text = port.CKCH2Baud;
        }

        private void CodeCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.CodePort1.BaudRate = int.Parse(CodeBaud.Text);
                Form1.f1.CodePort1.PortName = CodePort.Text;
                Form1.f1.CodePort1.DataBits = 8;
                Form1.f1.CodePort1.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.CodePort1.Parity = System.IO.Ports.Parity.None;
                Form1.f1.CodePort1.Open();
                if (Form1.f1.CodePort1.IsOpen)
                {
                    CodeIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CodeIsComm.ForeColor = Color.Green;
                    CodePort.Enabled = false;
                    CodeCon.Enabled = false;
                    CodeBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CodePort", CodePort.Text);
                    config.IniWriteValue("Port", "CodeBaud", CodeBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CodeRefresh_Click(object sender, EventArgs e)
        {
            CodePort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CodePort.Items.AddRange(ports);
        }

        private void CodeBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CodePort1.Close();
            if (!Form1.f1.CodePort1.IsOpen)
            {
                CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CodeIsComm.ForeColor = Color.Red;
                CodePort.Enabled = true;
                CodeCon.Enabled = true;
                CodeBaud.Enabled = true;
            }
        }

        private void ReadStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                //ReadStatus.Stop();
                if (Form1.f1.ADCPort1.IsOpen)
                {
                    CH1ADCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1ADCIsComm.ForeColor = Color.Green;
                    CH1ADCPort.Enabled = false;
                    CH1ADCCon.Enabled = false;
                    CH1ADCBaud.Enabled = false;
                }
                else
                {
                    CH1ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH1ADCIsComm.ForeColor = Color.Red;
                    CH1ADCPort.Enabled = true;
                    CH1ADCCon.Enabled = true;
                    CH1ADCBaud.Enabled = true;
                }
                if (Form1.f1.VDCPort1.IsOpen)
                {
                    CH1VDCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1VDCIsComm.ForeColor = Color.Green;
                    CH1VDCPort.Enabled = false;
                    CH1VDCCon.Enabled = false;
                    CH1VDCBaud.Enabled = false;
                }
                else
                {
                    CH1VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH1VDCIsComm.ForeColor = Color.Red;
                    CH1VDCPort.Enabled = true;
                    CH1VDCCon.Enabled = true;
                    CH1VDCBaud.Enabled = true;
                }
                if (Form1.f1.CH2ADCPort.IsOpen)
                {
                    CH2ADCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2ADCIsComm.ForeColor = Color.Green;
                    CH2ADCPort.Enabled = false;
                    CH2ADCCon.Enabled = false;
                    CH2ADCBaud.Enabled = false;
                }
                else
                {
                    CH2ADCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH2ADCIsComm.ForeColor = Color.Red;
                    CH2ADCPort.Enabled = true;
                    CH2ADCCon.Enabled = true;
                    CH2ADCBaud.Enabled = true;
                }
                if (Form1.f1.CH2VDCPort.IsOpen)
                {
                    CH2VDCIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2VDCIsComm.ForeColor = Color.Green;
                    CH2VDCPort.Enabled = false;
                    CH2VDCCon.Enabled = false;
                    CH2VDCBaud.Enabled = false;
                }
                else
                {
                    CH2VDCIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH2VDCIsComm.ForeColor = Color.Red;
                    CH2VDCPort.Enabled = true;
                    CH2VDCCon.Enabled = true;
                    CH2VDCBaud.Enabled = true;
                }
                if (Form1.f1.CodePort1.IsOpen)
                {
                    CodeIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CodeIsComm.ForeColor = Color.Green;
                    CodePort.Enabled = false;
                    CodeCon.Enabled = false;
                    CodeBaud.Enabled = false;
                }
                else
                {
                    CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CodeIsComm.ForeColor = Color.Red;
                    CodePort.Enabled = true;
                    CodeCon.Enabled = true;
                    CodeBaud.Enabled = true;
                }
                if (Form1.f1.CodePort2.IsOpen)
                {
                    CH2CodeIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2CodeIsComm.ForeColor = Color.Green;
                    CH2CodePort.Enabled = false;
                    CH2CodeCon.Enabled = false;
                    CH2CodeBaud.Enabled = false;
                }
                else
                {
                    CH2CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH2CodeIsComm.ForeColor = Color.Red;
                    CH2CodePort.Enabled = true;
                    CH2CodeCon.Enabled = true;
                    CH2CodeBaud.Enabled = true;
                }
                //if (Form1.f1.CH1FlowPort.IsOpen)
                if (Form1.f1.busRtuClientCH1.IsOpen())
                {
                    CH1FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1FlowIsComm.ForeColor = Color.Green;
                    CH1FlowPort.Enabled = false;
                    CH1FlowCon.Enabled = false;
                    CH1FlowBaud.Enabled = false;
                }
                else
                {
                    CH1FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                    CH1FlowIsComm.ForeColor = Color.Red;
                    CH1FlowPort.Enabled = true;
                    CH1FlowCon.Enabled = true;
                    CH1FlowBaud.Enabled = true;
                }
                // if (Form1.f1.CH2FlowPort.IsOpen)
                //if (Form1.f1.busRtuClientCH2.IsOpen())
                //{
                //    CH2FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                //    CH2FlowIsComm.ForeColor = Color.Green;
                //    CH2FlowPort.Enabled = false;
                //    CH2FlowCon.Enabled = false;
                //    CH2FlowBaud.Enabled = false;
                //}
                //else
                //{
                //    CH2FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                //    CH2FlowIsComm.ForeColor = Color.Red;
                //    CH2FlowPort.Enabled = true;
                //    CH2FlowCon.Enabled = true;
                //    CH2FlowBaud.Enabled = true;
                //}
                //// if (Form1.f1.CH3FlowPort.IsOpen)
                //if (Form1.f1.busRtuClientCH3.IsOpen())
                //{
                //    CH3FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                //    CH3FlowIsComm.ForeColor = Color.Green;
                //    CH3FlowPort.Enabled = false;
                //    CH3FlowCon.Enabled = false;
                //    CH3FlowBaud.Enabled = false;
                //}
                //else
                //{
                //    CH3FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                //    CH3FlowIsComm.ForeColor = Color.Red;
                //    CH3FlowPort.Enabled = true;
                //    CH3FlowCon.Enabled = true;
                //    CH3FlowBaud.Enabled = true;
                //}
                //// if (Form1.f1.CH4FlowPort.IsOpen)
                //if (Form1.f1.busRtuClientCH4.IsOpen())
                //{
                //    CH4FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                //    CH4FlowIsComm.ForeColor = Color.Green;
                //    CH4FlowPort.Enabled = false;
                //    CH4FlowCon.Enabled = false;
                //    CH4FlowBaud.Enabled = false;
                //}
                //else
                //{
                //    CH4FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                //    CH4FlowIsComm.ForeColor = Color.Red;
                //    CH4FlowPort.Enabled = true;
                //    CH4FlowCon.Enabled = true;
                //    CH4FlowBaud.Enabled = true;
                //}
                if (Form1.CH1POWER._serialPort.IsOpen)  
                {
                    labCKCH1.Text = I18N.GetLangText(dicLang, "已打开");
                    labCKCH1.ForeColor = Color.Green;
                    CKCH1Port.Enabled = false;
                    btnCKCh1Connect.Enabled = false;
                    CKCH1Baud.Enabled = false;
                }
                else
                {
                    labCKCH1.Text = I18N.GetLangText(dicLang, "未打开");
                    labCKCH1.ForeColor = Color.Red;
                    CKCH1Port.Enabled = true;
                    btnCKCh1Connect.Enabled = true;
                    CKCH1Baud.Enabled = true;
                }
                if (Form1.f1.CKCH2Port.IsOpen)
                {
                    labCKCH2.Text = I18N.GetLangText(dicLang, "已打开");
                    labCKCH2.ForeColor = Color.Green;
                    CKCH2Port.Enabled = false;
                    btnCKCh2Connect.Enabled = false;
                    CKCH2Baud.Enabled = false;
                }
                else
                {
                    labCKCH2.Text = I18N.GetLangText(dicLang, "未打开");
                    labCKCH2.ForeColor = Color.Red;
                    CKCH2Port.Enabled = true;
                    btnCKCh2Connect.Enabled = true;
                    CKCH2Baud.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ReadStatus.Stop();
                MessageBox.Show(ex.Message);
            }
        }

        private void CH1FlowCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等

                /*                Form1.f1.CH1FlowPort.BaudRate = int.Parse(CH1FlowBaud.Text);
                                Form1.f1.CH1FlowPort.PortName = CH1FlowPort.Text;
                                Form1.f1.CH1FlowPort.DataBits = 8;
                                Form1.f1.CH1FlowPort.StopBits = System.IO.Ports.StopBits.One;
                                Form1.f1.CH1FlowPort.Parity = System.IO.Ports.Parity.None;
                                Form1.f1.CH1FlowPort.Open();*/

                Form1.f1.busRtuClientCH1?.Close();
                Form1.f1.busRtuClientCH1 = new ModbusRtu(byte.Parse("01"));
                Form1.f1.busRtuClientCH1.SerialPortInni(sp =>
                {
                    sp.PortName = CH1FlowPort.Text;
                    sp.BaudRate = int.Parse(CH1FlowBaud.Text);
                    sp.DataBits = 8;
                    sp.StopBits = System.IO.Ports.StopBits.One;
                    sp.Parity = System.IO.Ports.Parity.None;
                });
                Form1.f1.busRtuClientCH1.Open();
                if (Form1.f1.busRtuClientCH1.IsOpen())
                {
                    CH1FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH1FlowIsComm.ForeColor = Color.Green;
                    CH1FlowPort.Enabled = false;
                    CH1FlowCon.Enabled = false;
                    CH1FlowBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH1FlowPort", CH1FlowPort.Text);
                    config.IniWriteValue("Port", "CH1FlowBaud", CH1FlowBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH1FlowRefresh_Click(object sender, EventArgs e)
        {
            CH1FlowPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH1FlowPort.Items.AddRange(ports);
        }

        private void CH1FlowBreak_Click(object sender, EventArgs e)
        {
            //Form1.f1.CH1IsReadFlow=false;
            Form1.f1.busRtuClientCH1.Close();
            if (!Form1.f1.busRtuClientCH1.IsOpen())
            {
                CH1FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH1FlowIsComm.ForeColor = Color.Red;
                CH1FlowPort.Enabled = true;
                CH1FlowCon.Enabled = true;
                CH1FlowBaud.Enabled = true;
            }
        }

        private void CH2FlowCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                //Form1.f1.CH2FlowPort.BaudRate = int.Parse(CH2FlowBaud.Text);
                //Form1.f1.CH2FlowPort.PortName = CH2FlowPort.Text;
                //Form1.f1.CH2FlowPort.DataBits = 8;
                //Form1.f1.CH2FlowPort.StopBits = System.IO.Ports.StopBits.One;
                //Form1.f1.CH2FlowPort.Parity = System.IO.Ports.Parity.None;
                //Form1.f1.CH2FlowPort.Open();
                //if (Form1.f1.CH2FlowPort.IsOpen)
                Form1.f1.busRtuClientCH2?.Close();
                Form1.f1.busRtuClientCH2 = new ModbusRtu(byte.Parse("01"));
                Form1.f1.busRtuClientCH2.SerialPortInni(sp =>
                {
                    sp.PortName = CH2FlowPort.Text;
                    sp.BaudRate = int.Parse(CH2FlowBaud.Text);
                    sp.DataBits = 8;
                    sp.StopBits = System.IO.Ports.StopBits.One;
                    sp.Parity = System.IO.Ports.Parity.None;
                });
                Form1.f1.busRtuClientCH2.Open();
                if (Form1.f1.busRtuClientCH2.IsOpen())
                {
                    CH2FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2FlowIsComm.ForeColor = Color.Green;
                    CH2FlowPort.Enabled = false;
                    CH2FlowCon.Enabled = false;
                    CH2FlowBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH2FlowPort", CH2FlowPort.Text);
                    config.IniWriteValue("Port", "CH2FlowBaud", CH2FlowBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH2FlowRefresh_Click(object sender, EventArgs e)
        {
            CH2FlowPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH2FlowPort.Items.AddRange(ports);
        }

        private void CH2FlowBreak_Click(object sender, EventArgs e)
        {
            //Form1.f1.CH2IsReadFlow = false;
            Form1.f1.busRtuClientCH2.Close();
            if (!Form1.f1.busRtuClientCH2.IsOpen())
            {
                CH2FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH2FlowIsComm.ForeColor = Color.Red;
                CH2FlowPort.Enabled = true;
                CH2FlowCon.Enabled = true;
                CH2FlowBaud.Enabled = true;
            }
        }

        private void CH3FlowCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                //Form1.f1.CH3FlowPort.BaudRate = int.Parse(CH3FlowBaud.Text);
                //Form1.f1.CH3FlowPort.PortName = CH3FlowPort.Text;
                //Form1.f1.CH3FlowPort.DataBits = 8;
                //Form1.f1.CH3FlowPort.StopBits = System.IO.Ports.StopBits.One;
                //Form1.f1.CH3FlowPort.Parity = System.IO.Ports.Parity.None;
                //Form1.f1.CH3FlowPort.Open();
                //if (Form1.f1.CH3FlowPort.IsOpen)
                Form1.f1.busRtuClientCH3?.Close();
                Form1.f1.busRtuClientCH3 = new ModbusRtu(byte.Parse("01"));
                Form1.f1.busRtuClientCH3.SerialPortInni(sp =>
                {
                    sp.PortName = CH3FlowPort.Text;
                    sp.BaudRate = int.Parse(CH3FlowBaud.Text);
                    sp.DataBits = 8;
                    sp.StopBits = System.IO.Ports.StopBits.One;
                    sp.Parity = System.IO.Ports.Parity.None;
                });
                Form1.f1.busRtuClientCH3.Open();
                if (Form1.f1.busRtuClientCH3.IsOpen())
                {
                    CH3FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH3FlowIsComm.ForeColor = Color.Green;
                    CH3FlowPort.Enabled = false;
                    CH3FlowCon.Enabled = false;
                    CH3FlowBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH3FlowPort", CH3FlowPort.Text);
                    config.IniWriteValue("Port", "CH3FlowBaud", CH3FlowBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH3FlowRefresh_Click(object sender, EventArgs e)
        {
            CH3FlowPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH3FlowPort.Items.AddRange(ports);
        }

        private void CH3FlowBreak_Click(object sender, EventArgs e)
        {
            //Form1.f1.CH3IsReadFlow = false;
            //Form1.f1.CH3FlowPort.Close();
            //if (!Form1.f1.CH3FlowPort.IsOpen)
            Form1.f1.busRtuClientCH3.Close();
            if (!Form1.f1.busRtuClientCH3.IsOpen())
            {
                CH3FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH3FlowIsComm.ForeColor = Color.Red;
                CH3FlowPort.Enabled = true;
                CH3FlowCon.Enabled = true;
                CH3FlowBaud.Enabled = true;
            }
        }

        private void CH4FlowCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                //Form1.f1.CH4FlowPort.BaudRate = int.Parse(CH4FlowBaud.Text);
                //Form1.f1.CH4FlowPort.PortName = CH4FlowPort.Text;
                //Form1.f1.CH4FlowPort.DataBits = 8;
                //Form1.f1.CH4FlowPort.StopBits = System.IO.Ports.StopBits.One;
                //Form1.f1.CH4FlowPort.Parity = System.IO.Ports.Parity.None;
                //Form1.f1.CH4FlowPort.Open();
                //if (Form1.f1.CH4FlowPort.IsOpen)
                Form1.f1.busRtuClientCH4?.Close();
                Form1.f1.busRtuClientCH4 = new ModbusRtu(byte.Parse("1"));
                Form1.f1.busRtuClientCH4.SerialPortInni(sp =>
                {
                    sp.PortName = CH4FlowPort.Text;
                    sp.BaudRate = int.Parse(CH4FlowBaud.Text);
                    sp.DataBits = 8;
                    sp.StopBits = System.IO.Ports.StopBits.One;
                    sp.Parity = System.IO.Ports.Parity.None;
                });
                Form1.f1.busRtuClientCH4.Open();
                if (Form1.f1.busRtuClientCH4.IsOpen())
                {
                    CH4FlowIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH4FlowIsComm.ForeColor = Color.Green;
                    CH4FlowPort.Enabled = false;
                    CH4FlowCon.Enabled = false;
                    CH4FlowBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH4FlowPort", CH4FlowPort.Text);
                    config.IniWriteValue("Port", "CH4FlowBaud", CH4FlowBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH4FlowRefresh_Click(object sender, EventArgs e)
        {
            CH4FlowPort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH4FlowPort.Items.AddRange(ports);
        }

        private void CH4FlowBreak_Click(object sender, EventArgs e)
        {
            //Form1.f1.CH4IsReadFlow = false;
            //Form1.f1.CH4FlowPort.Close();
            //if (!Form1.f1.CH4FlowPort.IsOpen)
            Form1.f1.busRtuClientCH4.Close();
            if (!Form1.f1.busRtuClientCH4.IsOpen())
            {
                CH4FlowIsComm.Text = I18N.GetLangText(dicLang, "未打开");
                CH4FlowIsComm.ForeColor = Color.Red;
                CH4FlowPort.Enabled = true;
                CH4FlowCon.Enabled = true;
                CH4FlowBaud.Enabled = true;
            }
        }

        private void CH2CodeCon_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.CodePort2.BaudRate = int.Parse(CH2CodeBaud.Text);
                Form1.f1.CodePort2.PortName = CH2CodePort.Text;
                Form1.f1.CodePort2.DataBits = 8;
                Form1.f1.CodePort2.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.CodePort2.Parity = System.IO.Ports.Parity.None;
                Form1.f1.CodePort2.Open();
                if (Form1.f1.CodePort2.IsOpen)
                {
                    CH2CodeIsComm.Text = I18N.GetLangText(dicLang, "已打开");
                    CH2CodeIsComm.ForeColor = Color.Green;
                    CH2CodePort.Enabled = false;
                    CH2CodeCon.Enabled = false;
                    CH2CodeBaud.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    //string dialog = Form1.f1.machine;
                    //ConfigINI mesconfig = new ConfigINI("Model", dialog);
                    config.IniWriteValue("Port", "CH2CodePort", CH2CodePort.Text);
                    config.IniWriteValue("Port", "CH2CodeBaud", CH2CodeBaud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CH2CodeRefresh_Click(object sender, EventArgs e)
        {
            CH2CodePort.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CH2CodePort.Items.AddRange(ports);
        }

        private void CH2CodeBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.CodePort2.Close();
            if (!Form1.f1.CodePort2.IsOpen)
            {
                CH2CodeIsComm.Text = I18N.GetLangText(dicLang, "未打开"); ;
                CH2CodeIsComm.ForeColor = Color.Red;
                CH2CodePort.Enabled = true;
                CH2CodeCon.Enabled = true;
                CH2CodeBaud.Enabled = true;
            }
        }

        private void btnCKCh1Connect_Click(object sender, EventArgs e)
        {
            try
            {



                //设置端口的参数，包括波特率等
                Form1.CH1POWER._serialPort.PortName = CKCH1Port.Text;
                Form1.CH1POWER._serialPort.BaudRate = int.Parse(CKCH1Baud.Text);
                Form1.CH1POWER._serialPort.DataBits = 8;
                Form1.CH1POWER._serialPort.StopBits = System.IO.Ports.StopBits.One;
                Form1.CH1POWER._serialPort.Parity = System.IO.Ports.Parity.None;
                //Form1.f1.CH1POWER._serialPort.Open();
          Form1.     CH1POWER.Start();
                if (Form1.CH1POWER._serialPort.IsOpen)
                {
                    Form1.CH1POWER._serialPort.WriteLine("SYST:REM");
                    labCKCH1.Text = I18N.GetLangText(dicLang, "已打开");
                    labCKCH1.ForeColor = Color.Green;
                    btnCKCh1Connect.Enabled = false;
                    CKCH1Baud.Enabled = false;
                    CKCH1Port.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    config.IniWriteValue("Port", "CKCH1Port", CKCH1Port.Text);
                    config.IniWriteValue("Port", "CKCH2Baud", CKCH1Baud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCKCh1Refresh_Click(object sender, EventArgs e)
        {
            CKCH1Port.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CKCH1Port.Items.AddRange(ports);
        }

        private void btnCKCh1Disconnect_Click(object sender, EventArgs e)
        {
            Form1.CH1POWER._serialPort.Close();
            if (!Form1.CH1POWER._serialPort.IsOpen)
            {
                labCKCH1.Text = I18N.GetLangText(dicLang, "未打开");
                labCKCH1.ForeColor = Color.Red;
                btnCKCh1Connect.Enabled = true;
                CKCH1Baud.Enabled = true;
                CKCH1Port.Enabled = true;
            }
        }

        private void btnCKCh2Connect_Click(object sender, EventArgs e)
        {
            try
            {
                //设置端口的参数，包括波特率等
                Form1.f1.CKCH2Port.PortName = CKCH2Port.Text;
                Form1.f1.CKCH2Port.BaudRate = int.Parse(CKCH2Baud.Text);
                Form1.f1.CKCH2Port.DataBits = 8;
                Form1.f1.CKCH2Port.StopBits = System.IO.Ports.StopBits.One;
                Form1.f1.CKCH2Port.Parity = System.IO.Ports.Parity.None;
                Form1.f1.CKCH2Port.Open();
                if (Form1.f1.CKCH2Port.IsOpen)
                {
                    if (Form1.f1.CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("SYST:REM");
                    labCKCH2.Text = I18N.GetLangText(dicLang, "已打开");
                    labCKCH2.ForeColor = Color.Green;
                    btnCKCh2Connect.Enabled = false;
                    CKCH2Baud.Enabled = false;
                    CKCH2Port.Enabled = false;
                    //将端口和波特率进行存储
                    string dialog;
                    dialog = "MultimeterPort.ini";
                    ConfigINI config = new ConfigINI("Port", dialog);
                    config.IniWriteValue("Port", "CKCH2Port", CKCH2Port.Text);
                    config.IniWriteValue("Port", "CKCH2Baud", CKCH2Baud.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCKCh2Refresh_Click(object sender, EventArgs e)
        {
            CKCH2Port.Items.Clear();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            CKCH2Port.Items.AddRange(ports);
        }

        private void btnCKCh2Disconnect_Click(object sender, EventArgs e)
        {
            Form1.f1.CKCH2Port.Close();
            if (!Form1.f1.CKCH2Port.IsOpen)
            {
                labCKCH2.Text = I18N.GetLangText(dicLang, "未打开");
                labCKCH2.ForeColor = Color.Red;
                btnCKCh2Connect.Enabled = true;
                CKCH2Baud.Enabled = true;
                CKCH2Port.Enabled = true;
            }
        }

        private void btnSerialPortLock_Click(object sender, EventArgs e)
        {
            Form1.f1.mSerialPort = !Form1.f1.mSerialPort;
        }
    }
}