using System.Collections.Generic;

namespace NetWorkHelper.Addins
{
    /// <summary>
	/// 用于宿主应用程序向插件传递必要的参数信息
	/// </summary>
	public static class AddinUtil
    {
        private static IDictionary<string, object> DicUtil = new Dictionary<string, object>();

        #region RegisterObject
        /// <summary>
        /// RegisterObject
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public static void RegisterObject(string name, object obj)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    AddinUtil.Remove(name);
                }

                AddinUtil.DicUtil.Add(name, obj);
            }
        }
        #endregion

        #region GetObject
        /// <summary>
        /// GetObject
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetObject(string name)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    return AddinUtil.DicUtil[name];
                }
                return null;
            }
        }
        #endregion

        #region Remove
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="name"></param>
        public static void Remove(string name)
        {
            lock (AddinUtil.DicUtil)
            {
                if (AddinUtil.DicUtil.ContainsKey(name))
                {
                    AddinUtil.DicUtil.Remove(name);
                }
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clear
        /// </summary>
        public static void Clear()
        {
            lock (AddinUtil.DicUtil)
            {
                AddinUtil.DicUtil.Clear();
            }
        }
        #endregion
    }
}
