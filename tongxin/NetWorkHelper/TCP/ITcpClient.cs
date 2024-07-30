using NetWorkHelper.ICommond;
using NetWorkHelper.IModels;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetWorkHelper.TCP
{
    public partial class ITcpClient : Component
    {


        //#region 读取ini文件
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        ///// <summary>
        ///// 读出INI文件
        ///// </summary>
        ///// <param name="Section">项目名称(如 [TypeName] )</param>
        ///// <param name="Key">键</param>
        //public string IniReadValue(string Section, string Key)
        //{
        //    StringBuilder temp = new StringBuilder(500);
        //    int i = GetPrivateProfileString(Section, Key, "", temp, 500, System.AppDomain.CurrentDomain.BaseDirectory + "MESConfig.ini");

        //    return temp.ToString();
        //}

        static int connecttimeout = 4000;
        static int receivetimeout = 2000;
        //static int sleeptime;
        static int timeoutsend = 3;
        //public void ReadINI()
        //{
        //    //string dialog;
        //    //dialog = System.AppDomain.CurrentDomain.BaseDirectory + "MESConfig.ini";
        //    //ControlINI mesconfig = new ControlINI(dialog);

        //    //连接设置
        //    connecttimeout = Convert.ToInt32(IniReadValue("MESConfig", "connectTimeout")); ;
        //    receivetimeout = Convert.ToInt32(IniReadValue("MESConfig", "receiveTimeout"));
        //    _reConnectTime = Convert.ToInt32(IniReadValue("MESConfig", "sleepTime"));

        //    timeoutsend = Convert.ToInt32(IniReadValue("MESConfig", "timeoutSend"))+1;



        //}
        //#endregion




        #region 构造函数
        public ITcpClient()
        {
            //ReadINI();
            InitializeComponent();
        }

        public ITcpClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region 变量

        public IClient Client = null;
        private Thread _startThread = null;
        private int _reConnectCount = 0;//重连计数
        private int _maxConnectCount => timeoutsend;
        private ConncetType _conncetType = ConncetType.Conncet;
        private bool _isReconnect = true;//是否开启断开重连
        private int _reConnectTime;//重连间隔时间
        private bool _isStart = false;// 是否启动
        private System.Timers.Timer _timer = new System.Timers.Timer(); // 连接后两秒未成功重连

        //bool timeoutreconnected=true;//连接超时的重连
        //bool datareconnected = true;//接收不到心跳包的重连

        #endregion

        #region 属性
        /// <summary>
        /// 服务端IP
        /// </summary>
        private string _serverip = "127.0.0.1";
        [Description("服务端IP")]
        [Category("TCP客户端")]
        public string ServerIp
        {
            set { _serverip = value; }
            get { return _serverip; }
        }
        /// <summary>
        /// 服务端监听端口
        /// </summary>
        private int _serverport = 5000;
        [Description("服务端监听端口")]
        [Category("TCP客户端")]
        public int ServerPort
        {
            set { _serverport = value; }
            get { return _serverport; }
        }

        /// <summary>
        /// 网络端点
        /// </summary>
        private IPEndPoint _ipEndPoint = null;
        [Description("网络端点,IP+PORT")]
        [Category("TCP客户端")]
        internal IPEndPoint IpEndPoint
        {
            get
            {
                try
                {
                    IPAddress ipAddress = null;
                    ipAddress = string.IsNullOrEmpty(ServerIp)
                        ? IPAddress.Any
                        : IPAddress.Parse(CommonMethod.HostnameToIp(ServerIp));

                    _ipEndPoint = new IPEndPoint(ipAddress, ServerPort);
                }
                catch
                {
                }
                return _ipEndPoint;
            }
        }

        /// <summary>
        /// 是否重连
        /// </summary>
        [Description("是否重连")]
        [Category("TCP客户端")]
        public bool IsReconnection
        {
            set { _isReconnect = value; }
            get { return _isReconnect; }
        }

        /// <summary>
        /// 设置断开重连时间间隔单位（毫秒）（默认3000毫秒）
        /// </summary>
        [Description("设置断开重连时间间隔单位（毫秒）（默认3000毫秒）")]
        [Category("TCP客户端")]
        public int ReConnectionTime
        {
            get { return _reConnectTime; }
            set { _reConnectTime = value; }
        }
        [Description("设置断开重连时间间隔单位（毫秒）（默认3000毫秒）")]
        [Category("TCP客户端"), Browsable(false)]
        public bool IsStart
        {
            get { return _isStart; }
            set { _isStart = value; }
        }
        #endregion

        #region 启动停止方法

        public void StartConnect()
        {
            if (IsStart)
                return;
            if (_startThread == null || !_startThread.IsAlive)
            {
                _startThread = new Thread(StartThread);
                _startThread.IsBackground = true;
                _startThread.Start();
            }
        }

        /// <summary>
        /// 启动客户端基础的一个线程
        /// </summary>
        private void StartThread()
        {
            if (_conncetType == ConncetType.ReConncet && IsReconnection && !IsStart) //如果是重连的延迟N秒
            {
                Thread.Sleep(ReConnectionTime);

                if (IsReconnection)
                {
                    TcpClientStateInfo(string.Format("正在重连..."), SocketState.Reconnection);
                }
                try
                {

                    _timer.Interval = connecttimeout;
                    _timer.Elapsed += Timer_Elapsed;
                    _timer.AutoReset = false;
                    _timer.Start();


                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.ReceiveTimeout = receivetimeout;
                    socket.SendTimeout = 1000;
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
                    socket.BeginConnect(IpEndPoint, new AsyncCallback(AcceptCallback), socket);
                    //timeoutreconnected = true;//连接超时的重连
                    //datareconnected = true;//接收不到心跳包的重连

                }
                catch (Exception ex)
                {
                    _timer.Stop();
                    TcpClientErrorMsg(string.Format("连接服务器失败，错误原因：{0},行号{1}", ex.Message, ex.StackTrace));
                    if (IsReconnection)
                    {
                        Reconnect();
                    }
                }
            }
            else if (!IsStart)
            {
                if (IsReconnection)
                {
                    TcpClientStateInfo("正在连接服务器... ...", SocketState.Connecting);
                    try
                    {

                        _timer.Interval = connecttimeout;
                        _timer.Elapsed += Timer_Elapsed;
                        _timer.AutoReset = false;
                        _timer.Start();


                        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        socket.ReceiveTimeout = receivetimeout;
                        socket.SendTimeout = 1000;
                        socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
                        socket.BeginConnect(IpEndPoint, new AsyncCallback(AcceptCallback), socket);
                        //timeoutreconnected = true;//连接超时的重连
                        //datareconnected = true;//接收不到心跳包的重连

                    }
                    catch (Exception ex)
                    {
                        _timer.Stop();
                        TcpClientErrorMsg(string.Format("连接服务器失败，错误原因：{0},行号{1}", ex.Message, ex.StackTrace));
                        if (IsReconnection)
                        {
                            Reconnect();
                        }
                    }
                }
            }


        }


        //连接超时则重新连接
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsReconnection)
            {
                //datareconnected = false;//接收不到心跳包的重连
                TcpClientErrorMsg(string.Format("连接服务器失败"));

                //if (Client != null)
                //{
                //    ShutdownClient(Client);
                //    Client.WorkSocket.Close();
                //}
                Reconnect();

                //reconnected = false;
            }
        }



        /// <summary>
        /// 当连接服务器之后的回调函数
        /// </summary>
        /// <param name="ar">TcpClient</param>
        private void AcceptCallback(IAsyncResult ar)
        {
            _timer.Stop();

            try
            {
                IsStart = true;

                Socket socket = (Socket)ar.AsyncState;
                socket.EndConnect(ar);

                Client = new IClient(socket);
                Client.WorkSocket.BeginReceive(Client.BufferInfo.ReceivedBuffer, 0, Client.BufferInfo.ReceivedBuffer.Length, 0, new AsyncCallback(ReadCallback), Client);

                _conncetType = ConncetType.Conncet;
                TcpClientStateInfo(string.Format("已连接服务器"), SocketState.Connected);
                _reConnectCount = 0;
                //timeoutreconnected = false;//连接超时的重连
                //datareconnected = false;//接收不到心跳包的重连


            }
            catch (SocketException ex)
            {
                IsStart = false;
                string msg = ex.Message;
                if (ex.NativeErrorCode.Equals(10060))
                {
                    //无法连接目标主机
                    msg = string.Format("{0} 无法连接: error code {1}!", "", ex.NativeErrorCode);
                }
                else if (ex.NativeErrorCode.Equals(10061))
                {
                    msg = string.Format("{0} 主动拒绝正在重连: error code {1}!", "", ex.NativeErrorCode);
                }
                else if (ex.NativeErrorCode.Equals(10053))
                {
                    //读写时主机断开
                    msg = string.Format("{0} 主动断开连接: error code {1}! ", "", ex.NativeErrorCode);
                }
                else
                {
                    //其他错误
                    msg = string.Format("Disconnected: error code {0}!", ex.NativeErrorCode);
                }

                if (IsReconnection)
                {
                    TcpClientErrorMsg(string.Format("连接服务器失败，错误原因：{0}", msg));

                    Reconnect();
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region  登录篇
        /// <summary>
        /// 重连模块
        /// </summary>
        private void Reconnect()
        {
            if (Client != null)
            {
                ShutdownClient(Client);
                Client.WorkSocket.Close();
                Client = null;
            }
            if (_conncetType == ConncetType.Conncet)
            {
                TcpClientStateInfo(string.Format("已断开服务器{0}", IsReconnection ? "，准备重连" : ""), SocketState.Disconnect);
            }
            if (!IsReconnection)
            {
                return;
            }
            _reConnectCount++;//每重连一次重连的次数加1


            if (_conncetType == ConncetType.Conncet)
            {
                _conncetType = ConncetType.ReConncet;

                //CommonMethod.EventInvoket(() => { ReconnectionStart(); });
            }
            _isStart = false;
            if (_reConnectCount < _maxConnectCount && IsReconnection)
            {
                StartConnect();
            }
            else
            {
                _timer.Stop();
                StopConnect();
                this.IsReconnection = false;
                _reConnectCount = 0;
                TcpClientStateInfo(string.Format("超过最大重连数，已断开服务器连接"), SocketState.Disconnect);
            }

        }
        #endregion

        #region 发送数据
        public void SendData(byte[] data)
        {
            try
            {
                if (Client != null && Client.WorkSocket != null)
                {
                    //异步发送数据
                    //cModel.ClientSocket.Send(data);
                    Client.WorkSocket.BeginSend(data, 0, data.Length, 0, new AsyncCallback(SendCallback), Client);
                    //timeoutreconnected = false;//连接超时的重连
                    //datareconnected = true;//接收不到心跳包的重连
                }
            }
            catch (SocketException ex)
            {
                TcpClientErrorMsg(string.Format("向服务端发送数据时发生错误，错误原因：{0},行号{1}", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// 发送完数据之后的回调函数
        /// </summary>
        /// <param name="ar">Clicent</param>
        private void SendCallback(IAsyncResult ar)
        {
            IClient iClient = (IClient)ar.AsyncState;
            if (iClient == null)
                return;
            Socket handler = iClient.WorkSocket;
            try
            {
                int bytesSent = handler.EndSend(ar);
            }
            catch (Exception ex)
            {
                TcpClientErrorMsg(string.Format("发送数据后回调时发生错误，错误原因：{0},行号{1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion

        #region 接收数据
        /// <summary>
        /// 当接收到数据之后的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            if (Client == null || !_isStart)
                return;
            Socket handler = Client.WorkSocket;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0 && bytesRead <= Client.BufferInfo.ReceivedBuffer.Length)
                {
                    byte[] bytes = new byte[bytesRead];
                    Array.Copy(Client.BufferInfo.ReceivedBuffer, 0, bytes, 0, bytesRead);
                    TcpClientRecevice(bytes);
                    handler.BeginReceive(Client.BufferInfo.ReceivedBuffer, 0, Client.BufferInfo.ReceivedBuffer.Length,
                        0, new AsyncCallback(ReadCallback), Client);

                }
                else
                {
                    if (IsReconnection)
                    {
                        Reconnect();
                    }
                }
            }
            catch (Exception ex)
            {
                //if (datareconnected)
                //{
                //    timeoutreconnected = false;//连接超时的重连
                ////IsStart = false;
                StopConnect();

                TcpClientErrorMsg(string.Format("接收数据失败，错误原因：{0},行号{1}", ex.Message, ex.StackTrace));


                Reconnect();

                //}

            }
        }
        #endregion

        #region  断开篇
        /// <summary>
        /// 关闭相连的scoket以及关联的StateObject,释放所有的资源
        /// </summary>
        public void StopConnect()
        {
            IsStart = false;
            if (Client != null)
            {
                ShutdownClient(Client);
                Client.WorkSocket.Close();
            }
            _conncetType = ConncetType.Conncet;
            _reConnectCount = 0;//前面三个初始化
        }
        public void ShutdownClient(IClient iClient)
        {
            try
            {
                iClient.WorkSocket.Shutdown(SocketShutdown.Both);
            }
            catch
            {
            }
        }
        #endregion

        #region 事件
        #region OnRecevice接收数据事件
        [Description("接收数据事件")]
        [Category("TcpClient事件")]
        public event EventHandler<TcpClientReceviceEventArgs> OnRecevice;
        protected virtual void TcpClientRecevice(byte[] data)
        {
            if (OnRecevice != null)
                CommonMethod.EventInvoket(() => { OnRecevice(this, new TcpClientReceviceEventArgs(data)); });

        }
        #endregion

        #region OnErrorMsg返回错误消息事件
        [Description("返回错误消息事件")]
        [Category("TcpClient事件")]
        public event EventHandler<TcpClientErrorEventArgs> OnErrorMsg;
        protected virtual void TcpClientErrorMsg(string msg)
        {
            if (OnErrorMsg != null)
                CommonMethod.EventInvoket(() => { OnErrorMsg(this, new TcpClientErrorEventArgs(msg)); });
        }
        #endregion

        #region OnStateInfo连接状态改变时返回连接状态事件
        [Description("连接状态改变时返回连接状态事件")]
        [Category("TcpClient事件")]
        public event EventHandler<TcpClientStateEventArgs> OnStateInfo;
        protected virtual void TcpClientStateInfo(string msg, SocketState state)
        {
            if (OnStateInfo != null)
                CommonMethod.EventInvoket(() => { OnStateInfo(this, new TcpClientStateEventArgs(msg, state)); });
        }
        #endregion
        #endregion
    }

    public enum ConncetType
    {
        Conncet,
        ReConncet,
        DisConncet
    }
}
