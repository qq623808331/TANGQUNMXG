using System;

namespace NetWorkHelper
{
    public delegate void FileSendEventHandler(object sender, FileSendEventArgs e);

    public class FileSendEventArgs : EventArgs
    {
        public FileSendEventArgs(SendFileManager sendFileManager)
            : base()
        {
            SendFileManager = sendFileManager;
        }

        public SendFileManager SendFileManager { get; }
    }
}
