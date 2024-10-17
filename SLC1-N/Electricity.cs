using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static SLC1_N.Model;

namespace SLC1_N
{
    public partial class Electricity : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        private LIN CH1lin;
        private LIN CH2lin;
        private List<string> CH1Order = new List<string>();
        private List<string> CH2Order = new List<string>();

        private List<string> ADOrder = new List<string>();
        private List<string> BEOrder = new List<string>();
        private List<string> CFOrder = new List<string>();


        public static Setup.Order ord = new Setup.Order();

        public Electricity()
        {
            InitializeComponent();
        }

        private void Electricity_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
            ReadElectricity();
            ReadLin();
            ReadPLCConfig();
            MachineNum.SelectedIndex = 0;
            ReadParametersNumber(1);
            CH1Order = Form1.f1.CH1Order;
            CH2Order = Form1.f1.CH2Order;
            ADOrder = Form1.f1.ADOrder;
            BEOrder = Form1.f1.BEOrder;
            CFOrder = Form1.f1.CFOrder;

            ReadBc();
            //ReadParameters(MachineNum.SelectedIndex + 1, ParaNum.SelectedIndex + 1);
            if (Form1.f1.plc.PLCIsRun)
            {
                ReadSignal.Interval = 200;
                ReadSignal.Start();
            }
            else
            {
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        //CH1调压阀读写
        private void CH1ValveRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1Readvalve();
            System.Threading.Thread.Sleep(100);
            CH1Pressure.Text = Form1.f1.plc.ch1pre.ToString();
        }

        private void CH1ValveWrite_Click(object sender, EventArgs e)
        {
            //short ch1pressure = Convert.ToInt16(CH1Pressure.Text);
            short ch1pressure;
            if (ChkPLCPress.Checked)
            {
                ch1pressure = Convert.ToInt16(Convert.ToDouble(CH1Pressure.Text) * 98);
            }
            else
            {
                ch1pressure = Convert.ToInt16(CH1Pressure.Text);
            }
            if (String.IsNullOrEmpty(CH1Pressure.Text.Trim()))
            {
                //MessageBox.Show("调压阀压力数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
            }
            else
            {
                if (ch1pressure > 100)
                {
                    //MessageBox.Show("调压阀压力数值不可以超过100！");
                    Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                }
                else
                {
                    Form1.f1.plc.ch1pre = ch1pressure;
                    Form1.f1.plc.CH1Writevalve();
                    WritePLCConfig();
                }
            }
            SaveFlowRunLog();
        }

