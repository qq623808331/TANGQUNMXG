using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class ConfigLogOn : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        private string user_pwd;
        private int CH;

        public ConfigLogOn(int ch)
        {
            InitializeComponent();
            CH = ch;
        }

        private void ConfigLogOn_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
            ReadParameters();
            Password.Focus();
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        private void Log_Click(object sender, EventArgs e)
        {
            if (Password.Text == "linglong29529959" || Password.Text == user_pwd)
            {
                if (CH == 1)
                {
                    Form1.f1.plc.CH1MachineReset();
                }
                if (CH == 2)
                {
                    Form1.f1.plc.CH2MachineReset();
                }
                this.Close();
            }
            else
            {
                Logger.Log(I18N.GetLangText(dicLang, "密码错误，请检查密码"));
                MessageBox.Show(I18N.GetLangText(dicLang, "密码错误，请检查密码"));
                Password.ResetText();
                Password.Focus();
            }
        }

        //读取注册表内容
        private void ReadParameters()
        {
            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set", true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set");
            }

            regName.OpenSubKey("User");
            if (regName.GetValue("reset_pwd") is null)
            {
                user_pwd = "admin12345";
            }
            else
            {
                user_pwd = regName.GetValue("reset_pwd").ToString();
                regName.Close();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Log.PerformClick(); //执行单击button的动作
            }
        }
    }
}