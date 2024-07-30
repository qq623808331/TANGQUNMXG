using System;

namespace NetWorkHelper
{
    public delegate void FileReceiveCompleteEventHandler(object sender, FileReceiveCompleteEventArgs e);

    public class FileReceiveCompleteEventArgs : EventArgs
    {
        public FileReceiveCompleteEventArgs() { }

        public FileReceiveCompleteEventArgs(bool success)
            : base()
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}