        //CH2调压阀读写
        private void CH2ValveRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2Readvalve();
            System.Threading.Thread.Sleep(100);
            CH2Pressure.Text = Form1.f1.plc.ch2pre.ToString();
        }

        private void CH2ValveWrite_Click(object sender, EventArgs e)
        {
            short ch2pressure = Convert.ToInt16(CH2Pressure.Text);

            if (ChkPLCPress.Checked)
            {
                ch2pressure = Convert.ToInt16(Convert.ToDouble(CH2Pressure.Text) * 98);
            }
            else
            {
                ch2pressure = Convert.ToInt16(CH2Pressure.Text);
            }
            if (String.IsNullOrEmpty(CH2Pressure.Text.Trim()))
            {
                // MessageBox.Show("调压阀压力数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
            }
            else
            {
                if (ch2pressure > 100)
                {
                    //MessageBox.Show("调压阀压力数值不可以超过100！");
                    Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                }
                else
                {
                    Form1.f1.plc.ch2pre = ch2pressure;
                    Form1.f1.plc.CH2Writevalve();
                    WritePLCConfig();
                }
            }
            SaveFlowRunLog();
        }

        //CH3调压阀读写
        private void CH3ValveRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH3Readvalve();
            System.Threading.Thread.Sleep(100);
            CH3Pressure.Text = Form1.f1.plc.ch3pre.ToString();
        }

        private void CH3ValveWrite_Click(object sender, EventArgs e)
        {
            //short ch3pressure = Convert.ToInt16(CH3Pressure.Text);
            short ch3pressure;
            if (ChkPLCPress.Checked)
            {
                ch3pressure = Convert.ToInt16(Convert.ToDouble(CH3Pressure.Text) * 98);
            }
            else
            {
                ch3pressure = Convert.ToInt16(CH3Pressure.Text);
            }
            if (String.IsNullOrEmpty(CH3Pressure.Text.Trim()))
            {
                //MessageBox.Show("调压阀压力数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
            }
            else
            {
                if (ch3pressure > 100)
                {
                    // MessageBox.Show("调压阀压力数值不可以超过100！");
                    Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                }
                else
                {
                    Form1.f1.plc.ch3pre = ch3pressure;
                    Form1.f1.plc.CH3Writevalve();
                    WritePLCConfig();
                }
            }
            SaveFlowRunLog();
        }

        //CH4调压阀读写
        private void CH4ValveRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH4Readvalve();
            System.Threading.Thread.Sleep(100);
            CH4Pressure.Text = Form1.f1.plc.ch4pre.ToString();
        }

        private void CH4ValveWrite_Click(object sender, EventArgs e)
        {
            //short ch4pressure = Convert.ToInt16(CH4Pressure.Text);
            short ch4pressure;
            if (ChkPLCPress.Checked)
            {
                ch4pressure = Convert.ToInt16(Convert.ToDouble(CH4Pressure.Text) * 98);
            }
            else
            {
                ch4pressure = Convert.ToInt16(CH4Pressure.Text);
            }
            if (String.IsNullOrEmpty(CH4Pressure.Text.Trim()))
            {
                //MessageBox.Show("调压阀压力数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
            }
            else
            {
                if (ch4pressure > 100)
                {
                    //MessageBox.Show("调压阀压力数值不可以超过100！");
                    Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                }
                else
                {
                    Form1.f1.plc.ch4pre = ch4pressure;
                    Form1.f1.plc.CH4Writevalve();
                    WritePLCConfig();
                }
            }
            SaveFlowRunLog();
        }

        private void CH1Pressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8))
            {
                e.Handled = true;
                //MessageBox.Show("请输入数字！");
                Logger.Log(I18N.GetLangText(dicLang, "请输入数字"));
                MessageBox.Show(I18N.GetLangText(dicLang, "请输入数字"));
            }
        }

        private void CH2Pressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8))
            {
                e.Handled = true;
                //MessageBox.Show("请输入数字！");
                Logger.Log(I18N.GetLangText(dicLang, "请输入数字"));
                MessageBox.Show(I18N.GetLangText(dicLang, "请输入数字"));
            }
        }

        private void CH3Pressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8))
            {
                e.Handled = true;
                //MessageBox.Show("请输入数字！");
                Logger.Log(I18N.GetLangText(dicLang, "请输入数字"));
                MessageBox.Show(I18N.GetLangText(dicLang, "请输入数字"));
            }
        }

        private void CH4Pressure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8))
            {
                e.Handled = true;
                //MessageBox.Show("请输入数字！");
                Logger.Log(I18N.GetLangText(dicLang, "请输入数字"));
                MessageBox.Show(I18N.GetLangText(dicLang, "请输入数字"));
            }
        }

        //CH1电压读写
        private void CH1VolRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1ReadVol();
            System.Threading.Thread.Sleep(100);
            double ch1vol = Convert.ToDouble(Form1.f1.plc.ch1vol) / 1000;
            CH1Vol.Text = ch1vol.ToString();
        }

        private void CH1VolWrite_Click(object sender, EventArgs e)
        {
            double ch1vol = Convert.ToDouble(CH1Vol.Text);
            if (String.IsNullOrEmpty(CH1Vol.Text.Trim()))
            {
                //MessageBox.Show("电压数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "电压数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值需填写"));
            }
            else
            {
                if (ch1vol > 60)
                {
                    //MessageBox.Show("电压数值不可以超过60！");
                    Logger.Log(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                }
                else
                {
                    Form1.f1.plc.ch1vol = Convert.ToInt32(ch1vol * 1000);
                    Form1.f1.plc.CH1WriteVol();
                    WritePLCConfig();
                }
            }
        }

        //CH1电流读写
        private void CH1ElectRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1ReadVolElec();
            System.Threading.Thread.Sleep(100);
            double ch1elec = Convert.ToDouble(Form1.f1.plc.ch1elec) / 10000;
            CH1Elect.Text = ch1elec.ToString();
        }

        private void CH1ElectWrite_Click(object sender, EventArgs e)
        {
            double ch1elec = Convert.ToDouble(CH1Elect.Text);
            if (String.IsNullOrEmpty(CH1Elect.Text.Trim()))
            {
                //MessageBox.Show("电流数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "电流数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值需填写"));
            }
            else
            {
                if (ch1elec > 5)
                {
                    //MessageBox.Show("电流数值不可以超过5！");
                    Logger.Log(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                }
                else
                {
                    Form1.f1.plc.ch1elec = Convert.ToInt32(ch1elec * 10000);
                    Form1.f1.plc.CH1WriteElec();
                    WritePLCConfig();
                }
            }
        }

        //CH2电压读写
        private void CH2VolRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2ReadVol();
            System.Threading.Thread.Sleep(100);
            double ch2vol = Convert.ToDouble(Form1.f1.plc.ch2vol) / 1000;
            CH2Vol.Text = ch2vol.ToString();
        }

        private void CH2VolWrite_Click(object sender, EventArgs e)
        {
            double ch2vol = Convert.ToDouble(CH2Vol.Text);
            if (String.IsNullOrEmpty(CH2Vol.Text.Trim()))
            {
                //MessageBox.Show("电压数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "电压数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值需填写"));
            }
            else
            {
                if (ch2vol > 60)
                {
                    // MessageBox.Show("电压数值不可以超过60！");
                    Logger.Log(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                }
                else
                {
                    Form1.f1.plc.ch2vol = Convert.ToInt32(ch2vol * 1000);
                    Form1.f1.plc.CH2WriteVol();
                    WritePLCConfig();
                }
            }
        }

        //CH2电流读写
        private void CH2ElectRead_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2ReadVolElec();
            System.Threading.Thread.Sleep(100);
            double ch2elec = Convert.ToDouble(Form1.f1.plc.ch2elec) / 10000;
            CH2Elect.Text = ch2elec.ToString();
        }

        private void CH2ElectWrite_Click(object sender, EventArgs e)
        {
            double ch2elec = Convert.ToDouble(CH2Elect.Text);
            if (String.IsNullOrEmpty(CH2Elect.Text.Trim()))
            {
                //MessageBox.Show("电流数值需填写！");
                Logger.Log(I18N.GetLangText(dicLang, "电流数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值需填写"));
            }
            else
            {
                if (ch2elec > 5)
                {
                    //MessageBox.Show("电流数值不可以超过5！");
                    Logger.Log(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                    MessageBox.Show(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                }
                else
                {
                    Form1.f1.plc.ch2elec = Convert.ToInt32(ch2elec * 10000);
                    Form1.f1.plc.CH2WriteElec();
                    WritePLCConfig();
                }
            }
        }

        private void CH1Vol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                e.Handled = true;
            }
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
                // MessageBox.Show("第一位不可以是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                //MessageBox.Show("只能输入一个小数点！");
                //Logger.Log("只能输入一个小数点");
                Logger.Log(I18N.GetLangText(dicLang, "只能输入一个小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "只能输入一个小数点"));
            }
            //小数点（最大到3位）
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 3 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                e.Handled = true;
                //MessageBox.Show("小数点后最多输入三位！");
                // Logger.Log("小数点后最多输入三位");
                Logger.Log(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
                MessageBox.Show(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
            }

            //光标在小数点右侧时候判断
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
                //MessageBox.Show("第一位是0，第二位必须是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
            }
        }

        private void CH1Elect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                e.Handled = true;
            }
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
                // MessageBox.Show("第一位不可以是小数点！");
                //Logger.Log("第一位不可以是小数点");
                Logger.Log(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                //MessageBox.Show("只能输入一个小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "只能输入一个小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "只能输入一个小数点"));
            }
            //小数点（最大到3位）
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 3 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                e.Handled = true;
                //MessageBox.Show("小数点后最多输入三位！");
                Logger.Log(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
                MessageBox.Show(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
            }

            //光标在小数点右侧时候判断
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
                //MessageBox.Show("第一位是0，第二位必须是小数点");
                Logger.Log(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
            }
        }

        private void CH2Vol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                e.Handled = true;
            }
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
                //MessageBox.Show("第一位不可以是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                //MessageBox.Show("只能输入一个小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "只能输入一个小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "只能输入一个小数点"));
            }
            //小数点（最大到3位）
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 3 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                e.Handled = true;
                //MessageBox.Show("小数点后最多输入三位！");
                Logger.Log(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
                MessageBox.Show(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
            }

            //光标在小数点右侧时候判断
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
                //MessageBox.Show("第一位是0，第二位必须是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
            }
        }

        private void CH2Elect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != (char)('.'))
            {
                e.Handled = true;
            }
            //第1位小数点不可
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text == "")
            {
                e.Handled = true;
                //MessageBox.Show("第一位不可以是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位不可以是小数点"));
            }
            //小数点只能1次
            if (e.KeyChar == (char)('.') && ((TextBox)sender).Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                //MessageBox.Show("只能输入一个小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "只能输入一个小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "只能输入一个小数点"));
            }
            //小数点（最大到3位）
            if (e.KeyChar != '\b' && (((TextBox)sender).SelectionStart) > (((TextBox)sender).Text.LastIndexOf('.')) + 3 && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                e.Handled = true;
                //MessageBox.Show("小数点后最多输入三位！");
                Logger.Log(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
                MessageBox.Show(I18N.GetLangText(dicLang, "小数点后最多输入三位"));
            }

            //光标在小数点右侧时候判断
            if (e.KeyChar != '\b' && ((TextBox)sender).SelectionStart >= (((TextBox)sender).Text.LastIndexOf('.')) && ((TextBox)sender).Text.IndexOf(".") >= 0)
            {
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 1)
                {
                    if ((((TextBox)sender).Text.Length).ToString() == (((TextBox)sender).Text.IndexOf(".") + 3).ToString())
                        e.Handled = true;
                }
                if ((((TextBox)sender).SelectionStart) == (((TextBox)sender).Text.LastIndexOf('.')) + 2)
                {
                    if ((((TextBox)sender).Text.Length - 3).ToString() == ((TextBox)sender).Text.IndexOf(".").ToString()) e.Handled = true;
                }
            }
            //第1位是0，第2位必须是小数点
            if (e.KeyChar != (char)('.') && e.KeyChar != 8 && ((TextBox)sender).Text == "0")
            {
                e.Handled = true;
                //MessageBox.Show("第一位是0，第二位必须是小数点！");
                Logger.Log(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
                MessageBox.Show(I18N.GetLangText(dicLang, "第一位是0，第二位必须是小数点"));
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Model.Electricity elec = new Model.Electricity();
            elec.CH1UPADCMax = Convert.ToDouble(CH1UPADCMax.Text);
            elec.CH1UPADCMin = Convert.ToDouble(CH1UPADCMin.Text);
            elec.CH1UPADCComp = Convert.ToDouble(CH1UPADCComp.Text);
            elec.CH2UPADCMax = Convert.ToDouble(CH2UPADCMax.Text);
            elec.CH2UPADCMin = Convert.ToDouble(CH2UPADCMin.Text);
            elec.CH2UPADCComp = Convert.ToDouble(CH2UPADCComp.Text);

            elec.CH1UPVDCMax = Convert.ToDouble(CH1UPVDCMax.Text);
            elec.CH1UPVDCMin = Convert.ToDouble(CH1UPVDCMin.Text);
            elec.CH1UPVDCComp = Convert.ToDouble(CH1UPVDCComp.Text);
            elec.CH2UPVDCMax = Convert.ToDouble(CH2UPVDCMax.Text);
            elec.CH2UPVDCMin = Convert.ToDouble(CH2UPVDCMin.Text);
            elec.CH2UPVDCComp = Convert.ToDouble(CH2UPVDCComp.Text);

            elec.CH1DOWNADCMax = Convert.ToDouble(CH1DOWNADCMax.Text);
            elec.CH1DOWNADCMin = Convert.ToDouble(CH1DOWNADCMin.Text);
            elec.CH1DOWNADCComp = Convert.ToDouble(CH1DOWNADCComp.Text);
            elec.CH2DOWNADCMax = Convert.ToDouble(CH2DOWNADCMax.Text);
            elec.CH2DOWNADCMin = Convert.ToDouble(CH1DOWNADCMin.Text);
            elec.CH2DOWNADCComp = Convert.ToDouble(CH2DOWNADCComp.Text);

            elec.CH1DOWNVDCMax = Convert.ToDouble(CH1DOWNVDCMax.Text);
            elec.CH1DOWNVDCMin = Convert.ToDouble(CH1DOWNVDCMin.Text);
            elec.CH1DOWNVDCComp = Convert.ToDouble(CH1DOWNVDCComp.Text);
            elec.CH2DOWNVDCMax = Convert.ToDouble(CH2DOWNVDCMax.Text);
            elec.CH2DOWNVDCMin = Convert.ToDouble(CH2DOWNVDCMin.Text);
            elec.CH2DOWNVDCComp = Convert.ToDouble(CH2DOWNVDCComp.Text);

            elec.CH1FWDADCMax = Convert.ToDouble(CH1FWDADCMax.Text);
            elec.CH1FWDADCMin = Convert.ToDouble(CH1FWDADCMin.Text);
            elec.CH1FWDADCComp = Convert.ToDouble(CH1FWDADCComp.Text);
            elec.CH2FWDADCMax = Convert.ToDouble(CH2FWDADCMax.Text);
            elec.CH2FWDADCMin = Convert.ToDouble(CH2FWDADCMin.Text);
            elec.CH2FWDADCComp = Convert.ToDouble(CH2FWDADCComp.Text);

            elec.CH1FWDVDCMax = Convert.ToDouble(CH1FWDVDCMax.Text);
            elec.CH1FWDVDCMin = Convert.ToDouble(CH1FWDVDCMin.Text);
            elec.CH1FWDVDCComp = Convert.ToDouble(CH1FWDVDCComp.Text);
            elec.CH2FWDVDCMax = Convert.ToDouble(CH2FWDVDCMax.Text);
            elec.CH2FWDVDCMin = Convert.ToDouble(CH2FWDVDCMin.Text);
            elec.CH2FWDVDCComp = Convert.ToDouble(CH2FWDVDCComp.Text);

            elec.CH1RWDADCMax = Convert.ToDouble(CH1RWDADCMax.Text);
            elec.CH1RWDADCMin = Convert.ToDouble(CH1RWDADCMin.Text);
            elec.CH1RWDADCComp = Convert.ToDouble(CH1RWDADCComp.Text);
            elec.CH2RWDADCMax = Convert.ToDouble(CH2RWDADCMax.Text);
            elec.CH2RWDADCMin = Convert.ToDouble(CH2RWDADCMin.Text);
            elec.CH2RWDADCComp = Convert.ToDouble(CH2RWDADCComp.Text);

            elec.CH1RWDVDCMax = Convert.ToDouble(CH1RWDVDCMax.Text);
            elec.CH1RWDVDCMin = Convert.ToDouble(CH1RWDVDCMin.Text);
            elec.CH1RWDVDCComp = Convert.ToDouble(CH1RWDVDCComp.Text);
            elec.CH2RWDVDCMax = Convert.ToDouble(CH2RWDVDCMax.Text);
            elec.CH2RWDVDCMin = Convert.ToDouble(CH2RWDVDCMin.Text);
            elec.CH2RWDVDCComp = Convert.ToDouble(CH2RWDVDCComp.Text);

            elec.CH1ElecMax = Convert.ToDouble(CH1ElecMax.Text);
            elec.CH1ElecMin = Convert.ToDouble(CH1ElecMin.Text);
            elec.CH1ElecComp = Convert.ToDouble(CH1ElecComp.Text);
            elec.CH2ElecMax = Convert.ToDouble(CH2ElecMax.Text);
            elec.CH2ElecMin = Convert.ToDouble(CH2ElecMin.Text);
            elec.CH2ElecComp = Convert.ToDouble(CH2ElecComp.Text);

            Form1.f1.elec = elec;
            WriteElectricity();
        }

        private void WriteElectricity()
        {
            //string dialog;
            //dialog = "Electricity.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            //新增 240801
            config.IniWriteValue("Limits", "TotalFlowMax", TotalFlowMax.Text);
            config.IniWriteValue("Limits", "TotalFlowMin", TotalFlowMin.Text);
            config.IniWriteValue("Limits", "TotalPreMax", TotalPreMax.Text);

            config.IniWriteValue("Limits", "CH1FWDFlowTime", CH1FWDFLOWTime.Text);
            config.IniWriteValue("Limits", "CH2FWDFlowTime", CH2FWDflowtime.Text);
            config.IniWriteValue("Limits", "CH1FWDPreTime", CH1FwdPreTime.Text);
            config.IniWriteValue("Limits", "CH2FWDPreTime", CH2FwdpreTime.Text);
            config.IniWriteValue("Limits", "CH1FWDFlowMax", CH1FWDFlowMax.Text);
            config.IniWriteValue("Limits", "CH2FWDFlowMax", CH2FWDFlowMax.Text);
            config.IniWriteValue("Limits", "CH1FWDFlowMin", CH1FWDFlowMin.Text);
            config.IniWriteValue("Limits", "CH2FWDFlowMin", CH2FWDFlowMin.Text);
            config.IniWriteValue("Limits", "CH1FWDPreMax", CH1FwdPreMax.Text);
            config.IniWriteValue("Limits", "CH2FWDPreMax", CH2FWDPreMax.Text);
            config.IniWriteValue("Limits", "CH1FWDPreMin", CH1FwdPreMin.Text);
            config.IniWriteValue("Limits", "CH2FWDPreMin", CH2FWDPreMin.Text);

            ////////////////////////240801
            config.IniWriteValue("Limits", "CH1UPADCMax", CH1UPADCMax.Text);
            config.IniWriteValue("Limits", "CH2UPADCMax", CH2UPADCMax.Text);
            config.IniWriteValue("Limits", "CH1UPADCMin", CH1UPADCMin.Text);
            config.IniWriteValue("Limits", "CH2UPADCMin", CH2UPADCMin.Text);
            config.IniWriteValue("Limits", "CH1ADCComp", CH1UPADCComp.Text);
            config.IniWriteValue("Limits", "CH2ADCComp", CH2UPADCComp.Text);

            config.IniWriteValue("Limits", "CH1UPVDCMax", CH1UPVDCMax.Text);
            config.IniWriteValue("Limits", "CH2UPVDCMax", CH2UPVDCMax.Text);
            config.IniWriteValue("Limits", "CH1UPVDCMin", CH1UPVDCMin.Text);
            config.IniWriteValue("Limits", "CH2UPVDCMin", CH2UPVDCMin.Text);
            config.IniWriteValue("Limits", "CH1UPVDCComp", CH1UPVDCComp.Text);
            config.IniWriteValue("Limits", "CH2UPVDCComp", CH2UPVDCComp.Text);

            config.IniWriteValue("Limits", "CH1DOWNADCMax", CH1DOWNADCMax.Text);
            config.IniWriteValue("Limits", "CH2DOWNADCMax", CH2DOWNADCMax.Text);
            config.IniWriteValue("Limits", "CH1DOWNADCMin", CH1DOWNADCMin.Text);
            config.IniWriteValue("Limits", "CH2DOWNADCMin", CH1DOWNADCMin.Text);
            config.IniWriteValue("Limits", "CH1DOWNADCComp", CH1DOWNADCComp.Text);
            config.IniWriteValue("Limits", "CH2DOWNADCComp", CH2DOWNADCComp.Text);

            config.IniWriteValue("Limits", "CH1DOWNVDCMax", CH1DOWNVDCMax.Text);
            config.IniWriteValue("Limits", "CH2DOWNVDCMax", CH2DOWNVDCMax.Text);
            config.IniWriteValue("Limits", "CH1DOWNVDCMin", CH1DOWNVDCMin.Text);
            config.IniWriteValue("Limits", "CH2DOWNVDCMin", CH2DOWNVDCMin.Text);
            config.IniWriteValue("Limits", "CH1DOWNVDCComp", CH1DOWNVDCComp.Text);
            config.IniWriteValue("Limits", "CH2DOWNVDCComp", CH2DOWNVDCComp.Text);

            config.IniWriteValue("Limits", "CH1FWDADCMax", CH1FWDADCMax.Text);
            config.IniWriteValue("Limits", "CH2FWDADCMax", CH2FWDADCMax.Text);
            config.IniWriteValue("Limits", "CH1FWDADCMin", CH1FWDADCMin.Text);
            config.IniWriteValue("Limits", "CH2FWDADCMin", CH2FWDADCMin.Text);
            config.IniWriteValue("Limits", "CH1FWDADCComp", CH1FWDADCComp.Text);
            config.IniWriteValue("Limits", "CH2FWDADCComp", CH2FWDADCComp.Text);

            config.IniWriteValue("Limits", "CH1FWDVDCMax", CH1FWDVDCMax.Text);
            config.IniWriteValue("Limits", "CH2FWDVDCMax", CH2FWDVDCMax.Text);
            config.IniWriteValue("Limits", "CH1FWDVDCMin", CH1FWDVDCMin.Text);
            config.IniWriteValue("Limits", "CH2FWDVDCMin", CH2FWDVDCMin.Text);
            config.IniWriteValue("Limits", "CH1FWDVDCComp", CH1FWDVDCComp.Text);
            config.IniWriteValue("Limits", "CH2FWDVDCComp", CH2FWDVDCComp.Text);




            config.IniWriteValue("Limits", "CH1RWDADCMax", CH1RWDADCMax.Text);
            config.IniWriteValue("Limits", "CH2RWDADCMax", CH2RWDADCMax.Text);
            config.IniWriteValue("Limits", "CH1RWDADCMin", CH1RWDADCMin.Text);
            config.IniWriteValue("Limits", "CH2RWDADCMin", CH2RWDADCMin.Text);
            config.IniWriteValue("Limits", "CH1RWDADCComp", CH1RWDADCComp.Text);
            config.IniWriteValue("Limits", "CH2RWDADCComp", CH2RWDADCComp.Text);

            config.IniWriteValue("Limits", "CH1RWDVDCMax", CH1RWDVDCMax.Text);
            config.IniWriteValue("Limits", "CH2RWDVDCMax", CH2RWDVDCMax.Text);
            config.IniWriteValue("Limits", "CH1RWDVDCMin", CH1RWDVDCMin.Text);
            config.IniWriteValue("Limits", "CH2RWDVDCMin", CH2RWDVDCMin.Text);
            config.IniWriteValue("Limits", "CH1RWDVDCComp", CH1RWDVDCComp.Text);
            config.IniWriteValue("Limits", "CH2RWDVDCComp", CH2RWDVDCComp.Text);

            config.IniWriteValue("Limits", "CH1ElecMax", CH1ElecMax.Text);
            config.IniWriteValue("Limits", "CH2ElecMax", CH2ElecMax.Text);
            config.IniWriteValue("Limits", "CH1ElecMin", CH1ElecMin.Text);
            config.IniWriteValue("Limits", "CH2ElecMin", CH2ElecMin.Text);
            config.IniWriteValue("Limits", "CH1ElecCompensation", CH1ElecComp.Text);
            config.IniWriteValue("Limits", "CH2ElecCompensation", CH2ElecComp.Text);

            //string dialog = "Flow.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            config.IniWriteValue("Calculation", "CH1OverTime", CH1OverTime.Text);
            config.IniWriteValue("Calculation", "CH2OverTime", CH2OverTime.Text);
            config.IniWriteValue("Calculation", "CH3OverTime", CH3OverTime.Text);
            config.IniWriteValue("Calculation", "CH4OverTime", CH4OverTime.Text);
            config.IniWriteValue("Calculation", "CH1Press_OverTime", CH1Press_OverTime.Text);
            config.IniWriteValue("Calculation", "CH2Press_OverTime", CH2Press_OverTime.Text);
            config.IniWriteValue("Calculation", "CH3Press_OverTime", CH3Press_OverTime.Text);
            config.IniWriteValue("Calculation", "CH4Press_OverTime", CH4Press_OverTime.Text);
            config.IniWriteValue("Calculation", "CH1_1FlowMax", CH1_1FlowMax.Text);
            config.IniWriteValue("Calculation", "CH1_2FlowMax", CH1_2FlowMax.Text);
            config.IniWriteValue("Calculation", "CH2_1FlowMax", CH2_1FlowMax.Text);
            config.IniWriteValue("Calculation", "CH2_2FlowMax", CH2_2FlowMax.Text);
            config.IniWriteValue("Calculation", "CH1_1FlowMin", CH1_1FlowMin.Text);
            config.IniWriteValue("Calculation", "CH1_2FlowMin", CH1_2FlowMin.Text);
            config.IniWriteValue("Calculation", "CH2_1FlowMin", CH2_1FlowMin.Text);
            config.IniWriteValue("Calculation", "CH2_2FlowMin", CH2_2FlowMin.Text);
            config.IniWriteValue("Calculation", "CH1Cont_ElecMax", CH1Cont_ElecMax.Text);
            config.IniWriteValue("Calculation", "CH1Cont_ElecMin", CH1Cont_ElecMin.Text);
            config.IniWriteValue("Calculation", "CH1Cont_Elec_Compen", CH1Cont_Elec_Compen.Text);
            config.IniWriteValue("Calculation", "CH2Cont_ElecMax", CH2Cont_ElecMax.Text);
            config.IniWriteValue("Calculation", "CH2Cont_ElecMin", CH2Cont_ElecMin.Text);
            config.IniWriteValue("Calculation", "CH2Cont_Elec_Compen", CH2Cont_Elec_Compen.Text);
            config.IniWriteValue("Calculation", "CH1Cont_PressMax", CH1Cont_PressMax.Text);
            config.IniWriteValue("Calculation", "CH1Cont_PressMin", CH1Cont_PressMin.Text);
            config.IniWriteValue("Calculation", "CH1Cont_Pre_Compen", CH1Cont_Pre_Compen.Text);
            config.IniWriteValue("Calculation", "CH2Cont_PressMax", CH2Cont_PressMax.Text);
            config.IniWriteValue("Calculation", "CH2Cont_PressMin", CH2Cont_PressMin.Text);
            config.IniWriteValue("Calculation", "CH2Cont_Pre_Compen", CH2Cont_Pre_Compen.Text);
            //泄气的气压上下限
            config.IniWriteValue("Calculation", "CH1RWDPressMax", CH1RWDPressMax.Text);
            config.IniWriteValue("Calculation", "CH2RWDPressMax", CH2RWDPressMax.Text);
            config.IniWriteValue("Calculation", "CH1RWDPressMin", CH1RWDPressMin.Text);
            config.IniWriteValue("Calculation", "CH2RWDPressMin", CH2RWDPressMin.Text);
            config.IniWriteValue("Calculation", "CH1RWDOverTime", CH1RWDOverTime.Text);
            config.IniWriteValue("Calculation", "CH2RWDOverTime", CH2RWDOverTime.Text);
            config.IniWriteValue("Calculation", "CH1_1PreMax", CH1_1PreMax.Text);
            config.IniWriteValue("Calculation", "CH1_1PreMin", CH1_1PreMin.Text);
            config.IniWriteValue("Calculation", "CH1_2PreMax", CH1_2PreMax.Text);
            config.IniWriteValue("Calculation", "CH1_2PreMin", CH1_2PreMin.Text);
            config.IniWriteValue("Calculation", "CH2_1PreMax", CH2_1PreMax.Text);
            config.IniWriteValue("Calculation", "CH2_1PreMin", CH2_1PreMin.Text);
            config.IniWriteValue("Calculation", "CH2_2PreMax", CH2_2PreMax.Text);
            config.IniWriteValue("Calculation", "CH2_2PreMin", CH2_2PreMin.Text);




            SaveFlowRunLog();
        }
        //6.18读取
        private void ReadElectricity()
        {
            //string dialog;
            //dialog = "Electricity.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            ReadConfig con = new ReadConfig();
            Model.Electricity elec;
            elec = con.ReadElectricity();

            //240731新增
            TotalFlowMax.Text = elec.TotalFlowMax.ToString();
            TotalFlowMin.Text = elec.TotalFlowMin.ToString();
            TotalPreMax.Text = elec.TotalPreMax.ToString();

            CH1UPADCMax.Text = elec.CH1UPADCMax.ToString();
            CH2UPADCMax.Text = elec.CH2UPADCMax.ToString();
            CH1UPADCMin.Text = elec.CH1UPADCMin.ToString();
            CH2UPADCMin.Text = elec.CH2UPADCMin.ToString();
            CH1UPADCComp.Text = elec.CH1UPADCComp.ToString();
            CH2UPADCComp.Text = elec.CH2UPADCComp.ToString();

            CH1UPVDCMax.Text = elec.CH1UPVDCMax.ToString();
            CH2UPVDCMax.Text = elec.CH2UPVDCMax.ToString();
            CH1UPVDCMin.Text = elec.CH1UPVDCMin.ToString();
            CH2UPVDCMin.Text = elec.CH2UPVDCMin.ToString();
            CH1UPVDCComp.Text = elec.CH1UPVDCComp.ToString();
            CH2UPVDCComp.Text = elec.CH2UPVDCComp.ToString();

            CH1DOWNADCMax.Text = elec.CH1DOWNADCMax.ToString();
            CH2DOWNADCMax.Text = elec.CH2DOWNADCMax.ToString();
            CH1DOWNADCMin.Text = elec.CH1DOWNADCMin.ToString();
            CH2DOWNADCMin.Text = elec.CH2DOWNADCMin.ToString();
            CH1DOWNADCComp.Text = elec.CH1DOWNADCComp.ToString();
            CH2DOWNADCComp.Text = elec.CH2DOWNADCComp.ToString();

            CH1DOWNVDCMax.Text = elec.CH1DOWNVDCMax.ToString();
            CH2DOWNVDCMax.Text = elec.CH2DOWNVDCMax.ToString();
            CH1DOWNVDCMin.Text = elec.CH1DOWNVDCMin.ToString();
            CH2DOWNVDCMin.Text = elec.CH2DOWNVDCMin.ToString();
            CH1DOWNVDCComp.Text = elec.CH1DOWNVDCComp.ToString();
            CH2DOWNVDCComp.Text = elec.CH2DOWNVDCComp.ToString();

            CH1FWDADCMax.Text = elec.CH1FWDADCMax.ToString();
            CH2FWDADCMax.Text = elec.CH2FWDADCMax.ToString();
            CH1FWDADCMin.Text = elec.CH1FWDADCMin.ToString();
            CH2FWDADCMin.Text = elec.CH2FWDADCMin.ToString();
            CH1FWDADCComp.Text = elec.CH1FWDADCComp.ToString();
            CH2FWDADCComp.Text = elec.CH2FWDADCComp.ToString();

            CH1FWDVDCMax.Text = elec.CH1FWDVDCMax.ToString();
            CH2FWDVDCMax.Text = elec.CH2FWDVDCMax.ToString();
            CH1FWDVDCMin.Text = elec.CH1FWDVDCMin.ToString();
            CH2FWDVDCMin.Text = elec.CH2FWDVDCMin.ToString();
            CH1FWDVDCComp.Text = elec.CH1FWDVDCComp.ToString();
            CH2FWDVDCComp.Text = elec.CH2FWDVDCComp.ToString();

            //新增
            CH1FWDFLOWTime.Text = elec.CH1FWDFlowTime.ToString();
            CH2FWDflowtime.Text = elec.CH2FWDFlowTime.ToString();
            CH1FwdPreTime.Text = elec.CH1FwdpreTime.ToString();
            CH2FwdpreTime.Text = elec.CH2FwdpreTime.ToString();
            CH1FWDFlowMax.Text = elec.CH1FwdFlowMax.ToString();
            CH2FWDFlowMax.Text = elec.CH2FwdFlowMax.ToString();
            CH1FWDFlowMin.Text = elec.CH1FwdFlowMin.ToString();
            CH2FWDFlowMin.Text = elec.CH2FwdFlowMin.ToString();
            CH1FwdPreMax.Text = elec.CH1FwdPreMax.ToString();
            CH2FWDPreMax.Text = elec.CH2FwdPreMax.ToString();
            CH2FWDFlowMax.Text = elec.CH2FwdFlowMax.ToString();
            CH1FwdPreMin.Text = elec.CH1FwdPreMin.ToString();
            CH2FWDPreMin.Text = elec.CH2FwdPreMin.ToString();


            CH1RWDADCMax.Text = elec.CH1RWDADCMax.ToString();
            CH2RWDADCMax.Text = elec.CH2RWDADCMax.ToString();
            CH1RWDADCMin.Text = elec.CH1RWDADCMin.ToString();
            CH2RWDADCMin.Text = elec.CH2RWDADCMin.ToString();
            CH1RWDADCComp.Text = elec.CH1RWDADCComp.ToString();
            CH2RWDADCComp.Text = elec.CH2RWDADCComp.ToString();

            CH1RWDVDCMax.Text = elec.CH1RWDVDCMax.ToString();
            CH2RWDVDCMax.Text = elec.CH2RWDVDCMax.ToString();
            CH1RWDVDCMin.Text = elec.CH1RWDVDCMin.ToString();
            CH2RWDVDCMin.Text = elec.CH2RWDVDCMin.ToString();
            CH1RWDVDCComp.Text = elec.CH1RWDVDCComp.ToString();
            CH2RWDVDCComp.Text = elec.CH2RWDVDCComp.ToString();

            CH1ElecMax.Text = elec.CH1ElecMax.ToString();
            CH2ElecMax.Text = elec.CH2ElecMax.ToString();
            CH1ElecMin.Text = elec.CH1ElecMin.ToString();
            CH2ElecMin.Text = elec.CH2ElecMin.ToString();
            CH1ElecComp.Text = elec.CH1ElecComp.ToString();
            CH2ElecComp.Text = elec.CH2ElecComp.ToString();

            Model.Flow flow;
            flow = con.ReadFlow();
            CH1OverTime.Text = flow.CH1OverTime.ToString();
            CH2OverTime.Text = flow.CH2OverTime.ToString();
            CH3OverTime.Text = flow.CH3OverTime.ToString();
            CH4OverTime.Text = flow.CH4OverTime.ToString();
            CH1Press_OverTime.Text = flow.CH1Press_OverTime.ToString();
            CH2Press_OverTime.Text = flow.CH2Press_OverTime.ToString();
            CH3Press_OverTime.Text = flow.CH3Press_OverTime.ToString();
            CH4Press_OverTime.Text = flow.CH4Press_OverTime.ToString();
            CH1_1FlowMax.Text = flow.CH1_1FlowMax.ToString();
            CH1_2FlowMax.Text = flow.CH1_2FlowMax.ToString();
            CH2_1FlowMax.Text = flow.CH2_1FlowMax.ToString();
            CH2_2FlowMax.Text = flow.CH2_2FlowMax.ToString();
            CH1_1FlowMin.Text = flow.CH1_1FlowMin.ToString();
            CH1_2FlowMin.Text = flow.CH1_2FlowMin.ToString();
            CH2_1FlowMin.Text = flow.CH2_1FlowMin.ToString();
            CH2_2FlowMin.Text = flow.CH2_2FlowMin.ToString();
            CH1Cont_ElecMax.Text = flow.CH1Cont_ElecMax.ToString();
            CH1Cont_ElecMin.Text = flow.CH1Cont_ElecMin.ToString();
            CH1Cont_Elec_Compen.Text = flow.CH1Cont_Elec_Compen.ToString();
            CH2Cont_ElecMax.Text = flow.CH2Cont_ElecMax.ToString();
            CH2Cont_ElecMin.Text = flow.CH2Cont_ElecMin.ToString();
            CH2Cont_Elec_Compen.Text = flow.CH2Cont_Elec_Compen.ToString();
            CH1Cont_PressMax.Text = flow.CH1Cont_PressMax.ToString();
            CH1Cont_PressMin.Text = flow.CH1Cont_PressMin.ToString();
            CH1Cont_Pre_Compen.Text = flow.CH1Cont_Pre_Compen.ToString();
            CH2Cont_PressMax.Text = flow.CH2Cont_PressMax.ToString();
            CH2Cont_PressMin.Text = flow.CH2Cont_PressMin.ToString();
            CH2Cont_Pre_Compen.Text = flow.CH2Cont_Pre_Compen.ToString();
            //泄气的气压上下限
            CH1RWDPressMax.Text = flow.CH1RWDPressMax.ToString();
            CH2RWDPressMax.Text = flow.CH2RWDPressMax.ToString();
            CH1RWDPressMin.Text = flow.CH1RWDPressMin.ToString();
            CH2RWDPressMin.Text = flow.CH2RWDPressMin.ToString();
            CH1RWDOverTime.Text = flow.CH1RWDOverTime.ToString();
            CH2RWDOverTime.Text = flow.CH2RWDOverTime.ToString();
            CH1_1PreMax.Text = flow.CH1_1PreMax.ToString();
            CH1_1PreMin.Text = flow.CH1_1PreMin.ToString();
            CH1_2PreMax.Text = flow.CH1_2PreMax.ToString();
            CH1_2PreMin.Text = flow.CH1_2PreMin.ToString();
            CH2_1PreMax.Text = flow.CH2_1PreMax.ToString();
            CH2_1PreMin.Text = flow.CH2_1PreMin.ToString();
            CH2_2PreMax.Text = flow.CH2_2PreMax.ToString();
            CH2_2PreMin.Text = flow.CH2_2PreMin.ToString();
        }

        /// <summary>
        /// PLC设置
        /// </summary>
        public void WritePLCConfig()
        {
            //string dialog = "PLC.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("PLC", "CH1Pressure", CH1Pressure.Text);
            config.IniWriteValue("PLC", "CH2Pressure", CH2Pressure.Text);
            config.IniWriteValue("PLC", "CH3Pressure", CH3Pressure.Text);
            config.IniWriteValue("PLC", "CH4Pressure", CH4Pressure.Text);
            config.IniWriteValue("PLC", "CH1Vol", CH1Vol.Text);
            config.IniWriteValue("PLC", "CH2Vol", CH2Vol.Text);
            config.IniWriteValue("PLC", "CH1Elect", CH1Elect.Text);
            config.IniWriteValue("PLC", "CH2Elect", CH2Elect.Text);
            config.IniWriteValue("PLC", "ChkPLCPress", ChkPLCPress.Checked.ToString());

            config.IniWriteValue("IPC", "CKCH1Vol", CKCh1Vol.Text);
            config.IniWriteValue("IPC", "CKCH2Vol", CKCh2Vol.Text);
            config.IniWriteValue("IPC", "CKCh1Current", CKCh1Current.Text);
            config.IniWriteValue("IPC", "CKCh2Current", CKCh2Current.Text);
        }

        /// <summary>
        /// 读取单位是否勾选以及调压阀压力
        /// </summary>
        private void ReadPLCConfig()
        {
            ReadConfig con = new ReadConfig();
            Setup.PLCPress press = con.ReadPLCConfig();
            CH1Pressure.Text = press.CH1Pressure;
            CH2Pressure.Text = press.CH2Pressure;
            CH3Pressure.Text = press.CH3Pressure;
            CH4Pressure.Text = press.CH4Pressure;
            CH1Vol.Text = press.CH1Vol;
            CH2Vol.Text = press.CH2Vol;
            CH1Elect.Text = press.CH1Elect;
            CH2Elect.Text = press.CH2Elect;
            CKCh1Vol.Text = press.CKCH1Vol;
            CKCh2Vol.Text = press.CKCH2Vol;
            CKCh1Current.Text = press.CKCH1Current;
            CKCh2Current.Text = press.CKCH2Current;

            ChkPLCPress.Checked = press.ChkPLCPress;
        }

        private void CH1PowerOpen_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1PowerOpen();
        }

        private void CH1PowerClose_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1PowerClose();
        }

        private void CH2PowerOpen_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2PowerOpen();
        }

        private void CH2PowerClose_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2PowerClose();
        }

        private void TestParams_Load(object sender, EventArgs e)
        {
            try
            {
                //Form1.f1.IsRun.Stop();

                //PortClose.Interval = 100;
                //PortClose.Start();
                MachineNum.SelectedIndex = 0;
                ReadParametersNumber(1);
                ReadParameters(MachineNum.SelectedIndex + 1, ParaNum.SelectedIndex + 1);
                //ReadPressConfig(MachineNum.SelectedIndex + 1);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
            }
        }

        //上传参数
        private void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //BtnUpload.Enabled = false;
                //Form1.f1.IsRun.Stop();
                //Form1.f1.CH2IsRun.Stop();
                //Form1.f1.CH3IsRun.Stop();
                //Form1.f1.CH4IsRun.Stop();

                //if (FullTime.Text == "" || BalanTime.Text == "" || TestTime1.Text == "" || ExhaustTime.Text == "" || DelayTime1.Text == "" || DelayTime2.Text == "" || RelieveDelay.Text == "" || Evolume.Text == "" || LUnit.Text == "" || PUnit.Text == ""
                //    || FPtoplimit.Text == "" || FPlowlimit.Text == "" || BalanPreMax.Text == "" || BalanPreMin.Text == "" || Leaktoplimit.Text == "" || Leaklowlimit.Text == "")
                //{
                //    MessageBox.Show("输入参数格式不对");
                //}
                //else
                //{
                //    double full = Convert.ToDouble(FullTime.Text) * 10;
                //    double balan = Convert.ToDouble(BalanTime.Text) * 10;
                //    double testtime = Convert.ToDouble(TestTime1.Text) * 10;
                //    double exhaust = Convert.ToDouble(ExhaustTime.Text) * 10;
                //    double relieve = Convert.ToDouble(RelieveDelay.Text) * 10;
                //    double delay1 = Convert.ToDouble(DelayTime1.Text) * 10;
                //    double delay2 = Convert.ToDouble(DelayTime2.Text) * 10;

                //    string hex_full = Convert.ToInt32(full).ToString("x4");
                //    string hex_balan = Convert.ToInt32(balan).ToString("x4");
                //    string hex_testtime = Convert.ToInt32(testtime).ToString("x4");
                //    string hex_exhaust = Convert.ToInt32(exhaust).ToString("x4");
                //    string hex_relieve = Convert.ToInt32(relieve).ToString("x4");
                //    string hex_delay1 = Convert.ToInt32(delay1).ToString("x4");
                //    string hex_delay2 = Convert.ToInt32(delay2).ToString("x4");

                //    byte[] fpmax1 = BitConverter.GetBytes(Convert.ToSingle(FPtoplimit.Text));//将字符串转换成字节数组
                //    string fpmax2 = BitConverter.ToString(fpmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string fptoplimit = fpmax2.Substring(4, 4) + fpmax2.Substring(0, 4);

                //    byte[] fpmin1 = BitConverter.GetBytes(Convert.ToSingle(FPlowlimit.Text));//将字符串转换成字节数组
                //    string fpmin2 = BitConverter.ToString(fpmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string fplowlimit = fpmin2.Substring(4, 4) + fpmin2.Substring(0, 4);

                //    byte[] bpmax1 = BitConverter.GetBytes(Convert.ToSingle(BalanPreMax.Text));//将字符串转换成字节数组
                //    string bpmax2 = BitConverter.ToString(bpmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string balanpremax = bpmax2.Substring(4, 4) + bpmax2.Substring(0, 4);

                //    byte[] bpmin1 = BitConverter.GetBytes(Convert.ToSingle(BalanPreMin.Text));//将字符串转换成字节数组
                //    string bpmin2 = BitConverter.ToString(bpmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string balanpremin = bpmin2.Substring(4, 4) + bpmin2.Substring(0, 4);

                //    byte[] leakmax1 = BitConverter.GetBytes(Convert.ToSingle(Leaktoplimit.Text));//将字符串转换成字节数组
                //    string leakmax2 = BitConverter.ToString(leakmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string leaktoplimit = leakmax2.Substring(4, 4) + leakmax2.Substring(0, 4);

                //    byte[] leakmin1 = BitConverter.GetBytes(Convert.ToSingle(Leaklowlimit.Text));//将字符串转换成字节数组
                //    string leakmin2 = BitConverter.ToString(leakmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string leaklowlimit = leakmin2.Substring(4, 4) + leakmin2.Substring(0, 4);

                //    //上传等效容积
                //    byte[] evol1 = BitConverter.GetBytes(Convert.ToSingle(Evolume.Text));//将字符串转换成字节数组
                //    string evol2 = BitConverter.ToString(evol1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                //    string evolume = evol2.Substring(4, 4) + evol2.Substring(0, 4);

                //    int punit = PUnit.SelectedIndex;
                //    int lunit = LUnit.SelectedIndex;

                //    string hex_punit = Convert.ToInt32(punit).ToString("x4");
                //    string hex_lunit = Convert.ToInt32(lunit).ToString("x4");

                //    SendText.Text = "10 03 EE 00 17 2E";
                //    SendText.Text += hex_full;
                //    SendText.Text += hex_balan;
                //    SendText.Text += hex_testtime;
                //    SendText.Text += hex_exhaust;
                //    SendText.Text += hex_relieve;
                //    SendText.Text += hex_delay1;
                //    SendText.Text += hex_delay2;
                //    SendText.Text += fptoplimit;
                //    SendText.Text += fplowlimit;
                //    SendText.Text += balanpremax;
                //    SendText.Text += balanpremin;
                //    SendText.Text += leaktoplimit;
                //    SendText.Text += leaklowlimit;
                //    SendText.Text += evolume;
                //    SendText.Text += hex_punit;
                //    SendText.Text += hex_lunit;

                //    switch (MachineNum.SelectedIndex)
                //    {
                //        case 0:
                //            string ch1sendstr = "01 " + SendText.Text;
                //            //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                //            Form1.f1.leftclient.btnSendData(ch1sendstr);
                //            Form1.f1.ch1stage = 10;
                //            break;
                //        case 1:
                //            string ch2sendstr = "02 " + SendText.Text;
                //            //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                //            Form1.f1.leftclient.btnSendData(ch2sendstr);
                //            Form1.f1.ch2stage = 10;
                //            break;
                //        case 2:
                //            string ch3sendstr = "03 " + SendText.Text;
                //            //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                //            Form1.f1.rightclient.btnSendData(ch3sendstr);
                //            Form1.f1.ch3stage = 10;
                //            break;
                //        case 3:
                //            string ch4sendstr = "04 " + SendText.Text;
                //            //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                //            Form1.f1.rightclient.btnSendData(ch4sendstr);
                //            Form1.f1.ch4stage = 10;
                //            break;
                //    }
                EndUpload1.Interval = 400;
                EndUpload1.Start();
                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
            }
        }

        //保存参数线圈置一
        private void EndUpload1_Tick(object sender, EventArgs e)
        {
            try
            {
                //EndUpload1.Stop();
                //SendText.Text = "05 00 03 FF 00";
                //switch (MachineNum.SelectedIndex)
                //{
                //    case 0:
                //        string ch1sendstr = "01 " + SendText.Text;
                //        //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                //        Form1.f1.leftclient.btnSendData(ch1sendstr);
                //        Form1.f1.ch1stage = 10;
                //        break;
                //    case 1:
                //        string ch2sendstr = "02 " + SendText.Text;
                //        //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                //        Form1.f1.leftclient.btnSendData(ch2sendstr);
                //        Form1.f1.ch2stage = 10;
                //        break;
                //    case 2:
                //        string ch3sendstr = "03 " + SendText.Text;
                //        //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                //        Form1.f1.rightclient.btnSendData(ch3sendstr);
                //        Form1.f1.ch3stage = 10;
                //        break;
                //    case 3:
                //        string ch4sendstr = "04 " + SendText.Text;
                //        //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                //        Form1.f1.rightclient.btnSendData(ch4sendstr);
                //        Form1.f1.ch4stage = 10;
                //        break;
                //}

                BeeSave.Interval = 300;
                BeeSave.Start();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
            }
        }

        //修改蜂鸣器设定，写入0是打开蜂鸣器，写入1是关闭蜂鸣器
        private void BeeSave_Tick(object sender, EventArgs e)
        {
            BeeSave.Stop();
            string hex_bee;
            if (ChkBee.Checked)
            {
                hex_bee = "FF00";
            }
            else
            {
                hex_bee = "0000";
            }

            SendText.Text = "05 00 04";
            SendText.Text += hex_bee;
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    string ch1sendstr = "01 " + SendText.Text;
                    //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                    Form1.f1.ch1client.btnSendData(ch1sendstr);
                    Form1.f1.ch1stage = 10;
                    Form1.f1.CH1IsRun.Interval = 1000;
                    Form1.f1.CH1IsRun.Start();
                    Form1.f1.ch1stage = 1;
                    break;

                case 1:
                    string ch2sendstr = "02 " + SendText.Text;
                    //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                    Form1.f1.ch1client.btnSendData(ch2sendstr);
                    Form1.f1.ch2stage = 10;
                    Form1.f1.CH2IsRun.Interval = 1000;
                    Form1.f1.CH2IsRun.Start();
                    Form1.f1.ch2stage = 1;
                    break;

                case 2:
                    string ch3sendstr = "03 " + SendText.Text;
                    //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                    Form1.f1.ch3client.btnSendData(ch3sendstr);
                    Form1.f1.ch3stage = 10;
                    Form1.f1.CH3IsRun.Interval = 1000;
                    Form1.f1.CH3IsRun.Start();
                    Form1.f1.ch3stage = 1;
                    break;

                case 3:
                    string ch4sendstr = "04 " + SendText.Text;
                    //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                    Form1.f1.ch3client.btnSendData(ch4sendstr);
                    Form1.f1.ch4stage = 10;
                    Form1.f1.CH4IsRun.Interval = 1000;
                    Form1.f1.CH4IsRun.Start();
                    Form1.f1.ch4stage = 1;
                    break;
            }
            BtnUpload.Enabled = true;
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.f1.CH1IsRun.Stop();
                Form1.f1.CH2IsRun.Stop();
                Form1.f1.CH3IsRun.Stop();
                Form1.f1.CH4IsRun.Stop();
                //BtnRead.Enabled = false;
                ReadBee.Interval = 400;
                ReadBee.Start();
                switch (MachineNum.SelectedIndex)
                {
                    case 0:
                        string ch1sendstr = "01 03 03 E8 00 1D";
                        //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                        Form1.f1.ch1client.btnSendData(ch1sendstr);
                        Form1.f1.ch1stage = 2;
                        //Form1.f1.IsRun.Interval = 1000;
                        //Form1.f1.IsRun.Start();
                        //Form1.f1.ch1stage = 1;
                        Form1.f1.ch1readpara = true;
                        break;

                    case 1:
                        string ch2sendstr = "02 03 03 E8 00 1D";
                        //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                        Form1.f1.ch1client.btnSendData(ch2sendstr);
                        Form1.f1.ch2stage = 2;
                        //Form1.f1.CH2IsRun.Interval = 1000;
                        //Form1.f1.CH2IsRun.Start();
                        //Form1.f1.ch2stage = 1;
                        Form1.f1.ch2readpara = true;
                        break;

                    case 2:
                        string ch3sendstr = "03 03 03 E8 00 1D";
                        //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                        Form1.f1.ch3client.btnSendData(ch3sendstr);
                        Form1.f1.ch3stage = 2;
                        //Form1.f1.CH3IsRun.Interval = 1000;
                        //Form1.f1.CH3IsRun.Start();
                        //Form1.f1.ch3stage = 1;
                        Form1.f1.ch3readpara = true;
                        break;

                    case 3:
                        string ch4sendstr = "04 03 03 E8 00 1D";
                        //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                        Form1.f1.ch3client.btnSendData(ch4sendstr);
                        Form1.f1.ch4stage = 2;
                        //Form1.f1.CH4IsRun.Interval = 1000;
                        //Form1.f1.CH4IsRun.Start();
                        //Form1.f1.ch4stage = 1;
                        Form1.f1.ch4readpara = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Logger.Log(ex.Message);
                //BtnRead.Enabled = true;
            }
        }

        //读取蜂鸣器，打开或关闭
        private void ReadBee_Tick(object sender, EventArgs e)
        {
            ReadBee.Stop();
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    string ch1sendstr = "01 01 00 04 00 01";
                    //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                    Form1.f1.ch1client.btnSendData(ch1sendstr);
                    Form1.f1.ch1stage = 6;
                    //Form1.f1.IsRun.Interval = 1000;
                    //Form1.f1.IsRun.Start();
                    //Form1.f1.ch1stage = 1;
                    Form1.f1.ch1readpara = true;
                    break;

                case 1:
                    string ch2sendstr = "02 01 00 04 00 01";
                    //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                    Form1.f1.ch1client.btnSendData(ch2sendstr);
                    Form1.f1.ch2stage = 6;
                    //Form1.f1.CH2IsRun.Interval = 1000;
                    //Form1.f1.CH2IsRun.Start();
                    //Form1.f1.ch2stage = 1;
                    Form1.f1.ch2readpara = true;
                    break;

                case 2:
                    string ch3sendstr = "03 01 00 04 00 01";
                    //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                    Form1.f1.ch3client.btnSendData(ch3sendstr);
                    Form1.f1.ch3stage = 6;
                    //Form1.f1.CH3IsRun.Interval = 1000;
                    //Form1.f1.CH3IsRun.Start();
                    //Form1.f1.ch3stage = 1;
                    Form1.f1.ch3readpara = true;
                    break;

                case 3:
                    string ch4sendstr = "04 01 00 04 00 01";
                    //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                    Form1.f1.ch3client.btnSendData(ch4sendstr);
                    Form1.f1.ch4stage = 6;
                    //Form1.f1.CH4IsRun.Interval = 1000;
                    //Form1.f1.CH4IsRun.Start();
                    //Form1.f1.ch4stage = 1;
                    Form1.f1.ch4readpara = true;
                    break;
            }
            ReadParam.Interval = 1000;
            ReadParam.Start();
        }

        private void ReadParam_Tick(object sender, EventArgs e)
        {
            ReadParam.Stop();
            Model.CH_PARAMS ch_params = new Model.CH_PARAMS();
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    ch_params = Form1.f1.ch1_1params;
                    Form1.f1.CH1IsRun.Interval = 1000;
                    Form1.f1.CH1IsRun.Start();
                    Form1.f1.ch1_1step = 5;
                    break;

                case 1:
                    ch_params = Form1.f1.ch1_2params;
                    Form1.f1.CH2IsRun.Interval = 1000;
                    Form1.f1.CH2IsRun.Start();
                    Form1.f1.ch1_2step = 5;
                    break;

                case 2:
                    ch_params = Form1.f1.ch2_1params;
                    Form1.f1.CH3IsRun.Interval = 1000;
                    Form1.f1.CH3IsRun.Start();
                    Form1.f1.ch2_1step = 5;
                    break;

                case 3:
                    ch_params = Form1.f1.ch2_2params;
                    Form1.f1.CH4IsRun.Interval = 1000;
                    Form1.f1.CH4IsRun.Start();
                    Form1.f1.ch2_2step = 5;
                    break;
            }
            FullTime.Text = ch_params.FullTime;
            BalanTime.Text = ch_params.BalanTime;
            TestTime1.Text = ch_params.TestTime1;
            ExhaustTime.Text = ch_params.ExhaustTime;
            Evolume.Text = ch_params.Evolume;
            DelayTime1.Text = ch_params.DelayTime1;
            DelayTime2.Text = ch_params.DelayTime2;
            RelieveDelay.Text = ch_params.RelieveDelay;
            FPtoplimit.Text = ch_params.FPtoplimit;
            FPlowlimit.Text = ch_params.FPlowlimit;
            BalanPreMax.Text = ch_params.BalanPreMax;
            BalanPreMin.Text = ch_params.BalanPreMin;
            Leaktoplimit.Text = ch_params.Leaktoplimit;
            Leaklowlimit.Text = ch_params.Leaklowlimit;
            PUnit.SelectedIndex = ch_params.PUnit_index;
            LUnit.SelectedIndex = ch_params.LUnit_index;
            ChkBee.Checked = ch_params.ChkBee;
            PressCompensation.Text = ch_params.PressCompensation;
            //BtnRead.Enabled = true;
        }

        public Model.CH_PARAMS readCHParam(int CH, int i)
        {
            //1 2
            //1 3
            //2 2
            //2 3
            //3 2
            //3 3
            //4 2
            //4 4
            ReadConfig con = new ReadConfig();
            return con.ReadParameters(CH, i);
        }

        private void BtnKeep_Click(object sender, EventArgs e)
        {
            //int i = Convert.ToInt32(ParaNum.Text);
            int i = ParaNum.SelectedIndex + 1;
            SetParameters(MachineNum.SelectedIndex + 1, i);
            SaveFlowRunLog();
            WriteBc();

        }


        public void WriteBc()
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Bc", "BCBC", PressCompensation.Text);

        }

        public void ReadBc()
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            PressCompensation.Text = mesconfig.IniReadValue("Bc", "BCBC");

        }


        private void ParaNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = ParaNum.SelectedIndex + 1;
            ReadParameters(MachineNum.SelectedIndex + 1, i);
        }

        //
        /// <summary>
        /// 写入注册表,CH是通道，i是参数编号
        /// </summary>
        /// <param name="CH"></param>
        /// <param name="i"></param>
        private void SetParameters(int CH, int i)
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            //mesconfig.IniWriteValue("Parameters", CH + "params_number", ParaNum.Text);
            mesconfig.IniWriteValue("Parameters", CH + "params_number", (ParaNum.SelectedIndex + 1).ToString());
            mesconfig.IniWriteValue("Parameters", CH + "paraname" + i, ParaName.Text);
            mesconfig.IniWriteValue("Parameters", CH + "fulltime" + i, FullTime.Text);
            mesconfig.IniWriteValue("Parameters", CH + "balantime" + i, BalanTime.Text);
            mesconfig.IniWriteValue("Parameters", CH + "testtime1" + i, TestTime1.Text);
            mesconfig.IniWriteValue("Parameters", CH + "exhausttime" + i, ExhaustTime.Text);
            mesconfig.IniWriteValue("Parameters", CH + "delaytime1" + i, DelayTime1.Text);
            mesconfig.IniWriteValue("Parameters", CH + "delaytime2" + i, DelayTime2.Text);
            mesconfig.IniWriteValue("Parameters", CH + "relievedelay" + i, RelieveDelay.Text);
            mesconfig.IniWriteValue("Parameters", CH + "evolume" + i, Evolume.Text);
            mesconfig.IniWriteValue("Parameters", CH + "fptoplimit" + i, FPtoplimit.Text);
            mesconfig.IniWriteValue("Parameters", CH + "fplowlimit" + i, FPlowlimit.Text);
            mesconfig.IniWriteValue("Parameters", CH + "balanpremax" + i, BalanPreMax.Text);
            mesconfig.IniWriteValue("Parameters", CH + "balanpremin" + i, BalanPreMin.Text);
            mesconfig.IniWriteValue("Parameters", CH + "leaktoplimit" + i, Leaktoplimit.Text);
            mesconfig.IniWriteValue("Parameters", CH + "leaklowlimit" + i, Leaklowlimit.Text);
            mesconfig.IniWriteValue("Parameters", CH + "punit" + i, PUnit.SelectedIndex.ToString());
            mesconfig.IniWriteValue("Parameters", CH + "lunit" + i, LUnit.SelectedIndex.ToString());
            mesconfig.IniWriteValue("Parameters", CH + "bee" + i, ChkBee.Checked.ToString());
            mesconfig.IniWriteValue("Parameters", CH + "unit", CHKUnit.Checked.ToString());
            mesconfig.IniWriteValue("Parameters", CH + "presscompensation", PressCompensation.Text);
            //MessageBox.Show("仪器编号：" + CH + "保存" + i + "组参数成功！");
            //Logger.Log("仪器编号：" + CH + "保存" + i + "组参数成功");
            Logger.Log(I18N.GetLangText(dicLang, "仪器参数保存成功"));
            MessageBox.Show(I18N.GetLangText(dicLang, "仪器参数保存成功"));
        }

        //读取注册表的参数编号
        private void ReadParametersNumber(int CH)
        {
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string paramnum = config.IniReadValue("Parameters", CH + "params_number");
            if (!String.IsNullOrEmpty(paramnum))
            {
                //ParaNum.Text = paramnum;
                ParaNum.SelectedIndex = Convert.ToInt32(paramnum) - 1;
                ReadParameters(CH, ParaNum.SelectedIndex + 1);
                //ReadParameters(CH, Convert.ToInt32(ParaNum.Text));
            }
        }

        //读取注册表
        private void ReadParameters(int CH, int i)
        {
            ReadConfig con = new ReadConfig();
            Model.CH_PARAMS ch_params;
            ch_params = con.ReadParameters(CH, i);

            ParaName.Text = ch_params.ParaName;
            FullTime.Text = ch_params.FullTime;
            BalanTime.Text = ch_params.BalanTime;
            TestTime1.Text = ch_params.TestTime1;
            ExhaustTime.Text = ch_params.ExhaustTime;
            DelayTime1.Text = ch_params.DelayTime1;
            DelayTime2.Text = ch_params.DelayTime2;
            RelieveDelay.Text = ch_params.RelieveDelay;
            Evolume.Text = ch_params.Evolume;
            FPtoplimit.Text = ch_params.FPtoplimit;
            FPlowlimit.Text = ch_params.FPlowlimit;
            BalanPreMax.Text = ch_params.BalanPreMax;
            BalanPreMin.Text = ch_params.BalanPreMin;
            Leaktoplimit.Text = ch_params.Leaktoplimit;
            Leaklowlimit.Text = ch_params.Leaklowlimit;
            PUnit.SelectedIndex = ch_params.PUnit_index;
            LUnit.SelectedIndex = ch_params.LUnit_index;
            ChkBee.Checked = ch_params.ChkBee;



            PressCompensation.Text = ch_params.PressCompensation;
            CHKUnit.Checked = ch_params.CHKUnit;
        }

        private void Compensation_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1IsRun.Stop();
            Form1.f1.CH2IsRun.Stop();
            Form1.f1.CH3IsRun.Stop();
            Form1.f1.CH4IsRun.Stop();
            byte[] press_comp = BitConverter.GetBytes(Convert.ToSingle(PressCompensation.Text));
            string str_presscomp = BitConverter.ToString(press_comp.Reverse().ToArray()).Replace("-", "");
            string hex_presscomp = str_presscomp.Substring(4, 4) + str_presscomp.Substring(0, 4);
            SendText.Text = "10 04 05 00 02 04";
            SendText.Text += hex_presscomp;
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    string ch1sendstr = "01 " + SendText.Text;
                    //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                    Form1.f1.ch1client.btnSendData(ch1sendstr);
                    Form1.f1.ch1stage = 10;
                    Form1.f1.CH1IsRun.Interval = 1000;
                    Form1.f1.CH1IsRun.Start();
                    Form1.f1.ch1stage = 1;
                    Form1.f1.ch1readpara = true;
                    SetPressCompen(1);
                    break;

                case 1:
                    string ch2sendstr = "02 " + SendText.Text;
                    //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                    Form1.f1.ch1client.btnSendData(ch2sendstr);
                    Form1.f1.ch2stage = 10;
                    Form1.f1.CH2IsRun.Interval = 1000;
                    Form1.f1.CH2IsRun.Start();
                    Form1.f1.ch2stage = 1;
                    Form1.f1.ch2readpara = true;
                    SetPressCompen(2);
                    break;

                case 2:
                    string ch3sendstr = "03 " + SendText.Text;
                    //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                    Form1.f1.ch3client.btnSendData(ch3sendstr);
                    Form1.f1.ch3stage = 10;
                    Form1.f1.CH3IsRun.Interval = 1000;
                    Form1.f1.CH3IsRun.Start();
                    Form1.f1.ch3stage = 1;
                    Form1.f1.ch3readpara = true;
                    SetPressCompen(3);
                    break;

                case 3:
                    string ch4sendstr = "04 " + SendText.Text;
                    //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                    Form1.f1.ch3client.btnSendData(ch4sendstr);
                    Form1.f1.ch4stage = 10;
                    Form1.f1.CH4IsRun.Interval = 1000;
                    Form1.f1.CH4IsRun.Start();
                    Form1.f1.ch4stage = 1;
                    Form1.f1.ch4readpara = true;
                    SetPressCompen(4);
                    break;
            }
        }

        private void PressureCalibration_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1IsRun.Stop();
            Form1.f1.CH2IsRun.Stop();
            Form1.f1.CH3IsRun.Stop();
            Form1.f1.CH4IsRun.Stop();
            PressCali.Interval = 400;
            PressCali.Start();
        }

        private void PressDiffCalibration_Click(object sender, EventArgs e)
        {
            Form1.f1.CH1IsRun.Stop();
            Form1.f1.CH2IsRun.Stop();
            Form1.f1.CH3IsRun.Stop();
            Form1.f1.CH4IsRun.Stop();
            PressDiffCali.Interval = 400;
            PressDiffCali.Start();
        }

        private void PressCali_Tick(object sender, EventArgs e)
        {
            PressCali.Stop();
            SendText.Text = " 05 00 05 FF 00";
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    string ch1sendstr = "01 " + SendText.Text;
                    //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                    Form1.f1.ch1client.btnSendData(ch1sendstr);
                    Form1.f1.ch1stage = 10;
                    Form1.f1.CH1IsRun.Interval = 1000;
                    Form1.f1.CH1IsRun.Start();
                    Form1.f1.ch1stage = 1;
                    Form1.f1.ch1readpara = true;
                    break;

                case 1:
                    string ch2sendstr = "02 " + SendText.Text;
                    //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                    Form1.f1.ch1client.btnSendData(ch2sendstr);
                    Form1.f1.ch2stage = 10;
                    Form1.f1.CH2IsRun.Interval = 1000;
                    Form1.f1.CH2IsRun.Start();
                    Form1.f1.ch2stage = 1;
                    Form1.f1.ch2readpara = true;
                    break;

                case 2:
                    string ch3sendstr = "03 " + SendText.Text;
                    //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                    Form1.f1.ch3client.btnSendData(ch3sendstr);
                    Form1.f1.ch3stage = 10;
                    Form1.f1.CH3IsRun.Interval = 1000;
                    Form1.f1.CH3IsRun.Start();
                    Form1.f1.ch3stage = 1;
                    Form1.f1.ch3readpara = true;
                    break;

                case 3:
                    string ch4sendstr = "04 " + SendText.Text;
                    //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                    Form1.f1.ch3client.btnSendData(ch4sendstr);
                    Form1.f1.ch4stage = 10;
                    Form1.f1.CH4IsRun.Interval = 1000;
                    Form1.f1.CH4IsRun.Start();
                    Form1.f1.ch4stage = 1;
                    Form1.f1.ch4readpara = true;
                    break;
            }
        }

        private void PressDiffCali_Tick(object sender, EventArgs e)
        {
            PressDiffCali.Stop();
            SendText.Text = " 05 00 06 FF 00";
            switch (MachineNum.SelectedIndex)
            {
                case 0:
                    string ch1sendstr = "01 " + SendText.Text;
                    //Form1.f1.left_ch1tcp.ClientSendMsgAsync(ch1sendstr);
                    Form1.f1.ch1client.btnSendData(ch1sendstr);
                    Form1.f1.ch1stage = 10;
                    Form1.f1.CH1IsRun.Interval = 1000;
                    Form1.f1.CH1IsRun.Start();
                    Form1.f1.ch1stage = 1;
                    Form1.f1.ch1readpara = true;
                    break;

                case 1:
                    string ch2sendstr = "02 " + SendText.Text;
                    //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                    Form1.f1.ch1client.btnSendData(ch2sendstr);
                    Form1.f1.ch2stage = 10;
                    Form1.f1.CH2IsRun.Interval = 1000;
                    Form1.f1.CH2IsRun.Start();
                    Form1.f1.ch2stage = 1;
                    Form1.f1.ch2readpara = true;
                    break;

                case 2:
                    string ch3sendstr = "03 " + SendText.Text;
                    //Form1.f1.right_ch1tcp.ClientSendMsgAsync(ch3sendstr);
                    Form1.f1.ch3client.btnSendData(ch3sendstr);
                    Form1.f1.ch3stage = 10;
                    Form1.f1.CH3IsRun.Interval = 1000;
                    Form1.f1.CH3IsRun.Start();
                    Form1.f1.ch3stage = 1;
                    Form1.f1.ch3readpara = true;
                    break;

                case 3:
                    string ch4sendstr = "04 " + SendText.Text;
                    //Form1.f1.right_ch2tcp.ClientSendMsgAsync(ch4sendstr);
                    Form1.f1.ch3client.btnSendData(ch4sendstr);
                    Form1.f1.ch4stage = 10;
                    Form1.f1.CH4IsRun.Interval = 1000;
                    Form1.f1.CH4IsRun.Start();
                    Form1.f1.ch4stage = 1;
                    Form1.f1.ch4readpara = true;
                    break;
            }
        }

        private void MachineNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void MachineNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ReadParametersNumber(MachineNum.SelectedIndex + 1);

            ReadParameters(MachineNum.SelectedIndex + 1, ParaNum.SelectedIndex + 1);
            ReadBc();
        }

        /// <summary>
        /// 写入压力补偿
        /// </summary>
        /// <param name="CH"></param>
        private void SetPressCompen(int CH)
        {
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Parameters", CH + "presscompensation", PressCompensation.Text);
        }

        private void CH1ElecSave_Click(object sender, EventArgs e)
        {
            Form1.f1.elec.CH1ElecMax = Convert.ToDouble(CH1ElecMax.Text);
            Form1.f1.elec.CH1ElecMin = Convert.ToDouble(CH1ElecMin.Text);
            Form1.f1.elec.CH1ElecComp = Convert.ToDouble(CH1ElecComp.Text);
            WriteElectricity();

        }

        private void CH2ElecSave_Click(object sender, EventArgs e)
        {
            Form1.f1.elec.CH2ElecMax = Convert.ToDouble(CH2ElecMax.Text);
            Form1.f1.elec.CH2ElecMin = Convert.ToDouble(CH2ElecMin.Text);
            Form1.f1.elec.CH2ElecComp = Convert.ToDouble(CH2ElecComp.Text);
            WriteElectricity();

        }


        //校验上充参数上下限
        public bool UpDate()
        {
            if (double.Parse(CH1UPADCMax.Text) < double.Parse(CH1UPADCMin.Text))
            {
                CH1UPADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1UPVDCMax.Text) < double.Parse(CH1UPVDCMin.Text))
            {
                CH1UPVDCMin.Text = "";
                return false;
            }
            if (double.Parse(CH2UPADCMax.Text) < double.Parse(CH2UPADCMin.Text))
            {
                CH2UPADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2UPVDCMax.Text) < double.Parse(CH2UPVDCMin.Text))
            {
                CH2UPVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1_2FlowMax.Text) < double.Parse(CH1_2FlowMin.Text))
            {
                CH1_2FlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1_2PreMax.Text) < double.Parse(CH1_2PreMin.Text))
            {
                CH1_2PreMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2_2FlowMax.Text) < double.Parse(CH2_2FlowMin.Text))
            {
                CH2_2FlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2_2PreMax.Text) < double.Parse(CH2_2PreMin.Text))
            {
                CH2_2PreMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1Cont_PressMax.Text) < double.Parse(CH1Cont_PressMin.Text))
            {
                CH1Cont_PressMin.Text = "";
                return false;

            }
            else if (double.Parse(CH1Cont_ElecMax.Text) < double.Parse(CH1Cont_ElecMin.Text))
            {
                CH1Cont_ElecMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2Cont_PressMax.Text) < double.Parse(CH2Cont_PressMin.Text))
            {
                CH2Cont_PressMin.Text = "";
                return false;

            }
            else if (double.Parse(CH2Cont_ElecMax.Text) < double.Parse(CH2Cont_ElecMin.Text))
            {
                CH2Cont_ElecMin.Text = "";
                return false;
            }
            else
            {
                return true;

            }



        }
        private void UPParamsSave_Click(object sender, EventArgs e)
        {
            if (UpDate())
            {


                Model.Electricity elec = Form1.f1.elec;
                elec.CH1UPADCMax = Convert.ToDouble(CH1UPADCMax.Text);
                elec.CH1UPADCMin = Convert.ToDouble(CH1UPADCMin.Text);
                elec.CH1UPADCComp = Convert.ToDouble(CH1UPADCComp.Text);
                elec.CH2UPADCMax = Convert.ToDouble(CH2UPADCMax.Text);
                elec.CH2UPADCMin = Convert.ToDouble(CH2UPADCMin.Text);
                elec.CH2UPADCComp = Convert.ToDouble(CH2UPADCComp.Text);

                elec.CH1UPVDCMax = Convert.ToDouble(CH1UPVDCMax.Text);
                elec.CH1UPVDCMin = Convert.ToDouble(CH1UPVDCMin.Text);
                elec.CH1UPVDCComp = Convert.ToDouble(CH1UPVDCComp.Text);
                elec.CH2UPVDCMax = Convert.ToDouble(CH2UPVDCMax.Text);
                elec.CH2UPVDCMin = Convert.ToDouble(CH2UPVDCMin.Text);
                elec.CH2UPVDCComp = Convert.ToDouble(CH2UPVDCComp.Text);
                Form1.f1.elec = elec;

                Model.Flow flow = Form1.f1.Flow;
                //

                flow.CH2OverTime = Convert.ToDouble(CH2OverTime.Text);
                flow.CH2Press_OverTime = Convert.ToDouble(CH2Press_OverTime.Text);
                flow.CH1_2FlowMax = Convert.ToDouble(CH1_2FlowMax.Text);
                flow.CH1_2FlowMin = Convert.ToDouble(CH1_2FlowMin.Text);
                flow.CH1_2PreMax = Convert.ToDouble(CH1_2PreMax.Text);
                flow.CH1_2PreMin = Convert.ToDouble(CH1_2PreMin.Text);
                flow.CH4OverTime = Convert.ToDouble(CH4OverTime.Text);
                flow.CH4Press_OverTime = Convert.ToDouble(CH2Press_OverTime.Text);
                flow.CH2_2FlowMax = Convert.ToDouble(CH2_2FlowMax.Text);
                flow.CH2_2FlowMin = Convert.ToDouble(CH2_2FlowMin.Text);
                flow.CH2_2PreMax = Convert.ToDouble(CH2_2PreMax.Text);
                flow.CH2_2PreMin = Convert.ToDouble(CH2_2PreMin.Text);

                flow.CH1Cont_ElecMax = Convert.ToDouble(CH1Cont_ElecMax.Text);
                flow.CH1Cont_ElecMin = Convert.ToDouble(CH1Cont_ElecMin.Text);
                flow.CH1Cont_Elec_Compen = Convert.ToDouble(CH1Cont_Elec_Compen.Text);
                flow.CH2Cont_ElecMax = Convert.ToDouble(CH2Cont_ElecMax.Text);
                flow.CH2Cont_ElecMin = Convert.ToDouble(CH2Cont_ElecMin.Text);
                flow.CH2Cont_Elec_Compen = Convert.ToDouble(CH2Cont_Elec_Compen.Text);
                flow.CH1Cont_PressMax = Convert.ToDouble(CH1Cont_PressMax.Text);
                flow.CH1Cont_PressMin = Convert.ToDouble(CH1Cont_PressMin.Text);
                flow.CH1Cont_Pre_Compen = Convert.ToDouble(CH1Cont_Pre_Compen.Text);
                flow.CH2Cont_PressMax = Convert.ToDouble(CH2Cont_PressMax.Text);
                flow.CH2Cont_PressMin = Convert.ToDouble(CH2Cont_PressMin.Text);
                flow.CH2Cont_Pre_Compen = Convert.ToDouble(CH2Cont_Pre_Compen.Text);
                Form1.f1.Flow = flow;

                WriteElectricity();
                UPParamsSave.Text = "保存成功";
            }
            else
            {
                UPParamsSave.Text = "保存失败";
            }

        }

        private void UPParamsCopy_Click(object sender, EventArgs e)
        {
            CH2UPADCMax.Text = CH1UPADCMax.Text;
            CH2UPADCMin.Text = CH1UPADCMin.Text;
            CH2UPADCComp.Text = CH1UPADCComp.Text;

            CH2UPVDCMax.Text = CH1UPVDCMax.Text;
            CH2UPVDCMin.Text = CH1UPVDCMin.Text;
            CH2UPVDCComp.Text = CH1UPVDCComp.Text;

            CH3OverTime.Text = CH1OverTime.Text;
            CH3Press_OverTime.Text = CH1Press_OverTime.Text;
            CH2_1FlowMax.Text = CH1_1FlowMax.Text;
            CH2_1FlowMin.Text = CH1_1FlowMin.Text;
            CH2_1PreMax.Text = CH1_1PreMax.Text;
            CH2_1PreMin.Text = CH1_1PreMin.Text;

            CH2Cont_ElecMax.Text = CH1Cont_ElecMax.Text;
            CH2Cont_ElecMin.Text = CH1Cont_ElecMin.Text;
            CH2Cont_Elec_Compen.Text = CH1Cont_Elec_Compen.Text;
            CH2Cont_PressMax.Text = CH1Cont_PressMax.Text;
            CH2Cont_PressMin.Text = CH1Cont_PressMin.Text;
            CH2Cont_Pre_Compen.Text = CH1Cont_Pre_Compen.Text;
        }

        private void RUPParamsCopy_Click(object sender, EventArgs e)
        {
            CH1UPADCMax.Text = CH2UPADCMax.Text;
            CH1UPADCMin.Text = CH2UPADCMin.Text;
            CH1UPADCComp.Text = CH2UPADCComp.Text;

            CH1UPVDCMax.Text = CH2UPVDCMax.Text;
            CH1UPVDCMin.Text = CH2UPVDCMin.Text;
            CH1UPVDCComp.Text = CH2UPVDCComp.Text;

            CH1OverTime.Text = CH3OverTime.Text;
            CH1Press_OverTime.Text = CH3Press_OverTime.Text;
            CH1_1FlowMax.Text = CH2_1FlowMax.Text;
            CH1_1FlowMin.Text = CH2_1FlowMin.Text;
            CH1_1PreMax.Text = CH2_1PreMax.Text;
            CH1_1PreMin.Text = CH2_1PreMin.Text;

            CH1Cont_ElecMax.Text = CH2Cont_ElecMax.Text;
            CH1Cont_ElecMin.Text = CH2Cont_ElecMin.Text;
            CH1Cont_Elec_Compen.Text = CH2Cont_Elec_Compen.Text;
            CH1Cont_PressMax.Text = CH2Cont_PressMax.Text;
            CH1Cont_PressMin.Text = CH2Cont_PressMin.Text;
            CH1Cont_Pre_Compen.Text = CH2Cont_Pre_Compen.Text;
        }
        //下充参数校验
        public bool DownDate()
        {
            if (double.Parse(CH1DOWNADCMax.Text) < double.Parse(CH1DOWNADCMin.Text))
            {
                CH1DOWNADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1DOWNVDCMax.Text) < double.Parse(CH1DOWNVDCMin.Text))
            {
                CH1DOWNVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2DOWNADCMax.Text) < double.Parse(CH2DOWNADCMin.Text))
            {
                CH2DOWNADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2DOWNVDCMax.Text) < double.Parse(CH2DOWNVDCMin.Text))
            {
                CH2DOWNVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1_1FlowMax.Text) < double.Parse(CH1_1FlowMin.Text))
            {
                CH1_1FlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1_1PreMax.Text) < double.Parse(CH1_1PreMin.Text))
            {
                CH1_1PreMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2_1FlowMax.Text) < double.Parse(CH2_1FlowMin.Text))
            {
                CH2_1FlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2_1PreMax.Text) < double.Parse(CH2_1PreMin.Text))
            {
                CH2_1PreMin.Text = "";
                return false;
            }
            else
            {
                return true;
            }

        }

        private void DOWNParamsSave_Click(object sender, EventArgs e)
        {

            if (DownDate())
            {

                Model.Electricity elec = Form1.f1.elec;
                elec.CH1DOWNADCMax = Convert.ToDouble(CH1DOWNADCMax.Text);
                elec.CH1DOWNADCMin = Convert.ToDouble(CH1DOWNADCMin.Text);
                elec.CH1DOWNADCComp = Convert.ToDouble(CH1DOWNADCComp.Text);
                elec.CH2DOWNADCMax = Convert.ToDouble(CH2DOWNADCMax.Text);
                elec.CH2DOWNADCMin = Convert.ToDouble(CH2DOWNADCMin.Text);
                elec.CH2DOWNADCComp = Convert.ToDouble(CH2DOWNADCComp.Text);

                elec.CH1DOWNVDCComp = Convert.ToDouble(CH1DOWNVDCComp.Text);
                elec.CH2DOWNVDCMax = Convert.ToDouble(CH2DOWNVDCMax.Text);
                elec.CH2DOWNVDCMin = Convert.ToDouble(CH2DOWNVDCMin.Text);
                elec.CH2DOWNVDCComp = Convert.ToDouble(CH2DOWNVDCComp.Text);
                Form1.f1.elec = elec;

                Model.Flow flow = Form1.f1.Flow;

                //
                flow.CH1OverTime = Convert.ToDouble(CH1OverTime.Text);
                flow.CH1Press_OverTime = Convert.ToDouble(CH1Press_OverTime.Text);
                flow.CH1_1FlowMax = Convert.ToDouble(CH1_1FlowMax.Text);
                flow.CH1_1FlowMin = Convert.ToDouble(CH1_1FlowMin.Text);
                flow.CH1_1PreMax = Convert.ToDouble(CH1_1PreMax.Text);
                flow.CH1_1PreMin = Convert.ToDouble(CH1_1PreMin.Text);
                flow.CH3OverTime = Convert.ToDouble(CH3OverTime.Text);
                flow.CH3Press_OverTime = Convert.ToDouble(CH3Press_OverTime.Text);
                flow.CH2_1FlowMax = Convert.ToDouble(CH2_1FlowMax.Text);
                flow.CH2_1FlowMin = Convert.ToDouble(CH2_1FlowMin.Text);
                flow.CH2_1PreMax = Convert.ToDouble(CH2_1PreMax.Text);
                flow.CH2_1PreMin = Convert.ToDouble(CH2_1PreMin.Text);

                Form1.f1.Flow = flow;

                WriteElectricity();
                DOWNParamsSave.Text = "保存成功";
            }
            else
            {
                DOWNParamsSave.Text = "保存失败";
            }

        }

        private void LDOWNParamsCopy_Click(object sender, EventArgs e)
        {
            CH2DOWNADCMax.Text = CH1DOWNADCMax.Text;
            CH2DOWNADCMin.Text = CH1DOWNADCMin.Text;
            CH2DOWNADCComp.Text = CH1DOWNADCComp.Text;

            CH2DOWNVDCMax.Text = CH1DOWNVDCMax.Text;
            CH2DOWNVDCMin.Text = CH1DOWNVDCMin.Text;
            CH2DOWNVDCComp.Text = CH1DOWNVDCComp.Text;

            CH4OverTime.Text = CH2OverTime.Text;
            CH4Press_OverTime.Text = CH2Press_OverTime.Text;
            CH2_2FlowMax.Text = CH1_2FlowMax.Text;
            CH2_2FlowMin.Text = CH1_2FlowMin.Text;
            CH2_2PreMax.Text = CH1_2PreMax.Text;
            CH2_2PreMin.Text = CH1_2PreMin.Text;

          

            CH3OverTime.Text = CH1OverTime.Text;
            CH3Press_OverTime.Text = CH1Press_OverTime.Text;
            CH2_1FlowMax.Text = CH1_1FlowMax.Text;
            CH2_1FlowMin.Text = CH1_1FlowMin.Text;
            CH2_1PreMax.Text = CH1_1PreMax.Text;
            CH2_1PreMin.Text = CH1_1PreMin.Text;
        }

        private void RDOWNParamsCopy_Click(object sender, EventArgs e)
        {
            CH1DOWNADCMax.Text = CH2DOWNADCMax.Text;
            CH1DOWNADCMin.Text = CH2DOWNADCMin.Text;
            CH1DOWNADCComp.Text = CH2DOWNADCComp.Text;

            CH1DOWNVDCMax.Text = CH2DOWNVDCMax.Text;
            CH1DOWNVDCMin.Text = CH2DOWNVDCMin.Text;
            CH1DOWNVDCComp.Text = CH2DOWNVDCComp.Text;

            CH2OverTime.Text = CH4OverTime.Text;
            CH2Press_OverTime.Text = CH4Press_OverTime.Text;
            CH1_2FlowMax.Text = CH2_2FlowMax.Text;
            CH1_2FlowMin.Text = CH2_2FlowMin.Text;
            CH1_2PreMax.Text = CH2_2PreMax.Text;
            CH1_2PreMin.Text = CH2_2PreMin.Text;


            CH1OverTime.Text = CH3OverTime.Text;
            CH1Press_OverTime.Text = CH3Press_OverTime.Text;
            CH1_1FlowMax.Text = CH2_1FlowMax.Text;
            CH1_1FlowMin.Text = CH2_1FlowMin.Text;
            CH1_1PreMax.Text = CH2_1PreMax.Text;
            CH1_1PreMin.Text = CH2_1PreMin.Text;


        }
        //同充参数校验
        public bool FwdDate()
        {
            if (double.Parse(CH1FWDADCMax.Text) < double.Parse(CH1FWDADCMin.Text))
            {
                CH1FWDADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1FWDVDCMax.Text) < double.Parse(CH1FWDVDCMin.Text))
            {
                CH1FWDVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2FWDADCMax.Text) < double.Parse(CH2FWDADCMin.Text))
            {
                CH2FWDADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2FWDVDCMax.Text) < double.Parse(CH2FWDVDCMin.Text))
            {
                CH2FWDVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1FWDFlowMax.Text) < double.Parse(CH1FWDFlowMin.Text))
            {
                CH1FWDFlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1FwdPreMax.Text) < double.Parse(CH1FwdPreMin.Text))
            {

                CH1FwdPreMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2FWDFlowMax.Text) < double.Parse(CH2FWDFlowMin.Text))
            {
                CH2FWDFlowMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2FWDPreMax.Text) < double.Parse(CH2FWDPreMin.Text))
            {
                CH2FWDPreMin.Text = "";
                return false;
            }
            else
            {
                return true;
            }


        }
        private void FWDParamsSave_Click(object sender, EventArgs e)
        {
            // if (FwdDate())
            {
                Model.Electricity elec = Form1.f1.elec;
 

                //新增
                elec.TotalFlowMax = Convert.ToDouble(TotalFlowMax.Text);
                elec.TotalFlowMin = Convert.ToDouble(TotalFlowMin.Text);
                elec.TotalPreMax = Convert.ToDouble(TotalPreMax.Text);

                elec.CH1FwdpreTime = Convert.ToDouble(CH1FwdPreTime.Text);
                elec.CH2FwdpreTime = Convert.ToDouble(CH2FwdpreTime.Text);

                elec.CH1FWDFlowTime = Convert.ToDouble(CH1FWDFLOWTime.Text);
                elec.CH2FWDFlowTime = Convert.ToDouble(CH2FWDflowtime.Text);

                elec.CH1FwdFlowMax = Convert.ToDouble(CH1FWDFlowMax.Text);
                elec.CH2FwdFlowMax = Convert.ToDouble(CH2FWDFlowMax.Text);

                //elec.CH1FwdFlowMin = Convert.ToDouble(CH1_1FlowMin.Text);
                //elec.CH2FwdFlowMin = Convert.ToDouble(CH2_2FlowMin.Text);
                elec.CH1FwdFlowMin = Convert.ToDouble(CH1FWDFlowMin.Text);
                elec.CH2FwdFlowMin = Convert.ToDouble(CH2FWDFlowMin.Text);

                elec.CH1FwdPreMax = Convert.ToDouble(CH1FwdPreMax.Text);
                elec.CH2FwdPreMax = Convert.ToDouble(CH2FWDPreMax.Text);

                elec.CH1FwdPreMin = Convert.ToDouble(CH1FwdPreMin.Text);
                elec.CH2FwdPreMin = Convert.ToDouble(CH2FWDPreMin.Text);




                elec.CH1FWDADCMax = Convert.ToDouble(CH1FWDADCMax.Text);
                elec.CH1FWDADCMin = Convert.ToDouble(CH1FWDADCMin.Text);
                elec.CH1FWDADCComp = Convert.ToDouble(CH1FWDADCComp.Text);
                elec.CH2FWDADCMax = Convert.ToDouble(CH2FWDADCMax.Text);
                elec.CH2FWDADCMin = Convert.ToDouble(CH2FWDADCMin.Text);
                elec.CH2FWDADCComp = Convert.ToDouble(CH2FWDADCComp.Text);

                elec.CH1FWDVDCMax = Convert.ToDouble(CH1FWDVDCMax.Text);
                elec.CH1FWDVDCMin = Convert.ToDouble(CH1FWDVDCMin.Text);
                elec.CH1FWDVDCComp = Convert.ToDouble(CH1FWDVDCComp.Text);
                elec.CH2FWDVDCMax = Convert.ToDouble(CH2FWDVDCMax.Text);
                elec.CH2FWDVDCMin = Convert.ToDouble(CH2FWDVDCMin.Text);
                elec.CH2FWDVDCComp = Convert.ToDouble(CH2FWDVDCComp.Text);




          
                Form1.f1.elec = elec;

                WriteElectricity();
                FWDParamsSave.Text = "保存成功";
            }
            //   else
            {
                //    FWDParamsSave.Text = "保存失败";
            }

        }

        private void LFWDParamsCopy_Click(object sender, EventArgs e)
        {
            CH2FWDADCMax.Text = CH1FWDADCMax.Text;
            CH2FWDADCMin.Text = CH1FWDADCMin.Text;
            CH2FWDADCComp.Text = CH1FWDADCComp.Text;

            CH2FWDVDCMax.Text = CH1FWDVDCMax.Text;
            CH2FWDVDCMin.Text = CH1FWDVDCMin.Text;
            CH2FWDVDCComp.Text = CH1FWDVDCComp.Text;
        }

        private void RFWDParamsCopy_Click(object sender, EventArgs e)
        {
            CH1FWDADCMax.Text = CH2FWDADCMax.Text;
            CH1FWDADCMin.Text = CH2FWDADCMin.Text;
            CH1FWDADCComp.Text = CH2FWDADCComp.Text;

            CH1FWDVDCMax.Text = CH2FWDVDCMax.Text;
            CH1FWDVDCMin.Text = CH2FWDVDCMin.Text;
            CH1FWDVDCComp.Text = CH2FWDVDCComp.Text;
        }

        //泄气参数校验
        public bool RwdDate()
        {
            if (double.Parse(CH1RWDADCMax.Text) < double.Parse(CH1RWDADCMin.Text))
            {
                CH1RWDADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1RWDVDCMax.Text) < double.Parse(CH1RWDVDCMin.Text))
            {
                CH1RWDVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2RWDADCMax.Text) < double.Parse(CH2RWDADCMin.Text))
            {
                CH2RWDADCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2RWDVDCMax.Text) < double.Parse(CH2RWDVDCMin.Text))
            {
                CH2RWDVDCMin.Text = "";
                return false;
            }
            else if (double.Parse(CH1RWDPressMax.Text) < double.Parse(CH1RWDPressMin.Text))
            {
                CH1RWDPressMin.Text = "";
                return false;
            }
            else if (double.Parse(CH2RWDPressMax.Text) < double.Parse(CH2RWDPressMin.Text))
            {
                CH2RWDPressMin.Text = "";
                return false;
            }
            else
            {
                return true;
            }

        }

        private void RWDParamsSave_Click(object sender, EventArgs e)
        {
            if (RwdDate())
            {


                Model.Electricity elec = Form1.f1.elec;
                elec.CH1RWDADCMax = Convert.ToDouble(CH1RWDADCMax.Text);
                elec.CH1RWDADCMin = Convert.ToDouble(CH1RWDADCMin.Text);
                elec.CH1RWDADCComp = Convert.ToDouble(CH1RWDADCComp.Text);
                elec.CH2RWDADCMax = Convert.ToDouble(CH2RWDADCMax.Text);
                elec.CH2RWDADCMin = Convert.ToDouble(CH2RWDADCMin.Text);
                elec.CH2RWDADCComp = Convert.ToDouble(CH2RWDADCComp.Text);

                elec.CH1RWDVDCMax = Convert.ToDouble(CH1RWDVDCMax.Text);
                elec.CH1RWDVDCMin = Convert.ToDouble(CH1RWDVDCMin.Text);
                elec.CH1RWDVDCComp = Convert.ToDouble(CH1RWDVDCComp.Text);
                elec.CH2RWDVDCMax = Convert.ToDouble(CH2RWDVDCMax.Text);
                elec.CH2RWDVDCMin = Convert.ToDouble(CH2RWDVDCMin.Text);
                elec.CH2RWDVDCComp = Convert.ToDouble(CH2RWDVDCComp.Text);
                Form1.f1.elec = elec;

                Model.Flow flow = Form1.f1.Flow;
                flow.CH1RWDPressMax = Convert.ToDouble(CH1RWDPressMax.Text);
                flow.CH2RWDPressMax = Convert.ToDouble(CH2RWDPressMax.Text);
                flow.CH1RWDPressMin = Convert.ToDouble(CH1RWDPressMin.Text);
                flow.CH2RWDPressMin = Convert.ToDouble(CH2RWDPressMin.Text);
                flow.CH1RWDOverTime = Convert.ToDouble(CH1RWDOverTime.Text);
                flow.CH2RWDOverTime = Convert.ToDouble(CH2RWDOverTime.Text);
                Form1.f1.Flow = flow;

                WriteElectricity();
                RWDParamsSave.Text = "保存成功";
            }
            else
            {
                RWDParamsSave.Text = "保存失败";
            }
        }

        private void LRWDParamsCopy_Click(object sender, EventArgs e)
        {
            CH2RWDADCMax.Text = CH1RWDADCMax.Text;
            CH2RWDADCMin.Text = CH1RWDADCMin.Text;
            CH2RWDADCComp.Text = CH1RWDADCComp.Text;

            CH2RWDVDCMax.Text = CH1RWDVDCMax.Text;
            CH2RWDVDCMin.Text = CH1RWDVDCMin.Text;
            CH2RWDVDCComp.Text = CH1RWDVDCComp.Text;

            CH2RWDPressMax.Text = CH1RWDPressMax.Text;
            CH2RWDPressMin.Text = CH1RWDPressMin.Text;
            CH2RWDOverTime.Text = CH1RWDOverTime.Text;
        }

        private void PRWDParamsCopy_Click(object sender, EventArgs e)
        {
            CH1RWDADCMax.Text = CH2RWDADCMax.Text;
            CH1RWDADCMin.Text = CH2RWDADCMin.Text;
            CH1RWDADCComp.Text = CH2RWDADCComp.Text;

            CH1RWDVDCMax.Text = CH2RWDVDCMax.Text;
            CH1RWDVDCMin.Text = CH2RWDVDCMin.Text;
            CH1RWDVDCComp.Text = CH2RWDVDCComp.Text;

            CH1RWDPressMax.Text = CH2RWDPressMax.Text;
            CH1RWDPressMin.Text = CH2RWDPressMin.Text;
            CH1RWDOverTime.Text = CH2RWDOverTime.Text;
        }

        private void uiSymbolButton11_Click(object sender, EventArgs e)
        {
            int para = ParaNum.SelectedIndex + 1;
            //if (ParaNum.Text == "1")
            if (para == 1)
            {
                SetParameters(1, 1);
                SetParameters(2, 1);
                SetParameters(3, 1);
                SetParameters(4, 1);
                //MessageBox.Show("仪器的第一组参数设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "仪器的第一组参数设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "仪器的第一组参数设置成功"));
            }
            else if (para == 2)
            {
                SetParameters(1, 2);
                SetParameters(2, 2);
                SetParameters(3, 2);
                SetParameters(4, 2);
                //MessageBox.Show("仪器的第二组参数设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "仪器的第二组参数设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "仪器的第二组参数设置成功"));
            }
            else if (para == 3)
            {
                SetParameters(1, 3);
                SetParameters(2, 3);
                SetParameters(3, 3);
                SetParameters(4, 3);
                //MessageBox.Show("仪器的第三组参数设置成功！");8
                Logger.Log(I18N.GetLangText(dicLang, "仪器的第三组参数设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "仪器的第三组参数设置成功"));
            }
            else if (para == 4 || para == 6 || para == 8)
            {
                SetParameters(MachineNum.SelectedIndex + 1, 4);
                SetParameters(MachineNum.SelectedIndex + 1, 6);
                SetParameters(MachineNum.SelectedIndex + 1, 8);
                //MessageBox.Show("仪器的第四组参数设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "仪器的第四组参数设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "仪器的第四组参数设置成功"));
            }
            else if (para == 5 || para == 7 || para == 9)
            {
                SetParameters(MachineNum.SelectedIndex + 1, 5);
                SetParameters(MachineNum.SelectedIndex + 1, 7);
                SetParameters(MachineNum.SelectedIndex + 1, 9);
                //MessageBox.Show("仪器的第五组参数设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "仪器的第五组参数设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "仪器的第五组参数设置成功"));
            }
        }

        private void LParametersCopy_Click(object sender, EventArgs e)
        {
            int para = ParaNum.SelectedIndex + 1;
            if (para == 1)
            {
                SetParameters(1, 1);
                SetParameters(2, 1);
                //SetParameters(3, 1);
                //SetParameters(4, 1);
                //SetParameters(5, 1);
                //MessageBox.Show("左工位的第一组参数(体积参数)设置成功");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第一组参数(体积参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第一组参数(体积参数)设置成功"));
            }
            else if (para == 2)
            {
                SetParameters(1, 2);
                SetParameters(2, 2);
                //SetParameters(3, 2);
                //SetParameters(4, 2);
                //SetParameters(5, 2);
                //MessageBox.Show("左工位的第二组参数(气密参数)设置成功");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第二组参数(气密参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第二组参数(气密参数)设置成功"));
            }
            else if (para == 3)
            {
                SetParameters(1, 3);
                SetParameters(2, 3);
                //SetParameters(3, 3);
                //SetParameters(4, 3);
                //SetParameters(5, 3);
                //MessageBox.Show("左工位的第三组参数(气密参数)设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第三组参数(气密参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第三组参数(气密参数)设置成功"));
            }
        }

        private void RParametersCopy_Click(object sender, EventArgs e)
        {
            int para = ParaNum.SelectedIndex + 1;
            if (para == 1)
            {
                SetParameters(3, 1);
                SetParameters(4, 1);
                //SetParameters(8, 1);
                //SetParameters(9, 1);
                //SetParameters(10, 1);
                // MessageBox.Show("右工位的第一组参数(体积参数)设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第一组参数(体积参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第一组参数(体积参数)设置成功"));
            }
            else if (para == 2)
            {
                SetParameters(3, 2);
                SetParameters(4, 2);
                //SetParameters(8, 2);
                //SetParameters(9, 2);
                //SetParameters(10, 2);
                //MessageBox.Show("右工位的第二组参数(气密参数)设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第二组参数(气密参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第二组参数(气密参数)设置成功"));
            }
            else if (para == 3)
            {
                SetParameters(3, 3);
                SetParameters(4, 3);
                //SetParameters(8, 3);
                //SetParameters(9, 3);
                //SetParameters(10, 3);
                //MessageBox.Show("左工位的第二组参数(气密参数)设置成功！");
                Logger.Log(I18N.GetLangText(dicLang, "左工位的第三组参数(气密参数)设置成功"));
                MessageBox.Show(I18N.GetLangText(dicLang, "左工位的第三组参数(气密参数)设置成功"));
            }
        }

        //CH1高电平屏蔽勾选，无勾选则为低电平
        private void CH1HighLevel_Click(object sender, EventArgs e)
        {
        }

        //CH1IGN勾选
        private void CH1IGN_Click(object sender, EventArgs e)
        {
        }

        //CH1上充勾选
        private void CH1UP_Click(object sender, EventArgs e)
        {
        }

        //CH1下充勾选
        private void CH1DOWN_Click(object sender, EventArgs e)
        {
        }

        //CH1同充勾选
        private void CH1FWD_Click(object sender, EventArgs e)
        {
        }

        //CH1LIN勾选
        private void CH1LIN_Click(object sender, EventArgs e)
        {
        }

        //CH2高电平屏蔽勾选，无勾选则为低电平
        private void CH2HighLevel_Click(object sender, EventArgs e)
        {
        }

        //CH2IGN勾选
        private void CH2IGN_Click(object sender, EventArgs e)
        {
        }

        //CH2上充勾选
        private void CH2UP_Click(object sender, EventArgs e)
        {
        }

        //CH2下充勾选
        private void CH2DOWN_Click(object sender, EventArgs e)
        {
        }

        //CH2同充勾选
        private void CH2FWD_Click(object sender, EventArgs e)
        {
        }

        //CH2LIN勾选
        private void CH2LIN_Click(object sender, EventArgs e)
        {
        }

        private void CH1UPLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH1DOWNLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH1FWDLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH1Pump_Click(object sender, EventArgs e)
        {
        }

        private void CH2UPLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH2DOWNLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH2FWDLeak_Click(object sender, EventArgs e)
        {
        }

        private void CH2Pump_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 更改后刷新程序的步骤
        /// </summary>
        private void RefreshIndex()
        {
            if (CH1UP.Checked)
            {
                CH1UPindex.Text = ((CH1Order.IndexOf("UP") == -1 ? 0 : CH1Order.IndexOf("UP")) + 1).ToString();// (CH1Order.IndexOf("UP") + 1).ToString();
            }
            else
            {
                CH1UPindex.Text = "";
            }
            if (CH1DOWN.Checked)
            {
                CH1DOWNindex.Text = ((CH1Order.IndexOf("DOWN") == -1 ? 0 : CH1Order.IndexOf("DOWN")) + 1).ToString();// (CH1Order.IndexOf("DOWN") + 1).ToString();
            }
            else
            {
                CH1DOWNindex.Text = "";
            }
            if (CH1FWD.Checked)
            {
                CH1FWDindex.Text = ((CH1Order.IndexOf("FWD") == -1 ? 0 : CH1Order.IndexOf("FWD")) + 1).ToString();// (CH1Order.IndexOf("FWD") + 1).ToString();
            }
            else
            {
                CH1FWDindex.Text = "";
            }
            if (CH1RWD.Checked)
            {
                CH1RWDindex.Text = ((CH1Order.IndexOf("RWD") == -1 ? 0 : CH1Order.IndexOf("RWD")) + 1).ToString(); //(CH1Order.IndexOf("RWD") + 1).ToString();
            }
            else
            {
                CH1RWDindex.Text = "";
            }
            if (CH1UPLeak.Checked)
            {
                CH1UPLeakindex.Text = ((CH1Order.IndexOf("UPLeak") == -1 ? 0 : CH1Order.IndexOf("UPLeak")) + 1).ToString(); //(CH1Order.IndexOf("UPLeak") + 1).ToString();
            }
            else
            {
                CH1UPLeakindex.Text = "";
            }
            if (CH1DOWNLeak.Checked)
            {
                CH1DOWNLeakindex.Text = ((CH1Order.IndexOf("DOWNLeak") == -1 ? 0 : CH1Order.IndexOf("DOWNLeak")) + 1).ToString(); //(CH1Order.IndexOf("DOWNLeak") + 1).ToString();
            }
            else
            {
                CH1DOWNLeakindex.Text = "";
            }
            if (CH1FWDLeak.Checked)
            {
                CH1FWDLeakindex.Text = ((CH1Order.IndexOf("FWDLeak") == -1 ? 0 : CH1Order.IndexOf("FWDLeak")) + 1).ToString();// (CH1Order.IndexOf("FWDLeak") + 1).ToString();
            }
            else
            {
                CH1FWDLeakindex.Text = "";
            }
            //CH1静态电流
            if (CH1QuiescentCurrnt.Checked)
            {
                CH1QuiescentCurrntIndex.Text = ((CH1Order.IndexOf("QC") == -1 ? 0 : CH1Order.IndexOf("QC")) + 1).ToString();// (CH1Order.IndexOf("QC") + 1).ToString();
            }
            else
            {
                CH1QuiescentCurrntIndex.Text = "";
            }
            if (CH2UP.Checked)
            {
                CH2UPindex.Text = ((CH2Order.IndexOf("UP") == -1 ? 0 : CH2Order.IndexOf("UP")) + 1).ToString(); //(CH2Order.IndexOf("UP") + 1).ToString();
            }
            else
            {
                CH2UPindex.Text = "";
            }
            if (CH2DOWN.Checked)
            {
                CH2DOWNindex.Text = ((CH2Order.IndexOf("DOWN") == -1 ? 0 : CH2Order.IndexOf("DOWN")) + 1).ToString(); //(CH2Order.IndexOf("DOWN") + 1).ToString();
            }
            else
            {
                CH2DOWNindex.Text = "";
            }
            if (CH2FWD.Checked)
            {
                CH2FWDindex.Text = ((CH2Order.IndexOf("FWD") == -1 ? 0 : CH2Order.IndexOf("FWD")) + 1).ToString(); //(CH2Order.IndexOf("FWD") + 1).ToString();
            }
            else
            {
                CH2FWDindex.Text = "";
            }
            if (CH2RWD.Checked)
            {
                CH2RWDindex.Text = ((CH2Order.IndexOf("RWD") == -1 ? 0 : CH2Order.IndexOf("RWD")) + 1).ToString(); //(CH2Order.IndexOf("RWD") + 1).ToString();
            }
            else
            {
                CH2RWDindex.Text = "";
            }
            if (CH2UPLeak.Checked)
            {
                CH2UPLeakindex.Text = ((CH2Order.IndexOf("UPLeak") == -1 ? 0 : CH2Order.IndexOf("UPLeak")) + 1).ToString();// (CH2Order.IndexOf("UPLeak") + 1).ToString();
            }
            else
            {
                CH2UPLeakindex.Text = "";
            }
            if (CH2DOWNLeak.Checked)
            {
                CH2DOWNLeakindex.Text = ((CH2Order.IndexOf("DOWNLeak") == -1 ? 0 : CH2Order.IndexOf("DOWNLeak")) + 1).ToString();// (CH2Order.IndexOf("DOWNLeak") + 1).ToString();
            }
            else
            {
                CH2DOWNLeakindex.Text = "";
            }
            if (CH2FWDLeak.Checked)
            {
                CH2FWDLeakindex.Text = ((CH2Order.IndexOf("FWDLeak") == -1 ? 0 : CH2Order.IndexOf("FWDLeak")) + 1).ToString();// (CH2Order.IndexOf("FWDLeak") + 1).ToString();
            }
            else
            {
                CH2FWDLeakindex.Text = "";
            }

            //CH2静态电流
            if (CH2QuiescentCurrnt.Checked)
            {
                CH2QuiescentCurrntIndex.Text = ((CH2Order.IndexOf("QC") == -1 ? 0 : CH2Order.IndexOf("QC")) + 1).ToString();// (CH2Order.IndexOf("QC") + 1).ToString();
            }
            else
            {
                CH2QuiescentCurrntIndex.Text = "";
            }

            //AD
            if (ADUP.Checked)
            {
                ADUPindex.Text = ((ADOrder.IndexOf("UP") == -1 ? 0 : ADOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                ADUPindex.Text = "";
            }
            if (ADDOWN.Checked)
            {
                ADDOWNindex.Text = ((ADOrder.IndexOf("DOWN") == -1 ? 0 : ADOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                ADDOWNindex.Text = "";
            }
            if (ADFWD.Checked)
            {
                ADFWDindex.Text = ((ADOrder.IndexOf("FWD") == -1 ? 0 : ADOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                ADFWDindex.Text = "";
            }
            if (ADRWD.Checked)
            {
                ADRWDindex.Text = ((ADOrder.IndexOf("RWD") == -1 ? 0 : ADOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                ADRWDindex.Text = "";
            }
            if (ADUPLeak.Checked)
            {
                ADUPLeakindex.Text = ((ADOrder.IndexOf("UPLeak") == -1 ? 0 : ADOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                ADUPLeakindex.Text = "";
            }
            if (ADDOWNLeak.Checked)
            {
                ADDOWNLeakindex.Text = ((ADOrder.IndexOf("DOWNLeak") == -1 ? 0 : ADOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                ADDOWNLeakindex.Text = "";
            }
            if (ADFWDLeak.Checked)
            {
                ADFWDLeakindex.Text = ((ADOrder.IndexOf("FWDLeak") == -1 ? 0 : ADOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                ADFWDLeakindex.Text = "";
            }
            if (ADQuiescentCurrnt.Checked)
            {
                ADQuiescentCurrntIndex.Text = ((ADOrder.IndexOf("QC") == -1 ? 0 : ADOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                ADQuiescentCurrntIndex.Text = "";
            }
            //BE
            if (BEUP.Checked)
            {
                BEUPindex.Text = ((BEOrder.IndexOf("UP") == -1 ? 0 : BEOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                BEUPindex.Text = "";
            }
            if (BEDOWN.Checked)
            {
                BEDOWNindex.Text = ((BEOrder.IndexOf("DOWN") == -1 ? 0 : BEOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                BEDOWNindex.Text = "";
            }
            if (BEFWD.Checked)
            {
                BEFWDindex.Text = ((BEOrder.IndexOf("FWD") == -1 ? 0 : BEOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                BEFWDindex.Text = "";
            }
            if (BERWD.Checked)
            {
                BERWDindex.Text = ((BEOrder.IndexOf("RWD") == -1 ? 0 : BEOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                BERWDindex.Text = "";
            }
            if (BEUPLeak.Checked)
            {
                BEUPLeakindex.Text = ((BEOrder.IndexOf("UPLeak") == -1 ? 0 : BEOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                BEUPLeakindex.Text = "";
            }
            if (BEDOWNLeak.Checked)
            {
                BEDOWNLeakindex.Text = ((BEOrder.IndexOf("DOWNLeak") == -1 ? 0 : BEOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                BEDOWNLeakindex.Text = "";
            }
            if (BEFWDLeak.Checked)
            {
                BEFWDLeakindex.Text = ((BEOrder.IndexOf("FWDLeak") == -1 ? 0 : BEOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                BEFWDLeakindex.Text = "";
            }
            if (BEQuiescentCurrnt.Checked)
            {
                BEQuiescentCurrntIndex.Text = ((BEOrder.IndexOf("QC") == -1 ? 0 : BEOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                BEQuiescentCurrntIndex.Text = "";
            }
            //CF
            if (CFUP.Checked)
            {
                CFUPindex.Text = ((CFOrder.IndexOf("UP") == -1 ? 0 : CFOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                CFUPindex.Text = "";
            }
            if (CFDOWN.Checked)
            {
                CFDOWNindex.Text = ((CFOrder.IndexOf("DOWN") == -1 ? 0 : CFOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                CFDOWNindex.Text = "";
            }
            if (CFFWD.Checked)
            {
                CFFWDindex.Text = ((CFOrder.IndexOf("FWD") == -1 ? 0 : CFOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                CFFWDindex.Text = "";
            }
            if (CFRWD.Checked)
            {
                CFRWDindex.Text = ((CFOrder.IndexOf("RWD") == -1 ? 0 : CFOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                CFRWDindex.Text = "";
            }
            if (CFUPLeak.Checked)
            {
                CFUPLeakindex.Text = ((CFOrder.IndexOf("UPLeak") == -1 ? 0 : CFOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                CFUPLeakindex.Text = "";
            }
            if (CFDOWNLeak.Checked)
            {
                CFDOWNLeakindex.Text = ((CFOrder.IndexOf("DOWNLeak") == -1 ? 0 : CFOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                CFDOWNLeakindex.Text = "";
            }
            if (CFFWDLeak.Checked)
            {
                CFFWDLeakindex.Text = ((CFOrder.IndexOf("FWDLeak") == -1 ? 0 : CFOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                CFFWDLeakindex.Text = "";
            }
            if (CFQuiescentCurrnt.Checked)
            {
                CFQuiescentCurrntIndex.Text = ((CFOrder.IndexOf("QC") == -1 ? 0 : CFOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                CFQuiescentCurrntIndex.Text = "";
            }
        }


        /// <summary>
        /// 测试流程步骤和Lin勾选
        /// </summary>
        private void WriteLin()
        {
            //string dialog = "Lin.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;

            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("Lin", "CH1HighLevel", CH1HighLevel.Checked.ToString());
            config.IniWriteValue("Lin", "CH2HighLevel", CH2HighLevel.Checked.ToString());
            config.IniWriteValue("Lin", "CH2LIN", CH2LIN.Checked.ToString());
            config.IniWriteValue("Lin", "CH1LIN", CH1LIN.Checked.ToString());
            config.IniWriteValue("Lin", "CH1UpDownChange", CH1UpDownChange.Checked.ToString());
            config.IniWriteValue("Lin", "CH2UpDownChange", CH2UpDownChange.Checked.ToString());

            config.IniWriteValue("Lin", "CH1IGN", CH1IGN.Checked.ToString());
            config.IniWriteValue("Lin", "CH2IGN", CH2IGN.Checked.ToString());


            config.IniWriteValue("Lin", "CH1UP", CH1UP.Checked.ToString());
            config.IniWriteValue("Lin", "CH1DOWN", CH1DOWN.Checked.ToString());
            config.IniWriteValue("Lin", "CH1FWD", CH1FWD.Checked.ToString());
            config.IniWriteValue("Lin", "CH1RWD", CH1RWD.Checked.ToString());
            config.IniWriteValue("Lin", "CH1LIN", CH1LIN.Checked.ToString());
            config.IniWriteValue("Lin", "CH1UPLeak", CH1UPLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CH1DOWNLeak", CH1DOWNLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CH1FWDLeak", CH1FWDLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CH1UPindex", CH1UPindex.Text);
            config.IniWriteValue("Lin", "CH1DOWNindex", CH1DOWNindex.Text);
            config.IniWriteValue("Lin", "CH1FWDindex", CH1FWDindex.Text);
            config.IniWriteValue("Lin", "CH1RWDindex", CH1RWDindex.Text);
            config.IniWriteValue("Lin", "CH1UPLeakindex", CH1UPLeakindex.Text);
            config.IniWriteValue("Lin", "CH1DOWNLeakindex", CH1DOWNLeakindex.Text);
            config.IniWriteValue("Lin", "CH1FWDLeakindex", CH1FWDLeakindex.Text);
            config.IniWriteValue("Lin", "CH1Pump", CH1Pump.Checked.ToString());

            config.IniWriteValue("Lin", "CH1QuiescentCurrnt", CH1QuiescentCurrnt.Checked.ToString());
            config.IniWriteValue("Lin", "CH1QuiescentCurrntIndex", CH1QuiescentCurrntIndex.Text);
            config.IniWriteValue("Lin", "CH2UP", CH2UP.Checked.ToString());
            config.IniWriteValue("Lin", "CH2DOWN", CH2DOWN.Checked.ToString());
            config.IniWriteValue("Lin", "CH2FWD", CH2FWD.Checked.ToString());
            config.IniWriteValue("Lin", "CH2RWD", CH2RWD.Checked.ToString());
            config.IniWriteValue("Lin", "CH2LIN", CH2LIN.Checked.ToString());
            config.IniWriteValue("Lin", "CH2UPLeak", CH2UPLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CH2DOWNLeak", CH2DOWNLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CH2FWDLeak", CH2FWDLeak.Checked.ToString());
            //mesconfig.IniWriteValue("Lin", "CH2Leak", CH2Leak.Checked.ToString());
            config.IniWriteValue("Lin", "CH2UPindex", CH2UPindex.Text);
            config.IniWriteValue("Lin", "CH2DOWNindex", CH2DOWNindex.Text);
            config.IniWriteValue("Lin", "CH2FWDindex", CH2FWDindex.Text);
            config.IniWriteValue("Lin", "CH2RWDindex", CH2RWDindex.Text);
            config.IniWriteValue("Lin", "CH2UPLeakindex", CH2UPLeakindex.Text);
            config.IniWriteValue("Lin", "CH2DOWNLeakindex", CH2DOWNLeakindex.Text);
            config.IniWriteValue("Lin", "CH2FWDLeakindex", CH2FWDLeakindex.Text);
            config.IniWriteValue("Lin", "CH2Pump", CH2Pump.Checked.ToString());

            config.IniWriteValue("Lin", "CH2QuiescentCurrnt", CH2QuiescentCurrnt.Checked.ToString());
            config.IniWriteValue("Lin", "CH2QuiescentCurrntIndex", CH2QuiescentCurrntIndex.Text);
            //AD
            config.IniWriteValue("Lin", "ADUP", ADUP.Checked.ToString());
            config.IniWriteValue("Lin", "ADDOWN", ADDOWN.Checked.ToString());
            config.IniWriteValue("Lin", "ADFWD", ADFWD.Checked.ToString());
            config.IniWriteValue("Lin", "ADRWD", ADRWD.Checked.ToString());
            config.IniWriteValue("Lin", "ADUPLeak", ADUPLeak.Checked.ToString());
            config.IniWriteValue("Lin", "ADDOWNLeak", ADDOWNLeak.Checked.ToString());
            config.IniWriteValue("Lin", "ADFWDLeak", ADFWDLeak.Checked.ToString());
            config.IniWriteValue("Lin", "ADUPindex", ADUPindex.Text);
            config.IniWriteValue("Lin", "ADDOWNindex", ADDOWNindex.Text);
            config.IniWriteValue("Lin", "ADFWDindex", ADFWDindex.Text);
            config.IniWriteValue("Lin", "ADRWDindex", ADRWDindex.Text);
            config.IniWriteValue("Lin", "ADUPLeakindex", ADUPLeakindex.Text);
            config.IniWriteValue("Lin", "ADDOWNLeakindex", ADDOWNLeakindex.Text);
            config.IniWriteValue("Lin", "ADFWDLeakindex", ADFWDLeakindex.Text);
            config.IniWriteValue("Lin", "ADQuiescentCurrnt", ADQuiescentCurrnt.Checked.ToString());
            config.IniWriteValue("Lin", "ADQuiescentCurrntIndex", ADQuiescentCurrntIndex.Text);
            //BE
            config.IniWriteValue("Lin", "BEUP", BEUP.Checked.ToString());
            config.IniWriteValue("Lin", "BEDOWN", BEDOWN.Checked.ToString());
            config.IniWriteValue("Lin", "BEFWD", BEFWD.Checked.ToString());
            config.IniWriteValue("Lin", "BERWD", BERWD.Checked.ToString());
            config.IniWriteValue("Lin", "BEUPLeak", BEUPLeak.Checked.ToString());
            config.IniWriteValue("Lin", "BEDOWNLeak", BEDOWNLeak.Checked.ToString());
            config.IniWriteValue("Lin", "BEFWDLeak", BEFWDLeak.Checked.ToString());
            config.IniWriteValue("Lin", "BEUPindex", BEUPindex.Text);
            config.IniWriteValue("Lin", "BEDOWNindex", BEDOWNindex.Text);
            config.IniWriteValue("Lin", "BEFWDindex", BEFWDindex.Text);
            config.IniWriteValue("Lin", "BERWDindex", BERWDindex.Text);
            config.IniWriteValue("Lin", "BEUPLeakindex", BEUPLeakindex.Text);
            config.IniWriteValue("Lin", "BEDOWNLeakindex", BEDOWNLeakindex.Text);
            config.IniWriteValue("Lin", "BEFWDLeakindex", BEFWDLeakindex.Text);
            config.IniWriteValue("Lin", "BEQuiescentCurrnt", BEQuiescentCurrnt.Checked.ToString());
            config.IniWriteValue("Lin", "BEQuiescentCurrntIndex", BEQuiescentCurrntIndex.Text);
            //CF
            config.IniWriteValue("Lin", "CFUP", CFUP.Checked.ToString());
            config.IniWriteValue("Lin", "CFDOWN", CFDOWN.Checked.ToString());
            config.IniWriteValue("Lin", "CFFWD", CFFWD.Checked.ToString());
            config.IniWriteValue("Lin", "CFRWD", CFRWD.Checked.ToString());
            config.IniWriteValue("Lin", "CFUPLeak", CFUPLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CFDOWNLeak", CFDOWNLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CFFWDLeak", CFFWDLeak.Checked.ToString());
            config.IniWriteValue("Lin", "CFUPindex", CFUPindex.Text);
            config.IniWriteValue("Lin", "CFDOWNindex", CFDOWNindex.Text);
            config.IniWriteValue("Lin", "CFFWDindex", CFFWDindex.Text);
            config.IniWriteValue("Lin", "CFRWDindex", CFRWDindex.Text);
            config.IniWriteValue("Lin", "CFUPLeakindex", CFUPLeakindex.Text);
            config.IniWriteValue("Lin", "CFDOWNLeakindex", CFDOWNLeakindex.Text);
            config.IniWriteValue("Lin", "CFFWDLeakindex", CFFWDLeakindex.Text);
            config.IniWriteValue("Lin", "CFQuiescentCurrnt", CFQuiescentCurrnt.Checked.ToString());
            config.IniWriteValue("Lin", "CFQuiescentCurrntIndex", CFQuiescentCurrntIndex.Text);
        }

        private void OrderLeftCopy_Click(object sender, EventArgs e)
        {
            CH2HighLevel.Checked = CH1HighLevel.Checked;
            CH2IGN.Checked = CH1IGN.Checked;
            CH2UP.Checked = CH1UP.Checked;
            CH2DOWN.Checked = CH1DOWN.Checked;
            CH2FWD.Checked = CH1FWD.Checked;
            CH2RWD.Checked = CH1RWD.Checked;
            CH2LIN.Checked = CH1LIN.Checked;
            CH2UPLeak.Checked = CH1UPLeak.Checked;
            CH2DOWNLeak.Checked = CH1DOWNLeak.Checked;
            CH2FWDLeak.Checked = CH1FWDLeak.Checked;
            CH2QuiescentCurrnt.Checked = CH1QuiescentCurrnt.Checked;
            CH2UpDownChange.Checked = CH1UpDownChange.Checked;
            CH2Order.Clear();
            if (CH2UP.Checked)
            {
                CH2Order.Add("UP");
            }
            if (CH2DOWN.Checked)
            {
                CH2Order.Add("DOWN");
            }
            if (CH2FWD.Checked)
            {
                CH2Order.Add("FWD");
            }
            if (CH2RWD.Checked)
            {
                CH2Order.Add("RWD");
            }
            if (CH2UPLeak.Checked)
            {
                CH2Order.Add("UPLeak");
            }
            if (CH2DOWNLeak.Checked)
            {
                CH2Order.Add("DOWNLeak");
            }
            if (CH2FWDLeak.Checked)
            {
                CH2Order.Add("FWDLeak");
            }
            if (CH2QuiescentCurrnt.Checked)
            {
                CH2Order.Add("QC");
            }



            if (!String.IsNullOrEmpty(CH1UPindex.Text))
            {
                CH2Order.Remove("UP");
                CH2Order.Insert(Convert.ToInt32(CH1UPindex.Text) - 1, "UP");
            }
            if (!String.IsNullOrEmpty(CH1DOWNindex.Text))
            {
                CH2Order.Remove("DOWN");
                CH2Order.Insert(Convert.ToInt32(CH1DOWNindex.Text) - 1, "DOWN");
            }
            if (!String.IsNullOrEmpty(CH1FWDindex.Text))
            {
                CH2Order.Remove("FWD");
                CH2Order.Insert(Convert.ToInt32(CH1FWDindex.Text) - 1, "FWD");
            }

            if (!String.IsNullOrEmpty(CH1RWDindex.Text))
            {
                CH2Order.Remove("RWD");
                CH2Order.Insert(Convert.ToInt32(CH1RWDindex.Text) - 1, "RWD");
            }
            if (!String.IsNullOrEmpty(CH1UPLeakindex.Text))
            {
                CH2Order.Remove("UPLeak");
                CH2Order.Insert(Convert.ToInt32(CH1UPLeakindex.Text) - 1, "UPLeak");
            }
            if (!String.IsNullOrEmpty(CH1DOWNLeakindex.Text))
            {
                CH2Order.Remove("DOWNLeak");
                CH2Order.Insert(Convert.ToInt32(CH1DOWNLeakindex.Text) - 1, "DOWNLeak");
            }
            if (!String.IsNullOrEmpty(CH1FWDLeakindex.Text))
            {
                CH2Order.Remove("FWDLeak");
                CH2Order.Insert(Convert.ToInt32(CH1FWDLeakindex.Text) - 1, "FWDLeak");
            }
            if (!String.IsNullOrEmpty(CH1QuiescentCurrntIndex.Text))
            {
                CH2Order.Remove("QC");
                CH2Order.Insert(Convert.ToInt32(CH1QuiescentCurrntIndex.Text) - 1, "QC");
            }

            RefreshIndex();
            if (Form1.f1.plc.PLCIsRun)
            {
                CH2Pump.Checked = CH1Pump.Checked;
                if (CH2Pump.Checked)
                {
                    Form1.f1.plc.CH2Pump();
                }
                else
                {
                    Form1.f1.plc.CH2Machine();
                }
                Form1.f1.CH2Pump = CH2Pump.Checked;
            }
            else
            {
                //bool check = (!CH2Pump.Checked);
                //CH2Pump.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void OrderRightCopy_Click(object sender, EventArgs e)
        {
            CH1HighLevel.Checked = CH2HighLevel.Checked;
            CH1IGN.Checked = CH2IGN.Checked;
            CH1UP.Checked = CH2UP.Checked;
            CH1DOWN.Checked = CH2DOWN.Checked;
            CH1FWD.Checked = CH2FWD.Checked;
            CH1RWD.Checked = CH2RWD.Checked;
            CH1LIN.Checked = CH2LIN.Checked;
            CH1UPLeak.Checked = CH2UPLeak.Checked;
            CH1DOWNLeak.Checked = CH2DOWNLeak.Checked;
            CH1FWDLeak.Checked = CH2FWDLeak.Checked;
            CH1QuiescentCurrnt.Checked = CH2QuiescentCurrnt.Checked;
            CH1UpDownChange.Checked = CH2UpDownChange.Checked;
            CH1Order.Clear();
            if (CH1UP.Checked)
            {
                CH1Order.Add("UP");
            }
            if (CH1DOWN.Checked)
            {
                CH1Order.Add("DOWN");
            }
            if (CH1FWD.Checked)
            {
                CH1Order.Add("FWD");
            }
            if (CH1RWD.Checked)
            {
                CH1Order.Add("RWD");
            }
            if (CH1UPLeak.Checked)
            {
                CH1Order.Add("UPLeak");
            }
            if (CH1DOWNLeak.Checked)
            {
                CH1Order.Add("DOWNLeak");
            }
            if (CH1FWDLeak.Checked)
            {
                CH1Order.Add("FWDLeak");
            }
            if (CH1QuiescentCurrnt.Checked)
            {
                CH1Order.Add("QC");
            }
            if (!String.IsNullOrEmpty(CH2UPindex.Text))
            {
                CH1Order.Remove("UP");
                CH1Order.Insert(Convert.ToInt32(CH2UPindex.Text) - 1, "UP");
            }
            if (!String.IsNullOrEmpty(CH2DOWNindex.Text))
            {
                CH1Order.Remove("DOWN");
                CH1Order.Insert(Convert.ToInt32(CH2DOWNindex.Text) - 1, "DOWN");
            }
            if (!String.IsNullOrEmpty(CH2FWDindex.Text))
            {
                CH1Order.Remove("FWD");
                CH1Order.Insert(Convert.ToInt32(CH2FWDindex.Text) - 1, "FWD");
            }

            if (!String.IsNullOrEmpty(CH2RWDindex.Text))
            {
                CH1Order.Remove("RWD");
                CH1Order.Insert(Convert.ToInt32(CH2RWDindex.Text) - 1, "RWD");
            }
            if (!String.IsNullOrEmpty(CH2UPLeakindex.Text))
            {
                CH1Order.Remove("UPLeak");
                CH1Order.Insert(Convert.ToInt32(CH2UPLeakindex.Text) - 1, "UPLeak");
            }
            if (!String.IsNullOrEmpty(CH2DOWNLeakindex.Text))
            {
                CH1Order.Remove("DOWNLeak");
                CH1Order.Insert(Convert.ToInt32(CH2DOWNLeakindex.Text) - 1, "DOWNLeak");
            }
            if (!String.IsNullOrEmpty(CH2FWDLeakindex.Text))
            {
                CH1Order.Remove("FWDLeak");
                CH1Order.Insert(Convert.ToInt32(CH2FWDLeakindex.Text) - 1, "FWDLeak");
            }
            if (!String.IsNullOrEmpty(CH2QuiescentCurrntIndex.Text))
            {
                CH1Order.Remove("QC");
                CH1Order.Insert(Convert.ToInt32(CH2QuiescentCurrntIndex.Text) - 1, "QC");
            }

            RefreshIndex();
            if (Form1.f1.plc.PLCIsRun)
            {
                CH1Pump.Checked = CH2Pump.Checked;
                if (CH1Pump.Checked)
                {
                    Form1.f1.plc.CH1Pump();
                }
                else
                {
                    Form1.f1.plc.CH1Machine();
                }
                Form1.f1.CH1Pump = CH1Pump.Checked;
            }
            else
            {
                //bool check = (!CH1Pump.Checked);
                //CH1Pump.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void ReadSignal_Tick(object sender, EventArgs e)
        {
            ReadSignal.Interval = 600;
            if (Form1.f1.plc.PLCIsRun)
            {
                CH1HighLevel.Checked = Form1.f1.plc.CH1HighLevel;
                CH1IGN.Checked = Form1.f1.plc.CH1IGN;
                CH1LIN.Checked = Form1.f1.plc.CH1LIN;

                //CH1Pump.Checked = Form1.f1.plc.CH1Pump;

                CH2HighLevel.Checked = Form1.f1.plc.CH2HighLevel;
                CH2IGN.Checked = Form1.f1.plc.CH2IGN;
                CH2LIN.Checked = Form1.f1.plc.CH2LIN;

                //CH2Pump.Checked = Form1.f1.plc.CH2Pump;
            }
            else
            {
                ReadSignal.Stop();
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
        }

        private void ReadLin()
        {
            ReadConfig con = new ReadConfig();
            ord = con.ReadLin();


            CH1Pump.Checked = ord.CH1Pump;
            CH2LIN.Checked = ord.CH2LIN;
            CH1HighLevel.Checked = ord.CH1HighLevel;

            CH1UpDownChange.Checked = ord.CH1UpDownChange;
            //CH1QuiescentCurrnt.Checked = ord.CH1QuiescentCurrnt;

            int[] asa = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            asa[0] = Convert.ToInt32(ord.CH1QuiescentCurrntIndex);
            asa[1] = Convert.ToInt32(ord.CH1UPindex);
            asa[2] = Convert.ToInt32(ord.CH1DOWNindex);
            asa[3] = Convert.ToInt32(ord.CH1FWDindex);
            asa[4] = Convert.ToInt32(ord.CH1RWDindex);
            asa[5] = Convert.ToInt32(ord.CH1UPLeakindex);
            asa[6] = Convert.ToInt32(ord.CH1DOWNLeakindex);
            asa[7] = Convert.ToInt32(ord.CH1FWDLeakindex);

            CH1UPindex.Text = ord.CH1UPindex;
            CH1DOWNindex.Text = ord.CH1DOWNindex;
            CH1FWDindex.Text = ord.CH1FWDindex;
            CH1RWDindex.Text = ord.CH1RWDindex;
            CH1UPLeakindex.Text = ord.CH1UPLeakindex;
            CH1DOWNLeakindex.Text = ord.CH1DOWNLeakindex;
            CH1FWDLeakindex.Text = ord.CH1FWDLeakindex;
            CH1QuiescentCurrntIndex.Text = ord.CH1QuiescentCurrntIndex;
            int i, j;
            for (j = 0; j < 8; j++)
                for (i = 0; i < 8; i++)
                {
                    if (asa[i] == j + 1)
                    {
                        if (i == 0)
                            CH1QuiescentCurrnt.Checked = ord.CH1QuiescentCurrnt;
                        if (i == 1)
                            CH1UP.Checked = ord.CH1UP;
                        if (i == 2)
                            CH1DOWN.Checked = ord.CH1DOWN;
                        if (i == 3)
                            CH1FWD.Checked = ord.CH1FWD;
                        if (i == 4)
                            CH1RWD.Checked = ord.CH1RWD;
                        if (i == 5)
                            CH1UPLeak.Checked = ord.CH1UPLeak;
                        if (i == 6)
                            CH1DOWNLeak.Checked = ord.CH1DOWNLeak;
                        if (i == 7)
                            CH1FWDLeak.Checked = ord.CH1FWDLeak;
                    }
                }
            asa[0] = Convert.ToInt32(ord.CH2QuiescentCurrntIndex);
            asa[1] = Convert.ToInt32(ord.CH2UPindex);
            asa[2] = Convert.ToInt32(ord.CH2DOWNindex);
            asa[3] = Convert.ToInt32(ord.CH2FWDindex);
            asa[4] = Convert.ToInt32(ord.CH2RWDindex);
            asa[5] = Convert.ToInt32(ord.CH2UPLeakindex);
            asa[6] = Convert.ToInt32(ord.CH2DOWNLeakindex);
            asa[7] = Convert.ToInt32(ord.CH2FWDLeakindex);
            //CH2UP.Checked = ord.CH2UP;
            //CH2DOWN.Checked = ord.CH2DOWN;
            //CH2FWD.Checked = ord.CH2FWD;
            //CH2RWD.Checked = ord.CH2RWD;
            //CH2UPLeak.Checked = ord.CH2UPLeak;
            //CH2DOWNLeak.Checked = ord.CH2DOWNLeak;
            //CH2FWDLeak.Checked = ord.CH2FWDLeak;
            //CH2QuiescentCurrnt.Checked = ord.CH2QuiescentCurrnt;
            for (j = 0; j < 8; j++)
                for (i = 0; i < 8; i++)
                {
                    if (asa[i] == j + 1)
                    {
                        if (i == 0)
                            CH2QuiescentCurrnt.Checked = ord.CH1QuiescentCurrnt;
                        if (i == 1)
                            CH2UP.Checked = ord.CH2UP;
                        if (i == 2)
                            CH2DOWN.Checked = ord.CH2DOWN;
                        if (i == 3)
                            CH2FWD.Checked = ord.CH2FWD;
                        if (i == 4)
                            CH2RWD.Checked = ord.CH2RWD;
                        if (i == 5)
                            CH2UPLeak.Checked = ord.CH2UPLeak;
                        if (i == 6)
                            CH2DOWNLeak.Checked = ord.CH2DOWNLeak;
                        if (i == 7)
                            CH2FWDLeak.Checked = ord.CH2FWDLeak;
                    }
                }
            CH2Pump.Checked = ord.CH2Pump;
            CH2UpDownChange.Checked = ord.CH2UpDownChange;


            CH2UPindex.Text = ord.CH2UPindex;
            CH2DOWNindex.Text = ord.CH2DOWNindex;
            CH2FWDindex.Text = ord.CH2FWDindex;
            CH2RWDindex.Text = ord.CH2RWDindex;
            CH2UPLeakindex.Text = ord.CH2UPLeakindex;
            CH2DOWNLeakindex.Text = ord.CH2DOWNLeakindex;
            CH2FWDLeakindex.Text = ord.CH2FWDLeakindex;
            CH2QuiescentCurrntIndex.Text = ord.CH2QuiescentCurrntIndex;

            //ADUP.Checked = ord.ADUP;
            //ADDOWN.Checked = ord.ADDOWN;
            //ADFWD.Checked = ord.ADFWD;
            //ADRWD.Checked = ord.ADRWD;
            //ADUPLeak.Checked = ord.ADUPLeak;
            //ADDOWNLeak.Checked = ord.ADDOWNLeak;
            //ADFWDLeak.Checked = ord.ADFWDLeak;
            //ADQuiescentCurrnt.Checked = ord.ADQuiescentCurrnt;
            ADQuiescentCurrntIndex.Text = ord.ADQuiescentCurrntIndex;
            ADUPindex.Text = ord.ADUPindex;
            ADDOWNindex.Text = ord.ADDOWNindex;
            ADFWDindex.Text = ord.ADFWDindex;
            ADRWDindex.Text = ord.ADRWDindex;
            ADUPLeakindex.Text = ord.ADUPLeakindex;
            ADDOWNLeakindex.Text = ord.ADDOWNLeakindex;
            ADFWDLeakindex.Text = ord.ADFWDLeakindex;



            asa[0] = Convert.ToInt32(ord.ADQuiescentCurrntIndex);
            asa[1] = Convert.ToInt32(ord.ADUPindex);
            asa[2] = Convert.ToInt32(ord.ADDOWNindex);
            asa[3] = Convert.ToInt32(ord.ADFWDindex);
            asa[4] = Convert.ToInt32(ord.ADRWDindex);
            asa[5] = Convert.ToInt32(ord.ADUPLeakindex);
            asa[6] = Convert.ToInt32(ord.ADDOWNLeakindex);
            asa[7] = Convert.ToInt32(ord.ADFWDLeakindex);
            //AD
            for (j = 0; j < 8; j++)
                for (i = 0; i < 8; i++)
                {
                    if (asa[i] == j + 1)
                    {
                        if (i == 0)
                            ADQuiescentCurrnt.Checked = ord.ADQuiescentCurrnt;
                        if (i == 1)
                            ADUP.Checked = ord.ADUP;
                        if (i == 2)
                            ADDOWN.Checked = ord.ADDOWN;
                        if (i == 3)
                            ADFWD.Checked = ord.ADFWD;
                        if (i == 4)
                            ADRWD.Checked = ord.ADRWD;
                        if (i == 5)
                            ADUPLeak.Checked = ord.ADUPLeak;
                        if (i == 6)
                            ADDOWNLeak.Checked = ord.ADDOWNLeak;
                        if (i == 7)
                            ADFWDLeak.Checked = ord.ADFWDLeak;
                    }
                }

            //BE
            asa[0] = Convert.ToInt32(ord.BEQuiescentCurrntIndex);
            asa[1] = Convert.ToInt32(ord.BEUPindex);
            asa[2] = Convert.ToInt32(ord.BEDOWNindex);
            asa[3] = Convert.ToInt32(ord.BEFWDindex);
            asa[4] = Convert.ToInt32(ord.BERWDindex);
            asa[5] = Convert.ToInt32(ord.BEUPLeakindex);
            asa[6] = Convert.ToInt32(ord.BEDOWNLeakindex);
            asa[7] = Convert.ToInt32(ord.BEFWDLeakindex);
            for (j = 0; j < 8; j++)
                for (i = 0; i < 8; i++)
                {
                    if (asa[i] == j + 1)
                    {
                        if (i == 0)
                            BEQuiescentCurrnt.Checked = ord.BEQuiescentCurrnt;
                        if (i == 1)
                            BEUP.Checked = ord.BEUP;
                        if (i == 2)
                            BEDOWN.Checked = ord.BEDOWN;
                        if (i == 3)
                            BEFWD.Checked = ord.BEFWD;
                        if (i == 4)
                            BERWD.Checked = ord.BERWD;
                        if (i == 5)
                            BEUPLeak.Checked = ord.BEUPLeak;
                        if (i == 6)
                            BEDOWNLeak.Checked = ord.BEDOWNLeak;
                        if (i == 7)
                            BEFWDLeak.Checked = ord.BEFWDLeak;
                    }
                }
            BEUPindex.Text = ord.BEUPindex;
            BEDOWNindex.Text = ord.BEDOWNindex;
            BEFWDindex.Text = ord.BEFWDindex;
            BERWDindex.Text = ord.BERWDindex;
            BEUPLeakindex.Text = ord.BEUPLeakindex;
            BEDOWNLeakindex.Text = ord.BEDOWNLeakindex;
            BEFWDLeakindex.Text = ord.BEFWDLeakindex;
            BEQuiescentCurrntIndex.Text = ord.BEQuiescentCurrntIndex;
            //CF
            //CFUP.Checked = ord.CFUP;
            //CFDOWN.Checked = ord.CFDOWN;
            //CFFWD.Checked = ord.CFFWD;
            //CFRWD.Checked = ord.CFRWD;
            //CFUPLeak.Checked = ord.CFUPLeak;
            //CFDOWNLeak.Checked = ord.CFDOWNLeak;
            //CFFWDLeak.Checked = ord.CFFWDLeak;
            //CFQuiescentCurrnt.Checked = ord.CFQuiescentCurrnt;
            asa[0] = Convert.ToInt32(ord.CFQuiescentCurrntIndex);
            asa[1] = Convert.ToInt32(ord.CFUPindex);
            asa[2] = Convert.ToInt32(ord.CFDOWNindex);
            asa[3] = Convert.ToInt32(ord.CFFWDindex);
            asa[4] = Convert.ToInt32(ord.CFRWDindex);
            asa[5] = Convert.ToInt32(ord.CFUPLeakindex);
            asa[6] = Convert.ToInt32(ord.CFDOWNLeakindex);
            asa[7] = Convert.ToInt32(ord.CFFWDLeakindex);
            for (j = 0; j < 8; j++)
                for (i = 0; i < 8; i++)
                {
                    if (asa[i] == j + 1)
                    {
                        if (i == 0)
                            CFQuiescentCurrnt.Checked = ord.BEQuiescentCurrnt;
                        if (i == 1)
                            CFUP.Checked = ord.CFUP;
                        if (i == 2)
                            CFDOWN.Checked = ord.CFDOWN;
                        if (i == 3)
                            CFFWD.Checked = ord.CFFWD;
                        if (i == 4)
                            CFRWD.Checked = ord.CFRWD;
                        if (i == 5)
                            CFUPLeak.Checked = ord.CFUPLeak;
                        if (i == 6)
                            CFDOWNLeak.Checked = ord.CFDOWNLeak;
                        if (i == 7)
                            CFFWDLeak.Checked = ord.CFFWDLeak;
                    }
                }

            CFUPindex.Text = ord.CFUPindex;
            CFDOWNindex.Text = ord.CFDOWNindex;
            CFFWDindex.Text = ord.CFFWDindex;
            CFRWDindex.Text = ord.CFRWDindex;
            CFUPLeakindex.Text = ord.CFUPLeakindex;
            CFDOWNLeakindex.Text = ord.CFDOWNLeakindex;
            CFFWDLeakindex.Text = ord.CFFWDLeakindex;
            CFQuiescentCurrntIndex.Text = ord.CFQuiescentCurrntIndex;



        }

        private void ChkPLCPress_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPLCPress.Checked)
            {
                PLCPreUnit1.Text = "kgf/cm2";
                PLCPreUnit2.Text = "kgf/cm2";
                PLCPreUnit3.Text = "kgf/cm2";
                PLCPreUnit4.Text = "kgf/cm2";
            }
            else
            {
                PLCPreUnit1.Text = "Kpa";
                PLCPreUnit2.Text = "Kpa";
                PLCPreUnit3.Text = "Kpa";
                PLCPreUnit4.Text = "Kpa";
            }
            WritePLCConfig();
        }

        private void btnCKCh1Save_Click(object sender, EventArgs e)
        {
            double ch1vol = Convert.ToDouble(CKCh1Vol.Text);
            if (String.IsNullOrEmpty(CKCh1Vol.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "电压数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值需填写"));
                return;
            }
            else if (ch1vol > 60.0D)
            {
                Logger.Log(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                return;
            }
            double ch1Current = Convert.ToDouble(CKCh1Current.Text);
            if (String.IsNullOrEmpty(CKCh1Current.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "电流数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值需填写"));
                return;
            }
            else if (ch1Current > 5.0D)
            {
                Logger.Log(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                return;
            }

            double Vs = double.Parse(CKCh1Vol.Text);
            double As = double.Parse(CKCh1Current.Text);
            if (Form1.CH1POWER._serialPort.IsOpen)
                Form1.CH1POWER._serialPort.WriteLine("VOLT " + Vs.ToString("0.0000"));
            if (Form1.CH1POWER._serialPort.IsOpen)
                Form1.CH1POWER._serialPort.WriteLine("CURR " + As.ToString("0.0000"));
            WritePLCConfig();
            SaveFlowRunLog();
        }

        private void btnCKCh2Save_Click(object sender, EventArgs e)
        {
            double ch2vol = Convert.ToDouble(CKCh2Vol.Text);
            if (String.IsNullOrEmpty(CKCh2Vol.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "电压数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值需填写"));
                return;
            }
            else if (ch2vol > 60.0D)
            {
                Logger.Log(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电压数值不可以超过60"));
                return;
            }
            double ch2Current = Convert.ToDouble(CKCh2Current.Text);
            if (String.IsNullOrEmpty(CKCh2Current.Text.Trim()))
            {
                Logger.Log(I18N.GetLangText(dicLang, "电流数值需填写"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值需填写"));
                return;
            }
            else if (ch2Current > 5.0D)
            {
                Logger.Log(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                MessageBox.Show(I18N.GetLangText(dicLang, "电流数值不可以超过5"));
                return;
            }

            double Vs = double.Parse(CKCh2Vol.Text);
            double As = double.Parse(CKCh2Current.Text);
            if (Form1.f1.CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("VOLT " + Vs.ToString("0.0000"));
            if (Form1.f1.CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("CURR " + As.ToString("0.0000"));

            WritePLCConfig();
            SaveFlowRunLog();
        }

        private void btnCKCh1On_Click(object sender, EventArgs e)
        {
            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
        }

        private void btnCKCh1Off_Click(object sender, EventArgs e)
        {
            Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
        }

        private void btnCKCh2On_Click(object sender, EventArgs e)
        {
            Form1.CH2POWER._serialPort.WriteLine("OUTP 1");         //if (Form1.f1.CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("OUTP 1");
        }

        private void btnCKCh2Off_Click(object sender, EventArgs e)
        {
            Form1.CH2POWER._serialPort.WriteLine("OUTP 0");
        }

        private void CH1UpDownChange_CheckedChanged(object sender, EventArgs e)
        {
            //WriteLin();
            ord.CH1UpDownChange = CH1UpDownChange.Checked;
            Form1.f1.CH1change = CH1UpDownChange.Checked;
        }

        private void CH2UpDownChange_CheckedChanged(object sender, EventArgs e)
        {
            //WriteLin();
            ord.CH2UpDownChange = CH2UpDownChange.Checked;
            Form1.f1.CH2change = CH2UpDownChange.Checked;
        }

        private void CH1QuiescentCurrnt_Click(object sender, EventArgs e)
        {
        }

        private void CH2QuiescentCurrnt_Click(object sender, EventArgs e)
        {
        }

        private void CH2QuiescentCurrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2QuiescentCurrnt.Checked && !CH2Order.Contains("QC"))
            {
                CH2Order.Add("QC");
                CH2QuiescentCurrntIndex.Text = ((CH2Order.IndexOf("QC") == -1 ? 0 : CH2Order.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                CH2Order.Remove("QC");
                CH2QuiescentCurrntIndex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2HighLevel_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2HighLevel.Checked)
                {
                    //Form1.f1.plc.CH2HLevelTure();
                    Form1.f1.plc.WriteCH2HLevelTure();
                }
                else
                {
                    //Form1.f1.plc.CH2HLevelFlase();
                    Form1.f1.plc.WriteCH2HLevelFalse();
                }
            }
            else
            {
                bool check = (!CH2HighLevel.Checked);
                CH2HighLevel.Checked = check;
                //MessageBox.Show("PLC未通讯！");
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
        }

        private void CH2IGN_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2IGN.Checked)
                {
                    //Form1.f1.plc.CH2IGNTure();
                    Form1.f1.plc.WriteCH2IGNTure();
                }
                else
                {
                    //Form1.f1.plc.CH2IGNFlase();
                    Form1.f1.plc.WriteCH2IGNFlase();
                }
            }
            else
            {
                bool check = (!CH2IGN.Checked);
                CH2IGN.Checked = check;
                //MessageBox.Show("PLC未通讯！");
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
        }

        private void CH2UP_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2UP.Checked && !CH2Order.Contains("UP"))
            {
                CH2Order.Add("UP");
                CH2UPindex.Text = ((CH2Order.IndexOf("UP") == -1 ? 0 : CH2Order.IndexOf("UP")) + 1).ToString();// (CH2Order.IndexOf("UP") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("UP");
                CH2UPindex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2DOWN_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2DOWN.Checked && !CH2Order.Contains("DOWN"))
            {
                CH2Order.Add("DOWN");
                CH2DOWNindex.Text = ((CH2Order.IndexOf("DOWN") == -1 ? 0 : CH2Order.IndexOf("DOWN")) + 1).ToString(); //(CH2Order.IndexOf("DOWN") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("DOWN");
                CH2DOWNindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2FWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2FWD.Checked && !CH2Order.Contains("FWD"))
            {
                CH2Order.Add("FWD");
                CH2FWDindex.Text = ((CH2Order.IndexOf("FWD") == -1 ? 0 : CH2Order.IndexOf("FWD")) + 1).ToString();// (CH2Order.IndexOf("FWD") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("FWD");
                CH2FWDindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2RWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2RWD.Checked && !CH2Order.Contains("RWD"))
            {
                CH2Order.Add("RWD");
                CH2RWDindex.Text = ((CH2Order.IndexOf("RWD") == -1 ? 0 : CH2Order.IndexOf("RWD")) + 1).ToString(); //(CH2Order.IndexOf("RWD") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("RWD");
                CH2RWDindex.Text = "";
            }
            RefreshIndex();
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2UPLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2UPLeak.Checked && !CH2Order.Contains("UPLeak"))
            {
                CH2Order.Add("UPLeak");
                CH2UPLeakindex.Text = ((CH2Order.IndexOf("UPLeak") == -1 ? 0 : CH2Order.IndexOf("UPLeak")) + 1).ToString(); //(CH2Order.IndexOf("UPLeak") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("UPLeak");
                CH2UPLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2DOWNLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2DOWNLeak.Checked && !CH2Order.Contains("DOWNLeak"))
            {
                CH2Order.Add("DOWNLeak");
                CH2DOWNLeakindex.Text = ((CH2Order.IndexOf("DOWNLeak") == -1 ? 0 : CH2Order.IndexOf("DOWNLeak")) + 1).ToString(); //(CH2Order.IndexOf("DOWNLeak") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("DOWNLeak");
                CH2DOWNLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2FWDLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH2FWDLeak.Checked && !CH2Order.Contains("FWDLeak"))
            {
                CH2Order.Add("FWDLeak");
                CH2FWDLeakindex.Text = ((CH2Order.IndexOf("FWDLeak") == -1 ? 0 : CH2Order.IndexOf("FWDLeak")) + 1).ToString();// (CH2Order.IndexOf("FWDLeak") + 1).ToString();
            }
            else
            {
                CH2Order.Remove("FWDLeak");
                CH2FWDLeakindex.Text = "";
                RefreshIndex();
            }
            // WriteLin();
            Form1.f1.CH2Order = CH2Order;
        }

        private void CH2Pump_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2Pump.Checked)
                {
                    //Form1.f1.plc.CH2Pump();
                    Form1.f1.plc.WriteCH2Pump();
                }
                else
                {
                    //Form1.f1.plc.CH2Machine();
                    Form1.f1.plc.WriteCH2Machine();
                }
                Form1.f1.CH2Pump = CH2Pump.Checked;
            }
            else
            {
                bool check = (!CH2Pump.Checked);
                CH2Pump.Checked = check;
                //MessageBox.Show("PLC未通讯！");
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
            //WriteLin();
        }

        private void CH2LIN_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2LIN.Checked)
                {
                    //Form1.f1.plc.CH2LINTure();
                    Form1.f1.plc.WriteCH2LINTure();
                }
                else
                {
                    //Form1.f1.plc.CH2LINFlase();
                    Form1.f1.plc.WriteCH2LINFlase();
                }
            }
            else
            {
                bool check = (!CH2LIN.Checked);
                CH2LIN.Checked = check;
                //MessageBox.Show("PLC未通讯！");
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
            //WriteLin();
        }

        private void CH1QuiescentCurrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1QuiescentCurrnt.Checked && !CH1Order.Contains("QC"))
            {
                CH1Order.Add("QC");
                CH1QuiescentCurrntIndex.Text = ((CH1Order.IndexOf("QC") == -1 ? 0 : CH1Order.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                CH1Order.Remove("QC");
                CH1QuiescentCurrntIndex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1HighLevel_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1HighLevel.Checked)
                {
                    //Form1.f1.plc.CH1HLevelTure();
                    Form1.f1.plc.WriteCH1HLevelTure();
                }
                else
                {
                    //Form1.f1.plc.CH1HLevelFlase();
                    Form1.f1.plc.WriteCH1HLevelFalse();
                }
            }
            else
            {
                bool check = (!CH1HighLevel.Checked);
                CH1HighLevel.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
        }

        private void CH1IGN_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1IGN.Checked)
                {
                    //Form1.f1.plc.CH1IGNTure();
                    Form1.f1.plc.WriteCH1IGNTure();
                }
                else
                {
                    //Form1.f1.plc.CH1IGNFlase();
                    Form1.f1.plc.WriteCH1IGNFlase();
                }
            }
            else
            {
                bool check = (!CH1IGN.Checked);
                CH1IGN.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
        }

        private void CH1UP_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1UP.Checked && !CH1Order.Contains("UP"))
            {
                CH1Order.Add("UP");
                CH1UPindex.Text = ((CH1Order.IndexOf("UP") == -1 ? 0 : CH1Order.IndexOf("UP")) + 1).ToString();// (CH1Order.IndexOf("UP") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("UP");
                CH1UPindex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1DOWN_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1DOWN.Checked && !CH1Order.Contains("DOWN"))
            {
                CH1Order.Add("DOWN");
                CH1DOWNindex.Text = ((CH1Order.IndexOf("DOWN") == -1 ? 0 : CH1Order.IndexOf("DOWN")) + 1).ToString();// (CH1Order.IndexOf("DOWN") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("DOWN");
                CH1DOWNindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1FWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1FWD.Checked && !CH1Order.Contains("FWD"))
            {
                if (CH1Order.IndexOf("FWD") != 0)
                    CH1Order.Add("FWD");
                CH1FWDindex.Text = ((CH1Order.IndexOf("FWD") == -1 ? 0 : CH1Order.IndexOf("FWD")) + 1).ToString();// (CH1Order.IndexOf("FWD") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("FWD");
                CH1FWDindex.Text = "";
                RefreshIndex();
            }
            // WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1RWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1RWD.Checked && !CH1Order.Contains("RWD"))
            {
                CH1Order.Add("RWD");
                CH1RWDindex.Text = ((CH1Order.IndexOf("RWD") == 1 ? 0 : CH1Order.IndexOf("RWD")) + 1).ToString();// (CH1Order.IndexOf("RWD") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("RWD");
                CH1RWDindex.Text = "";
            }

            RefreshIndex();
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1UPLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1UPLeak.Checked && !CH1Order.Contains("UPLeak"))
            {
                CH1Order.Add("UPLeak");
                CH1UPLeakindex.Text = ((CH1Order.IndexOf("UPLeak") == -1 ? 0 : CH1Order.IndexOf("UPLeak")) + 1).ToString(); //(CH1Order.IndexOf("UPLeak") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("UPLeak");
                CH1UPLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1DOWNLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1DOWNLeak.Checked && !CH1Order.Contains("DOWNLeak"))
            {
                CH1Order.Add("DOWNLeak");
                CH1DOWNLeakindex.Text = ((CH1Order.IndexOf("DOWNLeak") == -1 ? 0 : CH1Order.IndexOf("DOWNLeak")) + 1).ToString(); //(CH1Order.IndexOf("DOWNLeak") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("DOWNLeak");
                CH1DOWNLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1FWDLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CH1FWDLeak.Checked && !CH1Order.Contains("FWDLeak"))
            {
                CH1Order.Add("FWDLeak");
                CH1FWDLeakindex.Text = ((CH1Order.IndexOf("FWDLeak") == -1 ? 0 : CH1Order.IndexOf("FWDLeak")) + 1).ToString(); //(CH1Order.IndexOf("FWDLeak") + 1).ToString();
            }
            else
            {
                CH1Order.Remove("FWDLeak");
                CH1FWDLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CH1Order = CH1Order;
        }

        private void CH1Pump_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1Pump.Checked)
                {
                    //Form1.f1.plc.CH1Pump();
                    Form1.f1.plc.WriteCH1Pump();
                }
                else
                {
                    //Form1.f1.plc.CH1Machine();
                    Form1.f1.plc.WriteCH1Machine();
                }
                Form1.f1.CH1Pump = CH1Pump.Checked;
            }
            else
            {
                bool check = (!CH1Pump.Checked);
                CH1Pump.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
            //WriteLin();
        }

        private void CH1LIN_CheckedChanged(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1LIN.Checked)
                {
                    //Form1.f1.plc.CH1LINTure();
                    Form1.f1.plc.WriteCH1LINTure();
                }
                else
                {
                    //Form1.f1.plc.CH1LINFlase();
                    Form1.f1.plc.WriteCH1LINFlase();
                }
            }
            else
            {
                bool check = (!CH1LIN.Checked);
                CH1LIN.Checked = check;
                Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            ReadSignal.Interval = 800;
            ReadSignal.Start();
            //WriteLin();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            WriteLin();
            SaveFlowRunLog2();
            //清零
            ClearDate1();
            ClearDate2();

        }


        //清空数据
        public void ClearDate1()
        {
    Form1.f1.        CH1TestResult.UP_ADCMAX = 0;
            Form1.f1.CH1TestResult.UP_VDCMAX = 0;
            Form1.f1.CH1TestResult.UP_Pre = 0;
            Form1.f1.CH1TestResult.UP_Flow = 0;
            Form1.f1.CH1TestResult.DOWN_ADCMAX = 0;
            Form1.f1.CH1TestResult.DOWN_VDCMAX = 0;
            Form1.f1.CH1TestResult.DOWN_Pre = 0;
            Form1.f1.CH1TestResult.DOWN_Flow = 0;
            Form1.f1.CH1TestResult.ElecRatio = 0;
            Form1.f1.CH1TestResult.PressRatio = 0;
            Form1.f1.CH1TestResult.FWD_ADCMAX = 0;
            Form1.f1.CH1TestResult.FWD_VDCMAX = 0;
            Form1.f1.CH1TestResult.FWD_Pre1 = 0;
            Form1.f1.CH1TestResult.FWD_Pre2 = 0;
            Form1.f1.CH1TestResult.FWD_Flow1 = 0;
            Form1.f1.CH1TestResult.FWD_Flow2 = 0;

            Form1.f1.CH1TestResult.RWD_ADCMAX = 0;
            Form1.f1.CH1TestResult.RWD_VDCMAX = 0;

            Form1.f1.CH1TestResult.DOWN_Flowzuo = 0;
            Form1.f1.CH1TestResult.FWD_FullPre1 = "0";
            Form1.f1.CH1TestResult.BalanPre1 = "0";
            Form1.f1.CH1TestResult.Leak1 = "0";
            Form1.f1.CH1TestResult.FWD_FullPre2 = "0";
            Form1.f1.CH1TestResult.BalanPre2 = "0";
            Form1.f1.CH1TestResult.Leak2 = "0";
            Form1.f1.CH1TestResult.FWD_FullPre1 = "0";
            Form1.f1.CH1TestResult.FWD_BalanPre1 = "0";
            Form1.f1.CH1TestResult.FWD_Leak1 = "0";
     
            Form1.f1.CH1TestResult.FWD_FullPre2 = "0";
            Form1.f1.CH1TestResult.FWD_BalanPre2 = "0";
            Form1.f1.CH1TestResult.FWD_Leak2 = "0";
            Form1.f1.CH1RTElec.Text = "";

            Log log = new Log();
            log.PLC_Logmsg(DateTime.Now.ToString() + "CH1保存参数清零");

        }

        //清空数据
        public void ClearDate2()
        {


            Form1.f1.CH2TestResult.UP_ADCMAX = 0;
            Form1.f1.CH2TestResult.UP_VDCMAX = 0;
            Form1.f1.CH2TestResult.UP_Pre = 0;
            Form1.f1.CH2TestResult.UP_Flow = 0;
            Form1.f1.CH2TestResult.DOWN_ADCMAX = 0;
            Form1.f1.CH2TestResult.DOWN_VDCMAX = 0;
            Form1.f1.CH2TestResult.DOWN_Pre = 0;
            Form1.f1.CH2TestResult.DOWN_Flow = 0;
            Form1.f1.CH2TestResult.ElecRatio = 0;
            Form1.f1.CH2TestResult.PressRatio = 0;
            Form1.f1.CH2TestResult.FWD_ADCMAX = 0;
            Form1.f1.CH2TestResult.FWD_VDCMAX = 0;
            Form1.f1.CH2TestResult.FWD_Pre1 = 0;
            Form1.f1.CH2TestResult.FWD_Pre2 = 0;
            Form1.f1.CH2TestResult.FWD_Flow1 = 0;
            Form1.f1.CH2TestResult.FWD_Flow2 = 0;


            Form1.f1.CH1TestResult.RWD_ADCMAX = 0;
            Form1.f1.CH1TestResult.RWD_VDCMAX = 0;

            Form1.f1.CH2TestResult.FWD_FullPre1 = "0";
            Form1.f1.CH2TestResult.BalanPre1 = "0";
            Form1.f1.CH2TestResult.Leak1 = "0";
            Form1.f1.CH2TestResult.FWD_FullPre2 = "0";
            Form1.f1.CH2TestResult.BalanPre2 = "0";
            Form1.f1.CH2TestResult.Leak2 = "0";
            Form1.f1.CH2TestResult.FWD_FullPre1 = "0";
            Form1.f1.CH2TestResult.FWD_BalanPre1 = "0";
            Form1.f1.CH2TestResult.FWD_Leak1 = "0";

            Form1.f1.CH2TestResult.FWD_FullPre2 = "0";
            Form1.f1.CH2TestResult.FWD_BalanPre2 = "0";
            Form1.f1.CH2TestResult.FWD_Leak2 = "0";
            Form1.f1.CH2RTElec.Text = "";
            Log log = new Log();
            log.PLC_Logmsg(DateTime.Now.ToString() + "CH2保存参数清零");
        }


        public Model.CH_PARAMS ch2_2leakparams = new Model.CH_PARAMS();
        //电流电压上下限
        public Model.Electricity elec = new Model.Electricity();
        //上下充对比值
        public Model.Flow Flow = new Model.Flow();

        //测试参数
        public Model.CH_PARAMS ch1_1params = new Model.CH_PARAMS();

        public Model.CH_PARAMS ch1_2params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_1params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_2params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch1_1leakparams = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch1_2leakparams = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_1leakparams = new Model.CH_PARAMS();
   
        private Model.CH_Result left_ch1result = new Model.CH_Result();
        private Model.CH_Result left_ch2result = new Model.CH_Result();
        private Model.CH_Result right_ch1result = new Model.CH_Result();
        private Model.CH_Result right_ch2result = new Model.CH_Result();

        private void SaveFlowRunLog()
        {
            try
            {
                string fileName;
                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(DateTime.Now.ToString("yyyyMMdd")))
                        listFileSystemInfo.Add(fsinfo);
                }

                if (listFileSystemInfo.Count == 1)
                {
                    string maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                    file = file + "_" + "01";
                }
                else if (listFileSystemInfo.Count > 1)
                {
                    string maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                    string suffixCurrentNum = maxFile.Split('_')[1];
                    string suffixMaxNum = (Convert.ToInt32(suffixCurrentNum) + 1).ToString().PadLeft(2, '0');
                    file = file + "_" + suffixMaxNum;
                }

                string name = file + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, "气袋产品测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + Form1.f1.WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + Form1.f1.ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + Form1.f1.ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + Form1.f1.TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + Form1.f1.ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + Form1.f1.ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Form1.f1.Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + Form1.f1.machinepath + "\n");

                    {

                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");


                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax +Form1.f1. PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Flow.CH1_2PreMax.ToString() + "-" + Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");




                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + Form1.f1.CH2PressureUnit.Text + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Flow.CH2_2PreMax.ToString() + "-" + Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Flow.CH2_2FlowMax.ToString() + "-" + Flow.CH2_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");


                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), "", "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "");
                        //      CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), "", "Kpa", elec.TotalPreMax.ToString(), "0", "");
                        //     CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");


                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + Form1.f1.PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + Form1.f1.LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + Form1.f1.CH2PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + Form1.f1.CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + Form1.f1.PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + Form1.f1.LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + Form1.f1.CH2PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + Form1.f1.CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + Form1.f1.CH3PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + Form1.f1.CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + Form1.f1.CH4PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + Form1.f1.CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + Form1.f1.CH3PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + Form1.f1.CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + Form1.f1.CH4PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + Form1.f1.CH4LeakUnit.Text + "),\n");
                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }



        private void SaveFlowRunLog2()
        {
            try
            {
                string fileName;
                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(DateTime.Now.ToString("yyyyMMdd")))
                        listFileSystemInfo.Add(fsinfo);
                }

                if (listFileSystemInfo.Count == 1)
                {
                    string maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                    file = file + "_" + "01";
                }
                else if (listFileSystemInfo.Count > 1)
                {
                    string maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                    string suffixCurrentNum = maxFile.Split('_')[1];
                    string suffixMaxNum = (Convert.ToInt32(suffixCurrentNum) + 1).ToString().PadLeft(2, '0');
                    file = file + "_" + suffixMaxNum;
                }

                string name = file + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, "气袋产品测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + Form1.f1.WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + Form1.f1.ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + Form1.f1.ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + Form1.f1.TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + Form1.f1.ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + Form1.f1.ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Form1.f1.Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + Form1.f1.machinepath + "\n");

                    {

                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");


                        //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + Form1.f1.elec.CH1ElecMin + "-" + Form1.f1.elec.CH1ElecMax + "uA),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + Form1.f1.elec.CH1UPADCMin + "-" + Form1.f1.elec.CH1UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + Form1.f1.elec.CH1UPVDCMin + "-" + Form1.f1.elec.CH1UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Form1.f1.Flow.CH1_1PreMin + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Form1.f1.Flow.CH1_2PreMax.ToString() + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Form1.f1.Flow.CH1_2FlowMax.ToString() + "-" + Form1.f1.Flow.CH1_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + Form1.f1.elec.CH1DOWNADCMin + "-" + Form1.f1.elec.CH1DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + Form1.f1.elec.CH1DOWNVDCMin + "-" + Form1.f1.elec.CH1DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Form1.f1.Flow.CH1_2PreMin + "-" + Form1.f1.Flow.CH1_2PreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Form1.f1.Flow.CH1_1PreMin + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");

                        //if (!Electricity.ord.CH1UpDownChange)
                        //{
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + Form1.f1. elec.CH1UPADCMin + "-" + Form1.f1 .elec.CH1UPADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" +Form1.f1. elec.CH1UPVDCMin + "-" +Form1.f1. elec.CH1UPVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" +Form1.f1. Flow.CH1_1PreMin + "-" +Form1.f1. Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" +Form1.f1. Flow.CH1_2PreMax.ToString() + "-" +Form1.f1. Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" +Form1.f1. Flow.CH1_2FlowMax.ToString() + "-" +Form1.f1. Flow.CH1_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" +Form1.f1. elec.CH1DOWNADCMin + "-" +Form1.f1. elec.CH1DOWNADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" +Form1.f1. elec.CH1DOWNVDCMin + "-" +Form1.f1. elec.CH1DOWNVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" +Form1.f1. Flow.CH1_2PreMin + "-" +Form1.f1. Flow.CH1_2PreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" +Form1.f1. Flow.CH1_1PreMin + "-" +Form1.f1. Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");
                        //}
                        //else
                        //{
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" +Form1.f1. elec.CH1UPADCMin + "-" +Form1.f1. elec.CH1UPADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" +Form1.f1. elec.CH1UPVDCMin + "-" + Form1.f1.elec.CH1UPVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Form1.f1.Flow.CH1_1PreMin + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Form1.f1.Flow.CH1_2PreMax.ToString() + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Form1.f1.Flow.CH1_2FlowMax.ToString() + "-" + Form1.f1.Flow.CH1_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + Form1.f1.elec.CH1DOWNADCMin + "-" + Form1.f1.elec.CH1DOWNADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + Form1.f1.elec.CH1DOWNVDCMin + "-" + Form1.f1.elec.CH1DOWNVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Form1.f1.Flow.CH1_2PreMin + "-" + Form1.f1.Flow.CH1_2PreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Form1.f1.Flow.CH1_1PreMin + "-" + Form1.f1.Flow.CH1_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");
                        //}

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Form1.f1.Flow.CH1Cont_ElecMin + "-" + Form1.f1.Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Form1.f1.Flow.CH1Cont_PressMin + "-" + Form1.f1.Flow.CH1Cont_PressMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + Form1.f1.elec.TotalFlowMin + "-" + Form1.f1.elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + Form1.f1.elec.TotalPreMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + Form1.f1.elec.CH1FWDADCMin + "-" + Form1.f1.elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + Form1.f1.elec.CH1FWDVDCMin + "-" + Form1.f1.elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + Form1.f1.elec.CH1RWDADCMin + "-" + Form1.f1.elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + Form1.f1.elec.CH1RWDVDCMin + "-" + Form1.f1.elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Form1.f1.Flow.CH1RWDPressMin + "-" + Form1.f1.Flow.CH1RWDPressMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Form1.f1.Flow.CH1RWDPressMin + "-" + Form1.f1.Flow.CH1RWDPressMax + Form1.f1.CH2PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + Form1.f1.elec.CH2ElecMin + "-" + Form1.f1.elec.CH2ElecMax + "uA),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + Form1.f1.elec.CH2ElecMin + "-" + Form1.f1.elec.CH2ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + Form1.f1.elec.CH2UPADCMin + "-" + Form1.f1.elec.CH2UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + Form1.f1.elec.CH2UPVDCMin + "-" + Form1.f1.elec.CH2UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Form1.f1.Flow.CH2_2PreMax.ToString() + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Form1.f1.Flow.CH2_2FlowMax.ToString() + "-" + Form1.f1.Flow.CH2_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + Form1.f1.elec.CH2DOWNADCMin + "-" + Form1.f1.elec.CH2DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + Form1.f1.elec.CH2DOWNVDCMin + "-" + Form1.f1.elec.CH2DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Form1.f1.Flow.CH2_2PreMin + "-" + Form1.f1.Flow.CH2_2PreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");
                        //if (!Electricity.ord.CH2UpDownChange)
                        //{
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + Form1.f1.elec.CH2ElecMin + "-" + Form1.f1.elec.CH2ElecMax + "uA),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + Form1.f1.elec.CH2UPADCMin + "-" + Form1.f1.elec.CH2UPADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + Form1.f1.elec.CH2UPVDCMin + "-" + Form1.f1.elec.CH2UPVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Form1.f1.Flow.CH2_2PreMax.ToString() + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Form1.f1.Flow.CH2_2FlowMax.ToString() + "-" + Form1.f1.Flow.CH2_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + Form1.f1.elec.CH2DOWNADCMin + "-" + Form1.f1.elec.CH2DOWNADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + Form1.f1.elec.CH2DOWNVDCMin + "-" + Form1.f1.elec.CH2DOWNVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Form1.f1.Flow.CH2_2PreMin + "-" + Form1.f1.Flow.CH2_2PreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");

                        //}
                        //else
                        //{
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + Form1.f1.elec.CH2ElecMin + "-" + Form1.f1.elec.CH2ElecMax + "uA),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + Form1.f1.elec.CH2UPADCMin + "-" + Form1.f1.elec.CH2UPADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + Form1.f1.elec.CH2UPVDCMin + "-" + Form1.f1.elec.CH2UPVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Form1.f1.Flow.CH2_2PreMax.ToString() + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Form1.f1.Flow.CH2_2FlowMax.ToString() + "-" + Form1.f1.Flow.CH2_2FlowMin + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + Form1.f1.elec.CH2DOWNADCMin + "-" + Form1.f1.elec.CH2DOWNADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + Form1.f1.elec.CH2DOWNVDCMin + "-" + Form1.f1.elec.CH2DOWNVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Form1.f1.Flow.CH2_2PreMin + "-" + Form1.f1.Flow.CH2_2PreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Form1.f1.Flow.CH2_1PreMin + "-" + Form1.f1.Flow.CH2_1PreMax + Form1.f1.PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");

                        //}

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Form1.f1.Flow.CH2Cont_ElecMin + "-" + Form1.f1.Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Form1.f1.Flow.CH2Cont_PressMin + "-" + Form1.f1.Flow.CH2Cont_PressMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + Form1.f1.elec.TotalFlowMin + "-" + Form1.f1.elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + Form1.f1.elec.CH2FWDADCMin + "-" + Form1.f1.elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + Form1.f1.elec.CH2FWDVDCMin + "-" + Form1.f1.elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + Form1.f1.elec.CH2RWDADCMin + "-" + Form1.f1.elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + Form1.f1.elec.CH2RWDVDCMin + "-" + Form1.f1.elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Form1.f1.Flow.CH2RWDPressMin + "-" + Form1.f1.Flow.CH2RWDPressMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Form1.f1.Flow.CH2RWDPressMin + "-" + Form1.f1.Flow.CH2RWDPressMax + Form1.f1.CH4PressureUnit.Text + "),");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + Form1.f1.ch1_1leakparams.FPlowlimit + "-" + Form1.f1.ch1_1leakparams.FPtoplimit + Form1.f1.PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + Form1.f1.ch1_1leakparams.BalanPreMin + "-" + Form1.f1.ch1_1leakparams.BalanPreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + Form1.f1.ch1_1leakparams.Leaklowlimit + "-" + Form1.f1.ch1_1leakparams.Leaktoplimit + Form1.f1.LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + Form1.f1.ch1_2leakparams.FPlowlimit + "-" + Form1.f1.ch1_2leakparams.FPtoplimit + Form1.f1.CH2PressureUnit.Text + "),");
                  //      fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + Form1.f1.ch1_2leakparams.BalanPreMin + "-" + Form1.f1.ch1_2leakparams.BalanPreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + Form1.f1.ch1_2leakparams.Leaklowlimit + "-" + Form1.f1.ch1_2leakparams.Leaktoplimit + Form1.f1.CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + Form1.f1.ch1_1leakparams.FPlowlimit + "-" + Form1.f1.ch1_1leakparams.FPtoplimit + Form1.f1.PressureUnit.Text + "),");
                //        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + Form1.f1.ch1_1leakparams.BalanPreMin + "-" + Form1.f1.ch1_1leakparams.BalanPreMax + Form1.f1.PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + Form1.f1.ch1_1leakparams.Leaklowlimit + "-" + Form1.f1.ch1_1leakparams.Leaktoplimit + Form1.f1.LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + Form1.f1.ch1_2leakparams.FPlowlimit + "-" + Form1.f1.ch1_2leakparams.FPtoplimit + Form1.f1.CH2PressureUnit.Text + "),");
                   //     fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + Form1.f1.ch1_2leakparams.BalanPreMin + "-" + Form1.f1.ch1_2leakparams.BalanPreMax + Form1.f1.CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + Form1.f1.ch1_2leakparams.Leaklowlimit + "-" + Form1.f1.ch1_2leakparams.Leaktoplimit + Form1.f1.CH2LeakUnit.Text + "),");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + Form1.f1.ch2_1leakparams.FPlowlimit + "-" + Form1.f1.ch2_1leakparams.FPtoplimit + Form1.f1.CH3PressureUnit.Text + "),");
            //            fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + Form1.f1.ch2_1leakparams.BalanPreMin + "-" + Form1.f1.ch2_1leakparams.BalanPreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + Form1.f1.ch2_1leakparams.Leaklowlimit + "-" + Form1.f1.ch2_1leakparams.Leaktoplimit + Form1.f1.CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + Form1.f1.ch2_2leakparams.FPlowlimit + "-" + Form1.f1.ch2_2leakparams.FPtoplimit + Form1.f1.CH4PressureUnit.Text + "),");
             //           fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + Form1.f1.ch2_2leakparams.BalanPreMin + "-" + Form1.f1.ch2_2leakparams.BalanPreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + Form1.f1.ch2_2leakparams.Leaklowlimit + "-" + Form1.f1.ch2_2leakparams.Leaktoplimit + Form1.f1.CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + Form1.f1.ch2_1leakparams.FPlowlimit + "-" + Form1.f1.ch2_1leakparams.FPtoplimit + Form1.f1.CH3PressureUnit.Text + "),");
                    //    fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + Form1.f1.ch2_1leakparams.BalanPreMin + "-" + Form1.f1.ch2_1leakparams.BalanPreMax + Form1.f1.CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + Form1.f1.ch2_1leakparams.Leaklowlimit + "-" + Form1.f1.ch2_1leakparams.Leaktoplimit + Form1.f1.CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + Form1.f1.ch2_2leakparams.FPlowlimit + "-" + Form1.f1.ch2_2leakparams.FPtoplimit + Form1.f1.CH4PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + Form1.f1.ch2_2leakparams.BalanPreMin + "-" + Form1.f1.ch2_2leakparams.BalanPreMax + Form1.f1.CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + Form1.f1.ch2_2leakparams.Leaklowlimit + "-" + Form1.f1.ch2_2leakparams.Leaktoplimit + Form1.f1.CH4LeakUnit.Text + "),\n");
                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        private void ADQuiescentCurrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (ADQuiescentCurrnt.Checked && !ADOrder.Contains("QC"))
            {
                ADOrder.Add("QC");
                ADQuiescentCurrntIndex.Text = ((ADOrder.IndexOf("QC") == -1 ? 0 : ADOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("QC");
                ADQuiescentCurrntIndex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADUP_CheckedChanged(object sender, EventArgs e)
        {
            if (ADUP.Checked && !ADOrder.Contains("UP"))
            {
                ADOrder.Add("UP");
                ADUPindex.Text = ((ADOrder.IndexOf("UP") == -1 ? 0 : ADOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("UP");
                ADUPindex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADDOWN_CheckedChanged(object sender, EventArgs e)
        {
            if (ADDOWN.Checked && !ADOrder.Contains("DOWN"))
            {
                ADOrder.Add("DOWN");
                ADDOWNindex.Text = ((ADOrder.IndexOf("DOWN") == -1 ? 0 : ADOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("DOWN");
                ADDOWNindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADFWD_CheckedChanged(object sender, EventArgs e)
        {
            if (ADFWD.Checked && !ADOrder.Contains("FWD"))
            {
                ADOrder.Add("FWD");
                ADFWDindex.Text = ((ADOrder.IndexOf("FWD") == -1 ? 0 : ADOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("FWD");
                ADFWDindex.Text = "";
                RefreshIndex();
            }
            // WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADRWD_CheckedChanged(object sender, EventArgs e)
        {
            if (ADRWD.Checked && !ADOrder.Contains("RWD"))
            {
                ADOrder.Add("RWD");
                ADRWDindex.Text = ((ADOrder.IndexOf("RWD") == -1 ? 0 : ADOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("RWD");
                ADRWDindex.Text = "";
            }

            RefreshIndex();
            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADUPLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (ADUPLeak.Checked && !ADOrder.Contains("UPLeak"))
            {
                ADOrder.Add("UPLeak");
                ADUPLeakindex.Text = ((ADOrder.IndexOf("UPLeak") == -1 ? 0 : ADOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("UPLeak");
                ADUPLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADDOWNLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (ADDOWNLeak.Checked && !ADOrder.Contains("DOWNLeak"))
            {
                ADOrder.Add("DOWNLeak");
                ADDOWNLeakindex.Text = ((ADOrder.IndexOf("DOWNLeak") == -1 ? 0 : ADOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("DOWNLeak");
                ADDOWNLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void ADFWDLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (ADFWDLeak.Checked && !ADOrder.Contains("FWDLeak"))
            {
                ADOrder.Add("FWDLeak");
                ADFWDLeakindex.Text = ((ADOrder.IndexOf("FWDLeak") == -1 ? 0 : ADOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                ADOrder.Remove("FWDLeak");
                ADFWDLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.ADOrder = ADOrder;
        }

        private void BEQuiescentCurrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (BEQuiescentCurrnt.Checked && !BEOrder.Contains("QC"))
            {
                BEOrder.Add("QC");
                BEQuiescentCurrntIndex.Text = ((BEOrder.IndexOf("QC") == -1 ? 0 : BEOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("QC");
                BEQuiescentCurrntIndex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEUP_CheckedChanged(object sender, EventArgs e)
        {
            if (BEUP.Checked && !BEOrder.Contains("UP"))
            {
                BEOrder.Add("UP");
                BEUPindex.Text = ((BEOrder.IndexOf("UP") == -1 ? 0 : BEOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("UP");
                BEUPindex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEDOWN_CheckedChanged(object sender, EventArgs e)
        {
            if (BEDOWN.Checked && !BEOrder.Contains("DOWN"))
            {
                BEOrder.Add("DOWN");
                BEDOWNindex.Text = ((BEOrder.IndexOf("DOWN") == -1 ? 0 : BEOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("DOWN");
                BEDOWNindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEFWD_CheckedChanged(object sender, EventArgs e)
        {
            if (BEFWD.Checked && !BEOrder.Contains("FWD"))
            {
                BEOrder.Add("FWD");
                BEFWDindex.Text = ((BEOrder.IndexOf("FWD") == -1 ? 0 : BEOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("FWD");
                BEFWDindex.Text = "";
                RefreshIndex();
            }
            // WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BERWD_CheckedChanged(object sender, EventArgs e)
        {
            if (BERWD.Checked && !BEOrder.Contains("RWD"))
            {
                BEOrder.Add("RWD");
                BERWDindex.Text = ((BEOrder.IndexOf("RWD") == -1 ? 0 : BEOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("RWD");
                BERWDindex.Text = "";
            }

            RefreshIndex();
            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEUPLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (BEUPLeak.Checked && !BEOrder.Contains("UPLeak"))
            {
                BEOrder.Add("UPLeak");
                BEUPLeakindex.Text = ((BEOrder.IndexOf("UPLeak") == -1 ? 0 : BEOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("UPLeak");
                BEUPLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEDOWNLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (BEDOWNLeak.Checked && !BEOrder.Contains("DOWNLeak"))
            {
                BEOrder.Add("DOWNLeak");
                BEDOWNLeakindex.Text = ((BEOrder.IndexOf("DOWNLeak") == -1 ? 0 : BEOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("DOWNLeak");
                BEDOWNLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void BEFWDLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (BEFWDLeak.Checked && !BEOrder.Contains("FWDLeak"))
            {
                BEOrder.Add("FWDLeak");
                BEFWDLeakindex.Text = ((BEOrder.IndexOf("FWDLeak") == -1 ? 0 : BEOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                BEOrder.Remove("FWDLeak");
                BEFWDLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.BEOrder = BEOrder;
        }

        private void CFQuiescentCurrnt_CheckedChanged(object sender, EventArgs e)
        {
            if (CFQuiescentCurrnt.Checked && !CFOrder.Contains("QC"))
            {
                CFOrder.Add("QC");
                CFQuiescentCurrntIndex.Text = ((CFOrder.IndexOf("QC") == -1 ? 0 : CFOrder.IndexOf("QC")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("QC");
                CFQuiescentCurrntIndex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFUP_CheckedChanged(object sender, EventArgs e)
        {
            if (CFUP.Checked && !CFOrder.Contains("UP"))
            {
                CFOrder.Add("UP");
                CFUPindex.Text = ((CFOrder.IndexOf("UP") == -1 ? 0 : CFOrder.IndexOf("UP")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("UP");
                CFUPindex.Text = "";
                RefreshIndex();
            }

            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFDOWN_CheckedChanged(object sender, EventArgs e)
        {
            if (CFDOWN.Checked && !CFOrder.Contains("DOWN"))
            {
                CFOrder.Add("DOWN");
                CFDOWNindex.Text = ((CFOrder.IndexOf("DOWN") == -1 ? 0 : CFOrder.IndexOf("DOWN")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("DOWN");
                CFDOWNindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFFWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CFFWD.Checked && !CFOrder.Contains("FWD"))
            {
                CFOrder.Add("FWD");
                CFFWDindex.Text = ((CFOrder.IndexOf("FWD") == -1 ? 0 : CFOrder.IndexOf("FWD")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("FWD");
                CFFWDindex.Text = "";
                RefreshIndex();
            }
            // WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFRWD_CheckedChanged(object sender, EventArgs e)
        {
            if (CFRWD.Checked && !CFOrder.Contains("RWD"))
            {
                CFOrder.Add("RWD");
                CFRWDindex.Text = ((CFOrder.IndexOf("RWD") == -1 ? 0 : CFOrder.IndexOf("RWD")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("RWD");
                CFRWDindex.Text = "";
            }

            RefreshIndex();
            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFUPLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CFUPLeak.Checked && !CFOrder.Contains("UPLeak"))
            {
                CFOrder.Add("UPLeak");
                CFUPLeakindex.Text = ((CFOrder.IndexOf("UPLeak") == -1 ? 0 : CFOrder.IndexOf("UPLeak")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("UPLeak");
                CFUPLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFDOWNLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CFDOWNLeak.Checked && !CFOrder.Contains("DOWNLeak"))
            {
                CFOrder.Add("DOWNLeak");
                CFDOWNLeakindex.Text = ((CFOrder.IndexOf("DOWNLeak") == -1 ? 0 : CFOrder.IndexOf("DOWNLeak")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("DOWNLeak");
                CFDOWNLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void CFFWDLeak_CheckedChanged(object sender, EventArgs e)
        {
            if (CFFWDLeak.Checked && !CFOrder.Contains("FWDLeak"))
            {
                CFOrder.Add("FWDLeak");
                CFFWDLeakindex.Text = ((CFOrder.IndexOf("FWDLeak") == -1 ? 0 : CFOrder.IndexOf("FWDLeak")) + 1).ToString();
            }
            else
            {
                CFOrder.Remove("FWDLeak");
                CFFWDLeakindex.Text = "";
                RefreshIndex();
            }
            //WriteLin();
            Form1.f1.CFOrder = CFOrder;
        }

        private void OtherParaNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = ParaNum.SelectedIndex + 1;
            ReadParameters(MachineNum.SelectedIndex + 1, i);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void uiGroupBox9_Click(object sender, EventArgs e)
        {

        }

        private void uiGroupBox13_Click(object sender, EventArgs e)
        {

        }

        private void uiGroupBox20_Click(object sender, EventArgs e)
        {

        }

        private void uiGroupBox8_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void uiTabControlMenu2_Click(object sender, EventArgs e)
        {
           
        }

        private void Electricity_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void uiGroupBox33_Click(object sender, EventArgs e)
        {

        }

        private void CH2FWDFlowMax_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiGroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void uiTabControlMenu2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void uiTabControlMenu1_Click(object sender, EventArgs e)
        {
            
        }
    }
}