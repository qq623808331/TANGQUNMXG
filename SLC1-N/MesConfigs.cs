using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC1_N
{
    internal class MesConfigs
    {

        public class Main
        {
            public string LineNum { get; set; }
            public string BarCode { get; set; }
            public string TranBarCode { get; set; }
            public string BindBarCode { get; set; }
            public string DeviceCode { get; set; }
            public string LineCode { get; set; }
            public string CommandCard { get; set; }
            public string ProcessCode { get; set; }
            public string NGCode { get; set; }
            public int IsOnlyRecord { get; set; }
            public int IsPass { get; set; }
            public string CheckBy { get; set; }
            public string CheckTime { get; set; }
        }

        public class Detail
        {
            public string ItemName { get; set; }
            public double UpperLimits { get; set; }
            public double LowerLimits { get; set; }
            public double StandardVal { get; set; }
            public double ActualVal { get; set; }
            public string SoftVer { get; set; }
            public string CheckTime { get; set; }
        }
        public class Detail3
        {
            public string DeviceCode { get; set; }
            public string LineCode { get; set; }
            public int StatusCode { get; set; }
            public string CheckTime { get; set; }
        }
        public class Detail2
        {
            public string ItemName { get; set; }
            public double UpperLimits { get; set; }
            public double LowerLimits { get; set; }
            public double StandardVal { get; set; }
            public double ActualVal { get; set; }
            public string SoftVer { get; set; }
            public string CheckTime { get; set; }
        }

        public class Root
        {
            public Main Main { get; set; }
            public List<Detail> Detail { get; set; }


        }


        public class state
        {
            public string DeviceCode { get; set; }
            public string LineCode { get; set; }
            public string CreateTime { get; set; }
        }
    }
}

