using System;

namespace NetWorkHelper
{
    public delegate void FileSendBufferEventHandler(object sender, FileSendBufferEventArgs e);

    public class FileSendBufferEventArgs : EventArgs
    {
        public FileSendBufferEventArgs(SendFileManager sendFileManager, int size)
            : base()
        {
            SendFileManager = sendFileManager;
            Size = size;
        }

        public SendFileManager SendFileManager { get; }

        public int Size { get; }
    }
}
