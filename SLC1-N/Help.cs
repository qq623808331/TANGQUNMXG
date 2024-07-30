using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Hide();
            LinkLabel2.Hide();
            function5.Hide();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual1_Click(object sender, EventArgs e)
        {
            function1.Show();
            function2.Hide();
            function3.Hide();
            function5.Hide();
            Label3.Hide();
            LinkLabel2.Hide();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual2_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Show();
            function3.Hide();
            function5.Hide();
            Label3.Hide();
            LinkLabel2.Hide();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual3_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Show();
            function5.Hide();
            Label3.Hide();
            LinkLabel2.Hide();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual4_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Hide();
            function5.Hide();
            Label3.Show();
            LinkLabel2.Hide();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual5_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Hide();
            Label3.Hide();
            function5.Show();
            LinkLabel2.Show();
            Function6.Hide();
            Function7.Hide();
        }

        private void BtnManual6_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Hide();
            function5.Hide();
            Label3.Hide();
            LinkLabel2.Hide();
            Function6.Show();
            Function7.Hide();
        }

        private void BtnManual7_Click(object sender, EventArgs e)
        {
            function1.Hide();
            function2.Hide();
            function3.Hide();
            function5.Hide();
            Label3.Hide();
            LinkLabel2.Hide();
            Function6.Hide();
            Function7.Show();
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SQLConnectionMethod sql = new SQLConnectionMethod();
            OpenForm(sql);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.shzhll.com/");
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

    }
}
