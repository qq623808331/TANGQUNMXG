using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SLC1_N
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                string str = GetExceptionMsg(ex, string.Empty);
                WriteLogInfo("全局异常捕捉", str);
                //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            WriteLogInfo("全局异常捕捉", str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            WriteLogInfo("全局异常捕捉", str);
            //MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        private static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        public static void WriteLogInfo(string title, string content)
        {
            try
            {
                //文件夹目录
                string urlW = System.Windows.Forms.Application.StartupPath + "\\Logs";
                if (!Directory.Exists(urlW))//是否存在
                {
                    Directory.CreateDirectory(urlW);
                }
                string a = "=======================================================";
                string[] lines = { title, content, DateTime.Now.ToString(), a };
                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string url = urlW + "\\" + fileName;
                if (!File.Exists(url))
                {
                    File.Create(url);//创建该文件
                }
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(url, true))//为true则把文本追加到文件末尾
                {
                    foreach (string line in lines)
                    {
                        file.WriteLine(line);// 直接追加文件末尾，换行
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogInfo("全局异常捕捉", ex.Message);
            }
        }
    }
}