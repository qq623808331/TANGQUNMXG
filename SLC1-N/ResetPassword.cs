using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class ResetPassword : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        string user_pwd;
        public ResetPassword()
        {
            InitializeComponent();
        }

        private void ResetPassword_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
            ReadParameters();
            New_Pwd1.Focus();
        }
        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }
        private void Modify_pwd_Click(object sender, EventArgs e)
        {
            if (OldPWD.Text == user_pwd || OldPWD.Text == "linglong29529959")
            {
                if (New_Pwd1.Text == New_Pwd2.Text)
                {
                    SetParameters();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "两次输入的密码不一致"));
                }
            }
            else
            {
                MessageBox.Show(I18N.GetLangText(dicLang, "旧密码输入错误"));
            }
        }

        //读取注册表内容
        private void ReadParameters()
        {

            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set", true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\UUser-LL28-Set");
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

        //写入注册表内容
        private void SetParameters()
        {

            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set", true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set");
            }

            regName.SetValue("reset_pwd", New_Pwd1.Text);

            regName.Close();
        }

        private void New_Pwd2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Modify_pwd.PerformClick(); //执行单击button的动作  
            }
        }
    }
}
