using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();
        }

        private void Save_Load(object sender, EventArgs e)
        {
            Read();
        }

        private void BtnPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog1.ShowDialog();
            path.Text = FolderBrowserDialog1.SelectedPath;
        }

        private void BtnConMES_Click(object sender, EventArgs e)
        {
            MESConfig mes = new MESConfig();
            OpenForm(mes);
        }

        private void Use_Set_Click(object sender, EventArgs e)
        {
            Form1.f1.save.Path = path.Text;
            Form1.f1.save.ChkExcel = ChkExcel.Checked;
            Form1.f1.save.ChkMES = ChkMES.Checked;
            Form1.f1.save.ChkCSV = ChkCSV.Checked;
            Set();
            this.Close();
            //}
        }

        //写入
        private void Set()
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Save", "excel", ChkExcel.Checked.ToString());
            mesconfig.IniWriteValue("Save", "mes", ChkMES.Checked.ToString());
            mesconfig.IniWriteValue("Save", "csv", ChkCSV.Checked.ToString());
            mesconfig.IniWriteValue("Save", "path", path.Text);
        }

        /// <summary>
        /// 读出配置
        /// </summary>
        private void Read()
        {
            ReadConfig con = new ReadConfig();
            Setup.Save save = con.ReadSave();
            ChkExcel.Checked = save.ChkExcel;
            ChkMES.Checked = save.ChkMES;
            ChkCSV.Checked = save.ChkCSV;
            path.Text = save.Path;
        }

        public void OpenForm(System.Windows.Forms.Form frm)
        {
            if (frm == null) return;
            foreach (System.Windows.Forms.Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f.Name == frm.Name)
                {
                    f.Activate();
                    f.Show();
                    frm.Dispose();
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    return;
                }
            }
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frm.Show();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

        private void Warning_Click(object sender, EventArgs e)
        {
            Warning wa = new Warning();
            OpenForm(wa);
        }
    }
}