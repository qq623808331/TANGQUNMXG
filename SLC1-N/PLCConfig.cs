using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class PLCConfig : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        LIN CH1lin;
        LIN CH2lin;
        List<string> CH1Order = new List<string>();
        List<string> CH2Order = new List<string>();

        public PLCConfig()
        {
            InitializeComponent();
        }

        private void PLCConfig_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;
            //ReadFlow();
            CH1Order = Form1.f1.CH1Order;
            CH2Order = Form1.f1.CH2Order;
            Form1.f1.plc.ReadCH = true;
            if (Form1.f1.plc.PLCIsRun)
            {
                PLCReConnect.Enabled = false;
                ReadSignal.Interval = 200;
                ReadSignal.Start();
            }
            else
            {
                PLCReConnect.Enabled = true;
                PLCRun.BackColor = Color.Red;
                Front_SafetyDoor.BackColor = Color.White;
                Back_SafetyDoorUp.BackColor = Color.White;
                Back_SafetyDoorDown.BackColor = Color.White;
                Left_SafetyDoor.BackColor = Color.White;
                Right_SafetyDoor.BackColor = Color.White;
                CH1Stopping.BackColor = Color.White;
                CH2Stopping.BackColor = Color.White;
                Stopping.BackColor = Color.White;
                CH1Reset.BackColor = Color.White;
                CH2Reset.BackColor = Color.White;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));//PLC not communicating
            }
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        /// <summary>
        /// 循环读取本页面的实时信号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadSignal_Tick(object sender, EventArgs e)
        {
            ReadSignal.Interval = 600;
            if (Form1.f1.plc.PLCIsRun)
            {
                PLCRun.BackColor = Color.Green;
                if (Form1.f1.plc.Front_SafetyDoor)
                {
                    Front_SafetyDoor.BackColor = Color.Red;
                }
                else
                {
                    Front_SafetyDoor.BackColor = Color.Green;
                }
                if (Form1.f1.plc.Back_SafetyDoorUp)
                {
                    Back_SafetyDoorUp.BackColor = Color.Red;
                }
                else
                {
                    Back_SafetyDoorUp.BackColor = Color.Green;
                }
                if (Form1.f1.plc.Back_SafetyDoorDown)
                {
                    Back_SafetyDoorDown.BackColor = Color.Red;
                }
                else
                {
                    Back_SafetyDoorDown.BackColor = Color.Green;
                }
                if (Form1.f1.plc.Left_SafetyDoor)
                {
                    Left_SafetyDoor.BackColor = Color.Red;
                }
                else
                {
                    Left_SafetyDoor.BackColor = Color.Green;
                }
                if (Form1.f1.plc.Right_SafetyDoor)
                {
                    Right_SafetyDoor.BackColor = Color.Red;
                }
                else
                {
                    Right_SafetyDoor.BackColor = Color.Green;
                }
                if (Form1.f1.plc.CH1Stopping)
                {
                    CH1Stopping.BackColor = Color.Red;
                }
                else
                {
                    CH1Stopping.BackColor = Color.Green;
                }
                if (Form1.f1.plc.CH2Stopping)
                {
                    CH2Stopping.BackColor = Color.Red;
                }
                else
                {
                    CH2Stopping.BackColor = Color.Green;
                }
                if (Form1.f1.plc.Stopping)
                {
                    Stopping.BackColor = Color.Red;
                }
                else
                {
                    Stopping.BackColor = Color.Green;
                }
                if (Form1.f1.plc.CH1Reset)
                {
                    CH1Reset.BackColor = Color.Red;
                }
                else
                {
                    CH1Reset.BackColor = Color.Green;
                }
                if (Form1.f1.plc.CH2Reset)
                {
                    CH2Reset.BackColor = Color.Red;
                }
                else
                {
                    CH2Reset.BackColor = Color.Green;
                }
                Shield_SafetyDoor.Checked = Form1.f1.plc.Shield_SafetyDoor;
                //if (!CH1LIN.Checked)
                //{
                //    CH1UP.Checked = Form1.f1.plc.CH1UP;
                //    CH1DOWN.Checked = Form1.f1.plc.CH1DOWN;
                //    CH1FWD.Checked = Form1.f1.plc.CH1FWD;
                //}
                //if (!CH2LIN.Checked)
                //{
                //    CH2UP.Checked = Form1.f1.plc.CH2UP;
                //    CH2DOWN.Checked = Form1.f1.plc.CH2DOWN;
                //    CH2FWD.Checked = Form1.f1.plc.CH2FWD;
                //}
                CH1Shield_Bee.Checked = Form1.f1.plc.CH1Bee;
                CH2Shield_Bee.Checked = Form1.f1.plc.CH2Bee;
                CH1OpenClose.Checked = Form1.f1.plc.CH1;
                CH2OpenClose.Checked = Form1.f1.plc.CH2;
                CH3OpenClose.Checked = Form1.f1.plc.CH3;
                CH4OpenClose.Checked = Form1.f1.plc.CH4;
            }
            else
            {
                PLCRun.BackColor = Color.Red;
            }

        }
        private void PLCReConnect_Click(object sender, EventArgs e)
        {
            Form1.f1.PLC_Con();
            ReadSignal.Interval = 1500;
            ReadSignal.Start();
        }

        //CH1滑轨气缸伸出按下
        private void CH1SRCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH1SRCUP.Interval = 300;
            Timer_CH1SRCUP.Start();
            CH1SRCylinderUP.Enabled = false;
        }
        private void Timer_CH1SRCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH1SRCUP.Stop();
            CH1SRCylinderUP.Enabled = true;
        }
        private void CH1SRCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1HuaGuiShang1();
        }

        private void CH1SRCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1HuaGuiShang2();
        }
        //CH1滑轨气缸缩回按下
        private void CH1SRCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH1SRCDOWN.Interval = 300;
            Timer_CH1SRCDOWN.Start();
            CH1SRCylinderDown.Enabled = false;
        }
        private void Timer_CH1SRCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH1SRCDOWN.Stop();
            CH1SRCylinderDown.Enabled = true;
        }
        private void CH1SRCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1HuaGuiXia1();
        }

        private void CH1SRCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1HuaGuiXia2();
        }
        //CH1侧推气缸伸出按下
        private void CH1SPCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH1SPCUP.Interval = 300;
            Timer_CH1SPCUP.Start();
            CH1SPCylinderUP.Enabled = false;
        }
        private void Timer_CH1SPCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH1SPCUP.Stop();
            CH1SPCylinderUP.Enabled = true;



        }
        private void CH1SPCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1CeTuiShang1();
        }

        private void CH1SPCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1CeTuiShang2();
        }
        //CH1侧推气缸缩回按下
        private void CH1SPCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH1SPCDOWN.Interval = 300;
            Timer_CH1SPCDOWN.Start();
            CH1SPCylinderDown.Enabled = false;
        }

        private void Timer_CH1SPCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH1SPCDOWN.Stop();
            CH1SPCylinderDown.Enabled = true;
        }

        private void CH1SPCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1CeTuiXia1();
        }

        private void CH1SPCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1CeTuiXia2();
        }
        //CH1飞针气缸伸出按下
        private void CH1FNCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH1FNCUP.Interval = 300;
            Timer_CH1FNCUP.Start();
            CH1FNCylinderUP.Enabled = false;
        }
        private void Timer_CH1FNCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH1FNCUP.Stop();
            CH1FNCylinderUP.Enabled = true;
        }
        private void CH1FNCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1FeiZhenShang1();
        }

        private void CH1FNCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1FeiZhenShang2();
        }
        //CH1飞针气缸缩回按下
        private void CH1FNCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH1FNCDOWN.Interval = 300;
            Timer_CH1FNCDOWN.Start();
            CH1FNCylinderDown.Enabled = false;
        }

        private void Timer_CH1FNCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH1FNCDOWN.Stop();
            CH1FNCylinderDown.Enabled = true;
        }

        private void CH1FNCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1FeiZhenXia1();
        }

        private void CH1FNCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1FeiZhenXia2();
        }
        //CH1充气气缸伸出按下
        private void CH1PCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH1PCUP.Interval = 300;
            Timer_CH1PCUP.Start();
            CH1PCylinderUP.Enabled = false;
        }
        private void Timer_CH1PCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH1PCUP.Stop();
            CH1PCylinderUP.Enabled = true;
        }

        private void CH1PCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1ChongQiShang1();
        }

        private void CH1PCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1ChongQiShang2();
        }
        //CH1充气气缸缩回按下
        private void CH1PCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH1PCDOWN.Interval = 300;
            Timer_CH1PCDOWN.Start();
            CH1PCylinderDown.Enabled = false;
        }

        private void Timer_CH1PCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH1PCDOWN.Stop();
            CH1PCylinderDown.Enabled = true;
        }

        private void CH1PCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1ChongQiXia1();
        }

        private void CH1PCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH1ChongQiXia2();
        }
        //CH2滑轨气缸伸出按下
        private void CH2SRCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH2SRCUP.Interval = 300;
            Timer_CH2SRCUP.Start();
            CH2SRCylinderUP.Enabled = false;
        }

        private void Timer_CH2SRCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH2SRCUP.Stop();
            CH2SRCylinderUP.Enabled = true;
        }

        private void CH2SRCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2HuaGuiShang1();
        }

        private void CH2SRCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2HuaGuiShang2();
        }
        //CH2滑轨气缸缩回按下
        private void CH2SRCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH2SRCDOWN.Interval = 300;
            Timer_CH2SRCDOWN.Start();
            CH2SRCylinderDown.Enabled = false;
        }

        private void Timer_CH2SRCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH2SRCDOWN.Stop();
            CH2SRCylinderDown.Enabled = true;
        }

        private void CH2SRCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2HuaGuiXia1();
        }

        private void CH2SRCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2HuaGuiXia2();
        }
        //CH2侧推气缸伸出按下
        private void CH2SPCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH2SPCUP.Interval = 300;
            Timer_CH2SPCUP.Start();
            CH2SPCylinderUP.Enabled = false;
        }

        private void Timer_CH2SPCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH2SPCUP.Stop();
            CH2SPCylinderUP.Enabled = true;
        }

        private void CH2SPCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2CeTuiShang1();
        }

        private void CH2SPCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2CeTuiShang2();
        }
        //CH2侧推气缸缩回按下
        private void CH2SPCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH2SPCDOWN.Interval = 300;
            Timer_CH2SPCDOWN.Start();
            CH2SPCylinderDown.Enabled = false;
        }

        private void Timer_CH2SPCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH2SPCDOWN.Stop();
            CH2SPCylinderDown.Enabled = true;
        }

        private void CH2SPCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2CeTuiXia1();
        }

        private void CH2SPCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2CeTuiXia2();
        }
        //CH2飞针气缸伸出按下
        private void CH2FNCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH2FNCUP.Interval = 300;
            Timer_CH2FNCUP.Start();
            CH2FNCylinderUP.Enabled = false;
        }

        private void Timer_CH2FNCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH2FNCUP.Stop();
            CH2FNCylinderUP.Enabled = true;
        }

        private void CH2FNCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2FeiZhenShang1();
        }

        private void CH2FNCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2FeiZhenShang2();
        }
        //CH2飞针气缸缩回按下
        private void CH2FNCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH2FNCDOWN.Interval = 300;
            Timer_CH2FNCDOWN.Start();
            CH2FNCylinderDown.Enabled = false;
        }

        private void Timer_CH2FNCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH2FNCDOWN.Stop();
            CH2FNCylinderDown.Enabled = true;
        }

        private void CH2FNCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2FeiZhenXia1();
        }

        private void CH2FNCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2FeiZhenXia2();
        }
        //CH2充气气缸伸出按下
        private void CH2PCylinderUP_Click(object sender, EventArgs e)
        {
            Timer_CH2PCUP.Interval = 300;
            Timer_CH2PCUP.Start();
            CH2PCylinderUP.Enabled = false;
        }

        private void Timer_CH2PCUP_Tick(object sender, EventArgs e)
        {
            Timer_CH2PCUP.Stop();
            CH2PCylinderUP.Enabled = true;
        }

        private void CH2PCylinderUP_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2ChongQiShang1();
        }

        private void CH2PCylinderUP_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2ChongQiShang2();
        }
        //CH2充气气缸缩回按下
        private void CH2PCylinderDown_Click(object sender, EventArgs e)
        {
            Timer_CH2PCDOWN.Interval = 300;
            Timer_CH2PCDOWN.Start();
            CH2PCylinderDown.Enabled = false;
        }

        private void Timer_CH2PCDOWN_Tick(object sender, EventArgs e)
        {
            Timer_CH2PCDOWN.Stop();
            CH2PCylinderDown.Enabled = true;
        }

        private void CH2PCylinderDown_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2ChongQiXia1();
        }

        private void CH2PCylinderDown_MouseUp(object sender, MouseEventArgs e)
        {
            Form1.f1.plc.CH2ChongQiXia2();
        }
        //安全门屏蔽勾选
        private void Shield_SafetyDoor_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (Shield_SafetyDoor.Checked)
                {
                    Form1.f1.plc.SafetyDoorClose();
                }
                else
                {
                    Form1.f1.plc.SafetyDoorOpen();
                }
                ReadSignal.Interval = 800;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!Shield_SafetyDoor.Checked);
                Shield_SafetyDoor.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }

        ///// <summary>
        ///// 读取罐子体积和超时时间
        ///// </summary>
        //private void ReadFlow()
        //{
        //    //string dialog;
        //    //dialog = "Flow.ini";
        //    //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
        //    //string dialog = Form1.f1.machine;
        //    //ConfigINI config = new ConfigINI("Model", dialog);
        //    ReadConfig con = new ReadConfig();
        //    Setup.LinConfig lin = con.ReadLinConfig();
        //    CH1LinBaudrate.Text = lin.CH1LinBaudrate;
        //    CH2LinBaudrate.Text = lin.CH2LinBaudrate;
        //}
        /// <summary>
        /// CH1启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1OpenClose_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1OpenClose.Checked)
                {
                    Form1.f1.plc.CH1Open();
                }
                else
                {
                    Form1.f1.plc.CH1Close();
                }
                ReadSignal.Interval = 800;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH1OpenClose.Checked);
                CH1OpenClose.Checked = check;
                MessageBox.Show("PLC未通讯");
            }
            WritePLCConfig();
        }
        /// <summary>
        /// CH2启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH2OpenClose_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2OpenClose.Checked)
                {
                    Form1.f1.plc.CH2Open();
                }
                else
                {
                    Form1.f1.plc.CH2Close();
                }
                ReadSignal.Interval = 800;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH2OpenClose.Checked);
                CH2OpenClose.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }
        /// <summary>
        /// CH3启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH3OpenClose_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH3OpenClose.Checked)
                {
                    Form1.f1.plc.CH3Open();
                }
                else
                {
                    Form1.f1.plc.CH3Close();
                }
                ReadSignal.Interval = 800;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH3OpenClose.Checked);
                CH3OpenClose.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }
        /// <summary>
        /// CH4启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH4OpenClose_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH4OpenClose.Checked)
                {
                    Form1.f1.plc.CH4Open();
                }
                else
                {
                    Form1.f1.plc.CH4Close();
                }
                ReadSignal.Interval = 800;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH4OpenClose.Checked);
                CH4OpenClose.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }

        private void ADCUDC_Click(object sender, EventArgs e)
        {
            Electricity elec = new Electricity();
            OpenForm(elec);
        }



        //防止打开多个相同的窗口
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

        private void CH1Shield_Bee_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH1Shield_Bee.Checked)
                {
                    Form1.f1.plc.CH1BeeOpen();
                }
                else
                {
                    Form1.f1.plc.CH1BeeClose();
                }
                ReadSignal.Interval = 1600;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH1Shield_Bee.Checked);
                CH1Shield_Bee.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }

        private void CH2Shield_Bee_Click(object sender, EventArgs e)
        {
            ReadSignal.Stop();
            if (Form1.f1.plc.PLCIsRun)
            {
                if (CH2Shield_Bee.Checked)
                {
                    Form1.f1.plc.CH2BeeOpen();
                }
                else
                {
                    Form1.f1.plc.CH2BeeClose();
                }
                ReadSignal.Interval = 1600;
                ReadSignal.Start();
            }
            else
            {
                bool check = (!CH2Shield_Bee.Checked);
                CH2Shield_Bee.Checked = check;
                MessageBox.Show(I18N.GetLangText(dicLang, "PLC未通讯"));
            }
            WritePLCConfig();
        }

        private void PLCConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.f1.plc.ReadCH = false;
        }

        private void ValveAction_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1ValveAction();
            if (Form1.f1.plc.CH1LIN && !string.IsNullOrEmpty(CH1LinBaudrate.Text))
            {
                CH1lin = new LIN(1, int.Parse(CH1LinBaudrate.Text));
                CH1lin.LinComm();
                CH1LinComm.Interval = 500;
                CH1LinComm.Start();
            }
        }

        private void ValActionStop_Click(object sender, EventArgs e)
        {
            CH1LinComm.Stop();
            Form1.f1.plc.CH1VActionStop();
        }

        private void CH2ValveAction_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2ValveAction();
            if (Form1.f1.plc.CH2LIN)
            {
                CH2lin = new LIN(0, int.Parse(CH2LinBaudrate.Text));
                CH2lin.LinComm();
                CH2LinComm.Interval = 500;
                CH2LinComm.Start();
            }
        }

        private void CH2ValActionStop_Click(object sender, EventArgs e)
        {
            CH2LinComm.Stop();
            Form1.f1.plc.CH2VActionStop();
        }
        /// <summary>
        /// CH1阀泵单独启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1LinComm_Tick(object sender, EventArgs e)
        {
            if (Form1.f1.CH1Order.Contains("UP"))
            {
                CH1lin.LinUP();
            }
            else if (Form1.f1.CH1Order.Contains("DOWN"))
            {
                CH1lin.LinDOWN();
            }
            else if (Form1.f1.CH1Order.Contains("FWD"))
            {
                CH1lin.LinFWD();
            }
            else if (Form1.f1.CH1Order.Contains("RWD"))
            {
                CH1lin.LinRWD();
            }
        }
        /// <summary>
        /// CH2阀泵单独启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH2LinComm_Tick(object sender, EventArgs e)
        {
            if (Form1.f1.CH2Order.Contains("UP"))
            {
                CH2lin.LinUP();
            }
            else if (Form1.f1.CH2Order.Contains("DOWN"))
            {
                CH2lin.LinDOWN();
            }
            else if (Form1.f1.CH2Order.Contains("FWD"))
            {
                CH2lin.LinFWD();
            }
            else if (Form1.f1.CH2Order.Contains("RWD"))
            {
                CH2lin.LinRWD();
            }
        }



        /// <summary>
        /// 保存PLC设置
        /// </summary>
        private void WritePLCConfig()
        {
            //string dialog = "PLC.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("PLC", "safetydoor", Shield_SafetyDoor.Checked.ToString());
            config.IniWriteValue("PLC", "CH1shieldbee", CH1Shield_Bee.Checked.ToString());
            config.IniWriteValue("PLC", "CH2shieldbee", CH2Shield_Bee.Checked.ToString());
            config.IniWriteValue("PLC", "CH1openclose", CH1OpenClose.Checked.ToString());
            config.IniWriteValue("PLC", "CH2openclose", CH2OpenClose.Checked.ToString());
            config.IniWriteValue("PLC", "CH3openclose", CH3OpenClose.Checked.ToString());
            config.IniWriteValue("PLC", "CH4openclose", CH4OpenClose.Checked.ToString());
        }


        private void Save_Click(object sender, EventArgs e)
        {
            //Setup.LinConfig lin = new Setup.LinConfig();
            //lin.CH1LinBaudrate = CH1LinBaudrate.Text;
            //lin.CH2LinBaudrate = CH2LinBaudrate.Text;
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("Lin", "CH1LinBaudrate", CH1LinBaudrate.Text);
            config.IniWriteValue("Lin", "CH2LinBaudrate", CH2LinBaudrate.Text);
            //Form1.f1.linconfig = lin;
            //if(String.IsNullOrEmpty(CH1LinBaudrate.Text))
            //{
            //    Form1.f1.CH1LinBaudrate = 10417;
            //}else
            //{
            //    Form1.f1.CH1LinBaudrate = Convert.ToInt32(CH1LinBaudrate.Text);
            //}
            //if (String.IsNullOrEmpty(CH2LinBaudrate.Text))
            //{
            //    Form1.f1.CH2LinBaudrate = 10417;
            //}
            //else
            //{
            //    Form1.f1.CH2LinBaudrate = Convert.ToInt32(CH2LinBaudrate.Text);
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH1MachineReset();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2MachineReset();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.f1.plc.CH2MachineReset();
        }
    }
}
