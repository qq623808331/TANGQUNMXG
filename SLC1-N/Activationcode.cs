using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Activationcode : Form
    {
        public Activationcode()
        {
            InitializeComponent();
        }

        Form1 f1 = new Form1();

        string CODE;//永久激活码
        public string flag;
        public bool sign;//七天标志位码只能用一次
        public bool permanentSign;//永久标志位码只能用一次
        public bool TdaytSign;//三天标志位码只能用一次
        string TdayCODE;//三天激活码
        string SdayCODE;//七天激活码
        private void button1_Click()
        {
            if (sign)
            {
                if (textBox1.Text == "linglongkeji")
                {
                    RegistryKey regName;
                    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-", true);

                    if (regName is null)
                    {
                        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-");
                    }

                    regName.SetValue("使用天数", 336);//七天使用天数
                    sign = false;
                    regName.SetValue("标志位码1", sign);

                    Form1.TIME = 0;
                    regName.SetValue("time", Form1.TIME);
                    regName.Close();

                    MessageBox.Show("试用天数七天,请重新打开程序");
                    this.Close();
                }
            }

            if (permanentSign)
            {
                string asd = Encrypt(CODE);//算出激活码
                if (textBox1.Text == asd.ToString())
                {
                    RegistryKey regName;

                    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-", true);

                    if (regName is null)
                    {
                        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-");
                    }

                    regName.SetValue("使用天数", 999999999);
                    permanentSign = false;
                    Form1.TIME = 0;
                    regName.SetValue("time", Form1.TIME);
                    regName.SetValue("标志位码2", permanentSign);
                    regName.Close();



                    MessageBox.Show("永久激活,请重新打开程序");
                    this.Close();
                }
            }

            string dcx = Encrypt2(SdayCODE);
            if (textBox1.Text == dcx.ToString())
            {
                RegistryKey regName;

                regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-", true);

                if (regName is null)
                {
                    regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-");
                }
                Form1.TIME = 0;
                regName.SetValue("使用天数", 336);
                regName.SetValue("time", Form1.TIME);
                regName.Close();
                MessageBox.Show("试用天数七天,请重新打开程序");
                this.Close();




            }
            if (TdaytSign)
            {
                string rrt = Encrypt3(TdayCODE);
                if (textBox1.Text == rrt.ToString())
                {
                    RegistryKey regName;

                    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-", true);

                    if (regName is null)
                    {
                        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-");
                    }
                    TdaytSign = false;
                    Form1.TIME = 0;
                    regName.SetValue("使用天数", 144);
                    regName.SetValue("time", Form1.TIME);
                    regName.SetValue("标志位码3", TdaytSign);
                    regName.Close();


                    MessageBox.Show("试用天数三天,请重新打开程序");
                    this.Close();
                }
            }



        }

        private void Activationcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Activationcode_Load(object sender, EventArgs e)
        {
            GetMacAddress();
            GetMacAddress2();
            GetMacAddress3();
            Read();
        }
        public void Read()
        {
            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-", true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-");
            }

            regName.OpenSubKey("User");
            if (regName.GetValue("标志位码1") is null)
            {
                sign = true;
            }
            else
            {
                sign = Convert.ToBoolean(regName.GetValue("标志位码1").ToString());
            }
            if (regName.GetValue("标志位码2") is null)
            {
                permanentSign = true;
            }
            else
            {
                permanentSign = Convert.ToBoolean(regName.GetValue("标志位码2").ToString());
            }
            if (regName.GetValue("标志位码3") is null)
            {
                TdaytSign = true;
            }
            else
            {
                TdaytSign = Convert.ToBoolean(regName.GetValue("标志位码3").ToString());
            }

        }
        public string GetMacAddress()
        {

            DateTime currentDate = DateTime.Now;
            int year = currentDate.Year;
            int month = currentDate.Month;
            int day = currentDate.Day;
            string timeout = year.ToString() + month.ToString() + day.ToString();
            CODE = timeout;
            return CODE;

        }
        private string Encrypt(string CODE)
        {
            StringBuilder sb = new StringBuilder();
            byte[] data = Encoding.UTF8.GetBytes(CODE);
            byte[] hash = MD5.Create().ComputeHash(data);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
        private string Encrypt2(string CODE)
        {
            StringBuilder sb = new StringBuilder();
            byte[] data = Encoding.UTF8.GetBytes(CODE);
            byte[] hash = MD5.Create().ComputeHash(data);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
        private string Encrypt3(string CODE)
        {
            StringBuilder sb = new StringBuilder();
            byte[] data = Encoding.UTF8.GetBytes(CODE);
            byte[] hash = MD5.Create().ComputeHash(data);
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
        public string GetMacAddress2()
        {
            DateTime currentDate = DateTime.Now;
            int year = currentDate.Year;
            int month = currentDate.Month;
            int day = currentDate.Day;
            string timeout = year.ToString() + month.ToString() + day.ToString();
            SdayCODE = timeout + 7 + "linglongkeji";
            return SdayCODE;
        }
        public string GetMacAddress3()
        {
            DateTime currentDate = DateTime.Now;
            int year = currentDate.Year;
            int month = currentDate.Month;
            int day = currentDate.Day;
            string timeout = year.ToString() + month.ToString() + day.ToString();
            TdayCODE = timeout + 3 + "linglongkeji";
            return TdayCODE;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click();
        }
    }
}
