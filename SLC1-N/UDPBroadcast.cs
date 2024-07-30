using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SLC1_N
{
    class UDPBroadcast
    {
        //UDP广播获取仪器IP地址
        private static Socket sock;
        private static IPEndPoint iep1;
        private static byte[] data;
        Thread UDPlisten;

        public string[] ch_ipaddress;
        /// <summary>
        /// UDP广播获取仪器IP地址
        /// </summary>
        public void UDP_Broadcast()
        {
            ch_ipaddress = new string[3];
            string PrefixIP = new GetIP().GetLocalIP();
            int IP_index = PrefixIP.LastIndexOf(".");
            PrefixIP = PrefixIP.Remove(IP_index + 1);
            PrefixIP += "255";

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //255.255.255.255
            //iep1 = new IPEndPoint(IPAddress.Broadcast, 9999);
            IPAddress ip;
            ip = IPAddress.Parse(PrefixIP);
            iep1 = new IPEndPoint(ip, 9999);

            string hostname = Dns.GetHostName();
            data = Encoding.ASCII.GetBytes("hello,udp server");

            sock.SetSocketOption(SocketOptionLevel.Socket,
            SocketOptionName.Broadcast, 1);

            UDPlisten = new Thread(UDP_Listening);
            UDPlisten.Start();
            sock.SendTo(data, iep1);

        }
        public void UDP_Listening()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = sock.Receive(data);
                string RecvData = Encoding.ASCII.GetString(data, 0, recv);
                string[] IPData = RecvData.Split('\n');
                int ip_num = IPData.Length;
                if (ip_num > 0)
                {
                    int i;
                    for (i = 0; i < ip_num; i++)
                    {
                        string[] ipaddress = IPData[i].Split(':');
                        int ch_station = Convert.ToInt32(ipaddress[0]);
                        switch (ch_station)
                        {
                            case 1:
                                ch_ipaddress[0] = ipaddress[1];
                                break;
                            case 2:
                                ch_ipaddress[1] = ipaddress[1];
                                break;
                            case 3:
                                ch_ipaddress[2] = ipaddress[1];
                                break;
                            case 4:
                                ch_ipaddress[3] = ipaddress[1];
                                break;
                        }
                    }
                }
            }
        }
    }
}
