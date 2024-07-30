using NetWorkHelper;
using NetWorkHelper.TCP;
using System;
using System.Collections.Generic;

namespace SLC1_N
{
    public class FrmClient
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        private Log log = new Log();
        private int CH1;

        //int CH2;
        /// <summary>
        /// 连接状态
        /// </summary>
        private SocketState _connectState { get; set; }

        /// <summary>
        /// 连接对象
        /// </summary>
        private ITcpClient _client { get; set; }

        private System.Timers.Timer _timer { get; set; }
        public bool IsConnect = false;

        public FrmClient()
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify -= ChangLang;
            Global.i18n.ChangLangNotify += ChangLang;
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        //System.Timers.Timer _timerPone { get; set; }

        //SocketState _connectState2 { get; set; }
        //ITcpClient _client2 { get; set; }
        //System.Timers.Timer _timer2 { get; set; }
        //public bool CH2IsConnect = false;
        //System.Timers.Timer _timerPone2 { get; set; }

        //将发送数据转为十六进制数据
        private static byte[] StrtoHexbyte(String hexstring)
        {
            int i;
            hexstring = hexstring.Replace(" ", "");
            if ((hexstring.Length % 2) != 0)
            {
                byte[] returnbytes = new byte[(hexstring.Length + 1) / 2];

                for (i = 0; i < (hexstring.Length - 1) / 2; i++)
                {
                    returnbytes[i] = Convert.ToByte(hexstring.Substring(i * 2, 2), 16);
                }
                returnbytes[returnbytes.Length - 1] = Convert.ToByte(hexstring.Substring(hexstring.Length - 1, 1).PadLeft(2, '0'), 16);

                return returnbytes;
            }
            else
            {
                byte[] returnBytes = new byte[(hexstring.Length) / 2];

                for (i = 0; i < returnBytes.Length; i++)
                {
                    returnBytes[i] = Convert.ToByte(hexstring.Substring(i * 2, 2), 16);
                }

                return returnBytes;
            }
        }

        /// <summary>
        /// 计算CRC冗余码
        /// </summary>
        /// <param name="modbusdata"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private int Crc16_Modbus(byte[] modbusdata, int length)
        {
            int i, j;
            int crc = 0xffff;//0xffff or 0
            for (i = 0; i < length; i++)
            {
                crc ^= modbusdata[i] & 0xff;
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x01) == 1)
                    {
                        crc = (crc >> 1) ^ 0xa001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }

