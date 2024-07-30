
using System.Net;
using System.Windows.Forms;

namespace NetWorkHelper.ICommond
{
    public class CommonMethod
    {
        /// <summary>
        /// 域名转换为IP地址
        /// </summary>
        /// <param name="hostname">域名或IP地址</param>
        /// <returns>IP地址</returns>
        internal static string HostnameToIp(string hostname)
        {
            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(hostname, out ip))
                    return ip.ToString();
                else
                    return Dns.GetHostEntry(hostname).AddressList[0].ToString();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 外部调用是否需要用Invoket
        /// </summary>
        /// <param name="func">事件参数</param>
        internal static void EventInvoket(Action func)
        {
            Form form = null;
            if (Application.OpenForms.Count > 0)
            {
                form = Application.OpenForms[0];
            }
            //Form form = Application.OpenForms.Cast<Form>().FirstOrDefault();
            if (form != null && form.InvokeRequired)
            {
                form.Invoke(func);
            }
            else
            {
                func();
            }
        }
        /// <summary>
        /// 具有返回值的 非bool 外部调用是否需要用Invoket
        /// </summary>
        /// <param name="func">方法</param>
        /// <returns>返回客户操作之后的数据</returns>
        internal static object EventInvoket(Func<object> func)
        {
            object haveStr;
            Form form = null;
            if (Application.OpenForms.Count > 0)
            {
                form = Application.OpenForms[0];
            }
            //Form form = Application.OpenForms.Cast<Form>().FirstOrDefault();
            if (form != null && form.InvokeRequired)
            {
                haveStr = form.Invoke(func);
            }
            else
            {
                haveStr = func();
            }
            return haveStr;
        }
        public delegate void Action();
        public delegate TResult Func<TResult>();

        public delegate TResult Func<T, TResult>(T a);

        public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

        public delegate TResult Func<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3);

        public delegate TResult Func<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }
}
