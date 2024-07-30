using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class FlowConfig : Form
    {
        public FlowConfig()
        {
            InitializeComponent();
        }

        private void FlowConfig_Load(object sender, EventArgs e)
        {
            ReadFlow();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                Form1.f1.plc.ch1rwdtime = (short)(Convert.ToInt16(Convert.ToDouble(CH1RWDOverTime.Text) * 10));
                Form1.f1.plc.CH1RWDOvertime();
                Form1.f1.plc.ch2rwdtime = (short)(Convert.ToInt16(Convert.ToDouble(CH2RWDOverTime.Text) * 10));
                Form1.f1.plc.CH2RWDOvertime();
                Model.Flow Contrast = new Model.Flow();
                //string dialog = "Flow.ini";
                //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
                string dialog = Form1.f1.machine;
                ConfigINI config = new ConfigINI("Model", dialog);
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

                Contrast.CH1OverTime = Convert.ToDouble(CH1OverTime.Text);
                Contrast.CH2OverTime = Convert.ToDouble(CH2OverTime.Text);
                Contrast.CH3OverTime = Convert.ToDouble(CH3OverTime.Text);
                Contrast.CH4OverTime = Convert.ToDouble(CH4OverTime.Text);
                Contrast.CH1Press_OverTime = Convert.ToDouble(CH1Press_OverTime.Text);
                Contrast.CH2Press_OverTime = Convert.ToDouble(CH2Press_OverTime.Text);
                Contrast.CH3Press_OverTime = Convert.ToDouble(CH3Press_OverTime.Text);
                Contrast.CH4Press_OverTime = Convert.ToDouble(CH2Press_OverTime.Text);
                Contrast.CH1_1FlowMax = Convert.ToDouble(CH1_1FlowMax.Text);
                Contrast.CH1_2FlowMax = Convert.ToDouble(CH1_2FlowMax.Text);
                Contrast.CH2_1FlowMax = Convert.ToDouble(CH2_1FlowMax.Text);
                Contrast.CH2_2FlowMax = Convert.ToDouble(CH2_2FlowMax.Text);
                Contrast.CH1_1FlowMin = Convert.ToDouble(CH1_1FlowMin.Text);
                Contrast.CH1_2FlowMin = Convert.ToDouble(CH1_2FlowMin.Text);
                Contrast.CH2_1FlowMin = Convert.ToDouble(CH2_1FlowMin.Text);
                Contrast.CH2_2FlowMin = Convert.ToDouble(CH2_2FlowMin.Text);
                Contrast.CH1Cont_ElecMax = Convert.ToDouble(CH1Cont_ElecMax.Text);
                Contrast.CH1Cont_ElecMin = Convert.ToDouble(CH1Cont_ElecMin.Text);
                Contrast.CH1Cont_Elec_Compen = Convert.ToDouble(CH1Cont_Elec_Compen.Text);
                Contrast.CH2Cont_ElecMax = Convert.ToDouble(CH2Cont_ElecMax.Text);
                Contrast.CH2Cont_ElecMin = Convert.ToDouble(CH2Cont_ElecMin.Text);
                Contrast.CH2Cont_Elec_Compen = Convert.ToDouble(CH2Cont_Elec_Compen.Text);
                Contrast.CH1Cont_PressMax = Convert.ToDouble(CH1Cont_PressMax.Text);
                Contrast.CH1Cont_PressMin = Convert.ToDouble(CH1Cont_PressMin.Text);
                Contrast.CH1Cont_Pre_Compen = Convert.ToDouble(CH1Cont_Pre_Compen.Text);
                Contrast.CH2Cont_PressMax = Convert.ToDouble(CH2Cont_PressMax.Text);
                Contrast.CH2Cont_PressMin = Convert.ToDouble(CH2Cont_PressMin.Text);
                Contrast.CH2Cont_Pre_Compen = Convert.ToDouble(CH2Cont_Pre_Compen.Text);
                Contrast.CH1RWDPressMax = Convert.ToDouble(CH1RWDPressMax.Text);
                Contrast.CH2RWDPressMax = Convert.ToDouble(CH2RWDPressMax.Text);
                Contrast.CH1RWDPressMin = Convert.ToDouble(CH1RWDPressMin.Text);
                Contrast.CH2RWDPressMin = Convert.ToDouble(CH2RWDPressMin.Text);
                Contrast.CH1RWDOverTime = Convert.ToDouble(CH1RWDOverTime.Text);
                Contrast.CH2RWDOverTime = Convert.ToDouble(CH2RWDOverTime.Text);
                Contrast.CH1_1PreMax = Convert.ToDouble(CH1_1PreMax.Text);
                Contrast.CH1_1PreMin = Convert.ToDouble(CH1_1PreMin.Text);
                Contrast.CH1_2PreMax = Convert.ToDouble(CH1_2PreMax.Text);
                Contrast.CH1_2PreMin = Convert.ToDouble(CH1_2PreMin.Text);

                Form1.f1.Flow = Contrast;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // <summary>
        /// 读取罐子体积和超时时间
        /// </summary>
        private void ReadFlow()
        {
            //string dialog;
            //dialog = "Flow.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            ReadConfig con = new ReadConfig();
            Model.Flow flow = con.ReadFlow();
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
            CH1RWDPressMax.Text = flow.CH1RWDPressMax.ToString();
            CH2RWDPressMax.Text = flow.CH2RWDPressMax.ToString();
            CH1RWDPressMin.Text = flow.CH1RWDPressMin.ToString();
            CH2RWDPressMin.Text = flow.CH2RWDPressMin.ToString();
            CH1RWDOverTime.Text = flow.CH1RWDOverTime.ToString();
            CH2RWDOverTime.Text = flow.CH2RWDOverTime.ToString();
            CH1_1PreMax.Text = flow.CH1_1PreMax.ToString();
            CH1_1PreMin.Text = flow.CH2Cont_PressMax.ToString();
            CH1_2PreMax.Text = flow.CH1_1PreMin.ToString();
            CH1_2PreMin.Text = flow.CH1_2PreMin.ToString();
        }
    }
}