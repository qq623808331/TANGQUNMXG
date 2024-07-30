using System;
using System.Net;

namespace NetWorkHelper
{
    public delegate void ReceiveDataEventHandler(object sender, ReceiveDataEventArgs e);

    public class ReceiveDataEventArgs : EventArgs
    {
        public ReceiveDataEventArgs() { }

        public ReceiveDataEventArgs(byte[] buffer, IPEndPoint remoteIP)
            : base()
        {
            Buffer = buffer;
            RemoteIP = remoteIP;
        }

        public byte[] Buffer { get; set; }

        public IPEndPoint RemoteIP { get; set; }
    }
}
