using System;

namespace NetWorkHelper
{
    public delegate void ReadFileBufferEventHandler(object sender, ReadFileBufferEventArgs e);

    public class ReadFileBufferEventArgs : EventArgs
    {
        public ReadFileBufferEventArgs(int index, byte[] buffer)
            : base()
        {
            Index = index;
            Buffer = buffer;
        }

        public int Index { get; }

        public byte[] Buffer { get; }
    }
}
