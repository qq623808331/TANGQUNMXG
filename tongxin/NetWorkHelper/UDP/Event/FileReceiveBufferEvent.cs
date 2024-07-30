using System;

namespace NetWorkHelper
{
    public delegate void FileReceiveBufferEventHandler(object sender, FileReceiveBufferEventArgs e);

    public class FileReceiveBufferEventArgs : EventArgs
    {
        public FileReceiveBufferEventArgs(ReceiveFileManager receiveFileManager, int size)
            : base()
        {
            ReceiveFileManager = receiveFileManager;
            Size = size;
        }

        public ReceiveFileManager ReceiveFileManager { get; }

        public int Size { get; }
    }
}
