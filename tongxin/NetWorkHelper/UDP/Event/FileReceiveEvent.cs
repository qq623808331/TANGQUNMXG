using System;

namespace NetWorkHelper
{
    public delegate void FileReceiveEventHandler(object sender, FileReceiveEventArgs e);

    public class FileReceiveEventArgs : EventArgs
    {
        public FileReceiveEventArgs() { }

        public FileReceiveEventArgs(ReceiveFileManager receiveFileManager) : base()
        {
            ReceiveFileManager = receiveFileManager;
        }

        public ReceiveFileManager ReceiveFileManager { get; }

        public object Tag { get; set; }
    }
}
