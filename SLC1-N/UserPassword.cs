using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class UserPassword : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        public UserPassword()
        {
            InitializeComponent();
        }

        private void UserPassword_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
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
    }
}