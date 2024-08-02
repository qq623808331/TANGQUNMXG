using System;
using System.Text;
using System.Threading;
using USB2XXX;

namespace SLC1_N
{
    public class LIN_LDFParser
    {
        Int32[] DevHandles = new Int32[20];
        Int32 DevHandle = 0;
        //Byte LINIndex = 1;
        Byte LINIndex;
        bool state;
        Int32 DevNum;
        UInt64 LDFHandle;
        byte[] pFrameName;
        public LIN_LDFParser(int CH)
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
        Thread Thread_LINCONTER;
        public int LINCONTER;
        public void MainThread()
        {
            Thread_LINCONTER = new Thread(maintr);
            Thread_LINCONTER.IsBackground = true;
            Thread_LINCONTER.Start();
        }
        public void maintr()
        {
            Form1.f1.CH1lin.Main(Form1.f1.linconfig.LDFFileName);
            Form1.f1.CH2lin.Main(Form1.f1.linconfig.LDFFileName);
        }

        public Int16 Main(string LDFFileName)
        {
            try
            {
                //扫描查找设备
                DevNum = USB_DEVICE.USB_ScanDevice(DevHandles);
                if (DevNum <= 0)
                {
                    Console.WriteLine("No device connected!");
                    return 1;
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
                    return 1;
                }
                else
                {
                    Console.WriteLine("Open device success!");
                }
                state = USB_DEVICE.DEV_SetPowerLevel(DevHandle, 0);
                if (!state)
                {
                    Console.WriteLine("Open device error!");
                    return 1;
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
                    return 1;
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


                return 0;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }

        }
        /// <summary>
        /// 上充LDF
        /// </summary>
        public void LINUP(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            Int32 bbb;
            string sky;
            bbb = 0;
            try
            {
                if (LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1) == 0)
                {
                    if (LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1) == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1) == 0)
                        {


                        }
                    }
                }

            }
            catch (Exception ex) { };


        }



        /// <summary>
        /// 下充
        /// </summary>
        public void LINDOWN(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            Int32 bbb;
            string sky;
            bbb = 0;
            try
            {
                if (LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1) == 0)
                {
                    if (LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1) == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1) == 0)
                        {


                        }
                    }
                }

            }
            catch (Exception ex) { };

        }



        /// <summary>
        /// 同充
        /// </summary> 
        public void LINFWD(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            Int32 bbb;
            string sky;
            bbb = 0;
            try
            {
                if (LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1) == 0)
                {
                    if (LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1) == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1) == 0)
                        {


                        }
                    }
                }

            }
            catch (Exception ex) { };

        }



        /// <summary>
        /// 泄气
        /// </summary>
        public void LINRWD(string OSignalName, double PowerValue, string SignalName, string SchName)
        {
            //Main();
            try
            {
                if (LDFParser.LDF_SetSignalValue(LDFHandle, pFrameName, Encoding.ASCII.GetBytes(SignalName), 1) == 0)
                {
                    if (LDFParser.LDF_ExeFrameToBus(LDFHandle, pFrameName, 1) == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        if (LDFParser.LDF_ExeSchToBus(LDFHandle, Encoding.ASCII.GetBytes(SchName), 1) == 0)
                        {


                        }
                    }
                }

            }
            catch (Exception ex) { };

        }

        /// <summary>
        /// HEX发送
        /// </summary>
        public void SendHex(string hexStr)
        {
            byte[] DataBuffer = HexStringSToByteArray(hexStr.Trim());
            int ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x00, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
        }
        public byte[] HexStringSToByteArray(string hexValues)
        {

            string[] hexValuesSplit = hexValues.Split(' ');
            byte[] val = new byte[hexValuesSplit.Length];
            int i = 0;
            foreach (string hex in hexValuesSplit)
            {
                int value = HexStringToInt(hex);
                val[i] = (byte)value;
                i++;
            }
            return val;
        }

        /// <summary>
        /// 16进制转byte[]
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public string HexToString(byte[] Str)
        {
            string String = Encoding.Default.GetString(Str);
            byte[] str = new byte[Str.Length / 2];
            for (int i = 0; i < str.Length; i++)
            {
                int temp = Convert.ToInt32(String.Substring(i * 2, 2), 16);
                str[i] = (byte)temp;
            }
            string endstr = "";

            for (int i = 0; i < str.Length; i++)
            {
                endstr += str[i];
            }
            return endstr;
        }
        /// <summary>
        /// 16进制转int
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public int HexStringToInt(string hex)
        {
            int num1 = 0;
            int num2 = 0;
            char[] nums = hex.ToCharArray();
            if (hex.Length == 2)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    string strNum = nums[i].ToString().ToUpper();
                    switch (strNum)
                    {
                        case "A":
                            strNum = "10";
                            break;
                        case "B":
                            strNum = "11";
                            break;
                        case "C":
                            strNum = "12";
                            break;
                        case "D":
                            strNum = "13";
                            break;
                        case "E":
                            strNum = "14";
                            break;
                        case "F":
                            strNum = "15";
                            break;
                        default:

                            break;
                    }
                    if (i == 0)
                    {
                        num1 = int.Parse(strNum) * 16;
                    }
                    if (i == 1)
                    {
                        num2 = int.Parse(strNum);
                    }
                }
            }

            return num1 + num2;
        }


    }
}
