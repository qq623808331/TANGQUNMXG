namespace SLC1_N
{
    public class Model
    {
        public class CH_PARAMS
        {
            public string ParaName { get; set; }
            public string FullTime { get; set; }
            public string BalanTime { get; set; }
            public string TestTime1 { get; set; }
            public string ExhaustTime { get; set; }
            public string DelayTime1 { get; set; }
            public string DelayTime2 { get; set; }
            public string RelieveDelay { get; set; }
            public string Evolume { get; set; }
            public string FPtoplimit { get; set; }
            public string FPlowlimit { get; set; }
            public string BalanPreMax { get; set; }
            public string BalanPreMin { get; set; }
            public string Leaktoplimit { get; set; }
            public string Leaklowlimit { get; set; }
            public string PUnit { get; set; }
            public string LUnit { get; set; }
            public int PUnit_index { get; set; }
            public int LUnit_index { get; set; }
            public int progressBar_value { get; set; }
            public bool ChkBee { get; set; }
            public bool CHKUnit { get; set; }
            public string PressCompensation { get; set; }

        }

        public class CH_Result
        {
            public string SmallLeak { get; set; }
            public string BigLeak { get; set; }
            public string LeakPressure { get; set; }
            public string Result { get; set; }
            public int Test_Status { get; set; }
        }
        public class Electricity
        {
            /// <summary>
            ///  //新增 同充流量变量
            /// </summary>
            public double CH1FWDFlowTime { get; set; }
            public double CH2FWDFlowTime { get; set; }
            public double CH1FwdpreTime { get; set; }
            public double CH2FwdpreTime { get; set; }

            public double CH1FwdPreMax { get; set; }
            public double CH1FwdPreMin { get; set; }

            public double CH2FwdPreMax { get; set; }
            public double CH2FwdPreMin { get; set; }
            /// <summary>
            /// ///////////////////////////////////
            /// </summary>

            public double CH1FwdFlowMax { get; set; }
            public double CH1FwdFlowMin { get; set; }

            public double CH2FwdFlowMax { get; set; }
            public double CH2FwdFlowMin { get; set; }


            public double TotalFlowMax { get; set; }
            public double TotalFlowMin { get; set; }
            public double TotalPreMax { get; set; }
            public double CH1UPADCMax { get; set; }
            public double CH2UPADCMax { get; set; }
            public double CH1UPADCMin { get; set; }
            public double CH2UPADCMin { get; set; }
            public double CH1UPADCComp { get; set; }
            public double CH2UPADCComp { get; set; }
            public double CH1UPVDCMax { get; set; }
            public double CH2UPVDCMax { get; set; }
            public double CH1UPVDCMin { get; set; }
            public double CH2UPVDCMin { get; set; }
            public double CH1UPVDCComp { get; set; }
            public double CH2UPVDCComp { get; set; }
            public double CH1DOWNADCMax { get; set; }
            public double CH2DOWNADCMax { get; set; }
            public double CH1DOWNADCMin { get; set; }
            public double CH2DOWNADCMin { get; set; }
            public double CH1DOWNADCComp { get; set; }
            public double CH2DOWNADCComp { get; set; }
            public double CH1DOWNVDCMax { get; set; }
            public double CH2DOWNVDCMax { get; set; }
            public double CH1DOWNVDCMin { get; set; }
            public double CH2DOWNVDCMin { get; set; }
            public double CH1DOWNVDCComp { get; set; }
            public double CH2DOWNVDCComp { get; set; }
            public double CH1FWDADCMax { get; set; }
            public double CH2FWDADCMax { get; set; }
            public double CH1FWDADCMin { get; set; }
            public double CH2FWDADCMin { get; set; }
            public double CH1FWDADCComp { get; set; }
            public double CH2FWDADCComp { get; set; }
            public double CH1FWDVDCMax { get; set; }
            public double CH2FWDVDCMax { get; set; }
            public double CH1FWDVDCMin { get; set; }
            public double CH2FWDVDCMin { get; set; }
            public double CH1FWDVDCComp { get; set; }
            public double CH2FWDVDCComp { get; set; }
            public double CH1RWDADCMax { get; set; }
            public double CH2RWDADCMax { get; set; }
            public double CH1RWDADCMin { get; set; }
            public double CH2RWDADCMin { get; set; }
            public double CH1RWDADCComp { get; set; }
            public double CH2RWDADCComp { get; set; }
            public double CH1RWDVDCMax { get; set; }
            public double CH2RWDVDCMax { get; set; }
            public double CH1RWDVDCMin { get; set; }
            public double CH2RWDVDCMin { get; set; }
            public double CH1RWDVDCComp { get; set; }
            public double CH2RWDVDCComp { get; set; }
            public double CH1ElecMax { get; set; }
            public double CH2ElecMax { get; set; }
            public double CH1ElecMin { get; set; }
            public double CH2ElecMin { get; set; }
            public double CH1ElecComp { get; set; }
            public double CH2ElecComp { get; set; }


        }
        public class Flow
        {
            //新增同充参数240731
            public double FWDflowtime { get; set; }
            public double FWDpretime { get; set; }
            public double TotalFlowMax { get; set; }
            public double TotalFlowMin { get; set; }
            public double TotalPreMax { get; set; }


            public double CH1OverTime { get; set; }
            public double CH2OverTime { get; set; }
            public double CH3OverTime { get; set; }
            public double CH4OverTime { get; set; }
            public double CH1Press_OverTime { get; set; }
            public double CH2Press_OverTime { get; set; }
            public double CH3Press_OverTime { get; set; }
            public double CH4Press_OverTime { get; set; }
            public double CH1Cont_Pre_Compen { get; set; }
            public double CH1Cont_PressMax { get; set; }
            public double CH1Cont_PressMin { get; set; }
            public double CH2Cont_Pre_Compen { get; set; }
            public double CH2Cont_PressMax { get; set; }
            public double CH2Cont_PressMin { get; set; }
            public double CH1Cont_Elec_Compen { get; set; }
            public double CH1Cont_ElecMax { get; set; }
            public double CH1Cont_ElecMin { get; set; }
            public double CH2Cont_Elec_Compen { get; set; }
            public double CH2Cont_ElecMax { get; set; }
            public double CH2Cont_ElecMin { get; set; }
            //不充气的情况下测试阀泵的流量情况
            public double CH1_1FlowMax { get; set; }
            public double CH1_2FlowMax { get; set; }
            public double CH2_1FlowMax { get; set; }
            public double CH2_2FlowMax { get; set; }
            public double CH1_1FlowMin { get; set; }
            public double CH1_2FlowMin { get; set; }
            public double CH2_1FlowMin { get; set; }
            public double CH2_2FlowMin { get; set; }
            public double CH1RWDPressMax { get; set; }
            public double CH2RWDPressMax { get; set; }
            public double CH1RWDPressMin { get; set; }
            public double CH2RWDPressMin { get; set; }
            public double CH1RWDOverTime { get; set; }
            public double CH2RWDOverTime { get; set; }
            public double CH1_1PreMax { get; set; }
            public double CH1_1PreMin { get; set; }
            public double CH1_2PreMax { get; set; }
            public double CH1_2PreMin { get; set; }
            public double CH2_1PreMax { get; set; }
            public double CH2_1PreMin { get; set; }
            public double CH2_2PreMax { get; set; }
            public double CH2_2PreMin { get; set; }
        }
        public class TestResult
        {
            public double UP_ADCMAX { get; set; }
            public double DOWN_ADCMAX { get; set; }
            public double FWD_ADCMAX { get; set; }
            public double RWD_ADCMAX { get; set; }
            public double UP_VDCMAX { get; set; }
            public double DOWN_VDCMAX { get; set; }
            public double FWD_VDCMAX { get; set; }
            public double RWD_VDCMAX { get; set; }
            //public double UP_Elec { get; set; }
            //public double DOWN_Elec { get; set; }
            //public double FWD_Elec { get; set; }
            //public double RWD_Elec { get; set; }
            public double Elec { get; set; }
            public double UP_Flow { get; set; }
            public double DOWN_Flow { get; set; }
            public double FWD_Flow1 { get; set; }
            public double FWD_Flow2 { get; set; }
            public double RWD_Flow1 { get; set; }
            public double RWD_Flow2 { get; set; }
            public double UP_Pre { get; set; }
            public double DOWN_Pre { get; set; }
            public double FWD_Pre1 { get; set; }
            public double FWD_Pre2 { get; set; }
            public double RWD_Pre1 { get; set; }
            public double RWD_Pre2 { get; set; }
            public double ElecRatio { get; set; }
            public double PressRatio { get; set; }
            public string FullPre1 { get; set; }
            public string BalanPre1 { get; set; }
            public string Leak1 { get; set; }
            public string FullPre2 { get; set; }
            public string BalanPre2 { get; set; }
            public string Leak2 { get; set; }
            public string FWD_FullPre1 { get; set; }
            public string FWD_BalanPre1 { get; set; }
            public string FWD_Leak1 { get; set; }
            public string FWD_FullPre2 { get; set; }
            public string FWD_BalanPre2 { get; set; }
            public string FWD_Leak2 { get; set; }
        }
    }
}
