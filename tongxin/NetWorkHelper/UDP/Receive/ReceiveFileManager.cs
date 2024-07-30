using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace NetWorkHelper
{
    public class ReceiveFileManager : IDisposable
    {

        #region Fields
        private string _path;
        private string _tempFileName;
        private string _fullName;
        private int _partSize;
        private long _length;
        private FileStream _fileStream;
        private DateTime _lastReceiveTime;
        private Timer _timer;
        private int _interval = 5000;
        private long _receivePartCount;

        private static object SyncLock = new object();
        private static readonly int ReceiveTimeout = 5000;
        private static readonly string FileTemptName = ".tmp";

        #endregion

        #region Constructors

        public ReceiveFileManager() { }

        public ReceiveFileManager(string md5, string path, string fileName, long partCount, int partSize, long length, IPEndPoint remoteIP)
        {
            MD5 = md5;
            _path = path;
            Name = fileName;
            PartCount = partCount;
            _partSize = partSize;
            _length = length;
            RemoteIP = remoteIP;
            Create();
        }

        #endregion

        #region Events

        public event FileReceiveCompleteEventHandler ReceiveFileComplete;

        public event EventHandler ReceiveFileTimeout;

        #endregion

        #region Properties

        public string MD5 { get; }

        public long PartCount { get; }

        public string Name { get; }

        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                {
                    GetFileName();
                }
                return _fullName;
            }
        }

        public object Tag { get; set; }

        public bool Success { get; private set; }

        public IPEndPoint RemoteIP { get; }

        public bool Completed
        {
            get { return PartCount == _receivePartCount; }
        }

        private Stream FileStream { get; set; }

        private Dictionary<int, bool> ReceiveFilePartList { get; set; }

        private Timer Timer
        {
            get
            {
                if (_timer == null)
                {
                    _timer = new Timer(
                        new TimerCallback(delegate (object obj)
                        {
                            TimeSpan ts = DateTime.Now - _lastReceiveTime;
                            if (ts.TotalMilliseconds > ReceiveTimeout)
                            {
                                _lastReceiveTime = DateTime.Now;
                                OnReceiveFileTimeout(EventArgs.Empty);
                            }
                        }),
                        null,
                        Timeout.Infinite,
                        _interval);
                }
                return _timer;
            }
        }

        #endregion

        #region Methods

        private void Create()
        {
            _tempFileName = string.Format("{0}\\{1}{2}", _path, Name, FileTemptName);
            _fileStream = new FileStream(_tempFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, _partSize * 10, true);
            FileStream = Stream.Synchronized(_fileStream);
            ReceiveFilePartList = new Dictionary<int, bool>();
            for (int i = 0; i < PartCount; i++)
            {
                ReceiveFilePartList.Add(i, false);
            }
        }

        public void Start()
        {
            _lastReceiveTime = DateTime.Now;
            Timer.Change(0, _interval);
        }

        public int GetNextReceiveIndex()
        {
            foreach (int index in ReceiveFilePartList.Keys)
            {
                if (ReceiveFilePartList[index] == false)
                {
                    return index;
                }
            }
            return -1;
        }

        public int ReceiveBuffer(int index, byte[] buffer)
        {
            _lastReceiveTime = DateTime.Now;
            if (ReceiveFilePartList[index])
            {
                return 0;
            }
            else
            {
                lock (SyncLock)
                {
                    ReceiveFilePartList[index] = true;
                    _receivePartCount++;
                }
                FileStream.Position = index * _partSize;
                FileStream.BeginWrite(buffer, 0, buffer.Length, new AsyncCallback(EndWrite), _receivePartCount);
                return buffer.Length;
            }
        }

        protected virtual void OnReceiveFileComplete(FileReceiveCompleteEventArgs e)
        {
            ReceiveFileComplete?.Invoke(this, e);
        }

        protected virtual void OnReceiveFileTimeout(EventArgs e)
        {
            ReceiveFileTimeout?.Invoke(this, e);
        }

        private void EndWrite(IAsyncResult result)
        {
            if (FileStream == null)
            {
                return;
            }

            FileStream.EndWrite(result);

            long index = (long)result.AsyncState;
            if (index == PartCount)
            {
                Dispose();
                File.Move(_tempFileName, FileName);
                Success = MD5 == MD5Helper.CretaeMD5(FileName);
                OnReceiveFileComplete(new FileReceiveCompleteEventArgs(Success));
            }
        }

        private void GetFileName()
        {
            _fullName = string.Format("{0}\\{1}", _path, Name);
            int nameIndex = 1;
            int index = Name.LastIndexOf('.');
            while (File.Exists(_fullName))
            {
                _fullName = string.Format("{0}\\{1}", _path, Name.Insert(index, nameIndex.ToString("_0")));
                nameIndex++;
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
            if (_fileStream != null)
            {
                FileStream.Flush();
                FileStream.Close();
                FileStream.Dispose();
                FileStream = null;
                _fileStream = null;
            }
            if (ReceiveFilePartList != null)
            {
                ReceiveFilePartList.Clear();
                ReceiveFilePartList = null;
            }
        }

        #endregion
    }
}
