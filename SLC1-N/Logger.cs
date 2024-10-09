using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLC1_N
{
    public class Logger
    {
        // 用于存放写日志任务的队列
        private Queue<string> _queue;

        // 用于写日志的线程
        private Thread _loggingThread;

        private System.Windows.Forms.Timer timer1;

        // 用于通知是否有新日志要写的“信号器”
        private ManualResetEvent _hasNew;

        public static ListBox listBoxLog;

        // 构造函数，初始化。
        private Logger()
        {
            _queue = new Queue<string>();
            _hasNew = new ManualResetEvent(false);
            //_loggingThread = new Thread(Process);
            //_loggingThread.IsBackground = true;
            //_loggingThread.Start();
            timer1 = new System.Windows.Forms.Timer();
            //timer1.Tick += Timer1_Tick;
            timer1.Interval = 300;
            timer1.Tick += delegate (object o, EventArgs args)
            {
                Process();
            };
            timer1.Start();
        }

        // 使用单例模式，保持一个Logger对象
        private static readonly Logger _logger = new Logger();

        private static Logger GetInstance()
        {
            /* 不安全代码
            lock (locker) {
                if (_logger == null) {
                    _logger = new Logger();
                }
            }*/
            return _logger;
        }

        // 处理队列中的任务
        private void Process()
        {
            //while (true)
            //{
            // 等待接收信号，阻塞线程。
            //  _hasNew.WaitOne();
            // 接收到信号后，重置“信号器”，信号关闭。
            // _hasNew.Reset();
            // 由于队列中的任务可能在极速地增加，这里等待是为了一次能处理更多的任务，减少对队列的频繁“进出”操作。
            // Thread.Sleep(100);
            // 开始执行队列中的任务。
            // 由于执行过程中还可能会有新的任务，所以不能直接对原来的 _queue 进行操作，
            // 先将_queue中的任务复制一份后将其清空，然后对这份拷贝进行操作。

            Queue<string> queueCopy;
            lock (_queue)
            {
                queueCopy = new Queue<string>(_queue);
                _queue.Clear();
            }

            foreach (var item in queueCopy)
            {
                try
                {
                    if (listBoxLog != null)
                    {
                        listBoxLog.Items.Add(ToStr(item));
                        if (listBoxLog.Items.Count > 100)
                        {
                            listBoxLog.Items.RemoveAt(0);
                        }
                    };
                    if (listBoxLog.Items.Count > 2)
                    {
                        listBoxLog.SelectedIndex = listBoxLog.Items.Count - 1;
                    }
                    //listBoxLog.SelectedIndex = -1;
                }
                catch (Exception)
                {
                }
            }
            //}
        }

        public string ToStr(string content)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "==>:" + content;
        }

        private void WriteLog(string content)
        {
            lock (_queue)
            { // todo: 这里存在线程安全问题，可能会发生阻塞。
              // 将任务加到队列
                _queue.Enqueue(content);
            }

            // 打开“信号”
            _hasNew.Set();
        }

        // 公开一个Write方法供外部调用
        public static void Log(string content)
        {
            // WriteLog 方法只是向队列中添加任务，执行时间极短，所以使用Task.Run。
            Task.Run(() => GetInstance().WriteLog(content));
            Log log = new Log();
            log.PLC_Logmsg(DateTime.Now.ToString() + content);
        }

        public static void bind(ListBox listBox1)
        {
            listBoxLog = listBox1;
            listBoxLog.MouseDoubleClick += new MouseEventHandler(LogDoubleClick);
        }

        public static void LogDoubleClick(object sender, EventArgs e)
        {
            if (listBoxLog.SelectedItem != null)
            {
                MessageBox.Show(listBoxLog.SelectedItem.ToString());
            }
        }
    }
}