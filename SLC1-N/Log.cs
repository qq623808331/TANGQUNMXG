using System;
using System.IO;

namespace SLC1_N
{
    class Log
    {
        /// <summary>
        /// log日志，txt的
        /// </summary>
        /// <param name="Log1">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        /// 
        public void LINtest_Logmsg(string Log1)
        {
            #region 创建日志
            string Log = "";
            string path = System.Environment.CurrentDirectory + "\\LIN_Logtest";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void MES_Logmsg(string Log1)
        {
            #region 创建日志
            string Log = "";
            string path = System.Environment.CurrentDirectory + "\\LIN_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        /// <summary>
        /// log日志，txt的
        /// </summary>
        /// <param name="Log1">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public void TCP_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "  ";
            string path = System.Environment.CurrentDirectory + "\\TCP_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }

        /// <summary>
        /// log日志，txt的
        /// </summary>
        /// <param name="Log1">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public void PLC_Logmsg(string Log1)
        {
            #region 创建日志
            string Log = "";
            string path = System.Environment.CurrentDirectory + "\\PLC_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        /// <summary>
        /// log日志，txt的
        /// </summary>
        /// <param name="Log1">内容</param>
        /// <param name="name">名字</param>
        /// <param name="path">路径</param>
        public void CH1Port_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH1Port_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void CH2Port_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH2Port_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void CH1FlowPort_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH1FlowPort_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }

        public void CH2FlowPort_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH2FlowPort_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void CH3FlowPort_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH3FlowPort_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void CH4FlowPort_Logmsg(string Log1)
        {
            #region 创建日志
            //string Log = "";
            string Log = System.DateTime.Now.ToString("yyyyMMdd HH:mm:ss  ");
            string path = System.Environment.CurrentDirectory + "\\CH4FlowPort_Log";
            Log += Log1 + "\r\n";

            //生成目录
            //创建文件夹
            if (Directory.Exists(path) == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(path);
            }

            // 判断文件是否存在，不存在则创建，否则读取值显示到txt文档
            if (!System.IO.File.Exists(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt"))
            {
                FileStream fs1 = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(Log);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path + "/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt" + "", FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(Log);//开始写入值
                sr.Close();
                fs.Close();
            }
            #endregion
        }
        public void DeleteFile(string DirectoryName)
        {

            string deletePath = System.Environment.CurrentDirectory + "\\" + DirectoryName;
            DirectoryInfo log = new DirectoryInfo(deletePath);
            if (log.Exists)
            {
                FileInfo[] files = log.GetFiles();
                int deleteDays = -7;
                string date = DateTime.Today.AddDays(deleteDays).ToString("yyyyMMdd_hhmmss");
                foreach (FileInfo file in files)
                {
                    if (date.CompareTo(file.LastWriteTime.ToString("yyyyMMdd_hhmmss")) > 0)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(file.Name, ".txt")) ;
                        File.Delete(log + "\\" + file.Name);
                    }
                }

            }

        }
    }
}
