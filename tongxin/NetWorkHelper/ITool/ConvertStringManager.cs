/********************************************************************
 * *
 * * Copyright (C) 2013-2018 uiskin.cn
 * * 作者： BinGoo QQ：315567586 
 * * 请尊重作者劳动成果，请保留以上作者信息，禁止用于商业活动。
 * *
 * * 创建时间：2017-09-18
 * * 说明：字符串，数字转换管理类
 * *
********************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetWorkHelper.ITool
{
    public class ConvertStringManager
    {
        #region 十六进制字符串和数组互转
        /// <summary>
        /// 十六进制字符串转为字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] StringToHexByteArray(string s)
        {
            try
            {
                s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    s += " ";
                byte[] returnBytes = new byte[s.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
                return returnBytes;
            }
            catch
            {
                return new byte[0];
            }
        }
        /// <summary>
        /// 字节数组转为十六进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="intervalChar"></param>
        /// <returns></returns>
        public static string HexByteArrayToString(byte[] data, char intervalChar = ' ')
        {
            try
            {
                StringBuilder sb = new StringBuilder(data.Length * 3);
                foreach (byte b in data)
                {
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, intervalChar));
                }
                return sb.ToString().ToUpper();//将得到的字符全部以字母大写形式输出
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 各进制数间转换
        /// <summary>  
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。  
        /// </summary>  
        /// <param name="value">要转换的值,即原值</param>  
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>  
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>  
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制  
                string result = Convert.ToString(intValue, to);  //再转成目标进制  
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度  
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {

                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);  
                return "0";
            }
        }
        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>  
        /// 使用指定字符集将string转换成byte[]  
        /// </summary>  
        /// <param name="text">要转换的字符串</param>  
        /// <param name="encoding">字符编码</param>  
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }
        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>  
        /// 使用指定字符集将byte[]转换成string  
        /// </summary>  
        /// <param name="bytes">要转换的字节数组</param>  
        /// <param name="encoding">字符编码</param>  
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>  
        /// 将byte[]转换成int  
        /// </summary>  
        /// <param name="data">需要转换成整数的byte数组</param>  
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0  
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数  
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理  
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区  
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区  
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num  
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数  
            return num;
        }
        #endregion 

        #region 字符串、数组、列表互转
        /// <summary>  
        /// 把字符串按照分隔符转换成 List  
        /// </summary>  
        /// <param name="str">源字符串</param>  
        /// <param name="speater">分隔符</param>  
        /// <returns></returns>  
        public static List<string> GetStrList(string str, char speater)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    list.Add(strVal);
                }
            }
            return list;
        }
        /// <summary>  
        /// 把字符串按照分隔符转换成数组  
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="speater">分隔符</param>  
        /// <returns></returns>  
        public static string[] GetStrArray(string str, char speater)
        {
            return str.Split(speater);
        }
        /// <summary>  
        /// 把 字符串列表 按照分隔符组装成 新字符串  
        /// </summary>  
        /// <param name="list"></param>  
        /// <param name="speater"></param>  
        /// <returns></returns>  
        public static string GetListToString(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 删除最后一个字符之后的字符

        /// <summary>  
        /// 删除最后结尾的一个逗号  
        /// </summary>  
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>  
        /// 删除最后结尾的指定字符后的字符  
        /// </summary>  
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion  

        #region 全角半角装换
        /// <summary>  
        /// 转全角的函数(SBC case)  
        /// </summary>  
        /// <param name="input"></param>  
        /// <returns></returns>  
        public static string ToSbc(string input)
        {
            //半角转全角：  
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>  
        ///  转半角的函数(DBC case)  
        /// </summary>  
        /// <param name="input">输入</param>  
        /// <returns></returns>  
        public static string ToDbc(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 快速验证一个字符串是否符合指定的正则表达式。
        /// <summary>  
        /// 快速验证一个字符串是否符合指定的正则表达式。  
        /// </summary>  
        /// <param name="_express">正则表达式的内容。</param>  
        /// <param name="_value">需验证的字符串。</param>  
        /// <returns>是否合法的bool值。</returns>  
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        #endregion  

        #region 补足位数
        /// <summary>  
        /// 指定字符串的固定长度，如果字符串小于固定长度，  
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位  
        /// </summary>  
        /// <param name="text">原始字符串</param>  
        /// <param name="limitedLength">字符串的固定长度</param>  
        /// <param name="repairStr">要填补的字符</param>  
        public static string RepairZero(string text, int limitedLength, string repairStr)
        {
            //补足完整的字符串  
            string temp = "";

            //补足字符串  
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += repairStr;
            }

            //连接text  
            temp += text;

            //返回补足0的字符串  
            return temp;
        }
        #endregion  

        #region 数字转大写字符串人民币
        /// <summary>   
        /// 转换人民币大小金额   
        /// </summary>   
        /// <param name="num">金额</param>   
        /// <returns>返回大写形式</returns>   
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字   
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字   
            string str3 = "";    //从原num值中取出的值   
            string str4 = "";    //数字的字符串形式   
            string str5 = "";  //人民币大写金额形式   
            int i;    //循环变量   
            int j;    //num的值乘以100的字符串长度   
            string ch1 = "";    //数字的汉语读法   
            string ch2 = "";    //数字位的汉字读法   
            int nzero = 0;  //用来计算连续的零值是几个   
            int temp;            //从原num值中取出的值   

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数   
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式   
            j = str4.Length;      //找出最高位   
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分   

            //循环取出每一位需要转换的值   
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值   
                temp = Convert.ToInt32(str3);      //转换为数字   
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时   
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位   
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上   
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整”   
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /**/
        /// <summary>   
        /// 一个重载，将字符串先转换成数字在调用CmycurD(decimal num)   
        /// </summary>   
        /// <param name="num">用户输入的金额，字符串形式未转成decimal</param>   
        /// <returns></returns>   
        public static string CmycurD(string numstr)
        {
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                return CmycurD(num);
            }
            catch
            {
                return "非数字形式！";
            }
        }
        #endregion

        #region 数字转中文
        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns></returns>
        public static string NumberToChinese(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "零";
                    break;
            }
            if (str.Length > 1)
            {
                switch (str.Length)
                {
                    case 2:
                    case 6:
                        res += "十";
                        break;
                    case 3:
                    case 7:
                        res += "百";
                        break;
                    case 4:
                        res += "千";
                        break;
                    case 5:
                        res += "万";
                        break;
                    default:
                        res += "";
                        break;
                }
                res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            }
            return res;
        }
        #endregion
    }
}
