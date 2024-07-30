using System;

namespace SLC1_N
{
    public class ReadConfig
    {
        public Setup.Port ReadPort()
        {
            Setup.Port port = new Setup.Port();
            //string dialog = Form1.f1.machine;
            //ConfigINI config = new ConfigINI("Model", dialog);
            string dialog;
            dialog = "MultimeterPort.ini";
            ConfigINI config = new ConfigINI("Port", dialog);
            string ch1adcport = config.IniReadValue("Port", "CH1ADCPort");
            port.CH1ADCPort = ch1adcport;
            string ch1vdcport = config.IniReadValue("Port", "CH1VDCPort");
            port.CH1VDCPort = ch1vdcport;
            string ch1adcbaud = config.IniReadValue("Port", "CH1ADCBaud");
            port.CH1ADCBaud = ch1adcbaud;
            string ch1vdcbaud = config.IniReadValue("Port", "CH1VDCbaud ");
            port.CH1VDCBaud = ch1vdcbaud;
            string ch2adcport = config.IniReadValue("Port", "CH2ADCPort");
            port.CH2ADCPort = ch2adcport;
            string ch2vdcport = config.IniReadValue("Port", "CH2VDCPort");
            port.CH2VDCPort = ch2vdcport;
            string ch2adcbaud = config.IniReadValue("Port", "CH2ADCBaud");
            port.CH2ADCBaud = ch2adcbaud;
            string ch2vdcbaud = config.IniReadValue("Port", "CH2VDCBaud");
            port.CH2VDCBaud = ch2vdcbaud;
            string codeport = config.IniReadValue("Port", "CodePort");
            port.CodePort = codeport;
            string codebaud = config.IniReadValue("Port", "CodeBaud");
            port.CodeBaud = codebaud;
            string CH2codeport = config.IniReadValue("Port", "CH2CodePort");
            port.CH2CodePort = CH2codeport;
            string CH2codebaud = config.IniReadValue("Port", "CH2CodeBaud");
            port.CH2CodeBaud = CH2codebaud;
            string ch1flowport = config.IniReadValue("Port", "CH1FlowPort");
            port.CH1FlowPort = ch1flowport;
            string ch1flowbaud = config.IniReadValue("Port", "CH1FlowBaud");
            port.CH1FlowBaud = ch1flowbaud;
            string ch2flowport = config.IniReadValue("Port", "CH2FlowPort");
            port.CH2FlowPort = ch2flowport;
            string ch2flowbaud = config.IniReadValue("Port", "CH2FlowBaud");
            port.CH2FlowBaud = ch2flowbaud;
            string ch3flowport = config.IniReadValue("Port", "CH3FlowPort");
            port.CH3FlowPort = ch3flowport;
            string ch3flowbaud = config.IniReadValue("Port", "CH3FlowBaud");
            port.CH3FlowBaud = ch3flowbaud;
            string ch4flowport = config.IniReadValue("Port", "CH4FlowPort");
            port.CH4FlowPort = ch4flowport;
            string ch4flowbaud = config.IniReadValue("Port", "CH4FlowBaud");
            port.CH4FlowBaud = ch4flowbaud;

            port.CKCH1Port = config.IniReadValue("Port", "CKCH1Port");
            port.CKCH1Baud = config.IniReadValue("Port", "CKCH1Baud");
            port.CKCH2Port = config.IniReadValue("Port", "CKCH2Port");
            port.CKCH2Baud = config.IniReadValue("Port", "CKCH2Baud");

            return port;
        }

        /// <summary>
        /// 读取条码的勾选，条码的长度
        /// </summary>
        /// <returns></returns>
        public Setup.Code_Setting ReadCode()
        {
            Setup.Code_Setting set = new Setup.Code_Setting();
            string dialog = Form1.f1.machine;
            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            string ch1code_length = mesconfig.IniReadValue("Code", "left_codelength");
            if (String.IsNullOrEmpty(ch1code_length))
            {
                set.LeftCodeLength = "13";
            }
            else
            {
                set.LeftCodeLength = ch1code_length;
            }
            string ch2code_length = mesconfig.IniReadValue("Code", "right_codelength");
            if (String.IsNullOrEmpty(ch2code_length))
            {
                set.RightCodeLength = "13";
            }
            else
            {
                set.RightCodeLength = ch2code_length;
            }
            string chkleft = mesconfig.IniReadValue("Code", "chkleft");
            if (String.IsNullOrEmpty(chkleft))
            {
                set.CHKCH1 = true;
            }
            else
            {
                set.CHKCH1 = Convert.ToBoolean(chkleft);
            }
            string chkright = mesconfig.IniReadValue("Code", "chkright");
            if (String.IsNullOrEmpty(chkright))
            {
                set.CHKCH2 = true;
            }
            else
            {
                set.CHKCH2 = Convert.ToBoolean(chkright);
            }
            string ch1codelife = mesconfig.IniReadValue("CodeLife", "ch1codelife");
            if (!String.IsNullOrEmpty(ch1codelife))
            {
                set.LeftCodeLife = Convert.ToInt32(ch1codelife);
            }
            string ch2codelife = mesconfig.IniReadValue("CodeLife", "ch2codelife");
            if (!String.IsNullOrEmpty(ch2codelife))
            {
                set.RightCodeLife = Convert.ToInt32(ch2codelife);
            }
            set.CH1Code = mesconfig.IniReadValue("Code", "CH1Code");
            set.CH2Code = mesconfig.IniReadValue("Code", "CH2Code");
            return set;
        }

        /// <summary>
        /// 读取工单数据
        /// </summary>
        public Setup.Work_Order ReadWorkOrder()
        {
            Setup.Work_Order order = new Setup.Work_Order();
            //string dialog;
            //dialog = "WorkOrder.ini";
            //ConfigINI mesconfig = new ConfigINI(Form1.f1.machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            order.ProductName = config.IniReadValue("WorkOrder", "ProductName");
            //if (!String.IsNullOrEmpty(productname))
            //{
            //     = productname;
            //}
            order.ProductModel = config.IniReadValue("WorkOrder", "ProductModel");
            //if (!String.IsNullOrEmpty(productmodel))
            //{
            //     = productmodel;
            //}
            order.WorkOrder = config.IniReadValue("WorkOrder", "WorkOrder");
            //if (!String.IsNullOrEmpty(workorder))
            //{
            //     = workorder;
            //}
            order.ProductionItem = config.IniReadValue("WorkOrder", "ProductionItem");
            //if (!String.IsNullOrEmpty(productionitem))
            //{
            //     = productionitem;
            //}
            order.TestType = config.IniReadValue("WorkOrder", "TestType");
            //if (!String.IsNullOrEmpty(testtype))
            //{
            //     = testtype;
            //}
            order.TestStation = config.IniReadValue("WorkOrder", "TestStation");
            //if (!String.IsNullOrEmpty(teststation))
            //{
            //     = teststation;
            //}
            order.ProductNum = config.IniReadValue("WorkOrder", "ProductNum");
            //if (!String.IsNullOrEmpty(productnum))
            //{
            //     = productnum;
            //}
            return order;
        }

        /// <summary>
        /// 读取存储设置
        /// </summary>
        /// <returns></returns>
        public Setup.Save ReadSave()
        {
            Setup.Save save = new Setup.Save();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string chkexcel = config.IniReadValue("Save", "excel");
            if (!String.IsNullOrEmpty(chkexcel))
            {
                save.ChkExcel = Convert.ToBoolean(chkexcel);
            }
            string chkmes = config.IniReadValue("Save", "mes");
            if (!String.IsNullOrEmpty(chkmes))
            {
                save.ChkMES = Convert.ToBoolean(chkmes);
            }
            string chkcsv = config.IniReadValue("Save", "csv");
            if (!String.IsNullOrEmpty(chkcsv))
            {
                save.ChkCSV = Convert.ToBoolean(chkcsv);
            }
            string path = config.IniReadValue("Save", "path");
            if (!String.IsNullOrEmpty(path))
            {
                save.Path = path;
            }
            return save;
        }

        /// <summary>
        /// 读取参数
        /// </summary>
        /// <param name="CH"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public Model.CH_PARAMS ReadParameters(int CH, int i)
        {
            Model.CH_PARAMS ch_params = new Model.CH_PARAMS();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string paraname = config.IniReadValue("Parameters", CH + "paraname" + i);
            ch_params.ParaName = paraname;
            string fulltime = config.IniReadValue("Parameters", CH + "fulltime" + i);
            ch_params.FullTime = fulltime;
            string balantime = config.IniReadValue("Parameters", CH + "balantime" + i);
            ch_params.BalanTime = balantime;
            string testtime1 = config.IniReadValue("Parameters", CH + "testtime1" + i);
            ch_params.TestTime1 = testtime1;
            string exhausttime = config.IniReadValue("Parameters", CH + "exhausttime" + i);
            ch_params.ExhaustTime = exhausttime;
            string delaytime1 = config.IniReadValue("Parameters", CH + "delaytime1" + i);
            ch_params.DelayTime1 = delaytime1;
            string delaytime2 = config.IniReadValue("Parameters", CH + "delaytime2" + i);
            ch_params.DelayTime2 = delaytime2;
            string relievedelay = config.IniReadValue("Parameters", CH + "relievedelay" + i);
            ch_params.RelieveDelay = relievedelay;
            string evolume = config.IniReadValue("Parameters", CH + "evolume" + i);
            ch_params.Evolume = evolume;
            string fptoplimit = config.IniReadValue("Parameters", CH + "fptoplimit" + i);
            ch_params.FPtoplimit = fptoplimit;
            string fplowlimit = config.IniReadValue("Parameters", CH + "fplowlimit" + i);
            ch_params.FPlowlimit = fplowlimit;
            string balanpremax = config.IniReadValue("Parameters", CH + "balanpremax" + i);
            ch_params.BalanPreMax = balanpremax;
            string balanpremin = config.IniReadValue("Parameters", CH + "balanpremin" + i);
            ch_params.BalanPreMin = balanpremin;
            string leaktoplimit = config.IniReadValue("Parameters", CH + "leaktoplimit" + i);
            ch_params.Leaktoplimit = leaktoplimit;
            string leaklowlimit = config.IniReadValue("Parameters", CH + "leaklowlimit" + i);
            ch_params.Leaklowlimit = leaklowlimit;
            string punit = config.IniReadValue("Parameters", CH + "punit" + i);
            if (!String.IsNullOrEmpty(punit))
            {
                ch_params.PUnit_index = Convert.ToInt32(punit);
            }
            string lunit = config.IniReadValue("Parameters", CH + "lunit" + i);
            if (!String.IsNullOrEmpty(lunit))
            {
                ch_params.LUnit_index = Convert.ToInt32(lunit);
            }
            string bee = config.IniReadValue("Parameters", CH + "bee" + i);
            if (!String.IsNullOrEmpty(bee))
            {
                ch_params.ChkBee = Convert.ToBoolean(bee);
            }
            string presscompensation = config.IniReadValue("Parameters", CH + "presscompensation" );
            Log log = new Log();
            log.CH1Port_Logmsg("补偿值" + presscompensation);
            if (!String.IsNullOrEmpty(presscompensation))
            {
                ch_params.PressCompensation = "0";
            }
            else
            {
                ch_params.PressCompensation = presscompensation;
            }
           
            string unit = config.IniReadValue("Parameters", CH + "unit");
            if (String.IsNullOrEmpty(unit))
            {
                ch_params.CHKUnit = false;
            }
            else
            {
                ch_params.CHKUnit = Convert.ToBoolean(unit);
            }
            return ch_params;
        }

