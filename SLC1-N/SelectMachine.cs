using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class SelectMachine : Form
    {
        public SelectMachine()
        {
            InitializeComponent();
        }

        private void OpenINI_Click(object sender, EventArgs e)
        {
            OpenMachineINI.CheckFileExists = true;
            OpenMachineINI.Multiselect = false;
            OpenMachineINI.CheckPathExists = true;
            OpenMachineINI.AddExtension = true;
            OpenMachineINI.DefaultExt = ".ini";
            //OpenMachineINI.Filter = "配置文件(*.ini)|*.ini";
            OpenMachineINI.Filter = "File(*.ini)|*.ini";
            OpenMachineINI.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "Config\\Model\\";
            OpenMachineINI.ShowDialog();
            string machinepath = OpenMachineINI.FileName;
            if (!String.IsNullOrEmpty(machinepath) && (machinepath != "openFileDialog1"))
            {
                Form1.f1.machine = OpenMachineINI.SafeFileName;
                Form1.f1.machinepath = machinepath;
                Form1.f1.CH1csvworknum = 1;
                Form1.f1.CH2csvworknum = 1;
                Form1.f1.CH1mesworknum = 1;
                Form1.f1.CH2mesworknum = 1;
                OpenMachineINI.Dispose();
                this.Close();
            }
        }

        private void SelectMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (String.IsNullOrEmpty(Form1.f1.machine))
            {
                System.Environment.Exit(0);
            }
        }
    }
}
