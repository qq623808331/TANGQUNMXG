using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class ConfigTCP : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        public ConfigTCP()
        {
            InitializeComponent();
        }

        private void ConfigTCP_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;

            LeftCH1TCP.Text = "";
            LeftCH1IP.Text = "";
            LeftCH2TCP.Text = "";
            LeftCH2IP.Text = "";
            RightCH1TCP.Text = "";
            RightCH1IP.Text = "";
            RightCH2TCP.Text = "";
            RightCH2IP.Text = "";
            ReadConn.Interval = 800;
            ReadConn.Start();
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        private void ReadConn_Tick(object sender, EventArgs e)
        {
            LeftCH1TCP.Text = Form1.f1.LeftCH1TCP.Text;
            if (LeftCH1TCP.Text == "OK")
            {
                LeftCH1TCP.ForeColor = Color.Green;
            }
            else
            {
                LeftCH1TCP.ForeColor = Color.Red;
            }
            LeftCH1IP.Text = Form1.f1.ch1ipaddress;

            LeftCH2TCP.Text = Form1.f1.LeftCH2TCP.Text;
            if (LeftCH2TCP.Text == "OK")
            {
                LeftCH2TCP.ForeColor = Color.Green;
            }
            else
            {
                LeftCH2TCP.ForeColor = Color.Red;
            }
            LeftCH2IP.Text = Form1.f1.ch2ipaddress;

            RightCH1TCP.Text = Form1.f1.RightCH1TCP.Text;
            if (RightCH1TCP.Text == "OK")
            {
                RightCH1TCP.ForeColor = Color.Green;
            }
            else
            {
                RightCH1TCP.ForeColor = Color.Red;
            }
            RightCH1IP.Text = Form1.f1.ch3ipaddress;

            RightCH2TCP.Text = Form1.f1.RightCH2TCP.Text;
            if (RightCH2TCP.Text == "OK")
            {
                RightCH2TCP.ForeColor = Color.Green;
            }
            else
            {
                RightCH2TCP.ForeColor = Color.Red;
            }
            RightCH2IP.Text = Form1.f1.ch4ipaddress;
            ComputerIP.Text = Form1.f1.localipaddress;
        }

        private void CH1TCPReCon_Click(object sender, EventArgs e)
        {
            //if (Form1.f1.ch1client.IsConnect)
            //{
            //    Logger.Log(I18N.GetLangText(dicLang, "仪器已连接"));
            //    MessageBox.Show(I18N.GetLangText(dicLang, "仪器已连接"));
            //}
            //else
            //{
            //    Form1.f1.ch1client.btnConnect("2000", LeftCH1IP.Text, "9999");
            //}
            Form1.f1.ch1client.btnConnect("2000", LeftCH1IP.Text, "9999");
        }

        private void CH1TCPBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.TCPClose(1);
        }

        private void CH2TCPReCon_Click(object sender, EventArgs e)
        {
            //if (Form1.f1.ch2client.IsConnect)
            //{
            //    Logger.Log(I18N.GetLangText(dicLang, "仪器已连接"));
            //    MessageBox.Show(I18N.GetLangText(dicLang, "仪器已连接"));
            //}
            //else
            //{
            //    Form1.f1.ch2client.btnConnect("2000", LeftCH2IP.Text, "9999");
            //}
            Form1.f1.ch2client.btnConnect("2000", LeftCH2IP.Text, "9999");
        }

        private void CH2TCPBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.TCPClose(2);
        }

        private void CH3TCPReCon_Click(object sender, EventArgs e)
        {
            //Form1.f1.UDPBroadcast();
            //    if (Form1.f1.ch3client.IsConnect)
            //    {
            //        Logger.Log(I18N.GetLangText(dicLang, "仪器已连接"));
            //        MessageBox.Show(I18N.GetLangText(dicLang, "仪器已连接"));
            //    }
            //    else
            //    {
            //        Form1.f1.ch3client.btnConnect("2000", RightCH1IP.Text, "9999");
            //    }
            Form1.f1.ch3client.btnConnect("2000", RightCH1IP.Text, "9999");
        }

        private void CH3TCPBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.TCPClose(3);
        }

        private void CH4TCPReCon_Click(object sender, EventArgs e)
        {
            //if (Form1.f1.ch4client.IsConnect)
            //{
            //    Logger.Log(I18N.GetLangText(dicLang, "仪器已连接"));
            //    MessageBox.Show(I18N.GetLangText(dicLang, "仪器已连接"));
            //}
            //else
            //{
            //    Form1.f1.ch4client.btnConnect("2000", RightCH2IP.Text, "9999");
            //}
            //Form1.f1.UDPBroadcast();
            Form1.f1.ch4client.btnConnect("2000", RightCH2IP.Text, "9999");
        }

        private void CH4TCPBreak_Click(object sender, EventArgs e)
        {
            Form1.f1.TCPClose(4);
        }

        private void UDPReadIP_Click(object sender, EventArgs e)
        {
            Form1.f1.UDPBroadcast();
            Form1.f1.SocketConn = false;
        }
    }
}