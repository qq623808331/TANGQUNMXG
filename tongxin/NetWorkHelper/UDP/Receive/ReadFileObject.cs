
namespace NetWorkHelper
{
    internal class ReadFileObject
    {
        public ReadFileObject(int index, byte[] buffer)
        {
            Index = index;
            Buffer = buffer;
        }

        public int Index { get; set; }

        public byte[] Buffer { get; set; }
    }
}
