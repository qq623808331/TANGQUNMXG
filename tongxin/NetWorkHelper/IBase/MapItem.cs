namespace NetWorkHelper.IBase
{
    /// <summary>
    /// MapItem 映射项。
    /// </summary>
    public class MapItem
    {
        #region 构造函数
        public MapItem()
        {
        }

        public MapItem(string theSource, string theTarget)
        {
            this.source = theSource;
            this.target = theTarget;
        }
        #endregion

        #region Source
        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        #endregion

        #region Target
        private string target;
        public string Target
        {
            get { return target; }
            set { target = value; }
        }
        #endregion
    }
}
