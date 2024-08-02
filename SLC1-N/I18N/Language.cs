using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace SLC1_N
{

    public class I18N
    {
        public Action ChangLangNotify { get; set; }

        private static string mLanguage;

        private static string CfgPath
        {
            get
            {
                string langpath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "\\LLConfig\\Language\\";
                return langpath;
            }
        }

        public static string Language
        {
            get
            {
                return GetLang();
            }
            set
            {
                if (mLanguage != value)
                {
                    mLanguage = value;
                    SetLanguage();
                    string lang_file = CfgPath + "Language.lang";
                    if (File.Exists(lang_file))
                    {
                        File.WriteAllText(lang_file, mLanguage, Encoding.UTF8);
                    }
                }
            }
        }

        private static string LanguageDir = CfgPath + @"{0}\{1}.lang";

        private static void SetLanguage()
        {
            //int lang_i = 0;
            //if (!mLanguage.Equals("zh-CN"))
            //{
            //    lang_i = 1;
            //}
            //TMachineConfig.ChangeLanguage(lang_i);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(mLanguage);
        }

        private static string GetLang()
        {
            if (string.IsNullOrEmpty(mLanguage))
            {
                string lang = CfgPath + "Language.lang";
                if (File.Exists(lang))
                {
                    mLanguage = File.ReadAllText(lang, Encoding.UTF8);
                    SetLanguage();
                    return mLanguage;
                }
                else
                {
                    mLanguage = "zh-CN";
                    return mLanguage;
                }
            }

            return mLanguage;
        }

        public static Dictionary<string, string[]> LoadLanguage(object obj)
        {
            Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
            try
            {
                string filename = string.Format(LanguageDir, Language, obj is string ? obj.ToString() : obj.GetType().ToString());
                if (!File.Exists(filename))
                {
                    return null;
                }
                List<string> list_Get = Read(filename);
                foreach (string s in list_Get)
                {
                    string[] arr = s.Split('=');
                    string[] arr_value = arr[1].Split('#');
                    if (!dicLang.ContainsKey(arr[0]))
                    {
                        dicLang.Add(arr[0], arr_value);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dicLang;
        }

        public static string GetLangText(Dictionary<string, string[]> dic, string key)
        {
            string text = string.Empty;
            try
            {
                if (dic != null)
                    text = dic[key.Trim()][0];
                else
                    return key;
            }
            catch (Exception ex)
            {
                if (!Language.Equals("zh-CN"))
                {
                    return "#" + key;
                }
                else
                {
                    return key;
                }
            }

            return text;
        }

        private static List<string> Read(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.UTF8);
            string line;
            List<string> list = new List<string>();
            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                list.Add(line.ToString());
            }

            return list;
        }
    }
}
