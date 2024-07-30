using System;
using System.Text;
using USB2XXX;

namespace SLC1_N
{
    class CH2LINLDF
    {
        Int32[] DevHandles = new Int32[20];
        Int32 DevHandle = 0;
        //Byte LINIndex = 1;
        Byte LINIndex;
        bool state;
        Int32 DevNum;
        UInt64 LDFHandle;
        byte[] pFrameName;
        public CH2LINLDF(int CH)
        {
            if (CH == 1)
            {
                LINIndex = 0;
            }
            if (CH == 2)
            {
                LINIndex = 1;
            }
        }
        public void Main(string LDFFileName)
        {
            //扫描查找设备
            DevNum = USB_DEVICE.USB_ScanDevice(DevHandles);
            if (DevNum <= 0)
            {
                Console.WriteLine("No device connected!");
                return;
            }
            else
            {
                Console.WriteLine("Have {0} device connected!", DevNum);
            }
            DevHandle = DevHandles[0];
            //打开设备
            state = USB_DEVICE.USB_OpenDevice(DevHandle);
            if (!state)
            {
                Console.WriteLine("Open device error!");
                return;
            }
            else
            {
                Console.WriteLine("Open device success!");
            }
            state = USB_DEVICE.DEV_SetPowerLevel(DevHandle, 0);
            if (!state)
            {
                Console.WriteLine("Open device error!");
                return;
            }
            else
            {
                Console.WriteLine("Open device success!");
            }
            //UInt64 LDFHandle = LDFParser.LDF_ParserFile(DevHandle, LINIndex, 1, Encoding.ASCII.GetBytes("example.ldf"));
            LDFHandle = LDFParser.LDF_ParserFile(DevHandle, LINIndex, 1, Encoding.ASCII.GetBytes(LDFFileName));
            if (LDFHandle == 0)
            {
                Console.WriteLine("解析LDF文件失败!");
                return;
            }
            Console.WriteLine("ProtocolVersion = {0}", LDFParser.LDF_GetProtocolVersion(LDFHandle));
            Console.WriteLine("LINSpeed = {0}", LDFParser.LDF_GetLINSpeed(LDFHandle));
            byte[] MasterName = new byte[64];
            LDFParser.LDF_GetMasterName(LDFHandle, MasterName);
            Console.WriteLine("Master Name = {0}", Encoding.ASCII.GetString(MasterName).TrimEnd('\0'));
            int FrameLen = LDFParser.LDF_GetFrameQuantity(LDFHandle);
            for (int i = 0; i < FrameLen; i++)
            {
                byte[] FrameName = new byte[64];
                if (LDFParser.LDF_PARSER_OK == LDFParser.LDF_GetFrameName(LDFHandle, i, FrameName))
                {
                    byte[] PublisherName = new byte[64];
                    LDFParser.LDF_GetFramePublisher(LDFHandle, FrameName, PublisherName);
                    if (Encoding.ASCII.GetString(MasterName) == Encoding.ASCII.GetString(PublisherName))
                    {
                        //当前帧为主机发送数据帧
                        Console.WriteLine("[MW]Frame[{0}].Name={1},Publisher={2}", i, Encoding.ASCII.GetString(FrameName).TrimEnd('\0'), Encoding.ASCII.GetString(PublisherName).TrimEnd('\0'));
                        if (i == 0)
                        {
                            pFrameName = FrameName;
                        }
                    }
                    else
                    {
                        //当前帧为主机读数据帧
                        Console.WriteLine("[MR]Frame[{0}].Name={1},Publisher={2}", i, Encoding.ASCII.GetString(FrameName).TrimEnd('\0'), Encoding.ASCII.GetString(PublisherName).TrimEnd('\0'));
                    }
                    int SignalNum = LDFParser.LDF_GetFrameSignalQuantity(LDFHandle, FrameName);
                    for (int j = 0; j < SignalNum; j++)
                    {
                        byte[] SignalName = new byte[64];
                        if (LDFParser.LDF_PARSER_OK == LDFParser.LDF_GetFrameSignalName(LDFHandle, FrameName, j, SignalName))
                        {
                            Console.WriteLine("\tSignal[{0}].Name={1}", j, Encoding.ASCII.GetString(SignalName).TrimEnd('\0'));
                        }
                    }
                }
            }
            //主机读操作，读取从机返回的数据值
            //byte[] ValueStr = new byte[64];
            //LDFParser.LDF_ExeFrameToBus(LDFHandle, Encoding.ASCII.GetBytes("ID_DATA"),1);
            //LDFParser.LDF_GetSignalValueStr(LDFHandle, Encoding.ASCII.GetBytes("ID_DATA"), Encoding.ASCII.GetBytes("Supplier_ID"), ValueStr);
            //Console.WriteLine("ID_DATA.Supplier_ID={0}", Encoding.ASCII.GetString(ValueStr).TrimEnd('\0'));
            //LDFParser.LDF_GetSignalValueStr(LDFHandle, Encoding.ASCII.GetBytes("ID_DATA"), Encoding.ASCII.GetBytes("Machine_ID"), ValueStr);
            //Console.WriteLine("ID_DATA.Machine_ID={0}", Encoding.ASCII.GetString(ValueStr).TrimEnd('\0'));
            //LDFParser.LDF_GetSignalValueStr(LDFHandle, Encoding.ASCII.GetBytes("ID_DATA"), Encoding.ASCII.GetBytes("Chip_ID"), ValueStr);
            //Console.WriteLine("ID_DATA.Chip_ID={0}", Encoding.ASCII.GetString(ValueStr).TrimEnd('\0'));
            //主机写操作，发送数据给从机
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Reg_Set_Voltage"), 13.5);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Ramp_Time"), 3);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Cut_Off_Speed"), 4);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Exc_Limitation"), 15.6);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Derat_Shift"), 2);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("MM_Request"), 2);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("LIN_CONTROL"), Encoding.ASCII.GetBytes("Reg_Blind"), 1);
            //LDFParser.LDF_SetSignalValue(LDFHandle, Encoding.ASCII.GetBytes("MSM_LIN2_MBM_BLOSTERSTATUS_Rsp_MSG"), Encoding.ASCII.GetBytes("LmbrUpReq"), 1);
            //LDFParser.LDF_ExeFrameToBus(LDFHandle, Encoding.ASCII.GetBytes("MSM_LIN2_MBM_BLOSTERSTATUS_Rsp_MSG"), 1);
            ////LDFParser.LDF_ExeFrameToBus(LDFHandle, "LIN_CONTROL");
            ////执行调度表
            //LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes("MSM"), 1);
            //LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes("MSM_LIN2_MBM_BLOSTERSTATUS_Rsp_MSG"), 1);
            //LDFParser.LDF_GetSignalValueStr(LDFHandle, Encoding.ASCII.GetBytes("LIN_STATE"), Encoding.ASCII.GetBytes("MM_State"), ValueStr);
            //LDFParser.LDF_GetSignalValueStr(LDFHandle, Encoding.ASCII.GetBytes("MSM_LIN2_MBM_BLOSTERSTATUS_Rsp_MSG"), Encoding.ASCII.GetBytes("LmbrUpReq"), ValueStr);
            //Console.WriteLine("LIN_STATE.MM_State={0}", Encoding.ASCII.GetString(ValueStr).TrimEnd('\0'));
        }
        /// <summary>
        /// 上充
        /// </summary>
        public void LINUP(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            //Main();
            LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1);
            //LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(OSignalName), PowerValue);
            LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1);
            //执行调度表
            LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1);
        }
        /// <summary>
        /// 下充
        /// </summary>
        public void LINDOWN(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            //Main();
            LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1);
            //LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(OSignalName), PowerValue);
            LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1);
            //执行调度表
            LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1);
        }
        /// <summary>
        /// 同充
        /// </summary> 
        public void LINFWD(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            //Main();
            //LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(OSignalName), PowerValue);
            LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1);
            LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1);
            //string pframe = Encoding.ASCII.GetString(pFrameName).TrimEnd('\0');
            //执行调度表
            LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1);
        }
        /// <summary>
        /// 泄气
        /// </summary>
        public void LINRWD(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            //Main();
            LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1);
            //LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(OSignalName), PowerValue);
            LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1);
            //执行调度表
            LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1);
        }
    }
}
