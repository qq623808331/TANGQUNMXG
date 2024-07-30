using System.ComponentModel;
using System.Net;

namespace NetWorkHelper
{
    public delegate void RequestSendFileEventHandler(object sender, RequestSendFileEventArgs e);

    public class RequestSendFileEventArgs : CancelEventArgs
    {
        public RequestSendFileEventArgs()
            : base()
        {
        }

        public RequestSendFileEventArgs(TraFransfersFileStart traFransfersFileStart, IPEndPoint remoteIP)
            : base()
        {
            TraFransfersFileStart = traFransfersFileStart;
            RemoteIP = remoteIP;
        }

        public RequestSendFileEventArgs(TraFransfersFileStart traFransfersFileStart, IPEndPoint remoteIP, bool cancel)
            : base(cancel)
        {
            TraFransfersFileStart = traFransfersFileStart;
            RemoteIP = remoteIP;
        }

        public IPEndPoint RemoteIP { get; set; }

        public TraFransfersFileStart TraFransfersFileStart { get; set; }

        public string Path { get; set; }
    }
}
