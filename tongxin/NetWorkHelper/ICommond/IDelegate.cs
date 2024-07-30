/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：事件委托类，用于事件的申明
 * *
********************************************************************/

using NetWorkHelper.IModels;
using System;

namespace NetWorkHelper.ICommond
{
    public class TcpServerReceviceaEventArgs : EventArgs
    {
        public TcpServerReceviceaEventArgs(IClient iClient, byte[] data)
        {
            IClient = iClient;
            Data = data;
        }
        /// <summary>
        /// 客户端
        /// </summary>
        public IClient IClient { get; set; }
        /// <summary>
        /// 接收到的原始数据
        /// </summary>
        public byte[] Data { get; set; }
    }
    public class TcpServerClientEventArgs : EventArgs
    {
        public TcpServerClientEventArgs(IClient iClient)
        {
            IClient = iClient;
        }
        /// <summary>
        /// 客户端
        /// </summary>
        public IClient IClient { get; set; }
    }
    public class TcpServerStateEventArgs : EventArgs
    {
        public TcpServerStateEventArgs(IClient iClient, string msg, SocketState state)
        {
            IClient = iClient;
            Msg = msg;
            State = state;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public IClient IClient { get; set; }

        /// <summary>
        /// 状态消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        public SocketState State { get; set; }
    }
    public class TcpServerLogEventArgs : EventArgs
    {
        public TcpServerLogEventArgs(IClient iClient, LogType logType, string logMsg)
        {
            IClient = iClient;
            LogType = logType;
            LogMsg = logMsg;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public IClient IClient { get; set; }
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType LogType { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string LogMsg { get; set; }
    }
    public class TcpServerErrorEventArgs : EventArgs
    {
        public TcpServerErrorEventArgs(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
    public class TcpServerSendReturnEventArgs : EventArgs
    {
        public TcpServerSendReturnEventArgs(IClient iClient, int byteLen)
        {
            IClient = iClient;
            ByteLen = byteLen;
        }

        /// <summary>
        /// 客户端
        /// </summary>
        public IClient IClient { get; set; }
        /// <summary>
        /// 成功发送的数据长度
        /// </summary>
        public int ByteLen { get; set; }
    }
    public class TcpServerReturnClientCountEventArgs : EventArgs
    {
        public TcpServerReturnClientCountEventArgs(int clientCount)
        {
            ClientCount = clientCount;
        }

        /// <summary>
        /// 客户端数量
        /// </summary>
        public int ClientCount { get; set; }
    }

    public class TcpClientReceviceEventArgs : EventArgs
    {
        public TcpClientReceviceEventArgs(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// 接收到的原始数据
        /// </summary>
        public byte[] Data { get; set; }
    }
    public class TcpClientErrorEventArgs : EventArgs
    {
        public TcpClientErrorEventArgs(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
    public class TcpClientStateEventArgs : EventArgs
    {
        public TcpClientStateEventArgs(string stateInfo, SocketState state)
        {
            StateInfo = stateInfo;
            State = state;
        }

        /// <summary>
        /// 状态爱信息
        /// </summary>
        public string StateInfo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public SocketState State { get; set; }
    }

    public class HttpDownLoadEventArgs : EventArgs
    {
        public HttpDownLoadEventArgs(long totalSize, long curSize, int progress)
        {
            TotalSize = totalSize;
            CurSize = curSize;
            Progress = progress;
        }

        public long TotalSize { get; set; }
        public long CurSize { get; set; }
        public int Progress { get; set; }
    }
    #region 委托

    #region ITcpServer服务端事件委托
    //public delegate void TcpServerReceviceEventHandler(IClient iClient, byte[] data);
    //public delegate void TcpServerClientEventHandler(IClient iClient);
    //public delegate void TcpServerStateEventHandler(IClient iClient, string msg, SocketState state);
    //public delegate void TcpServerLogEventHandler(IClient iClient, LogType logType, string logMsg);
    //public delegate void TcpServerErrorEventHandler(string errorMsg);
    //public delegate void TcpServerSendReturnEventHandler(IClient iClient, int byteLen);
    //public delegate void TcpServerReturnClientCountEventHandler(int clientCount); 

    #endregion

    #region ITcpClient客户端事件委托
    //public delegate void TcpClientReceviceEventHandler(byte[] data);
    //public delegate void TcpClientErrorEventHandler(string errorMsg);
    //public delegate void TcpClientStateEventHandler(string msg, SocketState state); 
    #endregion

    #region 通用类的委托（可能到时双击注册事件时改变参数类型，谨慎使用）
    /// <summary>
    /// 不带参数的委托
    /// </summary>
    public delegate void NetWorkEventHandler();


    /// <summary>
    /// 带一个参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="object1"></param>
    public delegate void NetWorkEventHandler<T1>(T1 object1);

    /// <summary>
    /// 带两个参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    public delegate void NetWorkEventHandler<T1, T2>(T1 object1, T2 object2);

    /// <summary>
    /// 带三个参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    /// <param name="object3"></param>
    public delegate void NetWorkEventHandler<T1, T2, T3>(T1 object1, T2 object2, T3 object3);
    /// <summary>
    /// 带四个参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    /// <param name="object3"></param>
    /// <param name="object4"></param>
    public delegate void NetWorkEventHandler<T1, T2, T3, T4>(T1 object1, T2 object2, T3 object3, T4 object4);
    /// <summary>
    /// 带五个参数的委托
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <typeparam name="T4"></typeparam>
    /// <typeparam name="T5"></typeparam>
    /// <param name="object1"></param>
    /// <param name="object2"></param>
    /// <param name="object3"></param>
    /// <param name="object4"></param>
    /// <param name="object5"></param>
    public delegate void NetWorkEventHandler<T1, T2, T3, T4, T5>(T1 object1, T2 object2, T3 object3, T4 object4, T5 object5);
    public delegate void NetWorkEventHandler<T1, T2, T3, T4, T5, T6>(T1 object1, T2 object2, T3 object3, T4 object4, T5 object5, T6 object6);
    public delegate void NetWorkEventHandler<T1, T2, T3, T4, T5, T6, T7>(T1 object1, T2 object2, T3 object3, T4 object4, T5 object5, T6 object6, T7 object7);
    public delegate void NetWorkEventHandler<T1, T2, T3, T4, T5, T6, T7, T8>(T1 object1, T2 object2, T3 object3, T4 object4, T5 object5, T6 object6, T7 object7, T8 object8);
    public delegate void NetWorkEventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 object1, T2 object2, T3 object3, T4 object4, T5 object5, T6 object6, T7 object7, T8 object8, T9 object9);
    #endregion

    #endregion


}
