using System;
using System.Linq;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class TestParams : Form
    {
        //public static TestParams testparams;
        //public int stage;
        //Model.CH_PARAMS ch1params = new Model.CH_PARAMS();
        //Model.CH_PARAMS ch2params = new Model.CH_PARAMS();
        //Model.CH_PARAMS ch3params = new Model.CH_PARAMS();
        //Model.CH_PARAMS ch4params = new Model.CH_PARAMS();

        public TestParams()
        {
            InitializeComponent();
            //testparams = this;
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                MessageBox.Show(ex.Message);
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
                BtnRead.Enabled = false;
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
                MessageBox.Show(ex.Message);
                BtnRead.Enabled = true;
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
            BtnRead.Enabled = true;

        }
        private void BtnKeep_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(ParaNum.Text);
            SetParameters(MachineNum.SelectedIndex + 1, i);
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
            mesconfig.IniWriteValue("Parameters", CH + "params_number", ParaNum.Text);
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
            mesconfig.IniWriteValue("Parameters", CH + "unit" + i, CHKUnit.Checked.ToString());
            mesconfig.IniWriteValue("Parameters", CH + "presscompensation", PressCompensation.Text);
            MessageBox.Show("仪器编号：" + CH + "保存" + i + "组参数成功！");
        }



        //读取注册表的参数编号
        private void ReadParametersNumber(int CH)
        {
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string paramnum = config.IniReadValue("Parameters", CH + "params_number");
            if (!String.IsNullOrEmpty(paramnum))
            {
                ParaNum.Text = paramnum;
                ReadParameters(CH, Convert.ToInt32(ParaNum.Text));
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

        private void TestParams_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    SerialPort1.Close();
            //    if (Form1.f1.label6.Text == "已连接")
            //    {
            //        Form1.f1.SerialPort1.Open();
            //        Form1.f1.IsRun.Interval = 800;
            //        Form1.f1.IsRun.Start();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
            ReadParametersNumber(MachineNum.SelectedIndex + 1);
            ReadParameters(MachineNum.SelectedIndex + 1, ParaNum.SelectedIndex + 1);
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

        /// <summary>
        /// 存储单位勾选
        /// </summary>
        /// <param name="CH"></param>
        //private void SetPressUnit(int CH)
        //{

        //    RegistryKey regName;

        //    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + Form1.f1.machine, true);

        //    if (regName is null)
        //    {
        //        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + Form1.f1.machine);
        //    }

        //    regName.SetValue(CH + "unit", CHKUnit.Checked.ToString());

        //    regName.Close();
        //}

        /// <summary>
        /// 读取压力补偿和测试单位
        /// </summary>
        /// <param name="CH"></param>
        //private void ReadPressConfig(int CH)
        //{
        //    RegistryKey regName;

        //    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + Form1.f1.machine, true);

        //    if (regName is null)
        //    {
        //        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + Form1.f1.machine);
        //    }

        //    if (regName.GetValue(CH + "presscompensation") == null)
        //    {
        //        PressCompensation.Text = null;
        //    }
        //    else
        //    {
        //        regName.OpenSubKey("User");
        //        PressCompensation.Text = regName.GetValue(CH + "presscompensation").ToString();
        //    }
        //    if (regName.GetValue(CH + "unit") == null)
        //    {
        //        CHKUnit.Checked = false;
        //    }
        //    else
        //    {
        //        regName.OpenSubKey("User");
        //        CHKUnit.Checked = Convert.ToBoolean(regName.GetValue(CH + "unit"));
        //    }
        //    //ReadParameters(CH, Convert.ToInt32(ParaNum.Text));
        //}

        //private void CHKUnit_CheckedChanged(object sender, EventArgs e)
        //{
        //    switch (MachineNum.SelectedIndex + 1)
        //    {
        //        case 1:
        //            SetPressUnit(1);
        //            Form1.f1.ch1chkunit = CHKUnit.Checked;
        //            break;
        //        case 2:
        //            SetPressUnit(2);
        //            Form1.f1.ch2chkunit = CHKUnit.Checked;
        //            break;
        //        case 3:
        //            SetPressUnit(3);
        //            Form1.f1.ch3chkunit = CHKUnit.Checked;
        //            break;
        //        case 4:
        //            SetPressUnit(4);
        //            Form1.f1.ch4chkunit = CHKUnit.Checked;
        //            break;
        //    }
        //}


    }
}
