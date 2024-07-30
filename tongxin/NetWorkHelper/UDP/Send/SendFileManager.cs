using System;
using System.IO;

namespace NetWorkHelper
{
    /// <summary>
    /// 文件管理类
    /// </summary>
    public class SendFileManager : IDisposable
    {
        #region 变量

        private FileStream _fileStream;

        #endregion

        #region 构造函数

        public SendFileManager(string fileName)
        {
            FileName = fileName;
            Create(fileName);
        }

        public SendFileManager(string fileName, int partSize)
        {
            FileName = fileName;
            PartSize = partSize;
            Create(fileName);
        }

        #endregion

        #region 属性

        public long PartCount { get; set; }

        public long Length { get; private set; }

        public int PartSize { get; } = 1024 * 20;

        public string FileName { get; }

        public string Name
        {
            get { return new FileInfo(FileName).Name; }
        }

        public string MD5 { get; private set; }

        public object Tag { get; set; }

        internal Stream FileStream { get; private set; }

        #endregion

        #region 方法
        /// <summary>
        /// 创建初始化文件管理类
        /// </summary>
        /// <param name="fileName">文件路径</param>
        private void Create(string fileName)
        {
            _fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, PartSize * 10, true);
            FileStream = Stream.Synchronized(_fileStream);
            Length = _fileStream.Length;
            PartCount = Length / PartSize;
            if (Length % PartSize != 0)
            {
                PartCount++;
            }
            MD5 = MD5Helper.CretaeMD5(_fileStream);
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="index"></param>
        public void Read(int index)
        {
            int size = PartSize;
            if (Length - PartSize * index < PartSize)
            {
                size = (int)(Length - PartSize * index);
            }
            byte[] buffer = new byte[size];
            ReadFileObject obj = new ReadFileObject(index, buffer);
            FileStream.Position = index * PartSize;
            FileStream.BeginRead(buffer, 0, size, new AsyncCallback(EndRead), obj);
        }

        /// <summary>
        /// 结束读取文件
        /// </summary>
        /// <param name="result"></param>
        private void EndRead(IAsyncResult result)
        {
            if (FileStream == null)
            {
                return;
            }
            int length = FileStream.EndRead(result);
            ReadFileObject state = (ReadFileObject)result.AsyncState;
            int index = state.Index;
            byte[] buffer = state.Buffer;
            ReadFileBufferEventArgs e = null;
            if (length < PartSize)
            {
                byte[] realBuffer = new byte[length];
                Buffer.BlockCopy(buffer, 0, realBuffer, 0, length);
                e = new ReadFileBufferEventArgs(index, realBuffer);
            }
            else
            {
                e = new ReadFileBufferEventArgs(index, buffer);
            }
            OnReadFileBuffer(e);
        }

        #endregion

        #region 事件
        /// <summary>
        /// 读取文件事件
        /// </summary>
        public event ReadFileBufferEventHandler ReadFileBuffer;
        /// <summary>
        /// 读取文件方法
        /// </summary>
        /// <param name="e"></param>
        protected void OnReadFileBuffer(ReadFileBufferEventArgs e)
        {
            ReadFileBuffer?.Invoke(this, e);
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_fileStream != null)
            {
                FileStream.Flush();
                FileStream.Close();
                FileStream.Dispose();
                FileStream = null;
                _fileStream = null;
            }
        }

        #endregion
    }
}
