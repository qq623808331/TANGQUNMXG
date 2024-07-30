/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：应用程序帮助类
 * *
********************************************************************/
using System.Diagnostics;
using System.Threading;

namespace NetWorkHelper.Helper
{
    public static class ApplicationHelper
    {
        #region 启动指定程序 

        /// <summary>
        /// 启动一个应用程序或进程
        /// </summary>
        /// <param name="appFilePath">程序或线程的路径</param>
        public static void StartApplication(string appFilePath)
        {
            Process downprocess = new Process();
            downprocess.StartInfo.FileName = appFilePath;
            downprocess.Start();
        }
        #endregion

        #region 判断目标应用程序是否已经启动

        /// <summary>
        ///  目标应用程序是否已经启动。通常用于判断单实例应用。将占用锁。
        /// </summary>
        /// <param name="instanceName">应用名称</param>
        /// <returns></returns>
        public static bool IsAppInstanceExist(string instanceName)
        {

            bool createdNew = false;
            ApplicationHelper.MutexForSingletonExe = new Mutex(false, instanceName, out createdNew);

            return (!createdNew);
        }

        public static System.Threading.Mutex MutexForSingletonExe = null;
        /// <summary>
        /// 释放进程中占用的程序。
        /// </summary>
        /// <param name="instanceName"></param>
        public static void ReleaseAppInstance(string instanceName)
        {
            if (ApplicationHelper.MutexForSingletonExe != null)
            {
                ApplicationHelper.MutexForSingletonExe.Close();
                ApplicationHelper.MutexForSingletonExe = null;
            }
        }
        #endregion

        #region 打开网址
        /// <summary>
        /// 在浏览器中打开Url链接
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            Process.Start(url);
        }
        #endregion
    }
}
