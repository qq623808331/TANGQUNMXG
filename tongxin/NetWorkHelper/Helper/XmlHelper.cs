/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：实体类与XML互转帮助类
 * *
********************************************************************/
using System;
using System.IO;
using System.Xml.Serialization;

namespace NetWorkHelper.Helper
{
    /// <summary>
    /// 实体类与XML互转，多用于保存配置文件
    /// </summary>
    public class XmlHelper
    {
        #region 反序列化

        /// <summary>
        /// 反序列化返回实体类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns>返回实体类</returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }

        #endregion

        #region 序列化

        /// <summary>
        /// 序列化返回XML字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns>返回XML字符串</returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion

        #region 写XML字符串文件

        /// <summary>
        /// 写XML字符串文件
        /// </summary>
        /// <param name="datafilePath"></param>
        /// <param name="dataFileName"></param>
        /// <param name="message"></param>
        public static void WriteXmlData(string datafilePath, string dataFileName, string message)
        {
            //DirectoryInfo path=new DirectoryInfo(DataFileName);
            //如果数据文件目录不存在,则创建
            if (!Directory.Exists(datafilePath))
            {
                Directory.CreateDirectory(datafilePath);
            }
            FileInfo finfo = new FileInfo(datafilePath + dataFileName);
            try
            {
                using (FileStream fs = new FileStream(datafilePath + dataFileName, FileMode.Create))
                {
                    using (StreamWriter strwriter = new StreamWriter(fs))
                    {
                        try
                        {
                            strwriter.WriteLine(message);
                            strwriter.Flush();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(string.Format("数据文件写入失败信息:{0},行号{1}", ex.Message, ex.StackTrace));
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(string.Format("数据文件没有打开,详细信息如下:{0}", ee.Message));
            }
        }

        #endregion

        #region 读取XML文件

        /// <summary>
        /// 读取XML文件
        /// </summary>
        /// <param name="fileName">文件名（包含路径）</param>
        /// <returns>返回文件字符串</returns>
        public static string ReadXmlFile(string fileName)
        {
            //异常检测开始
            try
            {
                string fileContent = "";
                using (var reader = new StreamReader(fileName))
                {
                    fileContent = reader.ReadToEnd();
                }
                return fileContent;
            }
            catch
            {
                //抛出异常
                return "";
            }
            //异常检测结束
        }

        #endregion
    }
}
