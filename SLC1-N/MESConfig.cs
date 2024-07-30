using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class MESConfig : Form
    {
        public MESConfig()
        {
            InitializeComponent();
        }

        private void MESConfig_Load(object sender, EventArgs e)
        {
            Read();
        }

        private void Config_Click(object sender, EventArgs e)
        {
            Form1.f1.mesfilename = FileName.Text;
            Form1.f1.mesfolderpath = FolderPath.Text;

            Set();
            this.Close();
        }

        //写入端口参数
        private void Set()
        {
            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine, true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine);
            }

            regName.SetValue("mes_folderpath", FolderPath.Text);
            regName.SetValue("mes_filename", FileName.Text);

            regName.Close();
        }

        //读出站号、条码长度、通道数等参数
        private void Read()
        {
            RegistryKey regName;

            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine, true);

            if (regName is null)
            {
                regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine);
            }

            regName.OpenSubKey("User");
            if (regName.GetValue("mes_folderpath") != null)
            {
                FolderPath.Text = regName.GetValue("mes_folderpath").ToString();
            }
            if (regName.GetValue("mes_filename") != null)
            {
                FileName.Text = regName.GetValue("mes_filename").ToString();
            }
            regName.Close();
        }

        private void BtnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog1.ShowDialog();
            FolderPath.Text = FolderBrowserDialog1.SelectedPath;
        }
    }
}