        /// <summary>
        /// 读取每个通道设置的程序流程
        /// </summary>
        public Setup.Order ReadLin()
        {
            //string dialog;
            //dialog = "Lin.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            Setup.Order ord = new Setup.Order();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string ch1up = config.IniReadValue("Lin", "CH1UP");

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH1UpDownChange")))
                ord.CH1UpDownChange = false;
            else
                ord.CH1UpDownChange = Convert.ToBoolean(config.IniReadValue("Lin", "CH1UpDownChange"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH2UpDownChange")))
                ord.CH2UpDownChange = false;
            else
                ord.CH2UpDownChange = Convert.ToBoolean(config.IniReadValue("Lin", "CH2UpDownChange"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH1IGN")))
                ord.CH1IGN = false;
            else
                ord.CH1IGN = Convert.ToBoolean(config.IniReadValue("Lin", "CH1IGN"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH2IGN")))
                ord.CH2IGN = false;
            else
                ord.CH2IGN = Convert.ToBoolean(config.IniReadValue("Lin", "CH2IGN"));




            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH1HighLevel"))) 
                ord.CH1HighLevel = false;
            else
                ord.CH1HighLevel = Convert.ToBoolean(config.IniReadValue("Lin", "CH1HighLevel"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH2HighLevel")))
                ord.CH2HighLevel = false;
            else
                ord.CH2HighLevel = Convert.ToBoolean(config.IniReadValue("Lin", "CH2HighLevel"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH1LIN")))
                ord.CH1LIN = false;
            else
                ord.CH1LIN = Convert.ToBoolean(config.IniReadValue("Lin", "CH1LIN"));

            if (String.IsNullOrEmpty(config.IniReadValue("Lin", "CH2LIN")))
                ord.CH2LIN = false;
            else
                ord.CH2LIN = Convert.ToBoolean(config.IniReadValue("Lin", "CH2LIN"));

            if (String.IsNullOrEmpty(ch1up))
            {
                ord.CH1UP = false;
            }
            else
            {
                ord.CH1UP = Convert.ToBoolean(ch1up);
            }
            string ch1down = config.IniReadValue("Lin", "CH1DOWN");
            if (String.IsNullOrEmpty(ch1down))
            {
                ord.CH1DOWN = false;
            }
            else
            {
                ord.CH1DOWN = Convert.ToBoolean(ch1down);
            }
            string ch1fwd = config.IniReadValue("Lin", "CH1FWD");
            if (String.IsNullOrEmpty(ch1fwd))
            {
                ord.CH1FWD = false;
            }
            else
            {
                ord.CH1FWD = Convert.ToBoolean(ch1fwd);
            }
            string ch1rwd = config.IniReadValue("Lin", "CH1RWD");
            if (String.IsNullOrEmpty(ch1rwd))
            {
                ord.CH1RWD = false;
            }
            else
            {
                ord.CH1RWD = Convert.ToBoolean(ch1rwd);
            }
            string ch1upindex = config.IniReadValue("Lin", "CH1UPindex");
            if (!String.IsNullOrEmpty(ch1upindex))
            {
                ord.CH1UPindex = ch1upindex;
            }
            string ch1downindex = config.IniReadValue("Lin", "CH1DOWNindex");
            if (!String.IsNullOrEmpty(ch1downindex))
            {
                ord.CH1DOWNindex = ch1downindex;
            }
            string ch1fwdindex = config.IniReadValue("Lin", "CH1FWDindex");
            if (!String.IsNullOrEmpty(ch1fwdindex))
            {
                ord.CH1FWDindex = ch1fwdindex;
            }
            string ch1rwdindex = config.IniReadValue("Lin", "CH1RWDindex");
            if (!String.IsNullOrEmpty(ch1rwdindex))
            {
                ord.CH1RWDindex = ch1rwdindex;
            }
            string ch1upleakindex = config.IniReadValue("Lin", "CH1UPLeakindex");
            if (!String.IsNullOrEmpty(ch1upleakindex))
            {
                ord.CH1UPLeakindex = ch1upleakindex;
            }
            string ch1downleakindex = config.IniReadValue("Lin", "CH1DOWNLeakindex");
            if (!String.IsNullOrEmpty(ch1downleakindex))
            {
                ord.CH1DOWNLeakindex = ch1downleakindex;
            }
            string ch1fwdleakindex = config.IniReadValue("Lin", "CH1FWDLeakindex");
            if (!String.IsNullOrEmpty(ch1fwdleakindex))
            {
                ord.CH1FWDLeakindex = ch1fwdleakindex;
            }
            string ch1lin = config.IniReadValue("Lin", "CH1LIN");
            if (String.IsNullOrEmpty(ch1lin))
            {
                ord.CH1LIN = false;
            }
            else
            {
                ord.CH1LIN = Convert.ToBoolean(ch1lin);
            }
            string ch1upleak = config.IniReadValue("Lin", "CH1UPLeak");
            if (String.IsNullOrEmpty(ch1upleak))
            {
                ord.CH1UPLeak = false;
            }
            else
            {
                ord.CH1UPLeak = Convert.ToBoolean(ch1upleak);
            }
            string ch1downleak = config.IniReadValue("Lin", "CH1DOWNLeak");
            if (String.IsNullOrEmpty(ch1downleak))
            {
                ord.CH1DOWNLeak = false;
            }
            else
            {
                ord.CH1DOWNLeak = Convert.ToBoolean(ch1downleak);
            }
            string ch1fwdleak = config.IniReadValue("Lin", "CH1FWDLeak");
            if (String.IsNullOrEmpty(ch1fwdleak))
            {
                ord.CH1FWDLeak = false;
            }
            else
            {
                ord.CH1FWDLeak = Convert.ToBoolean(ch1fwdleak);
            }
            string ch1pump = config.IniReadValue("Lin", "CH1PUMP");
            if (String.IsNullOrEmpty(ch1pump))
            {
                ord.CH1Pump = false;
            }
            else
            {
                ord.CH1Pump = Convert.ToBoolean(ch1pump);
            }

            string ch2up = config.IniReadValue("Lin", "CH2UP");
            if (String.IsNullOrEmpty(ch2up))
            {
                ord.CH2UP = false;
            }
            else
            {
                ord.CH2UP = Convert.ToBoolean(ch2up);
            }
            string ch2down = config.IniReadValue("Lin", "CH2DOWN");
            if (String.IsNullOrEmpty(ch2down))
            {
                ord.CH2DOWN = false;
            }
            else
            {
                ord.CH2DOWN = Convert.ToBoolean(ch2down);
            }
            string ch2fwd = config.IniReadValue("Lin", "CH2FWD");
            if (String.IsNullOrEmpty(ch2fwd))
            {
                ord.CH2FWD = false;
            }
            else
            {
                ord.CH2FWD = Convert.ToBoolean(ch2fwd);
            }
            string ch2rwd = config.IniReadValue("Lin", "CH2RWD");
            if (String.IsNullOrEmpty(ch2rwd))
            {
                ord.CH2RWD = false;
            }
            else
            {
                ord.CH2RWD = Convert.ToBoolean(ch2rwd);
            }
            string ch2upindex = config.IniReadValue("Lin", "CH2UPindex");
            if (!String.IsNullOrEmpty(ch2upindex))
            {
                ord.CH2UPindex = ch2upindex;
            }
            string ch2downindex = config.IniReadValue("Lin", "CH2DOWNindex");
            if (!String.IsNullOrEmpty(ch2downindex))
            {
                ord.CH2DOWNindex = ch2downindex;
            }
            string ch2fwdindex = config.IniReadValue("Lin", "CH2FWDindex");
            if (!String.IsNullOrEmpty(ch2fwdindex))
            {
                ord.CH2FWDindex = ch2fwdindex;
            }
            string ch2rwdindex = config.IniReadValue("Lin", "CH2RWDindex");
            if (!String.IsNullOrEmpty(ch2rwdindex))
            {
                ord.CH2RWDindex = ch2rwdindex;
            }
            string ch2upleakindex = config.IniReadValue("Lin", "CH2UPLeakindex");
            if (!String.IsNullOrEmpty(ch2upleakindex))
            {
                ord.CH2UPLeakindex = ch2upleakindex;
            }
            string ch2downleakindex = config.IniReadValue("Lin", "CH2DOWNLeakindex");
            if (!String.IsNullOrEmpty(ch2downleakindex))
            {
                ord.CH2DOWNLeakindex = ch2downleakindex;
            }
            string ch2fwdleakindex = config.IniReadValue("Lin", "CH2FWDLeakindex");
            if (!String.IsNullOrEmpty(ch2fwdleakindex))
            {
                ord.CH2FWDLeakindex = ch2fwdleakindex;
            }
            string ch2upleak = config.IniReadValue("Lin", "CH2UPLeak");
            if (String.IsNullOrEmpty(ch2upleak))
            {
                ord.CH2UPLeak = false;
            }
            else
            {
                ord.CH2UPLeak = Convert.ToBoolean(ch2upleak);
            }
            string ch2downleak = config.IniReadValue("Lin", "CH2DOWNLeak");
            if (String.IsNullOrEmpty(ch2downleak))
            {
                ord.CH2DOWNLeak = false;
            }
            else
            {
                ord.CH2DOWNLeak = Convert.ToBoolean(ch2downleak);
            }
            string ch2fwdleak = config.IniReadValue("Lin", "CH2FWDLeak");
            if (String.IsNullOrEmpty(ch2fwdleak))
            {
                ord.CH2FWDLeak = false;
            }
            else
            {
                ord.CH2FWDLeak = Convert.ToBoolean(ch2fwdleak);
            }
            string ch2pump = config.IniReadValue("Lin", "CH2PUMP");
            if (String.IsNullOrEmpty(ch2pump))
            {
                ord.CH2Pump = false;
            }
            else
            {
                ord.CH2Pump = Convert.ToBoolean(ch2pump);
            }
            string ch2lin = config.IniReadValue("Lin", "CH2LIN");
            if (String.IsNullOrEmpty(ch2lin))
            {
                ord.CH2LIN = false;
            }
            else
            {
                ord.CH2LIN = Convert.ToBoolean(ch2lin);
            }
            string cH1UpDownChange = config.IniReadValue("Lin", "CH1UpDownChange");

            if ("True".Equals(cH1UpDownChange))
            {
                ord.CH1UpDownChange = true;
            }
            else
            {
                ord.CH1UpDownChange = false;
            }
            string cH2UpDownChange = config.IniReadValue("Lin", "CH2UpDownChange");

            if ("True".Equals(cH2UpDownChange))
            {
                ord.CH2UpDownChange = true;
            }
            else
            {
                ord.CH2UpDownChange = false;
            }

            string cH1QuiescentCurrnt = config.IniReadValue("Lin", "CH1QuiescentCurrnt");
            if (String.IsNullOrEmpty(cH1QuiescentCurrnt))
            {
                ord.CH1QuiescentCurrnt = false;
            }
            else
            {
                ord.CH1QuiescentCurrnt = Convert.ToBoolean(cH1QuiescentCurrnt);
            }
            string cH1QuiescentCurrntIndex = config.IniReadValue("Lin", "CH1QuiescentCurrntIndex");
            if (!String.IsNullOrEmpty(cH1QuiescentCurrntIndex))
            {
                ord.CH1QuiescentCurrntIndex = cH1QuiescentCurrntIndex;
            }
            string cH2QuiescentCurrnt = config.IniReadValue("Lin", "CH2QuiescentCurrnt");
            if (String.IsNullOrEmpty(cH2QuiescentCurrnt))
            {
                ord.CH2QuiescentCurrnt = false;
            }
            else
            {
                ord.CH2QuiescentCurrnt = Convert.ToBoolean(cH2QuiescentCurrnt);
            }
            string cH2QuiescentCurrntIndex = config.IniReadValue("Lin", "CH2QuiescentCurrntIndex");
            if (!String.IsNullOrEmpty(cH2QuiescentCurrntIndex))
            {
                ord.CH2QuiescentCurrntIndex = cH2QuiescentCurrntIndex;
            }

            //AD
            string ADUP = config.IniReadValue("Lin", "ADUP");
            if (String.IsNullOrEmpty(ADUP))
            {
                ord.ADUP = false;
            }
            else
            {
                ord.ADUP = Convert.ToBoolean(ADUP);
            }
            string ADDOWN = config.IniReadValue("Lin", "ADDOWN");
            if (String.IsNullOrEmpty(ADDOWN))
            {
                ord.ADDOWN = false;
            }
            else
            {
                ord.ADDOWN = Convert.ToBoolean(ADDOWN);
            }
            string ADFWD = config.IniReadValue("Lin", "ADFWD");
            if (String.IsNullOrEmpty(ADFWD))
            {
                ord.ADFWD = false;
            }
            else
            {
                ord.ADFWD = Convert.ToBoolean(ADFWD);
            }
            string ADRWD = config.IniReadValue("Lin", "ADRWD");
            if (String.IsNullOrEmpty(ADRWD))
            {
                ord.ADRWD = false;
            }
            else
            {
                ord.ADRWD = Convert.ToBoolean(ADRWD);
            }

            string ADUPLeak = config.IniReadValue("Lin", "ADUPLeak");
            if (String.IsNullOrEmpty(ADUPLeak))
            {
                ord.ADUPLeak = false;
            }
            else
            {
                ord.ADUPLeak = Convert.ToBoolean(ADUPLeak);
            }
            string ADDOWNLeak = config.IniReadValue("Lin", "ADDOWNLeak");
            if (String.IsNullOrEmpty(ADDOWNLeak))
            {
                ord.ADDOWNLeak = false;
            }
            else
            {
                ord.ADDOWNLeak = Convert.ToBoolean(ADDOWNLeak);
            }
            string ADFWDLeak = config.IniReadValue("Lin", "ADFWDLeak");
            if (String.IsNullOrEmpty(ADFWDLeak))
            {
                ord.ADFWDLeak = false;
            }
            else
            {
                ord.ADFWDLeak = Convert.ToBoolean(ADFWDLeak);
            }
            string ADQuiescentCurrnt = config.IniReadValue("Lin", "ADQuiescentCurrnt");
            if (String.IsNullOrEmpty(ADQuiescentCurrnt))
            {
                ord.ADQuiescentCurrnt = false;
            }
            else
            {
                ord.ADQuiescentCurrnt = Convert.ToBoolean(ADQuiescentCurrnt);
            }

            string ADUPindex = config.IniReadValue("Lin", "ADUPindex");
            if (!String.IsNullOrEmpty(ADUPindex))
            {
                ord.ADUPindex = ADUPindex;
            }
            string ADDOWNindex = config.IniReadValue("Lin", "ADDOWNindex");
            if (!String.IsNullOrEmpty(ADDOWNindex))
            {
                ord.ADDOWNindex = ADDOWNindex;
            }
            string ADFWDindex = config.IniReadValue("Lin", "ADFWDindex");
            if (!String.IsNullOrEmpty(ADFWDindex))
            {
                ord.ADFWDindex = ADFWDindex;
            }
            string ADRWDindex = config.IniReadValue("Lin", "ADRWDindex");
            if (!String.IsNullOrEmpty(ADRWDindex))
            {
                ord.ADRWDindex = ADRWDindex;
            }
            string ADUPLeakindex = config.IniReadValue("Lin", "ADUPLeakindex");
            if (!String.IsNullOrEmpty(ADUPLeakindex))
            {
                ord.ADUPLeakindex = ADUPLeakindex;
            }
            string ADDOWNLeakindex = config.IniReadValue("Lin", "ADDOWNLeakindex");
            if (!String.IsNullOrEmpty(ADDOWNLeakindex))
            {
                ord.ADDOWNLeakindex = ADDOWNLeakindex;
            }
            string ADFWDLeakindex = config.IniReadValue("Lin", "ADFWDLeakindex");
            if (!String.IsNullOrEmpty(ADFWDLeakindex))
            {
                ord.ADFWDLeakindex = ADFWDLeakindex;
            }
            string ADQuiescentCurrntIndex = config.IniReadValue("Lin", "ADQuiescentCurrntIndex");
            if (!String.IsNullOrEmpty(ADQuiescentCurrntIndex))
            {
                ord.ADQuiescentCurrntIndex = ADQuiescentCurrntIndex;
            }
            //BE
            string BEUP = config.IniReadValue("Lin", "BEUP");
            if (String.IsNullOrEmpty(BEUP))
            {
                ord.BEUP = false;
            }
            else
            {
                ord.BEUP = Convert.ToBoolean(BEUP);
            }
            string BEDOWN = config.IniReadValue("Lin", "BEDOWN");
            if (String.IsNullOrEmpty(BEDOWN))
            {
                ord.BEDOWN = false;
            }
            else
            {
                ord.BEDOWN = Convert.ToBoolean(BEDOWN);
            }
            string BEFWD = config.IniReadValue("Lin", "BEFWD");
            if (String.IsNullOrEmpty(BEFWD))
            {
                ord.BEFWD = false;
            }
            else
            {
                ord.BEFWD = Convert.ToBoolean(BEFWD);
            }
            string BERWD = config.IniReadValue("Lin", "BERWD");
            if (String.IsNullOrEmpty(BERWD))
            {
                ord.BERWD = false;
            }
            else
            {
                ord.BERWD = Convert.ToBoolean(BERWD);
            }

            string BEUPLeak = config.IniReadValue("Lin", "BEUPLeak");
            if (String.IsNullOrEmpty(BEUPLeak))
            {
                ord.BEUPLeak = false;
            }
            else
            {
                ord.BEUPLeak = Convert.ToBoolean(BEUPLeak);
            }
            string BEDOWNLeak = config.IniReadValue("Lin", "BEDOWNLeak");
            if (String.IsNullOrEmpty(BEDOWNLeak))
            {
                ord.BEDOWNLeak = false;
            }
            else
            {
                ord.BEDOWNLeak = Convert.ToBoolean(BEDOWNLeak);
            }
            string BEFWDLeak = config.IniReadValue("Lin", "BEFWDLeak");
            if (String.IsNullOrEmpty(BEFWDLeak))
            {
                ord.BEFWDLeak = false;
            }
            else
            {
                ord.BEFWDLeak = Convert.ToBoolean(BEFWDLeak);
            }
            string BEQuiescentCurrnt = config.IniReadValue("Lin", "BEQuiescentCurrnt");
            if (String.IsNullOrEmpty(BEQuiescentCurrnt))
            {
                ord.BEQuiescentCurrnt = false;
            }
            else
            {
                ord.BEQuiescentCurrnt = Convert.ToBoolean(BEQuiescentCurrnt);
            }

            string BEUPindex = config.IniReadValue("Lin", "BEUPindex");
            if (!String.IsNullOrEmpty(BEUPindex))
            {
                ord.BEUPindex = BEUPindex;
            }
            string BEDOWNindex = config.IniReadValue("Lin", "BEDOWNindex");
            if (!String.IsNullOrEmpty(BEDOWNindex))
            {
                ord.BEDOWNindex = BEDOWNindex;
            }
            string BEFWDindex = config.IniReadValue("Lin", "BEFWDindex");
            if (!String.IsNullOrEmpty(BEFWDindex))
            {
                ord.BEFWDindex = BEFWDindex;
            }
            string BERWDindex = config.IniReadValue("Lin", "BERWDindex");
            if (!String.IsNullOrEmpty(BERWDindex))
            {
                ord.BERWDindex = BERWDindex;
            }
            string BEUPLeakindex = config.IniReadValue("Lin", "BEUPLeakindex");
            if (!String.IsNullOrEmpty(BEUPLeakindex))
            {
                ord.BEUPLeakindex = BEUPLeakindex;
            }
            string BEDOWNLeakindex = config.IniReadValue("Lin", "BEDOWNLeakindex");
            if (!String.IsNullOrEmpty(BEDOWNLeakindex))
            {
                ord.BEDOWNLeakindex = BEDOWNLeakindex;
            }
            string BEFWDLeakindex = config.IniReadValue("Lin", "BEFWDLeakindex");
            if (!String.IsNullOrEmpty(BEFWDLeakindex))
            {
                ord.BEFWDLeakindex = BEFWDLeakindex;
            }
            string BEQuiescentCurrntIndex = config.IniReadValue("Lin", "BEQuiescentCurrntIndex");
            if (!String.IsNullOrEmpty(BEQuiescentCurrntIndex))
            {
                ord.BEQuiescentCurrntIndex = BEQuiescentCurrntIndex;
            }
            //CF
            string CFUP = config.IniReadValue("Lin", "CFUP");
            if (String.IsNullOrEmpty(CFUP))
            {
                ord.CFUP = false;
            }
            else
            {
                ord.CFUP = Convert.ToBoolean(CFUP);
            }
            string CFDOWN = config.IniReadValue("Lin", "CFDOWN");
            if (String.IsNullOrEmpty(CFDOWN))
            {
                ord.CFDOWN = false;
            }
            else
            {
                ord.CFDOWN = Convert.ToBoolean(CFDOWN);
            }
            string CFFWD = config.IniReadValue("Lin", "CFFWD");
            if (String.IsNullOrEmpty(CFFWD))
            {
                ord.CFFWD = false;
            }
            else
            {
                ord.CFFWD = Convert.ToBoolean(CFFWD);
            }
            string CFRWD = config.IniReadValue("Lin", "CFRWD");
            if (String.IsNullOrEmpty(CFRWD))
            {
                ord.CFRWD = false;
            }
            else
            {
                ord.CFRWD = Convert.ToBoolean(CFRWD);
            }

            string CFUPLeak = config.IniReadValue("Lin", "CFUPLeak");
            if (String.IsNullOrEmpty(CFUPLeak))
            {
                ord.CFUPLeak = false;
            }
            else
            {
                ord.CFUPLeak = Convert.ToBoolean(CFUPLeak);
            }
            string CFDOWNLeak = config.IniReadValue("Lin", "CFDOWNLeak");
            if (String.IsNullOrEmpty(CFDOWNLeak))
            {
                ord.CFDOWNLeak = false;
            }
            else
            {
                ord.CFDOWNLeak = Convert.ToBoolean(CFDOWNLeak);
            }
            string CFFWDLeak = config.IniReadValue("Lin", "CFFWDLeak");
            if (String.IsNullOrEmpty(CFFWDLeak))
            {
                ord.CFFWDLeak = false;
            }
            else
            {
                ord.CFFWDLeak = Convert.ToBoolean(CFFWDLeak);
            }
            string CFQuiescentCurrnt = config.IniReadValue("Lin", "CFQuiescentCurrnt");
            if (String.IsNullOrEmpty(CFQuiescentCurrnt))
            {
                ord.CFQuiescentCurrnt = false;
            }
            else
            {
                ord.CFQuiescentCurrnt = Convert.ToBoolean(CFQuiescentCurrnt);
            }

            string CFUPindex = config.IniReadValue("Lin", "CFUPindex");
            if (!String.IsNullOrEmpty(CFUPindex))
            {
                ord.CFUPindex = CFUPindex;
            }
            string CFDOWNindex = config.IniReadValue("Lin", "CFDOWNindex");
            if (!String.IsNullOrEmpty(CFDOWNindex))
            {
                ord.CFDOWNindex = CFDOWNindex;
            }
            string CFFWDindex = config.IniReadValue("Lin", "CFFWDindex");
            if (!String.IsNullOrEmpty(CFFWDindex))
            {
                ord.CFFWDindex = CFFWDindex;
            }
            string CFRWDindex = config.IniReadValue("Lin", "CFRWDindex");
            if (!String.IsNullOrEmpty(CFRWDindex))
            {
                ord.CFRWDindex = CFRWDindex;
            }
            string CFUPLeakindex = config.IniReadValue("Lin", "CFUPLeakindex");
            if (!String.IsNullOrEmpty(CFUPLeakindex))
            {
                ord.CFUPLeakindex = CFUPLeakindex;
            }
            string CFDOWNLeakindex = config.IniReadValue("Lin", "CFDOWNLeakindex");
            if (!String.IsNullOrEmpty(CFDOWNLeakindex))
            {
                ord.CFDOWNLeakindex = CFDOWNLeakindex;
            }
            string CFFWDLeakindex = config.IniReadValue("Lin", "CFFWDLeakindex");
            if (!String.IsNullOrEmpty(CFFWDLeakindex))
            {
                ord.CFFWDLeakindex = CFFWDLeakindex;
            }
            string CFQuiescentCurrntIndex = config.IniReadValue("Lin", "CFQuiescentCurrntIndex");
            if (!String.IsNullOrEmpty(CFQuiescentCurrntIndex))
            {
                ord.CFQuiescentCurrntIndex = CFQuiescentCurrntIndex;
            }


            return ord;
        }

        /// <summary>
        /// 读取流量参数
        /// </summary>
        public Model.Flow ReadFlow()
        {
            //string dialog;
            //dialog = "Flow.ini";
            //ConfigINI config = new ConfigINI(Form1.f1.machine, dialog);
            Model.Flow flow = new Model.Flow();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string ch1over = config.IniReadValue("Calculation", "CH1OverTime");
            if (String.IsNullOrEmpty(ch1over))
            {
                flow.CH1OverTime = 10;
            }
            else
            {
                flow.CH1OverTime = Convert.ToDouble(ch1over);
            }
            string ch2over = config.IniReadValue("Calculation", "CH2OverTime");
            if (String.IsNullOrEmpty(ch2over))
            {
                flow.CH2OverTime = 10;
            }
            else
            {
                flow.CH2OverTime = Convert.ToDouble(ch2over);
            }
            string ch3over = config.IniReadValue("Calculation", "CH3OverTime");
            if (String.IsNullOrEmpty(ch3over))
            {
                flow.CH3OverTime = 10;
            }
            else
            {
                flow.CH3OverTime = Convert.ToDouble(ch3over);
            }


            //3.7修改
            string CH1_2PreMin = config.IniReadValue("Calculation", "CH1_2PreMin");
            if (String.IsNullOrEmpty(CH1_2PreMin))
            {
                flow.CH1_2PreMin = 10;
            }
            else
            {
                flow.CH1_2PreMin = Convert.ToDouble(CH1_2PreMin);
            }

            string CH1_2PreMax = config.IniReadValue("Calculation", "CH1_2PreMax");
            if (String.IsNullOrEmpty(CH1_2PreMax))
            {
                flow.CH1_2PreMax = 10;
            }
            else
            {
                flow.CH1_2PreMax = Convert.ToDouble(CH1_2PreMax);
            }

            //3.7修改
            string CH2_2PreMin = config.IniReadValue("Calculation", "CH2_2PreMin");
            if (String.IsNullOrEmpty(CH2_2PreMin))
            {
                flow.CH2_2PreMin = 10;
            }
            else
            {
                flow.CH2_2PreMin = Convert.ToDouble(CH2_2PreMin);
                //flow.CH2_2PreMin = 10;
            }

            string CH2_2PreMax = config.IniReadValue("Calculation", "CH2_2PreMax");
            if (String.IsNullOrEmpty(CH2_2PreMax))
            {
                flow.CH2_2PreMax = 10;
            }
            else
            {
                flow.CH2_2PreMax = Convert.ToDouble(CH2_2PreMax);
            }


            string CH2_1PreMax = config.IniReadValue("Calculation", "CH2_1PreMax");
            if (String.IsNullOrEmpty(CH2_1PreMax))
            {
                flow.CH2_1PreMax = 10;
            }
            else
            {
                flow.CH2_1PreMax = Convert.ToDouble(CH2_1PreMax);
            }

            string CH2_1PreMin = config.IniReadValue("Calculation", "CH2_1PreMin");
            if (String.IsNullOrEmpty(CH2_1PreMin))
            {
                flow.CH2_1PreMin = 10;
            }
            else
            {
                flow.CH2_1PreMin = Convert.ToDouble(CH2_1PreMin);
            }

            string CH1_1PreMin = config.IniReadValue("Calculation", "CH1_1PreMin");
            if (String.IsNullOrEmpty(CH1_1PreMin))
            {
                flow.CH1_1PreMin = 10;
            }
            else
            {
                flow.CH1_1PreMin = Convert.ToDouble(CH1_1PreMin);
            }



            string ch4over = config.IniReadValue("Calculation", "CH4OverTime");
            if (String.IsNullOrEmpty(ch4over))
            {
                flow.CH4OverTime = 10;
            }
            else
            {
                flow.CH4OverTime = Convert.ToDouble(ch4over);
            }
            string ch1preover = config.IniReadValue("Calculation", "CH1Press_OverTime");
            if (String.IsNullOrEmpty(ch1preover))
            {
                flow.CH1Press_OverTime = 10;
            }
            else
            {
                flow.CH1Press_OverTime = Convert.ToDouble(ch1preover);
            }
            string ch2preover = config.IniReadValue("Calculation", "CH2Press_OverTime");
            if (String.IsNullOrEmpty(ch2preover))
            {
                flow.CH2Press_OverTime = 10;
            }
            else
            {
                flow.CH2Press_OverTime = Convert.ToDouble(ch2preover);
            }
            string ch3preover = config.IniReadValue("Calculation", "CH3Press_OverTime");
            if (String.IsNullOrEmpty(ch3preover))
            {
                flow.CH3Press_OverTime = 10;
            }
            else
            {
                flow.CH3Press_OverTime = Convert.ToDouble(ch3preover);
            }
            string ch4preover = config.IniReadValue("Calculation", "CH4Press_OverTime");
            if (String.IsNullOrEmpty(ch4preover))
            {
                flow.CH4Press_OverTime = 10;
            }
            else
            {
                flow.CH4Press_OverTime = Convert.ToDouble(ch4preover);
            }
            string ch1flowmax = config.IniReadValue("Calculation", "CH1_1FlowMax");
            if (String.IsNullOrEmpty(ch1flowmax))
            {
                flow.CH1_1FlowMax = 0;
            }
            else
            {
                flow.CH1_1FlowMax = Convert.ToDouble(ch1flowmax);
            }
            string ch2flowmax = config.IniReadValue("Calculation", "CH1_2FlowMax");
            if (String.IsNullOrEmpty(ch2flowmax))
            {
                flow.CH1_2FlowMax = 0;
            }
            else
            {
                flow.CH1_2FlowMax = Convert.ToDouble(ch2flowmax);
            }
            string ch3flowmax = config.IniReadValue("Calculation", "CH2_1FlowMax");
            if (String.IsNullOrEmpty(ch3flowmax))
            {
                flow.CH2_1FlowMax = 0;
            }
            else
            {
                flow.CH2_1FlowMax = Convert.ToDouble(ch3flowmax);
            }
            string ch4flowmax = config.IniReadValue("Calculation", "CH2_2FlowMax");
            if (String.IsNullOrEmpty(ch4flowmax))
            {
                flow.CH2_2FlowMax = 0;
            }
            else
            {
                flow.CH2_2FlowMax = Convert.ToDouble(ch4flowmax);
            }
            string ch1flowmin = config.IniReadValue("Calculation", "CH1_1FlowMin");
            if (String.IsNullOrEmpty(ch1flowmin))
            {
                flow.CH1_1FlowMin = 0;
            }
            else
            {
                flow.CH1_1FlowMin = Convert.ToDouble(ch1flowmin);
            }
            string ch2flowmin = config.IniReadValue("Calculation", "CH1_2FlowMin");
            if (String.IsNullOrEmpty(ch2flowmin))
            {
                flow.CH1_2FlowMin = 0;
            }
            else
            {
                flow.CH1_2FlowMin = Convert.ToDouble(ch2flowmin);
            }
            string ch3flowmin = config.IniReadValue("Calculation", "CH2_1FlowMin");
            if (String.IsNullOrEmpty(ch3flowmin))
            {
                flow.CH2_1FlowMin = 0;
            }
            else
            {
                flow.CH2_1FlowMin = Convert.ToDouble(ch3flowmin);
            }
            string ch4flowmin = config.IniReadValue("Calculation", "CH2_2FlowMin");
            if (String.IsNullOrEmpty(ch4flowmin))
            {
                flow.CH2_2FlowMin = 0;
            }
            else
            {
                flow.CH2_2FlowMin = Convert.ToDouble(ch4flowmin);
            }
            string ch1elecmax = config.IniReadValue("Calculation", "CH1Cont_ElecMax");
            if (String.IsNullOrEmpty(ch1elecmax))
            {
                flow.CH1Cont_ElecMax = 0;
            }
            else
            {
                flow.CH1Cont_ElecMax = Convert.ToDouble(ch1elecmax);
            }
            string ch1elecmin = config.IniReadValue("Calculation", "CH1Cont_ElecMin");
            if (String.IsNullOrEmpty(ch1elecmin))
            {
                flow.CH1Cont_ElecMin = 0;
            }
            else
            {
                flow.CH1Cont_ElecMin = Convert.ToDouble(ch1elecmin);
            }
            string ch1elecomp = config.IniReadValue("Calculation", "CH1Cont_Elec_Compen");
            if (String.IsNullOrEmpty(ch1elecomp))
            {
                flow.CH1Cont_Elec_Compen = 0;
            }
            else
            {
                flow.CH1Cont_Elec_Compen = Convert.ToDouble(ch1elecomp);
            }
            string ch2elecmax = config.IniReadValue("Calculation", "CH2Cont_ElecMax");
            if (String.IsNullOrEmpty(ch2elecmax))
            {
                flow.CH2Cont_ElecMax = 0;
            }
            else
            {
                flow.CH2Cont_ElecMax = Convert.ToDouble(ch2elecmax);
            }
            string ch2elecmin = config.IniReadValue("Calculation", "CH2Cont_ElecMin");
            if (String.IsNullOrEmpty(ch2elecmin))
            {
                flow.CH2Cont_ElecMin = 0;
            }
            else
            {
                flow.CH2Cont_ElecMin = Convert.ToDouble(ch2elecmin);
            }
            string ch2elecomp = config.IniReadValue("Calculation", "CH2Cont_Elec_Compen");
            if (String.IsNullOrEmpty(ch2elecomp))
            {
                flow.CH2Cont_Elec_Compen = 0;
            }
            else
            {
                flow.CH2Cont_Elec_Compen = Convert.ToDouble(ch2elecomp);
            }
            string ch1premax = config.IniReadValue("Calculation", "CH1Cont_PressMax");
            if (String.IsNullOrEmpty(ch1premax))
            {
                flow.CH1Cont_PressMax = 0;
            }
            else
            {
                flow.CH1Cont_PressMax = Convert.ToDouble(ch1premax);
            }
            string ch1premin = config.IniReadValue("Calculation", "CH1Cont_PressMin");
            if (String.IsNullOrEmpty(ch1premin))
            {
                flow.CH1Cont_PressMin = 0;
            }
            else
            {
                flow.CH1Cont_PressMin = Convert.ToDouble(ch1premin);
            }
            string ch1precomp = config.IniReadValue("Calculation", "CH1Cont_Pre_Compen");
            if (String.IsNullOrEmpty(ch1precomp))
            {
                flow.CH1Cont_Pre_Compen = 0;
            }
            else
            {
                flow.CH1Cont_Pre_Compen = Convert.ToDouble(ch1precomp);
            }
            string ch2premax = config.IniReadValue("Calculation", "CH2Cont_PressMax");
            if (String.IsNullOrEmpty(ch2premax))
            {
                flow.CH2Cont_PressMax = 0;
            }
            else
            {
                flow.CH2Cont_PressMax = Convert.ToDouble(ch2premax);
            }
            string ch2premin = config.IniReadValue("Calculation", "CH2Cont_PressMin");
            if (String.IsNullOrEmpty(ch2premin))
            {
                flow.CH2Cont_PressMin = 0;
            }
            else
            {
                flow.CH2Cont_PressMin = Convert.ToDouble(ch2premin);
            }
            string ch2precomp = config.IniReadValue("Calculation", "CH2Cont_Pre_Compen");
            if (String.IsNullOrEmpty(ch2precomp))
            {
                flow.CH2Cont_Pre_Compen = 0;
            }
            else
            {
                flow.CH2Cont_Pre_Compen = Convert.ToDouble(ch2precomp);
            }
            string ch1rwdmax = config.IniReadValue("Calculation", "CH1RWDPressMax");
            if (String.IsNullOrEmpty(ch1rwdmax))
            {
                flow.CH1RWDPressMax = 0;
            }
            else
            {
                flow.CH1RWDPressMax = Convert.ToDouble(ch1rwdmax);
            }
            string ch2rwdmax = config.IniReadValue("Calculation", "CH2RWDPressMax");
            if (String.IsNullOrEmpty(ch2rwdmax))
            {
                flow.CH2RWDPressMax = 0;
            }
            else
            {
                flow.CH2RWDPressMax = Convert.ToDouble(ch2rwdmax);
            }
            string ch1rwdmin = config.IniReadValue("Calculation", "CH1RWDPressMin");
            if (String.IsNullOrEmpty(ch1rwdmin))
            {
                flow.CH1RWDPressMin = 0;
            }
            else
            {
                flow.CH1RWDPressMin = Convert.ToDouble(ch1rwdmin);
            }
            string ch2rwdmin = config.IniReadValue("Calculation", "CH2RWDPressMin");
            if (String.IsNullOrEmpty(ch2rwdmin))
            {
                flow.CH2RWDPressMin = 0;
            }
            else
            {
                flow.CH2RWDPressMin = Convert.ToDouble(ch2rwdmin);
            }
            string ch1rwdover = config.IniReadValue("Calculation", "CH1RWDOverTime");
            if (String.IsNullOrEmpty(ch1rwdover))
            {
                flow.CH1RWDOverTime = 0;
            }
            else
            {
                flow.CH1RWDOverTime = Convert.ToDouble(ch1rwdover);
            }
            string ch2rwdover = config.IniReadValue("Calculation", "CH2RWDOverTime");
            if (String.IsNullOrEmpty(ch2rwdover))
            {
                flow.CH2RWDOverTime = 0;
            }
            else
            {
                flow.CH2RWDOverTime = Convert.ToDouble(ch2rwdover);
            }
            string ch1_1premax = config.IniReadValue("Calculation", "CH1_1PreMax");
            if (String.IsNullOrEmpty(ch1_1premax))
            {
                flow.CH1_1PreMax = 0;
            }
            else
            {
                flow.CH1_1PreMax = Convert.ToDouble(ch1_1premax);
            }
            string ch1_2premax = config.IniReadValue("Calculation", "CH1_2PreMax");
            if (String.IsNullOrEmpty(ch1_2premax))
            {
                flow.CH1_2PreMax = 0;
            }
            else
            {
                flow.CH1_2PreMax = Convert.ToDouble(ch1_2premax);
            }
            string ch2_1premax = config.IniReadValue("Calculation", "CH2_1PreMax");
            if (String.IsNullOrEmpty(ch2_1premax))
            {
                flow.CH2_1PreMax = 0;
            }
            else
            {
                flow.CH2_1PreMax = Convert.ToDouble(ch2_1premax);
            }
            string ch2_2premax = config.IniReadValue("Calculation", "CH2_2PreMax");
            if (String.IsNullOrEmpty(ch2_2premax))
            {
                flow.CH2_2PreMax = 0;
            }
            else
            {
                flow.CH2_2PreMax = Convert.ToDouble(ch2_2premax);
            }
            return flow;
        }

        /// <summary>
        /// 读取电流电压参数
        /// </summary>
        public Model.Electricity ReadElectricity()
        {
            //string dialog;
            //dialog = "Electricity.ini";
            //ConfigINI config = new ConfigINI(machine, dialog);
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            Model.Electricity elec = new Model.Electricity();
            string ch1upadcmax = config.IniReadValue("Limits", "CH1UPADCMax");
            if (String.IsNullOrEmpty(ch1upadcmax))
            {
                elec.CH1UPADCMax = 0;
            }
            else
            {
                elec.CH1UPADCMax = Convert.ToDouble(ch1upadcmax);
            }

           



            string ch2upadcmax = config.IniReadValue("Limits", "CH2UPADCMax");
            if (String.IsNullOrEmpty(ch2upadcmax))
            {
                elec.CH2UPADCMax = 0;
            }
            else
            {
                elec.CH2UPADCMax = Convert.ToDouble(ch2upadcmax);
            }
            string ch1upadcmin = config.IniReadValue("Limits", "CH1UPADCMin");
            if (String.IsNullOrEmpty(ch1upadcmin))
            {
                elec.CH1UPADCMin = 0;
            }
            else
            {
                elec.CH1UPADCMin = Convert.ToDouble(ch1upadcmin);
            }
            string ch2upadcmin = config.IniReadValue("Limits", "CH2UPADCMin");
            if (String.IsNullOrEmpty(ch2upadcmin))
            {
                elec.CH2UPADCMin = 0;
            }
            else
            {
                elec.CH2UPADCMin = Convert.ToDouble(ch2upadcmin);
            }
            string ch1adccompe = config.IniReadValue("Limits", "CH1UPADCComp");
            if (String.IsNullOrEmpty(ch1adccompe))
            {
                elec.CH1UPADCComp = 0;
            }
            else
            {
                elec.CH1UPADCComp = Convert.ToDouble(ch1adccompe);
            }
            string ch2upadccompe = config.IniReadValue("Limits", "CH2UPADCComp");
            if (String.IsNullOrEmpty(ch2upadccompe))
            {
                elec.CH2UPADCComp = 0;
            }
            else
            {
                elec.CH2UPADCComp = Convert.ToDouble(ch2upadccompe);
            }
            string ch1upvdccmax = config.IniReadValue("Limits", "CH1UPVDCMax");
            if (String.IsNullOrEmpty(ch1upvdccmax))
            {
                elec.CH1UPVDCMax = 0;
            }
            else
            {
                elec.CH1UPVDCMax = Convert.ToDouble(ch1upvdccmax);
            }
            string ch2upvdccmax = config.IniReadValue("Limits", "CH2UPVDCMax");
            if (String.IsNullOrEmpty(ch2upvdccmax))
            {
                elec.CH2UPVDCMax = 0;
            }
            else
            {
                elec.CH2UPVDCMax = Convert.ToDouble(ch2upvdccmax);
            }
            string ch1upvdcmin = config.IniReadValue("Limits", "CH1UPVDCMin");
            if (String.IsNullOrEmpty(ch1upvdcmin))
            {
                elec.CH1UPVDCMin = 0;
            }
            else
            {
                elec.CH1UPVDCMin = Convert.ToDouble(ch1upvdcmin);
            }
            string ch2upvdcmin = config.IniReadValue("Limits", "CH2UPVDCMin");
            if (String.IsNullOrEmpty(ch2upvdcmin))
            {
                elec.CH2UPVDCMin = 0;
            }
            else
            {
                elec.CH2UPVDCMin = Convert.ToDouble(ch2upvdcmin);
            }
            string ch1upvdccompe = config.IniReadValue("Limits", "CH1UPVDCComp");
            if (String.IsNullOrEmpty(ch1upvdccompe))
            {
                elec.CH1UPVDCComp = 0;
            }
            else
            {
                elec.CH1UPVDCComp = Convert.ToDouble(ch1upvdccompe);
            }
            string ch2upvdccompe = config.IniReadValue("Limits", "CH2UPVDCComp");
            if (String.IsNullOrEmpty(ch2upvdccompe))
            {
                elec.CH2UPVDCComp = 0;
            }
            else
            {
                elec.CH2UPVDCComp = Convert.ToDouble(ch2upvdccompe);
            }
            string ch1downadcmax = config.IniReadValue("Limits", "CH1DOWNADCMax");
            if (String.IsNullOrEmpty(ch1downadcmax))
            {
                elec.CH1DOWNADCMax = 0;
            }
            else
            {
                elec.CH1DOWNADCMax = Convert.ToDouble(ch1downadcmax);
            }
            string ch2downadcmax = config.IniReadValue("Limits", "CH2DOWNADCMax");
            if (String.IsNullOrEmpty(ch2downadcmax))
            {
                elec.CH2DOWNADCMax = 0;
            }
            else
            {
                elec.CH2DOWNADCMax = Convert.ToDouble(ch2downadcmax);
            }
            string ch1downadcmin = config.IniReadValue("Limits", "CH1DOWNADCMin");
            if (String.IsNullOrEmpty(ch1downadcmin))
            {
                elec.CH1DOWNADCMin = 0;
            }
            else
            {
                elec.CH1DOWNADCMin = Convert.ToDouble(ch1downadcmin);
            }
            string ch2downadcmin = config.IniReadValue("Limits", "CH2DOWNADCMin");
            if (String.IsNullOrEmpty(ch2downadcmin))
            {
                elec.CH2DOWNADCMin = 0;
            }
            else
            {
                elec.CH2DOWNADCMin = Convert.ToDouble(ch2downadcmin);
            }
            string ch1downadccompe = config.IniReadValue("Limits", "CH1DOWNADCComp");
            if (String.IsNullOrEmpty(ch1downadccompe))
            {
                elec.CH1DOWNADCComp = 0;
            }
            else
            {
                elec.CH1DOWNADCComp = Convert.ToDouble(ch1downadccompe);
            }
            string ch2downadccompe = config.IniReadValue("Limits", "CH2DOWNADCComp");
            if (String.IsNullOrEmpty(ch2downadccompe))
            {
                elec.CH2DOWNADCComp = 0;
            }
            else
            {
                elec.CH2DOWNADCComp = Convert.ToDouble(ch2downadccompe);
            }
            string ch1downvdccmax = config.IniReadValue("Limits", "CH1DOWNVDCMax");
            if (String.IsNullOrEmpty(ch1downvdccmax))
            {
                elec.CH1DOWNVDCMax = 0;
            }
            else
            {
                elec.CH1DOWNVDCMax = Convert.ToDouble(ch1downvdccmax);
            }
            string ch2downvdccmax = config.IniReadValue("Limits", "CH2DOWNVDCMax");
            if (String.IsNullOrEmpty(ch2downvdccmax))
            {
                elec.CH2DOWNVDCMax = 0;
            }
            else
            {
                elec.CH2DOWNVDCMax = Convert.ToDouble(ch2downvdccmax);
            }
            string ch1downvdcmin = config.IniReadValue("Limits", "CH1DOWNVDCMin");
            if (String.IsNullOrEmpty(ch1downvdcmin))
            {
                elec.CH1DOWNVDCMin = 0;
            }
            else
            {
                elec.CH1DOWNVDCMin = Convert.ToDouble(ch1downvdcmin);
            }
            string ch2downvdcmin = config.IniReadValue("Limits", "CH2DOWNVDCMin");
            if (String.IsNullOrEmpty(ch2downvdcmin))
            {
                elec.CH2DOWNVDCMin = 0;
            }
            else
            {
                elec.CH2DOWNVDCMin = Convert.ToDouble(ch2downvdcmin);
            }
            string ch1downvdccompe = config.IniReadValue("Limits", "CH1DOWNVDCComp");
            if (String.IsNullOrEmpty(ch1downvdccompe))
            {
                elec.CH1DOWNVDCComp = 0;
            }
            else
            {
                elec.CH1DOWNVDCComp = Convert.ToDouble(ch1downvdccompe);
            }
            string ch2downvdccompe = config.IniReadValue("Limits", "CH2DOWNVDCComp");
            if (String.IsNullOrEmpty(ch2downvdccompe))
            {
                elec.CH2DOWNVDCComp = 0;
            }
            else
            {
                elec.CH2DOWNVDCComp = Convert.ToDouble(ch2downvdccompe);
            }
            string ch1fwdadcmax = config.IniReadValue("Limits", "CH1FWDADCMax");
            if (String.IsNullOrEmpty(ch1fwdadcmax))
            {
                elec.CH1FWDADCMax = 0;
            }
            else
            {
                elec.CH1FWDADCMax = Convert.ToDouble(ch1fwdadcmax);
            }
            string ch2fwdadcmax = config.IniReadValue("Limits", "CH2FWDADCMax");
            if (String.IsNullOrEmpty(ch2fwdadcmax))
            {
                elec.CH2FWDADCMax = 0;
            }
            else
            {
                elec.CH2FWDADCMax = Convert.ToDouble(ch2fwdadcmax);
            }
            string ch1fwdadcmin = config.IniReadValue("Limits", "CH1FWDADCMin");
            if (String.IsNullOrEmpty(ch1fwdadcmin))
            {
                elec.CH1FWDADCMin = 0;
            }
            else
            {
                elec.CH1FWDADCMin = Convert.ToDouble(ch1fwdadcmin);
            }
            string ch2fwdadcmin = config.IniReadValue("Limits", "CH2FWDADCMin");
            if (String.IsNullOrEmpty(ch2fwdadcmin))
            {
                elec.CH2FWDADCMin = 0;
            }
            else
            {
                elec.CH2FWDADCMin = Convert.ToDouble(ch2fwdadcmin);
            }
            string ch1fwdadccompe = config.IniReadValue("Limits", "CH1FWDADCComp");
            if (String.IsNullOrEmpty(ch1fwdadccompe))
            {
                elec.CH1FWDADCComp = 0;
            }
            else
            {
                elec.CH1FWDADCComp = Convert.ToDouble(ch1fwdadccompe);
            }
            string ch2fwdadccompe = config.IniReadValue("Limits", "CH2FWDADCComp");
            if (String.IsNullOrEmpty(ch2fwdadccompe))
            {
                elec.CH2FWDADCComp = 0;
            }
            else
            {
                elec.CH2FWDADCComp = Convert.ToDouble(ch2fwdadccompe);
            }
            string ch1fwdvdccmax = config.IniReadValue("Limits", "CH1FWDVDCMax");
            if (String.IsNullOrEmpty(ch1fwdvdccmax))
            {
                elec.CH1FWDVDCMax = 0;
            }
            else
            {
                elec.CH1FWDVDCMax = Convert.ToDouble(ch1fwdvdccmax);
            }
            string ch2fwdvdccmax = config.IniReadValue("Limits", "CH2FWDVDCMax");
            if (String.IsNullOrEmpty(ch2fwdvdccmax))
            {
                elec.CH2FWDVDCMax = 0;
            }
            else
            {
                elec.CH2FWDVDCMax = Convert.ToDouble(ch2fwdvdccmax);
            }
            string ch1fwdvdcmin = config.IniReadValue("Limits", "CH1FWDVDCMin");
            if (String.IsNullOrEmpty(ch1fwdvdcmin))
            {
                elec.CH1FWDVDCMin = 0;
            }
            else
            {
                elec.CH1FWDVDCMin = Convert.ToDouble(ch1fwdvdcmin);
            }
            string ch2fwdvdcmin = config.IniReadValue("Limits", "CH2FWDVDCMin");
            if (String.IsNullOrEmpty(ch2fwdvdcmin))
            {
                elec.CH2FWDVDCMin = 0;
            }
            else
            {
                elec.CH2FWDVDCMin = Convert.ToDouble(ch2fwdvdcmin);
            }
            string ch1fwdvdccompe = config.IniReadValue("Limits", "CH1FWDVDCComp");
            if (String.IsNullOrEmpty(ch1fwdvdccompe))
            {
                elec.CH1FWDVDCComp = 0;
            }
            else
            {
                elec.CH1FWDVDCComp = Convert.ToDouble(ch1fwdvdccompe);
            }
            string ch2fwdvdccompe = config.IniReadValue("Limits", "CH2FWDVDCComp");
            if (String.IsNullOrEmpty(ch2fwdvdccompe))
            {
                elec.CH2FWDVDCComp = 0;
            }
            else
            {
                elec.CH2FWDVDCComp = Convert.ToDouble(ch2fwdvdccompe);
            }
            string ch1rwdadcmax = config.IniReadValue("Limits", "CH1RWDADCMax");
            if (String.IsNullOrEmpty(ch1rwdadcmax))
            {
                elec.CH1RWDADCMax = 0;
            }
            else
            {
                elec.CH1RWDADCMax = Convert.ToDouble(ch1rwdadcmax);
            }
            string ch2rwdadcmax = config.IniReadValue("Limits", "CH2RWDADCMax");
            if (String.IsNullOrEmpty(ch2rwdadcmax))
            {
                elec.CH2RWDADCMax = 0;
            }
            else
            {
                elec.CH2RWDADCMax = Convert.ToDouble(ch2rwdadcmax);
            }
            string ch1rwdadcmin = config.IniReadValue("Limits", "CH1RWDADCMin");
            if (String.IsNullOrEmpty(ch1rwdadcmin))
            {
                elec.CH1RWDADCMin = 0;
            }
            else
            {
                elec.CH1RWDADCMin = Convert.ToDouble(ch1rwdadcmin);
            }
            string ch2rwdadcmin = config.IniReadValue("Limits", "CH2RWDADCMin");
            if (String.IsNullOrEmpty(ch2rwdadcmin))
            {
                elec.CH2RWDADCMin = 0;
            }
            else
            {
                elec.CH2RWDADCMin = Convert.ToDouble(ch2rwdadcmin);
            }
            string ch1rwdadccompe = config.IniReadValue("Limits", "CH1RWDADCComp");
            if (String.IsNullOrEmpty(ch1rwdadccompe))
            {
                elec.CH1RWDADCComp = 0;
            }
            else
            {
                elec.CH1RWDADCComp = Convert.ToDouble(ch1rwdadccompe);
            }
            string ch2rwdadccompe = config.IniReadValue("Limits", "CH2RWDADCComp");
            if (String.IsNullOrEmpty(ch2rwdadccompe))
            {
                elec.CH2RWDADCComp = 0;
            }
            else
            {
                elec.CH2RWDADCComp = Convert.ToDouble(ch2rwdadccompe);
            }
            string ch1rwdvdccmax = config.IniReadValue("Limits", "CH1RWDVDCMax");
            if (String.IsNullOrEmpty(ch1rwdvdccmax))
            {
                elec.CH1RWDVDCMax = 0;
            }
            else
            {
                elec.CH1RWDVDCMax = Convert.ToDouble(ch1rwdvdccmax);
            }
            string ch2rwdvdccmax = config.IniReadValue("Limits", "CH2RWDVDCMax");
            if (String.IsNullOrEmpty(ch2rwdvdccmax))
            {
                elec.CH2RWDVDCMax = 0;
            }
            else
            {
                elec.CH2RWDVDCMax = Convert.ToDouble(ch2rwdvdccmax);
            }
            string ch1rwdvdcmin = config.IniReadValue("Limits", "CH1RWDVDCMin");
            if (String.IsNullOrEmpty(ch1rwdvdcmin))
            {
                elec.CH1RWDVDCMin = 0;
            }
            else
            {
                elec.CH1RWDVDCMin = Convert.ToDouble(ch1rwdvdcmin);
            }
            string ch2rwdvdcmin = config.IniReadValue("Limits", "CH2RWDVDCMin");
            if (String.IsNullOrEmpty(ch2rwdvdcmin))
            {
                elec.CH2RWDVDCMin = 0;
            }
            else
            {
                elec.CH2RWDVDCMin = Convert.ToDouble(ch2rwdvdcmin);
            }
            string ch1rwdvdccompe = config.IniReadValue("Limits", "CH1RWDVDCComp");
            if (String.IsNullOrEmpty(ch1rwdvdccompe))
            {
                elec.CH1RWDVDCComp = 0;
            }
            else
            {
                elec.CH1RWDVDCComp = Convert.ToDouble(ch1rwdvdccompe);
            }
            string ch2rwdvdccompe = config.IniReadValue("Limits", "CH2RWDVDCComp");
            if (String.IsNullOrEmpty(ch2rwdvdccompe))
            {
                elec.CH2RWDVDCComp = 0;
            }
            else
            {
                elec.CH2RWDVDCComp = Convert.ToDouble(ch2rwdvdccompe);
            }
            string ch1elecmax = config.IniReadValue("Limits", "CH1ElecMax");
            if (String.IsNullOrEmpty(ch1elecmax))
            {
                elec.CH1ElecMax = 0;
            }
            else
            {
                elec.CH1ElecMax = Convert.ToDouble(ch1elecmax);
            }
            string ch2elecmax = config.IniReadValue("Limits", "CH2ElecMax");
            if (String.IsNullOrEmpty(ch2elecmax))
            {
                elec.CH2ElecMax = 0;
            }
            else
            {
                elec.CH2ElecMax = Convert.ToDouble(ch2elecmax);
            }
            string ch1elecmin = config.IniReadValue("Limits", "CH1ElecMin");
            if (String.IsNullOrEmpty(ch1elecmin))
            {
                elec.CH1ElecMin = 0;
            }
            else
            {
                elec.CH1ElecMin = Convert.ToDouble(ch1elecmin);
            }
            string ch2elecmin = config.IniReadValue("Limits", "CH2ElecMin");
            if (String.IsNullOrEmpty(ch2elecmin))
            {
                elec.CH2ElecMin = 0;
            }
            else
            {
                elec.CH2ElecMin = Convert.ToDouble(ch2elecmin);
            }
            string ch1eleccompe = config.IniReadValue("Limits", "CH1ElecCompensation");
            if (String.IsNullOrEmpty(ch1eleccompe))
            {
                elec.CH1ElecComp = 0;
            }
            else
            {
                elec.CH1ElecComp = Convert.ToDouble(ch1eleccompe);
            }
            string ch2eleccompe = config.IniReadValue("Limits", "CH2ElecCompensation");
            if (String.IsNullOrEmpty(ch2eleccompe))
            {
                elec.CH2ElecComp = 0;
            }
            else
            {
                elec.CH2ElecComp = Convert.ToDouble(ch2eleccompe);
            }


            //新增
            string CH1FWDFlowTime = config.IniReadValue("Limits", "CH1FWDFlowTime");
            if (String.IsNullOrEmpty(CH1FWDFlowTime))
            {
                elec.CH1FlowTime = 0;
            }
            else
            {
                elec.CH1FlowTime = Convert.ToDouble(CH1FWDFlowTime);
            }

            string CH2FWDFlowTime = config.IniReadValue("Limits", "CH2FWDFlowTime");
            if (String.IsNullOrEmpty(CH2FWDFlowTime))
            {
                elec.CH2FlowTime = 0;
            }
            else
            {
                elec.CH2FlowTime = Convert.ToDouble(CH2FWDFlowTime);
            }

            string CH1FWDPreTime = config.IniReadValue("Limits", "CH1FWDPreTime");
            if (String.IsNullOrEmpty(CH1FWDPreTime))
            {
                elec.CH1FreFwdTime = 0;
            }
            else
            {
                elec.CH1FreFwdTime = Convert.ToDouble(CH1FWDPreTime);
            }

            string CH2FWDPreTime = config.IniReadValue("Limits", "CH2FWDPreTime");
            if (String.IsNullOrEmpty(CH2FWDPreTime))
            {
                elec.CH2FreFwdTime = 0;
            }
            else
            {
                elec.CH2FreFwdTime = Convert.ToDouble(CH2FWDPreTime);
            }

            string CH1FWDFlowMax = config.IniReadValue("Limits", "CH1FWDFlowMax");
            if (String.IsNullOrEmpty(CH1FWDFlowMax))
            {
                elec.CH1_2FlowFwdMax = 0;
            }
            else
            {
                elec.CH1_2FlowFwdMax = Convert.ToDouble(CH1FWDFlowMax);
            }

            string CH2FWDFlowMax = config.IniReadValue("Limits", "CH2FWDFlowMax");
            if (String.IsNullOrEmpty(CH2FWDFlowMax))
            {
                elec.CH2_2FlowFwdMax = 0;
            }
            else
            {
                elec.CH2_2FlowFwdMax = Convert.ToDouble(CH2FWDFlowMax);
            }
            string CH1FWDFlowMin = config.IniReadValue("Limits", "CH1FWDFlowMin");
            if (String.IsNullOrEmpty(CH1FWDFlowMin))
            {
                elec.CH1_2FlowFwdMin = 0;
            }
            else
            {
                elec.CH1_2FlowFwdMin = Convert.ToDouble(CH1FWDFlowMin);
            }
            string CH2FWDFlowMin = config.IniReadValue("Limits", "CH2FWDFlowMin");
            if (String.IsNullOrEmpty(CH2FWDFlowMin))
            {
                elec.CH2_2FlowFwdMin = 0;
            }
            else
            {
                elec.CH2_2FlowFwdMin = Convert.ToDouble(CH2FWDFlowMin);
            }
            string CH1FWDPreMax = config.IniReadValue("Limits", "CH1FWDPreMax");
            if (String.IsNullOrEmpty(CH1FWDPreMax))
            {
                elec.CH1_1PreFwdMax = 0;
            }
            else
            {
                elec.CH1_1PreFwdMax = Convert.ToDouble(CH1FWDPreMax);
            }
            string CH2FWDPreMax = config.IniReadValue("Limits", "CH2FWDPreMax");
            if (String.IsNullOrEmpty(CH2FWDPreMax))
            {
                elec.CH2_1PreFwdMax = 0;
            }
            else
            {
                elec.CH2_1PreFwdMax = Convert.ToDouble(CH2FWDPreMax);
            }
            string CH1FWDPreMin = config.IniReadValue("Limits", "CH1FWDPreMin");
            if (String.IsNullOrEmpty(CH1FWDPreMin))
            {
                elec.CH1_2PreFwdMin = 0;
            }
            else
            {
                elec.CH1_2PreFwdMin = Convert.ToDouble(CH1FWDPreMin);
            }

            string CH2FWDPreMin = config.IniReadValue("Limits", "CH2FWDPreMin");
            if (String.IsNullOrEmpty(CH2FWDPreMin))
            {
                elec.CH2_2PreFwdMin = 0;
            }
            else
            {
                elec.CH2_2PreFwdMin = Convert.ToDouble(CH2FWDPreMin);
            }





            return elec;
        }

        ///<summary>
        /// 读取产品计数
        /// </summary>
        public Setup.ProductCount ReadProduct()
        {
            Setup.ProductCount count = new Setup.ProductCount();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            string ch1pro = config.IniReadValue("Count", "CH1Product");
            if (!String.IsNullOrEmpty(ch1pro))
            {
                count.CH1Product = Convert.ToInt32(ch1pro);
            }
            string ch1pass = config.IniReadValue("Count", "CH1PassNum");
            if (!String.IsNullOrEmpty(ch1pass))
            {
                count.CH1PassNum = Convert.ToInt32(ch1pass);
            }
            string ch1ng = config.IniReadValue("Count", "CH1FailNum");
            if (!String.IsNullOrEmpty(ch1ng))
            {
                count.CH1FailNum = Convert.ToInt32(ch1ng);
            }
            string ch2pro = config.IniReadValue("Count", "CH2Product");
            if (!String.IsNullOrEmpty(ch2pro))
            {
                count.CH2Product = Convert.ToInt32(ch2pro);
            }
            string ch2pass = config.IniReadValue("Count", "CH2PassNum");
            if (!String.IsNullOrEmpty(ch2pass))
            {
                count.CH2PassNum = Convert.ToInt32(ch2pass);
            }
            string ch2ng = config.IniReadValue("Count", "CH2FailNum");
            if (!String.IsNullOrEmpty(ch2ng))
            {
                count.CH2FailNum = Convert.ToInt32(ch2ng);
            }
            return count;
        }

        public Setup.LinConfig ReadLinConfig()
        {
            Setup.LinConfig lin = new Setup.LinConfig();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            lin.LDFFileName = config.IniReadValue("LinConfig", "LDFFileName");
            lin.UPSignalName = config.IniReadValue("LinConfig", "UPSignalName");
            lin.DOWNSignalName = config.IniReadValue("LinConfig", "DOWNSignalName");
            lin.FWDSignalName = config.IniReadValue("LinConfig", "FWDSignalName");
            lin.RWDSignalName = config.IniReadValue("LinConfig", "RWDSignalName");
            lin.Schedule_tables = config.IniReadValue("LinConfig", "Schedule_tables");
            lin.PowerSignalName = config.IniReadValue("LinConfig", "PowerSignalName");
            string powerSignalValue = config.IniReadValue("LinConfig", "PowerSignalValue");
            if (string.IsNullOrEmpty(powerSignalValue))
            {
                lin.PowerSignalValue = 0;
            }
            else
            {
                lin.PowerSignalValue = Convert.ToDouble(powerSignalValue);
            }

            //AD
            lin.ADUPSignalName = config.IniReadValue("LinConfig", "ADUPSignalName");
            lin.ADDOWNSignalName = config.IniReadValue("LinConfig", "ADDOWNSignalName");
            lin.ADFWDSignalName = config.IniReadValue("LinConfig", "ADFWDSignalName");
            lin.ADRWDSignalName = config.IniReadValue("LinConfig", "ADRWDSignalName");
            lin.ADSchedule_tables = config.IniReadValue("LinConfig", "ADSchedule_tables");
            lin.ADPowerSignalName = config.IniReadValue("LinConfig", "ADPowerSignalName");
            string ADPowerSignalValue = config.IniReadValue("LinConfig", "ADPowerSignalValue");
            if (string.IsNullOrEmpty(ADPowerSignalValue))
            {
                lin.ADPowerSignalValue = 0;
            }
            else
            {
                lin.ADPowerSignalValue = Convert.ToDouble(ADPowerSignalValue);
            }

            //BE
            lin.BEUPSignalName = config.IniReadValue("LinConfig", "BEUPSignalName");
            lin.BEDOWNSignalName = config.IniReadValue("LinConfig", "BEDOWNSignalName");
            lin.BEFWDSignalName = config.IniReadValue("LinConfig", "BEFWDSignalName");
            lin.BERWDSignalName = config.IniReadValue("LinConfig", "BERWDSignalName");
            lin.BESchedule_tables = config.IniReadValue("LinConfig", "BESchedule_tables");
            lin.BEPowerSignalName = config.IniReadValue("LinConfig", "BEPowerSignalName");
            string BEPowerSignalValue = config.IniReadValue("LinConfig", "BEPowerSignalValue");
            if (string.IsNullOrEmpty(BEPowerSignalValue))
            {
                lin.BEPowerSignalValue = 0;
            }
            else
            {
                lin.BEPowerSignalValue = Convert.ToDouble(BEPowerSignalValue);
            }
            //CF
            lin.CFUPSignalName = config.IniReadValue("LinConfig", "CFUPSignalName");
            lin.CFDOWNSignalName = config.IniReadValue("LinConfig", "CFDOWNSignalName");
            lin.CFFWDSignalName = config.IniReadValue("LinConfig", "CFFWDSignalName");
            lin.CFRWDSignalName = config.IniReadValue("LinConfig", "CFRWDSignalName");
            lin.CFSchedule_tables = config.IniReadValue("LinConfig", "CFSchedule_tables");
            lin.CFPowerSignalName = config.IniReadValue("LinConfig", "CFPowerSignalName");
            string CFPowerSignalValue = config.IniReadValue("LinConfig", "CFPowerSignalValue");
            if (string.IsNullOrEmpty(CFPowerSignalValue))
            {
                lin.CFPowerSignalValue = 0;
            }
            else
            {
                lin.CFPowerSignalValue = Convert.ToDouble(CFPowerSignalValue);
            }

            return lin;
        }

        /// <summary>
        /// 需要写进PLC里面的值
        /// </summary>
        /// <returns></returns>
        public Setup.PLCPress ReadPLCConfig()
        {
            Setup.PLCPress press = new Setup.PLCPress();
            string dialog = Form1.f1.machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            press.CH1Pressure = config.IniReadValue("PLC", "CH1Pressure");
            press.CH2Pressure = config.IniReadValue("PLC", "CH2Pressure");
            press.CH3Pressure = config.IniReadValue("PLC", "CH3Pressure");
            press.CH4Pressure = config.IniReadValue("PLC", "CH4Pressure");
            press.CH1Vol = config.IniReadValue("PLC", "CH1Vol");
            press.CH2Vol = config.IniReadValue("PLC", "CH2Vol");
            press.CH1Elect = config.IniReadValue("PLC", "CH1Elect");
            press.CH2Elect = config.IniReadValue("PLC", "CH2Elect");

            press.CKCH1Vol = config.IniReadValue("IPC", "CKCH1Vol");
            press.CKCH2Vol = config.IniReadValue("IPC", "CKCH2Vol");
            press.CKCH1Current = config.IniReadValue("IPC", "CKCH1Current");
            press.CKCH2Current = config.IniReadValue("IPC", "CKCH2Current");

            //press.CH5Pressure = config.IniReadValue("PLC", "CH5Pressure");
            //press.CH6Pressure = config.IniReadValue("PLC", "CH6Pressure");
            //press.CH7Pressure = config.IniReadValue("PLC", "CH7Pressure");
            //press.CH8Pressure = config.IniReadValue("PLC", "CH8Pressure");
            //press.CH9Pressure = config.IniReadValue("PLC", "CH9Pressure");
            //press.CH10Pressure = config.IniReadValue("PLC", "CH10Pressure");

            return press;
        }
    }
}