            return crc;
        }

        public void FrmClient_Load(int ch1)
        {
            //_timerPone = new System.Timers.Timer();
            Global.i18n.ChangLangNotify -= ChangLang;
            Global.i18n.ChangLangNotify += ChangLang;
            _client = new ITcpClient();
            _client.IsReconnection = true;

            _client.OnStateInfo += Client_OnStateInfo;
            _client.OnRecevice += Client_OnRecevice;
            _client.OnErrorMsg += Client_OnErrorMsg;
            //_timerPone.Elapsed += CloseConnect;
            //_timerPone.Interval = 2000;
            //_timerPone.AutoReset = false;

            //_timerPone2 = new System.Timers.Timer();

            //_client2 = new ITcpClient();
            //_client2.IsReconnection = true;

            //_client2.OnStateInfo += Client2_OnStateInfo;
            //_client2.OnRecevice += Client2_OnRecevice;
            //_client2.OnErrorMsg += Client2_OnErrorMsg;
            CH1 = ch1;
            //CH2 = ch2;
            //_timerPone2.Elapsed += CloseConnect2;
            //_timerPone2.Interval = 2000;
            //_timerPone2.AutoReset = false;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //SendData(Ping);
            //_timerPone.Start();
        }

        private void _timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //SendData2(Ping);
            //_timerPone2.Start();
        }

        /// <summary>
        /// 连接事件
        /// </summary>
        public void btnConnect(string connectinterval, string ipaddress, string port)
        {
            try
            {
                if (!_client.IsStart)
                {
                    _client.IsReconnection = true;
                    int interval = int.Parse(connectinterval);
                    _timer = new System.Timers.Timer();
                    _timer.Interval = 1000 * interval;
                    _timer.Elapsed += _timer_Elapsed;
                    _timer.Start();
                    _client.ServerIp = ipaddress;
                    _client.ServerPort = int.Parse(port);

                    _client.StartConnect();
                    //Form1.f1.MESConnect.Enabled = false;
                    //if (Form1.f1.Account.Text != "操作员")
                    //{
                    //    Form1.f1.MESBreak.Enabled = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                _client.IsReconnection = false;
                _timer.Stop();
                _client.StopConnect();
                _client.Dispose();
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, ex.Message));
            }
        }

        ///// <summary>
        ///// 连接事件2
        ///// </summary>
        //public void btnConnect2(string connectinterval, string ipaddress, string port)
        //{
        //    try
        //    {
        //        if (!_client2.IsStart)
        //        {
        //            _client2.IsReconnection = true;
        //            int interval = int.Parse(connectinterval);

        //            _timer2 = new System.Timers.Timer();
        //            _timer2.Interval = 1000 * interval;
        //            _timer2.Elapsed += _timer2_Elapsed;
        //            _timer2.Start();

        //            _client2.ServerIp = ipaddress;
        //            _client2.ServerPort = int.Parse(port);

        //            _client2.StartConnect();

        //            //Form1.f1.LastMESConnect.Enabled = false;
        //            //if (Form1.f1.Account.Text != "操作员")
        //            //{
        //            //    Form1.f1.LastMESBreak.Enabled = true;
        //            //}

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _client2.IsReconnection = false;
        //        _timer2.Stop();
        //        _client2.StopConnect();
        //        _client2.Dispose();

        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }

        //}

        /// <summary>
        /// 连接错误
        /// </summary>
        private void Client_OnErrorMsg(object sender, NetWorkHelper.ICommond.TcpClientErrorEventArgs e)
        {
            //Form1.f1.textBox1.AppendText(e.ErrorMsg + Environment.NewLine);
            //Form1.f1.messtatic.Text = "未连接";
            //Form1.f1.messtatic.ForeColor = Color.Red;
            IsConnect = false;

            //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //System.IO.File.AppendAllText(@filepath, datetime + "  MES:  " + e.ErrorMsg + Environment.NewLine + "\n");
        }

        ///// <summary>
        ///// 连接错误2
        ///// </summary>
        //private void Client2_OnErrorMsg(object sender, NetWorkHelper.ICommond.TcpClientErrorEventArgs e)
        //{
        //    // Form1.f1.textBox3.AppendText(e.ErrorMsg + Environment.NewLine);
        //    //Form1.f1.lastmesstatic.Text = "未连接";
        //    //Form1.f1.lastmesstatic.ForeColor = Color.Red;
        //    CH2IsConnect = false;

        //    //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    //System.IO.File.AppendAllText(@filepath, datetime + "  LastStation:  " + e.ErrorMsg + Environment.NewLine + "\n");
        //}

        ///// <summary>
        ///// 接收数据转成uft-8字符
        ///// </summary>
        //public static string get_utf8(string unicodeString)
        //{
        //    UTF8Encoding utf8 = new UTF8Encoding();
        //    Byte[] encodedBytes = utf8.GetBytes(unicodeString);
        //    String decodedString = utf8.GetString(encodedBytes);
        //    return decodedString;
        //}

        //UTF8Encoding utf8 = new UTF8Encoding();
        /// <summary>
        /// 接收数据
        /// </summary>
        private void Client_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            //Form1.f1.SendTimeOut.Stop();
            //string receviceData = utf8.GetString(e.Data);

            //Form1.f1.SendTimeOut.Stop();
            //var buff = new byte[1024 * 1024];
            //定义一个1M的内存缓冲区 用于临时性存储接收到的信息
            //var arrRecMsg = new ArraySegment<byte>(buff);
            //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
            //var len = socketClient.Receive(buff);
            var len = e.Data.Length;
            if (len > 0)
            {
                string returnStr = "";

                for (int i = 0; i < len; i++)
                {
                    returnStr += e.Data[i].ToString("X2");//每个字节转换成两位十六进制
                                                          //returnStr += " ";//两个16进制用空格隔开,方便看数据
                }

                var message = returnStr;
                //var message = Encoding.UTF8.GetString(buff, 0, buff.Length);
                string value = message.Replace(" ", "");

                ////心跳包的接收判断
                //if (value.Trim().Contains("000000000004010101") && pingpongreceive)
                //{
                //    Invoke(new Action(() => RetryTimer.Stop()));
                //    pingpongreceive = false;
                //}
                //else
                //{
                //    Invoke(new Action(() => StageNum(value)));
                //}

                log.TCP_Logmsg("Receive:   " + message);
                Form1.f1.TCP_DataReceived(message, CH1);
                //string receviceData = get_utf8(unicodeData);

                //ClientClose();
            }
        }

        ///// <summary>
        ///// 接收数据2,前站数据
        ///// </summary>
        //private void Client2_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        //{
        //    var len = e.Data.Length;
        //    if (len > 0)
        //    {
        //        string returnStr = "";

        //        for (int i = 0; i < len; i++)
        //        {
        //            returnStr += e.Data[i].ToString("X2");//每个字节转换成两位十六进制
        //                                                  //returnStr += " ";//两个16进制用空格隔开,方便看数据
        //        }

        //        var message = returnStr;
        //        //var message = Encoding.UTF8.GetString(buff, 0, buff.Length);
        //        string value = message.Replace(" ", "");

        //        log.TCP_Logmsg("Receive:   " + message);
        //        Form1.f1.TCP_DataReceived(message, CH2);
        //    }
        //    //ClientClose2();
        //}

        /// <summary>
        /// 连接状态
        /// </summary>
        private void Client_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            _connectState = e.State;
            log.TCP_Logmsg(CH1 + "Client:   " + e.StateInfo);
            //Form1.f1.textBox1.AppendText(e.StateInfo + Environment.NewLine);
            //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //System.IO.File.AppendAllText(@filepath, datetime + "  MES:  " + e.StateInfo + Environment.NewLine + "\n");
            if (e.StateInfo.Contains("已断开服务器连接"))
            {
                if (CH1 == 1)
                {
                    Form1.f1.CH1IsRun.Stop();
                }
                else if (CH1 == 3)
                {
                    Form1.f1.CH3IsRun.Stop();
                }
                //_client.IsReconnection = false;
                //_timer.Stop();
                //_client.StopConnect();
                //_client.Dispose();

                //Form1.f1.MESConnect.Enabled = true;
                //Form1.f1.MESBreak.Enabled = false;
                _client.IsReconnection = false;
                _timer.Stop();
                _client.StopConnect();
                _client.Dispose();
                //System.Windows.Forms.MessageBox.Show("无法连接");
                Logger.Log(I18N.GetLangText(dicLang, "无法连接"));
                ClientClose();
                IsConnect = false;
                Form1.f1.TCPIsConnect(IsConnect, CH1);
            }
            else if (e.StateInfo.Contains("已连接服务器"))
            {
                IsConnect = true;
                Form1.f1.TCPIsConnect(IsConnect, CH1);
            }
            else
            {
                if (CH1 == 1)
                {
                    Form1.f1.CH1IsRun.Stop();
                }
                else if (CH1 == 3)
                {
                    Form1.f1.CH3IsRun.Stop();
                }
            }
        }

        ///// <summary>
        ///// 连接状态2
        ///// </summary>
        //private void Client2_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        //{
        //    _connectState2 = e.State;
        //    log.TCP_Logmsg( CH2 +"Client2:   " + e.StateInfo);
        //    //Form1.f1.textBox3.AppendText(e.StateInfo + Environment.NewLine);
        //    //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    //System.IO.File.AppendAllText(@filepath, datetime + "  LastStation:  " + e.StateInfo + Environment.NewLine + "\n");
        //    if (e.StateInfo.Contains("已断开服务器连接"))
        //    {
        //        if(CH2 == 2)
        //        {
        //            Form1.f1.CH2IsRun.Stop();
        //        }else if(CH2 == 4)
        //        {
        //            Form1.f1.CH4IsRun.Stop();
        //        }
        //        _client2.IsReconnection = false;
        //        _timer2.Stop();
        //        _client2.StopConnect();
        //        _client2.Dispose();
        //        System.Windows.Forms.MessageBox.Show("无法连接！");
        //        //_client2.IsReconnection = false;
        //        ClientClose2();
        //        CH2IsConnect = false;
        //        Form1.f1.TCPIsConnect(CH2IsConnect, CH2);
        //    }
        //    else if (e.StateInfo.Contains("已连接"))
        //    {
        //        CH2IsConnect = true;
        //        Form1.f1.TCPIsConnect(CH2IsConnect, CH2);
        //    }
        //    else
        //    {
        //        if (CH2 == 2)
        //        {
        //            Form1.f1.CH2IsRun.Stop();
        //        }
        //        else if (CH2 == 4)
        //        {
        //            Form1.f1.CH4IsRun.Stop();
        //        }
        //    }

        //}

        /// <summary>
        /// 发送数据
        /// </summary>
        public void btnSendData(string txtSend)
        {
            SendData(txtSend);
        }

        ///// <summary>
        ///// 发送数据2
        ///// </summary>
        //public void btnSendData(string txtSend)
        //{
        //    SendData2(txtSend);

        //}

        private void SendData(string sendMsg)
        {
            if (!string.IsNullOrWhiteSpace(sendMsg))
            {
                try
                {
                    byte[] byt = StrtoHexbyte(sendMsg);
                    int str2 = Crc16_Modbus(byt, byt.Length);
                    string str3 = Convert.ToString((str2 >> 8) & 0xff, 16);
                    string str4 = Convert.ToString(str2 & 0xff, 16);

                    if (str3.Length == 1)
                    {
                        str3 = "0" + str3;
                    }
                    if (str4.Length == 1)
                    {
                        str4 = "0" + str4;
                    }
                    sendMsg = sendMsg + str4 + str3;
                    //var message = Encoding.UTF8.GetBytes(sendMsg);
                    var message = StrtoHexbyte(sendMsg);
                    log.TCP_Logmsg("Send:   " + sendMsg);
                    //byte[] sendData = Encoding.Default.GetBytes(message);
                    if (Form1.f1.InvokeRequired)
                    {
                        Form1.f1.Invoke((Action)delegate ()
                        {
                            _client.SendData(message);
                            // txtSendData.Text = string.Empty;
                        });
                    }
                    else
                    {
                        _client.SendData(message);
                        //txtSendData.Text = string.Empty;
                    }
                }
                catch (Exception)
                {
                }
            }

            //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //System.IO.File.AppendAllText(@filepath, datetime + "  MES:Send:  " + data + "\n");
        }

        //void SendData2(string sendMsg)
        //{
        //    if (!string.IsNullOrWhiteSpace(sendMsg))
        //    {
        //        try
        //        {
        //            byte[] byt = StrtoHexbyte(sendMsg);
        //            int str2 = Crc16_Modbus(byt, byt.Length);
        //            string str3 = Convert.ToString((str2 >> 8) & 0xff, 16);
        //            string str4 = Convert.ToString(str2 & 0xff, 16);

        //            if (str3.Length == 1)
        //            {
        //                str3 = "0" + str3;
        //            }
        //            if (str4.Length == 1)
        //            {
        //                str4 = "0" + str4;
        //            }
        //            sendMsg = sendMsg + str4 + str3;
        //            //var message = Encoding.UTF8.GetBytes(sendMsg);
        //            var message = StrtoHexbyte(sendMsg);
        //            log.TCP_Logmsg("Send:   " + sendMsg);
        //            //byte[] sendData = Encoding.Default.GetBytes(message);
        //            if (Form1.f1.InvokeRequired)
        //            {
        //                Form1.f1.Invoke((Action)delegate ()
        //                {
        //                    _client2.SendData(message);
        //                    // txtSendData.Text = string.Empty;
        //                });
        //            }
        //            else
        //            {
        //                _client2.SendData(message);
        //                //txtSendData.Text = string.Empty;
        //            }

        //        }
        //        catch (Exception)
        //        {
        //        }

        //    }
        //    //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    //System.Threading.Thread.Sleep(50);
        //    //System.IO.File.AppendAllText(@filepath, datetime + "  LastStation:Send:  " + data + "\n");
        //}

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void ClientClose()
        {
            _client.IsReconnection = false;
            _timer.Stop();
            _client.StopConnect();
            _client.Dispose();
            IsConnect = false;
            Form1.f1.TCPIsConnect(false, CH1);
            //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //System.IO.File.AppendAllText(@filepath, datetime + "  MES:Close\n");
        }

        ///// <summary>
        ///// 关闭连接2
        ///// </summary>
        //public void ClientClose2()
        //{
        //    _client2.IsReconnection = false;
        //    _timer2.Stop();
        //    _client2.StopConnect();
        //    _client2.Dispose();
        //    CH2IsConnect = false;
        //    Form1.f1.TCPIsConnect(false, CH2);
        //    //string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    //System.IO.File.AppendAllText(@filepath, datetime + "  LastStation:Close\n");
        //}

        //public void CloseConnect2(object body, EventArgs e)
        //{
        //    _client2.StopConnect();
        //    _client2.StartConnect();
        //}
    }
}