using HslCommunication.Profinet.Melsec;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLC1_N
{
    public class PLCConnect
    {
        MelsecMcNet melsecFx = new MelsecMcNet("192.168.1.199", 6666);//创建对象
        Thread PLC_Con;
        public bool IsConnect;
        public string ErrorInfo;
        /// <summary>
        /// 连接PLC并查看是否连接成功
        /// </summary>
        //public void PLC_Connect()
        //{
        //    PLC_Con = new Thread(PLC_IsCon);
        //    PLC_Con.IsBackground = true;
        //    PLC_Con.Start();
        //}
        public bool PLC_IsCon()
        {

            IsConnect = melsecFx.ConnectServer().IsSuccess;//连接PLC
            return IsConnect;
        }

        Thread plc_signal;
        public bool PLCIsRun = true;
        //主界面也需要操作的状态
        public bool Front_SafetyDoor;
        public bool Back_SafetyDoorUp;
        public bool Back_SafetyDoorDown;
        public bool Left_SafetyDoor;
        public bool Right_SafetyDoor;
        public bool CH1Stopping;
        public bool CH2Stopping;
        public bool Stopping;
        public bool CH1Reset;
        public bool CH2Reset;
        public bool CH1ResetFinish;
        public bool CH2ResetFinish;
        public bool CH1NeedReset;
        public bool CH2NeedReset;
        public bool CH1SafetyGrating;
        public bool CH2SafetyGrating;
        public bool PressureWarning;
        public bool CH1Run;
        public bool CH1ARun;
        public bool CH1BRun;
        public bool CH1CRun;

        public bool CH2Run;
        public bool CH2DRun;
        public bool CH2ERun;
        public bool CH2FRun;



        //        CH1Run = melsecFx.ReadBool("M3300").Content;
        //        CH1ARun = melsecFx.ReadBool("M510").Content;
        //        CH1BRun = melsecFx.ReadBool("M511").Content;
        //        CH1CRun = melsecFx.ReadBool("M512").Content;

        //        CH2Run = melsecFx.ReadBool("M3301").Content;
        //        CH2DRun = melsecFx.ReadBool("M513").Content;
        //        CH2ERun = melsecFx.ReadBool("M514").Content;
        //        CH2FRun = melsecFx.ReadBool("M515").Content;


        //需要做勾选的状态
        public bool CH1HighLevel;
        public bool CH1IGN;
        //public bool CH1UP;
        //public bool CH1DOWN;
        //public bool CH1FWD;
        public bool CH1LIN;
        public bool CH2HighLevel;
        public bool CH2IGN;
        //public bool CH2UP;
        //public bool CH2DOWN;
        //public bool CH2FWD;
        public bool CH2LIN;
        public bool Shield_SafetyDoor;
        //条码使用次数
        public bool CH1CodeStart;
        public bool CH2CodeStart;
        public int CH1CodeCount;
        public int CH2CodeCount;
        //是否有启动信号
        public bool CH1IsStart;
        public bool CH2IsStart;
        //是否有静态电流读取成功信号
        public bool CH1ReaduA;
        public bool CH2ReaduA;
        public bool CH1ReaduASignal;
        public bool CH2ReaduASignal;
        //异常信号
        public bool CH1SRCylinderError;
        public bool CH1SPCylinderError;
        public bool CH1FNCylinderError;
        public bool CH1PCylinderUPError;
        public bool CH2SRCylinderError;
        public bool CH2SPCylinderError;
        public bool CH2FNCylinderError;
        public bool CH2PCylinderUPError;
        public bool CH1ProductError;
        public bool CH2ProductError;
        //结果信号
        public bool CH1NG;
        public bool CH1OK;
        public bool CH2NG;
        public bool CH2OK;
        //通道启用
        public bool ReadCH;
        public bool CH1;
        public bool CH2;
        public bool CH3;
        public bool CH4;
        public bool AutoModel;
        //蜂鸣器信号
        public bool CH1Bee;
        public bool CH2Bee;
        //读取泄气信号
        public bool ch1rwdend;
        public bool ch2rwdend;

        /// <summary>
        /// 循环查看各个信号的实时状态 
        /// </summary>
        public void PLC_IsRun()
        {
            //plc_signal = new Thread(ReadSignal);
            //plc_signal.IsBackground = true;
            //plc_signal.Start();
            Task.Run(() => { ReadSignal(); });
        }

        public void ReadSignal()
        {
            ////用前侧安全门的地址来做判断是否有成功通讯的信号位
            //OperateResult<bool> R_X = melsecFx.ReadBool("M3030");
            //PLCIsRun = R_X.IsSuccess;

            if (PLCIsRun)
            {
                Front_SafetyDoor = melsecFx.ReadBool("M2100").Content;
                Back_SafetyDoorUp = melsecFx.ReadBool("M2101").Content;
                Back_SafetyDoorDown = melsecFx.ReadBool("M2102").Content;
                //Left_SafetyDoor = melsecFx.ReadBool("M2103").Content;
                //Right_SafetyDoor = melsecFx.ReadBool("M2104").Content;
                Left_SafetyDoor = melsecFx.ReadBool("M2104").Content;
                Right_SafetyDoor = melsecFx.ReadBool("M2103").Content;
                CH1Stopping = melsecFx.ReadBool("M2000").Content;
                CH2Stopping = melsecFx.ReadBool("M2001").Content;
                Stopping = !(melsecFx.ReadBool("X3").Content);
                CH1Reset = melsecFx.ReadBool("M1106").Content;
                CH2Reset = melsecFx.ReadBool("M1107").Content;
                CH1ResetFinish = melsecFx.ReadBool("M1108").Content;
                CH2ResetFinish = melsecFx.ReadBool("M1109").Content;
                CH1NeedReset = melsecFx.ReadBool("M1042").Content;
                CH2NeedReset = melsecFx.ReadBool("M1043").Content;
                CH1SafetyGrating = melsecFx.ReadBool("M2004").Content;
                CH2SafetyGrating = melsecFx.ReadBool("M2005").Content;
                PressureWarning = melsecFx.ReadBool("M2108").Content;


                CH1Run = melsecFx.ReadBool("M3300").Content;
                CH1ARun = melsecFx.ReadBool("M510").Content;
                CH1BRun = melsecFx.ReadBool("M511").Content;
                CH1CRun = melsecFx.ReadBool("M512").Content;

                CH2Run = melsecFx.ReadBool("M3301").Content;
                CH2DRun = melsecFx.ReadBool("M513").Content;
                CH2ERun = melsecFx.ReadBool("M514").Content;
                CH2FRun = melsecFx.ReadBool("M515").Content;

                //需要勾选的状态
                CH1HighLevel = melsecFx.ReadBool("M2020").Content;
                CH1IGN = melsecFx.ReadBool("M2021").Content;
                //CH1UP = melsecFx.ReadBool("M2022").Content;
                //CH1DOWN = melsecFx.ReadBool("M2023").Content;
                //CH1FWD = melsecFx.ReadBool("M2024").Content;
                //CH1RWD = melsecFx.ReadBool("M2005").Content;
                CH1LIN = melsecFx.ReadBool("M2028").Content;
                CH2HighLevel = melsecFx.ReadBool("M2030").Content;
                CH2IGN = melsecFx.ReadBool("M2031").Content;
                //CH2UP = melsecFx.ReadBool("M2032").Content;
                //CH2DOWN = melsecFx.ReadBool("M2033").Content;
                //CH2FWD = melsecFx.ReadBool("M2034").Content;
                //CH2RWD = melsecFx.ReadBool("M2205").Content;
                CH2LIN = melsecFx.ReadBool("M2038").Content;
                Shield_SafetyDoor = melsecFx.ReadBool("M2002").Content;
                CH1CodeStart = melsecFx.ReadBool("M1213").Content;
                CH2CodeStart = melsecFx.ReadBool("M1218").Content;
                //条码使用次数
                CH1CodeCount = melsecFx.ReadInt32("D302").Content;
                CH2CodeCount = melsecFx.ReadInt32("D306").Content;
                //读取静态电流信号
                CH1ReaduA = melsecFx.ReadBool("M3016").Content;
                CH2ReaduA = melsecFx.ReadBool("M3018").Content;
                //CH1ReaduA = melsecFx.ReadBool("M3610").Content;
                //CH2ReaduA = melsecFx.ReadBool("M3611").Content;
                //CH1ReaduASignal = melsecFx.ReadBool("M3600").Content;
                //CH2ReaduASignal = melsecFx.ReadBool("M3601").Content;
                CH1ReaduASignal = true;
                CH2ReaduASignal = true;
                CH1IsStart = melsecFx.ReadBool("M2600").Content;
                CH2IsStart = melsecFx.ReadBool("M4600").Content;
                //异常信号
                CH1ProductError = melsecFx.ReadBool("M3020").Content;
                CH2ProductError = melsecFx.ReadBool("M3021").Content;
                CH1SRCylinderError = melsecFx.ReadBool("M1190").Content;
                CH1SPCylinderError = melsecFx.ReadBool("M1191").Content;
                CH1FNCylinderError = melsecFx.ReadBool("M1192").Content;
                CH1PCylinderUPError = melsecFx.ReadBool("M1193").Content;
                CH2SRCylinderError = melsecFx.ReadBool("M1194").Content;
                CH2SPCylinderError = melsecFx.ReadBool("M1195").Content;
                CH2FNCylinderError = melsecFx.ReadBool("M1196").Content;
                CH2PCylinderUPError = melsecFx.ReadBool("M1197").Content;
                //结果信号
                CH1NG = melsecFx.ReadBool("M3204").Content;
                CH1OK = melsecFx.ReadBool("M3205").Content;
                CH2NG = melsecFx.ReadBool("M3214").Content;
                CH2OK = melsecFx.ReadBool("M3215").Content;
                //泄气结束信号
                ch1rwdend = melsecFx.ReadBool("M2501").Content;
                ch2rwdend = melsecFx.ReadBool("M4501").Content;
                ////通道启用
                if (ReadCH)
                {
                    CH1 = !(melsecFx.ReadBool("M1200").Content);
                    CH2 = !(melsecFx.ReadBool("M1201").Content);
                    CH3 = !(melsecFx.ReadBool("M1202").Content);
                    CH4 = !(melsecFx.ReadBool("M1203").Content);
                    CH1Bee = melsecFx.ReadBool("M3110").Content;
                    CH2Bee = melsecFx.ReadBool("M3111").Content;
                    AutoModel = melsecFx.ReadBool("M3124").Content;
                }
            }
        }
        Thread ch1stopping;
        /// <summary>
        /// CH1急停弹窗
        /// </summary>
        public void CH1StoppingFalse()
        {
            ch1stopping = new Thread(CH1WriteStopF);
            ch1stopping.IsBackground = true;
            ch1stopping.Start();
        }
        public void CH1WriteStopF()
        {
            melsecFx.Write("M2000", false);
        }
        Thread ch2stopping;
        /// <summary>
        /// CH2急停弹窗
        /// </summary>
        public void CH2StoppingFalse()
        {
            ch2stopping = new Thread(CH2WriteStopF);
            ch2stopping.IsBackground = true;
            ch2stopping.Start();
        }
        public void CH2WriteStopF()
        {
            melsecFx.Write("M2001", false);
        }
        Thread ch1sgrating;
        /// <summary>
        /// CH1安全光栅触发
        /// </summary>
        public void CH1SafetyGratingFlase()
        {
            ch1sgrating = new Thread(CH1WriteSGF);
            ch1sgrating.IsBackground = true;
            ch1sgrating.Start();
        }
        public void CH1WriteSGF()
        {
            melsecFx.Write("M2004", false);
        }
        Thread ch2sgrating;
        /// <summary>
        /// CH2安全光栅触发
        /// </summary>
        public void CH2SafetyGratingFlase()
        {
            ch2sgrating = new Thread(CH2WriteSGF);
            ch2sgrating.IsBackground = true;
            ch2sgrating.Start();
        }
        public void CH2WriteSGF()
        {
            melsecFx.Write("M2005", false);
        }
        Thread ch1product;
        /// <summary>
        /// CH1料未感应复位
        /// </summary>
        public void CH1ProductFalse()
        {
            ch1product = new Thread(CH1WriteProductFalse);
            ch1product.IsBackground = true;
            ch1product.Start();
        }
        public void CH1WriteProductFalse()
        {
            melsecFx.Write("M3020", false);
        }
        Thread ch2product;
        /// <summary>
        /// CH2料未感应复位
        /// </summary>
        public void CH2ProductFalse()
        {
            ch2product = new Thread(CH2WriteProductFalse);
            ch2product.IsBackground = true;
            ch2product.Start();
        }
        public void CH2WriteProductFalse()
        {
            melsecFx.Write("M3021", false);
        }
        //一通道
        Thread ch1huaguishang1;
        /// <summary>
        /// 滑轨气缸伸出
        /// </summary>
        public void CH1HuaGuiShang1()
        {
            ch1huaguishang1 = new Thread(CH1WriteHuaGuiShang1);
            ch1huaguishang1.IsBackground = true;
            ch1huaguishang1.Start();
        }
        public void CH1WriteHuaGuiShang1()
        {
            melsecFx.Write("M1051", true);
        }

        Thread ch1huaguishang2;
        /// <summary>
        /// 滑轨气缸伸出停止
        /// </summary>
        public void CH1HuaGuiShang2()
        {
            ch1huaguishang2 = new Thread(CH1WriteHuaGuiShang2);
            ch1huaguishang2.IsBackground = true;
            ch1huaguishang2.Start();
        }
        public void CH1WriteHuaGuiShang2()
        {
            melsecFx.Write("M1051", false);
        }

        Thread ch1huaguixia1;
        /// <summary>
        /// 滑轨气缸缩回
        /// </summary>
        public void CH1HuaGuiXia1()
        {
            ch1huaguixia1 = new Thread(CH1WriteHuaGuiXia1);
            ch1huaguixia1.IsBackground = true;
            ch1huaguixia1.Start();
        }
        public void CH1WriteHuaGuiXia1()
        {
            melsecFx.Write("M1052", true);
        }
        Thread ch1huaguixia2;
        /// <summary>
        /// 滑轨气缸缩回停止
        /// </summary>
        public void CH1HuaGuiXia2()
        {
            ch1huaguixia2 = new Thread(CH1WriteHuaGuiXia2);
            ch1huaguixia2.IsBackground = true;
            ch1huaguixia2.Start();
        }
        public void CH1WriteHuaGuiXia2()
        {
            melsecFx.Write("M1052", false);
        }

        Thread ch1cetuishang1;
        /// <summary>
        /// 侧推气缸伸出
        /// </summary>
        public void CH1CeTuiShang1()
        {
            ch1cetuishang1 = new Thread(CH1WriteCeTuiShang1);
            ch1cetuishang1.IsBackground = true;
            ch1cetuishang1.Start();
        }
        public void CH1WriteCeTuiShang1()
        {
            melsecFx.Write("M1053", true);
        }

        Thread ch1cetuishang2;
        /// <summary>
        /// 侧推气缸伸出停止
        /// </summary>
        public void CH1CeTuiShang2()
        {
            ch1cetuishang2 = new Thread(CH1WriteCeTuiShang2);
            ch1cetuishang2.IsBackground = true;
            ch1cetuishang2.Start();
        }
        public void CH1WriteCeTuiShang2()
        {
            melsecFx.Write("M1053", false);
        }

        Thread ch1cetuixia1;
        /// <summary>
        /// 侧推气缸缩回
        /// </summary>
        public void CH1CeTuiXia1()
        {
            ch1cetuixia1 = new Thread(CH1WriteCeTuiXia1);
            ch1cetuixia1.IsBackground = true;
            ch1cetuixia1.Start();
        }
        public void CH1WriteCeTuiXia1()
        {
            melsecFx.Write("M1054", true);
        }

        Thread ch1cetuixia2;
        /// <summary>
        /// 侧推气缸缩回停止
        /// </summary>
        public void CH1CeTuiXia2()
        {
            ch1cetuixia2 = new Thread(CH1WriteCeTuiXia2);
            ch1cetuixia2.IsBackground = true;
            ch1cetuixia2.Start();
        }
        public void CH1WriteCeTuiXia2()
        {
            melsecFx.Write("M1054", false);
        }
        Thread ch1feizhenshang1;
        /// <summary>
        /// 飞针气缸伸出
        /// </summary>
        public void CH1FeiZhenShang1()
        {
            ch1feizhenshang1 = new Thread(CH1WriteFeiZhenShang1);
            ch1feizhenshang1.IsBackground = true;
            ch1feizhenshang1.Start();
        }
        public void CH1WriteFeiZhenShang1()
        {
            melsecFx.Write("M1055", true);
        }

        Thread ch1feizhenshang2;
        /// <summary>
        /// 飞针气缸伸出停止
        /// </summary>
        public void CH1FeiZhenShang2()
        {
            ch1feizhenshang2 = new Thread(CH1WriteFeiZhenShang2);
            ch1feizhenshang2.IsBackground = true;
            ch1feizhenshang2.Start();
        }
        public void CH1WriteFeiZhenShang2()
        {
            melsecFx.Write("M1055", false);
        }

        Thread ch1feizhenxia1;
        /// <summary>
        /// 飞针气缸缩回
        /// </summary>
        public void CH1FeiZhenXia1()
        {
            ch1feizhenxia1 = new Thread(CH1WriteFeiZhenXia1);
            ch1feizhenxia1.IsBackground = true;
            ch1feizhenxia1.Start();
        }
        public void CH1WriteFeiZhenXia1()
        {
            melsecFx.Write("M1056", true);
        }

        Thread ch1feizhenxia2;
        /// <summary>
        /// 飞针气缸缩回停止
        /// </summary>
        public void CH1FeiZhenXia2()
        {
            ch1feizhenxia2 = new Thread(CH1WriteFeiZhenXia2);
            ch1feizhenxia2.IsBackground = true;
            ch1feizhenxia2.Start();
        }
        public void CH1WriteFeiZhenXia2()
        {
            melsecFx.Write("M1056", false);
        }
        Thread ch1chongqishang1;
        /// <summary>
        /// 充气气缸伸出
        /// </summary>
        public void CH1ChongQiShang1()
        {
            ch1chongqishang1 = new Thread(CH1WriteChongQiShang1);
            ch1chongqishang1.IsBackground = true;
            ch1chongqishang1.Start();
        }
        public void CH1WriteChongQiShang1()
        {
            melsecFx.Write("M1057", true);
        }

        Thread ch1chongqishang2;
        /// <summary>
        /// 充气气缸伸出停止
        /// </summary>
        public void CH1ChongQiShang2()
        {
            ch1chongqishang2 = new Thread(CH1WriteChongQiShang2);
            ch1chongqishang2.IsBackground = true;
            ch1chongqishang2.Start();
        }
        public void CH1WriteChongQiShang2()
        {
            melsecFx.Write("M1057", false);
        }

        Thread ch1chongqixia1;
        /// <summary>
        /// 充气气缸缩回
        /// </summary>
        public void CH1ChongQiXia1()
        {
            ch1chongqixia1 = new Thread(CH1WriteChongQiXia1);
            ch1chongqixia1.IsBackground = true;
            ch1chongqixia1.Start();
        }
        public void CH1WriteChongQiXia1()
        {
            melsecFx.Write("M1058", true);
        }

        Thread ch1chongqixia2;
        /// <summary>
        /// 充气气缸缩回停止
        /// </summary>
        public void CH1ChongQiXia2()
        {
            ch1chongqixia2 = new Thread(CH1WriteChongQiXia2);
            ch1chongqixia2.IsBackground = true;
            ch1chongqixia2.Start();
        }
        public void CH1WriteChongQiXia2()
        {
            melsecFx.Write("M1058", false);
        }

        //二通道
        Thread ch2huaguishang1;
        /// <summary>
        /// 滑轨气缸伸出
        /// </summary>
        public void CH2HuaGuiShang1()
        {
            ch2huaguishang1 = new Thread(CH2WriteHuaGuiShang1);
            ch2huaguishang1.IsBackground = true;
            ch2huaguishang1.Start();
        }
        public void CH2WriteHuaGuiShang1()
        {
            melsecFx.Write("M1059", true);
        }

        Thread ch2huaguishang2;
        /// <summary>
        /// 滑轨气缸伸出停止
        /// </summary>
        public void CH2HuaGuiShang2()
        {
            ch2huaguishang2 = new Thread(CH2WriteHuaGuiShang2);
            ch2huaguishang2.IsBackground = true;
            ch2huaguishang2.Start();
        }
        public void CH2WriteHuaGuiShang2()
        {
            melsecFx.Write("M1059", false);
        }

        Thread ch2huaguixia1;
        /// <summary>
        /// 滑轨气缸缩回
        /// </summary>
        public void CH2HuaGuiXia1()
        {
            ch2huaguixia1 = new Thread(CH2WriteHuaGuiXia1);
            ch2huaguixia1.IsBackground = true;
            ch2huaguixia1.Start();
        }
        public void CH2WriteHuaGuiXia1()
        {
            melsecFx.Write("M1060", true);
        }
        Thread ch2huaguixia2;
        /// <summary>
        /// 滑轨气缸缩回停止
        /// </summary>
        public void CH2HuaGuiXia2()
        {
            ch2huaguixia2 = new Thread(CH2WriteHuaGuiXia2);
            ch2huaguixia2.IsBackground = true;
            ch2huaguixia2.Start();
        }
        public void CH2WriteHuaGuiXia2()
        {
            melsecFx.Write("M1060", false);
        }

        Thread ch2cetuishang1;
        /// <summary>
        /// 侧推气缸伸出
        /// </summary>
        public void CH2CeTuiShang1()
        {
            ch2cetuishang1 = new Thread(CH2WriteCeTuiShang1);
            ch2cetuishang1.IsBackground = true;
            ch2cetuishang1.Start();
        }
        public void CH2WriteCeTuiShang1()
        {
            melsecFx.Write("M1061", true);
        }

        Thread ch2cetuishang2;
        /// <summary>
        /// 侧推气缸伸出停止
        /// </summary>
        public void CH2CeTuiShang2()
        {
            ch2cetuishang2 = new Thread(CH2WriteCeTuiShang2);
            ch2cetuishang2.IsBackground = true;
            ch2cetuishang2.Start();
        }
        public void CH2WriteCeTuiShang2()
        {
            melsecFx.Write("M1061", false);
        }

        Thread ch2cetuixia1;
        /// <summary>
        /// 侧推气缸缩回
        /// </summary>
        public void CH2CeTuiXia1()
        {
            ch2cetuixia1 = new Thread(CH2WriteCeTuiXia1);
            ch2cetuixia1.IsBackground = true;
            ch2cetuixia1.Start();
        }
        public void CH2WriteCeTuiXia1()
        {
            melsecFx.Write("M1062", true);
        }

        Thread ch2cetuixia2;
        /// <summary>
        /// 侧推气缸缩回停止
        /// </summary>
        public void CH2CeTuiXia2()
        {
            ch2cetuixia2 = new Thread(CH2WriteCeTuiXia2);
            ch2cetuixia2.IsBackground = true;
            ch2cetuixia2.Start();
        }
        public void CH2WriteCeTuiXia2()
        {
            melsecFx.Write("M1062", false);
        }
        Thread ch2feizhenshang1;
        /// <summary>
        /// 飞针气缸伸出
        /// </summary>
        public void CH2FeiZhenShang1()
        {
            ch2feizhenshang1 = new Thread(CH2WriteFeiZhenShang1);
            ch2feizhenshang1.IsBackground = true;
            ch2feizhenshang1.Start();
        }
        public void CH2WriteFeiZhenShang1()
        {
            melsecFx.Write("M1063", true);
        }

        Thread ch2feizhenshang2;
        /// <summary>
        /// 飞针气缸伸出停止
        /// </summary>
        public void CH2FeiZhenShang2()
        {
            ch2feizhenshang2 = new Thread(CH2WriteFeiZhenShang2);
            ch2feizhenshang2.IsBackground = true;
            ch2feizhenshang2.Start();
        }
        public void CH2WriteFeiZhenShang2()
        {
            melsecFx.Write("M1063", false);
        }

        Thread ch2feizhenxia1;
        /// <summary>
        /// 飞针气缸缩回
        /// </summary>
        public void CH2FeiZhenXia1()
        {
            ch2feizhenxia1 = new Thread(CH2WriteFeiZhenXia1);
            ch2feizhenxia1.IsBackground = true;
            ch2feizhenxia1.Start();
        }
        public void CH2WriteFeiZhenXia1()
        {
            melsecFx.Write("M1064", true);
        }

        Thread ch2feizhenxia2;
        /// <summary>
        /// 飞针气缸缩回停止
        /// </summary>
        public void CH2FeiZhenXia2()
        {
            ch2feizhenxia2 = new Thread(CH2WriteFeiZhenXia2);
            ch2feizhenxia2.IsBackground = true;
            ch2feizhenxia2.Start();
        }
        public void CH2WriteFeiZhenXia2()
        {
            melsecFx.Write("M1064", false);
        }
        Thread ch2chongqishang1;
        /// <summary>
        /// 充气气缸伸出
        /// </summary>
        public void CH2ChongQiShang1()
        {
            ch2chongqishang1 = new Thread(CH2WriteChongQiShang1);
            ch2chongqishang1.IsBackground = true;
            ch2chongqishang1.Start();
        }
        public void CH2WriteChongQiShang1()
        {
            melsecFx.Write("M1065", true);
        }

        Thread ch2chongqishang2;
        /// <summary>
        /// 充气气缸伸出停止
        /// </summary>
        public void CH2ChongQiShang2()
        {
            ch2chongqishang2 = new Thread(CH2WriteChongQiShang2);
            ch2chongqishang2.IsBackground = true;
            ch2chongqishang2.Start();
        }
        public void CH2WriteChongQiShang2()
        {
            melsecFx.Write("M1065", false);
        }

        Thread ch2chongqixia1;
        /// <summary>
        /// 充气气缸缩回
        /// </summary>
        public void CH2ChongQiXia1()
        {
            ch2chongqixia1 = new Thread(CH2WriteChongQiXia1);
            ch2chongqixia1.IsBackground = true;
            ch2chongqixia1.Start();
        }
        public void CH2WriteChongQiXia1()
        {
            melsecFx.Write("M1066", true);
        }

        Thread ch2chongqixia2;
        /// <summary>
        /// 充气气缸缩回停止
        /// </summary>
        public void CH2ChongQiXia2()
        {
            ch2chongqixia2 = new Thread(CH2WriteChongQiXia2);
            ch2chongqixia2.IsBackground = true;
            ch2chongqixia2.Start();
        }
        public void CH2WriteChongQiXia2()
        {
            melsecFx.Write("M1066", false);
        }
        Thread ch1valve;
        public short ch1pre;
        /// <summary>
        /// CH1读取电子调压阀示数
        /// </summary>
        public void CH1Readvalve()
        {
            ch1valve = new Thread(CH1Read);
            ch1valve.IsBackground = true;
            ch1valve.Start();
        }
        public void CH1Read()
        {
            ch1pre = melsecFx.ReadInt16("D1002").Content;
        }
        /// <summary>
        /// CH1修改电子调压阀示数
        /// </summary>
        public void CH1Writevalve()
        {
            ch1valve = new Thread(CH1Write);
            ch1valve.IsBackground = true;
            ch1valve.Start();
        }
        public void CH1Write()
        {
            melsecFx.Write("D1040", ch1pre);
            melsecFx.Write("M1180", true);
        }
        Thread ch2valve;
        public short ch2pre;
        /// <summary>
        /// CH2读取电子调压阀示数
        /// </summary>
        public void CH2Readvalve()
        {
            ch2valve = new Thread(CH2Read);
            ch2valve.IsBackground = true;
            ch2valve.Start();
        }
        public void CH2Read()
        {
            ch2pre = melsecFx.ReadInt16("D1006").Content;
        }
        /// <summary>
        /// CH2修改电子调压阀示数
        /// </summary>
        public void CH2Writevalve()
        {
            ch2valve = new Thread(CH2Write);
            ch2valve.IsBackground = true;
            ch2valve.Start();
        }
        public void CH2Write()
        {
            melsecFx.Write("D1042", ch2pre);
            melsecFx.Write("M1181", true);
        }
        Thread ch3valve;
        public short ch3pre;
        /// <summary>
        /// CH3读取电子调压阀示数
        /// </summary>
        public void CH3Readvalve()
        {
            ch3valve = new Thread(CH3Read);
            ch3valve.IsBackground = true;
            ch3valve.Start();
        }
        public void CH3Read()
        {
            ch3pre = melsecFx.ReadInt16("D1010").Content;
        }
        /// <summary>
        /// CH3修改电子调压阀示数
        /// </summary>
        public void CH3Writevalve()
        {
            ch3valve = new Thread(CH3Write);
            ch3valve.IsBackground = true;
            ch3valve.Start();
        }
        public void CH3Write()
        {
            melsecFx.Write("D1044", ch3pre);
            melsecFx.Write("M1182", true);
        }
        Thread ch4valve;
        public short ch4pre;
        /// <summary>
        /// CH4读取电子调压阀示数
        /// </summary>
        public void CH4Readvalve()
        {
            ch4valve = new Thread(CH4Read);
            ch4valve.IsBackground = true;
            ch4valve.Start();
        }
        public void CH4Read()
        {
            ch4pre = melsecFx.ReadInt16("D1014").Content;
        }
        /// <summary>
        /// CH4修改电子调压阀示数
        /// </summary>
        public void CH4Writevalve()
        {
            ch4valve = new Thread(CH4Write);
            ch4valve.IsBackground = true;
            ch4valve.Start();
        }
        public void CH4Write()
        {
            melsecFx.Write("D1046", ch4pre);
            melsecFx.Write("M1183", true);
        }
        Thread thr_ch1vol;
        public int ch1vol;
        /// <summary>
        /// CH1读取电压示数
        /// </summary>
        public void CH1ReadVol()
        {
            thr_ch1vol = new Thread(CH1_ReadVol);
            thr_ch1vol.IsBackground = true;
            thr_ch1vol.Start();
        }
        public void CH1_ReadVol()
        {
            ch1vol = melsecFx.ReadInt32("D455").Content;
        }
        /// <summary>
        /// CH1修改电压示数
        /// </summary>
        public void CH1WriteVol()
        {
            thr_ch1vol = new Thread(CH1_WriteVol);
            thr_ch1vol.IsBackground = true;
            thr_ch1vol.Start();
        }
        public void CH1_WriteVol()
        {
            melsecFx.Write("D402", ch1vol);
            melsecFx.Write("M1226", true);
        }
        Thread thr_ch2vol;
        public int ch2vol;
        /// <summary>
        /// CH2读取电压示数
        /// </summary>
        public void CH2ReadVol()
        {
            thr_ch2vol = new Thread(CH2_ReadVol);
            thr_ch2vol.IsBackground = true;
            thr_ch2vol.Start();
        }
        public void CH2_ReadVol()
        {
            ch2vol = melsecFx.ReadInt32("D483").Content;
        }
        /// <summary>
        /// CH2修改电压示数
        /// </summary>
        public void CH2WriteVol()
        {
            thr_ch2vol = new Thread(CH2_WriteVol);
            thr_ch2vol.IsBackground = true;
            thr_ch2vol.Start();
        }
        public void CH2_WriteVol()
        {
            melsecFx.Write("D412", ch2vol);
            melsecFx.Write("M1246", true);
        }
        Thread thr_ch1elec;
        public int ch1elec;
        /// <summary>
        /// CH1读取电流示数
        /// </summary>
        public void CH1ReadVolElec()
        {
            thr_ch1elec = new Thread(CH1_ReadElec);
            thr_ch1elec.IsBackground = true;
            thr_ch1elec.Start();
        }
        public void CH1_ReadElec()
        {
            ch1elec = melsecFx.ReadInt32("D407").Content;
        }
        /// <summary>
        /// CH1修改电流示数
        /// </summary>
        public void CH1WriteElec()
        {
            thr_ch1elec = new Thread(CH1_WriteElec);
            thr_ch1elec.IsBackground = true;
            thr_ch1elec.Start();
        }
        public void CH1_WriteElec()
        {
            melsecFx.Write("D404", ch1elec);
            melsecFx.Write("M1229", true);
        }
        Thread thr_ch2elec;
        public int ch2elec;
        /// <summary>
        /// CH2读取电流示数
        /// </summary>
        public void CH2ReadVolElec()
        {
            thr_ch2elec = new Thread(CH2_ReadElec);
            thr_ch2elec.IsBackground = true;
            thr_ch2elec.Start();
        }
        public void CH2_ReadElec()
        {
            ch2elec = melsecFx.ReadInt32("D417").Content;
        }
        /// <summary>
        /// CH2修改电流示数
        /// </summary>
        public void CH2WriteElec()
        {
            thr_ch2elec = new Thread(CH2_WriteElec);
            thr_ch2elec.IsBackground = true;
            thr_ch2elec.Start();
        }
        public void CH2_WriteElec()
        {
            melsecFx.Write("D414", ch2elec);
            melsecFx.Write("M1249", true);
        }
        Thread SafetyDoor;
        /// <summary>
        /// 安全门屏蔽
        /// </summary>
        public void SafetyDoorClose()
        {
            SafetyDoor = new Thread(WriteSafetyDoorClose);
            SafetyDoor.IsBackground = true;
            SafetyDoor.Start();
        }
        public void WriteSafetyDoorClose()
        {
            melsecFx.Write("M2002", true);
        }
        public void SafetyDoorOpen()
        {
            SafetyDoor = new Thread(WriteSafetyDoorOpen);
            SafetyDoor.IsBackground = true;
            SafetyDoor.Start();
        }
        public void WriteSafetyDoorOpen()
        {
            melsecFx.Write("M2002", false);
        }
        Thread thr_CH1HighLevel;
        /// <summary>
        /// CH1高低电平切换
        /// </summary>
        public void CH1HLevelTure()
        {
            thr_CH1HighLevel = new Thread(WriteCH1HLevelTure);
            thr_CH1HighLevel.IsBackground = true;
            thr_CH1HighLevel.Start();
        }
        public void WriteCH1HLevelTure()
        {
            melsecFx.Write("M2020", true);
        }
        public void CH1HLevelFlase()
        {
            thr_CH1HighLevel = new Thread(WriteCH1HLevelFalse);
            thr_CH1HighLevel.IsBackground = true;
            thr_CH1HighLevel.Start();
        }
        public void WriteCH1HLevelFalse()
        {
            melsecFx.Write("M2020", false);
        }
        Thread thr_CH1IGN;
        /// <summary>
        /// CH1IGN切换
        /// </summary>
        public void CH1IGNTure()
        {
            thr_CH1IGN = new Thread(WriteCH1IGNTure);
            thr_CH1IGN.IsBackground = true;
            thr_CH1IGN.Start();
        }
        public void WriteCH1IGNTure()
        {
            melsecFx.Write("M2021", true);
        }
        public void CH1IGNFlase()
        {
            thr_CH1IGN = new Thread(WriteCH1IGNFlase);
            thr_CH1IGN.IsBackground = true;
            thr_CH1IGN.Start();
        }
        public void WriteCH1IGNFlase()
        {
            melsecFx.Write("M2021", false);
        }

        //Thread thr_CH1UP;
        ///// <summary>
        ///// CH1UP切换,同充
        ///// </summary>
        //public void CH1UPTure()
        //{
        //    thr_CH1UP = new Thread(WriteCH1UPTure);
        //    thr_CH1UP.IsBackground = true;
        //    thr_CH1UP.Start();
        //}
        //public void WriteCH1UPTure()
        //{
        //    melsecFx.Write("M2022", true);
        //}
        //public void CH1UPFlase()
        //{
        //    thr_CH1IGN = new Thread(WriteCH1UPFlase);
        //    thr_CH1IGN.IsBackground = true;
        //    thr_CH1IGN.Start();
        //}
        //public void WriteCH1UPFlase()
        //{
        //    melsecFx.Write("M2022", false);
        //}
        //Thread thr_CH1DOWN;
        ///// <summary>
        ///// CH1DOWN切换,下充
        ///// </summary>
        //public void CH1DOWNTure()
        //{
        //    thr_CH1DOWN = new Thread(WriteCH1DOWNTure);
        //    thr_CH1DOWN.IsBackground = true;
        //    thr_CH1DOWN.Start();
        //}
        //public void WriteCH1DOWNTure()
        //{
        //    melsecFx.Write("M2023", true);
        //}
        //public void CH1DOWNFlase()
        //{
        //    thr_CH1DOWN = new Thread(WriteCH1DOWNFlase);
        //    thr_CH1DOWN.IsBackground = true;
        //    thr_CH1DOWN.Start();
        //}
        //public void WriteCH1DOWNFlase()
        //{
        //    melsecFx.Write("M2023", false);
        //}
        //Thread thr_CH1FWD;
        ///// <summary>
        ///// CH1FWD切换,同充
        ///// </summary>
        //public void CH1FWDTure()
        //{
        //    thr_CH1FWD = new Thread(WriteCH1FWDTure);
        //    thr_CH1FWD.IsBackground = true;
        //    thr_CH1FWD.Start();
        //}
        //public void WriteCH1FWDTure()
        //{
        //    melsecFx.Write("M2024", true);
        //}
        //public void CH1FWDFlase()
        //{
        //    thr_CH1FWD = new Thread(WriteCH1FWDFlase);
        //    thr_CH1FWD.IsBackground = true;
        //    thr_CH1FWD.Start();
        //}
        //public void WriteCH1FWDFlase()
        //{
        //    melsecFx.Write("M2024", false);
        //}
        //Thread thr_CH1RWD;
        ///// <summary>
        ///// CH1RWD切换,泄气
        ///// </summary>
        //public void CH1RWDTure()
        //{
        //    thr_CH1RWD = new Thread(WriteCH1RWDTure);
        //    thr_CH1RWD.IsBackground = true;
        //    thr_CH1RWD.Start();
        //}
        //public void WriteCH1RWDTure()
        //{
        //    melsecFx.Write("M2005", true);
        //}
        //public void CH1RWDFlase()
        //{
        //    thr_CH1FWD = new Thread(WriteCH1RWDFlase);
        //    thr_CH1FWD.IsBackground = true;
        //    thr_CH1FWD.Start();
        //}
        //public void WriteCH1RWDFlase()
        //{
        //    melsecFx.Write("M2005", false);
        //}
        Thread thr_CH1LIN;
        /// <summary>
        /// CH1LIN切换,LIN通讯
        /// </summary>
        public void CH1LINTure()
        {
            thr_CH1LIN = new Thread(WriteCH1LINTure);
            thr_CH1LIN.IsBackground = true;
            thr_CH1LIN.Start();
        }
        public void WriteCH1LINTure()
        {
            melsecFx.Write("M2028", true);
        }
        public void CH1LINFlase()
        {
            thr_CH1LIN = new Thread(WriteCH1LINFlase);
            thr_CH1LIN.IsBackground = true;
            thr_CH1LIN.Start();
        }
        public void WriteCH1LINFlase()
        {
            melsecFx.Write("M2028", false);
        }
        Thread thr_CH2HighLevel;
        /// <summary>
        /// CH2高低电平切换
        /// </summary>
        public void CH2HLevelTure()
        {
            thr_CH2HighLevel = new Thread(WriteCH2HLevelTure);
            thr_CH2HighLevel.IsBackground = true;
            thr_CH2HighLevel.Start();
        }
        public void WriteCH2HLevelTure()
        {
            melsecFx.Write("M2030", true);
        }
        public void CH2HLevelFlase()
        {
            thr_CH2HighLevel = new Thread(WriteCH2HLevelFalse);
            thr_CH2HighLevel.IsBackground = true;
            thr_CH2HighLevel.Start();
        }
        public void WriteCH2HLevelFalse()
        {
            melsecFx.Write("M2030", false);
        }
        Thread thr_CH2IGN;
        /// <summary>
        /// CH2IGN切换
        /// </summary>
        public void CH2IGNTure()
        {
            thr_CH2IGN = new Thread(WriteCH2IGNTure);
            thr_CH2IGN.IsBackground = true;
            thr_CH2IGN.Start();
        }
        public void WriteCH2IGNTure()
        {
            melsecFx.Write("M2031", true);
        }
        public void CH2IGNFlase()
        {
            thr_CH2IGN = new Thread(WriteCH2IGNFlase);
            thr_CH2IGN.IsBackground = true;
            thr_CH2IGN.Start();
        }
        public void WriteCH2IGNFlase()
        {
            melsecFx.Write("M2031", false);
        }

        //Thread thr_CH2UP;
        ///// <summary>
        ///// CH2UP切换,同充
        ///// </summary>
        //public void CH2UPTure()
        //{
        //    thr_CH2UP = new Thread(WriteCH2UPTure);
        //    thr_CH2UP.IsBackground = true;
        //    thr_CH2UP.Start();
        //}
        //public void WriteCH2UPTure()
        //{
        //    melsecFx.Write("M2032", true);
        //}
        //public void CH2UPFlase()
        //{
        //    thr_CH2IGN = new Thread(WriteCH2UPFlase);
        //    thr_CH2IGN.IsBackground = true;
        //    thr_CH2IGN.Start();
        //}
        //public void WriteCH2UPFlase()
        //{
        //    melsecFx.Write("M2032", false);
        //}
        //Thread thr_CH2DOWN;
        ///// <summary>
        ///// CH2DOWN切换,下充
        ///// </summary>
        //public void CH2DOWNTure()
        //{
        //    thr_CH2DOWN = new Thread(WriteCH2DOWNTure);
        //    thr_CH2DOWN.IsBackground = true;
        //    thr_CH2DOWN.Start();
        //}
        //public void WriteCH2DOWNTure()
        //{
        //    melsecFx.Write("M2033", true);
        //}
        //public void CH2DOWNFlase()
        //{
        //    thr_CH2DOWN = new Thread(WriteCH2DOWNFlase);
        //    thr_CH2DOWN.IsBackground = true;
        //    thr_CH2DOWN.Start();
        //}
        //public void WriteCH2DOWNFlase()
        //{
        //    melsecFx.Write("M2033", false);
        //}
        //Thread thr_CH2FWD;
        ///// <summary>
        ///// CH2FWD切换,同充
        ///// </summary>
        //public void CH2FWDTure()
        //{
        //    thr_CH2FWD = new Thread(WriteCH2FWDTure);
        //    thr_CH2FWD.IsBackground = true;
        //    thr_CH2FWD.Start();
        //}
        //public void WriteCH2FWDTure()
        //{
        //    melsecFx.Write("M2034", true);
        //}
        //public void CH2FWDFlase()
        //{
        //    thr_CH2FWD = new Thread(WriteCH2FWDFlase);
        //    thr_CH2FWD.IsBackground = true;
        //    thr_CH2FWD.Start();
        //}
        //public void WriteCH2FWDFlase()
        //{
        //    melsecFx.Write("M2034", false);
        //}
        //Thread thr_CH2RWD;
        ///// <summary>
        ///// CH2RWD切换,泄气
        ///// </summary>
        //public void CH2RWDTure()
        //{
        //    thr_CH2RWD = new Thread(WriteCH2RWDTure);
        //    thr_CH2RWD.IsBackground = true;
        //    thr_CH2RWD.Start();
        //}
        //public void WriteCH2RWDTure()
        //{
        //    melsecFx.Write("M2005", true);
        //}
        //public void CH2RWDFlase()
        //{
        //    thr_CH2FWD = new Thread(WriteCH2RWDFlase);
        //    thr_CH2FWD.IsBackground = true;
        //    thr_CH2FWD.Start();
        //}
        //public void WriteCH2RWDFlase()
        //{
        //    melsecFx.Write("M2005", false);
        //}
        Thread thr_CH2LIN;
        /// <summary>
        /// CH2LIN切换,LIN通讯
        /// </summary>
        public void CH2LINTure()
        {
            thr_CH2LIN = new Thread(WriteCH2LINTure);
            thr_CH2LIN.IsBackground = true;
            thr_CH2LIN.Start();
        }
        public void WriteCH2LINTure()
        {
            melsecFx.Write("M2038", true);
        }
        public void CH2LINFlase()
        {
            thr_CH2LIN = new Thread(WriteCH2LINFlase);
            thr_CH2LIN.IsBackground = true;
            thr_CH2LIN.Start();
        }
        public void WriteCH2LINFlase()
        {
            melsecFx.Write("M2038", false);
        }
        Thread ch1codepass;
        /// <summary>
        /// CH1扫码
        /// </summary>
        public void CH1CodePass()
        {
            ch1codepass = new Thread(CH1WriteCodeTrue);
            ch1codepass.IsBackground = true;
            ch1codepass.Start();
        }
        public void CH1WriteCodeTrue()
        {
            melsecFx.Write("M1211", true);
        }
        Thread ch2codepass;
        /// <summary>
        /// CH2扫码
        /// </summary>
        public void CH2CodePass()
        {
            ch2codepass = new Thread(CH2WriteCodeTrue);
            ch2codepass.IsBackground = true;
            ch2codepass.Start();
        }
        public void CH2WriteCodeTrue()
        {
            melsecFx.Write("M1216", true);
        }
        Thread thr_ch1codelife;
        public int ch1codelife;
        /// <summary>
        /// CH1修改允许启动次数
        /// </summary>
        public void CH1CodeLife()
        {
            thr_ch1codelife = new Thread(CH1_WriteCount);
            thr_ch1codelife.IsBackground = true;
            thr_ch1codelife.Start();
        }
        public void CH1_WriteCount()
        {
            melsecFx.Write("D300", ch1codelife);
        }
        Thread thr_ch2codelife;
        public int ch2codelife;
        /// <summary>
        /// CH2修改允许启动次数
        /// </summary>
        public void CH2CodeLife()
        {
            thr_ch2codelife = new Thread(CH2_WriteLife);
            thr_ch2codelife.IsBackground = true;
            thr_ch2codelife.Start();
        }
        public void CH2_WriteLife()
        {
            melsecFx.Write("D304", ch2codelife);
        }

        //Thread CH1ADCResult;
        ///// <summary>
        ///// CH1工作电流结果
        ///// </summary>
        //public void CH1ADCOK()
        //{
        //    CH1ADCResult = new Thread(WriteCH1ADCOK);
        //    CH1ADCResult.IsBackground = true;
        //    CH1ADCResult.Start();
        //}
        //public void WriteCH1ADCOK()
        //{
        //    melsecFx.Write("M1142", true);
        //}
        //public void CH1ADCNG()
        //{
        //    CH1ADCResult = new Thread(WriteCH1ADCNG);
        //    CH1ADCResult.IsBackground = true;
        //    CH1ADCResult.Start();
        //}
        //public void WriteCH1ADCNG()
        //{
        //    melsecFx.Write("M1143", true);
        //}
        //Thread CH1VDCResult;
        ///// <summary>
        ///// CH1工作电压结果
        ///// </summary>
        //public void CH1VDCOK()
        //{
        //    CH1VDCResult = new Thread(WriteCH1VDCOK);
        //    CH1VDCResult.IsBackground = true;
        //    CH1VDCResult.Start();
        //}
        //public void WriteCH1VDCOK()
        //{
        //    melsecFx.Write("M1140", true);
        //}
        //public void CH1VDCNG()
        //{
        //    CH1ADCResult = new Thread(WriteCH1VDCNG);
        //    CH1ADCResult.IsBackground = true;
        //    CH1ADCResult.Start();
        //}
        //public void WriteCH1VDCNG()
        //{
        //    melsecFx.Write("M1141", true);
        //}
        Thread CH2ADCResult;
        /// <summary>
        /// CH2工作电流结果
        /// </summary>
        public void CH2ADCOK()
        {
            CH2ADCResult = new Thread(WriteCH2ADCOK);
            CH2ADCResult.IsBackground = true;
            CH2ADCResult.Start();
        }
        public void WriteCH2ADCOK()
        {
            melsecFx.Write("M1146", true);
        }
        public void CH2ADCNG()
        {
            CH2ADCResult = new Thread(WriteCH2ADCNG);
            CH2ADCResult.IsBackground = true;
            CH2ADCResult.Start();
        }
        public void WriteCH2ADCNG()
        {
            melsecFx.Write("M1147", true);
        }
        Thread CH2VDCResult;
        /// <summary>
        /// CH2工作电压结果
        /// </summary>
        public void CH2VDCOK()
        {
            CH2VDCResult = new Thread(WriteCH2VDCOK);
            CH2VDCResult.IsBackground = true;
            CH2VDCResult.Start();
        }
        public void WriteCH2VDCOK()
        {
            melsecFx.Write("M1144", true);
        }
        public void CH2VDCNG()
        {
            CH2ADCResult = new Thread(WriteCH2VDCNG);
            CH2ADCResult.IsBackground = true;
            CH2ADCResult.Start();
        }
        public void WriteCH2VDCNG()
        {
            melsecFx.Write("M1145", true);
        }
        Thread CH1FlowValve;
        /// <summary>
        /// CH1工作阀门，默认打开，写true关闭
        /// </summary>
        public void CH1valveopen()
        {
            CH1FlowValve = new Thread(WriteCH1ValveFalse);
            CH1FlowValve.IsBackground = true;
            CH1FlowValve.Start();
        }
        public void WriteCH1ValveFalse()
        {
            melsecFx.Write("M3005", false);
        }
        public void CH1valveclose()
        {
            CH1FlowValve = new Thread(WriteCH1ValveTrue);
            CH1FlowValve.IsBackground = true;
            CH1FlowValve.Start();
        }
        public void WriteCH1ValveTrue()
        {
            melsecFx.Write("M3005", true);
        }
        Thread CH2FlowValve;
        /// <summary>
        /// CH2工作阀门，默认打开，写true关闭
        /// </summary>
        public void CH2valveopen()
        {
            CH2FlowValve = new Thread(WriteCH2ValveFalse);
            CH2FlowValve.IsBackground = true;
            CH2FlowValve.Start();
        }
        public void WriteCH2ValveFalse()
        {
            melsecFx.Write("M3006", false);
        }
        public void CH2valveclose()
        {
            CH2FlowValve = new Thread(WriteCH2ValveTrue);
            CH2FlowValve.IsBackground = true;
            CH2FlowValve.Start();
        }
        public void WriteCH2ValveTrue()
        {
            melsecFx.Write("M3006", true);
        }
        Thread CH3FlowValve;
        /// <summary>
        /// CH3工作阀门，默认打开，写true关闭
        /// </summary>
        public void CH3valveopen()
        {
            CH3FlowValve = new Thread(WriteCH3ValveFalse);
            CH3FlowValve.IsBackground = true;
            CH3FlowValve.Start();
        }
        public void WriteCH3ValveFalse()
        {
            melsecFx.Write("M3007", false);
        }
        public void CH3valveclose()
        {
            CH3FlowValve = new Thread(WriteCH3ValveTrue);
            CH3FlowValve.IsBackground = true;
            CH3FlowValve.Start();
        }
        public void WriteCH3ValveTrue()
        {
            melsecFx.Write("M3007", true);
        }
        Thread CH4FlowValve;
        /// <summary>
        /// CH4工作阀门，默认打开，写true关闭
        /// </summary>
        public void CH4valveopen()
        {
            CH4FlowValve = new Thread(WriteCH4ValveFalse);
            CH4FlowValve.IsBackground = true;
            CH4FlowValve.Start();
        }
        public void WriteCH4ValveFalse()
        {
            melsecFx.Write("M3008", false);
        }
        public void CH4valveclose()
        {
            CH4FlowValve = new Thread(WriteCH4ValveTrue);
            CH4FlowValve.IsBackground = true;
            CH4FlowValve.Start();
        }
        public void WriteCH1QC(bool CON)
        {
           var reson= melsecFx.Write("M2027", CON);
            if (reson.IsSuccess)
            {

            }
            else
            {
                MessageBox.Show("PLC在静态电流切换失败，请重启上位机");
            }
        }
        public void WriteCH2QC(bool CON)
        {
            var reson = melsecFx.Write("M2037", CON);
            if (reson.IsSuccess)
            {

            }
            else
            {
                MessageBox.Show("PLC在静态电流切换失败，请重启上位机");
            }
        }
        public void WriteCH4ValveTrue()
        {
            melsecFx.Write("M3008", true);
        }
        Thread CH1_uAValue;
        public short CH1uA;
        /// <summary>
        /// CH1静态电流值
        /// </summary>
        public void CH1uAValue()
        {
            CH1_uAValue = new Thread(CH1ReaduAValue);
            CH1_uAValue.IsBackground = true;
            CH1_uAValue.Start();
        }
        public void CH1ReaduAValue()
        {
            CH1uA = melsecFx.ReadInt16("D2001").Content;
        }

        Thread CH2_uAValue;
        public short CH2uA;
        /// <summary>
        /// CH2静态电流值
        /// </summary>
        public void CH2uAValue()
        {
            CH2_uAValue = new Thread(CH2ReaduAValue);
            CH2_uAValue.IsBackground = true;
            CH2_uAValue.Start();
        }
        public void CH2ReaduAValue()
        {
            CH2uA = melsecFx.ReadInt16("D2007").Content;
        }
        Thread CH1uAresult;
        /// <summary>
        /// 写入CH1静态电流结果
        /// </summary>
        public void CH1uAOK()
        {
            CH1uAresult = new Thread(WriteCH1uAOK);
            CH1uAresult.IsBackground = true;
            CH1uAresult.Start();
        }
        public void WriteCH1uAOK()
        {
            melsecFx.Write("M3104", true);
        }
        public void CH1uANG()
        {
            CH1uAresult = new Thread(WriteCH1uANG);
            CH1uAresult.IsBackground = true;
            CH1uAresult.Start();
        }
        public void WriteCH1uANG()
        {
            melsecFx.Write("M3105", true);
        }
        Thread CH2uAresult;
        /// <summary>
        /// 写入CH2静态电流结果
        /// </summary>
        public void CH2uAOK()
        {
            CH2uAresult = new Thread(WriteCH2uAOK);
            CH2uAresult.IsBackground = true;
            CH2uAresult.Start();
        }
        public void WriteCH2uAOK()
        {
            melsecFx.Write("M3106", true);
        }
        public void CH2uANG()
        {
            CH2uAresult = new Thread(WriteCH2uANG);
            CH2uAresult.IsBackground = true;
            CH2uAresult.Start();
        }
        public void WriteCH2uANG()
        {
            melsecFx.Write("M3107", true);
        }
        Thread CH1codestartfalse;
        /// <summary>
        /// 需要CH1条码启动的复位
        /// </summary>
        public void CH1codestartFalse()
        {
            CH1codestartfalse = new Thread(WriteCH1codestartFalse);
            CH1codestartfalse.IsBackground = true;
            CH1codestartfalse.Start();
        }
        public void WriteCH1codestartFalse()
        {
            melsecFx.Write("M1213", false);
        }
        Thread CH2codestartfalse;
        /// <summary>
        /// 需要CH2条码启动的复位
        /// </summary>
        public void CH2codestartFalse()
        {
            CH2codestartfalse = new Thread(WriteCH2codestartFalse);
            CH2codestartfalse.IsBackground = true;
            CH2codestartfalse.Start();
        }
        public void WriteCH2codestartFalse()
        {
            melsecFx.Write("M1218", false);
        }
        Thread CH1_NeedResetF;
        /// <summary>
        /// 请用户复位的复位
        /// </summary>
        public void CH1NeedResetFALSE()
        {
            CH1_NeedResetF = new Thread(WriteCH1NeedResetFALSE);
            CH1_NeedResetF.IsBackground = true;
            CH1_NeedResetF.Start();
        }
        public void WriteCH1NeedResetFALSE()
        {
            melsecFx.Write("M1042", false);
        }
        Thread CH2_NeedResetF;
        /// <summary>
        /// 请用户复位的复位
        /// </summary>
        public void CH2NeedResetFALSE()
        {
            CH2_NeedResetF = new Thread(WriteCH2NeedResetFALSE);
            CH2_NeedResetF.IsBackground = true;
            CH2_NeedResetF.Start();
        }
        public void WriteCH2NeedResetFALSE()
        {
            melsecFx.Write("M1043", false);
        }
        Thread CH1FPNG;
        /// <summary>
        /// CH1流量NG
        /// </summary>
        public void CH1FullPreNG()
        {
            CH1FPNG = new Thread(WriteCH1FullPreNG);
            CH1FPNG.IsBackground = true;
            CH1FPNG.Start();
        }
        public void WriteCH1FullPreNG()
        {
            melsecFx.Write("M3032", true);
        }
        Thread CH2FPNG;
        /// <summary>
        /// CH2流量NG
        /// </summary>
        public void CH2FullPreNG()
        {
            CH2FPNG = new Thread(WriteCH2FullPreNG);
            CH2FPNG.IsBackground = true;
            CH2FPNG.Start();
        }
        public void WriteCH2FullPreNG()
        {
            melsecFx.Write("M3033", true);
        }
        Thread CH1OC;
        /// <summary>
        /// CH1通过控制比例阀开启与关闭
        /// </summary>
        public void CH1Close()
        {
            CH1OC = new Thread(WriteCH1Close);
            CH1OC.IsBackground = true;
            CH1OC.Start();
        }
        public void WriteCH1Close()
        {
            melsecFx.Write("M1200", true);
        }
        public void CH1Open()
        {
            CH1OC = new Thread(WriteCH1Open);
            CH1OC.IsBackground = true;
            CH1OC.Start();
        }
        public void WriteCH1Open()
        {
            melsecFx.Write("M1200", false);
        }
        Thread CH2OC;
        /// <summary>
        /// CH2通过控制比例阀开启与关闭
        /// </summary>
        public void CH2Close()
        {
            CH2OC = new Thread(WriteCH2Close);
            CH2OC.IsBackground = true;
            CH2OC.Start();
        }
        public void WriteCH2Close()
        {
            melsecFx.Write("M1201", true);
        }
        public void CH2Open()
        {
            CH2OC = new Thread(WriteCH2Open);
            CH2OC.IsBackground = true;
            CH2OC.Start();
        }
        public void WriteCH2Open()
        {
            melsecFx.Write("M1201", false);
        }
        Thread CH3OC;
        /// <summary>
        /// CH3通过控制比例阀开启与关闭
        /// </summary>
        public void CH3Close()
        {
            CH3OC = new Thread(WriteCH3Close);
            CH3OC.IsBackground = true;
            CH3OC.Start();
        }
        public void WriteCH3Close()
        {
            melsecFx.Write("M1202", true);
        }
        public void CH3Open()
        {
            CH3OC = new Thread(WriteCH3Open);
            CH3OC.IsBackground = true;
            CH3OC.Start();
        }
        public void WriteCH3Open()
        {
            melsecFx.Write("M1202", false);
        }
        Thread CH4OC;
        /// <summary>
        /// CH4通过控制比例阀开启与关闭
        /// </summary>
        public void CH4Close()
        {
            CH4OC = new Thread(WriteCH4Close);
            CH4OC.IsBackground = true;
            CH4OC.Start();
        }
        public void WriteCH4Close()
        {
            melsecFx.Write("M1203", true);
        }
        public void CH4Open()
        {
            CH4OC = new Thread(WriteCH4Open);
            CH4OC.IsBackground = true;
            CH4OC.Start();
        }
        public void WriteCH4Open()
        {
            melsecFx.Write("M1203", false);
        }
        Thread CH1code;
        /// <summary>
        /// CH1扫码一次
        /// </summary>
        public void CH1Code()
        {
            CH1code = new Thread(WriteCH1CodeCount);
            CH1code.IsBackground = true;
            CH1code.Start();
        }
        public void WriteCH1CodeCount()
        {
            melsecFx.Write("M1211", true);
        }
        Thread CH2code;
        /// <summary>
        /// CH2扫码一次
        /// </summary>
        public void CH2Code()
        {
            CH2code = new Thread(WriteCH2CodeCount);
            CH2code.IsBackground = true;
            CH2code.Start();
        }
        public void WriteCH2CodeCount()
        {
            melsecFx.Write("M1216", true);
        }
        public void CH1start()
        {
            melsecFx.Write("D500", 1);
        }

        public void CH2start()
        {
            melsecFx.Write("D501", 1);
        }
        Thread CH1reset;
        /// <summary>
        /// CH1复位
        /// </summary>
        public void CH1MachineReset()
        {
            CH1reset = new Thread(WriteCH1MachineReset);
            CH1reset.IsBackground = true;
            CH1reset.Start();
        }
        public void WriteCH1MachineReset()
        {
            melsecFx.Write("M1110", true);
        }
        Thread CH2reset;
        /// <summary>
        /// CH2复位
        /// </summary>
        public void CH2MachineReset()
        {
            CH2reset = new Thread(WriteCH2MachineReset);
            CH2reset.IsBackground = true;
            CH2reset.Start();
        }
        public void WriteCH2MachineReset()
        {
            melsecFx.Write("M1111", true);
        }
        Thread grating;
        /// <summary>
        /// 安全光栅
        /// </summary>
        public void GratingOpen()
        {
            grating = new Thread(WriteGratingOpen);
            grating.IsBackground = true;
            grating.Start();
        }
        public void WriteGratingOpen()
        {
            melsecFx.Write("M308", true);
        }
        public void GratingClose()
        {
            grating = new Thread(WriteGratingClose);
            grating.IsBackground = true;
            grating.Start();
        }
        public void WriteGratingClose()
        {
            melsecFx.Write("M308", false);
        }
        Thread CH1power;
        /// <summary>
        /// CH1程控电源
        /// </summary>
        public void CH1PowerOpen()
        {
            CH1power = new Thread(WriteCH1PowerOpen);
            CH1power.IsBackground = true;
            CH1power.Start();
        }
        public void WriteCH1PowerOpen()
        {
            System.Threading.Thread.Sleep(1000);
            melsecFx.Write("M1222", true);
        }
        public void CH1PowerClose()
        {
            CH1power = new Thread(WriteCH1PowerClose);
            CH1power.IsBackground = true;
            CH1power.Start();
        }
        public void WriteCH1PowerClose()
        {
            melsecFx.Write("M1224", true);
        }
        Thread CH2power;
        /// <summary>
        /// CH2程控电源
        /// </summary>
        public void CH2PowerOpen()
        {
            CH2power = new Thread(WriteCH2PowerOpen);
            CH2power.IsBackground = true;
            CH2power.Start();
        }
        public void WriteCH2PowerOpen()
        {
            melsecFx.Write("M1242", true);
        }
        public void CH2PowerClose()
        {
            CH2power = new Thread(WriteCH2PowerClose);
            CH2power.IsBackground = true;
            CH2power.Start();
        }
        public void WriteCH2PowerClose()
        {
            melsecFx.Write("M1244", true);
        }

        Thread CH1ValveBreak;
        /// <summary>
        /// CH1阀泵断开
        /// </summary>
        public void CH1PLCValveBreak()
        {
            CH1ValveBreak = new Thread(WriteCH1PLCValveBreak);
            CH1ValveBreak.IsBackground = true;
            CH1ValveBreak.Start();
        }
        public void WriteCH1PLCValveBreak()
        {
            melsecFx.Write("M3024", true);
        }
        Thread CH2ValveBreak;
        /// <summary>
        /// CH2阀泵断开
        /// </summary>
        public void CH2PLCValveBreak()
        {
            CH2ValveBreak = new Thread(WriteCH2PLCValveBreak);
            CH2ValveBreak.IsBackground = true;
            CH2ValveBreak.Start();
        }
        public void WriteCH2PLCValveBreak()
        {
            melsecFx.Write("M3025", true);
        }
        //Thread CH1FlowEnd;
        ///// <summary>
        ///// CH1流量测试完成
        ///// </summary>
        //public void CH1FlowFinish()
        //{
        //    CH1FlowEnd = new Thread(WriteCH1FlowFinish);
        //    CH1FlowEnd.IsBackground = true;
        //    CH1FlowEnd.Start();
        //}
        //public void WriteCH1FlowFinish()
        //{
        //    melsecFx.Write("M3030", true);
        //}
        //Thread CH2FlowEnd;
        ///// <summary>
        ///// CH2流量测试完成
        ///// </summary>
        //public void CH2FlowFinish()
        //{
        //    CH2FlowEnd = new Thread(WriteCH2FlowFinish);
        //    CH2FlowEnd.IsBackground = true;
        //    CH2FlowEnd.Start();
        //}
        //public void WriteCH2FlowFinish()
        //{
        //    melsecFx.Write("M3031", true);
        //}
        //Thread CH1Flowfail;
        ///// <summary>
        ///// CH1流量测试NG
        ///// </summary>
        //public void CH1FlowNG()
        //{
        //    CH1Flowfail = new Thread(WriteCH1FlowNG);
        //    CH1Flowfail.IsBackground = true;
        //    CH1Flowfail.Start();
        //}
        //public void WriteCH1FlowNG()
        //{
        //    melsecFx.Write("M3032", true);
        //}
        Thread CH2Flowfail;
        /// <summary>
        /// CH2流量测试NG
        /// </summary>
        public void CH2FlowNG()
        {
            CH2Flowfail = new Thread(WriteCH2FlowNG);
            CH2Flowfail.IsBackground = true;
            CH2Flowfail.Start();
        }
        public void WriteCH2FlowNG()
        {
            melsecFx.Write("M3033", true);
        }
        Thread CH1LinEND;
        /// <summary>
        /// CH1Lin矩阵通讯结束
        /// </summary>
        public void CH1LinFinish()
        {
            CH1LinEND = new Thread(WriteCH1LinFinish);
            CH1LinEND.IsBackground = true;
            CH1LinEND.Start();
        }
        public void WriteCH1LinFinish()
        {
            melsecFx.Write("M3113", true);
        }
        Thread CH2LinEND;
        /// <summary>
        /// CH2Lin矩阵通讯结束
        /// </summary>
        public void CH2LinFinish()
        {
            CH2LinEND = new Thread(WriteCH2LinFinish);
            CH2LinEND.IsBackground = true;
            CH2LinEND.Start();
        }
        public void WriteCH2LinFinish()
        {
            melsecFx.Write("M3114", true);
        }
        Thread PressError;
        /// <summary>
        /// 气压报警
        /// </summary>
        public void PressWarning()
        {
            PressError = new Thread(WritePressWarning);
            PressError.IsBackground = true;
            PressError.Start();
        }
        public void WritePressWarning()
        {
            melsecFx.Write("M2108", false);
        }
        Thread ch1bee;
        /// <summary>
        /// CH1蜂鸣器
        /// </summary>
        public void CH1BeeOpen()
        {
            ch1bee = new Thread(WriteCH1BeeOpen);
            ch1bee.IsBackground = true;
            ch1bee.Start();
        }
        public void WriteCH1BeeOpen()
        {
            melsecFx.Write("M3110", true);
        }
        public void CH1BeeClose()
        {
            ch1bee = new Thread(WriteCH1BeeClose);
            ch1bee.IsBackground = true;
            ch1bee.Start();
        }
        public void WriteCH1BeeClose()
        {
            melsecFx.Write("M3110", false);
        }
        Thread ch2bee;
        /// <summary>
        /// CH2蜂鸣器
        /// </summary>
        public void CH2BeeOpen()
        {
            ch2bee = new Thread(WriteCH2BeeOpen);
            ch2bee.IsBackground = true;
            ch2bee.Start();
        }
        public void WriteCH2BeeOpen()
        {
            melsecFx.Write("M3111", true);
        }
        public void CH2BeeClose()
        {
            ch2bee = new Thread(WriteCH2BeeClose);
            ch2bee.IsBackground = true;
            ch2bee.Start();
        }
        public void WriteCH2BeeClose()
        {
            melsecFx.Write("M3111", false);
        }
        Thread automodel;
        /// <summary>
        /// 自动状态下禁止手动
        /// </summary>
        public void AutoModelFalse()
        {
            automodel = new Thread(WriteAutoModelFalse);
            automodel.IsBackground = true;
            automodel.Start();
        }
        public void WriteAutoModelFalse()
        {
            melsecFx.Write("M3124", false);
        }
        Thread ch1valveaction;
        /// <summary>
        /// CH1阀泵单独动作
        /// </summary>
        public void CH1ValveAction()
        {
            ch1valveaction = new Thread(WriteCH1ValveAction);
            ch1valveaction.IsBackground = true;
            ch1valveaction.Start();
        }
        public void WriteCH1ValveAction()
        {
            melsecFx.Write("M3120", false);
        }
        /// <summary>
        /// CH1阀泵单独动作停止
        /// </summary>
        public void CH1VActionStop()
        {
            ch1valveaction = new Thread(WriteCH1VActionStop);
            ch1valveaction.IsBackground = true;
            ch1valveaction.Start();
        }
        public void WriteCH1VActionStop()
        {
            melsecFx.Write("M3121", false);
        }
        Thread ch2valveaction;
        /// <summary>
        /// CH2阀泵单独动作
        /// </summary>
        public void CH2ValveAction()
        {
            ch2valveaction = new Thread(WriteCH2ValveAction);
            ch2valveaction.IsBackground = true;
            ch2valveaction.Start();
        }
        public void WriteCH2ValveAction()
        {
            melsecFx.Write("M3122", false);
        }
        /// <summary>
        /// CH2阀泵单独动作停止
        /// </summary>
        public void CH2VActionStop()
        {
            ch2valveaction = new Thread(WriteCH2VActionStop);
            ch2valveaction.IsBackground = true;
            ch2valveaction.Start();
        }
        public void WriteCH2VActionStop()
        {
            melsecFx.Write("M3123", false);
        }
        Thread ch1upstart;
        /// <summary>
        /// CH1上充开始
        /// </summary>
        public void CH1UPStart()
        {
            ch1upstart = new Thread(WriteCH1UPStart);
            ch1upstart.IsBackground = true;
            ch1upstart.Start();
        }
        public void WriteCH1UPStart()
        {
            melsecFx.Write("M2022", true);
        }
        Thread ch1upadc;
        /// <summary>
        /// CH1上充电流结果
        /// </summary>
        public void CH1UPADCOK()
        {
            ch1upadc = new Thread(WriteCH1UPADCOK);
            ch1upadc.IsBackground = true;
            ch1upadc.Start();
        }
        public void WriteCH1UPADCOK()
        {
            melsecFx.Write("M2204", true);
        }
        public void CH1UPADCNG()
        {
            ch1upadc = new Thread(WriteCH1UPADCNG);
            ch1upadc.IsBackground = true;
            ch1upadc.Start();
        }
        public void WriteCH1UPADCNG()
        {
            melsecFx.Write("M2205", true);
        }
        Thread ch1upvdc;
        /// <summary>
        /// CH1上充电压结果
        /// </summary>
        public void CH1UPVDCOK()
        {
            ch1upvdc = new Thread(WriteCH1UPVDCOK);
            ch1upvdc.IsBackground = true;
            ch1upvdc.Start();
        }
        public void WriteCH1UPVDCOK()
        {
            melsecFx.Write("M2206", true);
        }
        public void CH1UPVDCNG()
        {
            ch1upvdc = new Thread(WriteCH1UPVDCNG);
            ch1upvdc.IsBackground = true;
            ch1upvdc.Start();
        }
        public void WriteCH1UPVDCNG()
        {
            melsecFx.Write("M2207", true);
        }
        Thread ch1upflow;
        /// <summary>
        /// CH1上充流量结果,此结果为上充的同时下充打开测得的流量结果
        /// </summary>
        public void CH1UPFLOWOK()
        {
            ch1upflow = new Thread(WriteCH1UPFLOWOK);
            ch1upflow.IsBackground = true;
            ch1upflow.Start();
        }
        public void WriteCH1UPFLOWOK()
        {
            melsecFx.Write("M2212", true);
        }
        public void CH1UPFLOWNG()
        {
            ch1upflow = new Thread(WriteCH1UPFLOWNG);
            ch1upflow.IsBackground = true;
            ch1upflow.Start();
        }
        public void WriteCH1UPFLOWNG()
        {
            melsecFx.Write("M2213", true);
        }
        Thread ch1upflowend;
        /// <summary>
        /// CH1上充流量测试完成
        /// </summary>
        public void CH1UPFlowEnd()
        {
            ch1upflowend = new Thread(WriteCH1UPFlowEnd);
            ch1upflowend.IsBackground = true;
            ch1upflowend.Start();
        }
        public void WriteCH1UPFlowEnd()
        {
            melsecFx.Write("M2211", true);
        }
        Thread ch1upratio;
        /// <summary>
        /// CH1比值结果
        /// </summary>
        public void CH1RatioOK()
        {
            ch1upratio = new Thread(WriteCH1RatioOK);
            ch1upratio.IsBackground = true;
            ch1upratio.Start();
        }
        public void WriteCH1RatioOK()
        {
            melsecFx.Write("M2610", true);
        }
        public void CH1RatioNG()
        {
            ch1upratio = new Thread(WriteCH1RatioNG);
            ch1upratio.IsBackground = true;
            ch1upratio.Start();
        }
        public void WriteCH1RatioNG()
        {
            melsecFx.Write("M2611", true);
        }
        Thread ch1downstart;
        /// <summary>
        /// CH1下充开始
        /// </summary>
        public void CH1DOWNStart()
        {
            ch1downstart = new Thread(WriteCH1DOWNStart);
            ch1downstart.IsBackground = true;
            ch1downstart.Start();
        }
        public void WriteCH1DOWNStart()
        {
            melsecFx.Write("M2023", true);
        }
        Thread ch1downadc;
        /// <summary>
        /// CH1下充电流结果
        /// </summary>
        public void CH1DOWNADCOK()
        {
            ch1downadc = new Thread(WriteCH1DOWNADCOK);
            ch1downadc.IsBackground = true;
            ch1downadc.Start();
        }
        public void WriteCH1DOWNADCOK()
        {
            melsecFx.Write("M2304", true);
        }
        public void CH1DOWNADCNG()
        {
            ch1downadc = new Thread(WriteCH1DOWNADCNG);
            ch1downadc.IsBackground = true;
            ch1downadc.Start();
        }
        public void WriteCH1DOWNADCNG()
        {
            melsecFx.Write("M2305", true);
        }
        Thread ch1downvdc;
        /// <summary>
        /// CH1下充电压结果
        /// </summary>
        public void CH1DOWNVDCOK()
        {
            ch1downvdc = new Thread(WriteCH1DOWNVDCOK);
            ch1downvdc.IsBackground = true;
            ch1downvdc.Start();
        }
        public void WriteCH1DOWNVDCOK()
        {
            melsecFx.Write("M2306", true);
        }
        public void CH1DOWNVDCNG()
        {
            ch1downvdc = new Thread(WriteCH1DOWNVDCNG);
            ch1downvdc.IsBackground = true;
            ch1downvdc.Start();
        }
        public void WriteCH1DOWNVDCNG()
        {
            melsecFx.Write("M2307", true);
        }
        Thread ch1downflow;
        /// <summary>
        /// CH1下充流量结果,此结果为下充的同时上充打开测得的流量结果
        /// </summary>
        public void CH1DOWNFLOWOK()
        {
            ch1downflow = new Thread(WriteCH1DOWNFLOWOK);
            ch1downflow.IsBackground = true;
            ch1downflow.Start();
        }
        public void WriteCH1DOWNFLOWOK()
        {
            melsecFx.Write("M2312", true);
        }
        /// <summary>
        /// CH1下充流量结果,此结果为下充的同时上充打开测得的流量结果
        /// </summary>
        public void CH1DOWNFLOWNG()
        {
            ch1downflow = new Thread(WriteCH1DOWNFLOWNG);
            ch1downflow.IsBackground = true;
            ch1downflow.Start();
        }
        public void WriteCH1DOWNFLOWNG()
        {
            melsecFx.Write("M2313", true);
        }
        Thread ch1downflowend;
        /// <summary>
        /// CH1下充流量测试完成
        /// </summary>
        public void CH1DOWNFlowEnd()
        {
            ch1downflowend = new Thread(WriteCH1DOWNFlowEnd);
            ch1downflowend.IsBackground = true;
            ch1downflowend.Start();
        }
        public void WriteCH1DOWNFlowEnd()
        {
            melsecFx.Write("M2311", true);
        }
        Thread ch1fwdstart;
        /// <summary>
        /// CH1同充开始
        /// </summary>
        public void CH1FWDStart()
        {
            ch1fwdstart = new Thread(WriteCH1FWDStart);
            ch1fwdstart.IsBackground = true;
            ch1fwdstart.Start();
        }
        public void WriteCH1FWDStart()
        {
            melsecFx.Write("M2024", true);
        }
        Thread ch1fwdadc;
        /// <summary>
        /// CH1同充电流结果
        /// </summary>
        public void CH1FWDADCOK()
        {
            ch1fwdadc = new Thread(WriteCH1FWDADCOK);
            ch1fwdadc.IsBackground = true;
            ch1fwdadc.Start();
        }
        public void WriteCH1FWDADCOK()
        {
            melsecFx.Write("M2408", true);
        }
        public void CH1FWDADCNG()
        {
            ch1fwdadc = new Thread(WriteCH1FWDADCNG);
            ch1fwdadc.IsBackground = true;
            ch1fwdadc.Start();
        }
        public void WriteCH1FWDADCNG()
        {
            melsecFx.Write("M2409", true);
        }
        Thread ch1fwdvdc;
        /// <summary>
        /// CH1同充电压结果
        /// </summary>
        public void CH1FWDVDCOK()
        {
            ch1fwdvdc = new Thread(WriteCH1FWDVDCOK);
            ch1fwdvdc.IsBackground = true;
            ch1fwdvdc.Start();
        }
        public void WriteCH1FWDVDCOK()
        {
            melsecFx.Write("M2410", true);
        }
        public void CH1FWDVDCNG()
        {
            ch1fwdvdc = new Thread(WriteCH1FWDVDCNG);
            ch1fwdvdc.IsBackground = true;
            ch1fwdvdc.Start();
        }
        public void WriteCH1FWDVDCNG()
        {
            melsecFx.Write("M2411", true);
        }
        Thread ch1fwdflowend;
        /// <summary>
        /// CH1同充流量测试完成
        /// </summary>
        public void CH1FWDFlowEnd()
        {
            ch1fwdflowend = new Thread(WriteCH1FWDFlowEnd);
            ch1fwdflowend.IsBackground = true;
            ch1fwdflowend.Start();
        }
        public void WriteCH1FWDFlowEnd()
        {
            melsecFx.Write("M2416", true);
        }
        Thread ch1rwdstart;
        /// <summary>
        /// CH1泄气开始
        /// </summary>
        public void CH1RWDStart()
        {
            ch1rwdstart = new Thread(WriteCH1RWDStart);
            ch1rwdstart.IsBackground = true;
            ch1rwdstart.Start();
        }
        public void WriteCH1RWDStart()
        {
            melsecFx.Write("M2025", true);
        }
        Thread ch1rwdadc;
        /// <summary>
        /// CH1泄气电流结果
        /// </summary>
        public void CH1RWDADCOK()
        {
            ch1rwdadc = new Thread(WriteCH1RWDADCOK);
            ch1rwdadc.IsBackground = true;
            ch1rwdadc.Start();
        }
        public void WriteCH1RWDADCOK()
        {
            melsecFx.Write("M2508", true);
        }
        public void CH1RWDADCNG()
        {
            ch1rwdadc = new Thread(WriteCH1RWDADCNG);
            ch1rwdadc.IsBackground = true;
            ch1rwdadc.Start();
        }
        public void WriteCH1RWDADCNG()
        {
            melsecFx.Write("M2509", true);
        }
        Thread ch1rwdvdc;
        /// <summary>
        /// CH1泄气电压结果
        /// </summary>
        public void CH1RWDVDCOK()
        {
            ch1rwdvdc = new Thread(WriteCH1RWDVDCOK);
            ch1rwdvdc.IsBackground = true;
            ch1rwdvdc.Start();
        }
        public void WriteCH1RWDVDCOK()
        {
            melsecFx.Write("M2510", true);
        }
        public void CH1RWDVDCNG()
        {
            ch1rwdvdc = new Thread(WriteCH1RWDVDCNG);
            ch1rwdvdc.IsBackground = true;
            ch1rwdvdc.Start();
        }
        public void WriteCH1RWDVDCNG()
        {
            melsecFx.Write("M2511", true);
        }
        Thread ch1rwdflowend;
        /// <summary>
        /// CH1泄气压力测试完成
        /// </summary>
        public void CH1RWDFlowEnd()
        {
            ch1rwdflowend = new Thread(WriteCH1RWDFlowEnd);
            ch1rwdflowend.IsBackground = true;
            ch1rwdflowend.Start();
        }
        public void WriteCH1RWDFlowEnd()
        {
            melsecFx.Write("M2516", true);
        }
        Thread ch1rwdpressok;
        /// <summary>
        /// CH1泄气压力测试OK
        /// </summary>
        public void CH1RWDPressOK()
        {
            ch1rwdpressok = new Thread(WriteCH1RWDPressOK);
            ch1rwdpressok.IsBackground = true;
            ch1rwdpressok.Start();
        }
        public void WriteCH1RWDPressOK()
        {
            melsecFx.Write("M2512", true);
        }
        Thread ch1rwdpressng;
        /// <summary>
        /// CH1泄气压力测试NG
        /// </summary>
        public void CH1RWDPressNG()
        {
            ch1rwdpressng = new Thread(WriteCH1RWDPressNG);
            ch1rwdpressng.IsBackground = true;
            ch1rwdpressng.Start();
        }
        public void WriteCH1RWDPressNG()
        {
            melsecFx.Write("M2513", true);
        }
        Thread ch1flowend;
        /// <summary>
        /// CH1全部流程测试完成
        /// </summary>
        public void CH1FlowEnd()
        {
            ch1flowend = new Thread(WriteCH1FlowEnd);
            ch1flowend.IsBackground = true;
            ch1flowend.Start();
        }
        public void WriteCH1FlowEnd()
        {
            melsecFx.Write("M2601", true);
        }
        Thread ch2flowend;
        /// <summary>
        /// CH2全部流程测试完成
        /// </summary>
        public void CH2FlowEnd()
        {
            ch2flowend = new Thread(WriteCH2FlowEnd);
            ch2flowend.IsBackground = true;
            ch2flowend.Start();
        }
        public void WriteCH2FlowEnd()
        {
            melsecFx.Write("M4601", true);
        }
        Thread ch1rwdOvertime;
        public short ch1rwdtime;
        /// <summary>
        /// CH1泄气时间
        /// </summary>
        public void CH1RWDOvertime()
        {
            ch1rwdOvertime = new Thread(WriteCH1RWDOvertime);
            ch1rwdOvertime.IsBackground = true;
            ch1rwdOvertime.Start();
        }
        public void WriteCH1RWDOvertime()
        {
            melsecFx.Write("D800", ch1rwdtime);
        }
        Thread ch2rwdOvertime;
        public short ch2rwdtime;
        /// <summary>
        /// CH2泄气时间
        /// </summary>
        public void CH2RWDOvertime()
        {
            ch2rwdOvertime = new Thread(WriteCH2RWDOvertime);
            ch2rwdOvertime.IsBackground = true;
            ch2rwdOvertime.Start();
        }
        public void WriteCH2RWDOvertime()
        {
            melsecFx.Write("D802", ch2rwdtime);
        }
        Thread ch2upstart;
        /// <summary>
        /// CH2上充开始
        /// </summary>
        public void CH2UPStart()
        {
            ch2upstart = new Thread(WriteCH2UPStart);
            ch2upstart.IsBackground = true;
            ch2upstart.Start();
        }
        public void WriteCH2UPStart()
        {
            melsecFx.Write("M2032", true);
        }
        Thread ch2upadc;
        /// <summary>
        /// CH2上充电流结果
        /// </summary>
        public void CH2UPADCOK()
        {
            ch2upadc = new Thread(WriteCH2UPADCOK);
            ch2upadc.IsBackground = true;
            ch2upadc.Start();
        }
        public void WriteCH2UPADCOK()
        {
            melsecFx.Write("M4204", true);
        }
        public void CH2UPADCNG()
        {
            ch2upadc = new Thread(WriteCH2UPADCNG);
            ch2upadc.IsBackground = true;
            ch2upadc.Start();
        }
        public void WriteCH2UPADCNG()
        {
            melsecFx.Write("M4205", true);
        }
        Thread ch2upvdc;
        /// <summary>
        /// CH2上充电压结果
        /// </summary>
        public void CH2UPVDCOK()
        {
            ch2upvdc = new Thread(WriteCH2UPVDCOK);
            ch2upvdc.IsBackground = true;
            ch2upvdc.Start();
        }
        public void WriteCH2UPVDCOK()
        {
            melsecFx.Write("M4206", true);
        }
        public void CH2UPVDCNG()
        {
            ch2upvdc = new Thread(WriteCH2UPVDCNG);
            ch2upvdc.IsBackground = true;
            ch2upvdc.Start();
        }
        public void WriteCH2UPVDCNG()
        {
            melsecFx.Write("M4207", true);
        }
        Thread ch2upflow;
        /// <summary>
        /// CH2上充流量结果,此结果为上充的同时下充打开测得的流量结果
        /// </summary>
        public void CH2UPFLOWOK()
        {
            ch2upflow = new Thread(WriteCH2UPFLOWOK);
            ch2upflow.IsBackground = true;
            ch2upflow.Start();
        }
        public void WriteCH2UPFLOWOK()
        {
            melsecFx.Write("M4212", true);
        }
        public void CH2UPFLOWNG()
        {
            ch2upflow = new Thread(WriteCH2UPFLOWNG);
            ch2upflow.IsBackground = true;
            ch2upflow.Start();
        }
        public void WriteCH2UPFLOWNG()
        {
            melsecFx.Write("M4213", true);
        }
        Thread ch2upflowend;
        /// <summary>
        /// CH2上充流量测试完成
        /// </summary>
        public void CH2UPFlowEnd()
        {
            ch2upflowend = new Thread(WriteCH2UPFlowEnd);
            ch2upflowend.IsBackground = true;
            ch2upflowend.Start();
        }
        public void WriteCH2UPFlowEnd()
        {
            melsecFx.Write("M4211", true);
        }
        Thread ch2upratio;
        /// <summary>
        /// CH2比值结果
        /// </summary>
        public void CH2RatioOK()
        {
            ch2upratio = new Thread(WriteCH2RatioOK);
            ch2upratio.IsBackground = true;
            ch2upratio.Start();
        }
        public void WriteCH2RatioOK()
        {
            melsecFx.Write("M4610", true);
        }
        public void CH2RatioNG()
        {
            ch2upratio = new Thread(WriteCH2RatioNG);
            ch2upratio.IsBackground = true;
            ch2upratio.Start();
        }
        public void WriteCH2RatioNG()
        {
            melsecFx.Write("M4611", true);
        }
        Thread ch2downstart;
        /// <summary>
        /// CH2下充开始
        /// </summary>
        public void CH2DOWNStart()
        {
            ch2downstart = new Thread(WriteCH2DOWNStart);
            ch2downstart.IsBackground = true;
            ch2downstart.Start();
        }
        public void WriteCH2DOWNStart()
        {
            melsecFx.Write("M2033", true);
        }
        Thread ch2downadc;
        /// <summary>
        /// CH2下充电流结果
        /// </summary>
        public void CH2DOWNADCOK()
        {
            ch2downadc = new Thread(WriteCH2DOWNADCOK);
            ch2downadc.IsBackground = true;
            ch2downadc.Start();
        }
        public void WriteCH2DOWNADCOK()
        {
            melsecFx.Write("M4304", true);
        }
        public void CH2DOWNADCNG()
        {
            ch2downadc = new Thread(WriteCH2DOWNADCNG);
            ch2downadc.IsBackground = true;
            ch2downadc.Start();
        }
        public void WriteCH2DOWNADCNG()
        {
            melsecFx.Write("M4305", true);
        }
        Thread ch2downvdc;
        /// <summary>
        /// CH2下充电压结果
        /// </summary>
        public void CH2DOWNVDCOK()
        {
            ch2downvdc = new Thread(WriteCH2DOWNVDCOK);
            ch2downvdc.IsBackground = true;
            ch2downvdc.Start();
        }
        public void WriteCH2DOWNVDCOK()
        {
            melsecFx.Write("M4306", true);
        }
        public void CH2DOWNVDCNG()
        {
            ch2downvdc = new Thread(WriteCH2DOWNVDCNG);
            ch2downvdc.IsBackground = true;
            ch2downvdc.Start();
        }
        public void WriteCH2DOWNVDCNG()
        {
            melsecFx.Write("M4307", true);
        }
        Thread ch2downflow;
        /// <summary>
        /// CH2下充流量结果,此结果为下充的同时上充打开测得的流量结果
        /// </summary>
        public void CH2DOWNFLOWOK()
        {
            ch2downflow = new Thread(WriteCH2DOWNFLOWOK);
            ch2downflow.IsBackground = true;
            ch2downflow.Start();
        }
        public void WriteCH2DOWNFLOWOK()
        {
            melsecFx.Write("M4312", true);
        }
        public void CH2DOWNFLOWNG()
        {
            ch2downflow = new Thread(WriteCH2DOWNFLOWNG);
            ch2downflow.IsBackground = true;
            ch2downflow.Start();
        }
        public void WriteCH2DOWNFLOWNG()
        {
            melsecFx.Write("M4313", true);
        }
        Thread ch2downflowend;
        /// <summary>
        /// CH2下充流量测试完成
        /// </summary>
        public void CH2DOWNFlowEnd()
        {
            ch2downflowend = new Thread(WriteCH2DOWNFlowEnd);
            ch2downflowend.IsBackground = true;
            ch2downflowend.Start();
        }
        public void WriteCH2DOWNFlowEnd()
        {
            melsecFx.Write("M4311", true);
        }
        Thread ch2fwdstart;
        /// <summary>
        /// CH2同充开始
        /// </summary>
        public void CH2FWDStart()
        {
            ch2fwdstart = new Thread(WriteCH2FWDStart);
            ch2fwdstart.IsBackground = true;
            ch2fwdstart.Start();
        }
        public void WriteCH2FWDStart()
        {
            melsecFx.Write("M2034", true);
        }
        Thread ch2fwdadc;
        /// <summary>
        /// CH2同充电流结果
        /// </summary>
        public void CH2FWDADCOK()
        {
            ch2fwdadc = new Thread(WriteCH2FWDADCOK);
            ch2fwdadc.IsBackground = true;
            ch2fwdadc.Start();
        }
        public void WriteCH2FWDADCOK()
        {
            melsecFx.Write("M4408", true);
        }
        public void CH2FWDADCNG()
        {
            ch2fwdadc = new Thread(WriteCH2FWDADCNG);
            ch2fwdadc.IsBackground = true;
            ch2fwdadc.Start();
        }
        public void WriteCH2FWDADCNG()
        {
            melsecFx.Write("M4409", true);
        }
        Thread ch2fwdvdc;
        /// <summary>
        /// CH2同充电压结果
        /// </summary>
        public void CH2FWDVDCOK()
        {
            ch2fwdvdc = new Thread(WriteCH2FWDVDCOK);
            ch2fwdvdc.IsBackground = true;
            ch2fwdvdc.Start();
        }
        public void WriteCH2FWDVDCOK()
        {
            melsecFx.Write("M4410", true);
        }
        public void CH2FWDVDCNG()
        {
            ch2fwdvdc = new Thread(WriteCH2FWDVDCNG);
            ch2fwdvdc.IsBackground = true;
            ch2fwdvdc.Start();
        }
        public void WriteCH2FWDVDCNG()
        {
            melsecFx.Write("M4411", true);
        }
        Thread ch2fwdflowend;
        /// <summary>
        /// CH2同充流量测试完成
        /// </summary>
        public void CH2FWDFlowEnd()
        {
            ch2fwdflowend = new Thread(WriteCH2FWDFlowEnd);
            ch2fwdflowend.IsBackground = true;
            ch2fwdflowend.Start();
        }
        public void WriteCH2FWDFlowEnd()
        {
            melsecFx.Write("M4416", true);
        }
        Thread ch2rwdstart;
        /// <summary>
        /// CH2泄气开始
        /// </summary>
        public void CH2RWDStart()
        {
            ch2rwdstart = new Thread(WriteCH2RWDStart);
            ch2rwdstart.IsBackground = true;
            ch2rwdstart.Start();
        }
        public void WriteCH2RWDStart()
        {
            melsecFx.Write("M2035", true);
        }
        Thread ch2rwdadc;
        /// <summary>
        /// CH2泄气电流结果
        /// </summary>
        public void CH2RWDADCOK()
        {
            ch2rwdadc = new Thread(WriteCH2RWDADCOK);
            ch2rwdadc.IsBackground = true;
            ch2rwdadc.Start();
        }
        public void WriteCH2RWDADCOK()
        {
            melsecFx.Write("M4508", true);
        }
        public void CH2RWDADCNG()
        {
            ch2rwdadc = new Thread(WriteCH2RWDADCNG);
            ch2rwdadc.IsBackground = true;
            ch2rwdadc.Start();
        }
        public void WriteCH2RWDADCNG()
        {
            melsecFx.Write("M4509", true);
        }
        Thread ch2rwdvdc;
        /// <summary>
        /// CH2泄气电压结果
        /// </summary>
        public void CH2RWDVDCOK()
        {
            ch2rwdvdc = new Thread(WriteCH2RWDVDCOK);
            ch2rwdvdc.IsBackground = true;
            ch2rwdvdc.Start();
        }
        public void WriteCH2RWDVDCOK()
        {
            melsecFx.Write("M4510", true);
        }
        public void CH2RWDVDCNG()
        {
            ch2rwdvdc = new Thread(WriteCH2RWDVDCNG);
            ch2rwdvdc.IsBackground = true;
            ch2rwdvdc.Start();
        }
        public void WriteCH2RWDVDCNG()
        {
            melsecFx.Write("M4511", true);
        }
        Thread ch2rwdflowend;
        /// <summary>
        /// CH2泄气压力测试完成
        /// </summary>
        public void CH2RWDFlowEnd()
        {
            ch2rwdflowend = new Thread(WriteCH2RWDFlowEnd);
            ch2rwdflowend.IsBackground = true;
            ch2rwdflowend.Start();
        }
        public void WriteCH2RWDFlowEnd()
        {
            melsecFx.Write("M4516", true);
        }
        Thread ch2rwdpressok;
        /// <summary>
        /// CH2泄气压力测试OK
        /// </summary>
        public void CH2RWDPressOK()
        {
            ch2rwdpressok = new Thread(WriteCH2RWDPressOK);
            ch2rwdpressok.IsBackground = true;
            ch2rwdpressok.Start();
        }
        public void WriteCH2RWDPressOK()
        {
            melsecFx.Write("M4512", true);
        }
        Thread ch2rwdpressng;
        /// <summary>
        /// CH2泄气压力测试NG
        /// </summary>
        public void CH2RWDPressNG()
        {
            ch2rwdpressng = new Thread(WriteCH2RWDPressNG);
            ch2rwdpressng.IsBackground = true;
            ch2rwdpressng.Start();
        }
        public void WriteCH2RWDPressNG()
        {
            melsecFx.Write("M4513", true);
        }
        Thread ch1pump;
        /// <summary>
        /// CH1阀泵供气
        /// </summary>
        public void CH1Pump()
        {
            ch1pump = new Thread(WriteCH1Pump);
            ch1pump.IsBackground = true;
            ch1pump.Start();
        }
        public void WriteCH1Pump()
        {
            melsecFx.Write("M3250", true);
        }
        /// <summary>
        /// CH1调压阀供气
        /// </summary>
        public void CH1Machine()
        {
            ch1pump = new Thread(WriteCH1Machine);
            ch1pump.IsBackground = true;
            ch1pump.Start();
        }
        public void WriteCH1Machine()
        {
            melsecFx.Write("M3250", false);
        }
        Thread ch2pump;
        /// <summary>
        /// CH2阀泵供气
        /// </summary>
        public void CH2Pump()
        {
            ch2pump = new Thread(WriteCH2Pump);
            ch2pump.IsBackground = true;
            ch2pump.Start();
        }
        public void WriteCH2Pump()
        {
            melsecFx.Write("M3253", true);
        }
        /// <summary>
        /// CH2调压阀供气
        /// </summary>
        public void CH2Machine()
        {
            ch2pump = new Thread(WriteCH2Machine);
            ch2pump.IsBackground = true;
            ch2pump.Start();
        }
        public void WriteCH2Machine()
        {
            melsecFx.Write("M3253", false);
        }
        Thread ch1leak;
        /// <summary>
        /// CH1上充气密启动
        /// </summary>
        public void CH1UPLeak()
        {
            ch1leak = new Thread(WriteCH1UPLeak);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH1UPLeak()
        {
            melsecFx.Write("M4010", true);
        }
        /// <summary>
        /// CH1下充气密启动
        /// </summary>
        public void CH1DOWNLeak()
        {
            ch1leak = new Thread(WriteCH1DOWNLeak);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH1DOWNLeak()
        {
            melsecFx.Write("M4011", true);
        }
        /// <summary>
        /// CH1同充气密启动
        /// </summary>
        public void CH1FWDLeakTrue()
        {
            ch1leak = new Thread(WriteCH1FWDLeakTrue);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH1FWDLeakTrue()
        {
            melsecFx.Write("M4012", true);
        }

        /// <summary>
        /// CH1同充气密关闭
        /// </summary>
        public void CH1FWDLeakFalse()
        {
            ch1leak = new Thread(WriteCH1FWDLeakFalse);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH1FWDLeakFalse()
        {
            melsecFx.Write("M4012", false);
            melsecFx.Write("M2400", false);

        }

        /// <summary>
        /// CH1-1平衡
        /// </summary>
        public void CH1Balance()
        {
            ch1leak = new Thread(WriteCH1Balance);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH1Balance()
        {
            //melsecFx.Write("M3235", true);
            Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
        }
        /// <summary>
        /// CH1-2平衡
        /// </summary>
        public void CH2Balance()
        {
            ch1leak = new Thread(WriteCH2Balance);
            ch1leak.IsBackground = true;
            ch1leak.Start();
        }
        public void WriteCH2Balance()
        {
            //melsecFx.Write("M3236", true);
            if (Form1.f1.CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("OUTP 0");
        }
        Thread ch1udpre;
        /// <summary>
        /// CH1上充的时候下充也要读取压力并判断结果
        /// </summary>
        public void CH1UPPreOK()
        {
            ch1udpre = new Thread(WriteCH1UPPreOK);
            ch1udpre.IsBackground = true;
            ch1udpre.Start();
        }
        public void WriteCH1UPPreOK()
        {
            melsecFx.Write("M2214", true);
        }
        /// <summary>
        /// CH1上充的时候下充也要读取压力并判断结果
        /// </summary>
        public void CH1UPPreNG()
        {
            ch1udpre = new Thread(WriteCH1UPPreNG);
            ch1udpre.IsBackground = true;
            ch1udpre.Start();
        }
        public void WriteCH1UPPreNG()
        {
            melsecFx.Write("M2215", true);
        }
        /// <summary>
        /// CH1下充的时候上充也要读取压力并判断结果
        /// </summary>
        public void CH1DOWNPreOK()
        {
            ch1udpre = new Thread(WriteCH1DOWNPreOK);
            ch1udpre.IsBackground = true;
            ch1udpre.Start();
        }
        public void WriteCH1DOWNPreOK()
        {
            melsecFx.Write("M2314", true);
        }
        /// <summary>
        /// CH1下充的时候上充也要读取压力并判断结果
        /// </summary>
        public void CH1DOWNPreNG()
        {
            ch1udpre = new Thread(WriteCH1DOWNPreNG);
            ch1udpre.IsBackground = true;
            ch1udpre.Start();
        }
        public void WriteCH1DOWNPreNG()
        {
            melsecFx.Write("M2315", true);
        }
        Thread ch2leak;
        /// <summary>
        /// CH2上充气密启动
        /// </summary>
        public void CH2UPLeakTrue()
        {
            ch2leak = new Thread(WriteCH2UPLeakTrue);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2UPLeakTrue()
        {
            melsecFx.Write("M4013", true);
        }

        public void CH2UPLeakFalse()
        {
            ch2leak = new Thread(WriteCH2UPLeakFalse);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2UPLeakFalse()
        {
            melsecFx.Write("M4013", false);
            melsecFx.Write("M4400", false);
        }

        public void CH2DownLeakFalse()
        {
            ch2leak = new Thread(WriteCH2DownLeakFalse);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2DownLeakFalse()
        {
            melsecFx.Write("M4014", false);
        }

        public void CH1DownLeakFalse()
        {
            ch2leak = new Thread(WriteCH1DownLeakFalse);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH1DownLeakFalse()
        {
            melsecFx.Write("M4011", false);
        }

        public void CH1LeakFalse()
        {
            ch2leak = new Thread(WriteCH1LeakFalse);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH1LeakFalse()
        {
            melsecFx.Write("M4010", false);
        }

        /// <summary>
        /// CH2下充气密启动
        /// </summary>
        public void CH2DOWNLeak()
        {
            ch2leak = new Thread(WriteCH2DOWNLeak);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2DOWNLeak()
        {
            melsecFx.Write("M4014", true);
        }
        /// <summary>
        /// CH2同充气密启动
        /// </summary>
        public void CH2FWDLeakTrue()
        {
            ch2leak = new Thread(WriteCH2FWDLeakTrue);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2FWDLeakTrue()
        {
            melsecFx.Write("M4015", true);
        }

        /// <summary>
        /// CH2同充气密关闭
        /// </summary>
        public void CH2FWDLeakFalse()
        {
            ch2leak = new Thread(WriteCH2FWDLeakFalse);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH2FWDLeakFalse()
        {
            melsecFx.Write("M4015", false);
        }
        /// <summary>
        /// CH2-1平衡
        /// </summary>
        public void CH3Balance()
        {
            ch2leak = new Thread(WriteCH3Balance);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH3Balance()
        {
            melsecFx.Write("M3237", true);
        }
        /// <summary>
        /// CH2-2平衡
        /// </summary>
        public void CH4Balance()
        {
            ch2leak = new Thread(WriteCH4Balance);
            ch2leak.IsBackground = true;
            ch2leak.Start();
        }
        public void WriteCH4Balance()
        {
            melsecFx.Write("M3238", true);
        }
        Thread ch2udpre;
        /// <summary>
        /// CH2上充的时候下充也要读取压力并判断结果
        /// </summary>
        public void CH2UPPreOK()
        {
            ch2udpre = new Thread(WriteCH2UPPreOK);
            ch2udpre.IsBackground = true;
            ch2udpre.Start();
        }
        public void WriteCH2UPPreOK()
        {
            melsecFx.Write("M4214", true);
        }
        /// <summary>
        /// CH2上充的时候下充也要读取压力并判断结果
        /// </summary>
        public void CH2UPPreNG()
        {
            ch2udpre = new Thread(WriteCH2UPPreNG);
            ch2udpre.IsBackground = true;
            ch2udpre.Start();
        }
        public void WriteCH2UPPreNG()
        {
            melsecFx.Write("M4215", true);
        }
        /// <summary>
        /// CH2下充的时候上充也要读取压力并判断结果
        /// </summary>
        public void CH2DOWNPreOK()
        {
            ch2udpre = new Thread(WriteCH2DOWNPreOK);
            ch2udpre.IsBackground = true;
            ch2udpre.Start();
        }
        public void WriteCH2DOWNPreOK()
        {
            melsecFx.Write("M4314", true);
        }
        /// <summary>
        /// CH2下充的时候上充也要读取压力并判断结果
        /// </summary>
        public void CH2DOWNPreNG()
        {
            ch2udpre = new Thread(WriteCH2DOWNPreNG);
            ch2udpre.IsBackground = true;
            ch2udpre.Start();
        }
        public void WriteCH2DOWNPreNG()
        {
            melsecFx.Write("M4315", true);
        }

        /// <summary>
        /// CH1上充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH1SC(bool mBool)
        {
            melsecFx.Write("M5100", mBool);
        }

        /// <summary>
        /// CH1下充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH1XC(bool mBool)
        {
            melsecFx.Write("M5101", mBool);
        }

        /// <summary>
        /// CH1同充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH1TC(bool mBool)
        {
            melsecFx.Write("M5102", mBool);
        }

        /// <summary>
        /// CH1泄气ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH1XQ(bool mBool)
        {
            melsecFx.Write("M5103", mBool);
        }


        /// <summary>
        /// CH2上充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH2SC(bool mBool)
        {
            melsecFx.Write("M5104", mBool);
        }

        /// <summary>
        /// CH2下充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH2XC(bool mBool)
        {
            melsecFx.Write("M5105", mBool);
        }

        /// <summary>
        /// CH2同充ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH2TC(bool mBool)
        {
            melsecFx.Write("M5106", mBool);
        }

        /// <summary>
        /// CH2泄气ON/OFF
        /// </summary>
        /// <param name="mBool"></param>
        public void WriteCH2XQ(bool mBool)
        {
            melsecFx.Write("M5107", mBool);
        }



        //Thread airvalve;
        ///// <summary>
        ///// CH1气源切换阀打开
        ///// </summary>
        //public void CH1AirValveOpen()
        //{
        //    airvalve = new Thread(WriteCH1AirValveOpen);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH1AirValveOpen()
        //{
        //    melsecFx.Write("M3200", true);
        //}
        //public void CH1AirValveClose()
        //{
        //    airvalve = new Thread(WriteCH1AirValveClose);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH1AirValveClose()
        //{
        //    melsecFx.Write("M3200", false);
        //}
        ///// <summary>
        ///// CH2气源切换阀打开
        ///// </summary>
        //public void CH2AirValveOpen()
        //{
        //    airvalve = new Thread(WriteCH2AirValveOpen);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH2AirValveOpen()
        //{
        //    melsecFx.Write("M3201", true);
        //}
        //public void CH2AirValveClose()
        //{
        //    airvalve = new Thread(WriteCH2AirValveClose);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH2AirValveClose()
        //{
        //    melsecFx.Write("M3201", false);
        //}
        ///// <summary>
        ///// CH3气源切换阀打开
        ///// </summary>
        //public void CH3AirValveOpen()
        //{
        //    airvalve = new Thread(WriteCH3AirValveOpen);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH3AirValveOpen()
        //{
        //    melsecFx.Write("M3202", true);
        //}
        //public void CH3AirValveClose()
        //{
        //    airvalve = new Thread(WriteCH3AirValveClose);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH3AirValveClose()
        //{
        //    melsecFx.Write("M3202", false);
        //}
        ///// <summary>
        ///// CH4气源切换阀打开
        ///// </summary>
        //public void CH4AirValveOpen()
        //{
        //    airvalve = new Thread(WriteCH4AirValveOpen);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH4AirValveOpen()
        //{
        //    melsecFx.Write("M3203", true);
        //}
        //public void CH4AirValveClose()
        //{
        //    airvalve = new Thread(WriteCH4AirValveClose);
        //    airvalve.IsBackground = true;
        //    airvalve.Start();
        //}
        //public void WriteCH4AirValveClose()
        //{
        //    melsecFx.Write("M3203", false);
        //}
        //Thread ch1rwdend;
        ///// <summary>
        ///// CH1泄气测试完成
        ///// </summary>
        //public void CH1RWDEnd()
        //{
        //    ch1rwdend = new Thread(WriteCH1RWDEnd);
        //    ch1rwdend.IsBackground = true;
        //    ch1rwdend.Start();
        //}
        //public void WriteCH1RWDEnd()
        //{
        //    melsecFx.Write("M2501", true);
        //}
        //Thread coudecount;
        //public int LeftCodeCount;
        //public int RightCodeCount;
        ///// <summary>
        ///// 循环查看各个信号的实时状态
        ///// </summary>
        //public void PLC_IsRun()
        //{
        //    plc_signal = new Thread(ReadSignal);
        //    plc_signal.IsBackground = true;
        //    plc_signal.Start();
        //}

        //public void ReadSignal()
        //{
        //    //用前侧安全门的地址来做判断是否有成功通讯的信号位
        //    OperateResult<bool> R_X = melsecFx.ReadBool("M2100");
        //    PLCIsRun = R_X.IsSuccess;

        //    if (PLCIsRun)
        //    {
        //        Front_SafetyDoor = melsecFx.ReadBool("M2100").Content;
        //        Back_SafetyDoorUp = melsecFx.ReadBool("M2101").Content;
        //    }
        //}

        public void CH1TCNG()
        {
            melsecFx.Write("M2209", true);
        }
        public void CH2TCNG()
        {
            melsecFx.Write("M4415", true);
        }

        public void CH2XQWC()
        {
            melsecFx.Write("M4414", true);
        }

        public void CH1Rset()
        {
            melsecFx.Write("M3300", false);
            melsecFx.Write("M510", false);
            melsecFx.Write("M511", false);
            melsecFx.Write("M512", false);
        }
        public void CH2Rset()
        {
            melsecFx.Write("M3301", false);
            melsecFx.Write("M513", false);
            melsecFx.Write("M514", false);
            melsecFx.Write("M515", false);
        }
    }
}
