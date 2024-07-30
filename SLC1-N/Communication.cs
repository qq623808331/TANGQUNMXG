using System;

namespace SLC1_N
{
    class Communication
    {
        /// <summary>
        /// 十六进制接收
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            try
            {
                if (bytes != null)
                {
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        returnStr += bytes[i].ToString("X2");//每个字节转换成两位十六进制
                                                             //     returnStr += " ";//两个16进制用空格隔开,方便看数据
                    }
                }
                return returnStr;
            }
            catch (Exception)
            {
                return returnStr;
            }
        }


        public Model.CH_PARAMS ReadParams(string text, bool chkunit)
        {
            Model.CH_PARAMS ch_params = new Model.CH_PARAMS();
            string hexstring_paramindex = text.Substring(6, 4);
            string hexstring_paramname1 = text.Substring(10, 4);
            string hexstring_paramname2 = text.Substring(14, 4);
            string hexstring_paramname3 = text.Substring(18, 4);
            string hexstring_paramname4 = text.Substring(22, 4);
            string hexstring_paramname5 = text.Substring(26, 4);
            string hexstring_full = text.Substring(30, 4);
            string hexstring_balan = text.Substring(34, 4);
            string hexstring_test = text.Substring(38, 4);
            string hexstring_exhasut = text.Substring(42, 4);
            string hexstring_relievedelay = text.Substring(46, 4);
            string hexstring_delay1 = text.Substring(50, 4);
            string hexstring_delay2 = text.Substring(54, 4);

            //十六进制转十进制
            double full = Convert.ToDouble(Convert.ToInt32(hexstring_full, 16)) / 10;
            double balan = Convert.ToDouble(Convert.ToInt32(hexstring_balan, 16)) / 10;
            double test = Convert.ToDouble(Convert.ToInt32(hexstring_test, 16)) / 10;
            double exhaust = Convert.ToDouble(Convert.ToInt32(hexstring_exhasut, 16)) / 10;
            double relievedelay = Convert.ToDouble(Convert.ToInt32(hexstring_relievedelay, 16)) / 10;
            double delay1 = Convert.ToDouble(Convert.ToInt32(hexstring_delay1, 16)) / 10;
            double delay2 = Convert.ToDouble(Convert.ToInt32(hexstring_delay2, 16)) / 10;

            ch_params.FullTime = full.ToString();
            ch_params.BalanTime = balan.ToString();
            ch_params.TestTime1 = test.ToString();
            ch_params.ExhaustTime = exhaust.ToString();
            ch_params.DelayTime1 = delay1.ToString();
            ch_params.DelayTime2 = delay2.ToString();
            ch_params.RelieveDelay = relievedelay.ToString();

            //读取浮点型参数
            string hexstring_fptop1 = text.Substring(58, 4);
            string hexstring_fptop2 = text.Substring(62, 4);
            string hexstring_fptop = hexstring_fptop2 + hexstring_fptop1;
            string hexstring_fplow1 = text.Substring(66, 4);
            string hexstring_fplow2 = text.Substring(70, 4);
            string hexstring_fplow = hexstring_fplow2 + hexstring_fplow1;
            string hexstring_bpmax1 = text.Substring(74, 4);
            string hexstring_bpmax2 = text.Substring(78, 4);
            string hexstring_bpmax = hexstring_bpmax2 + hexstring_bpmax1;
            string hexstring_bpmin1 = text.Substring(82, 4);
            string hexstring_bpmin2 = text.Substring(86, 4);
            string hexstring_bpmin = hexstring_bpmin2 + hexstring_bpmin1;
            string hexstring_ltop1 = text.Substring(90, 4);
            string hexstring_ltop2 = text.Substring(94, 4);
            string hexstring_ltop = hexstring_ltop2 + hexstring_ltop1;
            string hexstring_llow1 = text.Substring(98, 4);
            string hexstring_llow2 = text.Substring(102, 4);
            string hexstring_llow = hexstring_llow2 + hexstring_llow1;
            string hexstring_evol1 = text.Substring(106, 4);
            string hexstringg_evol2 = text.Substring(110, 4);
            string hexstringg_evol = hexstringg_evol2 + hexstring_evol1;
            UInt32 fpmax = Convert.ToUInt32(hexstring_fptop, 16);//字符串转16进制32位无符号整数
            ch_params.FPtoplimit = BitConverter.ToSingle(BitConverter.GetBytes(fpmax), 0).ToString();//IEEE754 字节转换float
            UInt32 fpmin = Convert.ToUInt32(hexstring_fplow, 16);//字符串转16进制32位无符号整数
            ch_params.FPlowlimit = BitConverter.ToSingle(BitConverter.GetBytes(fpmin), 0).ToString();//IEEE754 字节转换float
            UInt32 bpmax = Convert.ToUInt32(hexstring_bpmax, 16);//字符串转16进制32位无符号整数
            ch_params.BalanPreMax = BitConverter.ToSingle(BitConverter.GetBytes(bpmax), 0).ToString();//IEEE754 字节转换float
            UInt32 bpmin = Convert.ToUInt32(hexstring_bpmin, 16);//字符串转16进制32位无符号整数
            ch_params.BalanPreMin = BitConverter.ToSingle(BitConverter.GetBytes(bpmin), 0).ToString();//IEEE754 字节转换float
            UInt32 leakmax = Convert.ToUInt32(hexstring_ltop, 16);//字符串转16进制32位无符号整数
            ch_params.Leaktoplimit = BitConverter.ToSingle(BitConverter.GetBytes(leakmax), 0).ToString();//IEEE754 字节转换float
            UInt32 leakmin = Convert.ToUInt32(hexstring_llow, 16);//字符串转16进制32位无符号整数
            ch_params.Leaklowlimit = BitConverter.ToSingle(BitConverter.GetBytes(leakmin), 0).ToString();//IEEE754 字节转换float
            UInt32 evol = Convert.ToUInt32(hexstringg_evol, 16);//字符串转16进制32位无符号整数
            ch_params.Evolume = BitConverter.ToSingle(BitConverter.GetBytes(evol), 0).ToString();//IEEE754 字节转换float

            int a = Convert.ToInt32(full + balan + test + exhaust);
            if (a > 50)
            {
                ch_params.progressBar_value = (Convert.ToInt32(full + balan + test + exhaust) - 2) * 10;
            }
            else
            {
                ch_params.progressBar_value = (Convert.ToInt32(full + balan + test + exhaust) - 1) * 10;
            }

            //读取单位
            string hexstring_lunit = text.Substring(114, 4);
            string hexstring_punit = text.Substring(118, 4);

            int punit = Int32.Parse(hexstring_lunit, System.Globalization.NumberStyles.HexNumber);
            int lunit = Int32.Parse(hexstring_punit, System.Globalization.NumberStyles.HexNumber);
            ch_params.PUnit_index = punit;
            ch_params.LUnit_index = lunit;
            switch (punit)
            {
                case 0:
                    ch_params.PUnit = "Pa";
                    break;
                case 1:
                    ch_params.PUnit = "KPa";
                    break;
                case 2:
                    ch_params.PUnit = "MPa";
                    break;
                case 3:
                    ch_params.PUnit = "bar";
                    break;
                case 4:
                    ch_params.PUnit = "Psi";
                    break;
                case 5:
                    ch_params.PUnit = "kg/cm^2";
                    break;
                case 6:
                    ch_params.PUnit = "atm";
                    break;
                case 7:
                    ch_params.PUnit = "mmHg";
                    break;
            }
            switch (lunit)
            {
                case 0:
                    ch_params.LUnit = "Pa";
                    break;
                case 1:
                    ch_params.LUnit = "KPa";
                    break;
                case 2:
                    ch_params.LUnit = "mbar";
                    break;
                case 3:
                    ch_params.LUnit = "sccm";
                    break;
                case 4:
                    ch_params.LUnit = "ccm/s";
                    break;
                case 5:
                    ch_params.LUnit = "Pa/s";
                    break;
            }
            ch_params.CHKUnit = chkunit;

            return ch_params;
        }

        public Model.CH_Result ReadLeak(string text)
        {
            Model.CH_Result ch_result = new Model.CH_Result();
            string bigleak1 = text.Substring(10, 4);
            string bigleak2 = text.Substring(14, 4);
            string bleak = bigleak2 + bigleak1;
            string sl1 = text.Substring(22, 4);
            string sl2 = text.Substring(26, 4);
            string sleak = sl2 + sl1;
            string lp1 = text.Substring(98, 4);
            string lp2 = text.Substring(102, 4);
            string leakpre = lp2 + lp1;

            string LeakPressure = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(leakpre, 16)), 0).ToString("F2");//IEEE754 字节转换float
            double leakpress = Convert.ToDouble(LeakPressure);
            if (leakpress < 0)
            {
                leakpress = 0;
            }
            ch_result.LeakPressure = leakpress.ToString("F2");
            ch_result.BigLeak = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(bleak, 16)), 0).ToString("F2");//IEEE754 字节转换float
            ch_result.SmallLeak = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(sleak, 16)), 0).ToString("F2");//IEEE754 字节转换float

            string hexstring_result1 = text.Substring(34, 4);
            int result = Convert.ToInt32(hexstring_result1, 16);
            if (result == 1)
            {
                ch_result.Result = "OK";
            }
            else if (result == 2)
            {
                ch_result.Result = "NG";
            }
            else
            {
                ch_result.Result = "";
            }
            //string hexstring_result2 = text.Substring(36, 2);
            //byte[] re1 = System.BitConverter.GetBytes(Convert.ToUInt32(hexstring_result1, 16));
            //string result1 = System.Text.ASCIIEncoding.ASCII.GetString(re1);
            //byte[] re2 = System.BitConverter.GetBytes(Convert.ToUInt32(hexstring_result2, 16));
            //string result2 = System.Text.ASCIIEncoding.ASCII.GetString(re2);
            //ch_result.Result += result1;

            return ch_result;
        }

    }
}
