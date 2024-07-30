/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2017-09-18
 * * 说明：Csv文件操作类
 * *
********************************************************************/
using System.Data;
using System.IO;
using System.Text;

namespace NetWorkHelper.ITool
{
    class CsvManager
    {
        /// <summary>  
        /// 导出报表为Csv  
        /// </summary>  
        /// <param name="dt">DataTable</param>  
        /// <param name="strFilePath">物理路径</param>  
        /// <param name="tableheader">表头</param>  
        /// <param name="columname">字段标题,逗号分隔</param>  
        public static bool DataTable2Csv(DataTable dt, string strFilePath, string tableheader, string columname)
        {
            try
            {
                string strBufferLine;
                StreamWriter strmWriterObj = new StreamWriter(strFilePath, false, Encoding.UTF8);
                strmWriterObj.WriteLine(tableheader);
                strmWriterObj.WriteLine(columname);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strBufferLine = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j > 0)
                            strBufferLine += ",";
                        strBufferLine += dt.Rows[i][j].ToString();
                    }
                    strmWriterObj.WriteLine(strBufferLine);
                }
                strmWriterObj.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>  
        /// 将Csv读入DataTable  
        /// </summary>  
        /// <param name="filePath">csv文件路径</param>  
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>  
        /// <param name="dataTable">要导出的dataTable</param>  
        public static DataTable Csv2Dt(string filePath, int n, DataTable dataTable)
        {
            StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false);
            int i, m = 0;
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    break;
                }
                if (m >= n + 1)
                {
                    string[] split = str.Split(',');

                    DataRow dr = dataTable.NewRow();
                    for (i = 0; i < split.Length; i++)
                    {
                        dr[i] = split[i];
                    }
                    dataTable.Rows.Add(dr);
                }
            }
            return dataTable;
        }
    }
}
