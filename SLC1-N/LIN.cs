using System;
using System.Text;

namespace SLC1_N
{
    class LIN
    {
        USB_DEVICE.DEVICE_INFO DevInfo = new USB_DEVICE.DEVICE_INFO();
        Int32[] DevHandles = new Int32[20];
        Int32 DevHandle = 0;
        //Byte LINIndex = 1;
        Byte LINIndex;
        bool state;
        Int32 DevNum, ret = 0;
        //Int32 DevIndex;
        Int32 DevBaudrate;
        public LIN(byte index, int baudrate)
        {
            //DevIndex = index;
            LINIndex = index;
            DevBaudrate = baudrate;
        }
        /// <summary>
        /// 扫描查找设备
        /// </summary>
        public void LinComm()
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
            //DevHandle = DevHandles[DevIndex];
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
            //获取固件信息
            StringBuilder FuncStr = new StringBuilder(256);
            state = USB_DEVICE.DEV_GetDeviceInfo(DevHandle, ref DevInfo, FuncStr);
            if (!state)
            {
                Console.WriteLine("Get device infomation error!");
                return;
            }
            else
            {
                Console.WriteLine("Firmware Info:");
                Console.WriteLine("    Name:" + Encoding.Default.GetString(DevInfo.FirmwareName));
                Console.WriteLine("    Build Date:" + Encoding.Default.GetString(DevInfo.BuildDate));
                Console.WriteLine("    Firmware Version:v{0}.{1}.{2}", (DevInfo.FirmwareVersion >> 24) & 0xFF, (DevInfo.FirmwareVersion >> 16) & 0xFF, DevInfo.FirmwareVersion & 0xFFFF);
                Console.WriteLine("    Hardware Version:v{0}.{1}.{2}", (DevInfo.HardwareVersion >> 24) & 0xFF, (DevInfo.HardwareVersion >> 16) & 0xFF, DevInfo.HardwareVersion & 0xFFFF);
                Console.WriteLine("    Functions:" + DevInfo.Functions.ToString("X8"));
                Console.WriteLine("    Functions String:" + FuncStr);
            }
            //初始化配置LIN
            //ret = USB2LIN_EX.LIN_EX_Init(DevHandle, LINIndex, 19200, 1);//初始化为主机
            ret = USB2LIN_EX.LIN_EX_Init(DevHandle, LINIndex, DevBaudrate, 1);//初始化为主机
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Config LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Config LIN Success!");
            }
            /************************************主机写数据************************************/
            ret = USB2LIN_EX.LIN_EX_MasterBreak(DevHandle, LINIndex);//发送Break信号，常用于唤醒设备
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
            ////主机模式发送数据,ID和数据根据实际情况进行修改
            ////byte[] DataBuffer=new byte[8]{0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08};
            //byte[] DataBuffer = new byte[4] { 0x00, 0x00, 0x08, 0x00 };
            ////ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex,(byte)0x02,DataBuffer,(byte)DataBuffer.Length,(byte)1);
            //ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x80, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            //if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            //{
            //    Console.WriteLine("Write LIN failed!");
            //    return;
            //}
            //else
            //{
            //    Console.WriteLine("Write LIN Success!");
            //}
            ////主机模式读数据，ID根据实际情况修改
            ////ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex,(byte)0x03,DataBuffer);
            //ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex, (byte)0xC1, DataBuffer);
            //if (ret < 0)
            //{
            //    Console.WriteLine("Read LIN failed!");
            //}
            //else if (ret == 0)
            //{
            //    Console.WriteLine("The slave machine is not responding!");
            //}
            //else
            //{
            //    Console.Write("Read Data:");
            //    for (int j = 0; j < ret; j++)
            //    {
            //        Console.Write("{0:X2} ", DataBuffer[j]);
            //    }
            //    Console.WriteLine("");
            //}
        }
        /// <summary>
        /// 同充
        /// </summary>
        public void LinFWD()
        {
            //主机模式发送数据,ID和数据根据实际情况进行修改
            //byte[] DataBuffer=new byte[8]{0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08};
            byte[] DataBuffer = new byte[4] { 0x00, 0x00, 0x08, 0x00 };
            //ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex,(byte)0x02,DataBuffer,(byte)DataBuffer.Length,(byte)1);
            ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x80, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
            //主机模式读数据，ID根据实际情况修改
            //ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex,(byte)0x03,DataBuffer);
            ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex, (byte)0xC1, DataBuffer);
            if (ret < 0)
            {
                Console.WriteLine("Read LIN failed!");
            }
            else if (ret == 0)
            {
                Console.WriteLine("The slave machine is not responding!");
            }
            else
            {
                Console.Write("Read Data:");
                for (int j = 0; j < ret; j++)
                {
                    Console.Write("{0:X2} ", DataBuffer[j]);
                }
                Console.WriteLine("");
            }
            return;
        }
        /// <summary>
        /// 上充
        /// </summary>
        public void LinUP()
        {
            //主机模式发送数据,ID和数据根据实际情况进行修改
            //byte[] DataBuffer=new byte[8]{0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08};
            byte[] DataBuffer = new byte[4] { 0x40, 0x00, 0x00, 0x00 };
            //ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex,(byte)0x02,DataBuffer,(byte)DataBuffer.Length,(byte)1);
            ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x80, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
            //主机模式读数据，ID根据实际情况修改
            //ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex,(byte)0x03,DataBuffer);
            ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex, (byte)0xC1, DataBuffer);
            if (ret < 0)
            {
                Console.WriteLine("Read LIN failed!");
            }
            else if (ret == 0)
            {
                Console.WriteLine("The slave machine is not responding!");
            }
            else
            {
                Console.Write("Read Data:");
                for (int j = 0; j < ret; j++)
                {
                    Console.Write("{0:X2} ", DataBuffer[j]);
                }
                Console.WriteLine("");
            }
            return;
        }
        /// <summary>
        /// 下充
        /// </summary>
        public void LinDOWN()
        {
            //主机模式发送数据,ID和数据根据实际情况进行修改
            //byte[] DataBuffer=new byte[8]{0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08};
            byte[] DataBuffer = new byte[4] { 0x80, 0x00, 0x00, 0x00 };
            //ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex,(byte)0x02,DataBuffer,(byte)DataBuffer.Length,(byte)1);
            ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x80, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
            //主机模式读数据，ID根据实际情况修改
            //ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex,(byte)0x03,DataBuffer);
            ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex, (byte)0xC1, DataBuffer);
            if (ret < 0)
            {
                Console.WriteLine("Read LIN failed!");
            }
            else if (ret == 0)
            {
                Console.WriteLine("The slave machine is not responding!");
            }
            else
            {
                Console.Write("Read Data:");
                for (int j = 0; j < ret; j++)
                {
                    Console.Write("{0:X2} ", DataBuffer[j]);
                }
                Console.WriteLine("");
            }
            return;
        }
        /// <summary>
        /// 泄气
        /// </summary>
        public void LinRWD()
        {
            //主机模式发送数据,ID和数据根据实际情况进行修改
            //byte[] DataBuffer=new byte[8]{0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08};
            byte[] DataBuffer = new byte[4] { 0x20, 0x00, 0x00, 0x00 };
            //ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex,(byte)0x02,DataBuffer,(byte)DataBuffer.Length,(byte)1);
            ret = USB2LIN_EX.LIN_EX_MasterWrite(DevHandle, LINIndex, (byte)0x80, DataBuffer, (byte)DataBuffer.Length, (byte)1);
            if (ret != USB2LIN_EX.LIN_EX_SUCCESS)
            {
                Console.WriteLine("Write LIN failed!");
                return;
            }
            else
            {
                Console.WriteLine("Write LIN Success!");
            }
            //主机模式读数据，ID根据实际情况修改
            //ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex,(byte)0x03,DataBuffer);
            ret = USB2LIN_EX.LIN_EX_MasterRead(DevHandle, LINIndex, (byte)0xC1, DataBuffer);
            if (ret < 0)
            {
                Console.WriteLine("Read LIN failed!");
            }
            else if (ret == 0)
            {
                Console.WriteLine("The slave machine is not responding!");
            }
            else
            {
                Console.Write("Read Data:");
                for (int j = 0; j < ret; j++)
                {
                    Console.Write("{0:X2} ", DataBuffer[j]);
                }
                Console.WriteLine("");
            }
            return;
        }
    }
}
