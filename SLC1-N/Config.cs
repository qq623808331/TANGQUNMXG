using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Config : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
            ReadCodeCount();
            if (Form1.f1.plc.PLCIsRun)
            {
                //ReadCount.Interval = 500;
                //ReadCount.Start();
                ReadCount();
            }
            else
            {
                LeftCodeCount.Text = "0";
                RightCodeCount.Text = "0";
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            Read();
            ReadWorkOrder();
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        private void Use_Set_Click(object sender, EventArgs e)
        {
            try
            {
                //Form1.f1.left_codelife = Convert.ToInt32(LeftCodeLife.Text);
                //Form1.f1.right_codelife = Convert.ToInt32(RightCodeLife.Text);
                Form1.f1.codesetting.LeftCodeLength = LeftCodeLength.Text;
                Form1.f1.codesetting.RightCodeLength = RightCodeLength.Text;
                Form1.f1.codesetting.CHKCH1 = CHKCH1.Checked;
                Form1.f1.codesetting.CHKCH2 = CHKCH2.Checked;
                Set();
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
            }
        }

        private void CodeSetting_Click(object sender, EventArgs e)
        {
            int ch1codecount = Convert.ToInt32(LeftCodeLife.Text);
            if (String.IsNullOrEmpty(LeftCodeLife.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "可用产品个数需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "可用产品个数需填写"));
            }
            else
            {
                string dialog = Form1.f1.machine;
                ConfigINI mesconfig = new ConfigINI("Model", dialog);
                mesconfig.IniWriteValue("CodeLife", "CH1CodeLife", LeftCodeLife.Text);
                Form1.f1.codesetting.LeftCodeLife = Convert.ToInt32(LeftCodeLife.Text);
                Form1.f1.plc.ch1codelife = ch1codecount;
                Form1.f1.plc.CH1CodeLife();
            }
        }

        private void CH2CodeSetting_Click(object sender, EventArgs e)
        {
            int ch2codecount = Convert.ToInt32(RightCodeLife.Text);
            if (String.IsNullOrEmpty(RightCodeLife.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "可用产品个数需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "可用产品个数需填写"));
            }
            else
            {
                //string dialog;
                //dialog = "CodeCount.ini";
                //ConfigINI mesconfig = new ConfigINI(Form1.f1.machine, dialog);
                string dialog;
                dialog = Form1.f1.machine;
                ConfigINI mesconfig = new ConfigINI("Model", dialog);
                mesconfig.IniWriteValue("CodeLife", "CH2CodeLife", RightCodeLife.Text);
                Form1.f1.codesetting.RightCodeLife = Convert.ToInt32(RightCodeLife.Text);
                Form1.f1.plc.ch2codelife = ch2codecount;
                Form1.f1.plc.CH2CodeLife();
                Form1.f1.plc.CH2CodePass();
            }
        }

        /// <summary>
        /// 读取条码使用次数和规定次数
        /// </summary>
        private void ReadCodeCount()
        {
            ReadConfig con = new ReadConfig();
            Setup.Code_Setting set = con.ReadCode();
            LeftCodeLife.Text = set.LeftCodeLife.ToString();
            RightCodeLife.Text = set.RightCodeLife.ToString();
        }

        //写入参数
        private void Set()
        {
            string dialog;
            dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Code", "left_codelength", LeftCodeLength.Text);
            mesconfig.IniWriteValue("Code", "right_codelength", RightCodeLength.Text);
            mesconfig.IniWriteValue("Code", "chkleft", CHKCH1.Checked.ToString());
            mesconfig.IniWriteValue("Code", "chkright", CHKCH2.Checked.ToString());
        }

        //读出站号、条码长度、通道数等参数
        private void Read()
        {
            ReadConfig con = new ReadConfig();
            Setup.Code_Setting set = con.ReadCode();
            if (String.IsNullOrEmpty(set.LeftCodeLength))
            {
                LeftCodeLength.Text = "13";
            }
            else
            {
                LeftCodeLength.Text = set.LeftCodeLength;
            }
            if (String.IsNullOrEmpty(set.RightCodeLength))
            {
                RightCodeLength.Text = "13";
            }
            else
            {
                RightCodeLength.Text = set.RightCodeLength;
            }
            CHKCH1.Checked = set.CHKCH1;
            CHKCH2.Checked = set.CHKCH2;
        }

        //读取PLC中已测试的次数
        private void ReadCount()
        {
            if (Form1.f1.plc.CH1CodeCount > 0)
            {
                int leftlife = Convert.ToInt32(LeftCodeLife.Text);
                LeftCodeCount.Text = (leftlife - Form1.f1.plc.CH1CodeCount).ToString();
            }
            if (Form1.f1.plc.CH2CodeCount > 0)
            {
                int rightlife = Convert.ToInt32(RightCodeLife.Text);
                RightCodeCount.Text = (rightlife - Form1.f1.plc.CH2CodeCount).ToString();
            }
        }

        private void WorkOrderStore_Click(object sender, EventArgs e)
        {
            //string dialog = "WorkOrder.ini";
            //ConfigINI mesconfig = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("WorkOrder", "ProductName", ProductName.Text);
            mesconfig.IniWriteValue("WorkOrder", "ProductModel", ProductModel.Text);
            mesconfig.IniWriteValue("WorkOrder", "WorkOrder", WorkOrder.Text);
            mesconfig.IniWriteValue("WorkOrder", "ProductionItem", ProductionItem.Text);
            mesconfig.IniWriteValue("WorkOrder", "TestType", TestType.Text);
            mesconfig.IniWriteValue("WorkOrder", "TestStation", TestStation.Text);
            mesconfig.IniWriteValue("WorkOrder", "ProductNum", ProductNum.Text);
            Form1.f1.ProductName.Text = ProductName.Text;
            Form1.f1.ProductModel.Text = ProductModel.Text;
            Form1.f1.WorkOrder.Text = WorkOrder.Text;
            Form1.f1.ProductionItem.Text = ProductionItem.Text;
            Form1.f1.TestType.Text = TestType.Text;
            Form1.f1.TestStation.Text = TestStation.Text;
            Form1.f1.ProductNum.Text = ProductNum.Text;
            this.Close();
        }

        /// <summary>
        /// 读取工单数据
        /// </summary>
        private void ReadWorkOrder()
        {
            ReadConfig con = new ReadConfig();
            Setup.Work_Order order = con.ReadWorkOrder();
            ProductName.Text = order.ProductName;
            ProductModel.Text = order.ProductModel;
            WorkOrder.Text = order.WorkOrder;
            ProductionItem.Text = order.ProductionItem;
            TestType.Text = order.TestType;
            TestStation.Text = order.TestStation;
            ProductNum.Text = order.ProductNum;
        }

        private void CH1ProductCount_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1Product = 0;
            Form1.f1.CH1PassNum = 0;
            Form1.f1.CH1FailNum = 0;
            Form1.f1.WriteCH1ProductCount();
            Form1.f1.CH1ProductNumber.Text = "0";
            Form1.f1.CH1PassNumber.Text = "0";
            Form1.f1.CH1FailNumber.Text = "0";
            Form1.f1.CH1PassRate.Text = "0";
            Form1.f1.CH1CT.Text = "0";
        }

        private void CH2ProductCount_Click(object sender, EventArgs e)
        {
            Form1.f1.CH2Product = 0;
            Form1.f1.CH2PassNum = 0;
            Form1.f1.CH2FailNum = 0;
            Form1.f1.WriteCH2ProductCount();
            Form1.f1.CH2ProductNumber.Text = "0";
            Form1.f1.CH2PassNumber.Text = "0";
            Form1.f1.CH2FailNumber.Text = "0";
            Form1.f1.CH2PassRate.Text = "0";
            Form1.f1.CH2CT.Text = "0";
        }
    }
}