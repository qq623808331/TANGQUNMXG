/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2014-08-05
 * * 说明：文件操作类
 * *
********************************************************************/
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NetWorkHelper.Helper
{
    public static class FileHelper
    {
        #region 将字符串写成文件 
        /// <summary>
        /// 将字符串写成文件
        /// </summary>       
        public static void GenerateFile(string filePath, string text)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            StreamWriter sw = new StreamWriter(fs);

            sw.Write(text);
            sw.Flush();

            sw.Close();
            fs.Close();
        }
        #endregion

        #region 读取文本文件的内容
        /// <summary>
        /// 读取文本文件的内容 
        /// </summary>       
        public static string GetFileContent(string file_path)
        {
            if (!File.Exists(file_path))
            {
                return null;
            }

            StreamReader reader = new StreamReader(file_path, Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();

            return content;
        }
        #endregion

        #region 将二进制数据写入文件中 
        /// <summary>
        /// 将二进制数据写入文件中 
        /// </summary>    
        public static void WriteBuffToFile(byte[] buff, int offset, int len, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(buff, offset, len);
            bw.Flush();

            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// WriteBuffToFile 将二进制数据写入文件中
        /// </summary>   
        public static void WriteBuffToFile(byte[] buff, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(buff);
            bw.Flush();

            bw.Close();
            fs.Close();
        }
        #endregion

        #region 从文件中读取二进制数据
        /// <summary>
        /// 从文件中读取二进制数据 
        /// </summary>      
        public static byte[] ReadFileReturnBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryReader br = new BinaryReader(fs);

            byte[] buff = br.ReadBytes((int)fs.Length);

            br.Close();
            fs.Close();

            return buff;
        }
        #endregion

        #region 获取要打开的文件路径 
        /// <summary>
        /// 获取要打开的文件路径 
        /// </summary>        
        public static string GetFileToOpen(string title)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "All Files (*.*)|*.*";
            openDlg.FileName = "";
            if (title != null)
            {
                openDlg.Title = title;
            }

            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;

            DialogResult res = openDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return openDlg.FileName;
            }

            return null;
        }

        /// <summary>
        /// 获取要打开的文件路径
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="iniDir">初始目录</param>
        /// <param name="extendName">文件类型</param>
        /// <returns></returns>
		public static string GetFileToOpen(string title, string iniDir, string extendName)
        {
            return GetFileToOpen2(title, iniDir, extendName);
        }
        /// <summary>
        /// 获取要打开的文件路径
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="iniDir">初始目录</param>
        /// <param name="extendNames">文件类型</param>
        /// <returns></returns>
        public static string GetFileToOpen2(string title, string iniDir, params string[] extendNames)
        {
            StringBuilder filterBuilder = new StringBuilder("(");
            for (int i = 0; i < extendNames.Length; i++)
            {
                filterBuilder.Append("*");
                filterBuilder.Append(extendNames[i]);
                if (i < extendNames.Length - 1)
                {
                    filterBuilder.Append(";");
                }
                else
                {
                    filterBuilder.Append(")");
                }
            }
            filterBuilder.Append("|");
            for (int i = 0; i < extendNames.Length; i++)
            {
                filterBuilder.Append("*");
                filterBuilder.Append(extendNames[i]);
                if (i < extendNames.Length - 1)
                {
                    filterBuilder.Append(";");
                }
            }

            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = filterBuilder.ToString();
            openDlg.FileName = "";
            openDlg.InitialDirectory = iniDir;
            if (title != null)
            {
                openDlg.Title = title;
            }

            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;

            DialogResult res = openDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return openDlg.FileName;
            }

            return null;
        }
        #endregion

        #region 获取要打开的文件夹 
        /// <summary>
        /// 获取要打开的文件夹
        /// </summary>
        /// <param name="newFolderButton">新建文件夹按钮是否显示在浏览文件夹对话框中</param>
        /// <returns></returns>
        public static string GetFolderToOpen(bool newFolderButton)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = newFolderButton;
            DialogResult res = folderDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }

            return null;
        }
        #endregion

        #region 获取要保存的文件的路径 
        /// <summary>
        /// 获取要保存的文件的路径
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="defaultName">默认名称</param>
        /// <param name="iniDir">初始化文件夹</param>
        /// <returns></returns>
        public static string GetPathToSave(string title, string defaultName, string iniDir)
        {
            string extendName = Path.GetExtension(defaultName);
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = string.Format("The Files (*{0})|*{0}", extendName);
            saveDlg.FileName = defaultName;
            saveDlg.InitialDirectory = iniDir;
            saveDlg.OverwritePrompt = false;
            if (title != null)
            {
                saveDlg.Title = title;
            }

            DialogResult res = saveDlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                return saveDlg.FileName;
            }

            return null;
        }
        #endregion

        #region 获取不包括路径的文件名
        /// <summary>
        /// 获取不包括路径的文件名
        /// </summary>
        /// <param name="filePath">指定的文件夹</param>
        /// <returns></returns>
        public static string GetFileNameNoPath(string filePath)
        {
            return Path.GetFileName(filePath);
        }
        #endregion

        #region 获取文件的大小
        /// <summary>
        /// 获取目标文件的大小
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static ulong GetFileSize(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            return (ulong)info.Length;
        }
        #endregion

        #region 获取文件夹的大小
        /// <summary>
        /// 获取某个文件夹的大小。
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns></returns>
        public static ulong GetDirectorySize(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return 0;
            }
            ulong len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += (ulong)fi.Length;
            }

            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += FileHelper.GetDirectorySize(dis[i].FullName);
                }
            }

            return len;
        }
        #endregion

        #region 从文件流中读取指定大小的内容 
        /// <summary>
        /// 从文件流中读取指定大小的内容
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="buff"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        public static void ReadFileData(FileStream fs, byte[] buff, int count, int offset)
        {
            int readCount = 0;
            while (readCount < count)
            {
                int read = fs.Read(buff, offset + readCount, count - readCount);
                readCount += read;
            }

            return;
        }
        #endregion

        #region 获取文件所在的目录路径
        /// <summary>
        /// 获取文件所在的目录路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileDirectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
        #endregion

        #region 删除文件 
        /// <summary>
        /// 删除文件 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #region 确保扩展名正确 
        /// <summary>
        /// 确保扩展名正确
        /// </summary>
        /// <param name="origin_path">文件名</param>
        /// <param name="extend_name">扩展名</param>
        /// <returns></returns>
        public static string EnsureExtendName(string origin_path, string extend_name)
        {
            if (Path.GetExtension(origin_path) != extend_name)
            {
                origin_path += extend_name;
            }

            return origin_path;
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        public static void ClearDirectory(string dirPath)
        {
            string[] filePaths = Directory.GetFiles(dirPath);
            foreach (string file in filePaths)
            {
                File.Delete(file);
            }

            foreach (string childDirPath in Directory.GetDirectories(dirPath))
            {
                FileHelper.DeleteDirectory(childDirPath);
            }
        }
        #endregion

        #region 删除文件夹
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        public static void DeleteDirectory(string dirPath)
        {
            foreach (string filePath in Directory.GetFiles(dirPath))
            {
                File.Delete(filePath);
            }

            foreach (string childDirPath in Directory.GetDirectories(dirPath))
            {
                FileHelper.DeleteDirectory(childDirPath);
            }

            DirectoryInfo dir = new DirectoryInfo(dirPath);
            dir.Refresh();
            if ((dir.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                dir.Attributes &= ~FileAttributes.ReadOnly;
            }
            dir.Delete();
        }
        #endregion

        #region 将某个文件夹下的多个文件和子文件夹移到另外一个目录中
        /// <summary>
        /// 将某个文件夹下的多个文件和子文件夹移到另外一个目录中。
        /// </summary>
        /// <param name="oldParentDirectoryPath">移动之前文件和子文件夹所处的父目录路径</param>
        /// <param name="filesBeMoved">被移动的文件名称的集合</param>
        /// <param name="directoriesBeMoved">被移动的文件夹名称的集合</param>
        /// <param name="newParentDirectoryPath">移往的目标文件夹路径</param>
        public static void Move(string oldParentDirectoryPath, IEnumerable<string> filesBeMoved, IEnumerable<string> directoriesBeMoved, string newParentDirectoryPath)
        {
            if (filesBeMoved != null)
            {
                foreach (string beMoved in filesBeMoved)
                {
                    string pathOfBeMoved = oldParentDirectoryPath + beMoved;
                    if (File.Exists(pathOfBeMoved))
                    {
                        File.Move(pathOfBeMoved, newParentDirectoryPath + beMoved);
                    }
                }
            }

            if (directoriesBeMoved != null)
            {
                foreach (string beMoved in directoriesBeMoved)
                {
                    string pathOfBeMoved = oldParentDirectoryPath + beMoved;

                    if (Directory.Exists(pathOfBeMoved))
                    {
                        Directory.Move(pathOfBeMoved, newParentDirectoryPath + beMoved);
                    }
                }
            }
        }
        #endregion

        #region 拷贝多个文件和文件夹
        /// <summary>
        /// 拷贝多个文件和文件夹
        /// </summary>
        /// <param name="sourceParentDirectoryPath">被拷贝的文件和文件夹所处的父目录路径</param>
        /// <param name="filesBeCopyed">被复制的文件名称的集合</param>
        /// <param name="directoriesCopyed">被复制的文件夹名称的集合</param>
        /// <param name="destParentDirectoryPath">目标目录的路径</param>
        public static void Copy(string sourceParentDirectoryPath, IEnumerable<string> filesBeCopyed, IEnumerable<string> directoriesCopyed, string destParentDirectoryPath)
        {
            bool sameParentDir = sourceParentDirectoryPath == destParentDirectoryPath;
            if (filesBeCopyed != null)
            {
                foreach (string beCopyed in filesBeCopyed)
                {
                    string newName = beCopyed;
                    while (sameParentDir && File.Exists(destParentDirectoryPath + newName))
                    {
                        newName = "副本-" + newName;
                    }
                    string pathOfBeCopyed = sourceParentDirectoryPath + beCopyed;
                    if (File.Exists(pathOfBeCopyed))
                    {
                        File.Copy(pathOfBeCopyed, destParentDirectoryPath + newName);
                    }
                }
            }

            if (directoriesCopyed != null)
            {
                foreach (string beCopyed in directoriesCopyed)
                {
                    string newName = beCopyed;
                    while (sameParentDir && Directory.Exists(destParentDirectoryPath + newName))
                    {
                        newName = "副本-" + newName;
                    }
                    string pathOfBeCopyed = sourceParentDirectoryPath + beCopyed;
                    if (Directory.Exists(pathOfBeCopyed))
                    {
                        CopyDirectoryAndFiles(sourceParentDirectoryPath, beCopyed, destParentDirectoryPath, newName);
                    }

                }
            }
        }

        /// <summary>
        /// 递归拷贝文件夹以及下面的所有文件
        /// </summary>       
        private static void CopyDirectoryAndFiles(string sourceParentDirectoryPath, string dirBeCopyed, string destParentDirectoryPath, string newDirName)
        {
            Directory.CreateDirectory(destParentDirectoryPath + newDirName);
            DirectoryInfo source = new DirectoryInfo(sourceParentDirectoryPath + dirBeCopyed);
            foreach (FileInfo file in source.GetFiles())
            {
                File.Copy(file.FullName, destParentDirectoryPath + newDirName + "\\" + file.Name);
            }
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyDirectoryAndFiles(sourceParentDirectoryPath + dirBeCopyed + "\\", dir.Name, destParentDirectoryPath + newDirName + "\\", dir.Name);
            }
        }
        #endregion

        #region 获取目标目录下以及其子目录下的所有文件（采用相对路径）
        /// <summary>
        /// 获取目标目录下以及其子目录下的所有文件（采用相对路径）
        /// </summary>        
        public static List<string> GetOffspringFiles(string dirPath)
        {
            List<string> list = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            DoGetOffspringFiles(dirPath, dir, ref list);
            return list;
        }

        private static void DoGetOffspringFiles(string rootPath, DirectoryInfo dir, ref List<string> list)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                list.Add(file.FullName.Substring(rootPath.Length));
            }
            foreach (DirectoryInfo childDir in dir.GetDirectories())
            {
                DoGetOffspringFiles(rootPath, childDir, ref list);
            }
        }
        #endregion
    }
}
