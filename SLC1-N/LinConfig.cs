using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class LinConfig : Form
    {
        public LinConfig()
        {
            InitializeComponent();
        }

        private void LinConfig_Load(object sender, EventArgs e)
        {
            ReadLinConfig();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            OpenLDF.CheckFileExists = true;
            OpenLDF.Multiselect = false;
            OpenLDF.CheckPathExists = true;
            OpenLDF.AddExtension = true;
            OpenLDF.DefaultExt = ".ldf";
            //OpenLDF.Filter = "LDF文件(*.ldf)|*.ldf";
            OpenLDF.Filter = "LDF File(*.ldf)|*.ldf";
            OpenLDF.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            OpenLDF.ShowDialog();
            string machinepath = OpenLDF.FileName;
            if (!String.IsNullOrEmpty(machinepath) && (machinepath != "openFileDialog1"))
            {
                LDFFileName.Text = OpenLDF.SafeFileName;
                //this.Close();
            }
        }

        private void LinStore_Click(object sender, EventArgs e)
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("LinConfig", "LDFFileName", LDFFileName.Text);

            mesconfig.IniWriteValue("LinConfig", "UPSignalName", UPSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "DOWNSignalName", DOWNSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "FWDSignalName", FWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "RWDSignalName", RWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "Schedule_tables", Schedule_tables.Text);
            mesconfig.IniWriteValue("LinConfig", "PowerSignalName", PowerSignalName.Text);

            //AD
            mesconfig.IniWriteValue("LinConfig", "ADUPSignalName", ADUPSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "ADDOWNSignalName", ADDOWNSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "ADFWDSignalName", ADFWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "ADRWDSignalName", ADRWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "ADSchedule_tables", ADSchedule_tables.Text);
            mesconfig.IniWriteValue("LinConfig", "ADPowerSignalName", ADPowerSignalName.Text);
            //BE
            mesconfig.IniWriteValue("LinConfig", "BEUPSignalName", BEUPSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "BEDOWNSignalName", BEDOWNSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "BEFWDSignalName", BEFWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "BERWDSignalName", BERWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "BESchedule_tables", BESchedule_tables.Text);
            mesconfig.IniWriteValue("LinConfig", "BEPowerSignalName", BEPowerSignalName.Text);
            //CF
            mesconfig.IniWriteValue("LinConfig", "CFUPSignalName", CFUPSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "CFDOWNSignalName", CFDOWNSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "CFFWDSignalName", CFFWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "CFRWDSignalName", CFRWDSignalName.Text);
            mesconfig.IniWriteValue("LinConfig", "CFSchedule_tables", CFSchedule_tables.Text);
            mesconfig.IniWriteValue("LinConfig", "CFPowerSignalName", CFPowerSignalName.Text);

            if (String.IsNullOrEmpty(PowerSignalValue.Text))
            {
                PowerSignalValue.Text = "0";
            }
            if (String.IsNullOrEmpty(ADPowerSignalValue.Text))
            {
                ADPowerSignalValue.Text = "0";
            }

            if (String.IsNullOrEmpty(BEPowerSignalValue.Text))
            {
                BEPowerSignalValue.Text = "0";
            }
            if (String.IsNullOrEmpty(CFPowerSignalValue.Text))
            {
                CFPowerSignalValue.Text = "0";
            }
            mesconfig.IniWriteValue("LinConfig", "PowerSignalValue", PowerSignalValue.Text);
            mesconfig.IniWriteValue("LinConfig", "ADPowerSignalValue", ADPowerSignalValue.Text);
            mesconfig.IniWriteValue("LinConfig", "BEPowerSignalValue", BEPowerSignalValue.Text);
            mesconfig.IniWriteValue("LinConfig", "CFPowerSignalValue", CFPowerSignalValue.Text);


            Form1.f1.linconfig.LDFFileName = LDFFileName.Text;

            Form1.f1.linconfig.UPSignalName = UPSignalName.Text;
            Form1.f1.linconfig.DOWNSignalName = DOWNSignalName.Text;
            Form1.f1.linconfig.FWDSignalName = FWDSignalName.Text;
            Form1.f1.linconfig.RWDSignalName = RWDSignalName.Text;
            Form1.f1.linconfig.Schedule_tables = Schedule_tables.Text;
            Form1.f1.linconfig.PowerSignalName = PowerSignalName.Text;
            Form1.f1.linconfig.PowerSignalValue = Convert.ToDouble(PowerSignalValue.Text);
            //AD
            Form1.f1.linconfig.ADUPSignalName = ADUPSignalName.Text;
            Form1.f1.linconfig.ADDOWNSignalName = ADDOWNSignalName.Text;
            Form1.f1.linconfig.ADFWDSignalName = ADFWDSignalName.Text;
            Form1.f1.linconfig.ADRWDSignalName = ADRWDSignalName.Text;
            Form1.f1.linconfig.ADSchedule_tables = ADSchedule_tables.Text;
            Form1.f1.linconfig.ADPowerSignalName = ADPowerSignalName.Text;
            Form1.f1.linconfig.ADPowerSignalValue = Convert.ToDouble(ADPowerSignalValue.Text);
            //BE
            Form1.f1.linconfig.BEUPSignalName = BEUPSignalName.Text;
            Form1.f1.linconfig.BEDOWNSignalName = BEDOWNSignalName.Text;
            Form1.f1.linconfig.BEFWDSignalName = BEFWDSignalName.Text;
            Form1.f1.linconfig.BERWDSignalName = BERWDSignalName.Text;
            Form1.f1.linconfig.BESchedule_tables = BESchedule_tables.Text;
            Form1.f1.linconfig.BEPowerSignalName = BEPowerSignalName.Text;
            Form1.f1.linconfig.BEPowerSignalValue = Convert.ToDouble(BEPowerSignalValue.Text);
            //CF
            Form1.f1.linconfig.CFUPSignalName =CFUPSignalName.Text;
            Form1.f1.linconfig.CFDOWNSignalName = CFDOWNSignalName.Text;
            Form1.f1.linconfig.CFFWDSignalName = CFFWDSignalName.Text;
            Form1.f1.linconfig.CFRWDSignalName = CFRWDSignalName.Text;
            Form1.f1.linconfig.CFSchedule_tables = CFSchedule_tables.Text;
            Form1.f1.linconfig.CFPowerSignalName = CFPowerSignalName.Text;
            Form1.f1.linconfig.CFPowerSignalValue = Convert.ToDouble(CFPowerSignalValue.Text);

            this.Close();
        }
        /// <summary>
        /// 读取Lin配置
        /// </summary>
        private void ReadLinConfig()
        {
            ReadConfig con = new ReadConfig();
            Setup.LinConfig lin = con.ReadLinConfig();


            LDFFileName.Text = lin.LDFFileName;
            UPSignalName.Text = lin.UPSignalName;
            DOWNSignalName.Text = lin.DOWNSignalName;
            FWDSignalName.Text = lin.FWDSignalName;
            RWDSignalName.Text = lin.RWDSignalName;
            Schedule_tables.Text = lin.Schedule_tables;
            PowerSignalName.Text = lin.PowerSignalName;
            PowerSignalValue.Text = lin.PowerSignalValue.ToString();

            //AD
            ADUPSignalName.Text = lin.ADUPSignalName;
            ADDOWNSignalName.Text = lin.ADDOWNSignalName;
            ADFWDSignalName.Text = lin.ADFWDSignalName;
            ADRWDSignalName.Text = lin.ADRWDSignalName;
            ADSchedule_tables.Text = lin.ADSchedule_tables;
            ADPowerSignalName.Text = lin.ADPowerSignalName;
            ADPowerSignalValue.Text = lin.ADPowerSignalValue.ToString();
            //BE
            BEUPSignalName.Text = lin.BEUPSignalName;
            BEDOWNSignalName.Text = lin.BEDOWNSignalName;
            BEFWDSignalName.Text = lin.BEFWDSignalName;
            BERWDSignalName.Text = lin.BERWDSignalName;
            BESchedule_tables.Text = lin.BESchedule_tables;
            BEPowerSignalName.Text = lin.BEPowerSignalName;
            BEPowerSignalValue.Text = lin.BEPowerSignalValue.ToString();
            //CF
          
            CFUPSignalName.Text = lin.CFUPSignalName;
            CFDOWNSignalName.Text = lin.CFDOWNSignalName;
            CFFWDSignalName.Text = lin.CFFWDSignalName;
            CFRWDSignalName.Text = lin.CFRWDSignalName;
            CFSchedule_tables.Text = lin.CFSchedule_tables;
            CFPowerSignalName.Text = lin.CFPowerSignalName;
            CFPowerSignalValue.Text = lin.CFPowerSignalValue.ToString();
        }
    }
}
