using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static SLC1_N.Model;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO.Ports;
using static SLC1_N.Setup;
using System.Security.Policy;
using Microsoft.Office.Interop.Excel;
using Sunny.UI.Win32;
using System.Timers;
namespace SLC1_N
{
    public partial class Form1 : Form
    {
        public static Form1 f1;
        private float X, Y;

        public int ch1stage;
        public int ch2stage;
        public int ch3stage;
        public int ch4stage;
        private int ch1write;
        private int ch2write;
        private int ch3write;
        private int ch4write;

        private Communication comm = new Communication();
        public PLCConnect plc = new PLCConnect();
        private Log log = new Log();

        //UDP广播获取仪器IP地址
        private static Socket sock;

        private static IPEndPoint iep1;
        private static byte[] data;
        private Thread UDPlisten;
        private string UDPRecvData;
        public bool SocketConn = true;

        //仪器的TCP地址
        public string localipaddress;

        public string ch1ipaddress;
        public string ch2ipaddress;
        public string ch3ipaddress;
        public string ch4ipaddress;

        //仪器建立socket通讯
        public FrmClient ch1client = new FrmClient();

        public FrmClient ch2client = new FrmClient();
        public FrmClient ch3client = new FrmClient();
        public FrmClient ch4client = new FrmClient();

        //测试参数
        public Model.CH_PARAMS ch1_1params = new Model.CH_PARAMS();

        public Model.CH_PARAMS ch1_2params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_1params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_2params = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch1_1leakparams = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch1_2leakparams = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_1leakparams = new Model.CH_PARAMS();
        public Model.CH_PARAMS ch2_2leakparams = new Model.CH_PARAMS();
        private Model.CH_Result left_ch1result = new Model.CH_Result();
        private Model.CH_Result left_ch2result = new Model.CH_Result();
        private Model.CH_Result right_ch1result = new Model.CH_Result();
        private Model.CH_Result right_ch2result = new Model.CH_Result();

        //存储设置
        public Setup.Save save = new Setup.Save();

        //读取参数
        public bool ch1readpara = false;

        public bool ch2readpara = false;
        public bool ch3readpara = false;
        public bool ch4readpara = false;

        //计算产品合格数
        public int CH1Product;

        public int CH1PassNum;
        public int CH1FailNum;
        public int CH2Product;
        public int CH2PassNum;
        public int CH2FailNum;

        //计算产品是否符合扫码设置数目
        public Setup.Code_Setting codesetting = new Setup.Code_Setting();

        //计算流量和压力所需的时间
        private long ch1fullstart;

        private long ch1fullend;
        private long ch2fullstart;
        private long ch2fullend;
        private long ch3fullstart;
        private long ch3fullend;
        private long ch4fullstart;
        private long ch4fullend;
        private long ch1pressstart;
        private long ch1pressend;
        private long ch2pressstart;
        private long ch2pressend;
        private long ch3pressstart;
        private long ch3pressend;
        private long ch4pressstart;
        private long ch4pressend;

        //电流电压上下限
        public Model.Electricity elec = new Model.Electricity();

        private double CH1ADC;
        private double CH2ADC;
        private double CH1VDC;
        private double CH2VDC;
        private bool JudgeCH1ADC;
        private bool JudgeCH2ADC;
        private double CH1ADCMax;
        private double CH2ADCMax;
        private double CH1VDCMax;
        private double CH2VDCMax;

        /// <summary>
        /// CH1当前执行的程序
        /// </summary>
        public string CH1RTStep="";

        public bool CH1Pump;
        public string CH2RTStep="";
        public bool CH2Pump;

        /// <summary>
        /// 设置的程序
        /// </summary>
        public List<string> CH1Order = new List<string>();

        public List<string> CH2Order = new List<string>();

        public List<string> CH1OrderTemp = new List<string>();

        public List<string> CH2OrderTemp = new List<string>();

        public List<string> ADOrder = new List<string>();
        public List<string> BEOrder = new List<string>();
        public List<string> CFOrder = new List<string>();

        private List<ValueClass> CH1ADCList = new List<ValueClass>();
        private List<ValueClass> CH1VDCList = new List<ValueClass>();
        private List<ValueClass> CH2ADCList = new List<ValueClass>();
        private List<ValueClass> CH2VDCList = new List<ValueClass>();

        /// <summary>
        /// 左工位程序当前步数
        /// </summary>
        private int CH1Step;

        /// <summary>
        /// 右工位程序当前步数
        /// </summary>
        private int CH2Step;

        private double CH1Q;
        private double CH2Q;
        private double CH3Q;
        private double CH4Q;
        private double CH1PressMax;
        private double CH2PressMax;
        private double CH3PressMax;
        private double CH4PressMax;
        private bool CH1flowtest;
        private bool CH2flowtest;
        private bool CH3flowtest;
        private bool CH4flowtest;
        //卡控启动的判断
        private bool plcch1lastsignal = false;
        private bool plcch2lastsignal = false;

        //bool CH2lastuAsignal = false;
        private bool ch1readresult;

        private bool ch2readresult;

        private bool ch1Start;
        private bool ch2Start;

        //上下充对比值
        public Model.Flow Flow = new Model.Flow();

        public double CH1lastpress;
        public double CH2lastpress;
        public double CH1lastelec;
        public double CH2lastelec;
        public double CH1cont_press;
        public double CH2cont_press;
        public double CH1cont_elec;
        public double CH2cont_elec;

        /// <summary>
        /// 记录上一次测试是什么模式，如果上下充交替则做对比值，否则不做对比;
        /// 0为上充，1为下充，2为同充，3为新的一轮
        /// </summary>
        private int CH1lastmodel = 3;

        /// <summary>
        /// 记录上一次测试是什么模式，如果上下充交替则做对比值，否则不做对比;
        /// 0为上充，1为下充，2为同充，3为新的一轮
        /// </summary>
        private int CH2lastmodel = 3;

        //MES路径和文件名
        public string mesfilename;

        public string mesfolderpath;
        public string Characters;
        private string CH1ADCresult;
        private string CH2ADCresult;
        private string CH1VDCresult;
        private string CH2VDCresult;
        private string CH1Elecresult;
        private string CH2Elecresult;
        public Model.TestResult CH1TestResult = new Model.TestResult();
        public Model.TestResult CH2TestResult = new Model.TestResult();
        private string CH1timestamp;
        private string CH2timestamp;

        ////Lin通讯
        //public int CH1LinBaudrate;
        //public int CH2LinBaudrate;
        //public Setup.LinConfig linconfig = new Setup.LinConfig();
        //LIN CH1lin;
        //LIN CH2lin;
        public LIN_LDFParser CH1lin;

        public LIN_LDFParser CH2lin;

        //机种名称
        public string machine;

        public string machinepath;
        public int CH1csvworknum = 1;
        public int CH2csvworknum = 1;
        public int CH1mesworknum = 1;
        public int CH2mesworknum = 1;
        public static      SerialPortReader CH1POWER=new SerialPortReader("COM17",9600,10);
        public static       SerialPortReader CH2POWER= new SerialPortReader("COM20", 9600, 10);

        public static SerialPortReader CH1ADC_PORT = new SerialPortReader("COM17", 9600, 10);
        public static SerialPortReader CH2ADC_PORT = new SerialPortReader("COM20", 9600, 10);
        //警告
        private WarningInfo wa = new WarningInfo();

        /// <summary>
        /// 仪器的数据发送,1是读取仪器启动状态，2是读取参数，3是读取测试结果,4是读取仪器复位状态，5是保持仪器通讯
        /// </summary>
        public int ch1_1step = 1;

        public int ch1_2step = 1;
        public int ch2_1step = 1;
        public int ch2_2step = 1;
        public Setup.LinConfig linconfig;

        //判断是不是刚打开软件，是的话不对结果做文件存储
        private bool CH1IsStart = false;

        private bool CH2IsStart = false;
        private bool CH1ResetCode = true;
        private bool CH2ResetCode = true;

        //判断csv是否需要写入标题
        private bool IsWriteTitle = false;

        private long ch1uAstarttime;
        private long ch2uAstarttime;
        private long ch1uAendtime;
        private long ch2uAendtime;
        private double CH1uAValue;
        private double CH2uAValue;
        private List<double> CH1uAarray = new List<double>();
        private List<double> CH2uAarray = new List<double>();

        private string CH1RunName = string.Empty;
        private string CH2RunName = string.Empty;

        private int CH1ReadElecCount = 0;
        private int CH2ReadElecCount = 0;

        private System.Timers.Timer timerCH1CT;
        private System.Timers.Timer timerCH2CT;

        public ModbusRtu busRtuClientCH1;
        public ModbusRtu busRtuClientCH2;
        public ModbusRtu busRtuClientCH3;
        public ModbusRtu busRtuClientCH4;

        public bool mSerialPort = true;

        /// <summary>
        /// 初始化Timer统计CT控件
        /// </summary>
        private void InitTimer()
        {
            //设置定时间隔(毫秒为单位)
            int interval = 300;
            timerCH1CT = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timerCH1CT.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timerCH1CT.Enabled = false;
            //绑定Elapsed事件
            timerCH1CT.Elapsed += new System.Timers.ElapsedEventHandler(TimerCT1);

            timerCH2CT = new System.Timers.Timer(interval);
            //设置执行一次（false）还是一直执行(true)
            timerCH2CT.AutoReset = true;
            //设置是否执行System.Timers.Timer.Elapsed事件
            timerCH2CT.Enabled = false;
            //绑定Elapsed事件
            timerCH2CT.Elapsed += new System.Timers.ElapsedEventHandler(TimerCT2);
        }

        private DateTime dtCT1;
        private DateTime dtCT2;

        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        private void TimerCT1(object sender, System.Timers.ElapsedEventArgs e)
        {
            double time = DiffSeconds(dtCT1, DateTime.Now);
            this.Invoke(new System.Action(() => { CH1CT.Text = time.ToString("F1") + "S"; }));
        }

        private void TimerCT2(object sender, System.Timers.ElapsedEventArgs e)
        {
            double time = DiffSeconds(dtCT2, DateTime.Now);
            this.Invoke(new System.Action(() => { CH2CT.Text = time.ToString("F1") + "S"; }));
        }

        public double DiffSeconds(DateTime startTime, DateTime endTime)
        {
            lock (this)
            {
                TimeSpan secondSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
                return secondSpan.TotalSeconds;
            }
        }

        /// <summary>
        /// 定时清理内存
        /// </summary>
        private static void SetTimer()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer(); //初始化定时器
            aTimer.Interval = 60 * 1000;//配置时间1分钟
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            aTimer.AutoReset = true;//每到指定时间Elapsed事件是到时间就触发
            aTimer.Enabled = true; //指示 Timer 是否应引发 Elapsed 事件。
        }

        //定时器触发的处理事件
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            if(Form1.f1.ONtime.Text!= ((int)stopwatch.Elapsed.TotalMinutes).ToString())
            Form1.f1.ONtime.Text=((int)stopwatch.Elapsed.TotalMinutes).ToString();

            //清理内存
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                //以下系统进程没有权限，所以跳过，防止出错影响效率。
                if ((process.ProcessName == "System") && (process.ProcessName == "Idle"))
                    continue;
                try
                {
                    EmptyWorkingSet(process.Handle);
                }
                catch { }
            }
        }

        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc); //清理内存相关
        /// <summary>
        /// ///////新加后台读取串口
        /// </summary>
        ///       
        public SerialPort ADCPort1, VDCPort1, CH2ADCPort, CH2VDCPort;
      
        private CancellationTokenSource _cancellationTokenSource;
        public Form1()
        {
            //dllCall.RegistDLL();
            InitializeComponent();
            this.Shown += Form1_Shown;
            f1 = this;
        }
        private static void CH1POWER_DataReceived(string data)
        {
                try
                {
                double data2;
                 
                //string[] strArray = data.ToString().Split(' ');
                //double number1 = Convert.ToDouble(strArray[0].ToString());
                //string numberFromStringFormat = string.Format("{0:F5}", number1);
                if (double.TryParse(data, System.Globalization.NumberStyles.Float,System.Globalization.NumberFormatInfo.InvariantInfo, out data2))
                {
                     if(data2<1)
                    {
                        Form1.f1.CH1RTADC.Text = data2.ToString();
                        Form1.f1.CH1ADCList.Add(new ValueClass { Value = data2 });
                    }
                   
                 
                }
                ;
                 }
                catch (Exception ex)
                {
                    Logger.Log(ex.StackTrace);
                    throw;
                }
      
        
        }
        private static void CH2POWER_DataReceived(string data)
        {
            try
            {
                double data2;

                //string[] strArray = data.ToString().Split(' ');
                //double number1 = Convert.ToDouble(strArray[0].ToString());
                //string numberFromStringFormat = string.Format("{0:F5}", number1);
                if (double.TryParse(data, System.Globalization.NumberStyles.Float, System.Globalization.NumberFormatInfo.InvariantInfo, out data2))
                {
                    if (data2 < 1)
                    {
                        Form1.f1.CH2RTADC.Text = data2.ToString();
                        Form1.f1.CH2ADCList.Add(new ValueClass { Value = data2 });
                    }
                }
                    
                
            ;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.StackTrace);
                throw;
            }


        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ChangeLanguage(I18N.Language);
            if (I18N.Language == "en-Us") this.LoadENUSLang();
            else this.LoadZHCNLang();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;

            CH1Tlight.Text = "";
            CH2Tlight.Text = "";

            {
                plc.WriteCH1SC(false);
                plc.WriteCH1XC(false);
                plc.WriteCH1TC(false);
                plc.WriteCH1XQ(false);
                plc.WriteCH2SC(false);
                plc.WriteCH2XC(false);
                plc.WriteCH2TC(false);
                plc.WriteCH2XQ(false);


                plc.WriteCH1QC(false);
                plc.WriteCH2QC(false);
            }

            ADCPort1 = new SerialPort { };
            VDCPort1 = new SerialPort { };
            CH2ADCPort = new SerialPort { };
            CH2VDCPort = new SerialPort { };
            ReadMultimeterPort();
            ReadFlowTask();
            Task.Run(() =>
            {
                while (true)
                {

                    try
                    {
                        if (true && mSerialPort)
                        {
                            CH1POWER.Write("MEASure:CURRent?");
                            Thread.Sleep(400);

                               CH2POWER.Write("MEASure:CURRent?");
                            Thread.Sleep(400);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.StackTrace);
                        wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", ex.StackTrace);
                    }
                }
            });
            //后台电压电流读取线程
            Task.Run(() =>
            {
                
                
                while (true && mSerialPort)
                {
                    try
                    {
                        if (true && mSerialPort)
                        {
                            try
                            {
                                ReadMultimeterPort();
                                Thread.Sleep(100);
                                PortSend(1);
                                Thread.Sleep(300);
                                PortSend(3);
                                Thread.Sleep(300);
                                PortSend(2);
                                Thread.Sleep(300);
                                PortSend(4);
                                Thread.Sleep(300);
                            }
                            catch (Exception ex)
                            {
                                Logger.Log(ex.StackTrace);
                                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", ex.StackTrace);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.StackTrace);
                        wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", ex.StackTrace);
                    }
                }
            });
         

        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        //窗口自适应分辨率
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }

        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * newy;
                con.Font = new System.Drawing.Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
        }

        //窗口改变尺寸事件
        private void Form1_Resize(object sender, EventArgs e)
        {
            // throw new Exception("The method or operation is not implemented.");
            float newx = (this.Width) / X;
            // float newy = (this.Height - this.statusStrip1.Height) / (Y - y);
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            //      this.Text = this.Width.ToString() + " " + this.Height.ToString();
        }
        private static Stopwatch stopwatch;
        private static System.Timers.Timer timer13;
        private void Form1_Load(object sender, EventArgs e)
        {
            ///////新加串口初始化
            ///
         

            ////
            //Sunny.UI.UIMessageTip.ShowError(                                                                                                                                                                                                                                                                                                                                                                                          "AAAAA", 1000);
            //this.TopMost = true;
            timer1.Interval = 100;
            timer1.Start();
            string lang = I18N.Language;
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;

            ch1client.FrmClient_Load(1);
            ch2client.FrmClient_Load(2);
            ch3client.FrmClient_Load(3);
            ch4client.FrmClient_Load(4);
            //窗口自适应分辨率
            // this.Resize += new EventHandler(Form1_Resize);//Eric
            X = this.Width;
            Y = this.Height;
            // y = this.statusStrip1.Height;
            // setTag(this);//Eric

            //界面初始化
            //CH1ProductNumber.Text = "未连接";
            //CH1ProductNumber.ForeColor = Color.Red;
            Admin.Text = "";
            //Left_Mac_Status.Text = "待机中";
            LeftCH1SmallLeak.Text = " ";
            LeftCH1BigLeak.Text = " ";
            LeftCH1LeakPress.Text = " ";
            left_CH1Tlight.Text = " ";
            LeftCH2SmallLeak.Text = " ";
            LeftCH2BigLeak.Text = " ";
            LeftCH2LeakPress.Text = " ";
            left_CH2Tlight.Text = " ";
            RightCH1SmallLeak.Text = " ";
            RightCH1BigLeak.Text = " ";
            RightCH1LeakPress.Text = " ";
            right_CH1Tlight.Text = " ";
            RightCH2SmallLeak.Text = " ";
            RightCH2BigLeak.Text = " ";
            RightCH2LeakPress.Text = " ";
            right_CH2Tlight.Text = " ";
            CH1Tlight.Text = " ";
            CH2Tlight.Text = " ";
            CH1RTADC.Text = "";
            CH1RTVDC.Text = "";
            CH2RTADC.Text = "";
            CH2RTVDC.Text = "";
            CH1RTElec.Text = "";
            CH2RTElec.Text = "";
            CH1_1flow.Text = "";
            CH1_2flow.Text = "";
            CH2_1flow.Text = "";
            CH2_2flow.Text = "";
            CH1_1FullPress.Text = "";
            CH1_2FullPress.Text = "";
            CH2_1FullPress.Text = "";
            CH2_2FullPress.Text = "";
            //CH1uAStatus.Visible = false;
            //CH2uAStatus.Visible = false;
            //CH1FlowPress.Text = "";
            //CH2FlowPress.Text = "";
            CH1ParamIndex.Text = "";
            CH2ParamIndex.Text = "";

            LogOn logon = new LogOn();
            logon.ShowDialog();
            //Machine mac = new Machine(Characters);
            //mac.ShowDialog();
            SelectMachine mac = new SelectMachine();
            mac.ShowDialog();
             
            stopwatch = new Stopwatch();
            stopwatch.Start();

            // 初始化 Timer
            //timer13 = new System.Timers.Timer(1000); // 每1000毫秒（1秒）触发一次
            //timer13.Elapsed += OnTimedEvent;
            //timer13.AutoReset = true; // 设置为自动重置
            //timer13.Enabled = true; // 启动计时器

            ReadFlow();
            ReadLin();
            ReadAllConfig();
            UDPBroadcast();
            PLC_Con();
            log.DeleteFile("CH1Port_Logmsg");
            log.DeleteFile("CH2Port_Logmsg");
            log.DeleteFile("CH1FlowPort_Logmsg");
            log.DeleteFile("CH2FlowPort_Logmsg");
            log.DeleteFile("CH3FlowPort_Logmsg");
            log.DeleteFile("CH4FlowPort_Logmsg");
            log.DeleteFile("PLC_Log");
            log.DeleteFile("TCP_Log");

            //定时GC
            SetTimer();
            //统计CT
            InitTimer();
            CH1IsRun.Interval = 500;
        }


                    [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public const int WM_CLOSE = 0x10;


     
                    /// <summary>
                    /// PLC连接
                    /// </summary>
                    public void PLC_Con()
        {
            try
            {
                //plc.PLC_Connect();
                System.Threading.Thread.Sleep(500);
                bool Isconnect = plc.PLC_IsCon();
                if (Isconnect)
                {
                    PLCSignal.Interval = 200;
                    PLCSignal.Start();
                }
                else
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "PLC通讯失败,连接不上"));
                    PLCRun.BackColor = Color.Red;
                    Logger.Log(I18N.GetLangText(dicLang, "PLC通讯失败,连接不上"));
                    //MessageBox.Show("PLC通讯失败！");
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "PLC连接") + ex.Message);
                PLCRun.BackColor = Color.Red;
                //MessageBox.Show("PLC连接:" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// PLC循环发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PLCSignal_Tick_1(object sender, EventArgs e)
        {
            try
            {

                ///////
                ///if
                ///
                plc.PLC_IsRun();
                if (Fwdjg == 1 && Fwdjg2 == 1 && left_CH1Tlight.Text.Contains("OK") && left_CH2Tlight.Text.Contains("OK"))
                {
                    Fwdjg = 0;
                    Fwdjg2 = 0;
                    //    Thread.Sleep(100);
                    CH1Step += 1;
                    CH1Method(CH1Step);
                }
                if (CH2JGFLAG == 2 && right_CH1Tlight.Text.Contains("OK") && right_CH2Tlight.Text.Contains("OK"))
                {
                    CH2JGFLAG = 0;
                    //Thread.Sleep(100);
                    CH2Step += 1;
                    CH2Method(CH2Step);
                }

                //PLCSignal.Stop();
          
                System.Threading.Thread.Sleep(200);
                if (plc.PLCIsRun)
                {
                    ReadPLC();
                    PLCRun.BackColor = Color.Green;
                    if (plc.Front_SafetyDoor)
                    {
                        log.PLC_Logmsg("Signal:  " + "Front_SafetyDoor:" + plc.Front_SafetyDoor);
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "前侧安全门");
                        if (ptr == IntPtr.Zero)
                        {
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "前侧安全门打开，前侧安全门"));
                            //MessageBox.Show("前侧安全门打开！", "前侧安全门");
                            Logger.Log(I18N.GetLangText(dicLang, "前侧安全门打开，前侧安全门"));
                        }
                    }

                    if (plc.Back_SafetyDoorUp)
                    {
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "后侧上方安全门");
                        if (ptr == IntPtr.Zero)
                        {
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "后侧上方安全门打开，后侧上方安全门"));
                            //MessageBox.Show("后侧上方安全门打开！", "后侧上方安全门");
                            Logger.Log(I18N.GetLangText(dicLang, "后侧上方安全门打开，后侧上方安全门"));
                        }
                    }
                    if (plc.Back_SafetyDoorDown)
                    {
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "后侧下方安全门");
                        if (ptr == IntPtr.Zero)
                        {
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "后侧下方安全门打开， 后侧下方安全门"));
                            //MessageBox.Show("后侧下方安全门打开！", "后侧下方安全门");
                            Logger.Log(I18N.GetLangText(dicLang, "后侧下方安全门打开， 后侧下方安全门"));
                        }
                    }
                    if (plc.Left_SafetyDoor)
                    {
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "左侧安全门打开");
                        if (ptr == IntPtr.Zero)
                        {
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "左侧安全门打开，左侧安全门"));
                            //       wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "左侧安全门打开，左侧安全门"));
                            //MessageBox.Show("左侧安全门打开！", "左侧安全门");
                            Logger.Log(I18N.GetLangText(dicLang, "左侧安全门打开，左侧安全门"));
                            //Logger.Log(I18N.GetLangText(dicLang, "左侧安全门打开，左侧安全门"));
                        }
                    }
                    if (plc.Right_SafetyDoor)
                    {
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "右侧安全门");
                        if (ptr == IntPtr.Zero)
                        {
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "右侧安全门打开，右侧安全门"));
                            // wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "右侧安全门打开，右侧安全门"));
                            // MessageBox.Show("！", "右侧安全门");
                            Logger.Log(I18N.GetLangText(dicLang, "右侧安全门打开，右侧安全门"));
                        }
                    }
                    if (plc.CH1Stopping)
                    {
                        Reset(1);
                        //Reset(2);
                        //CH2LinUP.Stop();
                        IntPtr ptr = FindWindow(null, "CH1急停");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "左工位急停按下，CH1急停"));
                            plc.CH1StoppingFalse();
                            //DialogResult Reset = MessageBox.Show("左工位急停按下！", "CH1急停", MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH1StoppingFalse();
                            //}
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "左工位急停按下，CH1急停"));
                        }
                    }
                    if (plc.CH2Stopping)
                    {
                        //Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "CH2急停");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "左工位急停按下，CH1急停"));
                            plc.CH2StoppingFalse();
                            //DialogResult Reset = MessageBox.Show("右工位急停按下！", "CH2急停", MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH2StoppingFalse();
                            //}
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "左工位急停按下，CH1急停"));
                        }
                    }
                    if (plc.Stopping)
                    {
                        Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "总急停");
                        if (ptr == IntPtr.Zero)
                        {
                            // MessageBox.Show("总急停按下！", "总急停");
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "总急停按下，总急停"));
                            Logger.Log(I18N.GetLangText(dicLang, "总急停按下，总急停"));
                        }
                    }
                    if (plc.CH1Reset)
                    {
                        Reset(1);


                        //Reset(2);
                        IntPtr ptr = FindWindow(null, "左复位");
                        if (ptr == IntPtr.Zero)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.Focus();
                            {
                                timerCH1CT.Stop();
                                CH1IsStart = false;
                                if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                                this.Invoke(new System.Action(() => { CH1CT.Text = "0S"; }));
                            }
                            // MessageBox.Show("左复位按下！", "左复位");
                            Logger.Log(I18N.GetLangText(dicLang, "左复位按下，左复位"));
                            this.logDisplay1.listBox1.Items.Clear();
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "左复位按下，左复位"));
                        }
                    }
                    if (plc.CH2Reset)
                    {
                        //Reset(1);
                        Reset(2);
                        IntPtr ptr = FindWindow(null, "右复位");
                        if (ptr == IntPtr.Zero)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.Focus();
                            {
                                timerCH2CT.Stop();
                                CH2IsStart = false;

                             //   if (CKCH2Port.IsOpen) Form1.f1.CKCH2Port.WriteLine("OUTP 0");
                                CH2POWER.Write("OUTP 0");
                                this.Invoke(new System.Action(() => { CH2CT.Text = "0S"; }));
                            }
                            //MessageBox.Show("右复位按下！", "右复位");
                            Logger.Log(I18N.GetLangText(dicLang, "右复位按下，右复位"));
                            this.logDisplay1.listBox1.Items.Clear();
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "右复位按下，右复位"));
                        }
                    }
                    if (plc.CH1ResetFinish)
                    {
                        IntPtr ptr = FindWindow(null, "左复位");
                        if (ptr != IntPtr.Zero)
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH2ResetFinish)
                    {
                        IntPtr ptr = FindWindow(null, "右复位");
                        if (ptr != IntPtr.Zero)
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH1NeedReset)
                    {
                        //IntPtr ptr = FindWindow(null, "CH1请复位");
                        //if (ptr == IntPtr.Zero)
                        //{
                        //    Logger.Log(I18N.GetLangText(dicLang, "左工位请复位，CH1请复位"));
                        //    DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "左工位请复位，CH1请复位"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                        //    if (Reset == DialogResult.OK)
                        //    {
                        //        plc.CH1NeedResetFALSE();
                        //    }
                        //}
                        Logger.Log(I18N.GetLangText(dicLang, "左工位请复位，CH1请复位"));
                        plc.CH1NeedResetFALSE();
                    }
                    if (plc.CH2NeedReset)
                    {
                        //IntPtr ptr = FindWindow(null, "CH2请复位");
                        //if (ptr == IntPtr.Zero)
                        //{
                        //    Logger.Log(I18N.GetLangText(dicLang, "右工位请复位，CH2请复位"));
                        //    DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "右工位请复位，CH2请复位"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                        //    if (Reset == DialogResult.OK)
                        //    {
                        //        plc.CH2NeedResetFALSE();
                        //    }
                        //}

                        Logger.Log(I18N.GetLangText(dicLang, "右工位请复位，CH2请复位"));
                        plc.CH2NeedResetFALSE();
                    }
                    if (plc.CH1SRCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH1滑轨气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH1滑轨气缸异常！", "CH1滑轨气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH1滑轨气缸异常，CH1滑轨气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "CH1滑轨气缸异常，CH1滑轨气缸"));
                        }
                    }
                    if (plc.CH2SRCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH2滑轨气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH2滑轨气缸异常！", "CH2滑轨气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH2滑轨气缸异常，CH2滑轨气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "CH2滑轨气缸异常，CH2滑轨气缸"));
                        }
                    }
                    if (plc.CH1SPCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH1侧推气缸");
                        if (ptr == IntPtr.Zero)
                        {
                           //MessageBox.Show("CH1侧推气缸异常！", "CH1侧推气缸");
                       //     Logger.Log(I18N.GetLangText(dicLang, "CH1侧推气缸异常，CH1侧推气缸"));
                        //    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "CH1侧推气缸异常，CH1侧推气缸"));
                        }
                    }
                    if (plc.CH2SPCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH2侧推气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH2侧推气缸异常！", "CH2侧推气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH2侧推气缸异常，CH2侧推气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "CH2侧推气缸异常，CH2侧推气缸"));
                        }
                    }
                    if (plc.CH1FNCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH1飞针气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH1飞针气缸异常！", "CH1飞针气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH1飞针气缸异常，CH1飞针气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "CH1飞针气缸异常，CH1飞针气缸"));
                        }
                    }
                    if (plc.CH2FNCylinderError)
                    {
                        IntPtr ptr = FindWindow(null, "CH2飞针气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH2飞针气缸异常！", "CH2飞针气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH2飞针气缸异常，CH2飞针气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "CH2飞针气缸异常，CH2飞针气缸"));
                        }
                    }
                    if (plc.CH1PCylinderUPError)
                    {
                        IntPtr ptr = FindWindow(null, "CH1充气气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH1充气气缸异常！", "CH1充气气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH1充气气缸异常，CH1充气气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "CH1充气气缸异常，CH1充气气缸"));
                        }
                    }
                    if (plc.CH2PCylinderUPError)
                    {
                        IntPtr ptr = FindWindow(null, "CH2充气气缸");
                        if (ptr == IntPtr.Zero)
                        {
                            //MessageBox.Show("CH2充气气缸异常！", "CH2充气气缸");
                            Logger.Log(I18N.GetLangText(dicLang, "CH2充气气缸异常，CH2充气气缸"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "CH2充气气缸异常，CH2充气气缸"));
                        }
                    }
                    if (plc.CH1CodeStart)
                    {
                        IntPtr ptr = FindWindow(null, "CH1条码启动");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"));
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //     if (Reset == DialogResult.OK)
                            {
                                plc.CH1codestartFalse();
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }

                    if (plc.CH2CodeStart)
                    {
                        IntPtr ptr = FindWindow(null, "CH2条码启动");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"));
                            // DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //  if (Reset == DialogResult.OK)
                            {
                                plc.CH2codestartFalse();
                            }
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH1ProductError)
                    {
                        IntPtr ptr = FindWindow(null, "CH1物料");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "左工位物料未感应到，CH1物料"));
                            plc.CH1ProductFalse();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "左工位物料未感应到，CH1物料"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH1ProductFalse();
                            //}
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH2ProductError)
                    {
                        IntPtr ptr = FindWindow(null, "CH2物料");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "右工位物料未感应到，CH2物料"));
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "右工位物料未感应到，CH2物料"));
                            plc.CH2ProductFalse();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "右工位物料未感应到，CH2物料"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH2ProductFalse();
                            //}
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH1SafetyGrating)
                    {
                        IntPtr ptr = FindWindow(null, "CH1安全光栅");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "左工位安全光栅触发，CH1安全光栅"));
                            plc.CH1SafetyGratingFlase();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "左工位安全光栅触发，CH1安全光栅"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH1SafetyGratingFlase();
                            //}
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "左"), I18N.GetLangText(dicLang, "左工位安全光栅触发，CH1安全光栅"));
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.CH2SafetyGrating)
                    {
                        IntPtr ptr = FindWindow(null, "CH2安全光栅");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "右工位安全光栅触发，CH2安全光栅"));
                            plc.CH2SafetyGratingFlase();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "右工位安全光栅触发， CH2安全光栅"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.CH2SafetyGratingFlase();
                            //}
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), I18N.GetLangText(dicLang, "右"), I18N.GetLangText(dicLang, "右工位安全光栅触发，CH2安全光栅"));
                        }
                        else
                        {
                            PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                        }
                    }
                    if (plc.PressureWarning)
                    {
                        IntPtr ptr = FindWindow(null, "气压报警");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "气压报警，气压报警"));
                            plc.PressWarning();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "气压报警，气压报警"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.PressWarning();
                            //}
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "气压报警，气压报警"));
                        }
                    }
                    if (plc.AutoModel)
                    {
                        IntPtr ptr = FindWindow(null, "自动状态");
                        if (ptr == IntPtr.Zero)
                        {
                            Logger.Log(I18N.GetLangText(dicLang, "自动状态下禁止手动，自动状态"));
                            plc.AutoModelFalse();
                            //DialogResult Reset = MessageBox.Show(I18N.GetLangText(dicLang, "自动状态下禁止手动，自动状态"), I18N.GetLangText(dicLang, "通知"), MessageBoxButtons.OK);
                            //if (Reset == DialogResult.OK)
                            //{
                            //    plc.AutoModelFalse();
                            //}
                        }
                    }
                    if (CH1ResetCode)
                    {
                        CH1ResetCode = false;
                        if (plc.CH1CodeCount <= 0)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.Focus();
                        }
                    }
                    if (CH2ResetCode)
                    {
                        CH2ResetCode = false;
                        if (plc.CH2CodeCount <= 0)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.Focus();
                        }
                    }

                    if ((plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun) & !CH1IsStart)
                    {
                        

                        ch1Start = true;
                        this.logDisplay1.listBox1.Items.Clear();
                        mSerialPort = true;
                        CH1ADC = 0;
                        CH1VDC = 0;
                        if (CH1IsStart == false)
                        {
                            DataGridView1.ClearRows();
                            CH1progressBar.Value = 0;
                            CH2progressBar.Value = 0;
                            CH1progressBar.Maximum = 100;
                            CH2progressBar.Maximum = 100;
                            CH1IsStart = true;
                            {
                                dtCT1 = DateTime.Now;
                                timerCH1CT.Start();
                                if (Form1.CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                                plcch1lastsignal = (plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun);
                            }
                            CH1Status.Text = I18N.GetLangText(dicLang, "测试");
                            CH1Status.ForeColor = Color.Green;
                            ch1readresult = false;
                            CH1Tlight.Text = "";
                            LeftCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                            LeftCH1Status.ForeColor = Color.Black;
                            left_CH1Tlight.Text = "";
                            LeftCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                            LeftCH2Status.ForeColor = Color.Black;
                            left_CH2Tlight.Text = "";
                            CH1_1flow.Text = "";
                            CH1_2flow.Text = "";
                            LeftCH1LeakPress.Text = "";
                            LeftCH1BigLeak.Text = "";
                            LeftCH1SmallLeak.Text = "";
                            CH1_1FullPress.Text = "";
                            LeftCH2LeakPress.Text = "";
                            LeftCH2BigLeak.Text = "";
                            LeftCH2SmallLeak.Text = "";
                            CH1_2FullPress.Text = "";
                        }

                        CH1RunName = "";

                        CH1OrderTemp = CH1Order;
                        if (plc.CH1ARun)
                        {
                            CH1RunName = I18N.GetLangText(dicLang, "A通道");
                            CH1OrderTemp = ADOrder;
                        }
                        if (plc.CH1BRun)
                        {
                            CH1RunName = I18N.GetLangText(dicLang, "B通道");
                            CH1OrderTemp = BEOrder;
                        }
                        if (plc.CH1CRun)
                        {
                            CH1RunName = I18N.GetLangText(dicLang, "C通道");
                            CH1OrderTemp = CFOrder;
                        }
                    }
                    else if (!CH1IsStart)
                    {
                        CH1Status.Text = I18N.GetLangText(dicLang, "待机");
                        CH1Status.ForeColor = Color.Black;

                        //LeftCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                        //LeftCH1Status.ForeColor = Color.Black;
                        //LeftCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                        //LeftCH2Status.ForeColor = Color.Black;
                    }

                    if ((plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun) & !CH2IsStart)
                    {
                       
                        ch2Start = true;
                        this.logDisplay1.listBox1.Items.Clear();
                        mSerialPort = true;
                        CH2ADC = 0;
                        CH2VDC = 0;
                        if (CH2IsStart == false)
                        {
                            DataGridView2.ClearRows();
                            CH3progressBar.Value = 0;
                            CH4progressBar.Value = 0;
                            CH3progressBar.Maximum = 100;
                            CH4progressBar.Maximum = 100;
                            CH2IsStart = true;
                            {
                                dtCT2 = DateTime.Now;
                                timerCH2CT.Start();
                                // if (CKCH2Port.IsOpen)   if (CKCH2Port.IsOpen)   Form1.f1.CKCH2Port.WriteLine("OUTP 1");
                            }
                            CH2Status.Text = I18N.GetLangText(dicLang, "测试");
                            plcch2lastsignal = (plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun);
                            CH2Status.ForeColor = Color.Green;
                            CH2Tlight.Text = "";
                            ch2readresult = false;
                            RightCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                            RightCH1Status.ForeColor = Color.Green;
                            right_CH1Tlight.Text = "";
                            RightCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                            RightCH2Status.ForeColor = Color.Green;
                            right_CH2Tlight.Text = "";
                            CH2_1flow.Text = "";
                            CH2_2flow.Text = "";
                            RightCH1LeakPress.Text = "";
                            RightCH1BigLeak.Text = "";
                            RightCH1SmallLeak.Text = "";
                            CH2_1FullPress.Text = "";
                            RightCH2LeakPress.Text = "";
                            RightCH2BigLeak.Text = "";
                            RightCH2SmallLeak.Text = "";
                            CH2_2FullPress.Text = "";
                        }
                        CH2RunName = "";
                        CH2OrderTemp = CH2Order;
                        if (plc.CH2DRun)
                        {
                            CH2RunName = I18N.GetLangText(dicLang, "D通道");
                            CH2OrderTemp = ADOrder;
                        }
                        if (plc.CH2ERun)
                        {
                            CH2RunName = I18N.GetLangText(dicLang, "E通道");
                            CH2OrderTemp = BEOrder;
                        }
                        if (plc.CH2FRun)
                        {
                            CH2RunName = I18N.GetLangText(dicLang, "F通道");
                            CH2OrderTemp = CFOrder;
                        }
                    }
                    else if (!CH2IsStart)
                    {
                        CH2Status.Text = I18N.GetLangText(dicLang, "待机");
                        CH2Status.ForeColor = Color.Black;

                        //RightCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                        //RightCH1Status.ForeColor = Color.Green;
                        //RightCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                        //RightCH2Status.ForeColor = Color.Green;
                    }

                    if ((plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun) & CH1IsStart && CH1OrderTemp.Count > 0)
                    {
                        if ((plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun) && plcch1lastsignal)
                        {
                               CH1Method(0);
                                            CH1Step = 0;
                           //数据清零
                            DateZero();
                          
                            plc.CH1Rset();
                        }
                    }
                    else
                    {
                        plcch1lastsignal = false;
                    }
                    if ((plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun) & CH2IsStart && CH2OrderTemp.Count > 0)
                    {
                        if ((plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun) && plcch2lastsignal)
                        {
                            CH2Method(0);
                            DateZero2();
                            CH2Step = 0;
                            plc.CH2Rset();
                        }
                    }
                    else
                    {
                        plcch2lastsignal = false;
                    }

                    if (plc.CH1NG)
                    {
                        //if (ch1readresult != true)
                        if (CH1IsStart && ch1Start)
                        {
                            ch1readresult = true;
                            CH1Tlight.Text = "NG";
                            log.MES_Logmsg("jinru");
                            CreateFile(1);
                            CH1Tlight.ForeColor = Color.Red;
                            {
                                plc.WriteCH1SC(false);
                                plc.WriteCH1XC(false);
                                plc.WriteCH1TC(false);
                                plc.WriteCH1XQ(false);
                                Thread.Sleep(50);
                            }

                            {
                                timerCH1CT.Stop();
                                CH1progressBar.Value = CH1progressBar.Maximum;
                                CH2progressBar.Value = CH2progressBar.Maximum;
                                //CH1progressBar.BackColor = Color.Red;
                                //CH2progressBar.BackColor = Color.Red;
                                CH1IsStart = false;
                                if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                            }

                            //产品计数
                            CH1Product += 1;
                            CH1FailNum += 1;
                            WriteCH1ProductCount();
                            CH1ProductNumber.Text = CH1Product.ToString();
                            //CH1PassNumber.Text = CH1PassNum.ToString();
                            CH1FailNumber.Text = CH1FailNum.ToString();
                            CH1PassRate.Text = (Math.Round((decimal)CH1PassNum / CH1Product, 2) * 100).ToString() + "%";

                            if (save.ChkMES)
                            {
                                AddMES(1);
                            }


                            CHXProBarFlag[1] = 0;
                            CHXProBarFlag[2] = 0;

                            CH1LinUP.Stop();
                            CH1ReadFlowT.Stop();
                            CH1ReadPress.Stop();
                            CH2ReadFlowT.Stop();
                            CH2ReadPress.Stop();
                            JudgeCH1ADC = false;
                        }
                    }
                    else if (plc.CH1OK && !CH1IsStart)
                    {
                        if (!ch1readresult && ch1Start)
                        {
                            ch1readresult = true;
                            CH1Tlight.Text = "OK";
                            CreateFile(1);
                            log.MES_Logmsg(DateTime.Now.ToString() + "jinruliucheng");
                            CH1Tlight.ForeColor = Color.Green;
                            {
                                plc.WriteCH1SC(false);
                                plc.WriteCH1XC(false);
                                plc.WriteCH1TC(false);
                                plc.WriteCH1XQ(false);
                                Thread.Sleep(50);
                            }
                            {
                                timerCH1CT.Stop();
                                CH1progressBar.Value = CH1progressBar.Maximum;
                                CH2progressBar.Value = CH2progressBar.Maximum;
                                CH1IsStart = false;
                                if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                            }
                            //产品计数
                            CH1Product += 1;
                            CH1PassNum += 1;
                            WriteCH1ProductCount();
                            CH1ProductNumber.Text = CH1Product.ToString();
                            CH1PassNumber.Text = CH1PassNum.ToString();
                            CH1PassRate.Text = (Math.Round((decimal)CH1PassNum / CH1Product, 2) * 100).ToString() + "%";

                            if (save.ChkMES)
                            {
                                AddMES(1);
                            }

                        }
                    }
                    else
                    {
                        CH1Tlight.Text = "";
                    }

                 
                    if (plc.CH2NG)
                    {
                        if (CH2IsStart && ch2Start)
                        {
                            ch2readresult = true;
                            CH2Tlight.Text = "NG";
                            CH2Tlight.ForeColor = Color.Red;
                            {
                                plc.WriteCH2SC(false);
                                plc.WriteCH2XC(false);
                                plc.WriteCH2TC(false);
                                plc.WriteCH2XQ(false);
                                Thread.Sleep(50);
                            }
                            {
                                timerCH2CT.Stop();
                                CH3progressBar.Value = CH3progressBar.Maximum;
                                CH4progressBar.Value = CH4progressBar.Maximum;
                                //CH3progressBar.BackColor = Color.Red;
                                //CH4progressBar.BackColor = Color.Red;
                                CH2IsStart = false;
                                //if (CKCH2Port.IsOpen)   Form1.f1.CKCH2Port.WriteLine("OUTP 0");
                            }
                            //产品计数
                            CH2Product += 1;
                            CH2FailNum += 1;
                            WriteCH2ProductCount();
                            CH2ProductNumber.Text = CH2Product.ToString();
                            //CH2PassNumber.Text = CH2PassNum.ToString();
                            CH2FailNumber.Text = CH2FailNum.ToString();
                            CH2PassRate.Text = (Math.Round((decimal)CH2PassNum / CH2Product, 2) * 100).ToString() + "%";
                            //CH2CT.Text = (Math.Round((decimal)CH2FailNum / CH2Product, 2) * 100).ToString() + "%";
                            //CH2Status.Text = "待机";
                            //CH2Status.ForeColor = Color.Black;
                            if (save.ChkMES)
                            {
                                AddMES(2);
                            }
                            CreateFile(2);
                            CHXProBarFlag[3] = 0;
                            CHXProBarFlag[4] = 0;
                            CH2LinUP.Stop();
                            CH3ReadFlowT.Stop();
                            CH3ReadPress.Stop();
                            CH4ReadFlowT.Stop();
                            CH4ReadPress.Stop();
                            JudgeCH2ADC = false;
                        }
                    }
                    else if (plc.CH2OK && !CH2IsStart)
                    {
                        if (!ch2readresult && ch2Start)
                        {
                            ch2readresult = true;
                            CH2Tlight.Text = "OK";
                            CH2Tlight.ForeColor = Color.Green;
                            {
                                plc.WriteCH1SC(false);
                                plc.WriteCH1XC(false);
                                plc.WriteCH1TC(false);
                                plc.WriteCH1XQ(false);
                                Thread.Sleep(50);
                            }
                            {
                                plc.CH2XQWC();
                                timerCH2CT.Stop();
                                CH3progressBar.Value = CH3progressBar.Maximum;
                                CH4progressBar.Value = CH4progressBar.Maximum;
                                CH2IsStart = false;
                                //if (CKCH2Port.IsOpen)   Form1.f1.CKCH2Port.WriteLine("OUTP 0");
                            }

                            ////产品计数
                            CH2Product += 1;
                            CH2PassNum += 1;
                            WriteCH2ProductCount();
                            CH2ProductNumber.Text = CH2Product.ToString();
                            CH2PassNumber.Text = CH2PassNum.ToString();
                            CH2PassRate.Text = (Math.Round((decimal)CH2PassNum / CH2Product, 2) * 100).ToString() + "%";
                            if (save.ChkMES)
                            {
                                AddMES(2);
                            }
                            CreateFile(2);
                        }
                    }
                    else
                    {
                        CH2Tlight.Text = "";
                    }
                }
                else
                {
                    Logger.Log(I18N.GetLangText(dicLang, "PLC运行信号读取失败"));
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "PLC运行信号读取失败"));
                    //PLCSignal.Stop();
                    PLCRun.BackColor = Color.Red;
                    //MessageBox.Show("PLC运行信号读取失败！");
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "PLC收发") + ":" + ex.Message);
                //PLCSignal.Stop();
                PLCRun.BackColor = Color.Red;
                //MessageBox.Show("PLC收发:" + ex.Message);
                //MessageBox.Show("PLC收发:" + ex.StackTrace);
                Logger.Log(ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// UDP广播获取仪器IP地址
        /// </summary>
        public void UDPBroadcast()
        {
            try
            {
                ch1ipaddress = null;
                ch2ipaddress = null;
                ch3ipaddress = null;
                ch4ipaddress = null;
                UDPRecvData = "";
                string PrefixIP = new GetIP().GetLocalIP();
                if (PrefixIP.Length > 1)
                {
                    localipaddress = PrefixIP;
                    int IP_index = PrefixIP.LastIndexOf(".");
                    PrefixIP = PrefixIP.Remove(IP_index + 1);
                    PrefixIP += "255";

                    sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    //255.255.255.255
                    //iep1 = new IPEndPoint(IPAddress.Broadcast, 9999);
                    IPAddress ip;
                    ip = IPAddress.Parse(PrefixIP);
                    iep1 = new IPEndPoint(ip, 9999);

                    string hostname = Dns.GetHostName();
                    data = Encoding.ASCII.GetBytes("hello,udp server");

                    sock.SetSocketOption(SocketOptionLevel.Socket,
                    SocketOptionName.Broadcast, 1);

                    UDPOverTime.Interval = 3000;
                    UDPOverTime.Start();
                    UDPlisten = new Thread(UDP_Listening);
                    UDPlisten.Start();
                    sock.SendTo(data, iep1);
                    UDPRead.Interval = 3000;
                    UDPRead.Start();
                }
                else
                {
                    // MessageBox.Show("未获取IP地址，请重试!");
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "未获取IP地址，请重试"));
                    Logger.Log(I18N.GetLangText(dicLang, "未获取IP地址，请重试"));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("UDP广播:" + ex.Message);
                Logger.Log("UDP broadcast:" + ex.Message);
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "UDP broadcast:" + ex.Message);
            }
        }

        private void UDP_Listening()
        {
            while (true)
            {
                Invoke(new System.Action(() =>
                {
                    UDPOverTime.Stop();
                }));
                byte[] data = new byte[1024];
                int recv = sock.Receive(data);
                string RecvData = Encoding.ASCII.GetString(data, 0, recv);
                UDPRecvData += RecvData;
                UDPRecvData += "\n";
            }
        }

        private void UDPRead_Tick(object sender, EventArgs e)
        {
            UDPRead.Stop();
            UDP_Parse();
        }

        public void UDP_Parse()
        {
            try
            {
                if (!String.IsNullOrEmpty(UDPRecvData))
                {
                    ch1ipaddress = null;
                    ch2ipaddress = null;
                    ch3ipaddress = null;
                    ch4ipaddress = null;
                    string[] IPData = UDPRecvData.Split('\n');
                    int ip_num = IPData.Length;
                    if (ip_num > 0)
                    {
                        int i;
                        for (i = 0; i < ip_num - 1; i++)
                        {
                            string[] ipaddress = IPData[i].Split(':');
                            int ch_station = Convert.ToInt32(ipaddress[0]);
                            switch (ch_station)
                            {
                                case 1:
                                    ch1ipaddress = ipaddress[1];
                                    break;

                                case 2:
                                    ch2ipaddress = ipaddress[1];
                                    break;

                                case 3:
                                    ch3ipaddress = ipaddress[1];
                                    break;

                                case 4:
                                    ch4ipaddress = ipaddress[1];
                                    break;
                            }
                        }
                        if (SocketConn)
                        {
                            if (ch1ipaddress is null)
                            {
                                Invoke(new System.Action(() =>
                                {
                                    //MessageBox.Show("CH1仪器无法获取IP地址！");
                                    TCPIsConnect(false, 1);
                                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1仪器无法获取IP地址"));
                                    Logger.Log(I18N.GetLangText(dicLang, "CH1仪器无法获取IP地址"));
                                }));
                            }
                            else
                            {
                                //left_ch1tcp = new TCPsocket(ch1ipaddress, "9999", 1);
                                //left_ch1tcp.ClientConnect();
                                ch1client.btnConnect("2000", ch1ipaddress, "9999");
                            }
                            if (ch2ipaddress is null)
                            {
                                Invoke(new System.Action(() =>
                                {
                                    //MessageBox.Show("CH2仪器无法获取IP地址！");
                                    TCPIsConnect(false, 2);
                                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2仪器无法获取IP地址"));
                                    Logger.Log(I18N.GetLangText(dicLang, "CH2仪器无法获取IP地址"));
                                }));
                            }
                            else
                            {
                                //left_ch2tcp = new CH2TCPsocket(ch2ipaddress, "9999", 2);
                                //left_ch2tcp.ClientConnect();
                                ch2client.btnConnect("2000", ch2ipaddress, "9999");
                                //left_ch2tcp = new TCPsocket(ch2ipaddress, "9999", 2);
                                //left_ch2tcp.ClientConnect();
                                //System.Threading.Thread.Sleep(500);
                                //left_ch2tcp = new CH2TCPsocket(ch2ipaddress, "9999", 2);
                                //left_ch2tcp.ClientConnect();
                            }
                            if (ch3ipaddress is null)
                            {
                                Invoke(new System.Action(() =>
                                {
                                    //MessageBox.Show("CH3仪器无法获取IP地址！");
                                    TCPIsConnect(false, 3);
                                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH3仪器无法获取IP地址"));
                                    Logger.Log(I18N.GetLangText(dicLang, "CH3仪器无法获取IP地址"));
                                }));
                            }
                            else
                            {
                                //right_ch1tcp = new TCPsocket(ch3ipaddress, "9999", 3);
                                //right_ch1tcp.ClientConnect();
                                ch3client.btnConnect("2000", ch3ipaddress, "9999");
                            }
                            if (ch4ipaddress is null)
                            {
                                Invoke(new System.Action(() =>
                                {
                                    //MessageBox.Show("CH4仪器无法获取IP地址！");
                                    TCPIsConnect(false, 4);
                                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH4仪器无法获取IP地址"));
                                    Logger.Log(I18N.GetLangText(dicLang, "CH4仪器无法获取IP地址"));
                                }));
                            }
                            else
                            {
                                //right_ch2tcp = new TCPsocket(ch4ipaddress, "9999", 4);
                                //right_ch2tcp.ClientConnect();
                                ch4client.btnConnect("2000", ch4ipaddress, "9999");
                            }
                        }
                    }
                    else
                    {
                        Invoke(new System.Action(() =>
                        {
                            //MessageBox.Show("无法搜索到仪器IP！");
                            TCPIsConnect(false, 1);
                            TCPIsConnect(false, 2);
                            TCPIsConnect(false, 3);
                            TCPIsConnect(false, 4);
                            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "无法搜索到仪器IP"));
                            Logger.Log(I18N.GetLangText(dicLang, "无法搜索到仪器IP"));
                        }));
                    }
                }
                else
                {
                    Invoke(new System.Action(() =>
                    {
                        //MessageBox.Show("无法搜索到仪器IP！");
                        TCPIsConnect(false, 1);
                        TCPIsConnect(false, 2);
                        TCPIsConnect(false, 3);
                        TCPIsConnect(false, 4);
                        wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "无法搜索到仪器IP"));
                        Logger.Log(I18N.GetLangText(dicLang, "无法搜索到仪器IP"));
                    }));
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "UDP:" + ex.Message);
                //MessageBox.Show("UDP:" + ex.Message);
                Logger.Log("UDP:" + ex.Message);
            }
        }

        /// <summary>
        /// TCP关闭
        /// </summary>
        public void TCPClose(int CH)
        {
            if (CH == 1)
            {
                //left_ch1tcp.ClientClose();
                CH1IsRun.Stop();
                ch1client.ClientClose();
            }
            if (CH == 2)
            {
                //left_ch2tcp.ClientClose();
                CH2IsRun.Stop();
                //CH2RTPress.Stop();
                ch2client.ClientClose();
            }
            if (CH == 3)
            {
                //right_ch1tcp.ClientClose();
                CH3IsRun.Stop();
                ch3client.ClientClose();
            }
            if (CH == 4)
            {
                //right_ch2tcp.ClientClose();
                CH4IsRun.Stop();
                //CH4RTPress.Stop();
                ch4client.ClientClose();
            }
        }

        /// <summary>
        /// TCP消息弹窗显示
        /// </summary>
        /// <param name="error"></param>
        public void TCP_Messagebox(string error, int CH)
        {
            Invoke(new System.Action(() =>
            {
                if (CH == 1)
                {
                    LeftCH1TCP.Text = "NG";
                    LeftCH1TCP.ForeColor = Color.Red;
                    CH1IsRun.Stop();
                    //  CHXProBarFlag[1] = 0;
                    ProBarRun.Stop();
                    //CH1RTPress.Stop();
                }
                if (CH == 2)
                {
                    LeftCH2TCP.Text = "NG";
                    LeftCH2TCP.ForeColor = Color.Red;
                    CH2IsRun.Stop();
                    //   CHXProBarFlag[2] = 0;
                    ProBarRun.Stop();
                    //CH2RTPress.Stop();
                }
                if (CH == 3)
                {
                    RightCH1TCP.Text = "NG";
                    RightCH1TCP.ForeColor = Color.Red;
                    CH3IsRun.Stop();
                    //   CHXProBarFlag[3] = 0;
                    ProBarRun.Stop();
                    //CH3RTPress.Stop();
                }
                if (CH == 4)
                {
                    RightCH2TCP.Text = "NG";
                    RightCH2TCP.ForeColor = Color.Red;
                    CH4IsRun.Stop();
                    // CHXProBarFlag[4] = 0;
                    ProBarRun.Stop();
                    //CH4RTPress.Stop();
                }
                //MessageBox.Show(error);
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", error);
                Logger.Log(error);
            }));
        }

        /// <summary>
        /// TCP是否成功连接的状态提示
        /// </summary>
        public void TCPIsConnect(bool IsConn, int CH)
        {
            Invoke(new System.Action(() =>
            {
                if (CH == 1)
                {
                    if (IsConn)
                    {
                        LeftCH1TCP.Text = "OK";
                        LeftCH1TCP.ForeColor = Color.Green;
                        CH1IsRun.Interval = 300;
                        CH1IsRun.Start();
                        // ch1_1step = 5;//5-27
                        //ch1stage = 1;
                    }
                    else
                    {
                        LeftCH1TCP.Text = "NG";
                        LeftCH1TCP.ForeColor = Color.Red;
                    }
                }
                if (CH == 2)
                {
                    if (IsConn)
                    {
                        LeftCH2TCP.Text = "OK";
                        LeftCH2TCP.ForeColor = Color.Green;
                        CH2IsRun.Interval = 300;

                        CH2IsRun.Start();
                        // ch1_2step = 5;//5-27
                        //ch2stage = 1;
                    }
                    else
                    {
                        LeftCH2TCP.Text = "NG";
                        LeftCH2TCP.ForeColor = Color.Red;
                    }
                }
                if (CH == 3)
                {
                    if (IsConn)
                    {
                        RightCH1TCP.Text = "OK";
                        RightCH1TCP.ForeColor = Color.Green;
                        CH3IsRun.Interval = 300;
                        CH3IsRun.Start();
                        //ch3stage = 1;
                        //   ch2_1step = 5;//5-27
                    }
                    else
                    {
                        RightCH1TCP.Text = "NG";
                        RightCH1TCP.ForeColor = Color.Red;
                    }
                }
                if (CH == 4)
                {
                    if (IsConn)
                    {
                        RightCH2TCP.Text = "OK";
                        RightCH2TCP.ForeColor = Color.Green;
                        CH4IsRun.Interval = 1300;
                        CH4IsRun.Start();
                        //ch4stage = 1;
                        //  ch2_2step = 5;//5-27
                    }
                    else
                    {
                        RightCH2TCP.Text = "NG";
                        RightCH2TCP.ForeColor = Color.Red;
                    }
                }
            }));
        }
        private static readonly object lockObject = new object();
        public static void CH1lock()
        {
            lock (lockObject)
            {
             Form1.f1.   CH1Stagenum();
                Thread.Sleep(200);
            }
        }
        public static void CH2lock()
        {
            lock (lockObject)
            {
           Form1. f1.    CH2Stagenum();
                Thread.Sleep(200);
            }
        }

        public static void CH3lock()
        {
            lock (lockObject)
            {
                Form1.f1.CH3Stagenum();
                Thread.Sleep(200);
            }
        }


        public static void CH4lock()
        {
            lock (lockObject)
            {
                Form1.f1.CH4Stagenum();
                Thread.Sleep(200);
            }
        }

        /// <summary>
        /// TCP信息接收
        /// </summary>
        /// <param name="text"></param>
        public void TCP_DataReceived(string text, int CH)
        {
            try
            {
                Invoke(new System.Action(() =>
                {
                    if (CH == 1)
                    {
                        CH1ReceiveText.Clear();
                        CH1ReceiveText.Text = text;
                        CH1Stagenum();
                        //CH1lock();



                    }
                    if (CH == 2)
                    {
                        CH2ReceiveText.Clear();
                        CH2ReceiveText.Text = text;

                        CH2Stagenum();

                    }
                    if (CH == 3)
                    {
                        CH3ReceiveText.Clear();
                        CH3ReceiveText.Text = text;
                        CH3Stagenum();

                    }
                    if (CH == 4)
                    {
                        CH4ReceiveText.Clear();
                        CH4ReceiveText.Text = text;
                        CH4Stagenum();

                    }
                    //PortLog log = new PortLog();
                    //log.Logmsg("Receive:  " + CH1ReceiveText.Text);
                }));
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "TCP_DataReceived:" + ex.Message);
                //MessageBox.Show("TCP信息接收:" + ex.Message);
                Logger.Log("TCP_DataReceived:" + ex.Message);
                Logger.Log("TCP_DataReceived:" + ex.StackTrace);
            }
        }

        /// <summary>
        /// CH1根据变量判断仪器过程，对读取的数据进行不同的转换
        /// </summary>
        private void CH1Stagenum()
        {
            try
            {
                switch (ch1stage)
                {
                    case 0://利用定时器进入状态位读取
                        CH1IsRun.Interval = 200;
                        CH1IsRun.Start();
                        ch1stage = 1;
                        break;

                    case 1://此时为状态位读取
                        string str1;
                        str1 = CH1ReceiveText.Text;

                        //if (str1.Length > 8 && str1.Substring(2, 6) == "010101")
                        if (str1.Substring(6, 2) == "01")
                        {
                            //CH1IsRun.Stop();
                            ch1write = 0;
                            ch1_1step = 2;
                            CHXProBarFlag[1] = 0;
                            LeftCH1Status.ForeColor = Color.Green;
                            LeftCH1Status.Text = I18N.GetLangText(dicLang, "启动");
                            left_CH1Tlight.Text = "";
                            LeftCH1BigLeak.Text = "";
                            LeftCH1SmallLeak.Text = "";
                            LeftCH1LeakPress.Text = "";
                            ch1readpara = false;
                            CH1progressBar.Value = 0;
                            //测试参数ToolStripMenuItem.Enabled = false;

                            //ReadParams.Interval = 400;
                            //ReadParams.Start();
                        }
                        break;

                    case 2://此时为读取参数并数据转换
                        string str2;
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                        str2 = CH1ReceiveText.Text;
                        //ReadParams.Stop();
                        if (str2.Length == 126 && str2.Substring(2, 2) == "03")
                        {
                           // CH1IsRun.Stop();
                            ch1_1params = comm.ReadParams(str2, ch1_1params.CHKUnit);
                            if (CH1RTStep.Contains("Leak"))
                            {
                                if (!Equals(ch1_1leakparams, ch1_1params))
                                {
                                    IsWriteTitle = true;
                                }
                                ch1_1leakparams = ch1_1params;
                            }
                            if (ch1_1params.CHKUnit)
                            {
                                PressureUnit.Text = "kgf/cm2";
                                LeakUnit.Text = "g/cm2";
                                ch1_1params.FPtoplimit = (Convert.ToDouble(ch1_1params.FPtoplimit) / 98).ToString();
                                ch1_1params.FPlowlimit = (Convert.ToDouble(ch1_1params.FPlowlimit) / 98).ToString();
                                //ch1_1params.BalanPreMax = (Convert.ToDouble(ch1_1params.BalanPreMax) ).ToString();
                                //ch1_1params.BalanPreMin = (Convert.ToDouble(ch1_1params.BalanPreMin) ).ToString();
                                ch1_1params.Leaktoplimit = (Convert.ToDouble(ch1_1params.Leaktoplimit) / 98).ToString();
                                ch1_1params.Leaklowlimit = (Convert.ToDouble(ch1_1params.Leaklowlimit) / 98).ToString();
                            }
                            else
                            {
                                PressureUnit.Text = ch1_1params.PUnit;
                                LeakUnit.Text = ch1_1params.LUnit;
                            }
                            CH1progressBar.Maximum = ch1_1params.progressBar_value;
                            if (!ch1readpara)
                            {
                                CHXProBarFlag[1] = 1;
                                ProBarRun.Start();
                                CH1IsRun.Interval = 600;
                                CH1IsRun.Start();
                                ch1_1step = 3;
                                //LeakResult.Interval = 500;
                                //LeakResult.Start();
                            }
                        }
                        //else
                        //{
                        //    ReadParams.Interval = 500;
                        //    ReadParams.Start();
                        //}
                        break;

                    case 3://循环读取测试结果
                        string str4;
                        str4 = CH1ReceiveText.Text;
                        //LeakResult.Stop();
                        if (str4.Length == 110 && str4.Substring(2, 2) == "03")
                        {
                            left_ch1result = comm.ReadLeak(str4);
                            //测流量时，输出压力归0，不显示
                            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD")
                            {
                                left_ch1result.LeakPressure = "0";
                            }
                            if (ch1_1params.CHKUnit)
                            {
                                LeftCH1LeakPress.Text = (Convert.ToDouble(left_ch1result.LeakPressure) / 98).ToString("f2");
                                LeftCH1SmallLeak.Text = (Convert.ToDouble(left_ch1result.SmallLeak) / 98).ToString("f2");
                            }
                            else
                            {
                                LeftCH1LeakPress.Text = left_ch1result.LeakPressure;
                                //textBox1.Text += "\n";
                                //textBox1.Text += left_ch1result.LeakPressure;
                                LeftCH1SmallLeak.Text = left_ch1result.SmallLeak;
                            }
                            LeftCH1BigLeak.Text = left_ch1result.BigLeak;
                            left_CH1Tlight.Text = left_ch1result.Result;

                            if (left_CH1Tlight.Text.Contains("NG") is true)
                            {
                                left_CH1Tlight.ForeColor = Color.Red;
                            }
                            else if (left_CH1Tlight.Text.Contains("OK") is true)
                            {
                                left_CH1Tlight.ForeColor = Color.Green;
                            }

                            if (str4.Substring(8, 2) == "05" || str4.Substring(8, 2) == "00")
                            {
                                if (str4.Substring(8, 2) == "05")
                                {
                                    LeftCH1Status.Text = I18N.GetLangText(dicLang, "排气");
                                }
                                if (left_CH1Tlight.Text.Contains("OK") || left_CH1Tlight.Text.Contains("NG"))
                                {
                                    //排气阶段或结束测试
                                    //LeakResult.Stop();
                                    //CH1IsRun.Interval = 400;
                                    //CH1IsRun.Start();
                                    ch1_1step = 4;
                                    CHXProBarFlag[1] = 0;
                                    CH1progressBar.Value = CH1progressBar.Maximum;
                                    //ProBarRun.Stop();
                                }
                            }
                            else if (str4.Substring(8, 2) == "04")
                            {
                                //plc.CH1valveclose();
                                LeftCH1Status.Text = I18N.GetLangText(dicLang, "检测");
                            }
                            else if (str4.Substring(8, 2) == "01")
                            {
                                //充气阶段压力没有值，取平衡阶段最初的值，取不到值的时候取两次
                                //充气阶段
                                //fullpressure = LeakPressure.Text;
                                LeftCH1Status.Text = I18N.GetLangText(dicLang, "准备");
                                if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                            }
                            else if (str4.Substring(8, 2) == "02")
                            {
                                //balanpressure = LeakPressure.Text;
                                LeftCH1Status.Text = I18N.GetLangText(dicLang, "充气");
                                if (CH1RTStep == "UPLeak" || CH1RTStep == "DOWNLeak")
                                {
                                    CH1TestResult.FullPre1 = LeftCH1LeakPress.Text;
                                }
                                else if (CH1RTStep == "FWDLeak")
                                {
                                    CH1TestResult.FWD_FullPre1 = LeftCH1LeakPress.Text;
                                }
                                if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                            }
                            else if (str4.Substring(8, 2) == "03")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                LeftCH1Status.Text = I18N.GetLangText(dicLang, "平衡");

                                if (!CH1IsRun.Enabled)
                                {
                                    CH1IsRun.Start();
                                    ch1_1step = 3;
                                }
                                if (CH1RTStep == "UPLeak" || CH1RTStep == "DOWNLeak" || CH1RTStep == "FWDLeak")
                                {
                                    plc.CH1FWDLeakFalse();
                                    //把IO关了 M4010 FOSE
                                    plc.CH1LeakFalse();//5-27
                                    if (CH1POWER._serialPort.IsOpen)
                                        Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                                    if (CH1RTStep == "FWDLeak")
                                    {
                                        CH1TestResult.FWD_BalanPre1 = LeftCH1LeakPress.Text;
                                    }
                                    else
                                    {
                                        CH1TestResult.BalanPre1 = LeftCH1LeakPress.Text;
                                    }
                                    if (CH1Pump)
                                    {
                                        CH1LinUP.Stop();
                                        plc.CH1Balance();
                                    }
                                }
                                else
                                     if (CH1POWER._serialPort.IsOpen)
                                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                            }
                        }

                        break;

                    case 4:
                        //按下复位以后收到返回信息则判断状态位
                        CH1IsRun.Interval = 300;
                        CH1IsRun.Start();
                        ch1stage = 5;

                        break;

                    case 5://对仪器是否结束的判断
                        string str5;
                        str5 = CH1ReceiveText.Text;
                        if (str5.Length > 8 && str5.Substring(6, 2) == "00")
                        {

                            //3.9
                            ReadConfig con = new ReadConfig();
                            Model.CH_PARAMS ch_params;
                            ch_params = con.ReadParameters(1, 2);
                            string Leaklowlimit = ch_params.Leaklowlimit;

                           // CH1IsRun.Stop();
                            ch1_1step = 5;
                            ch1stage = 10;
                            LeftCH1Status.ForeColor = Color.Black;
                            LeftCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                            //if (网络设置ToolStripMenuItem.Enabled)
                            //{
                            //    测试参数ToolStripMenuItem.Enabled = true;
                            //}
                            if (ch1write == 0 && !String.IsNullOrEmpty(CH1RTStep))
                            {
                                ch1write = 1;
                                if (CH1RTStep == "FWDLeak")
                                {
                                    Fwdjg = 1;
                                    CH1TestResult.FWD_Leak1 = LeftCH1SmallLeak.Text;
                                }
                                else
                                {
                                    CH1TestResult.Leak1 = LeftCH1SmallLeak.Text;
                                }

                                //界面表格
                                if (CH1RTStep == "UPLeak")
                                {
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-1上充气密"), LeftCH1SmallLeak.Text, LeakUnit.Text, ch1_1params.Leaktoplimit, ch1_1params.Leaklowlimit, left_CH1Tlight.Text);
                                    if (left_CH1Tlight.Text == "NG")
                                    {
                                        FlowNG(1);
                                    }
                                }
                                if (CH1RTStep == "FWDLeak")
                                {
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-1同充气密"), LeftCH1SmallLeak.Text, LeakUnit.Text, ch1_1params.Leaktoplimit, Leaklowlimit, left_CH1Tlight.Text);
                                }

                                //else if (CH1RTStep == "DOWNLeak")
                                //{
                                //    CH1Display("CH1-2下充气密", LeftCH1SmallLeak.Text, LeakUnit.Text, ch1_1params.Leaktoplimit, ch1_1params.Leaklowlimit, left_CH1Tlight.Text);
                                //}

                                if (CH1RTStep == "FWDLeak" && Fwdjg2 == 1 && left_CH1Tlight.Text.Contains("OK") && left_CH2Tlight.Text.Contains("OK") && !left_CH2Tlight.Text.Contains("NG"))
                                {

                                    //Fwdjg = 0;
                                    //Thread.Sleep(100);
                                    //CH1Step += 1;
                                    //CH1Method(CH1Step);
                                }
                                else if (CH1RTStep == "UPLeak" && left_CH1Tlight.Text.Contains("OK") && !left_CH2Tlight.Text.Contains("NG"))
                                //  if (CH1RTStep != "FWDLeak"&& CH1RTStep!= "QC" && left_CH1Tlight.Text.Contains("OK") && !left_CH2Tlight.Text.Contains("NG"))
                                {
                                    left_CH1Tlight.Text = "";
                                    //plc.CH1PowerClose();
                                    Thread.Sleep(100);
                                    CH1Step += 1;
                                    CH1Method(CH1Step);
                                    // ch1_1step = 5;//5-27
                                }
                                else if (left_CH1Tlight.Text.Contains("NG"))
                                {
                                    FlowNG(1);
                                }
                                else
                                {
                                    CH1IsRun.Interval = 800;
                                    CH1IsRun.Start();
                                    // ch1_1step = 1;//5-27
                                }
                            }
                            else
                            {
                                CH1IsRun.Interval = 800;
                                CH1IsRun.Start();
                                //ch1_1step = 5;//5-27
                            }
                        }
                        break;

                    case 6://对蜂鸣器的读取
                        string str6 = CH1ReceiveText.Text;
                        if (str6.Length > 8 && str6.Substring(6, 2) == "01")
                        {
                            ch1_1params.ChkBee = true;
                        }
                        else
                        {
                            ch1_1params.ChkBee = false;
                        }
                        break;

                    case 7:
                        string str7 = CH1ReceiveText.Text;
                        //if (str7.Length == 18 && str7.Substring(2, 2) == "03")
                        //{
                        //    string rtpress1 = str7.Substring(6, 4);
                        //    string rtpress2 = str7.Substring(10, 4);
                        //    string hex_rtpress = rtpress2 + rtpress1;
                        //    string rtpress = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(hex_rtpress, 16)), 0).ToString("F2");
                        //    double realtimepress = Convert.ToDouble(rtpress);
                        //textBox1.Text += realtimepress.ToString();
                        //    if (realtimepress > CH1PressMax)
                        //    {
                        //        CH1PressMax = realtimepress;
                        //    }
                        //}
                        if (str7.Length == 102 && str7.Substring(2, 2) == "03")
                        {
                            string press_unit = str7.Substring(6, 4);
                            int unit_index = Convert.ToInt32(press_unit, 16);
                            switch (unit_index)
                            {
                                case 0:
                                    PressureUnit.Text = "Pa";
                                    //realtimepress = realtimepress * 0.001;
                                    break;

                                case 1:
                                    PressureUnit.Text = "KPa";
                                    break;

                                case 2:
                                    PressureUnit.Text = "MPa";
                                    //realtimepress = realtimepress * 1000;
                                    break;

                                case 3:
                                    PressureUnit.Text = "bar";
                                    //realtimepress = realtimepress * 100;
                                    break;

                                case 4:
                                    PressureUnit.Text = "Psi";
                                    //realtimepress = realtimepress * 6.89476;
                                    break;

                                case 5:
                                    PressureUnit.Text = "kg/cm^2";
                                    //realtimepress = realtimepress * 98.0665;
                                    break;

                                case 6:
                                    PressureUnit.Text = "atm";
                                    //realtimepress = realtimepress * 101.325;
                                    break;

                                case 7:
                                    PressureUnit.Text = "mmHg";
                                    //realtimepress = realtimepress * 0.13332;
                                    break;
                            }
                            string rtpress1 = str7.Substring(90, 4);
                            string rtpress2 = str7.Substring(94, 4);
                            string hex_rtpress = rtpress2 + rtpress1;
                            string rtpress = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(hex_rtpress, 16)), 0).ToString("F2");
                            double realtimepress = Convert.ToDouble(rtpress);
                            CH1_1FullPress.Text = realtimepress.ToString();
                            if (CH1RTStep == "RWD")
                            {
                                //textBox1.Text += realtimepress.ToString();
                                CH1PressMax = realtimepress;
                            }
                            else
                            {
                                if (realtimepress > CH1PressMax)
                                {
                                    CH1PressMax = realtimepress;
                                }
                                if (CH1RTStep == "DOWN")
                                {
                                    if (CH1PressMax > Flow.CH1_1PreMax || CH1PressMax < Flow.CH1_1PreMin)
                                    {
                                        plc.CH1DOWNPreNG();
                                        FlowNG(1);
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), CH1PressMax.ToString(), PressureUnit.Text, Flow.CH1_1PreMax.ToString(), Flow.CH1_1PreMin.ToString(), "NG");
                                    }
                                }
                            }
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1-1收发") + ":" + ex.Message);
                //MessageBox.Show("CH1-1收发：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1-1收发") + ":" + ex.Message+DateTime.Now+ToString()+ex.StackTrace);
            }
        }

        //同充气密标志位
        public static int Fwdjg = 0;
        public static int Fwdjg2 = 0;
        public static int Fwdjg3 = 0;

        /// <summary>
        /// CH2根据变量判断仪器过程，对读取的数据进行不同的转换
        /// </summary>
        private void CH2Stagenum()
        {
            try
            {
                switch (ch2stage)
                {
                    case 0://利用定时器进入状态位读取
                        CH2IsRun.Interval = 200;
                        CH2IsRun.Start();
                        ch2stage = 1;
                        break;

                    case 1://此时为状态位读取
                        string str1;
                        str1 = CH2ReceiveText.Text;
                        //if (str1.Length > 8 && str1.Substring(2, 6) == "010101")
                        if (str1.Substring(6, 2) == "01")
                        {
                            ch2write = 0;
                            CHXProBarFlag[2] = 0;
                            LeftCH2Status.ForeColor = Color.Green;
                            LeftCH2Status.Text = I18N.GetLangText(dicLang, "启动");
                            left_CH2Tlight.Text = "";
                            LeftCH2BigLeak.Text = "";
                            LeftCH2SmallLeak.Text = "";
                            LeftCH2LeakPress.Text = "";
                            ch2readpara = false;
                            CH2progressBar.Value = 0;
                            //测试参数ToolStripMenuItem.Enabled = false;
                            ch1_2step = 2;
                        }
                        break;

                    case 2://此时为读取参数并数据转换
                        string str2;
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                        str2 = CH2ReceiveText.Text;
                        //CH2ReadParams.Stop();
                        if (str2.Length == 126 && str2.Substring(2, 2) == "03")
                        {
                            CH2IsRun.Stop();
                            ch1_2params = comm.ReadParams(str2, ch1_2params.CHKUnit);
                            if (CH1RTStep.Contains("Leak"))
                            {
                                if (!Equals(ch1_2leakparams, ch1_2params))
                                {
                                    IsWriteTitle = true;
                                }
                                ch1_2leakparams = ch1_2params;
                            }
                            if (ch1_2params.CHKUnit)
                            {
                                CH2PressureUnit.Text = "kgf/cm2";
                                CH2LeakUnit.Text = "g/cm2";
                                ch1_2params.FPtoplimit = (Convert.ToDouble(ch1_2params.FPtoplimit) / 98).ToString();
                                ch1_2params.FPlowlimit = (Convert.ToDouble(ch1_2params.FPlowlimit) / 98).ToString();
                                //ch1_2params.BalanPreMax = (Convert.ToDouble(ch1_2params.BalanPreMax) ).ToString();
                                //ch1_2params.BalanPreMin = (Convert.ToDouble(ch1_2params.BalanPreMin) ).ToString();
                                ch1_2params.Leaktoplimit = (Convert.ToDouble(ch1_2params.Leaktoplimit) / 98).ToString();
                                ch1_2params.Leaklowlimit = (Convert.ToDouble(ch1_2params.Leaklowlimit) / 98).ToString();
                            }
                            else
                            {
                                CH2PressureUnit.Text = ch1_2params.PUnit;
                                CH2LeakUnit.Text = ch1_2params.LUnit;
                            }
                            CH2progressBar.Maximum = ch1_2params.progressBar_value;
                            if (!ch2readpara)
                            {
                                CHXProBarFlag[2] = 1;
                                ProBarRun.Start();
                                CH2IsRun.Interval = 600;
                                CH2IsRun.Start();
                                ch1_2step = 3;
                            }
                        }
                        break;

                    case 3://循环读取测试结果
                        string str4;
                        str4 = CH2ReceiveText.Text;
                        //LeakResult.Stop();
                        if (str4.Length == 110 && str4.Substring(2, 2) == "03")
                        {
                            left_ch2result = comm.ReadLeak(str4);
                            //测流量时，输出压力归0，不显示
                            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD")
                            {
                                left_ch2result.LeakPressure = "0";
                            }
                            if (ch1_2params.CHKUnit)
                            {
                                LeftCH2LeakPress.Text = (Convert.ToDouble(left_ch2result.LeakPressure) / 98).ToString("f2");
                                LeftCH2SmallLeak.Text = (Convert.ToDouble(left_ch2result.SmallLeak) / 98).ToString("f2");
                            }
                            else
                            {
                                LeftCH2LeakPress.Text = left_ch2result.LeakPressure;
                                LeftCH2SmallLeak.Text = left_ch2result.SmallLeak;
                            }
                            LeftCH2BigLeak.Text = left_ch2result.BigLeak;
                            left_CH2Tlight.Text = left_ch2result.Result;

                            if (left_CH2Tlight.Text.Contains("NG") is true)
                            {
                                left_CH2Tlight.ForeColor = Color.Red;
                            }
                            else if (left_CH2Tlight.Text.Contains("OK") is true)
                            {
                                left_CH2Tlight.ForeColor = Color.Green;
                            }

                            if (str4.Substring(8, 2) == "05" || str4.Substring(8, 2) == "00")
                            {
                                if (str4.Substring(8, 2) == "05")
                                {
                                    LeftCH2Status.Text = I18N.GetLangText(dicLang, "排气");
                                }
                                if (left_CH2Tlight.Text.Contains("OK") || left_CH2Tlight.Text.Contains("NG"))
                                {
                                    //排气阶段或结束测试

                                    ch1_2step = 4;
                                    CHXProBarFlag[2] = 0;
                                    CH2progressBar.Value = CH2progressBar.Maximum;
                                    //CH2ProBarRun.Stop();
                                }
                            }
                            else if (str4.Substring(8, 2) == "04")
                            {
                                //plc.CH2valveclose();
                                LeftCH2Status.Text = I18N.GetLangText(dicLang, "检测");
                            }
                            else if (str4.Substring(8, 2) == "01")
                            {
                                //充气阶段压力没有值，取平衡阶段最初的值，取不到值的时候取两次
                                //充气阶段
                                //fullpressure = LeakPressure.Text;
                                LeftCH2Status.Text = I18N.GetLangText(dicLang, "准备");
                            }
                            else if (str4.Substring(8, 2) == "02")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                LeftCH2Status.Text = I18N.GetLangText(dicLang, "充气");

                                if (CH1RTStep == "UPLeak" || CH1RTStep == "DOWNLeak")
                                {
                                    if (CH1POWER._serialPort.IsOpen)
                                        Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                                    CH1TestResult.FullPre2 = LeftCH2LeakPress.Text;
                                }
                                else if (CH1RTStep == "FWDLeak")
                                {
                                    if (CH1POWER._serialPort.IsOpen)
                                        Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                                    CH1TestResult.FWD_FullPre2 = LeftCH2LeakPress.Text;
                                }
                            }
                            else if (str4.Substring(8, 2) == "03")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                LeftCH2Status.Text = I18N.GetLangText(dicLang, "平衡");

                                if (!CH2IsRun.Enabled)
                                {
                                    CH2IsRun.Start();
                                    ch1_2step = 3;
                                }
                                if (CH1RTStep == "UPLeak" || CH1RTStep == "DOWNLeak" || CH1RTStep == "FWDLeak")
                                {
                                    plc.CH1FWDLeakFalse();
                                    plc.CH1DownLeakFalse();//5-27
                                    if (CH1POWER._serialPort.IsOpen)
                                        Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                                    if (CH1RTStep == "FWDLeak")
                                    {
                                        CH1TestResult.FWD_BalanPre2 = LeftCH2LeakPress.Text;
                                    }
                                    else
                                    {
                                        CH1TestResult.BalanPre2 = LeftCH2LeakPress.Text;
                                    }
                                    if (CH1Pump)
                                    {
                                        CH1LinUP.Stop();
                                        plc.CH2Balance();
                                    }
                                }
                                else
                                {
                                    if (CH1POWER._serialPort.IsOpen)
                                        Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                                }
                            }
                        }

                        break;

                    case 4:
                        //按下复位以后收到返回信息则判断状态位
                        CH2IsRun.Interval = 300;
                        CH2IsRun.Start();
                        ch2stage = 5;

                        break;

                    case 5://对仪器是否结束的判断
                        string str5;
                        str5 = CH2ReceiveText.Text;

                        if (str5.Length > 8 && str5.Substring(6, 2) == "00")
                        {
                            CH2IsRun.Stop();
                            ch1_2step = 5;
                            ch2stage = 10;
                            LeftCH2Status.ForeColor = Color.Black;
                            LeftCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                            //if (网络设置ToolStripMenuItem.Enabled is true)
                            //{
                            //    测试参数ToolStripMenuItem.Enabled = true;
                            //}

                            //3.9
                            ReadConfig con = new ReadConfig();
                            Model.CH_PARAMS ch_params;
                            ch_params = con.ReadParameters(1, 2);
                            string Leaklowlimit = ch_params.Leaklowlimit;

                            if (ch2write == 0 && !String.IsNullOrEmpty(CH1RTStep))
                            {
                                ch2write = 1;
                                if (CH1RTStep != "FWDLeak")

                                {

                                    CH1TestResult.Leak2 = LeftCH2SmallLeak.Text;
                                }
                                if (CH1RTStep == "DOWNLeak")
                                {
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-2下充气密"), LeftCH2SmallLeak.Text, CH2LeakUnit.Text, ch1_2params.Leaktoplimit, Leaklowlimit, left_CH2Tlight.Text);
                                }
                                if (CH1RTStep == "FWDLeak")
                                {
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-2同充气密"), LeftCH2SmallLeak.Text, CH2LeakUnit.Text, ch1_2params.Leaktoplimit, Leaklowlimit, left_CH2Tlight.Text);
                                }

                                if (CH1RTStep == "FWDLeak")
                                {
                                    CH1TestResult.FWD_Leak2 = LeftCH2SmallLeak.Text;
                                    Fwdjg2 = 1;

                                }
                                if (CH1RTStep == "FWDLeak" && Fwdjg == 1 && left_CH2Tlight.Text.Contains("OK") && left_CH1Tlight.Text.Contains("OK") && !left_CH1Tlight.Text.Contains("NG"))
                                {

                                    //Fwdjg2 = 0;
                                    //Thread.Sleep(100);
                                    //CH1Step += 1;
                                    //CH1Method(CH1Step);

                                }
                                else if (CH1RTStep == "DOWNLeak" && left_CH2Tlight.Text.Contains("OK") && !left_CH1Tlight.Text.Contains("NG"))
                                {
                                    CH1Step += 1;
                                    CH1Method(CH1Step);
                                    left_CH2Tlight.Text = "";

                                }
                                else if (left_CH2Tlight.Text.Contains("NG"))
                                {
                                    FlowNG(1);
                                }
                                else
                                {
                                    CH2IsRun.Interval = 800;
                                    CH2IsRun.Start();
                                    //  ch1_2step = 5;//5-27
                                }

                            }
                            else
                            {
                                CH2IsRun.Interval = 800;
                                CH2IsRun.Start();
                                // ch1_2step = 5;//5-27
                            }
                        }
                        break;

                    case 6://对蜂鸣器的读取
                        string str6 = CH2ReceiveText.Text;
                        if (str6.Length > 8 && str6.Substring(6, 2) == "01")
                        {
                            ch1_2params.ChkBee = true;
                        }
                        else
                        {
                            ch1_2params.ChkBee = false;
                        }
                        break;

                    case 7:
                        string str7 = CH2ReceiveText.Text;
                        if (str7.Length == 102 && str7.Substring(2, 2) == "03")
                        //if ( str7.Substring(2, 2) == "03")
                        {
                            string press_unit = str7.Substring(6, 4);
                            int unit_index = Convert.ToInt32(press_unit, 16);
                            switch (unit_index)
                            {
                                case 0:
                                    CH2PressureUnit.Text = "Pa";
                                    //realtimepress = realtimepress * 0.001;
                                    break;

                                case 1:
                                    CH2PressureUnit.Text = "KPa";
                                    break;

                                case 2:
                                    CH2PressureUnit.Text = "MPa";
                                    //realtimepress = realtimepress * 1000;
                                    break;

                                case 3:
                                    CH2PressureUnit.Text = "bar";
                                    //realtimepress = realtimepress * 100;
                                    break;

                                case 4:
                                    CH2PressureUnit.Text = "Psi";
                                    //realtimepress = realtimepress * 6.89476;
                                    break;

                                case 5:
                                    CH2PressureUnit.Text = "kg/cm^2";
                                    //realtimepress = realtimepress * 98.0665;
                                    break;

                                case 6:
                                    CH2PressureUnit.Text = "atm";
                                    //realtimepress = realtimepress * 101.325;
                                    break;

                                case 7:
                                    CH2PressureUnit.Text = "mmHg";
                                    //realtimepress = realtimepress * 0.13332;
                                    break;
                            }
                            string rtpress1 = str7.Substring(90, 4);
                            string rtpress2 = str7.Substring(94, 4);
                            //string rtpress1 = str7.Substring(6, 4);
                            //string rtpress2 = str7.Substring(10, 4);
                            string hex_rtpress = rtpress2 + rtpress1;
                            string rtpress = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(hex_rtpress, 16)), 0).ToString("F2");
                            double realtimepress = Convert.ToDouble(rtpress);
                            CH1_2FullPress.Text = realtimepress.ToString();
                            if (CH1RTStep == "RWD")
                            {
                                CH2PressMax = realtimepress;
                            }
                            else
                            {
                                if (realtimepress > CH2PressMax)
                                {
                                    CH2PressMax = realtimepress;
                                }
                                if (CH1RTStep == "UP")
                                {
                                    if (CH2PressMax > Flow.CH1_2PreMax || CH2PressMax < Flow.CH1_2PreMin)
                                    {
                                        plc.CH1UPPreNG();
                                        FlowNG(1);
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH2PressMax.ToString(), CH2PressureUnit.Text, Flow.CH1_2PreMax.ToString(), Flow.CH1_2PreMin.ToString(), "NG");
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1-2收发") + "：" + ex.Message);
                //MessageBox.Show("CH1-2收发：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1-2收发") + ":" + ex.Message + DateTime.Now + ToString() + ex.StackTrace);
            }
        }

        /// <summary>
        /// CH3根据变量判断仪器过程，对读取的数据进行不同的转换
        /// </summary>
        private void CH3Stagenum()
        {
            try
            {
                //Debug.Write("ch3stage:" + ch3stage);
                switch (ch3stage)
                {
                    //case 0://利用定时器进入状态位读取
                    //    CH3IsRun.Interval = 400;
                    //    CH3IsRun.Start();

                    //    ch3stage = 1;
                    //    break;

                    case 1://此时为状态位读取
                        string str1;
                        str1 = CH3ReceiveText.Text;
                        //  if (CKCH2Port.IsOpen)   Form1.f1.CKCH2Port.WriteLine("OUTP 1");
                        if (str1.Substring(6, 2) == "01")
                        {
                            //CH3IsRun.Stop();//ERIC
                            CHXProBarFlag[3] = 0;
                            ch3write = 0;

                            RightCH1Status.ForeColor = Color.Green;
                            RightCH1Status.Text = I18N.GetLangText(dicLang, "启动");
                            RightCH1LeakPress.Text = "";
                            RightCH1BigLeak.Text = "";
                            RightCH1SmallLeak.Text = "";
                            right_CH1Tlight.Text = "";
                            ch3readpara = false;
                            ch2_1step = 2;
                            CH3progressBar.Value = 0;
                            //测试参数ToolStripMenuItem.Enabled = false;

                            //CH3ReadParams.Interval = 400;
                            //CH3ReadParams.Start();
                        }
                        break;

                    case 2://此时为读取参数并数据转换
                        string str2;
                        str2 = CH3ReceiveText.Text;
                        //CH3ReadParams.Stop();
                        CH2POWER.Write("OUTP 1");
                        if (str2.Length == 126 && str2.Substring(2, 2) == "03")
                        {
                            CH2POWER.Write("OUTP 1");
                            CH3IsRun.Stop();
                            ch2_1params = comm.ReadParams(str2, ch2_1params.CHKUnit);
                            if (CH2RTStep.Contains("Leak"))
                            {
                                if (!Equals(ch2_1leakparams, ch2_1params))
                                {
                                    IsWriteTitle = true;
                                }
                                ch2_1leakparams = ch2_1params;
                            }
                            if (ch2_1params.CHKUnit)
                            {
                                CH3PressureUnit.Text = "kgf/cm2";
                                CH3LeakUnit.Text = "g/cm2";
                                ch2_1params.FPtoplimit = (Convert.ToDouble(ch2_1params.FPtoplimit) / 98).ToString();
                                ch2_1params.FPlowlimit = (Convert.ToDouble(ch2_1params.FPlowlimit) / 98).ToString();
                                //ch2_1params.BalanPreMax = (Convert.ToDouble(ch2_1params.BalanPreMax) / 98).ToString();
                                //ch2_1params.BalanPreMin = (Convert.ToDouble(ch2_1params.BalanPreMin) / 98).ToString();
                                ch2_1params.Leaktoplimit = (Convert.ToDouble(ch2_1params.Leaktoplimit) / 98).ToString();
                                ch2_1params.Leaklowlimit = (Convert.ToDouble(ch2_1params.Leaklowlimit) / 98).ToString();
                            }
                            else
                            {
                                CH3PressureUnit.Text = ch2_1params.PUnit;
                                CH3LeakUnit.Text = ch2_1params.LUnit;
                            }
                            CH3progressBar.Maximum = ch2_1params.progressBar_value;
                            if (!ch3readpara)
                            {
                                CHXProBarFlag[3] = 1;
                                ProBarRun.Start();
                                CH3IsRun.Interval = 600;
                                CH3IsRun.Start();
                                ch2_1step = 3;
                                //CH3LeakResult.Interval = 500;
                                //CH3LeakResult.Start();
                            }
                        }
                        break;

                    case 3://循环读取测试结果
                        string str4;
                        str4 = CH3ReceiveText.Text;
                        //LeakResult.Stop();
                        if (str4.Length == 110 && str4.Substring(2, 2) == "03")
                        {
                            //if (!CH3IsRun.Enabled)
                            //{
                            //    CH3IsRun.Start();
                            //}
                            right_ch1result = comm.ReadLeak(str4);
                            //测流量时，输出压力归0，不显示
                            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD")
                            {
                                right_ch1result.LeakPressure = "0";
                            }
                            if (ch2_1params.CHKUnit)
                            {
                                RightCH1LeakPress.Text = (Convert.ToDouble(right_ch1result.LeakPressure) / 98).ToString("f2");
                                RightCH1SmallLeak.Text = (Convert.ToDouble(right_ch1result.SmallLeak) / 98).ToString("f2");
                            }
                            else
                            {
                                RightCH1LeakPress.Text = right_ch1result.LeakPressure;
                                RightCH1SmallLeak.Text = right_ch1result.SmallLeak;
                            }
                            RightCH1BigLeak.Text = right_ch1result.BigLeak;
                            right_CH1Tlight.Text = right_ch1result.Result;

                            if (right_CH1Tlight.Text.Contains("NG") is true)
                            {
                                right_CH1Tlight.ForeColor = Color.Red;
                            }
                            else if (right_CH1Tlight.Text.Contains("OK") is true)
                            {
                                right_CH1Tlight.ForeColor = Color.Green;
                            }

                            if (str4.Substring(8, 2) == "05" || str4.Substring(8, 2) == "00")
                            {
                                if (str4.Substring(8, 2) == "05")
                                {
                                    RightCH1Status.Text = I18N.GetLangText(dicLang, "排气");
                                }
                                if (right_CH1Tlight.Text.Contains("OK") || right_CH1Tlight.Text.Contains("NG"))
                                {
                                    //排气阶段或结束测试
                                    //CH3LeakResult.Stop();
                                    //CH3IsRun.Interval = 400;
                                    //CH3IsRun.Start();
                                    //ch3stage = 5;
                                    ch2_1step = 4;
                                    CHXProBarFlag[3] = 0;
                                    CH3progressBar.Value = CH3progressBar.Maximum;

                                    //CH3ProBarRun.Stop();
                                }
                            }
                            else if (str4.Substring(8, 2) == "04")
                            {
                                RightCH1Status.Text = I18N.GetLangText(dicLang, "检测");
                                CH2POWER.Write("OUTP 0");
                            }
                            else if (str4.Substring(8, 2) == "01")
                            {
                                //充气阶段压力没有值，取平衡阶段最初的值，取不到值的时候取两次
                                //充气阶段
                                //fullpressure = LeakPressure.Text;
                                RightCH1Status.Text = I18N.GetLangText(dicLang, "准备");
                            }
                            else if (str4.Substring(8, 2) == "02")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                RightCH1Status.Text = I18N.GetLangText(dicLang, "充气");
                                CH2POWER.Write("OUTP 1");
                                if (CH2RTStep == "UPLeak" || CH2RTStep == "DOWNLeak")
                                {
                                    CH2TestResult.FullPre1 = RightCH1LeakPress.Text;
                                }
                                else if (CH2RTStep == "FWDLeak")
                                {
                                    CH2TestResult.FWD_FullPre1 = RightCH1LeakPress.Text;
                                }
                            }
                            else if (str4.Substring(8, 2) == "03")
                            {
    
                                //balanpressure = LeakPressure.Text;
                                RightCH1Status.Text = I18N.GetLangText(dicLang, "平衡");

                                if (CH2RTStep == "UPLeak" || CH2RTStep == "DOWNLeak" || CH2RTStep == "FWDLeak")
                                {
                                    plc.CH2FWDLeakFalse();
                                    plc.CH2UPLeakFalse();
                                    CH2POWER.Write("OUTP 0");
                                    if (CH2RTStep == "FWDLeak")
                                    {
                                        CH2TestResult.FWD_BalanPre1 = RightCH1LeakPress.Text;
                                    }
                                    else
                                    {
                                        CH2TestResult.BalanPre1 = RightCH1LeakPress.Text;
                                    }
                                    if (CH2Pump)
                                    {
                                        CH2LinUP.Stop();
                                        plc.CH3Balance();
                                    }
                                }
                                else CH2POWER.Write("OUTP 1");
                            }
                        }

                        break;

                    case 4:
                        //按下复位以后收到返回信息则判断状态位
                        CH3IsRun.Interval = 300;
                        CH3IsRun.Start();
                        ch3stage = 5;
                        break;

                    case 5://对仪器是否结束的判断
                        string str5;
                        str5 = CH3ReceiveText.Text;
                        if (str5.Length > 8 && str5.Substring(6, 2) == "00")
                        {
                            CH3IsRun.Stop(); //ERIC
                            ch2_1step = 5;
                            ch3stage = 10;
                            RightCH1Status.ForeColor = Color.Black;
                            RightCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                            //if (网络设置ToolStripMenuItem.Enabled is true)
                            //{
                            //    测试参数ToolStripMenuItem.Enabled = true;
                            //}

                            //3.9
                            ReadConfig con = new ReadConfig();
                            Model.CH_PARAMS ch_params;
                            ch_params = con.ReadParameters(1, 2);
                            string Leaklowlimit = ch_params.Leaklowlimit;

                            if (ch3write == 0 && !String.IsNullOrEmpty(CH2RTStep))
                            {
                                //ch3timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (CH2RTStep == "FWDLeak")
                                {
                                    CH2JGFLAG++;
                                    CH2TestResult.FWD_Leak1 = RightCH1SmallLeak.Text;
                                }
                                else
                                {
                                    CH2TestResult.Leak1 = RightCH1SmallLeak.Text;
                                }
                                if (CH2RTStep == "UPLeak")
                                {
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-1上充气密"), RightCH1SmallLeak.Text, CH3LeakUnit.Text, ch2_1params.Leaktoplimit, ch2_1params.Leaklowlimit, right_CH1Tlight.Text);
                                }
                                //if(CH1RTStep == "DOWNLeak")
                                //{
                                //    CH3Display("CH2-2下充气密", RightCH1SmallLeak.Text, CH3LeakUnit.Text, ch2_1params.Leaktoplimit, ch2_1params.Leaklowlimit, right_CH1Tlight.Text);
                                //}
                                if (CH2RTStep == "FWDLeak")
                                {
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-1同充气密"), RightCH1SmallLeak.Text, CH3LeakUnit.Text, ch2_1params.Leaktoplimit, Leaklowlimit, right_CH1Tlight.Text);
                                }
                                if (CH2RTStep == "FWDLeak" && CH2JGFLAG == 2 && right_CH1Tlight.Text.Contains("OK") && !right_CH2Tlight.Text.Contains("NG"))
                                {

                                    //Thread.Sleep(100);
                                    //CH2Step += 1;
                                    //CH2Method(CH2Step);
                                }
                                else if (CH2RTStep == "UPLeak" && right_CH1Tlight.Text.Contains("OK") && !right_CH2Tlight.Text.Contains("NG"))
                                {
                                    right_CH1Tlight.Text = "";
                                    CH2Step += 1;
                                    CH2Method(CH2Step);
                                }
                                else if (right_CH1Tlight.Text.Contains("NG"))
                                {
                                    FlowNG(2);
                                }
                                else
                                {
                                    CH3IsRun.Interval = 800;
                                    CH3IsRun.Start();
                                    // ch2_1step = 5; //5-27
                                }
                            }
                            else
                            {
                                CH3IsRun.Interval = 800;
                                CH3IsRun.Start();
                                // ch2_1step = 5;//5-27
                            }
                        }
                        break;

                    case 6://对蜂鸣器的读取
                        string str6 = CH3ReceiveText.Text;
                        if (str6.Substring(6, 2) == "01")
                        {
                            ch2_1params.ChkBee = true;
                        }
                        else
                        {
                            ch2_1params.ChkBee = false;
                        }
                        break;

                    case 7:
                        string str7 = CH3ReceiveText.Text;
                        if (str7.Length == 102 && str7.Substring(2, 2) == "03")
                        {
                            string press_unit = str7.Substring(6, 4);
                            int unit_index = Convert.ToInt32(press_unit, 16);
                            switch (unit_index)
                            {
                                case 0:
                                    CH3PressureUnit.Text = "Pa";
                                    //realtimepress = realtimepress * 0.001;
                                    break;

                                case 1:
                                    CH3PressureUnit.Text = "KPa";
                                    break;

                                case 2:
                                    CH3PressureUnit.Text = "MPa";
                                    //realtimepress = realtimepress * 1000;
                                    break;

                                case 3:
                                    CH3PressureUnit.Text = "bar";
                                    //realtimepress = realtimepress * 100;
                                    break;

                                case 4:
                                    CH3PressureUnit.Text = "Psi";
                                    //realtimepress = realtimepress * 6.89476;
                                    break;

                                case 5:
                                    CH3PressureUnit.Text = "kg/cm^2";
                                    //realtimepress = realtimepress * 98.0665;
                                    break;

                                case 6:
                                    CH3PressureUnit.Text = "atm";
                                    //realtimepress = realtimepress * 101.325;
                                    break;

                                case 7:
                                    CH3PressureUnit.Text = "mmHg";
                                    //realtimepress = realtimepress * 0.13332;
                                    break;
                            }
                            string rtpress1 = str7.Substring(90, 4);
                            string rtpress2 = str7.Substring(94, 4);
                            string hex_rtpress = rtpress2 + rtpress1;
                            string rtpress = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(hex_rtpress, 16)), 0).ToString("F2");
                            double realtimepress = Convert.ToDouble(rtpress);
                            textBox2.Text += realtimepress;
                            CH2_1FullPress.Text = realtimepress.ToString();
                            if (CH2RTStep == "RWD")
                            {
                                //textBox1.Text += realtimepress.ToString();
                                CH3PressMax = realtimepress;
                            }
                            else
                            {
                                if (realtimepress > CH3PressMax)
                                {
                                    CH3PressMax = realtimepress;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2-1收发") + "：" + ex.Message);
                //MessageBox.Show("CH2-1收发：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2-1收发") + ":" + ex.Message + DateTime.Now + ToString() + ex.StackTrace);
            }
        }

        public int CH2JGFLAG = 0;

        /// <summary>
        /// CH4根据变量判断仪器过程，对读取的数据进行不同的转换
        /// </summary>
        private void CH4Stagenum()
        {
            try
            {
                switch (ch4stage)
                {
                    case 0://利用定时器进入状态位读取
                        CH4IsRun.Interval = 400;
                        CH4IsRun.Start();

                        ch4stage = 1;
                        break;

                    case 1://此时为状态位读取
                        string str1;
                        str1 = CH4ReceiveText.Text;
                         
                        if (str1.Substring(6, 2) == "01")
                        {
                            //CH4IsRun.Stop();

                            ch4write = 0;
                            CHXProBarFlag[4] = 0;
                            RightCH2Status.ForeColor = Color.Green;
                            RightCH2Status.Text = I18N.GetLangText(dicLang, "启动");
                            RightCH2LeakPress.Text = "";
                            RightCH2BigLeak.Text = "";
                            RightCH2SmallLeak.Text = "";
                            right_CH2Tlight.Text = "";
                            ch4readpara = false;

                            CH4progressBar.Value = 0;
                            //测试参数ToolStripMenuItem.Enabled = false;
                            ch2_2step = 2;
                            //CH4ReadParams.Interval = 400;
                            //CH4ReadParams.Start();
                        }
                        break;

                    case 2://此时为读取参数并数据转换
                        string str2;

                        str2 = CH4ReceiveText.Text;
                        //CH4ReadParams.Stop();
                        CH2POWER.Write("OUTP 1");
                        if (str2.Length >= 126 && str2.Substring(2, 2) == "03")
                        {
                            //  CH4IsRun.Stop();
                            ch2_2params = comm.ReadParams(str2, ch2_2params.CHKUnit);
                            if (CH2RTStep.Contains("Leak"))
                            {
                                if (!Equals(ch2_2leakparams, ch2_2params))
                                {
                                    IsWriteTitle = true;
                                }
                                ch2_2leakparams = ch2_2params;
                            }
                            if (ch2_2params.CHKUnit)
                            {
                                CH4PressureUnit.Text = "kgf/cm2";
                                CH4LeakUnit.Text = "g/cm2";
                                ch2_2params.FPtoplimit = (Convert.ToDouble(ch2_2params.FPtoplimit) / 98).ToString();
                                ch2_2params.FPlowlimit = (Convert.ToDouble(ch2_2params.FPlowlimit) / 98).ToString();
                                //ch2_2params.BalanPreMax = (Convert.ToDouble(ch2_2params.BalanPreMax) / 98).ToString();
                                //ch2_2params.BalanPreMin = (Convert.ToDouble(ch2_2params.BalanPreMin) / 98).ToString();
                                ch2_2params.Leaktoplimit = (Convert.ToDouble(ch2_2params.Leaktoplimit) / 98).ToString();
                                ch2_2params.Leaklowlimit = (Convert.ToDouble(ch2_2params.Leaklowlimit) / 98).ToString();


                            }
                            else
                            {
                                CH4PressureUnit.Text = ch2_2params.PUnit;
                                CH4LeakUnit.Text = ch2_2params.LUnit;
                            }

                            CH4progressBar.Maximum = ch2_2params.progressBar_value;
                            if (!ch4readpara)
                            {
                                ProBarRun.Start();
                                CHXProBarFlag[4] = 1;
                                CH4IsRun.Interval = 600;
                                CH4IsRun.Start();
                                ch2_2step = 3;
                                //CH4ProBarRun.Start();
                                //CH4LeakResult.Interval = 500;
                                //CH4LeakResult.Start();
                            }
                        }
                        break;

                    case 3://循环读取测试结果
                        string str4;
                        str4 = CH4ReceiveText.Text;
                        //LeakResult.Stop();
                        ch2_2step = 3;
                        if (str4.Length >= 110 && str4.Substring(2, 2) == "03")
                        {
                            //if (!CH4IsRun.Enabled)
                            //{
                            //    CH4IsRun.Start();
                            //}
                            right_ch2result = comm.ReadLeak(str4);
                            //测流量时，输出压力归0，不显示
                            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD")
                            {
                                right_ch2result.LeakPressure = "0";
                            }
                            if (ch2_2params.CHKUnit)
                            {
                                RightCH2LeakPress.Text = (Convert.ToDouble(right_ch2result.LeakPressure) / 98).ToString("f2");
                                RightCH2SmallLeak.Text = (Convert.ToDouble(right_ch2result.SmallLeak) / 98).ToString("f2");
                            }
                            else
                            {
                                RightCH2LeakPress.Text = right_ch2result.LeakPressure;
                                RightCH2SmallLeak.Text = right_ch2result.SmallLeak;
                            }
                            RightCH2BigLeak.Text = right_ch2result.BigLeak;
                            right_CH2Tlight.Text = right_ch2result.Result;

                            if (right_CH2Tlight.Text.Contains("NG") is true)
                            {
                                right_CH2Tlight.ForeColor = Color.Red;
                            }
                            else if (right_CH2Tlight.Text.Contains("OK") is true)
                            {
                                right_CH2Tlight.ForeColor = Color.Green;
                            }

                            if (str4.Substring(8, 2) == "05" || str4.Substring(8, 2) == "00")
                            {
                                if (str4.Substring(8, 2) == "05")
                                {
                                    RightCH2Status.Text = I18N.GetLangText(dicLang, "排气");
                                }
                                if (right_CH2Tlight.Text.Contains("OK") || right_CH2Tlight.Text.Contains("NG"))
                                {
                                    //排气阶段或结束测试
                                    //CH4LeakResult.Stop();
                                    //CH4IsRun.Interval = 400;
                                    //CH4IsRun.Start();
                                    //ch4stage = 5;
                                    ch2_2step = 4;
                                    CHXProBarFlag[4] = 0;
                                    CH4progressBar.Value = CH4progressBar.Maximum;
                                    //CH4ProBarRun.Stop();
                                }
                            }
                            else if (str4.Substring(8, 2) == "04")
                            {
                                RightCH2Status.Text = I18N.GetLangText(dicLang, "检测");
                                CH2POWER.Write("OUTP 0");
                            }
                            else if (str4.Substring(8, 2) == "01")
                            {
                                //充气阶段压力没有值，取平衡阶段最初的值，取不到值的时候取两次
                                //充气阶段
                                //fullpressure = LeakPressure.Text;
                                RightCH2Status.Text = I18N.GetLangText(dicLang, "准备");
                                if (Convert.ToDouble(CH2RTVDC.Text) < 3)
                                    CH2POWER.Write("OUTP 1");
                            }
                            else if (str4.Substring(8, 2) == "02")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                RightCH2Status.Text = I18N.GetLangText(dicLang, "充气");
                                if (CH2RTStep == "UPLeak" || CH2RTStep == "DOWNLeak")
                                {
                                    CH2TestResult.FullPre2 = RightCH2LeakPress.Text;
                                }
                                else if (CH2RTStep == "FWDLeak")
                                {
                                    CH2TestResult.FWD_FullPre2 = RightCH2LeakPress.Text;
                                }
                                // else
                                CH2POWER.Write("OUTP 1");
                            }
                            else if (str4.Substring(8, 2) == "03")
                            {
                                //平衡阶段
                                //balanpressure = LeakPressure.Text;
                                RightCH2Status.Text = I18N.GetLangText(dicLang, "平衡");

                                if (CH2RTStep == "UPLeak" || CH2RTStep == "DOWNLeak" || CH2RTStep == "FWDLeak")
                                {
                                    plc.CH2FWDLeakFalse();
                                    plc.CH2DownLeakFalse();//4014
                                                           //if (Convert.ToDouble(CH2RTVDC.Text) > 3)
                                    CH2POWER.Write("OUTP 0");
                                    if (CH2RTStep == "FWDLeak")
                                    {
                                        CH2TestResult.FWD_BalanPre2 = RightCH2LeakPress.Text;
                                    }
                                    else
                                    {
                                        CH2TestResult.BalanPre2 = RightCH2LeakPress.Text;
                                    }

                                    if (CH2Pump)
                                    {

                                        CH2LinUP.Stop();
                                        plc.CH4Balance();
                                    }
                                }
                                else //if (Convert.ToDouble(CH2RTVDC.Text) < 3)
                                    CH2POWER.Write("OUTP 1");

                            }
                        }

                        break;

                    case 4:
                        //按下复位以后收到返回信息则判断状态位
                        CH4IsRun.Interval = 300;
                        CH4IsRun.Start();
                        ch4stage = 5;
                        break;

                    case 5://对仪器是否结束的判断
                        string str5;
                        str5 = CH4ReceiveText.Text;
                        if (str5.Length > 8 && str5.Substring(6, 2) == "00")
                        {
                            CH4IsRun.Stop();
                            ch2_2step = 5;
                            ch4stage = 10;
                            RightCH2Status.ForeColor = Color.Black;
                            RightCH2Status.Text = I18N.GetLangText(dicLang, "待机");


                            //3.9
                            ReadConfig con = new ReadConfig();
                            Model.CH_PARAMS ch_params;
                            ch_params = con.ReadParameters(1, 2);
                            string Leaklowlimit = ch_params.Leaklowlimit;



                            //Debug.Write("B08" + RightCH2Status.Text);
                            //if (网络设置ToolStripMenuItem.Enabled is true)
                            //{
                            //    测试参数ToolStripMenuItem.Enabled = true;
                            //}
                            if (ch4write == 0 && !String.IsNullOrEmpty(CH2RTStep))
                            {
                                //ch4timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                if (CH2RTStep == "FWDLeak")
                                {
                                    CH2JGFLAG++;
                                    CH2TestResult.FWD_Leak2 = RightCH2SmallLeak.Text;
                                }
                                else
                                {
                                    CH2TestResult.Leak2 = RightCH2SmallLeak.Text;
                                }
                                //if (CH2RTStep == "UPLeak")
                                //{
                                //    CH3Display("CH2-2上充气密", RightCH2SmallLeak.Text, CH4LeakUnit.Text, ch2_2params.Leaktoplimit, ch2_2params.Leaklowlimit, right_CH2Tlight.Text);
                                //}
                                if (CH2RTStep == "DOWNLeak")
                                {
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-2下充气密"), RightCH2SmallLeak.Text, CH4LeakUnit.Text, ch2_2params.Leaktoplimit, Leaklowlimit, right_CH2Tlight.Text);
                                }
                                if (CH2RTStep == "FWDLeak")
                                {
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-2同充气密"), RightCH2SmallLeak.Text, CH4LeakUnit.Text, ch2_2params.Leaktoplimit, Leaklowlimit, right_CH2Tlight.Text);
                                }
                                if (CH2RTStep == "FWDLeak" && CH2JGFLAG == 2 && right_CH2Tlight.Text.Contains("OK") && !right_CH1Tlight.Text.Contains("NG"))
                                {
                                    //plc.CH1PowerClose();
                                    //Thread.Sleep(100);
                                    //CH2Step += 1;
                                    //CH2Method(CH2Step);
                                }
                                else if (CH2RTStep == "DOWNLeak" && right_CH2Tlight.Text.Contains("OK") && !right_CH1Tlight.Text.Contains("NG"))
                                {
                                    right_CH2Tlight.Text = "";
                                    CH2Step += 1;
                                    CH2Method(CH2Step);
                                }
                                else if (right_CH2Tlight.Text.Contains("NG"))
                                {
                                    FlowNG(2);
                                }
                                else
                                {
                                    CH4IsRun.Interval = 800;
                                    CH4IsRun.Start();
                                    //  ch2_2step = 5;//5-27
                                }
                            }
                            else
                            {
                                CH4IsRun.Interval = 800;
                                CH4IsRun.Start();
                                //  ch2_2step = 5;//5-27
                            }
                        }
                        break;

                    case 6://对蜂鸣器的读取
                        string str6 = CH4ReceiveText.Text;
                        if (str6.Substring(6, 2) == "01")
                        {
                            ch2_2params.ChkBee = true;
                        }
                        else
                        {
                            ch2_2params.ChkBee = false;
                        }
                        break;

                    case 7:
                        string str7 = CH4ReceiveText.Text;
                        if (str7.Length == 102 && str7.Substring(2, 2) == "03")
                        {
                            string press_unit = str7.Substring(6, 4);
                            int unit_index = Convert.ToInt32(press_unit, 16);
                            switch (unit_index)
                            {
                                case 0:
                                    CH4PressureUnit.Text = "Pa";
                                    //realtimepress = realtimepress * 0.001;
                                    break;

                                case 1:
                                    CH4PressureUnit.Text = "KPa";
                                    break;

                                case 2:
                                    CH4PressureUnit.Text = "MPa";
                                    //realtimepress = realtimepress * 1000;
                                    break;

                                case 3:
                                    CH4PressureUnit.Text = "bar";
                                    //realtimepress = realtimepress * 100;
                                    break;

                                case 4:
                                    CH4PressureUnit.Text = "Psi";
                                    //realtimepress = realtimepress * 6.89476;
                                    break;

                                case 5:
                                    CH4PressureUnit.Text = "kg/cm^2";
                                    //realtimepress = realtimepress * 98.0665;
                                    break;

                                case 6:
                                    CH4PressureUnit.Text = "atm";
                                    //realtimepress = realtimepress * 101.325;
                                    break;

                                case 7:
                                    CH4PressureUnit.Text = "mmHg";
                                    //realtimepress = realtimepress * 0.13332;
                                    break;
                            }
                            string rtpress1 = str7.Substring(90, 4);
                            string rtpress2 = str7.Substring(94, 4);
                            string hex_rtpress = rtpress2 + rtpress1;
                            string rtpress = BitConverter.ToSingle(BitConverter.GetBytes(Convert.ToUInt32(hex_rtpress, 16)), 0).ToString("F2");
                            double realtimepress = Convert.ToDouble(rtpress);
                            textBox1.Text += realtimepress;
                            CH2_2FullPress.Text = realtimepress.ToString();
                            if (CH2RTStep == "RWD")
                            {
                                //textBox1.Text += realtimepress.ToString();
                                CH4PressMax = realtimepress;
                            }
                            else
                            {
                                if (realtimepress > CH4PressMax)
                                {
                                    CH4PressMax = realtimepress;
                                }
                                if (CH2RTStep == "UP")
                                {
                                    if (CH4PressMax > Flow.CH2_2PreMax || CH4PressMax < Flow.CH2_2PreMin)
                                    {
                                        plc.CH2UPPreNG();
                                        FlowNG(2);
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2_2PreMax.ToString(), Flow.CH2_2PreMin.ToString(), "NG");
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2-2收发") + "：" + ex.Message);
                //MessageBox.Show("CH2-2收发：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2-2收发") + ":" + ex.Message + DateTime.Now + ToString() + ex.StackTrace);
            }
        }

        //读取状态位的定时器
        private void IsRun_Tick(object sender, EventArgs e)
        {
            try
            {
                string text;
                switch (ch1_1step)
                {
                    case 1:
                        text = "01 01 00 02 00 01";
                    
                        ch1client.btnSendData(text);
                        ch1stage = 1;
                        break;

                    case 2:
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                        text = "01 03 03 E8 00 1D";
                        ch1client.btnSendData(text);
                        ch1stage = 2;
                        break;

                    case 3:
                        text = "01 03 04 0A 00 19";
                        ch1client.btnSendData(text);
                        ch1stage = 3;
                        break;

                    case 4:
                        text = "01 01 00 02 00 01";
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
                        ch1client.btnSendData(text);
                        ch1stage = 5;
                        break;

                    case 5:
                        text = "01 01 00 02 00 01";

                        ch1client.btnSendData(text);
                        ch1stage = 10;
                        break;
                }
            }
            catch (Exception ex)
            {
               // CH1IsRun.Stop();
                //MessageBox.Show("CH1_1通道：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2-CH1_1通道") + "：" + ex.Message);
            }
        }

        private List<int> CHXProBarFlag = new List<int> { 0, 0, 0, 0, 0 };

        private void ProgressBarRun_Tick(object sender, EventArgs e)
        {
            ProBarRun.Interval = 100;
            int a, b;
            b = 0;
            for (a = 1; a <= 4; a++)
            {
                if (CHXProBarFlag[a] == 1)
                    b = 1;
            }
            if (b == 0)
            {
                ProBarRun.Stop();
                return;
            }
            if (CHXProBarFlag[1] == 1)
            {
                //CH1progressBar.PerformStep();
                CH1progressBar.Value += 1;
            }
            if (CHXProBarFlag[2] == 1)
            {
                //CH2progressBar.PerformStep();
                CH2progressBar.Value += 1;
            }
            if (CHXProBarFlag[3] == 1)
            {
                //CH3progressBar.PerformStep();
                CH3progressBar.Value += 1;

                Debug.WriteLine("CH3progressBar.Maximum:" + CH3progressBar.Maximum);
                Debug.WriteLine("CH3progressBar:" + CH3progressBar.Value);
            }
            if (CHXProBarFlag[4] == 1)
            {
                //CH4progressBar.PerformStep();
                CH4progressBar.Value += 1;
            }
        }

        private void CH2IsRun_Tick(object sender, EventArgs e)
        {
            try
            {
                string text;
                switch (ch1_2step)
                {
                    case 1:
                     
                        text = "02 01 00 02 00 01";
                        ch2client.btnSendData(text);
                        ch2stage = 1;
                        break;

                    case 2:
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                        text = "02 03 03 E8 00 1D";
                        ch2client.btnSendData(text);
                        ch2stage = 2;
                        break;

                    case 3:
                        text = "02 03 04 0A 00 19";
                        ch2client.btnSendData(text);
                        ch2stage = 3;
                        break;

                    case 4:
                  
                        text = "02 01 00 02 00 01";
                        ch2client.btnSendData(text);
                        ch2stage = 5;
                        break;

                    case 5:

                        text = "02 01 00 02 00 01";
                        ch2client.btnSendData(text);
                        ch2stage = 10;
                        break;
                }
            }
            catch (Exception ex)
            {
                CH2IsRun.Stop();
                //MessageBox.Show("CH1_2通道：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1_2通道") + "：" + ex.Message);
            }
        }

        private void CH3IsRun_Tick(object sender, EventArgs e)
        {
            try
            {
                string text;

                switch (ch2_1step)
                {
                    case 1:
                        text = "03 01 00 02 00 01";
                       
                        ch3client.btnSendData(text);
                        ch3stage = 1;
                        break;

                    case 2:
                        CH2POWER.Write("OUTP 1");
                        text = "03 03 03 E8 00 1D";
                        ch3client.btnSendData(text);
                        ch3stage = 2;
                        break;

                    case 3:
                        text = "03 03 04 0A 00 19";
                        ch3client.btnSendData(text);
                        ch3stage = 3;
                        break;

                    case 4:
                        text = "03 01 00 02 00 01";
                        CH2POWER.Write("OUTP 0");
                        ch3client.btnSendData(text);
                        ch3stage = 5;
                        break;

                    case 5:
                         
                        text = "03 01 00 02 00 01";
                        ch3client.btnSendData(text);
                        ch3stage = 10;
                        break;
                }
            }
            catch (Exception ex)
            {
                CH3IsRun.Stop();
                //MessageBox.Show("CH2_1通道：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2_1通道") + "：" + ex.Message);
            }
        }

        private void CH4IsRun_Tick(object sender, EventArgs e)
        {
            try
            {
                string text;
                switch (ch2_2step)
                {
                    case 1:
                        text = "04 01 00 02 00 01";
                         
                        ch4client.btnSendData(text);
                        ch4stage = 1;
                        break;

                    case 2:
                        text = "04 03 03 E8 00 1D";
                        CH2POWER.Write("OUTP 1");
                        ch4client.btnSendData(text);
                        ch4stage = 2;
                        break;

                    case 3:
                        text = "04 03 04 0A 00 19";
                        ch4client.btnSendData(text);
                        ch4stage = 3;
                        break;

                    case 4:
                        text = "04 01 00 02 00 01";
                        CH2POWER.Write("OUTP 0");
                        ch4client.btnSendData(text);
                        ch4stage = 5;
                        break;

                    case 5:
                        text = "04 01 00 02 00 01";
                        ch4client.btnSendData(text);
                        ch4stage = 10;
                        break;
                }
            }
            catch (Exception ex)
            {
                CH4IsRun.Stop();
                //MessageBox.Show("CH2_2通道：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2_2通道") + "：" + ex.Message);
            }
        }

        //将数据写入Excel表格
        private void AddExcel(int CH)
        {
            try
            {
                string datatime = DateTime.Now.ToString("yyyyMMdd");
                if (save.Path == "")//若路径处不输入则获取桌面路径
                {
                    save.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                }
                string filepath = save.Path + "\\";
                if (CH == 1 || CH == 2)
                {
                    filepath += "Left\\";
                }
                else
                {
                    filepath += "Right\\";
                }
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                string name = datatime + ".xls";
                filepath += name;
                Excel.Application xapp = new Excel.Application();
                if (File.Exists(filepath) == false)//判断所选路径是否有文件
                {
                    //若不存在该文件，则创建新文件
                    var str1 = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbooks xbook1 = str1.Workbooks;
                    Excel.Workbook xbook2 = str1.Workbooks.Add(true);
                    xbook2.SaveAs(filepath);//按照指定路径存储新文件
                    xbook2.Close();
                }
                //若存在该文件，则打开文件并写入数据
                Excel.Workbook xbook = xapp.Workbooks._Open(filepath, Missing.Value, Missing.Value,
                                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                   Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                Excel.Worksheet xsheet = (Excel.Worksheet)xbook.Sheets[1];

                string nowdate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //string[] fieldArr = { "时间", "条形码", "测试结果", "左工位测试压力", "左工位当前压差值", "左工位微漏泄漏量", "左工位气密结果",
                // "右工位测试压力", "右工位当前压差值", "右工位微漏泄漏量", "右工位气密结果", "工作电流", "工作电流结果",
                //"工作电压", "工作电压结果", "静态电流", "静态电流结果"};//列名

                string[] fieldArr = { I18N.GetLangText(dicLang, "时间"),I18N.GetLangText(dicLang, "条形码"),I18N.GetLangText(dicLang, "测试结果"), I18N.GetLangText(dicLang, "左工位测试压力"),
                   I18N.GetLangText(dicLang, "左工位当前压差值"), I18N.GetLangText(dicLang, "左工位微漏泄漏量"),I18N.GetLangText(dicLang, "左工位气密结果") ,I18N.GetLangText(dicLang, "右工位测试压力"),
                   I18N.GetLangText(dicLang, "右工位当前压差值"),I18N.GetLangText(dicLang, "右工位微漏泄漏量"), I18N.GetLangText(dicLang, "右工位气密结果") ,I18N.GetLangText(dicLang, "工作电流"),
                   I18N.GetLangText(dicLang, "工作电流结果"),I18N.GetLangText(dicLang, "工作电压"), I18N.GetLangText(dicLang, "工作电压结果"), I18N.GetLangText(dicLang, "静态电流"),I18N.GetLangText(dicLang, "静态电流结果")};//列名

                string[] dataArr = null;
                if (CH == 1)
                {
                    dataArr = new string[] { DateTime.Now.ToString(), left_CH1Code.Text, CH1Tlight.Text, LeftCH1LeakPress.Text + ch1_1params.PUnit,
                    LeftCH1BigLeak + "Pa" , LeftCH1SmallLeak.Text + ch1_1params.LUnit, left_CH1Tlight.Text, LeftCH2LeakPress.Text + ch1_2params.PUnit,
                    LeftCH2BigLeak + "Pa", LeftCH2SmallLeak.Text + ch1_2params.LUnit, left_CH2Tlight.Text, CH1ADCMax.ToString(), CH1ADCresult,
                    CH1VDCMax.ToString(), CH1VDCresult, CH1RTElec.Text ,CH1Elecresult }; //行名
                }
                if (CH == 2)
                {
                    dataArr = new string[] { DateTime.Now.ToString(), right_CH1Code.Text, CH2Tlight.Text, RightCH1LeakPress.Text + ch2_1params.PUnit,
                    RightCH1BigLeak + "Pa" , RightCH1SmallLeak.Text + ch2_1params.LUnit, right_CH1Tlight.Text, RightCH2LeakPress.Text + ch2_2params.PUnit,
                    RightCH2BigLeak + "Pa", RightCH2SmallLeak.Text + ch2_2params.LUnit, right_CH2Tlight.Text, CH2ADCMax.ToString(), CH2ADCresult,
                    CH2VDCMax.ToString(), CH2VDCresult, CH2RTElec.Text ,CH2Elecresult }; //行名
                }

                int c = xsheet.UsedRange.Rows.Count;
                int a;
                for (a = 0; a < fieldArr.Length; a++)
                {
                    xsheet.Cells[a + 1][1] = fieldArr[a];
                }
                int b;
                //    int j;
                for (b = 0; b < dataArr.Length; b++)
                {
                    xsheet.Cells[b + 1][c + 1] = dataArr[b];
                }

                xbook.Save();
                xsheet = null;
                xbook.Close();
                xapp.DisplayAlerts = false;
                xapp.Quit();
                xapp = null;
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "Excel:" + ex.Message);
                //MessageBox.Show("Excel:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "Excel:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private IntPtr flagtime;

        //将数据写入CSV文件中
        /*     private void AddCSV(int CH)
             {
                 try
                 {
                     string fileName;
                     string file = DateTime.Now.ToString("yyyyMMdd");
                     string productname = machine.Replace(".ini", "");
                     if (String.IsNullOrEmpty(save.Path))
                     {
                         fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                     }
                     else
                     {
                         fileName = save.Path + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                     }
                     if (!Directory.Exists(fileName))
                     {
                         Directory.CreateDirectory(fileName);
                     }
                     string name = file + ".csv";
                     fileName += name;
                     if (File.Exists(fileName) == false)
                     {
                         StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                         fileWriter1.Write(I18N.GetLangText(dicLang, "气袋产品测试记录报表") + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");
                         fileWriter1.Flush();
                         fileWriter1.Close();
                     }
                     if (IsWriteTitle)
                     {
                         IsWriteTitle = false;
                         StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1,充气时间") + "：," + ch1_1leakparams.FullTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_1leakparams.BalanTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_1leakparams.TestTime1 + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_1leakparams.ExhaustTime + "s\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2,充气时间") + "：," + ch1_2leakparams.FullTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_2leakparams.BalanTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_2leakparams.TestTime1 + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_2leakparams.ExhaustTime + "s\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1,充气时间") + "：," + ch2_1leakparams.FullTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_1leakparams.BalanTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_1leakparams.TestTime1 + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_1leakparams.ExhaustTime + "s\n");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2,充气时间") + "：," + ch2_2leakparams.FullTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_2leakparams.BalanTime + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_2leakparams.TestTime1 + "s,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_2leakparams.ExhaustTime + "s\n");
                         //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号"), I18N.GetLangText(dicLang, "测试时间"), I18N.GetLangText(dicLang, "条形码"), I18N.GetLangText(dicLang, "判定结果"), "OK/NG,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                       //  if (!Electricity.ord.CH1UpDownChange)
                         {
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                         }
                         //else
                         //{
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(plm)") + ",");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充）(plm)") + ",");
                         //}

                         fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");

                    //     if (!Electricity.ord.CH2UpDownChange)
                         {
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                         }
                         //else
                         //{
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                         //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                         //}

                         fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");
                         fileWriter1.Flush();
                         fileWriter1.Close();
                     }
                     StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                     switch (CH)
                     {
                         case 1:
                             fileWriter.Write(CH1csvworknum + I18N.GetLangText(dicLang, "左")+ ",#" + CH1timestamp + "#,'" + left_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH1Tlight.Text + ",");
                             fileWriter.Write(CH1RTElec.Text + "," + CH1TestResult.UP_ADCMAX + "," + CH1TestResult.UP_VDCMAX + "," + CH1TestResult.UP_Pre + ",");
                             fileWriter.Write(CH1TestResult.UP_Flow + "," + CH1TestResult.DOWN_ADCMAX + "," + CH1TestResult.DOWN_VDCMAX + ",");
                             fileWriter.Write(CH1TestResult.DOWN_Pre + "," + CH1TestResult.DOWN_Flow + "," + CH1TestResult.ElecRatio + ",");
                             fileWriter.Write(CH1TestResult.PressRatio + "," + CH1TestResult.FWD_ADCMAX + "," + CH1TestResult.FWD_VDCMAX + ",");
                             fileWriter.Write(CH1TestResult.FWD_Pre1 + "," + CH1TestResult.FWD_Pre2 + "," + CH1TestResult.FWD_Flow1 + "," + CH1TestResult.FWD_Flow2 + ",");
                             fileWriter.Write(CH1TestResult.RWD_ADCMAX + "," + CH1TestResult.RWD_VDCMAX + "," + CH1TestResult.RWD_Pre1 + "," + CH1TestResult.RWD_Pre2 + ",,,,,,,,,,,,,,,,,,,,,,");
                             fileWriter.Write(CH1TestResult.FullPre1 + "," + CH1TestResult.BalanPre1 + "," + CH1TestResult.Leak1 + ",");
                             fileWriter.Write(CH1TestResult.FullPre2 + "," + CH1TestResult.BalanPre2 + "," + CH1TestResult.Leak2 + ",");
                             fileWriter.Write(CH1TestResult.FWD_FullPre1 + "," + CH1TestResult.FWD_BalanPre1 + "," + CH1TestResult.FWD_Leak1 + ",");
                             fileWriter.Write(CH1TestResult.FWD_FullPre2 + "," + CH1TestResult.FWD_BalanPre2 + "," + CH1TestResult.FWD_Leak2 + ",\n");
                             CH1csvworknum += 1;
                             break;

                         case 2:
                             fileWriter.Write(CH2csvworknum + I18N.GetLangText(dicLang, "右")+ ",#" + CH2timestamp + "#,'" + right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH2Tlight.Text + ",,,,,,,,,,,,,,,,,,,,,,");
                             fileWriter.Write(CH2RTElec.Text + "," + CH2TestResult.UP_ADCMAX + "," + CH2TestResult.UP_VDCMAX + "," + CH2TestResult.UP_Pre + ",");
                             fileWriter.Write(CH2TestResult.UP_Flow + "," + CH2TestResult.DOWN_ADCMAX + "," + CH2TestResult.DOWN_VDCMAX + ",");
                             fileWriter.Write(CH2TestResult.DOWN_Pre + "," + CH2TestResult.DOWN_Flow + "," + CH2TestResult.ElecRatio + ",");
                             fileWriter.Write(CH2TestResult.PressRatio + "," + CH2TestResult.FWD_ADCMAX + "," + CH2TestResult.FWD_VDCMAX + ",");
                             fileWriter.Write(CH2TestResult.FWD_Pre1 + "," + CH2TestResult.FWD_Pre2 + "," + CH2TestResult.FWD_Flow1 + "," + CH2TestResult.FWD_Flow2 + ",");
                             fileWriter.Write(CH2TestResult.RWD_ADCMAX + "," + CH2TestResult.RWD_VDCMAX + "," + CH2TestResult.RWD_Pre1 + "," + CH2TestResult.RWD_Pre2 + ",");
                             fileWriter.Write(",,,,,,,,,,,," + CH2TestResult.FullPre1 + "," + CH2TestResult.BalanPre1 + "," + CH2TestResult.Leak1 + ",");
                             fileWriter.Write(CH2TestResult.FullPre2 + "," + CH2TestResult.BalanPre2 + "," + CH2TestResult.Leak2 + ",");
                             fileWriter.Write(CH2TestResult.FWD_FullPre1 + "," + CH2TestResult.FWD_BalanPre1 + "," + CH2TestResult.FWD_Leak1 + ",");
                             fileWriter.Write(CH2TestResult.FWD_FullPre2 + "," + CH2TestResult.FWD_BalanPre2 + "," + CH2TestResult.FWD_Leak2 + ",\n");
                             CH2csvworknum += 1;
                             break;
                     }
                     fileWriter.Flush();
                     fileWriter.Close();
                     switch (CH)
                     {
                         case 1:

                             if (plc.CH1CodeCount <= 0)
                             {
                                 left_CH1Code.ResetText();
                                 left_CH1Code.ScrollToCaret();
                                 left_CH1Code.Focus();
                                 IntPtr ptr = FindWindow(null, "CH1条码启动");
                                 winforclose.Interval = 300;
                                 flagtime = ptr;
                                 winforclose.Start();
                                 if (ptr == IntPtr.Zero)
                                 {
                                     //   MessageBox.Show("左工位需要扫码启动!", "CH1条码启动", MessageBoxButtons.OK);
                                     // if(Reset == DialogResult.OK)
                                     Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动"));
                                 }
                                 else
                                 {
                                     PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                                 }
                             }
                             break;

                         case 2:
                             if (plc.CH2CodeCount <= 0)
                             {
                                 right_CH1Code.ResetText();
                                 right_CH1Code.ScrollToCaret();
                                 right_CH1Code.Focus();
                                 IntPtr ptr = FindWindow(null, "CH2条码启动");
                                 if (ptr == IntPtr.Zero)
                                 {
                                     //      MessageBox.Show("右工位需要扫码启动!", "CH2条码启动", MessageBoxButtons.OK);
                                     //     if (Reset == DialogResult.OK)
                                     {
                                     }
                                     Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动"));
                                 }
                                 else
                                 {
                                     PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                                 }
                             }
                             break;
                     }
                 }
                 catch (Exception ex)
                 {
                     wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CSV:" + ex.Message);
                     // MessageBox.Show("CSV:" + ex.Message);
                     Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                     Logger.Log(ex.StackTrace);
                 }
             }*/

        private void AddCSV(int CH)
        {
            try
            {
                /* string fileName;
                 string file = DateTime.Now.ToString("yyyyMMdd");
                 string productname = machine.Replace(".ini", "");
                 if (String.IsNullOrEmpty(save.Path))
                 {
                     fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                 }
                 else
                 {
                     fileName = save.Path + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                 }
                 if (!Directory.Exists(fileName))
                 {
                     Directory.CreateDirectory(fileName);
                 }*/
                string fileName;
                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(file))
                        listFileSystemInfo.Add(fsinfo);
                }
                string maxFile = file;
                if (listFileSystemInfo.Count > 0)
                {
                    maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                }

                string name = maxFile + ".csv";
                fileName += name;
                log.MES_Logmsg(DateTime.Now.ToString() + fileName);
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, " 测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");

                    //fileWriter1.Flush();
                    //fileWriter1.Close();
                    {
                        //IsWriteTitle = false;
                        //StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                        /*       fileWriter1.Write(I18N.GetLangText(dicLang, "左1,充气时间") + "：," + ch1_1leakparams.FullTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_1leakparams.BalanTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_1leakparams.TestTime1 + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_1leakparams.ExhaustTime + "s\n");

                               fileWriter1.Write(I18N.GetLangText(dicLang, "左2,充气时间") + "：," + ch1_2leakparams.FullTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_2leakparams.BalanTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_2leakparams.TestTime1 + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_2leakparams.ExhaustTime + "s\n");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "右1,充气时间") + "：," + ch2_1leakparams.FullTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_1leakparams.BalanTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_1leakparams.TestTime1 + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_1leakparams.ExhaustTime + "s\n");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "右2,充气时间") + "：," + ch2_2leakparams.FullTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_2leakparams.BalanTime + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_2leakparams.TestTime1 + "s,");
                               fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_2leakparams.ExhaustTime + "s\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");
                               fileWriter1.Write("\r\n");*/

                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");

                        //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                        //   if (!Electricity.ord.CH1UpDownChange)
                        {
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            //7.24
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Flow.CH1_2PreMax.ToString() + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + PressureUnit.Text + ")," + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + PressureUnit.Text + ")," + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                           fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + "(" + Flow.CH1_1FlowMin + "-" + Flow.CH1_1FlowMax + "),");
                  
                        }
                    

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");

                        //      if (!Electricity.ord.CH2UpDownChange)
                        {
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");

                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Flow.CH2_2PreMax.ToString() + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Flow.CH2_2FlowMax.ToString() + "-" + Flow.CH2_2FlowMin + PressureUnit.Text + ")," + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(lpm)") + "(" + Flow.CH2_2FlowMin + "-" + Flow.CH2_2FlowMax + "),");
                        }
                        //else
                        //{
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                        //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        //}

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                      //  fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                        //fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                       // fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                        //fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");
                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }
                /* if (IsWriteTitle)
                 {
                     IsWriteTitle = false;
                     StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1,充气时间") + "：," + ch1_1leakparams.FullTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_1leakparams.BalanTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_1leakparams.TestTime1 + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_1leakparams.ExhaustTime + "s\n");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2,充气时间") + "：," + ch1_2leakparams.FullTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_2leakparams.BalanTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_2leakparams.TestTime1 + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_2leakparams.ExhaustTime + "s\n");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1,充气时间") + "：," + ch2_1leakparams.FullTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_1leakparams.BalanTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_1leakparams.TestTime1 + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_1leakparams.ExhaustTime + "s\n");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2,充气时间") + "：," + ch2_2leakparams.FullTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_2leakparams.BalanTime + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_2leakparams.TestTime1 + "s,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_2leakparams.ExhaustTime + "s\n");
                     //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果")+ "OK/NG,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                  //   if (!Electricity.ord.CH1UpDownChange)
                     {
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                     }
                     //else
                     //{
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(plm)") + ",");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充）(plm)") + ",");
                     //}

                     fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");

                    // if (!Electricity.ord.CH2UpDownChange)
                     {
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                         fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                     }
                     //else
                     //{
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                     //    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                     //}

                     fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");
                     fileWriter1.Flush();
                     fileWriter1.Close();
                 }*/
                StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                int i = 0;
                switch (CH)
                {
                    case 1:

                        //////////////////////////


                        
                        string[] CSVwritedate = new string[100];
                        fileWriter.Write(
                            CH1csvworknum + I18N.GetLangText(dicLang, "左") + "#,"
                         + CH1timestamp + "#,'"
                         + left_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ","
                         + CH1Tlight.Text + ",");

                        CSVwritedate[0] = CH1csvworknum + I18N.GetLangText(dicLang, "左") + "#,";
                        CSVwritedate[1] = CH1timestamp + "#,'";
                        CSVwritedate[2] = left_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",";
                        CSVwritedate[3] = CH1Tlight.Text + ",";
                   
                        //fileWriter.Write(
                        //    CH1RTElec.Text + "," 
                        //    + CH1TestResult.UP_ADCMAX + ","
                        //    + CH1TestResult.UP_VDCMAX + "," 
                        //    + CH1TestResult.UP_Pre + "," 
                        //    + CH1PressMax.ToString() + ",");
                        CSVwritedate[4] = CH1RTElec.Text;
                        CSVwritedate[5] = CH1TestResult.UP_ADCMAX.ToString() + ",";
                        CSVwritedate[6]= CH1TestResult.UP_VDCMAX.ToString()+ ",";
                        CSVwritedate[7] = CH1TestResult.UP_Pre.ToString() + ",";
                        CSVwritedate[8] = CH1PressMax.ToString() + ",";
                        //fileWriter.Write(CH1RTElec.Text + "," 
                        //    + CH1TestResult.UP_ADCMAX + "," 
                        //    + CH1TestResult.UP_VDCMAX + "," 
                        //    + CH1TestResult.UP_Pre + ","
                        //    + CH1PressMax.ToString() + ",");
                        CSVwritedate[9] = CH1RTElec.Text.ToString() + ",";
                        CSVwritedate[10] = CH1TestResult.UP_ADCMAX.ToString() + ",";
                        CSVwritedate[11] = CH1TestResult.UP_VDCMAX.ToString()+",";
                        CSVwritedate[12] = CH1TestResult.UP_Pre.ToString()+",";
                        CSVwritedate[13] = CH1PressMax.ToString() + ",";
          

                        //fileWriter.Write(CH1TestResult.UP_Flow + "," 
                        //    + up_downFlow.ToString() + "," 
                        //    + CH1TestResult.DOWN_ADCMAX + "," 
                        //    + CH1TestResult.DOWN_VDCMAX + ",");
                        CSVwritedate[14] = CH1TestResult.UP_Flow + ",";
                        CSVwritedate[15] = up_downFlow.ToString() + ",";
                        CSVwritedate[16] = CH1TestResult.DOWN_ADCMAX + ",";
                        CSVwritedate[17] = CH1TestResult.DOWN_VDCMAX + ",";

                        //fileWriter.Write(CH1TestResult.DOWN_Pre + "," +
                           // down_UPPre + "," +
                           //"" + CH1TestResult.DOWN_Flow + "," 
                           //+ down_upFlow + ","
                           //+ CH1TestResult.ElecRatio + ",");
                        CSVwritedate[18] = CH1TestResult.DOWN_Pre + ",";
                        CSVwritedate[19] = down_UPPre + ",";
                        CSVwritedate[20] = CH1TestResult.DOWN_Flow + ",";
                        CSVwritedate[21] = CH1TestResult.ElecRatio + ",";



                        //fileWriter.Write(CH1TestResult.PressRatio + "," 
                        //    + CH1TestResult.FWD_ADCMAX + ","
                        //    + CH1TestResult.FWD_VDCMAX + ",");
                        CSVwritedate[22] = CH1TestResult.PressRatio + ",";
                        CSVwritedate[23] = CH1TestResult.FWD_ADCMAX + ","; 
                        CSVwritedate[24] = CH1TestResult.FWD_VDCMAX + ",";




                        //fileWriter.Write(CH1TestResult.DOWN_Pre + ","
                        //    + down_UPPre + "," +
                        //    "" + CH1TestResult.DOWN_Flow + ","
                        //    + down_upFlow + "," +
                        //    CH1TestResult.ElecRatio + ",");

                        CSVwritedate[25] = CH1TestResult.DOWN_Pre + ",";
                        CSVwritedate[26] = down_UPPre + ",";
                        CSVwritedate[27] = CH1TestResult.DOWN_Flow + ",";
                        CSVwritedate[28] = down_upFlow + ",";
                        CSVwritedate[29] = CH1TestResult.ElecRatio + ",";


                        //fileWriter.Write(CH1TestResult.PressRatio + "," + 
                        //    CH1TestResult.FWD_ADCMAX + "," +
                        //    CH1TestResult.FWD_VDCMAX + ",");

                        CSVwritedate[30] = CH1TestResult.PressRatio + ",";
                        CSVwritedate[31] = CH1TestResult.FWD_ADCMAX + ",";
                        CSVwritedate[32] = CH1TestResult.FWD_VDCMAX + ",";

                        //fileWriter.Write(CH1TestResult.FWD_Pre1 + "," + 
                        //    CH1TestResult.FWD_Pre2 + ","
                        //    + CH1TestResult.FWD_Flow1 + ","
                        //    + CH1TestResult.FWD_Flow2 + ",");
                        CSVwritedate[33] = CH1TestResult.FWD_Pre1 + ",";
                        CSVwritedate[34] = CH1TestResult.FWD_Pre2 + ",";
                        CSVwritedate[35] = CH1TestResult.FWD_Flow1 + ",";
                        CSVwritedate[36] = CH1TestResult.FWD_Flow2 + ",";


                        //fileWriter.Write(CH1TestResult.RWD_ADCMAX + ","
                        //    + CH1TestResult.RWD_VDCMAX + "," 
                        //    + CH1TestResult.RWD_Pre1 + "," 
                        //    + CH1TestResult.RWD_Pre2 + "," +
                        //    ",,,,,,,,,,,,,,,,,,,,,");

                        CSVwritedate[37] = CH1TestResult.RWD_ADCMAX + ",";
                        CSVwritedate[38] = CH1TestResult.RWD_VDCMAX + ",";
                        CSVwritedate[39] = CH1TestResult.RWD_Pre1 + ",";
                        CSVwritedate[40] = CH1TestResult.RWD_Pre2 + ",";
                        for (i = 0; i < 20; i++)
                        {
                            CSVwritedate[41 + i] = ",";
                        }
                        fileWriter.Write(CH1TestResult.FullPre1 + ","
                            + CH1TestResult.BalanPre1 + "," 
                            + CH1TestResult.Leak1 + ",");

                        CSVwritedate[60] = CH1TestResult.FullPre1 + ",";
                        CSVwritedate[61] = CH1TestResult.BalanPre1 + ",";
                        CSVwritedate[62] = CH1TestResult.Leak1 + ",";


                        fileWriter.Write(CH1TestResult.FullPre2 + "," + CH1TestResult.BalanPre2 + "," + CH1TestResult.Leak2 + ",");
                        fileWriter.Write(CH1TestResult.FWD_FullPre1 + "," + CH1TestResult.FWD_BalanPre1 + "," + CH1TestResult.FWD_Leak1 + ",");
                        fileWriter.Write(CH1TestResult.FWD_FullPre2 + "," + CH1TestResult.FWD_BalanPre2 + "," + CH1TestResult.FWD_Leak2 + ",\n");
                        CH1csvworknum += 1;


                        string csvall = "";
                        
                        for (i = 0; i < 14; i++)
                        {
                            csvall += CSVwritedate[i];
                        }
                        fileWriter.Write(csvall);







                        ////////////////////////////
                        //fileWriter.Write(CH1csvworknum + I18N.GetLangText(dicLang, "左") + ",#" + CH1timestamp + "#,'" + left_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH1Tlight.Text + ",");
                        //fileWriter.Write(CH1RTElec.Text + "," + CH1TestResult.UP_ADCMAX + "," + CH1TestResult.UP_VDCMAX + "," + CH1TestResult.UP_Pre + "," + CH1PressMax.ToString() + ",");
                        //fileWriter.Write(CH1TestResult.UP_Flow + "," + up_downFlow.ToString() + "," + CH1TestResult.DOWN_ADCMAX + "," + CH1TestResult.DOWN_VDCMAX + ",");
                        //fileWriter.Write(CH1TestResult.DOWN_Pre + "," + down_UPPre + "," +
                        //    "" + CH1TestResult.DOWN_Flow + "," + down_upFlow + "," + CH1TestResult.ElecRatio + ",");
                        //fileWriter.Write(CH1TestResult.PressRatio + "," + CH1TestResult.FWD_ADCMAX + "," + CH1TestResult.FWD_VDCMAX + ",");
                        ////fileWriter.Write(CH1TestResult.PressRatio + "," + CH1RTADC.Text + "," + CH1RTVDC.Text + ",");
                        ////fileWriter.Write(CH1TestResult.PressRatio + "," + CH1TestResult.FWD_ADCMAX + "," + CH1TestResult.FWD_VDCMAX + ",");
                        //fileWriter.Write(CH1TestResult.FWD_Pre1 + "," + CH1TestResult.FWD_Pre2 + "," + CH1TestResult.FWD_Flow1 + "," + CH1TestResult.FWD_Flow2 + ",");
                        //fileWriter.Write(CH1TestResult.RWD_ADCMAX + "," + CH1TestResult.RWD_VDCMAX + "," + CH1TestResult.RWD_Pre1 + "," + CH1TestResult.RWD_Pre2 + ",,,,,,,,,,,,,,,,,,,,,,");
                        //fileWriter.Write(CH1TestResult.FullPre1 + "," + CH1TestResult.BalanPre1 + "," + CH1TestResult.Leak1 + ",");
                        //fileWriter.Write(CH1TestResult.FullPre2 + "," + CH1TestResult.BalanPre2 + "," + CH1TestResult.Leak2 + ",");
                        //fileWriter.Write(CH1TestResult.FWD_FullPre1 + "," + CH1TestResult.FWD_BalanPre1 + "," + CH1TestResult.FWD_Leak1 + ",");
                        //fileWriter.Write(CH1TestResult.FWD_FullPre2 + "," + CH1TestResult.FWD_BalanPre2 + "," + CH1TestResult.FWD_Leak2 + ",\n");
                        //CH1csvworknum += 1;
                        break;

                    case 2:
                        fileWriter.Write(CH2csvworknum + I18N.GetLangText(dicLang, "右") + ",#" + CH2timestamp + "#,'" + right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH2Tlight.Text + ",,,,,,,,,,,,,,,,,,,,,,,,,,,,");
                        fileWriter.Write(CH2RTElec.Text + "," + CH2TestResult.UP_ADCMAX + "," + CH2TestResult.UP_VDCMAX + "," + CH2TestResult.UP_Pre + "," + CH3PressMax.ToString() + ",");
                        fileWriter.Write(CH2TestResult.UP_Flow + "," + CH4Q.ToString() + "," + CH2TestResult.DOWN_ADCMAX + "," + CH2TestResult.DOWN_VDCMAX + ",");
                        fileWriter.Write(CH2TestResult.DOWN_Pre + "," + down_UPPre2 + "," +
                            CH2TestResult.DOWN_Flow + "," + CH2TestResult.ElecRatio + ",");
                        fileWriter.Write(CH2TestResult.PressRatio + "," + CH2TestResult.FWD_ADCMAX + "," + CH2TestResult.FWD_VDCMAX + ",");
                        //  fileWriter.Write(CH2TestResult.PressRatio + "," + CH2RTADC.Text + "," + CH2RTVDC.Text + ",");
                        fileWriter.Write(CH2TestResult.FWD_Pre1 + "," + CH2TestResult.FWD_Pre2 + "," + CH2TestResult.FWD_Flow1 + "," + CH2TestResult.FWD_Flow2 + ",");
                        fileWriter.Write(CH2TestResult.RWD_ADCMAX + "," + CH2TestResult.RWD_VDCMAX + "," + CH2TestResult.RWD_Pre1 + "," + CH2TestResult.RWD_Pre2 + ",");
                        fileWriter.Write(",,,,,,,,,,,," + CH2TestResult.FullPre1 + "," + CH2TestResult.BalanPre1 + "," + CH2TestResult.Leak1 + ",");
                        fileWriter.Write(CH2TestResult.FullPre2 + "," + CH2TestResult.BalanPre2 + "," + CH2TestResult.Leak2 + ",");
                        fileWriter.Write(CH2TestResult.FWD_FullPre1 + "," + CH2TestResult.FWD_BalanPre1 + "," + CH2TestResult.FWD_Leak1 + ",");
                        fileWriter.Write(CH2TestResult.FWD_FullPre2 + "," + CH2TestResult.FWD_BalanPre2 + "," + CH2TestResult.FWD_Leak2 + ",\n");
                        CH2csvworknum += 1;
                        break;
                }
                fileWriter.Flush();
                fileWriter.Close();
                switch (CH)
                {
                    case 1:

                        if (plc.CH1CodeCount <= 0)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.ScrollToCaret();
                            left_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH1条码启动");
                            winforclose.Interval = 300;
                            flagtime = ptr;
                            winforclose.Start();
                            if (ptr == IntPtr.Zero)
                            {
                                //   MessageBox.Show("左工位需要扫码启动!", "CH1条码启动", MessageBoxButtons.OK);
                                // if(Reset == DialogResult.OK)
                                Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;

                    case 2:
                        if (plc.CH2CodeCount <= 0)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.ScrollToCaret();
                            right_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH2条码启动");
                            if (ptr == IntPtr.Zero)
                            {
                                //      MessageBox.Show("右工位需要扫码启动!", "CH2条码启动", MessageBoxButtons.OK);
                                //     if (Reset == DialogResult.OK)
                                {
                                }
                                Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CSV:" + ex.Message);
                // MessageBox.Show("CSV:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }


        private void AddCSV2(int CH)
        {
            try
            {

                string fileName;

                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(file))
                        listFileSystemInfo.Add(fsinfo);
                }
                string maxFile = file;
                if (listFileSystemInfo.Count > 0)
                {
                    maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                }

                string name = maxFile + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, " 测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");


                    {



                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");

                        //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                        //   if (!Electricity.ord.CH1UpDownChange)
                        {
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                         //   fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Flow.CH1_2PreMax.ToString() + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                       //     fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + PressureUnit.Text + ")," + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                      //      fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");

                        }


                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");

                            fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                     //       fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Flow.CH2_2PreMax.ToString() + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                  //          fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Flow.CH2_2FlowMax.ToString() + "-" + Flow.CH2_2FlowMin + PressureUnit.Text + ")," + ",");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                    //       fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                   


                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                    //    fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                  //     fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                  //      fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                  //      fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                //       fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                   //     fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
               //         fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");
                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }

                StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                switch (CH)
                {
                    case 1:
                        fileWriter.Write(CH1csvworknum + I18N.GetLangText(dicLang, "左") + ",#");//作业序号
                        fileWriter.Write(CH1timestamp + ",#");//测试时间
                        fileWriter.Write(left_CH1Code
.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH1Tlight.Text + ",");//判定结果
                        fileWriter.Write(CH1RTElec.Text + ",");//左avg静态电流

                        fileWriter.Write(CH1TestResult.UP_ADCMAX + ",");//"左max电流(上充)”
                        fileWriter.Write(CH1TestResult.UP_VDCMAX + ",");//左max电压(上充)
                        fileWriter.Write(CH1TestResult.UP_Pre + ",");//左输出压力(上充)”
                   //     fileWriter.Write("999" + ",");//左输出压力(上下充)”
                        fileWriter.Write(CH1TestResult.UP_Flow + ",");//左max流量（上充）
                    //    fileWriter.Write("999" + ",");//左max流量（上下充）
                        fileWriter.Write(CH1TestResult.DOWN_ADCMAX + ",");//左max电流（下充）
                        fileWriter.Write(CH1TestResult.DOWN_VDCMAX + ",");//左max电压（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Pre + ",");//左输出压力（下充）
                   //     fileWriter.Write(999 + ",");//左输出压力（下上充）
                        fileWriter.Write(CH1TestResult.DOWN_Flow + ",");//左max流量（下充）


                        fileWriter.Write(CH1TestResult.ElecRatio + ",");//左电流对比值
                        fileWriter.Write(CH1TestResult.PressRatio + ",");//左压力对比值
                        fileWriter.Write(CH1TestResult.FWD_ADCMAX + ",");//左max电流（同充）
                        fileWriter.Write(CH1TestResult.FWD_VDCMAX + ",");//左max电压（同充）
                        fileWriter.Write(CH1TestResult.FWD_Pre1 + ",");//左输出压力（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Pre2 + ",");//左输出压力（同充下）
                        fileWriter.Write(CH1TestResult.FWD_Flow1 + ",");//左max流量（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Flow2 + ",");//左max流量（同充下）
                        fileWriter.Write(CH1TestResult.RWD_ADCMAX + ",");//左max电流（泄气）
                        fileWriter.Write(CH1TestResult.RWD_VDCMAX + ",");//左max电压（泄气）
                        fileWriter.Write(CH1TestResult.RWD_Pre1 + ",");//左输出压力（泄气上）
                        fileWriter.Write(CH1TestResult.RWD_Pre2 + ",");//左输出压力（泄气下）

                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,");


                        fileWriter.Write(CH1TestResult.FullPre1 + ",");//左1压力（充气）
                        fileWriter.Write(CH1TestResult.BalanPre1 + ",");//左1压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak1 + ",");//左1泄漏量
                        fileWriter.Write(CH1TestResult.FullPre2 + ",");//左2压力（充气）
                        fileWriter.Write(CH1TestResult.BalanPre2 + ",");//左2压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak2 + ",");//左2泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre1 + ",");//左1同充压力（充气）
                        fileWriter.Write(CH1TestResult.FWD_BalanPre1 + ",");//左1同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak1 + ",");//左1同充泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre2 + ","); //左2同充压力（充气）
                        fileWriter.Write(CH1TestResult.FWD_BalanPre2 + ",");// 左2同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak2 + ",\n");// 左2同充泄漏量


                        CH1csvworknum += 1;
                        break;

                    case 2:


                        fileWriter.Write(CH2csvworknum + I18N.GetLangText(dicLang, "右") + ",#");//作业序号
                        fileWriter.Write(CH2timestamp + ",#");//测试时间

                        fileWriter.Write(right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH2Tlight.Text + ",");//判定结果
                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,,");

                        fileWriter.Write(CH2RTElec.Text + ",");//右avg静态电流 1
                        fileWriter.Write(CH2TestResult.UP_ADCMAX + ",");//"右max电流(上充)” 2
                          //     fileWriter.Write(CH2TestResult.UP_Pre + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_VDCMAX + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_Pre + ",");//"右输出压力(上充)(40-45KPa)4
                        fileWriter.Write(CH2TestResult.UP_Flow + ",");//右max流量(上充)(plm)5
                        fileWriter.Write(CH2TestResult.DOWN_ADCMAX + ",");//右max电流(下充)(0.3-1.2A)”6
                                                                   
                        fileWriter.Write(CH2TestResult.DOWN_VDCMAX + ",");//右max电压(下充)(14.4-14.6V)7
                                                                  
                        fileWriter.Write(CH2TestResult.DOWN_Pre + ",");//右输出压力(下充)(40-45KPa)8
                        fileWriter.Write(CH2TestResult.DOWN_Pre1 + ",");//右输出压力(下上充)(40-45KPa)8新增
                        fileWriter.Write(CH2TestResult.DOWN_Flow + ",");//右max流量(下充)(plm)9
                        fileWriter.Write(CH2TestResult.ElecRatio + ",");//右电流对比值(0.5-1.2)10

                        fileWriter.Write(CH2TestResult.PressRatio + ",");//右压力对比值(0-1.35)11


                        fileWriter.Write(CH2TestResult.FWD_ADCMAX + ",");//右max电流(同充)(0.3-1.2)A12
                        fileWriter.Write(CH2TestResult.FWD_VDCMAX + ",");//右max电压(同充)(14.4-14.6)A13
                        fileWriter.Write(CH2TestResult.FWD_Pre1 + ",");//右输出压力（同充上）14
                        fileWriter.Write(CH2TestResult.FWD_Pre2 + ",");//右输出压力(同充下)15
                        fileWriter.Write(CH2TestResult.FWD_Flow1 + ",");//右max流量(同充上)(plm)16

                        fileWriter.Write(CH2TestResult.FWD_Flow2 + ",");//右max流量(同充下)(plm)17
                        fileWriter.Write(CH2TestResult.RWD_ADCMAX + ",");//右max电流(泄气)(0.2-0.6)18
                        fileWriter.Write(CH2TestResult.RWD_VDCMAX + ",");//右max电压(泄气)(14.4-14.6)19

                        fileWriter.Write(CH2TestResult.RWD_Pre1 + ",");//右输出压力(泄气上)(0-5KPa)20
                        fileWriter.Write(CH2TestResult.RWD_Pre2 + ",");// 右输出压力(泄气下)(0 - 5KPa)21

                     

                        fileWriter.Write(",,,,,,,,,,,,");


                        fileWriter.Write(CH2TestResult.FullPre1 + ",");//右1压力（充气）
                        fileWriter.Write(CH2TestResult.BalanPre1 + ",");//右1压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak1 + ",");//右1泄漏量
                        fileWriter.Write(CH2TestResult.FullPre2 + ",");//右2压力（充气）
                        fileWriter.Write(CH2TestResult.BalanPre2 + ",");//右2压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak2 + ",");//右2泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre1 + ",");//右1同充压力（充气）
                        fileWriter.Write(CH2TestResult.FWD_BalanPre1 + ",");//右1同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak1 + ",");//右1同充泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre2 + ","); //右2同充压力（充气）
                        fileWriter.Write(CH2TestResult.FWD_BalanPre2 + ",");// 右2同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak2 + ",\n");// 右2同充泄漏量


                        CH2csvworknum += 1;
                        break;
                }
                fileWriter.Flush();
                fileWriter.Close();
                switch (CH)
                {
                    case 1:

                        if (plc.CH1CodeCount <= 0)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.ScrollToCaret();
                            left_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH1条码启动");
                            winforclose.Interval = 300;
                            flagtime = ptr;
                            winforclose.Start();
                            if (ptr == IntPtr.Zero)
                            {
                                //   MessageBox.Show("左工位需要扫码启动!", "CH1条码启动", MessageBoxButtons.OK);
                                // if(Reset == DialogResult.OK)
                                Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;

                    case 2:
                        if (plc.CH2CodeCount <= 0)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.ScrollToCaret();
                            right_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH2条码启动");
                            if (ptr == IntPtr.Zero)
                            {
                                //      MessageBox.Show("右工位需要扫码启动!", "CH2条码启动", MessageBoxButtons.OK);
                                //     if (Reset == DialogResult.OK)
                                {
                                }
                                Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CSV:" + ex.Message);
                // MessageBox.Show("CSV:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        //新增参数
        private void AddCSV3(int CH)
        {
            try
            {

                string fileName;

                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(file))
                        listFileSystemInfo.Add(fsinfo);
                }
                string maxFile = file;
                if (listFileSystemInfo.Count > 0)
                {
                    maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                }

                string name = maxFile + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, " 测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");


                    {



                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");

        
                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                     fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Flow.CH1_2PreMax.ToString() + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                             fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + PressureUnit.Text + ")," );
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                            fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                       fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Flow.CH2_2PreMax.ToString() + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                          fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Flow.CH2_2FlowMax.ToString() + "-" + Flow.CH2_2FlowMin + PressureUnit.Text + ")," );
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");


                //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), "", "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "");
                  //      CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), "", "Kpa", elec.TotalPreMax.ToString(), "0", "");
                   //     CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" +"0" + "-" + elec.TotalPreMax + "),");
                      

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                 //       fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                 //       fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                       fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                  //   fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                     //   fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
               //   fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                   //     fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");

                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }

                StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                switch (CH)
                {
                    case 1:
                        fileWriter.Write(CH1csvworknum + I18N.GetLangText(dicLang, "左") + ",#");//作业序号
                        fileWriter.Write(CH1timestamp + ",#");//测试时间
                        fileWriter.Write(left_CH1Code
.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH1Tlight.Text + ",");//判定结果
                        fileWriter.Write(CH1RTElec.Text + ",");//左avg静态电流

                        fileWriter.Write(CH1TestResult.UP_ADCMAX + ",");//"左max电流(上充)”
                        fileWriter.Write(CH1TestResult.UP_VDCMAX + ",");//左max电压(上充)
                        fileWriter.Write(CH1TestResult.UP_Pre + ",");//左输出压力(上充)”
                        fileWriter.Write(CH1TestResult.UP_Prezuo + ",");//左输出压力(上下充)”
                        fileWriter.Write(CH1TestResult.UP_Flow + ",");//左max流量（上充）
                      fileWriter.Write(CH1TestResult.UP_Flowzuo + ",");//左max流量（上下充）
                        fileWriter.Write(CH1TestResult.DOWN_ADCMAX + ",");//左max电流（下充）
                        fileWriter.Write(CH1TestResult.DOWN_VDCMAX + ",");//左max电压（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Pre + ",");//左输出压力（下充）
                     fileWriter.Write(CH1TestResult.DOWN_Prezuo + ",");//左输出压力（下上充）
                        fileWriter.Write(CH1TestResult.DOWN_Flow + ",");//左max流量（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Flowzuo + ",");//左max流量（下上充）




                        fileWriter.Write(CH1TestResult.ElecRatio + ",");//左电流对比值
                        fileWriter.Write(CH1TestResult.PressRatio + ",");//左压力对比值

                        fileWriter.Write(CH1TestResult.FWD_FlowSumzuo + ",");//左流量总FWD新增
                        fileWriter.Write(CH1TestResult.FWD_PreSumzuo + ",");//左压差总FWD新增

                        fileWriter.Write(CH1TestResult.FWD_ADCMAX + ",");//左max电流（同充）
                        fileWriter.Write(CH1TestResult.FWD_VDCMAX + ",");//左max电压（同充）
                        fileWriter.Write(CH1TestResult.FWD_Pre1 + ",");//左输出压力（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Pre2 + ",");//左输出压力（同充下）
                        fileWriter.Write(CH1TestResult.FWD_Flow1 + ",");//左max流量（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Flow2 + ",");//左max流量（同充下）
                        fileWriter.Write(CH1TestResult.RWD_ADCMAX + ",");//左max电流（泄气）
                        fileWriter.Write(CH1TestResult.RWD_VDCMAX + ",");//左max电压（泄气）
                        fileWriter.Write(CH1TestResult.RWD_Pre1 + ",");//左输出压力（泄气上）
                        fileWriter.Write(CH1TestResult.RWD_Pre2 + ",");//左输出压力（泄气下）

                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,,,,,,,,");


                        fileWriter.Write(CH1TestResult.FullPre1 + ",");//左1压力（充气）
                  //      fileWriter.Write(CH1TestResult.BalanPre1 + ",");//左1压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak1 + ",");//左1泄漏量
                        fileWriter.Write(CH1TestResult.FullPre2 + ",");//左2压力（充气）
                  //      fileWriter.Write(CH1TestResult.BalanPre2 + ",");//左2压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak2 + ",");//左2泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre1 + ",");//左1同充压力（充气）
                  //      fileWriter.Write(CH1TestResult.FWD_BalanPre1 + ",");//左1同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak1 + ",");//左1同充泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre2 + ","); //左2同充压力（充气）
                //        fileWriter.Write(CH1TestResult.FWD_BalanPre2 + ",");// 左2同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak2 + ",\n");// 左2同充泄漏量


                        CH1csvworknum += 1;
                        break;

                    case 2:


                        fileWriter.Write(CH2csvworknum + I18N.GetLangText(dicLang, "右") + ",#");//作业序号
                        fileWriter.Write(CH2timestamp + ",#");//测试时间

                        fileWriter.Write(right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH2Tlight.Text + ",");//判定结果
                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,,,,,,,,");

                        fileWriter.Write(CH2RTElec.Text + ",");//右avg静态电流 1
                        fileWriter.Write(CH2TestResult.UP_ADCMAX + ",");//"右max电流(上充)” 2
                                                                        //     fileWriter.Write(CH2TestResult.UP_Pre + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_VDCMAX + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_Pre + ",");//"右输出压力(上充)(40-45KPa)4
                        fileWriter.Write(CH2TestResult.UP_Pre1 + ",");//"右输出压力(上充)(40-45KPa)新增
                        fileWriter.Write(CH2TestResult.UP_Flow + ",");//右max流量(上充)(plm)5
                        fileWriter.Write(CH2TestResult.UP_Flow1 + ",");//右max流量(上下充)(plm)5新增
                        fileWriter.Write(CH2TestResult.DOWN_ADCMAX + ",");//右max电流(下充)(0.3-1.2A)”6

                        fileWriter.Write(CH2TestResult.DOWN_VDCMAX + ",");//右max电压(下充)(14.4-14.6V)7

                        fileWriter.Write(CH2TestResult.DOWN_Pre + ",");//右输出压力(下充)(40-45KPa)8
                        fileWriter.Write(CH2TestResult.DOWN_Pre1 + ",");//右输出压力(下上充)(40-45KPa)8新增
                        fileWriter.Write(CH2TestResult.DOWN_Flow + ",");//右max流量(下充)(plm)9
                        fileWriter.Write(CH2TestResult.DOWN_Flow1 + ",");//右max流量(下上充)(plm)9新增
                        fileWriter.Write(CH2TestResult.ElecRatio + ",");//右电流对比值(0.5-1.2)10

                        fileWriter.Write(CH2TestResult.PressRatio + ",");//右压力对比值(0-1.35)11

                        fileWriter.Write(CH2TestResult.FWD_FlowSum + ",");//右流量总FWD新增
                        fileWriter.Write(CH2TestResult.FWD_PreSum + ",");//右压差总FWD新增


                        fileWriter.Write(CH2TestResult.FWD_ADCMAX + ",");//右max电流(同充)(0.3-1.2)A12
                        fileWriter.Write(CH2TestResult.FWD_VDCMAX + ",");//右max电压(同充)(14.4-14.6)A13
                        fileWriter.Write(CH2TestResult.FWD_Pre1 + ",");//右输出压力（同充上）14
                        fileWriter.Write(CH2TestResult.FWD_Pre2 + ",");//右输出压力(同充下)15
                        fileWriter.Write(CH2TestResult.FWD_Flow1 + ",");//右max流量(同充上)(plm)16

                        fileWriter.Write(CH2TestResult.FWD_Flow2 + ",");//右max流量(同充下)(plm)17
                        fileWriter.Write(CH2TestResult.RWD_ADCMAX + ",");//右max电流(泄气)(0.2-0.6)18
                        fileWriter.Write(CH2TestResult.RWD_VDCMAX + ",");//右max电压(泄气)(14.4-14.6)19

                        fileWriter.Write(CH2TestResult.RWD_Pre1 + ",");//右输出压力(泄气上)(0-5KPa)20
                        fileWriter.Write(CH2TestResult.RWD_Pre2 + ",");// 右输出压力(泄气下)(0 - 5KPa)21



                        fileWriter.Write(",,,,,,,,");


                        fileWriter.Write(CH2TestResult.FullPre1 + ",");//右1压力（充气）
                  
                        
                        //    fileWriter.Write(CH2TestResult.BalanPre1 + ",");//右1压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak1 + ",");//右1泄漏量
                        fileWriter.Write(CH2TestResult.FullPre2 + ",");//右2压力（充气）
                  //      fileWriter.Write(CH2TestResult.BalanPre2 + ",");//右2压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak2 + ",");//右2泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre1 + ",");//右1同充压力（充气）
              //          fileWriter.Write(CH2TestResult.FWD_BalanPre1 + ",");//右1同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak1 + ",");//右1同充泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre2 + ","); //右2同充压力（充气）
                      //  fileWriter.Write(CH2TestResult.FWD_BalanPre2 + ",");// 右2同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak2 + ",\n");// 右2同充泄漏量


                        CH2csvworknum += 1;
                        break;
                }
                fileWriter.Flush();
                fileWriter.Close();
                switch (CH)
                {
                    case 1:

                        if (plc.CH1CodeCount <= 0)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.ScrollToCaret();
                            left_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH1条码启动");
                            winforclose.Interval = 300;
                            flagtime = ptr;
                            winforclose.Start();
                            if (ptr == IntPtr.Zero)
                            {
                                //   MessageBox.Show("左工位需要扫码启动!", "CH1条码启动", MessageBoxButtons.OK);
                                // if(Reset == DialogResult.OK)
                                Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;

                    case 2:
                        if (plc.CH2CodeCount <= 0)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.ScrollToCaret();
                            right_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH2条码启动");
                            if (ptr == IntPtr.Zero)
                            {
                                //      MessageBox.Show("右工位需要扫码启动!", "CH2条码启动", MessageBoxButtons.OK);
                                //     if (Reset == DialogResult.OK)
                                {
                                }
                                Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CSV:" + ex.Message);
                // MessageBox.Show("CSV:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }


        private void AddCSV4(int CH)
        {
            try
            {

                string fileName;

                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = Form1.f1.machine.Replace(".ini", "");
                if (String.IsNullOrEmpty(Form1.f1.save.Path))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = Form1.f1.save.Path + "\\" + productname + "\\" + Form1.f1.WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                List<FileSystemInfo> listFileSystemInfo = new List<FileSystemInfo>();
                DirectoryInfo d = new DirectoryInfo(fileName);
                FileSystemInfo[] fsinfos = d.GetFileSystemInfos();
                foreach (FileSystemInfo fsinfo in fsinfos)
                {
                    if (Path.GetFileNameWithoutExtension(fsinfo.FullName).StartsWith(file))
                        listFileSystemInfo.Add(fsinfo);
                }
                string maxFile = file;
                if (listFileSystemInfo.Count > 0)
                {
                    maxFile = Path.GetFileNameWithoutExtension(listFileSystemInfo[listFileSystemInfo.Count - 1].FullName);
                }

                string name = maxFile + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, " 测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");


                    {



                        Electricity electricity = new Electricity();
                        Model.CH_PARAMS param = new Model.CH_PARAMS();
                        param = electricity.readCHParam(1, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(1, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(2, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(2, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(3, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH1阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        param = electricity.readCHParam(4, 2);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2仪器,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");

                        param = electricity.readCHParam(4, 3);
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右CH2阀泵,充气时间") + "：," + param.FullTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + param.BalanTime + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + param.TestTime1 + "s,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + param.ExhaustTime + "s\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");
                        fileWriter1.Write("\r\n");


                        fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号") + "," + I18N.GetLangText(dicLang, "测试时间") + "," + I18N.GetLangText(dicLang, "条形码") + "," + I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上下充)") + "(" + Flow.CH1_2PreMax.ToString() + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上下充)(lpm)") + "(" + Flow.CH1_2FlowMax.ToString() + "-" + Flow.CH1_2FlowMin + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下上充)(lpm)") + ",");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + "(" + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上下充)") + "(" + Flow.CH2_2PreMax.ToString() + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上下充)(lpm)") + "(" + Flow.CH2_2FlowMax.ToString() + "-" + Flow.CH2_2FlowMin + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下上充)(plm)") + ",");



                        fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");


                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), "", "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "");
                        //      CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), "", "Kpa", elec.TotalPreMax.ToString(), "0", "");
                        //     CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max流量(FWD总)") + "(" + elec.TotalFlowMin + "-" + elec.TotalFlowMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "max压力差(FWD)") + "(" + "0" + "-" + elec.TotalPreMax + "),");


                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(plm)") + ",");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");

                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                        //       fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                        //       fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "+(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                        //   fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                        //   fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                        //   fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                        //   fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                        //   fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                        //     fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                        fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");

                        fileWriter1.Flush();
                        fileWriter1.Close();
                    }
                }

                StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);
                switch (CH)
                {
                    case 1:
                        fileWriter.Write(CH1csvworknum + I18N.GetLangText(dicLang, "左") + ",#");//作业序号
                        fileWriter.Write(CH1timestamp + ",#");//测试时间
                        fileWriter.Write(left_CH1Code
.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH1Tlight.Text + ",");//判定结果
                        fileWriter.Write(CH1RTElec.Text + ",");//左avg静态电流

                        fileWriter.Write(CH1TestResult.UP_ADCMAX + ",");//"左max电流(上充)”
                        fileWriter.Write(CH1TestResult.UP_VDCMAX + ",");//左max电压(上充)
                        fileWriter.Write(CH1TestResult.UP_Pre + ",");//左输出压力(上充)”
                        fileWriter.Write(CH1TestResult.UP_Prezuo + ",");//左输出压力(上下充)”
                        fileWriter.Write(CH1TestResult.UP_Flow + ",");//左max流量（上充）
                        fileWriter.Write(CH1TestResult.UP_Flowzuo + ",");//左max流量（上下充）
                        fileWriter.Write(CH1TestResult.DOWN_ADCMAX + ",");//左max电流（下充）
                        fileWriter.Write(CH1TestResult.DOWN_VDCMAX + ",");//左max电压（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Pre + ",");//左输出压力（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Prezuo + ",");//左输出压力（下上充）
                        fileWriter.Write(CH1TestResult.DOWN_Flow + ",");//左max流量（下充）
                        fileWriter.Write(CH1TestResult.DOWN_Flowzuo + ",");//左max流量（下上充）




                        fileWriter.Write(CH1TestResult.ElecRatio + ",");//左电流对比值
                        fileWriter.Write(CH1TestResult.PressRatio + ",");//左压力对比值

                        fileWriter.Write(CH1TestResult.FWD_FlowSumzuo + ",");//左流量总FWD新增
                        fileWriter.Write(CH1TestResult.FWD_PreSumzuo + ",");//左压差总FWD新增

                        fileWriter.Write(CH1TestResult.FWD_ADCMAX + ",");//左max电流（同充）
                        fileWriter.Write(CH1TestResult.FWD_VDCMAX + ",");//左max电压（同充）
                        fileWriter.Write(CH1TestResult.FWD_Pre1 + ",");//左输出压力（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Pre2 + ",");//左输出压力（同充下）
                        fileWriter.Write(CH1TestResult.FWD_Flow1 + ",");//左max流量（同充上）
                        fileWriter.Write(CH1TestResult.FWD_Flow2 + ",");//左max流量（同充下）
                        fileWriter.Write(CH1TestResult.RWD_ADCMAX + ",");//左max电流（泄气）
                        fileWriter.Write(CH1TestResult.RWD_VDCMAX + ",");//左max电压（泄气）
                        fileWriter.Write(CH1TestResult.RWD_Pre1 + ",");//左输出压力（泄气上）
                        fileWriter.Write(CH1TestResult.RWD_Pre2 + ",");//左输出压力（泄气下）

                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,,,,,,,,");


                        fileWriter.Write(CH1TestResult.FullPre1 + ",");//左1压力（充气）
                                                                       //      fileWriter.Write(CH1TestResult.BalanPre1 + ",");//左1压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak1 + ",");//左1泄漏量
                        fileWriter.Write(CH1TestResult.FullPre2 + ",");//左2压力（充气）
                                                                       //      fileWriter.Write(CH1TestResult.BalanPre2 + ",");//左2压力（平衡）
                        fileWriter.Write(CH1TestResult.Leak2 + ",");//左2泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre1 + ",");//左1同充压力（充气）
                                                                           //      fileWriter.Write(CH1TestResult.FWD_BalanPre1 + ",");//左1同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak1 + ",");//左1同充泄漏量
                        fileWriter.Write(CH1TestResult.FWD_FullPre2 + ","); //左2同充压力（充气）
                                                                            //        fileWriter.Write(CH1TestResult.FWD_BalanPre2 + ",");// 左2同充压力（平衡）
                        fileWriter.Write(CH1TestResult.FWD_Leak2 + ",\n");// 左2同充泄漏量


                        CH1csvworknum += 1;
                        break;

                    case 2:


                        fileWriter.Write(CH2csvworknum + I18N.GetLangText(dicLang, "右") + ",#");//作业序号
                        fileWriter.Write(CH2timestamp + ",#");//测试时间

                        fileWriter.Write(right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + ",");//条形码
                        fileWriter.Write(CH2Tlight.Text + ",");//判定结果
                        fileWriter.Write(",,,,,,,,,,,,,,,,,,,,,,,,,,,");

                        fileWriter.Write(CH2RTElec.Text + ",");//右avg静态电流 1
                        fileWriter.Write(CH2TestResult.UP_ADCMAX + ",");//"右max电流(上充)” 2
                                                                        //     fileWriter.Write(CH2TestResult.UP_Pre + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_VDCMAX + ",");//右max电压(上充)”3
                        fileWriter.Write(CH2TestResult.UP_Pre + ",");//"右输出压力(上充)(40-45KPa)4
                        fileWriter.Write(CH2TestResult.UP_Pre1 + ",");//"右输出压力(上充)(40-45KPa)新增
                        fileWriter.Write(CH2TestResult.UP_Flow + ",");//右max流量(上充)(plm)5
                        fileWriter.Write(CH2TestResult.UP_Flow1 + ",");//右max流量(上下充)(plm)5新增
                        fileWriter.Write(CH2TestResult.DOWN_ADCMAX + ",");//右max电流(下充)(0.3-1.2A)”6

                        fileWriter.Write(CH2TestResult.DOWN_VDCMAX + ",");//右max电压(下充)(14.4-14.6V)7

                        fileWriter.Write(CH2TestResult.DOWN_Pre + ",");//右输出压力(下充)(40-45KPa)8
                        fileWriter.Write(CH2TestResult.DOWN_Pre1 + ",");//右输出压力(下上充)(40-45KPa)8新增
                        fileWriter.Write(CH2TestResult.DOWN_Flow + ",");//右max流量(下充)(plm)9
                        fileWriter.Write(CH2TestResult.DOWN_Flow1 + ",");//右max流量(下上充)(plm)9新增
                        fileWriter.Write(CH2TestResult.ElecRatio + ",");//右电流对比值(0.5-1.2)10

                        fileWriter.Write(CH2TestResult.PressRatio + ",");//右压力对比值(0-1.35)11

                        fileWriter.Write(CH2TestResult.FWD_FlowSum + ",");//右流量总FWD新增
                        fileWriter.Write(CH2TestResult.FWD_PreSum + ",");//右压差总FWD新增


                        fileWriter.Write(CH2TestResult.FWD_ADCMAX + ",");//右max电流(同充)(0.3-1.2)A12
                        fileWriter.Write(CH2TestResult.FWD_VDCMAX + ",");//右max电压(同充)(14.4-14.6)A13
                        fileWriter.Write(CH2TestResult.FWD_Pre1 + ",");//右输出压力（同充上）14
                        fileWriter.Write(CH2TestResult.FWD_Pre2 + ",");//右输出压力(同充下)15
                        fileWriter.Write(CH2TestResult.FWD_Flow1 + ",");//右max流量(同充上)(plm)16

                        fileWriter.Write(CH2TestResult.FWD_Flow2 + ",");//右max流量(同充下)(plm)17
                        fileWriter.Write(CH2TestResult.RWD_ADCMAX + ",");//右max电流(泄气)(0.2-0.6)18
                        fileWriter.Write(CH2TestResult.RWD_VDCMAX + ",");//右max电压(泄气)(14.4-14.6)19

                        fileWriter.Write(CH2TestResult.RWD_Pre1 + ",");//右输出压力(泄气上)(0-5KPa)20
                        fileWriter.Write(CH2TestResult.RWD_Pre2 + ",");// 右输出压力(泄气下)(0 - 5KPa)21



                        fileWriter.Write(",,,,,,,,");


                        fileWriter.Write(CH2TestResult.FullPre1 + ",");//右1压力（充气）


                        //    fileWriter.Write(CH2TestResult.BalanPre1 + ",");//右1压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak1 + ",");//右1泄漏量
                        fileWriter.Write(CH2TestResult.FullPre2 + ",");//右2压力（充气）
                                                                       //      fileWriter.Write(CH2TestResult.BalanPre2 + ",");//右2压力（平衡）
                        fileWriter.Write(CH2TestResult.Leak2 + ",");//右2泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre1 + ",");//右1同充压力（充气）
                                                                           //          fileWriter.Write(CH2TestResult.FWD_BalanPre1 + ",");//右1同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak1 + ",");//右1同充泄漏量
                        fileWriter.Write(CH2TestResult.FWD_FullPre2 + ","); //右2同充压力（充气）
                                                                            //  fileWriter.Write(CH2TestResult.FWD_BalanPre2 + ",");// 右2同充压力（平衡）
                        fileWriter.Write(CH2TestResult.FWD_Leak2 + ",\n");// 右2同充泄漏量


                        CH2csvworknum += 1;
                        break;
                }
                fileWriter.Flush();
                fileWriter.Close();
                switch (CH)
                {
                    case 1:

                        if (plc.CH1CodeCount <= 0)
                        {
                            left_CH1Code.ResetText();
                            left_CH1Code.ScrollToCaret();
                            left_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH1条码启动");
                            winforclose.Interval = 300;
                            flagtime = ptr;
                            winforclose.Start();
                            if (ptr == IntPtr.Zero)
                            {
                                //   MessageBox.Show("左工位需要扫码启动!", "CH1条码启动", MessageBoxButtons.OK);
                                // if(Reset == DialogResult.OK)
                                Logger.Log(I18N.GetLangText(dicLang, "左工位需要扫码启动，CH1条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;

                    case 2:
                        if (plc.CH2CodeCount <= 0)
                        {
                            right_CH1Code.ResetText();
                            right_CH1Code.ScrollToCaret();
                            right_CH1Code.Focus();
                            IntPtr ptr = FindWindow(null, "CH2条码启动");
                            if (ptr == IntPtr.Zero)
                            {
                                //      MessageBox.Show("右工位需要扫码启动!", "CH2条码启动", MessageBoxButtons.OK);
                                //     if (Reset == DialogResult.OK)
                                {
                                }
                                Logger.Log(I18N.GetLangText(dicLang, "右工位需要扫码启动，CH2条码启动"));
                            }
                            else
                            {
                                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CSV:" + ex.Message);
                // MessageBox.Show("CSV:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CSV:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        //将数据写入CSV文件中,作为mes文件
        private void AddMES(int CH)
        {
            try
            {
                string fileName;
                string file = DateTime.Now.ToString("yyyyMMdd");
                string productname = machine.Replace(".ini", "");
                //if (String.IsNullOrEmpty(save.Path))
                //{
                //    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                //}
                //else
                //{
                //    fileName = save.Path + "\\" + productname + "\\" + WorkOrder.Text + "\\";
                //}
                if (String.IsNullOrEmpty(mesfolderpath))
                {
                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MES\\" + productname + "\\" + WorkOrder.Text + "\\";
                }
                else
                {
                    fileName = mesfolderpath + "\\MES\\" + productname + "\\" + WorkOrder.Text + "\\";
                }
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                string name = file + ".csv";
                fileName += name;
                if (File.Exists(fileName) == false)
                {
                    StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
                    fileWriter1.Write(I18N.GetLangText(dicLang, "气袋产品测试记录报表") + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "工单单号") + "：," + WorkOrder.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产料号") + "：," + ProductionItem.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品名称") + "：," + ProductName.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试类型") + "：," + TestType.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "产品型号") + "：," + ProductModel.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "生产数量") + "：," + ProductNum.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "操作人员") + "：," + Admin.Text + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "测试程序文件") + "：," + machinepath + "\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左1,充气时间") + "：," + ch1_1leakparams.FullTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_1leakparams.BalanTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_1leakparams.TestTime1 + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_1leakparams.ExhaustTime + "s\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左2,充气时间") + "：," + ch1_2leakparams.FullTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch1_2leakparams.BalanTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch1_2leakparams.TestTime1 + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch1_2leakparams.ExhaustTime + "s\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右1,充气时间") + "：," + ch2_1leakparams.FullTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_1leakparams.BalanTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_1leakparams.TestTime1 + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_1leakparams.ExhaustTime + "s\n");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右2,充气时间") + "：," + ch2_2leakparams.FullTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "平衡时间") + "：," + ch2_2leakparams.BalanTime + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "检测时间") + "：," + ch2_2leakparams.TestTime1 + "s,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "排气时间") + "：," + ch2_2leakparams.ExhaustTime + "s\n");
                    //fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "作业序号"), I18N.GetLangText(dicLang, "测试时间"), I18N.GetLangText(dicLang, "条形码"), I18N.GetLangText(dicLang, "判定结果") + "OK/NG,");

                    fileWriter1.Write(I18N.GetLangText(dicLang, "左avg静态电流") + "(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "uA),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(上充)") + "(" + elec.CH1UPADCMin + "-" + elec.CH1UPADCMax + "A),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(上充)") + "(" + elec.CH1UPVDCMin + "-" + elec.CH1UPVDCMax + "V),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(上充)") + "(" + Flow.CH1_1PreMin + "-" + Flow.CH1_1PreMax + PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(上充)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(下充)") + "(" + elec.CH1DOWNADCMin + "-" + elec.CH1DOWNADCMax + "A),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(下充)") + "(" + elec.CH1DOWNVDCMin + "-" + elec.CH1DOWNVDCMax + "V),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(下充)") + "(" + Flow.CH1_2PreMin + "-" + Flow.CH1_2PreMax + CH2PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(下充)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左电流对比值") + "(" + Flow.CH1Cont_ElecMin + "-" + Flow.CH1Cont_ElecMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左压力对比值") + "(" + Flow.CH1Cont_PressMin + "-" + Flow.CH1Cont_PressMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(同充)") + elec.CH1FWDADCMin + "-" + elec.CH1FWDADCMax + ")A,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(同充)") + "(" + elec.CH1FWDVDCMin + "-" + elec.CH1FWDVDCMax + ")A,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充上)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(同充下)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充上)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max流量(同充下)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电流(泄气)") + "(" + elec.CH1RWDADCMin + "-" + elec.CH1RWDADCMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左max电压(泄气)") + "(" + elec.CH1RWDVDCMin + "-" + elec.CH1RWDVDCMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气上)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左输出压力(泄气下)") + "(" + Flow.CH1RWDPressMin + "-" + Flow.CH1RWDPressMax + CH2PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右avg静态电流") + "(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "uA),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(上充)") + "(" + elec.CH2UPADCMin + "-" + elec.CH2UPADCMax + "A),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(上充)") + "(" + elec.CH2UPVDCMin + "-" + elec.CH2UPVDCMax + "V),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(上充)") + "(" + Flow.CH2_1PreMin + "-" + Flow.CH2_1PreMax + CH3PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(上充)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(下充)") + "(" + elec.CH2DOWNADCMin + "-" + elec.CH2DOWNADCMax + "A),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(下充)") + "(" + elec.CH2DOWNVDCMin + "-" + elec.CH2DOWNVDCMax + "V),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(下充)") + "(" + Flow.CH2_2PreMin + "-" + Flow.CH2_2PreMax + CH4PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(下充)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右电流对比值") + "(" + Flow.CH2Cont_ElecMin + "-" + Flow.CH2Cont_ElecMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右压力对比值") + "(" + Flow.CH2Cont_PressMin + "-" + Flow.CH2Cont_PressMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(同充)") + "(" + elec.CH2FWDADCMin + "-" + elec.CH2FWDADCMax + ")A,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(同充)") + "(" + elec.CH2FWDVDCMin + "-" + elec.CH2FWDVDCMax + ")A,");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充上)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(同充下)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充上)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max流量(同充下)(lpm)") + ",");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电流(泄气)") + "(" + elec.CH2RWDADCMin + "-" + elec.CH2RWDADCMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右max电压(泄气)") + "(" + elec.CH2RWDVDCMin + "-" + elec.CH2RWDVDCMax + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气上)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH3PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右输出压力(泄气下)") + "(" + Flow.CH2RWDPressMin + "-" + Flow.CH2RWDPressMax + CH4PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
                //    fileWriter1.Write(I18N.GetLangText(dicLang, "左1压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左1泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
              //      fileWriter1.Write(I18N.GetLangText(dicLang, "左2压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左2泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(充气)") + "(" + ch1_1leakparams.FPlowlimit + "-" + ch1_1leakparams.FPtoplimit + PressureUnit.Text + "),");
             //       fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充压力(平衡)") + "(" + ch1_1leakparams.BalanPreMin + "-" + ch1_1leakparams.BalanPreMax + PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左1同充泄漏量") + "(" + ch1_1leakparams.Leaklowlimit + "-" + ch1_1leakparams.Leaktoplimit + LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(充气)") + "(" + ch1_2leakparams.FPlowlimit + "-" + ch1_2leakparams.FPtoplimit + CH2PressureUnit.Text + "),");
                //    fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充压力(平衡)") + "(" + ch1_2leakparams.BalanPreMin + "-" + ch1_2leakparams.BalanPreMax + CH2PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "左2同充泄漏量") + "(" + ch1_2leakparams.Leaklowlimit + "-" + ch1_2leakparams.Leaktoplimit + CH2LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
               //     fileWriter1.Write(I18N.GetLangText(dicLang, "右1压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右1泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
               //     fileWriter1.Write(I18N.GetLangText(dicLang, "右2压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右2泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(充气)") + "(" + ch2_1leakparams.FPlowlimit + "-" + ch2_1leakparams.FPtoplimit + CH3PressureUnit.Text + "),");
                  //  fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充压力(平衡)") + "(" + ch2_1leakparams.BalanPreMin + "-" + ch2_1leakparams.BalanPreMax + CH3PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右1同充泄漏量") + "(" + ch2_1leakparams.Leaklowlimit + "-" + ch2_1leakparams.Leaktoplimit + CH3LeakUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(充气)") + "(" + ch2_2leakparams.FPlowlimit + "-" + ch2_2leakparams.FPtoplimit + CH4PressureUnit.Text + "),");
                 //   fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充压力(平衡)") + "(" + ch2_2leakparams.BalanPreMin + "-" + ch2_2leakparams.BalanPreMax + CH4PressureUnit.Text + "),");
                    fileWriter1.Write(I18N.GetLangText(dicLang, "右2同充泄漏量") + "(" + ch2_2leakparams.Leaklowlimit + "-" + ch2_2leakparams.Leaktoplimit + CH4LeakUnit.Text + "),\n");

                    fileWriter1.Flush();
                    fileWriter1.Close();
                }
                StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);



                switch (CH)
                {
                    case 1:

                        fileWriter.Write(CH1mesworknum + I18N.GetLangText(dicLang, "左") + ",#" + CH1timestamp + "#,'" + left_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH1Tlight.Text + ",");
                        fileWriter.Write(CH1RTElec.Text + "," + CH1TestResult.UP_ADCMAX + "," + CH1TestResult.UP_VDCMAX + "," + CH1TestResult.UP_Pre + ",");
                        fileWriter.Write(CH1TestResult.UP_Flow + "," + CH1TestResult.DOWN_ADCMAX + "," + CH1TestResult.DOWN_VDCMAX + ",");
                        fileWriter.Write(CH1TestResult.DOWN_Pre + "," + CH1TestResult.DOWN_Flow + "," + CH1TestResult.ElecRatio + ",");
                        fileWriter.Write(CH1TestResult.PressRatio + "," + CH1TestResult.FWD_ADCMAX + "," + CH1TestResult.FWD_VDCMAX + ",");
                        fileWriter.Write(CH1TestResult.FWD_Pre1 + "," + CH1TestResult.FWD_Pre2 + "," + CH1TestResult.FWD_Flow1 + "," + CH1TestResult.FWD_Flow2 + ",");
                        //fileWriter.Write(CH1TestResult.RWD_ADCMAX + "," + CH1TestResult.RWD_VDCMAX + "," + CH1TestResult.RWD_Pre1 + "," + CH1TestResult.RWD_Pre2 + ",,,,,,,,,,,,,,,,,,,,,,");
                        fileWriter.Write(CH1TestResult.RWD_ADCMAX + "," + CH1TestResult.RWD_VDCMAX + "," + CH1TestResult.RWD_Pre1 + "," + CH1TestResult.RWD_Pre2 + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0"
                            + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + ",");
                        fileWriter.Write(CH1TestResult.FullPre1 + "," + CH1TestResult.BalanPre1 + "," + CH1TestResult.Leak1 + ",");
                        fileWriter.Write(CH1TestResult.FullPre2 + "," + CH1TestResult.BalanPre2 + "," + CH1TestResult.Leak2 + ",");
                        fileWriter.Write(CH1TestResult.FWD_FullPre1 + "," + CH1TestResult.FWD_BalanPre1 + "," + CH1TestResult.FWD_Leak1 + ",");
                        fileWriter.Write(CH1TestResult.FWD_FullPre2 + "," + CH1TestResult.FWD_BalanPre2 + "," + CH1TestResult.FWD_Leak2 + ",\n");
                        CH1mesworknum += 1;
                        break;

                    case 2:
                        //fileWriter.Write(CH2mesworknum + I18N.GetLangText(dicLang, "右") + ",#" + CH2timestamp + "#,'" + right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH2Tlight.Text + ",,,,,,,,,,,,,,,,,,,,,,");
                        fileWriter.Write(CH2mesworknum + I18N.GetLangText(dicLang, "右") + ",#" + CH2timestamp + "#,'" + right_CH1Code.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "," + CH2Tlight.Text + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0"
                            + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + ",");
                        fileWriter.Write(CH2RTElec.Text + "," + CH2TestResult.UP_ADCMAX + "," + CH2TestResult.UP_VDCMAX + "," + CH2TestResult.UP_Pre + ",");
                        fileWriter.Write(CH2TestResult.UP_Flow + "," + CH2TestResult.DOWN_ADCMAX + "," + CH2TestResult.DOWN_VDCMAX + ",");
                        fileWriter.Write(CH2TestResult.DOWN_Pre + "," + CH2TestResult.DOWN_Flow + "," + CH2TestResult.ElecRatio + ",");
                        fileWriter.Write(CH2TestResult.PressRatio + "," + CH2TestResult.FWD_ADCMAX + "," + CH2TestResult.FWD_VDCMAX + ",");
                        fileWriter.Write(CH2TestResult.FWD_Pre1 + "," + CH2TestResult.FWD_Pre2 + "," + CH2TestResult.FWD_Flow1 + "," + CH2TestResult.FWD_Flow2 + ",");
                        fileWriter.Write(CH2TestResult.RWD_ADCMAX + "," + CH2TestResult.RWD_VDCMAX + "," + CH2TestResult.RWD_Pre1 + "," + CH2TestResult.RWD_Pre2 + ",");
                        //fileWriter.Write(",,,,,,,,,,,," + CH2TestResult.FullPre1 + "," + CH2TestResult.BalanPre1 + "," + CH2TestResult.Leak1 + ",");
                        fileWriter.Write("0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + "0" + "," + CH2TestResult.FullPre1 + "," + CH2TestResult.BalanPre1 + "," + CH2TestResult.Leak1 + ",");
                        fileWriter.Write(CH2TestResult.FullPre2 + "," + CH2TestResult.BalanPre2 + "," + CH2TestResult.Leak2 + ",");
                        fileWriter.Write(CH2TestResult.FWD_FullPre1 + "," + CH2TestResult.FWD_BalanPre1 + "," + CH2TestResult.FWD_Leak1 + ",");
                        fileWriter.Write(CH2TestResult.FWD_FullPre2 + "," + CH2TestResult.FWD_BalanPre2 + "," + CH2TestResult.FWD_Leak2 + ",\n");
                        CH2mesworknum += 1;
                        break;
                }
                fileWriter.Flush();
                fileWriter.Close();
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "MES:" + ex.Message);
                //MessageBox.Show("MES:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "MES:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
            //try
            //{
            //    string fileName;
            //    string file;
            //    if (String.IsNullOrEmpty(mesfolderpath))
            //    {
            //        fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            //    }
            //    else
            //    {
            //        fileName = mesfolderpath + "\\";
            //    }
            //    if (!Directory.Exists(fileName))
            //    {
            //        Directory.CreateDirectory(fileName);
            //    }
            //    if (String.IsNullOrEmpty(mesfilename))
            //    {
            //        file = DateTime.Now.ToString("yyyyMMdd");
            //    }
            //    else
            //    {
            //        file = mesfilename;
            //    }
            //    string name = file + ".csv";
            //    fileName += name;
            //    if (File.Exists(fileName) == false)
            //    {
            //        StreamWriter fileWriter1 = new StreamWriter(fileName, true, Encoding.UTF8);
            //        fileWriter1.Write("阀泵产品测试记录报表\n");
            //        fileWriter1.Write("工单单号：," + WorkOrder.Text + "\n");
            //        fileWriter1.Write("生产料号：," + ProductionItem.Text + "\n");
            //        fileWriter1.Write("产品名称：," + ProductName.Text + "\n");
            //        fileWriter1.Write("测试类型：," + TestType.Text + "\n");
            //        fileWriter1.Write("产品型号：," + ProductModel.Text + "\n");
            //        fileWriter1.Write("生产数量：," + ProductNum.Text + "\n");
            //        fileWriter1.Write("操作人员：," + Admin.Text + "\n");
            //        fileWriter1.Write("测试程序文件：," + fileName + "\n");
            //        fileWriter1.Write("作业序号,测试时间,条形码,判定结果 OK/NG,");
            //        if (CH == 1)
            //        {
            //            int i;
            //            for (i = 0; i < CH1Order.Count; i++)
            //            {
            //                fileWriter1.Write("MAX工作电流（" + CH1Order[i] + ")(" + elec.CH1ADCMin + "-" + elec.CH1ADCMax + "A),");
            //                fileWriter1.Write("MAX工作电压（" + CH1Order[i] + ")(" + elec.CH1VDCMin + "-" + elec.CH1VDCMax + "V),");
            //                fileWriter1.Write("AVG静态电流（" + CH1Order[i] + ")(" + elec.CH1ElecMin + "-" + elec.CH1ElecMax + "V),");
            //                //fileWriter1.Write("MAX流量（" + CH1Order[i] + ")(plm),");
            //                if (CH1Order[i] == "FWD" || CH1Order[i] == "RWD" || CH1Order[i] == "UP")
            //                {
            //                    fileWriter1.Write("MAX流量(UP)(plm),");
            //                    fileWriter1.Write("MAX输出压力(UP)(" + PressureUnit.Text + "),");
            //                }
            //                if (CH1Order[i] == "FWD" || CH1Order[i] == "RWD" || CH1Order[i] == "DOWN")
            //                {
            //                    fileWriter1.Write("MAX流量(DOWN)(plm),");
            //                    fileWriter1.Write("MAX输出压力(DOWN)(" + CH2PressureUnit.Text + "),");
            //                }
            //            }
            //            fileWriter1.Write("比值（电流)(" + Contrast.CH1cont_elecmin + "-" + Contrast.CH1cont_elecmax + "),");
            //            fileWriter1.Write("比值（压力)(" + Contrast.CH1cont_pressmin + "-" + Contrast.CH1cont_pressmax + "),");
            //            if (CH1Leak)
            //            {
            //                fileWriter1.Write("泄漏量（气密)(" + left_ch1params.Leaklowlimit + "-" + left_ch1params.Leaktoplimit + left_ch1params.LUnit + "),");
            //                fileWriter1.Write("泄漏量（气密)(" + left_ch2params.Leaklowlimit + "-" + left_ch2params.Leaktoplimit + left_ch2params.LUnit + "),");
            //            }
            //            fileWriter1.Write("\n");
            //        }
            //        else
            //        {
            //            int i;
            //            for (i = 0; i < CH2Order.Count; i++)
            //            {
            //                fileWriter1.Write("MAX工作电流（" + CH2Order[i] + ")(" + elec.CH2ADCMin + "-" + elec.CH2ADCMax + "A),");
            //                fileWriter1.Write("MAX工作电压（" + CH2Order[i] + ")(" + elec.CH2VDCMin + "-" + elec.CH2VDCMax + "V),");
            //                fileWriter1.Write("MAX静态电流（" + CH2Order[i] + ")(" + elec.CH2ElecMin + "-" + elec.CH2ElecMax + "V),");
            //                if (CH2Order[i] == "FWD" || CH2Order[i] == "RWD" || CH2Order[i] == "UP")
            //                {
            //                    fileWriter1.Write("MAX流量(UP)(plm),");
            //                    fileWriter1.Write("MAX输出压力(UP)(" + CH3PressureUnit.Text + "),");
            //                }
            //                if (CH2Order[i] == "FWD" || CH2Order[i] == "RWD" || CH2Order[i] == "DOWN")
            //                {
            //                    fileWriter1.Write("MAX流量(DOWN)(plm),");
            //                    fileWriter1.Write("MAX输出压力(DOWN)(" + CH4PressureUnit.Text + "),");
            //                }
            //            }
            //            fileWriter1.Write("比值（电流)(" + Contrast.CH2cont_elecmin + "-" + Contrast.CH2cont_elecmax + "),");
            //            fileWriter1.Write("比值（压力)(" + Contrast.CH2cont_pressmin + "-" + Contrast.CH2cont_pressmax + "),");
            //            if (CH2Leak)
            //            {
            //                fileWriter1.Write("泄漏量（气密)(" + right_ch1params.Leaklowlimit + "-" + right_ch1params.Leaktoplimit + right_ch1params.LUnit + "),");
            //                fileWriter1.Write("泄漏量（气密)(" + right_ch1params.Leaklowlimit + "-" + right_ch1params.Leaktoplimit + right_ch1params.LUnit + "),");
            //            }
            //            fileWriter1.Write("\n");
            //        }
            //        fileWriter1.Flush();
            //        fileWriter1.Close();
            //    }
            //    StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.UTF8);

            //    string nowdate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            //    if (CH == 1)
            //    {
            //        fileWriter.Write(CH1MESJobOrder + "左,");
            //        fileWriter.Write("#" + CH1testbegin + "#,");
            //        fileWriter.Write("'" + left_CH1Code.Text + ",");
            //        string result;
            //        if (CH1Tlight.Text == "OK")
            //        {
            //            result = "PASS";
            //        }
            //        else
            //        {
            //            result = "FAIL";
            //        }
            //        fileWriter.Write(result + ",");
            //        fileWriter.Write(CH1TestResult.Elec + ",");
            //        int i;
            //        for (i = 0; i < CH1Order.Count; i++)
            //        {
            //            if (CH1Order[i] == "UP")
            //            {
            //                fileWriter.Write(CH1TestResult.UP_ADCMAX + ",");
            //                fileWriter.Write(CH1TestResult.UP_VDCMAX + ",");
            //                //fileWriter.Write(CH1TestResult.UP_Elec + ",");
            //                fileWriter.Write(CH1TestResult.UP_Flow + ",");
            //                fileWriter.Write(CH1TestResult.UP_Pre + ",");
            //            }
            //            if (CH1Order[i] == "DOWN")
            //            {
            //                fileWriter.Write(CH1TestResult.DOWN_ADCMAX + ",");
            //                fileWriter.Write(CH1TestResult.DOWN_VDCMAX + ",");
            //                //fileWriter.Write(CH1TestResult.DOWN_Elec + ",");
            //                fileWriter.Write(CH1TestResult.DOWN_Flow + ",");
            //                fileWriter.Write(CH1TestResult.DOWN_Pre + ",");
            //            }
            //            if (CH1Order[i] == "FWD")
            //            {
            //                fileWriter.Write(CH1TestResult.FWD_ADCMAX + ",");
            //                fileWriter.Write(CH1TestResult.FWD_VDCMAX + ",");
            //                //fileWriter.Write(CH1TestResult.FWD_Elec + ",");
            //                fileWriter.Write(CH1TestResult.FWD_Flow1 + ",");
            //                fileWriter.Write(CH1TestResult.FWD_Flow2 + ",");
            //                fileWriter.Write(CH1TestResult.FWD_Pre1 + ",");
            //                fileWriter.Write(CH1TestResult.FWD_Pre2 + ",");
            //            }
            //            if (CH1Order[i] == "RWD")
            //            {
            //                fileWriter.Write(CH1TestResult.RWD_ADCMAX + ",");
            //                fileWriter.Write(CH1TestResult.RWD_VDCMAX + ",");
            //                //fileWriter.Write(CH1TestResult.RWD_Elec + ",");
            //                fileWriter.Write(CH1TestResult.RWD_Flow1 + ",");
            //                fileWriter.Write(CH1TestResult.RWD_Flow2 + ",");
            //                fileWriter.Write(CH1TestResult.RWD_Pre1 + ",");
            //                fileWriter.Write(CH1TestResult.RWD_Pre2 + ",");
            //            }
            //        }
            //        if (CH1Leak)
            //        {
            //            fileWriter.Write(LeftCH1SmallLeak + ",");
            //            fileWriter.Write(LeftCH2SmallLeak + ",");
            //        }
            //        fileWriter.Write("\n");
            //        CH1MESJobOrder += 1;
            //    }
            //    if (CH == 2)
            //    {
            //        fileWriter.Write(CH2MESJobOrder + "右,");
            //        fileWriter.Write("#" + CH2testbegin + "#,");
            //        fileWriter.Write("'" + right_CH1Code + ",");
            //        string result;
            //        if (CH2Tlight.Text == "OK")
            //        {
            //            result = "PASS";
            //        }
            //        else
            //        {
            //            result = "FAIL";
            //        }
            //        fileWriter.Write(result + ",");
            //        fileWriter.Write(CH2TestResult.Elec + ",");
            //        int i;
            //        for (i = 0; i < CH2Order.Count; i++)
            //        {
            //            if (CH2Order[i] == "UP")
            //            {
            //                fileWriter.Write(CH2TestResult.UP_ADCMAX + ",");
            //                fileWriter.Write(CH2TestResult.UP_VDCMAX + ",");
            //                //fileWriter.Write(CH2TestResult.UP_Elec + ",");
            //                fileWriter.Write(CH2TestResult.UP_Flow + ",");
            //                fileWriter.Write(CH2TestResult.UP_Pre + ",");
            //            }
            //            if (CH2Order[i] == "DOWN")
            //            {
            //                fileWriter.Write(CH2TestResult.DOWN_ADCMAX + ",");
            //                fileWriter.Write(CH2TestResult.DOWN_VDCMAX + ",");
            //                //fileWriter.Write(CH2TestResult.DOWN_Elec + ",");
            //                fileWriter.Write(CH2TestResult.DOWN_Flow + ",");
            //                fileWriter.Write(CH2TestResult.DOWN_Pre + ",");
            //            }
            //            if (CH2Order[i] == "FWD")
            //            {
            //                fileWriter.Write(CH2TestResult.FWD_ADCMAX + ",");
            //                fileWriter.Write(CH2TestResult.FWD_VDCMAX + ",");
            //                //fileWriter.Write(CH2TestResult.FWD_Elec + ",");
            //                fileWriter.Write(CH2TestResult.FWD_Flow1 + ",");
            //                fileWriter.Write(CH2TestResult.FWD_Flow2 + ",");
            //                fileWriter.Write(CH2TestResult.FWD_Pre1 + ",");
            //                fileWriter.Write(CH2TestResult.FWD_Pre2 + ",");
            //            }
            //            if (CH2Order[i] == "RWD")
            //            {
            //                fileWriter.Write(CH2TestResult.RWD_ADCMAX + ",");
            //                fileWriter.Write(CH2TestResult.RWD_VDCMAX + ",");
            //                //fileWriter.Write(CH2TestResult.RWD_Elec + ",");
            //                fileWriter.Write(CH2TestResult.RWD_Flow1 + ",");
            //                fileWriter.Write(CH2TestResult.RWD_Flow2 + ",");
            //                fileWriter.Write(CH2TestResult.RWD_Pre1 + ",");
            //                fileWriter.Write(CH2TestResult.RWD_Pre2 + ",");
            //            }
            //        }
            //        if (CH2Leak)
            //        {
            //            fileWriter.Write(RightCH1SmallLeak + ",");
            //            fileWriter.Write(RightCH2SmallLeak + ",");
            //        }
            //        fileWriter.Write("\n");
            //        CH2MESJobOrder += 1;
            //    }
            //    CH1CSVJobOrder += 1;
            //    fileWriter.Flush();
            //    fileWriter.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("MES:" + ex.Message);
            //    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "MES:" + ex.Message);
            //}
        }

        //写入数据
        private void CreateFile(int CH)
        {
            if (save.ChkExcel)
            {
                AddExcel(CH);
            }
            if (save.ChkCSV)
            {
                AddCSV4(CH);

            }
        }

        //在界面显示数据
        private void CH1Display(string item, string data, string unit, string max, string min, string result)
        {
            Invoke(new System.Action(() =>
            {
                //if (!Electricity.ord.CH1UpDownChange)
                {
                    string nowdate = DateTime.Now.ToString("HH:mm:ss");
                    string[] dataArr = { nowdate, item, data, unit, max, min, result };
                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                    {
                        if (this.DataGridView1.Rows[i].Cells["Column2"].Value.ToString() == item)
                        {
                            this.DataGridView1.Rows.RemoveAt(i);
                        }
                    }
                    DataGridView1.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
                }
                //else
                //{
                //    string nowdate = DateTime.Now.ToString("HH:mm:ss");
                //    string replaceItem = item;
                //    if (item.EndsWith(I18N.GetLangText(dicLang, "上充"))) replaceItem = replaceItem.Replace(I18N.GetLangText(dicLang, "上充"), I18N.GetLangText(dicLang, "下充"));
                //    if (item.EndsWith(I18N.GetLangText(dicLang, "下充"))) replaceItem = replaceItem.Replace(I18N.GetLangText(dicLang, "下充"), I18N.GetLangText(dicLang, "上充"));

                //    string[] dataArr = { nowdate, replaceItem, data, unit, max, min, result };
                //    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                //    {
                //        if (this.DataGridView1.Rows[i].Cells["Column2"].Value.ToString() == replaceItem)
                //        {
                //            this.DataGridView1.Rows.RemoveAt(i);
                //        }
                //    }
                //    DataGridView1.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
                //}
            }));
        }

        ////在界面显示数据
        //private void CH2Display(string item, string data, string unit, string max, string min, string result)
        //{
        //    string nowdate = DateTime.Now.ToString("HH:mm:ss");
        //    string[] dataArr = { nowdate, item, data, unit, max, min, result };

        //    DataGridView1.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
        //}
        //在界面显示数据
        private void CH3Display(string item, string data, string unit, string max, string min, string result)
        {
            //  if (!Electricity.ord.CH2UpDownChange)
            {
                string nowdate = DateTime.Now.ToString("HH:mm:ss");
                string[] dataArr = { nowdate, item, data, unit, max, min, result };
                for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                {
                    if (this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value.ToString() == item)
                    {
                        this.DataGridView2.Rows.RemoveAt(i);
                    }
                }
                DataGridView2.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
            }
            //else
            //{
            //    string nowdate = DateTime.Now.ToString("HH:mm:ss");
            //    string replaceItem = item;
            //    if (item.EndsWith(I18N.GetLangText(dicLang, "上充"))) replaceItem = replaceItem.Replace(I18N.GetLangText(dicLang, "上充"), I18N.GetLangText(dicLang, "下充"));
            //    if (item.EndsWith(I18N.GetLangText(dicLang, "下充"))) replaceItem = replaceItem.Replace(I18N.GetLangText(dicLang, "下充"), I18N.GetLangText(dicLang, "上充"));

            //    string[] dataArr = { nowdate, replaceItem, data, unit, max, min, result };
            //    for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
            //    {
            //        if (this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value.ToString() == replaceItem)
            //        {
            //            this.DataGridView2.Rows.RemoveAt(i);
            //        }
            //    }
            //    DataGridView2.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
            //}
        }

        ////在界面显示数据
        //private void CH4Display(string item, string data, string unit, string max, string min, string result)
        //{
        //    string nowdate = DateTime.Now.ToString("HH:mm:ss");
        //    string[] dataArr = { nowdate, item, data, unit, max, min, result };

        //    DataGridView2.Rows.Insert(0, dataArr[0], dataArr[1], dataArr[2], dataArr[3], dataArr[4], dataArr[5], dataArr[6]);
        //}

        //写入条码
        private void SetLeftCode()
        {
            string dialog = machine;

            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Code", "CH1Code", left_CH1Code.Text);
        }

        //写入条码
        private void SetRightCode()
        {
            string dialog = machine;

            ConfigINI mesconfig = new ConfigINI("Model", dialog);
            mesconfig.IniWriteValue("Code", "CH2Code", right_CH1Code.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult DR = MessageBox.Show(I18N.GetLangText(dicLang, "确认退出系统吗"), I18N.GetLangText(dicLang, "提示"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DR == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
            else e.Cancel = true;
        }

 

        /// <summary>
        /// 条码输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodePort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                int len = CodePort1.BytesToRead;//获取可以读取的字节数
                //if (plc.CH1ARun == true && plc.CH2ARun == true)
                if ((plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun) && (plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun))
                {
                    if (CodePort1.IsOpen) CodePort1.DiscardInBuffer();
                }
                else
                if (len > 1)
                {
                    byte[] buff = new byte[len];//创建缓存数据数组
                    CodePort1.Read(buff, 0, len);//把数据读取到buff数组
                    Invoke((new System.Action(() => //接收计数
                    {
                        string code = Encoding.Default.GetString(buff);
                        CodeJudge(code, 1);
                        if (CodePort1.IsOpen)
                        {
                            CodePort1.DiscardInBuffer();
                            CodePort1.DiscardOutBuffer();
                        }
                    })));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Code:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "Code:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private void CodePort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                int len = CodePort2.BytesToRead;//获取可以读取的字节数
                                                //if (plc.CH1ARun == true && plc.CH2ARun == true)
                if ((plc.CH1Run | plc.CH1ARun | plc.CH1BRun | plc.CH1CRun) && (plc.CH2Run | plc.CH2DRun | plc.CH2ERun | plc.CH2FRun))
                {
                    if (CodePort2.IsOpen) CodePort2.DiscardInBuffer();
                }
                else
            if (len > 1)
                {
                    byte[] buff = new byte[len];//创建缓存数据数组
                    CodePort2.Read(buff, 0, len);//把数据读取到buff数组
                    Invoke((new System.Action(() => //接收计数
                    {
                        string code = Encoding.Default.GetString(buff);
                        CodeJudge(code, 2);
                        if (CodePort2.IsOpen)
                        {
                            CodePort2.DiscardInBuffer();
                            CodePort2.DiscardOutBuffer();
                        }
                    })));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Code2:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "Code2:") + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private void CodeJudge(string code, int CH)
        {
            if (CH == 1 && String.IsNullOrEmpty(left_CH1Code.Text))
            {
                left_CH1Code.Text = code.Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
                if (left_CH1Code.TextLength > 0)
                {
                    if (left_CH1Code.Text == right_CH1Code.Text)
                    {
                        //MessageBox.Show("条形码重复!");
                        Logger.Log(I18N.GetLangText(dicLang, "条形码重复"));
                        left_CH1Code.ResetText();
                        left_CH1Code.Focus();
                    }
                    else
                    {
                        plc.CH1Code();
                        SetLeftCode();
                    }
                }
            }
            else if (CH == 2 && String.IsNullOrEmpty(right_CH1Code.Text))
            {
                right_CH1Code.Text = code.Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
                if (right_CH1Code.TextLength > 0)
                {
                    if (right_CH1Code.Text == left_CH1Code.Text)
                    {
                        //MessageBox.Show("条形码重复!");
                        Logger.Log(I18N.GetLangText(dicLang, "条形码重复"));
                        right_CH1Code.ResetText();
                        right_CH1Code.Focus();
                    }
                    else
                    {
                        plc.CH2Code();
                        SetRightCode();
                    }
                }
            }
            //if (codesetting.CHKCH1 && String.IsNullOrEmpty(left_CH1Code.Text))
            //{
            //    left_CH1Code.Text = code;
            //    if (left_CH1Code.TextLength > 0)
            //    {
            //        if (left_CH1Code.Text == right_CH1Code.Text)
            //        {
            //            MessageBox.Show("条形码重复!");
            //            left_CH1Code.ResetText();
            //            left_CH1Code.Focus();
            //        }
            //        else
            //        {
            //            plc.CH1Code();
            //            SetLeftCode();
            //        }
            //    }
            //}
            //else if (codesetting.CHKCH1 && !codesetting.CHKCH2)
            //{
            //    left_CH1Code.Text = code;
            //    if (left_CH1Code.TextLength > 0)
            //    {
            //        if (left_CH1Code.Text == right_CH1Code.Text)
            //        {
            //            MessageBox.Show("条形码重复!");
            //            left_CH1Code.ResetText();
            //            left_CH1Code.Focus();
            //        }
            //        else
            //        {
            //            plc.CH1Code();
            //            SetLeftCode();
            //        }
            //    }
            //}
            //else
            //{
            //    right_CH1Code.Text = code;
            //    if (right_CH1Code.TextLength > 0)
            //    {
            //        if (left_CH1Code.Text == right_CH1Code.Text)
            //        {
            //            MessageBox.Show("条形码重复!");
            //            right_CH1Code.ResetText();
            //            right_CH1Code.Focus();
            //        }
            //        else
            //        {
            //            plc.CH2Code();
            //            SetRightCode();
            //        }
            //    }
            //}
        }

        ///// <summary>
        ///// 左工位保存条码使用次数
        ///// </summary>
        //private void WriteCH1CodeCount()
        //{
        //    string dialog;
        //    dialog = "CodeCount.ini";
        //    ConfigINI mesconfig = new ConfigINI(dialog);
        //    mesconfig.IniWriteValue("CodeCount", "CH1CodeCount", left_codelife.ToString());
        //}
        ///// <summary>
        ///// 右工位保存条码使用次数
        ///// </summary>
        //private void WriteCH2RingCount()
        //{
        //    string dialog;
        //    dialog = "CodeCount.ini";
        //    ConfigINI mesconfig = new ConfigINI(dialog);
        //    mesconfig.IniWriteValue("CodeCount", "CH2CodeCount", right_codelife.ToString());
        //}

        //private void ReadCodeCount()
        //{
        //    //string dialog;
        //    //dialog = "CodeCount.ini";
        //    //ConfigINI mesconfig = new ConfigINI(machine, dialog);

        //    string ch1code_life = mesconfig.IniReadValue("CodeLife", "CH1CodeLife");
        //    if (String.IsNullOrEmpty(ch1code_life))
        //    {
        //        left_codelife = 0;
        //    }
        //    else
        //    {
        //        left_codelife = Convert.ToInt32(ch1code_life);
        //    }
        //    //string ch1code_count = mesconfig.IniReadValue("CodeCount", "CH1CodeCount");
        //    //if (String.IsNullOrEmpty(ch1code_count))
        //    //{
        //    //    left_codecount = 0;
        //    //}
        //    //else
        //    //{
        //    //    left_codecount = Convert.ToInt32(ch1code_count);
        //    //}
        //    string ch2code_life = mesconfig.IniReadValue("CodeLife", "CH2CodeLife");
        //    if (String.IsNullOrEmpty(ch2code_life))
        //    {
        //        right_codelife = 0;
        //    }
        //    else
        //    {
        //        right_codelife = Convert.ToInt32(ch2code_life);
        //    }
        //    //string ch2code_count = mesconfig.IniReadValue("CodeCount", "CH2CodeCount");
        //    //if (String.IsNullOrEmpty(ch2code_count))
        //    //{
        //    //    right_codecount = 0;
        //    //}
        //    //else
        //    //{
        //    //    right_codecount = Convert.ToInt32(ch2code_count);
        //    //}
        //}
        /// <summary>
        /// 读取条码使用次数和规定次数
        /// </summary>
        public void WriteCH1ProductCount()
        {
            //string dialog;
            //dialog = "ProductCount.ini";
            //ConfigINI config = new ConfigINI(machine, dialog);
            string dialog = machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("Count", "CH1Product", CH1Product.ToString());

            config.IniWriteValue("Count", "CH1PassNum", CH1PassNum.ToString());
            config.IniWriteValue("Count", "CH1FailNum", CH1FailNum.ToString());
            if (CH1Product == 0)
                CH1PassRate.Text = (Math.Round((decimal)0, 2) * 100).ToString();
            else
                CH1PassRate.Text = (Math.Round((decimal)CH1PassNum / CH1Product, 2) * 100).ToString();

            //if (CH1Product == 0)
            //    CH1CT.Text = (Math.Round((decimal)0, 2) * 100).ToString();
            //else
            //    CH1CT.Text = (Math.Round((decimal)CH1FailNum / CH1Product, 2) * 100).ToString();
        }

        public void WriteCH2ProductCount()
        {
            //string dialog;
            //dialog = "ProductCount.ini";
            //ConfigINI config = new ConfigINI(machine, dialog);
            string dialog = machine;
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("Count", "CH2Product", CH2Product.ToString());
            config.IniWriteValue("Count", "CH2PassNum", CH2PassNum.ToString());
            config.IniWriteValue("Count", "CH2FailNum", CH2FailNum.ToString());
            if (CH2Product == 0)
            {
                CH2PassRate.Text = (Math.Round((decimal)0, 2) * 100).ToString();
                //CH2CT.Text = (Math.Round((decimal)0, 2) * 100).ToString();
            }
            else
            {
                CH2PassRate.Text = (Math.Round((decimal)CH2PassNum / CH2Product, 2) * 100).ToString();
                //CH2CT.Text = (Math.Round((decimal)CH2FailNum / CH2Product, 2) * 100).ToString();
            }
        }

        /// <summary>
        /// 读取条码设定设置、存储设置、工单信息等各种设置
        /// </summary>
        private void ReadAllConfig()
        {
            ReadConfig con = new ReadConfig();
            save = con.ReadSave();
            //读取条码配置
            codesetting = con.ReadCode();
            left_CH1Code.Text = codesetting.CH1Code;
            right_CH1Code.Text = codesetting.CH2Code;
            Setup.ProductCount set;
            set = con.ReadProduct();
            CH1Product = set.CH1Product;
            CH1PassNum = set.CH1PassNum;
            CH1FailNum = set.CH1FailNum;
            CH2Product = set.CH2Product;
            CH2PassNum = set.CH2PassNum;
            CH2FailNum = set.CH2FailNum;
            CH1ProductNumber.Text = CH1Product.ToString();
            CH1PassNumber.Text = CH1PassNum.ToString();
            CH1FailNumber.Text = CH1FailNum.ToString();
            if (CH1Product != 0)
            {
                CH1PassRate.Text = ((Convert.ToDouble(CH1PassNum) / Convert.ToDouble(CH1Product)) * 100).ToString("f2") + "%";
                //CH1CT.Text = ((Convert.ToDouble(CH1FailNum) / Convert.ToDouble(CH1Product)) * 100).ToString("f2") + "%";
            }
            CH2ProductNumber.Text = CH2Product.ToString();
            CH2PassNumber.Text = CH2PassNum.ToString();
            CH2FailNumber.Text = CH2FailNum.ToString();
            if (CH2Product != 0)
            {
                CH2PassRate.Text = ((Convert.ToDouble(CH2PassNum) / Convert.ToDouble(CH2Product)) * 100).ToString("f2") + "%";
                //CH2CT.Text = ((Convert.ToDouble(CH2FailNum) / Convert.ToDouble(CH2Product)) * 100).ToString("f2") + "%";
            }
            //读取工单配置
            Setup.Work_Order workorder = con.ReadWorkOrder();
            ProductName.Text = workorder.ProductName;
            ProductModel.Text = workorder.ProductModel;
            WorkOrder.Text = workorder.WorkOrder;
            ProductionItem.Text = workorder.ProductionItem;
            TestType.Text = workorder.TestType;
            TestStation.Text = workorder.TestStation;
            ProductNum.Text = workorder.ProductNum;

            //读取Lin设置
            linconfig = con.ReadLinConfig();

            //CH1LinBaudrate =Convert.ToInt32 (lin.CH1LinBaudrate);
            //CH2LinBaudrate = Convert.ToInt32(lin.CH2LinBaudrate);
        }
        //读取CH1工作电流
        private static void CH1ADC_PORT_DataReceived(string data)
        {
            try
            {
                
                if (data.Contains("VDC"))
                {
                    Form1.CH1ADC_PORT.Write("ADC");
                    Thread.Sleep(500);
                    Logger.Log("ADC1SET");

                }
                if (data.StartsWith("+") && data.Contains("ADC"))
                {
                    int adc_index = 0;
                    adc_index = data.IndexOf("ADC");
                    if (adc_index > 0)
                    {
                        Decimal dData = 0.0M;
                        string adc = data.Remove(adc_index);
                        if (adc.Contains("E"))
                        {
                            dData = Convert.ToDecimal(Decimal.Parse(adc.ToString(), System.Globalization.NumberStyles.Float));
                            adc = dData.ToString();
                            Log log = new Log();
                           // log.PLC_Logmsg(DateTime.Now.ToString() + "CH1静态电流" + adc);
                        }
                        {
                            if (adc.Length > 0)
                            {
                                //CH1uAValue = Convert.ToDouble(adc);
                                //CH1ADC = Math.Round(Convert.ToDouble(adc), 5);
                                //if (CH1RTStep == "UP")
                                //{
                                //    CH1ADC += elec.CH1UPADCComp;
                                //}
                                //if (CH1RTStep == "DOWN")
                                //{
                                //    CH1ADC += elec.CH1DOWNADCComp;
                                //}
                                //if (CH1RTStep == "FWD")
                                //{
                                //    CH1ADC += elec.CH1FWDADCComp;
                                //}
                                //if (CH1RTStep == "RWD")
                                //{
                                //    CH1ADC += elec.CH1RWDADCComp;
                                //}

                            
                                    if (Form1.f1. CH1RTStep == "QC")
                                    {
                                        string[] strArray = adc.ToString().Split(' ');
                                        double number1 = Convert.ToDouble(strArray[0].ToString());
                                        number1 = number1 * 1000000;
                                        string numberFromStringFormat = string.Format("{0:F5}", number1);
                                        Form1.f1.CH1uAarray.Add(number1);
                                    }
                        

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.StackTrace);
               
            }


        }
        /// <summary>
        /// CH1ADC串口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        public void CH1ADCPortOpen(string port, int baudrate)
        {
            try
            {
                //设置端口的参数，包括波特率等

                if (!CH1ADC_PORT._serialPort.IsOpen)
                {
                    CH1ADC_PORT._serialPort.BaudRate = baudrate;
                    CH1ADC_PORT._serialPort.PortName = port;
                    CH1ADC_PORT._serialPort.DataBits = 8;
                    CH1ADC_PORT._serialPort.StopBits = System.IO.Ports.StopBits.One;
                    CH1ADC_PORT._serialPort.Parity = System.IO.Ports.Parity.None;


                    if (!CH1ADC_PORT._serialPort.IsOpen)
                    {
                        CH1ADC_PORT = new SerialPortReader(port, baudrate, 100);
                        CH1ADC_PORT.DataReceived += CH1ADC_PORT_DataReceived;
                        CH1ADC_PORT.Start();
                        Form1.CH1ADC_PORT._serialPort.WriteLine("ADC");
                        Thread.Sleep(200);
                        Form1.CH1ADC_PORT._serialPort.WriteLine("FIXED");
                        Thread.Sleep(200);
                        Form1.CH1ADC_PORT._serialPort.WriteLine("RANGE 1");
                        Thread.Sleep(200);
                     
                        Logger.Log(I18N.GetLangText(dicLang, "CH1ADC打开串口为"+ port));
                    }

                }
                 



                /////////////////
                //if (!ADCPort1.IsOpen)
                //{
                //    ADCPort1.BaudRate = baudrate;
                //    ADCPort1.PortName = port;
                //    ADCPort1.DataBits = 8;
                //    ADCPort1.StopBits = System.IO.Ports.StopBits.One;
                //    ADCPort1.Parity = System.IO.Ports.Parity.None;
                //    ADCPort1.ReadBufferSize = 4096;
                //    ADCPort1.WriteTimeout = 1000;
                //    ADCPort1.ReadTimeout = 1000;
                //    ADCPort1.RtsEnable = false;
                //    ADCPort1.Open();
                //    this.ADCPort1.DataReceived += this.ADCPort1_DataReceived;
                //    if (ADCPort1.IsOpen)
                //    {
                //        byte[] data = Encoding.Default.GetBytes("ADC\r\n");
                //        ADCPort1.Write(data, 0, data.Length);
                //        data = Encoding.Default.GetBytes("FIXED\r\n");
                //        ADCPort1.Write(data, 0, data.Length);
                //        data = Encoding.Default.GetBytes("RANGE 1\r\n");
                //        ADCPort1.Write(data, 0, data.Length);
                //    }
                //}
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CH1ADC:" + ex.Message);
                //MessageBox.Show("CH1ADC:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1ADC") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// CH1VDC串口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        public void CH1VDCPortOpen(string port, int baudrate)
        {
            try
            {
                if (!VDCPort1.IsOpen)
                {
                    //设置端口的参数，包括波特率等
                    VDCPort1.BaudRate = baudrate;
                    VDCPort1.PortName = port;
                    VDCPort1.DataBits = 8;
                    VDCPort1.StopBits = System.IO.Ports.StopBits.One;
                    VDCPort1.Parity = System.IO.Ports.Parity.None;
                    VDCPort1.ReadBufferSize = 4096;
                    VDCPort1.WriteTimeout = 1000;
                    VDCPort1.ReadTimeout = 1000;
                    VDCPort1.RtsEnable = true;

                    VDCPort1.Open();
                    if (VDCPort1.IsOpen)
                    {
                        byte[] data = Encoding.Default.GetBytes("VDC\r\n");
                        VDCPort1.Write(data, 0, data.Length);

                    }
                    this.VDCPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.VDCPort1_DataReceived);
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CH1VDC:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1VDC") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
        //读取CH2工作电流
        private static void CH2ADC_PORT_DataReceived(string data)
        {
            try
            {
                if (data.Contains("VDC"))
                {
                    Form1.CH2ADC_PORT.Write("ADC");
                    Thread.Sleep(500);
                    Logger.Log("ADC2SET");
                    
                }


                if (data.StartsWith("+") && data.Contains("ADC"))
                {
                    int adc_index = 0;
                    adc_index = data.IndexOf("ADC");
                    if (adc_index > 0)
                    {
                        Decimal dData = 0.0M;
                        string adc = data.Remove(adc_index);
                        if (adc.Contains("E"))
                        {
                            dData = Convert.ToDecimal(Decimal.Parse(adc.ToString(), System.Globalization.NumberStyles.Float));
                            adc = dData.ToString();
                        }
                        {
                            if (adc.Length > 0)
                            {
                                //CH1uAValue = Convert.ToDouble(adc);
                                //CH1ADC = Math.Round(Convert.ToDouble(adc), 5);
                                //if (CH1RTStep == "UP")
                                //{
                                //    CH1ADC += elec.CH1UPADCComp;
                                //}
                                //if (CH1RTStep == "DOWN")
                                //{
                                //    CH1ADC += elec.CH1DOWNADCComp;
                                //}
                                //if (CH1RTStep == "FWD")
                                //{
                                //    CH1ADC += elec.CH1FWDADCComp;
                                //}
                                //if (CH1RTStep == "RWD")
                                //{
                                //    CH1ADC += elec.CH1RWDADCComp;
                                //}


                                if (Form1.f1.CH2RTStep == "QC")
                                {
                                    string[] strArray = adc.ToString().Split(' ');
                                    double number1 = Convert.ToDouble(strArray[0].ToString());
                                    number1 = number1 * 1000000;
                                    string numberFromStringFormat = string.Format("{0:F5}", number1);
                                    Form1.f1.CH2uAarray.Add(number1);
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.StackTrace);
              
            }
        }

        /// <summary>
        /// CH2ADC串口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        public void CH2ADCPortOpen(string port, int baudrate)
        {
            try
            {
                if (!CH2ADC_PORT._serialPort.IsOpen)
                {
                    CH2ADC_PORT._serialPort.BaudRate = baudrate;
                    CH2ADC_PORT._serialPort.PortName = port;
                    CH2ADC_PORT._serialPort.DataBits = 8;
                    CH2ADC_PORT._serialPort.StopBits = System.IO.Ports.StopBits.One;
                    CH2ADC_PORT._serialPort.Parity = System.IO.Ports.Parity.None;


                    if (!CH2ADC_PORT._serialPort.IsOpen)
                    {
                        CH2ADC_PORT = new SerialPortReader(port, baudrate, 100);
                        CH2ADC_PORT.DataReceived += CH2ADC_PORT_DataReceived;
                        CH2ADC_PORT.Start();
                        
                        Form1.CH2ADC_PORT.Write("ADC");
                        Thread.Sleep(500);
                        Logger.Log("ADC2:"+ port+baudrate);
                        CH2ADC_PORT.Write("FIXED");
                        Thread.Sleep(200);
                        CH2ADC_PORT.Write("RANGE 1");
                        Thread.Sleep(200);
                     
                    }

                }

            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CH2ADC:" + ex.Message);
                //MessageBox.Show("CH2ADC:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2ADC") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// CH2VDC串口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        public void CH2VDCPortOpen(string port, int baudrate)
        {
            try
            {
                if (!CH2VDCPort.IsOpen)
                {
                    //设置端口的参数，包括波特率等
                    CH2VDCPort.BaudRate = baudrate;
                    CH2VDCPort.PortName = port;
                    CH2VDCPort.DataBits = 8;
                    CH2VDCPort.StopBits = System.IO.Ports.StopBits.One;
                    CH2VDCPort.Parity = System.IO.Ports.Parity.None;
                    CH2VDCPort.ReadBufferSize = 4096;
                    CH2VDCPort.ReadTimeout = 1000;
                    CH2VDCPort.WriteTimeout = 1000;
                    CH2VDCPort.RtsEnable = true;
                    CH2VDCPort.Open();
                    if (CH2VDCPort.IsOpen)
                    {
                        byte[] data = Encoding.Default.GetBytes("VDC\r\n");
                        CH2VDCPort.Write(data, 0, data.Length);

                    }
                    this.CH2VDCPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CH2VDCPort_DataReceived);
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CH2VDC:" + ex.Message);
                //MessageBox.Show("CH2VDC:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2VDC") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

               
        //读取CH1工作电压
        private void VDCPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                int len = VDCPort1.BytesToRead;//获取可以读取的字节数
                if (len > 1)
                {
                    byte[] buff = new byte[len];//创建缓存数据数组
                    VDCPort1.Read(buff, 0, len);//把数据读取到buff数组
                    string str_vdc = Encoding.Default.GetString(buff);
                    if (str_vdc.StartsWith("+") && str_vdc.Contains("VDC"))
                    {
                        int vdc_index = 0;
                        vdc_index = str_vdc.IndexOf("VDC");

                        if (vdc_index > 0)
                        {
                            Decimal dData = 0.0M;
                            string vdc = str_vdc.Remove(vdc_index);
                            if (vdc.Contains("E"))
                            {
                                dData = Convert.ToDecimal(Decimal.Parse(vdc.ToString(), System.Globalization.NumberStyles.Float));
                                vdc = dData.ToString();
                            }

                            //   string vdc = str_vdc.Remove(vdc_index);
                            if (!String.IsNullOrEmpty(vdc) && vdc.Contains("."))
                            {
                                CH1VDC = Math.Round(Convert.ToDouble(vdc), 3);
                                if (CH1RTStep == "UP")
                                {
                                    CH1VDC += elec.CH1UPVDCComp;
                                }
                                if (CH1RTStep == "DOWN")
                                {
                                    CH1VDC += elec.CH1DOWNVDCComp;
                                }
                                if (CH1RTStep == "FWD")
                                {
                                    CH1VDC += elec.CH1FWDVDCComp;
                                }
                                if (CH1RTStep == "RWD")
                                {
                                    CH1VDC += elec.CH1RWDVDCComp;
                                }
                                Invoke((new System.Action(() => //接收计数
                                {
                                    string[] strArray = CH1VDC.ToString().Split(' ');
                                    double number1 = Convert.ToDouble(strArray[0].ToString());
                                    // string numberFromStringFormat = string.Format("{0:F2}", number1);

                                    string numberFromStringFormat = string.Format("{0:F2}", CH1VDC);
                                    CH1RTVDC.Text = numberFromStringFormat;
                                })));
                              
                                CH1VDCList.Add(new ValueClass { Value = CH1VDC });
                            }
                        }
                    }
                    if (VDCPort1.IsOpen)
                    {
                        VDCPort1.DiscardInBuffer();
                        VDCPort1.DiscardOutBuffer();
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "VDCPort:" + ex.Message);
                //MessageBox.Show("VDCPort:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "VDCPort") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

     

        //读取CH2工作电压
        private void CH2VDCPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                int len = CH2VDCPort.BytesToRead;//获取可以读取的字节数
                if (len > 1)
                {
                    byte[] buff = new byte[len];//创建缓存数据数组
                    CH2VDCPort.Read(buff, 0, len);//把数据读取到buff数组
                    string str_vdc = Encoding.Default.GetString(buff);
                    if (str_vdc.StartsWith("+") && str_vdc.Contains("VDC") && str_vdc.Contains("E"))
                    {
                        int vdc_index;
                        vdc_index = str_vdc.IndexOf("VDC");
                        if (vdc_index > 0)
                        {
                            Decimal dData = 0.0M;
                            string vdc = str_vdc.Remove(vdc_index);
                            if (vdc.Contains("E"))
                            {

                                dData = Convert.ToDecimal(Decimal.Parse(vdc.ToString(), System.Globalization.NumberStyles.Float));
                                vdc = dData.ToString();
                            }
                            //string vdc = str_vdc.Remove(vdc_index);
                            if (vdc.Length > 0)
                            {
                                CH2VDC = Math.Round(Convert.ToDouble(vdc), 2);
                                if (CH2RTStep == "UP")
                                {
                                    CH2VDC += elec.CH2UPVDCComp;
                                }
                                if (CH2RTStep == "DOWN")
                                {
                                    CH2VDC += elec.CH2DOWNVDCComp;
                                }
                                if (CH2RTStep == "FWD")
                                {
                                    CH2VDC += elec.CH2FWDVDCComp;
                                }
                                if (CH2RTStep == "RWD")
                                {
                                    CH2VDC += elec.CH2RWDVDCComp;
                                }
                                Invoke((new System.Action(() => //接收计数
                                {
                                    string[] strArray = CH2VDC.ToString().Split(' ');
                                    double number1 = Convert.ToDouble(strArray[0].ToString());
                                    string numberFromStringFormat = string.Format("{0:F2}", CH2VDC);
                                    CH2RTVDC.Text = numberFromStringFormat;
                                })));
                                CH2VDCList.Add(new ValueClass { Value = CH2VDC });
                            }
                        }
                    }
                    if (CH2VDCPort.IsOpen)
                    {
                        CH2VDCPort.DiscardInBuffer();
                        CH2VDCPort.DiscardOutBuffer();
                    }

                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", "CH2VDCPort:" + ex.Message);
                //MessageBox.Show("CH2VDCPort:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2VDCPort") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }
   
        private void PortSend(int type)
        {
            try
            {
                string str = "MEAS?\r\n";
                //String str = SendPort.Text;
                byte[] data = Encoding.Default.GetBytes(str);

                switch (type)
                {
                    //type1为CH1ADCF
                    case 1:
                        //if (ADCPort1.IsOpen)
                        //    ADCPort1.Write(data, 0, data.Length);
                  
                            CH1ADC_PORT.Write("MEAS?");
                            break;

                    case 2:
                        if (VDCPort1.IsOpen)
                            VDCPort1.Write(data, 0, data.Length);
                        break;

                    case 3:
                        CH2ADC_PORT.Write("MEAS?");
                        break;
                    case 4:
                        if (CH2VDCPort.IsOpen)
                            CH2VDCPort.Write(data, 0, data.Length);

                        break;
                }

            }
            catch (Exception ex)
            {

                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "电流电压发送") + ":" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "电流电压发送") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        private string startime;
        //int ch11iread = 0;

        private void CH1ReadElec_Tick(object sender, EventArgs e)
        {

        }

        private void CH2ReadElec_Tick(object sender, EventArgs e)
        {

        }

        private bool CH1ReadElecResult()
        {
            try
            {
                //Thread.Sleep(500);
                List<ValueClass> CH1ADCListTemp = new List<ValueClass>(CH1ADCList.GetRange(0, CH1ADCList.Count > 0 ? CH1ADCList.Count - 1 : 0));
                List<ValueClass> CH1VDCListTemp = new List<ValueClass>(CH1VDCList.GetRange(0, CH1VDCList.Count > 0 ? CH1VDCList.Count - 1 : 0));
                double ch1adcmax = 0;
                double ch1vdcmax = 0;
                double ch1adcmin = 0;
                double ch1vdcmin = 0;
                if (CH1RTStep == "UP")
                {
                    ch1adcmax = elec.CH1UPADCMax;
                    ch1vdcmax = elec.CH1UPVDCMax;
                    ch1adcmin = elec.CH1UPADCMin;
                    ch1vdcmin = elec.CH1UPVDCMin;
                }
                if (CH1RTStep == "DOWN")
                {
                    ch1adcmax = elec.CH1DOWNADCMax;
                    ch1vdcmax = elec.CH1DOWNVDCMax;
                    ch1adcmin = elec.CH1DOWNADCMin;
                    ch1vdcmin = elec.CH1DOWNVDCMin;
                }
                if (CH1RTStep == "FWD")
                {
                    ch1adcmax = elec.CH1FWDADCMax;
                    ch1vdcmax = elec.CH1FWDVDCMax;
                    ch1adcmin = elec.CH1FWDADCMin;
                    ch1vdcmin = elec.CH1FWDVDCMin;
                }
                if (CH1RTStep == "RWD")
                {
                    ch1adcmax = elec.CH1RWDADCMax;
                    ch1vdcmax = elec.CH1RWDVDCMax;
                    ch1adcmin = elec.CH1RWDADCMin;
                    ch1vdcmin = elec.CH1RWDVDCMin;
                }
                if (CH1VDCListTemp?.Count > 0)
                {
                    CH1VDCMax = CH1VDCListTemp.Max(x => x.Value);
                }
                if (CH1ADCListTemp?.Count > 0)
                {
                    CH1ADCMax = CH1ADCListTemp.Max(x => x.Value);
                }
                // if (ADCPort1.IsOpen && VDCPort1.IsOpen)
                {
                    if (CH1ADCListTemp?.Count >= 0)
                    {
                        var lstADCFind = CH1ADCListTemp.FindAll(p => p.Value >= ch1adcmin && p.Value <= ch1adcmax);
                        //if (lstADCFind.Count == 0) CH1ReadElecCount++;
                        if (lstADCFind.Count == 0)
                        {
                            CH1ADCresult = "NG";

                            if (CH1RTStep == "UP")
                            {
                                plc.CH1UPADCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH1ADCMax.ToString(), "A", ch1adcmax.ToString(), ch1adcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "DOWN")
                            {
                                plc.CH1DOWNADCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH1ADCMax.ToString(), "A", ch1adcmax.ToString(), ch1adcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "FWD")
                            {
                                plc.CH1FWDADCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH1ADCMax.ToString(), "A", ch1adcmax.ToString(), ch1adcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "RWD")
                            {
                                plc.CH1RWDADCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH1ADCMax.ToString(), "A", ch1adcmax.ToString(), ch1adcmin.ToString(), "NG");
                            }
                            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                            {
                                FlowNG(1);
                                return false;
                            }
                        }
                    }
                    if (CH1VDCListTemp?.Count >= 0)
                    {
                        var lstVDCFind = CH1VDCListTemp.FindAll(p => p.Value >= ch1vdcmin && p.Value <= ch1vdcmax);
                        //if (lstVDCFind.Count == 0) CH1ReadElecCount++;
                        if (lstVDCFind.Count == 0)
                        {
                            CH1VDCresult = "NG";
                            if (CH1RTStep == "UP")
                            {
                                plc.CH1UPVDCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH1VDCMax.ToString(), "V", ch1vdcmax.ToString(), ch1vdcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "DOWN")
                            {
                                plc.CH1DOWNVDCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH1VDCMax.ToString(), "V", ch1vdcmax.ToString(), ch1vdcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "FWD")
                            {
                                plc.CH1FWDVDCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH1VDCMax.ToString(), "V", ch1vdcmax.ToString(), ch1vdcmin.ToString(), "NG");
                            }
                            else if (CH1RTStep == "RWD")
                            {
                                plc.CH1RWDVDCNG();
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH1VDCMax.ToString(), "V", ch1vdcmax.ToString(), ch1vdcmin.ToString(), "NG");
                            }
                            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                            {
                                FlowNG(1);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1电流电压") + ":" + ex.Message);
                //CH1ReadElec.Stop();
                //MessageBox.Show("CH1电流电压：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH1电流电压") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
                // return false;
                return true;
            }
        }

        private bool CH2ReadElecResult()
        {
            try
            {
                //Thread.Sleep(500);
                List<ValueClass> CH2ADCListTemp = new List<ValueClass>(CH2ADCList.GetRange(0, CH2ADCList.Count > 0 ? CH2ADCList.Count - 1 : 0));
                List<ValueClass> CH2VDCListTemp = new List<ValueClass>(CH2VDCList.GetRange(0, CH2VDCList.Count > 0 ? CH2VDCList.Count - 1 : 0));

                double ch2adcmax = 0;
                double ch2vdcmax = 0;
                double ch2adcmin = 0;
                double ch2vdcmin = 0;
                if (CH2RTStep == "UP")
                {
                    ch2adcmax = elec.CH2UPADCMax;
                    ch2vdcmax = elec.CH2UPVDCMax;
                    ch2adcmin = elec.CH2UPADCMin;
                    ch2vdcmin = elec.CH2UPVDCMin;
                }
                if (CH2RTStep == "DOWN")
                {
                    ch2adcmax = elec.CH2DOWNADCMax;
                    ch2vdcmax = elec.CH2DOWNVDCMax;
                    ch2adcmin = elec.CH2DOWNADCMin;
                    ch2vdcmin = elec.CH2DOWNVDCMin;
                }
                if (CH2RTStep == "FWD")
                {
                    ch2adcmax = elec.CH2FWDADCMax;
                    ch2vdcmax = elec.CH2FWDVDCMax;
                    ch2adcmin = elec.CH2FWDADCMin;
                    ch2vdcmin = elec.CH2FWDVDCMin;
                }
                if (CH2RTStep == "RWD")
                {
                    ch2adcmax = elec.CH2RWDADCMax;
                    ch2vdcmax = elec.CH2RWDVDCMax;
                    ch2adcmin = elec.CH2RWDADCMin;
                    ch2vdcmin = elec.CH2RWDVDCMin;
                }

                if (CH2VDCListTemp?.Count > 0)
                {
                    CH2VDCMax = CH2VDCListTemp.Max(x => x.Value);
                }
                if (CH2ADCListTemp?.Count > 0)
                {
                    CH2ADCMax = CH2ADCListTemp.Max(x => x.Value);
                }
                //     if (CH2ADCPort.IsOpen && CH2VDCPort.IsOpen)
                {
                    if (CH2ADCListTemp?.Count >= 0)
                    {
                        var lstADCFind = CH2ADCListTemp.FindAll(p => p.Value >= ch2adcmin && p.Value <= ch2adcmax);
                        //if (CH2ADCListTemp.Max() > ch2adcmax || CH2ADCListTemp.Min() < ch2adcmin)
                        //if (lstADCFind.Count == 0) CH2ReadElecCount++;
                        if (lstADCFind.Count == 0)
                        {
                            CH2ADCresult = "NG";

                            if (CH2RTStep == "UP")
                            {
                                plc.CH2UPADCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH2ADCMax.ToString(), "A", ch2adcmax.ToString(), ch2adcmin.ToString(), "NG");
                            }
                            else if (CH2RTStep == "DOWN")
                            {
                                plc.CH2DOWNADCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH2ADCMax.ToString(), "A", ch2adcmax.ToString(), ch2adcmin.ToString(), "NG");
                            }
                            else if (CH2RTStep == "FWD")
                            {
                                plc.CH2FWDADCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH2ADCMax.ToString(), "A", ch2adcmax.ToString(), ch2adcmin.ToString(), "NG");
                            }
                            else if (CH2RTStep == "RWD")
                            {
                                plc.CH2RWDADCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH2ADCMax.ToString(), "A", ch2adcmax.ToString(), ch2adcmin.ToString(), "NG");
                            }
                            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD")
                            {
                                                  FlowNG(2);
                                return false;
                            }
                        }
                    }
                    if (CH2VDCListTemp?.Count >= 0)
                    {
                        var lstVDCFind = CH2VDCListTemp.FindAll(p => p.Value >= ch2vdcmin && p.Value <= ch2vdcmax);
                        //if (lstVDCFind.Count == 0) CH2ReadElecCount++;
                        if (lstVDCFind.Count == 0)
                        {
                            CH2VDCresult = "NG";

                            if (CH2RTStep == "UP")
                            {
                                plc.CH2UPVDCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH2VDCMax.ToString(), "V", ch2vdcmax.ToString(), ch2vdcmin.ToString(), "NG");
                            }
                            else if (CH2RTStep == "DOWN")
                            {
                                plc.CH2DOWNVDCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH2VDCMax.ToString(), "V", ch2vdcmax.ToString(), ch2vdcmin.ToString(), "NG");
                                FlowNG(2);
                            }
                            else if (CH2RTStep == "FWD")
                            {
                                plc.CH2FWDVDCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH2VDCMax.ToString(), "V", ch2vdcmax.ToString(), ch2vdcmin.ToString(), "NG");
                                FlowNG(2);
                            }
                            else if (CH2RTStep == "RWD")
                            {
                                plc.CH2RWDVDCNG();
                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH2VDCMax.ToString(), "V", ch2vdcmax.ToString(), ch2vdcmin.ToString(), "NG");
                                FlowNG(2);
                            }
                            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD")
                            {
                                FlowNG(2);
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //CH2ReadElec.Stop();
                //MessageBox.Show("CH2电流电压：" + ex.Message);
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2电流电压") + ":" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH2电流电压") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
                // return false;
                return true;
            }
        }
        List<double> data2 = new List<double>();
        /// <summary>
        /// CH1读取静态电流并判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1ReaduA_Tick(object sender, EventArgs e)
        {

            //计算时间
            ch1uAendtime = System.DateTime.Now.Ticks;
            TimeSpan ts1 = new TimeSpan(ch1uAstarttime);
            TimeSpan ts2 = new TimeSpan(ch1uAendtime);
            TimeSpan utime = ts2.Subtract(ts1).Duration();
            double uatime = utime.TotalSeconds;
            double maxadc=0;
            if (uatime > 6)
            {
                int i, j;
                i = j = 0;

                CH1ReaduA.Stop();
                int len = 0;
                double sum = 0;
                int overlen = 0;
                i = j = 0;
                for (; i < CH1uAarray.Count; i++)
                {
                    if (CH1uAarray[i] > 0)
                    {

                        if (CH1uAarray[i] < 50)
                        {
                            len++;
                            sum += CH1uAarray[i];
                        }
                        else if (CH1uAarray[i] > 400)
                        {
                            overlen++;
                            if(maxadc < CH1uAarray[i])
                                maxadc = CH1uAarray[i];
                            // sum = CH1uAarray[i];
                        }
                    }

                }
                i = CH1uAarray.Count;
 
                double CH1uAResult = 0;
                // if (overlen < 2)
                if (len > 0)
                    CH1uAResult = (sum / len);
                else if (overlen > 0)
                {
                    if (maxadc < 2000)
                    {
                        CH1uAResult = 22;
                    }
                }
                //else
                //    CH1uAResult = CH1uAarray[j];
                CH1TestResult = new Model.TestResult();
                CH1uAResult += elec.CH1ElecComp;//电流补偿
                                                //CH1uAarray.Clear(); 10.9
                log.PLC_Logmsg("静态CH1" + CH1uAResult.ToString());
                if (CH1uAResult < 0)
                {
                    CH1uAResult = 0 - CH1uAResult;
                }
                string numberFromStringFormat = string.Format("{0:F1}", CH1uAResult);
                CH1uAResult = Convert.ToDouble(numberFromStringFormat);
                CH1RTElec.Text = CH1uAResult.ToString();
                //10.9
                plc.WriteCH1QC(false);
                CH1TestResult.Elec = CH1uAResult;
                //if (CH1uA > elec.CH1ElecMax || CH1uA < elec.CH1ElecMin || plc.CH1uA == 0)
                if (CH1uAResult > elec.CH1ElecMax || CH1uAResult < elec.CH1ElecMin)
                {
                    plc.CH1uANG();
                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), CH1uAResult.ToString(), "uA", elec.CH1ElecMax.ToString(), elec.CH1ElecMin.ToString(), "NG");
                    CH1Elecresult = "NG";
                    FlowNG(1);
                }
                else
                {
                    plc.CH1uAOK();
                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), CH1uAResult.ToString(), "uA", elec.CH1ElecMax.ToString(), elec.CH1ElecMin.ToString(), "OK");
                    CH1Elecresult = "OK";
                    CH1Step += 1;
                    CH1Method(CH1Step);
                }
            }
        }

        /// <summary>
        /// CH2读取静态电流并判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH2ReaduA_Tick(object sender, EventArgs e)
        {
              //计算时间
            ch2uAendtime = System.DateTime.Now.Ticks;
            TimeSpan ts1 = new TimeSpan(ch2uAstarttime);
            TimeSpan ts2 = new TimeSpan(ch2uAendtime);
            TimeSpan utime = ts2.Subtract(ts1).Duration();
            double uatime = utime.TotalSeconds;

            if (uatime > 6)
            {

                int i, j;
                i = j = 0;

                CH2ReaduA.Stop();
                int len = 0;
                double sum = 0;
                int overlen = 0;
                i = j = 0;
                double maxadc = 0;
                for (; i < CH2uAarray.Count; i++)
                {
                    if (CH2uAarray[i] > 0)
                    {

                        if (CH2uAarray[i] < 50)
                        {
                            len++;
                            sum += CH2uAarray[i];
                        }
                        else if (CH2uAarray[i] > 400)
                        {
                            overlen++;
                            if (maxadc < CH2uAarray[i])
                                maxadc = CH2uAarray[i];
                            // sum = CH1uAarray[i];
                        }
                    }

                }
                i = CH2uAarray.Count;

                double CH2uAResult = 0;

                if (len > 0)
                    CH2uAResult = (sum / len);
                else if (overlen > 0)
                {
                    if (maxadc < 2000)
                    {
                        CH2uAResult = 23;
                    }
                }
                  
                CH2TestResult = new Model.TestResult();
                CH2uAResult += elec.CH2ElecComp;
                if (CH2uAResult < 0)
                {
                    CH2uAResult = 0 - CH2uAResult;
                }
                string numberFromStringFormat = string.Format("{0:F1}", CH2uAResult);
                CH2uAResult = Convert.ToDouble(numberFromStringFormat);
            
                CH2RTElec.Text = CH2uAResult.ToString();
                plc.WriteCH2QC(false);
                //Debug.WriteLine("CH2uAarray:" + string.Join(",", CH2uAarray));
                CH2TestResult.Elec = CH2uAResult;
                //if (CH2uA > elec.CH2ElecMax || CH2uA < elec.CH2ElecMin || plc.CH2uA == 0)
                if (CH2uAResult > elec.CH2ElecMax || CH2uAResult < elec.CH2ElecMin)
                {
                    plc.CH2uANG();
                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), CH2uAResult.ToString(), "uA", elec.CH2ElecMax.ToString(), elec.CH2ElecMin.ToString(), "NG");
                    CH2Elecresult = "NG";
                    FlowNG(2);
                }
                else
                {
                    plc.CH2uAOK();
                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), CH2uAResult.ToString(), "uA", elec.CH2ElecMax.ToString(), elec.CH2ElecMin.ToString(), "OK");
                    CH2Elecresult = "OK";
                    CH2Step += 1;
                    CH2Method(CH2Step);
                }
            }
        }

        /// <summary>
        /// 读取电压电流端口
        /// </summary>
        private void ReadMultimeterPort()
        {
            //string dialog;
            //dialog = "MultimeterPort.ini";
            //ConfigINI mesconfig = new ConfigINI("Port", dialog);
            ReadConfig con = new ReadConfig();
            Setup.Port port = con.ReadPort();
            if ((!String.IsNullOrEmpty(port.CH1ADCBaud)) && (!String.IsNullOrEmpty(port.CH1ADCPort)))
            {
                CH1ADCPortOpen(port.CH1ADCPort, int.Parse(port.CH1ADCBaud));
            }

            if ((!String.IsNullOrEmpty(port.CH1VDCBaud)) && (!String.IsNullOrEmpty(port.CH1VDCPort)))
            {
                CH1VDCPortOpen(port.CH1VDCPort, int.Parse(port.CH1VDCBaud));
            }

            if ((!String.IsNullOrEmpty(port.CH2ADCBaud)) && (!String.IsNullOrEmpty(port.CH2ADCPort)))
            {
                CH2ADCPortOpen(port.CH2ADCPort, int.Parse(port.CH2ADCBaud));
            }

            if (!String.IsNullOrEmpty(port.CH2VDCBaud) && (!String.IsNullOrEmpty(port.CH2VDCPort)))
            {
                CH2VDCPortOpen(port.CH2VDCPort, int.Parse(port.CH2VDCBaud));
            }
            if (!String.IsNullOrEmpty(port.CodePort) && (!String.IsNullOrEmpty(port.CodeBaud)))
            {
                CodePortOpen(port.CodePort, int.Parse(port.CodeBaud));
            }
            if (!String.IsNullOrEmpty(port.CH2CodePort) && (!String.IsNullOrEmpty(port.CH2CodeBaud)))
            {
                CH2CodePortOpen(port.CH2CodePort, int.Parse(port.CH2CodeBaud));
            }

            if (!String.IsNullOrEmpty(port.CH1FlowPort) && !String.IsNullOrEmpty(port.CH1FlowBaud))
            {
                FlowOpen(1, port.CH1FlowPort, int.Parse(port.CH1FlowBaud));
            }

            if (!String.IsNullOrEmpty(port.CH2FlowPort) && !String.IsNullOrEmpty(port.CH2FlowBaud))
            {
                FlowOpen(2, port.CH2FlowPort, int.Parse(port.CH2FlowBaud));
            }

            if (!String.IsNullOrEmpty(port.CH3FlowPort) && !String.IsNullOrEmpty(port.CH3FlowBaud))
            {
                FlowOpen(3, port.CH3FlowPort, int.Parse(port.CH3FlowBaud));
            }

            if (!String.IsNullOrEmpty(port.CH4FlowPort) && !String.IsNullOrEmpty(port.CH4FlowBaud))
            {
                FlowOpen(4, port.CH4FlowPort, int.Parse(port.CH4FlowBaud));
            }

            //程控电源
            if (!String.IsNullOrEmpty(port.CKCH1Port) && !String.IsNullOrEmpty(port.CKCH1Baud))
            {
                FlowOpen(5, port.CKCH1Port, int.Parse(port.CKCH1Baud));
            }
            if (!String.IsNullOrEmpty(port.CKCH2Port) && !String.IsNullOrEmpty(port.CKCH2Baud))
            {
                FlowOpen(6, port.CKCH2Port, int.Parse(port.CKCH2Baud));
            }
        }

        /// <summary>
        /// 流量计端口打开
        /// </summary>
        private void FlowOpen(int CH, string port, int baudrate)
        {
            try
            {
                switch (CH)
                {
                    case 1:
                        //设置端口的参数，包括波特率等
                        //CH1FlowPort.BaudRate = baudrate;
                        //CH1FlowPort.PortName = port;
                        //CH1FlowPort.DataBits = 8;
                        //CH1FlowPort.StopBits = System.IO.Ports.StopBits.One;
                        //CH1FlowPort.Parity = System.IO.Ports.Parity.None;
                        //CH1FlowPort.Open();
                        //busRtuClientCH1?.Close();
                        if (busRtuClientCH1 == null)
                        {
                            busRtuClientCH1 = new ModbusRtu(byte.Parse("1"));
                            busRtuClientCH1.SerialPortInni(sp =>
                            {
                                sp.PortName = port;
                                sp.BaudRate = baudrate;
                                sp.DataBits = 8;
                                sp.StopBits = System.IO.Ports.StopBits.One;
                                sp.Parity = System.IO.Ports.Parity.None;
                            });
                        }
                        if (!busRtuClientCH1.IsOpen()) busRtuClientCH1.Open();
                        break;

                    case 2:
                        //设置端口的参数，包括波特率等
                        //CH2FlowPort.BaudRate = baudrate;
                        //CH2FlowPort.PortName = port;
                        //CH2FlowPort.DataBits = 8;
                        //CH2FlowPort.StopBits = System.IO.Ports.StopBits.One;
                        //CH2FlowPort.Parity = System.IO.Ports.Parity.None;
                        //CH2FlowPort.Open();
                        //busRtuClientCH2?.Close();


                        //if (busRtuClientCH2 == null)
                        //{
                        //    busRtuClientCH2 = new ModbusRtu(byte.Parse("1"));
                        //    busRtuClientCH2.SerialPortInni(sp =>
                        //    {
                        //        sp.PortName = port;
                        //        sp.BaudRate = baudrate;
                        //        sp.DataBits = 8;
                        //        sp.StopBits = System.IO.Ports.StopBits.One;
                        //        sp.Parity = System.IO.Ports.Parity.None;
                        //    });
                        //}
                        //if (!busRtuClientCH2.IsOpen()) busRtuClientCH2.Open();
                        break;

                    case 3:
                        //设置端口的参数，包括波特率等
                        //CH3FlowPort.BaudRate = baudrate;
                        //CH3FlowPort.PortName = port;
                        //CH3FlowPort.DataBits = 8;
                        //CH3FlowPort.StopBits = System.IO.Ports.StopBits.One;
                        //CH3FlowPort.Parity = System.IO.Ports.Parity.None;
                        //CH3FlowPort.Open();
                        //busRtuClientCH3?.Close();


                        //if (busRtuClientCH3 == null)
                        //{
                        //    busRtuClientCH3 = new ModbusRtu(byte.Parse("1"));
                        //    busRtuClientCH3.SerialPortInni(sp =>
                        //    {
                        //        sp.PortName = port;
                        //        sp.BaudRate = baudrate;
                        //        sp.DataBits = 8;
                        //        sp.StopBits = System.IO.Ports.StopBits.One;
                        //        sp.Parity = System.IO.Ports.Parity.None;
                        //    });
                        //    busRtuClientCH3.Open();
                        //}
                        //if (!busRtuClientCH3.IsOpen()) busRtuClientCH3.Open();
                        break;

                    case 4:
                        //设置端口的参数，包括波特率等
                        //CH4FlowPort.BaudRate = baudrate;
                        //CH4FlowPort.PortName = port;
                        //CH4FlowPort.DataBits = 8;
                        //CH4FlowPort.StopBits = System.IO.Ports.StopBits.One;
                        //CH4FlowPort.Parity = System.IO.Ports.Parity.None;
                        //CH4FlowPort.Open();
                        //busRtuClientCH4?.Close();


                        //if (busRtuClientCH4 == null)
                        //{
                        //    busRtuClientCH4 = new ModbusRtu(byte.Parse("1"));
                        //    busRtuClientCH4.SerialPortInni(sp =>
                        //    {
                        //        sp.PortName = port;
                        //        sp.BaudRate = baudrate;
                        //        sp.DataBits = 8;
                        //        sp.StopBits = System.IO.Ports.StopBits.One;
                        //        sp.Parity = System.IO.Ports.Parity.None;
                        //    });
                        //}
                        //if (!busRtuClientCH4.IsOpen()) busRtuClientCH4.Open();
                        break;

                    case 5:
                        //设置端口的参数，包括波特率等
                        if (!CH1POWER._serialPort.IsOpen)
                        {
                            CH1POWER._serialPort.BaudRate = baudrate;
                            CH1POWER._serialPort.PortName = port;
                            CH1POWER._serialPort.DataBits = 8;
                            CH1POWER._serialPort.StopBits = System.IO.Ports.StopBits.One;
                            CH1POWER._serialPort.Parity = System.IO.Ports.Parity.None;

                          
                            if (!CH1POWER._serialPort.IsOpen)
                            {
                                CH1POWER = new SerialPortReader(port, baudrate, 1);
                                CH1POWER.DataReceived += CH1POWER_DataReceived;
                                CH1POWER.Start();
                                
                                CH1POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                                CH1POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                                CH1POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                            }


                       //     if (CH1POWER._serialPort.IsOpen) CH1POWER._serialPort.WriteLine();
                        }

                        break;

                    case 6:
                        //设置端口的参数，包括波特率等

                        if (!CH2POWER._serialPort.IsOpen)
                        {
                            CH2POWER._serialPort.BaudRate = baudrate;
                            CH2POWER._serialPort.PortName = port;
                            CH2POWER._serialPort.DataBits = 8;
                            CH2POWER._serialPort.StopBits = System.IO.Ports.StopBits.One;
                            CH2POWER._serialPort.Parity = System.IO.Ports.Parity.None;


                            if (!CH2POWER._serialPort.IsOpen)
                            {
                                CH2POWER = new SerialPortReader(port, baudrate, 1);
                                CH2POWER.DataReceived += CH2POWER_DataReceived;
                                CH2POWER.Start();
                                CH2POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                                CH2POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                                CH2POWER.Write("SYST:REM");
                                System.Threading.Thread.Sleep(200);
                            }
                                                      
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "流量计端口打开") + ":" + ex.Message);
                //MessageBox.Show("流量计端口打开" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "端口打开") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// 条码端口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        private void CodePortOpen(string port, int baudrate)
        {
            try
            {
                if (!CodePort1.IsOpen)
                {
                    //设置端口的参数，包括波特率等
                    CodePort1.BaudRate = baudrate;
                    CodePort1.PortName = port;
                    CodePort1.DataBits = 8;
                    CodePort1.StopBits = System.IO.Ports.StopBits.One;
                    CodePort1.Parity = System.IO.Ports.Parity.None;
                    CodePort1.Open();
                    this.CodePort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CodePort1_DataReceived);
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "条码端口打开") + ":" + ex.Message);
                //MessageBox.Show("条码端口打开" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "条码端口打开") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// 条码端口打开
        /// </summary>
        /// <param name="port"></param>
        /// <param name="baudrate"></param>
        private void CH2CodePortOpen(string port, int baudrate)
        {
            try
            {
                if (!CodePort2.IsOpen)
                {
                    //设置端口的参数，包括波特率等
                    CodePort2.BaudRate = baudrate;
                    CodePort2.PortName = port;
                    CodePort2.DataBits = 8;
                    CodePort2.StopBits = System.IO.Ports.StopBits.One;
                    CodePort2.Parity = System.IO.Ports.Parity.None;
                    CodePort2.Open();
                    this.CodePort2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.CodePort2_DataReceived);
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "条码端口打开") + ":" + ex.Message);
                //MessageBox.Show("条码端口打开" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "条码端口打开") + ":" + ex.Message);
                Logger.Log(ex.StackTrace);
            }
        }

        /// <summary>
        /// 读取流量相关的上下限参数、电压电流参数
        /// </summary>Elec
        private void ReadFlow()
        {
            ReadConfig con = new ReadConfig();
            Flow = con.ReadFlow();
            elec = con.ReadElectricity();
        }
        public bool CH1change;
        public bool CH2change;

        /// <summary>
        /// 读取程序步骤
        /// </summary>
        private void ReadLin()
        {
            try
            {
                Setup.Order ord;
                ReadConfig con = new ReadConfig();
                ord = con.ReadLin();
                if (ord.CH1IGN) { }//暂时不使用
                if (ord.CH2IGN) { }//暂时不使用
                if (ord.CH1UpDownChange)
                {
                    CH1change = true;
                }
                else
                    CH1change = false;
                if (ord.CH2UpDownChange)
                {
                    CH2change = true;
                }
                else
                {
                    CH2change = false;
                }



                if (ord.CH1HighLevel)
                {
                    Form1.f1.plc.WriteCH1HLevelTure();
                }
                else
                {
                    Form1.f1.plc.WriteCH1HLevelFalse();
                }

                if (ord.CH2HighLevel)
                {
                    Form1.f1.plc.WriteCH2HLevelTure();
                }
                else
                {
                    Form1.f1.plc.WriteCH2HLevelFalse();
                }

                if (ord.CH1LIN)
                {

                    Form1.f1.plc.WriteCH1LINTure();
                }
                else
                {

                    Form1.f1.plc.WriteCH1LINFlase();
                }

                if (ord.CH2LIN)
                {

                    Form1.f1.plc.WriteCH2LINTure();
                }
                else
                {

                    Form1.f1.plc.WriteCH2LINFlase();
                }










                //if (ord.CH1UpDownChange)

                if (ord.CH1UP)
                {
                    //CH1Order.Add("UP");
                    CH1Order.Add(" ");
                }
                if (ord.CH1DOWN)
                {
                    //CH1Order.Add("DOWN");
                    CH1Order.Add(" ");
                }

                if (ord.CH1FWD)
                {
                    //CH1Order.Add("FWD");
                    CH1Order.Add(" ");
                }

                if (ord.CH1RWD)
                {
                    //CH1Order.Add("RWD");
                    CH1Order.Add(" ");
                }
                if (ord.CH1UPLeak)
                {
                    //CH1Order.Add("UPLeak");
                    CH1Order.Add(" ");
                }
                if (ord.CH1DOWNLeak)
                {
                    //CH1Order.Add("DOWNLeak");
                    CH1Order.Add(" ");
                }
                if (ord.CH1FWDLeak)
                {
                    //CH1Order.Add("FWDLeak");
                    CH1Order.Add(" ");
                }
                if (ord.CH1QuiescentCurrnt)
                {
                    //CH1Order.Add("QC");
                    CH1Order.Add(" ");
                }
                if (!String.IsNullOrEmpty(ord.CH1UPindex))
                {
                    //CH1Order.Remove("UP");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1UPindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1UPindex) - 1, "UP");
                }

                if (!String.IsNullOrEmpty(ord.CH1DOWNindex))
                {
                    //CH1Order.Remove("DOWN");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1DOWNindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1DOWNindex) - 1, "DOWN");
                }

                if (!String.IsNullOrEmpty(ord.CH1FWDindex))
                {
                    //CH1Order.Remove("FWD");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1FWDindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1FWDindex) - 1, "FWD");
                }

                if (!String.IsNullOrEmpty(ord.CH1RWDindex))
                {
                    //CH1Order.Remove("RWD");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1RWDindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1RWDindex) - 1, "RWD");
                }

                if (!String.IsNullOrEmpty(ord.CH1UPLeakindex))
                {
                    //CH1Order.Remove("UPLeak");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1UPLeakindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1UPLeakindex) - 1, "UPLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH1DOWNLeakindex))
                {
                    //CH1Order.Remove("DOWNLeak");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1DOWNLeakindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1DOWNLeakindex) - 1, "DOWNLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH1FWDLeakindex))
                {
                    //CH1Order.Remove("FWDLeak");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1FWDLeakindex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1FWDLeakindex) - 1, "FWDLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH1QuiescentCurrntIndex))
                {
                    //CH1Order.Remove("QC");
                    CH1Order.RemoveRange(Convert.ToInt32(ord.CH1QuiescentCurrntIndex) - 1, 1);
                    CH1Order.Insert(Convert.ToInt32(ord.CH1QuiescentCurrntIndex) - 1, "QC");
                }

                while (CH1Order.Remove(" ")) ;
                CH1Pump = ord.CH1Pump;
                if (ord.CH2UP)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2DOWN)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2FWD)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2RWD)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2UPLeak)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2DOWNLeak)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2FWDLeak)
                {
                    CH2Order.Add(" ");
                }
                if (ord.CH2QuiescentCurrnt)
                {
                    CH2Order.Add(" ");
                }
                if (!String.IsNullOrEmpty(ord.CH2UPindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2UPindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2UPindex) - 1, "UP");
                }
                if (!String.IsNullOrEmpty(ord.CH2DOWNindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2DOWNindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2DOWNindex) - 1, "DOWN");
                }
                if (!String.IsNullOrEmpty(ord.CH2FWDindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2FWDindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2FWDindex) - 1, "FWD");
                }

                if (!String.IsNullOrEmpty(ord.CH2RWDindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2RWDindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2RWDindex) - 1, "RWD");
                }
                if (!String.IsNullOrEmpty(ord.CH2UPLeakindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2UPLeakindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2UPLeakindex) - 1, "UPLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH2DOWNLeakindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2DOWNLeakindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2DOWNLeakindex) - 1, "DOWNLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH2FWDLeakindex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2FWDLeakindex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2FWDLeakindex) - 1, "FWDLeak");
                }
                if (!String.IsNullOrEmpty(ord.CH2QuiescentCurrntIndex))
                {
                    CH2Order.RemoveRange(Convert.ToInt32(ord.CH2QuiescentCurrntIndex) - 1, 1);
                    CH2Order.Insert(Convert.ToInt32(ord.CH2QuiescentCurrntIndex) - 1, "QC");
                }
                while (CH2Order.Remove(" ")) ;
                CH2Pump = ord.CH2Pump;
                {
                    if (Form1.f1.plc.PLCIsRun)
                    {
                        if (CH1Pump == true)
                        {
                            Form1.f1.plc.CH1Pump();
                        }
                        else
                        {
                            Form1.f1.plc.CH1Machine();
                        }
                    }
                    else
                    {
                        // MessageBox.Show("PLC未通讯！");
                        Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                    }
                }

                //AD
                if (ord.ADUP)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADDOWN)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADFWD)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADRWD)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADUPLeak)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADDOWNLeak)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADFWDLeak)
                {
                    ADOrder.Add(" ");
                }
                if (ord.ADQuiescentCurrnt)
                {
                    ADOrder.Add(" ");
                }
                if (!String.IsNullOrEmpty(ord.ADUPindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADUPindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADUPindex) - 1, "UP");
                }

                if (!String.IsNullOrEmpty(ord.ADDOWNindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADDOWNindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADDOWNindex) - 1, "DOWN");
                }

                if (!String.IsNullOrEmpty(ord.ADFWDindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADFWDindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADFWDindex) - 1, "FWD");
                }

                if (!String.IsNullOrEmpty(ord.ADRWDindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADRWDindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADRWDindex) - 1, "RWD");
                }

                if (!String.IsNullOrEmpty(ord.ADUPLeakindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADUPLeakindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADUPLeakindex) - 1, "UPLeak");
                }
                if (!String.IsNullOrEmpty(ord.ADDOWNLeakindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADDOWNLeakindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADDOWNLeakindex) - 1, "DOWNLeak");
                }
                if (!String.IsNullOrEmpty(ord.ADFWDLeakindex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADFWDLeakindex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADFWDLeakindex) - 1, "FWDLeak");
                }
                if (!String.IsNullOrEmpty(ord.ADQuiescentCurrntIndex))
                {
                    ADOrder.RemoveRange(Convert.ToInt32(ord.ADQuiescentCurrntIndex) - 1, 1);
                    ADOrder.Insert(Convert.ToInt32(ord.ADQuiescentCurrntIndex) - 1, "QC");
                }

                while (ADOrder.Remove(" ")) ;
                //BE
                if (ord.BEUP)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEDOWN)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEFWD)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BERWD)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEUPLeak)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEDOWNLeak)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEFWDLeak)
                {
                    BEOrder.Add(" ");
                }
                if (ord.BEQuiescentCurrnt)
                {
                    BEOrder.Add(" ");
                }
                if (!String.IsNullOrEmpty(ord.BEUPindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEUPindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEUPindex) - 1, "UP");
                }

                if (!String.IsNullOrEmpty(ord.BEDOWNindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEDOWNindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEDOWNindex) - 1, "DOWN");
                }

                if (!String.IsNullOrEmpty(ord.BEFWDindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEFWDindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEFWDindex) - 1, "FWD");
                }

                if (!String.IsNullOrEmpty(ord.BERWDindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BERWDindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BERWDindex) - 1, "RWD");
                }

                if (!String.IsNullOrEmpty(ord.BEUPLeakindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEUPLeakindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEUPLeakindex) - 1, "UPLeak");
                }
                if (!String.IsNullOrEmpty(ord.BEDOWNLeakindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEDOWNLeakindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEDOWNLeakindex) - 1, "DOWNLeak");
                }
                if (!String.IsNullOrEmpty(ord.BEFWDLeakindex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEFWDLeakindex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEFWDLeakindex) - 1, "FWDLeak");
                }
                if (!String.IsNullOrEmpty(ord.BEQuiescentCurrntIndex))
                {
                    BEOrder.RemoveRange(Convert.ToInt32(ord.BEQuiescentCurrntIndex) - 1, 1);
                    BEOrder.Insert(Convert.ToInt32(ord.BEQuiescentCurrntIndex) - 1, "QC");
                }
                while (BEOrder.Remove(" ")) ;

                //CF

                if (ord.CFUP)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFDOWN)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFFWD)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFRWD)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFUPLeak)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFDOWNLeak)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFFWDLeak)
                {
                    CFOrder.Add(" ");
                }
                if (ord.CFQuiescentCurrnt)
                {
                    CFOrder.Add(" ");
                }
                if (!String.IsNullOrEmpty(ord.CFUPindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFUPindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFUPindex) - 1, "UP");
                }

                if (!String.IsNullOrEmpty(ord.CFDOWNindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFDOWNindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFDOWNindex) - 1, "DOWN");
                }

                if (!String.IsNullOrEmpty(ord.CFFWDindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFFWDindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFFWDindex) - 1, "FWD");
                }

                if (!String.IsNullOrEmpty(ord.CFRWDindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFRWDindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFRWDindex) - 1, "RWD");
                }

                if (!String.IsNullOrEmpty(ord.CFUPLeakindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFUPLeakindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFUPLeakindex) - 1, "UPLeak");
                }
                if (!String.IsNullOrEmpty(ord.CFDOWNLeakindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFDOWNLeakindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFDOWNLeakindex) - 1, "DOWNLeak");
                }
                if (!String.IsNullOrEmpty(ord.CFFWDLeakindex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFFWDLeakindex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFFWDLeakindex) - 1, "FWDLeak");
                }
                if (!String.IsNullOrEmpty(ord.CFQuiescentCurrntIndex))
                {
                    CFOrder.RemoveRange(Convert.ToInt32(ord.CFQuiescentCurrntIndex) - 1, 1);
                    CFOrder.Insert(Convert.ToInt32(ord.CFQuiescentCurrntIndex) - 1, "QC");
                }
                while (CFOrder.Remove(" ")) ;

                if (Form1.f1.plc.PLCIsRun)
                {
                    if (CH2Pump == true)
                    {
                        Form1.f1.plc.CH2Pump();
                    }
                    else
                    {
                        Form1.f1.plc.CH2Machine();
                    }
                }
                else
                {
                    //MessageBox.Show("PLC未通讯！");
                    Logger.Log(I18N.GetLangText(dicLang, "PLC未通讯"));
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("读取流程配置错误,请重新配置测试流程:" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "读取流程配置错误,请重新配置测试流程") + ":" + ex.Message);
            }
        }

        private void DayTime_Tick(object sender, EventArgs e)
        {
            string time = System.DateTime.Now.ToString("HH:mm:ss");
            Now.Text = time;
        }

        private void UDPOverTime_Tick(object sender, EventArgs e)
        {
            UDPOverTime.Stop();
            LeftCH1TCP.Text = "NG";
            LeftCH1TCP.ForeColor = Color.Red;

            LeftCH2TCP.Text = "NG";
            LeftCH2TCP.ForeColor = Color.Red;

            RightCH1TCP.Text = "NG";
            RightCH1TCP.ForeColor = Color.Red;

            RightCH2TCP.Text = "NG";
            RightCH2TCP.ForeColor = Color.Red;
            //MessageBox.Show("UDP广播无回复！");

            Logger.Log(I18N.GetLangText(dicLang, "UDP广播无回复"));
            wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "UDP广播无回复"));
        }

        private void CH1LinUP_Tick(object sender, EventArgs e)
        {
            if (CH1RTStep == "UP" || (CH1RTStep == "UPLeak" && CH1Pump))
            {
                //CH1lin.LinUP();
                if (string.IsNullOrEmpty(CH1RunName))
                {
                    CH1lin.LINUP(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.UPSignalName, linconfig.Schedule_tables);
                //    log.MES_Logmsg("CH1:liN发送FWDT同充指令");
                }

                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                {
                    CH1lin.LINUP(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADUPSignalName, linconfig.ADSchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                {
                    CH1lin.LINUP(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEUPSignalName, linconfig.BESchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                {
                    CH1lin.LINUP(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFUPSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH1RTStep == "DOWN" || (CH1RTStep == "DOWNLeak" && CH1Pump))
            {
                //CH1lin.LinDOWN();
                if (string.IsNullOrEmpty(CH1RunName))
                {
                    CH1lin.LINDOWN(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.DOWNSignalName, linconfig.Schedule_tables);
                  //  log.MES_Logmsg("CH1:liN发送Down指令");
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                {
                    CH1lin.LINDOWN(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADDOWNSignalName, linconfig.ADSchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                {
                    CH1lin.LINDOWN(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEDOWNSignalName, linconfig.BESchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                {
                    CH1lin.LINDOWN(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFDOWNSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH1RTStep == "FWD" || (CH1RTStep == "FWDLeak" && CH1Pump))
            {
                //CH1lin.LinFWD();
                if (string.IsNullOrEmpty(CH1RunName))
                {

                    CH1lin.LINFWD(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.FWDSignalName, linconfig.Schedule_tables);
                 //   log.MES_Logmsg("CH1:liN发送FWDT同充指令");
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                {
                    CH1lin.LINFWD(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADFWDSignalName, linconfig.ADSchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                {
                    CH1lin.LINFWD(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEFWDSignalName, linconfig.BESchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                {
                    CH1lin.LINFWD(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFFWDSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH1RTStep == "RWD" || (CH1RTStep == "RWDLeak" && CH1Pump))
            {
                //CH1lin.LinRWD();
                if (string.IsNullOrEmpty(CH1RunName))
                {
                    CH1lin.LINRWD(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.RWDSignalName, linconfig.Schedule_tables);
             //       log.MES_Logmsg("CH1:liN发送RWD泄气指令");
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                {
                    CH1lin.LINRWD(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADRWDSignalName, linconfig.ADSchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                {
                    CH1lin.LINRWD(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BERWDSignalName, linconfig.BESchedule_tables);
                }
                if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                {
                    CH1lin.LINRWD(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFRWDSignalName, linconfig.CFSchedule_tables);
                }
            }
        }

        private void CH2LinUP_Tick(object sender, EventArgs e)
        {
            if (CH2RTStep == "UP" || (CH2RTStep == "UPLeak" && CH2Pump))
            {
                //CH2lin.LinUP();
                if (string.IsNullOrEmpty(CH2RunName))
                {
                    CH2lin.LINUP(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.UPSignalName, linconfig.Schedule_tables);
                    log.MES_Logmsg("CH2:liN发送up上充指令");
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                {
                    CH2lin.LINUP(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADUPSignalName, linconfig.ADSchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                {
                    CH2lin.LINUP(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEUPSignalName, linconfig.BESchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                {
                    CH2lin.LINUP(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFUPSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH2RTStep == "DOWN" || (CH2RTStep == "DOWNLeak" && CH2Pump))
            {
                //CH2lin.LinDOWN();
                if (string.IsNullOrEmpty(CH2RunName))
                {
                    CH2lin.LINDOWN(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.DOWNSignalName, linconfig.Schedule_tables);
                //   log.MES_Logmsg("CH2:liN发送DOWN下充指令");
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                {
                    CH2lin.LINDOWN(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADDOWNSignalName, linconfig.ADSchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                {
                    CH2lin.LINDOWN(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEDOWNSignalName, linconfig.BESchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                {
                    CH2lin.LINDOWN(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFDOWNSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH2RTStep == "FWD" || (CH2RTStep == "FWDLeak" && CH2Pump))
            {
                //CH2lin.LinFWD();
                if (string.IsNullOrEmpty(CH2RunName))
                {
                 //   log.MES_Logmsg("CH2:liN发送FWD同充指令");
                    CH2lin.LINFWD(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.FWDSignalName, linconfig.Schedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                {
                    CH2lin.LINFWD(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADFWDSignalName, linconfig.ADSchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                {
                    CH2lin.LINFWD(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BEFWDSignalName, linconfig.BESchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                {
                    CH2lin.LINFWD(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFFWDSignalName, linconfig.CFSchedule_tables);
                }
            }
            if (CH2RTStep == "RWD" || (CH2RTStep == "RWDLeak" && CH2Pump))
            {
                //CH2lin.LinRWD();
                if (string.IsNullOrEmpty(CH2RunName))
                {
                    CH2lin.LINRWD(linconfig.PowerSignalName, linconfig.PowerSignalValue, linconfig.RWDSignalName, linconfig.Schedule_tables);
                    log.MES_Logmsg("CH2:liN发送RWD泄气指令");
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                {
                    CH2lin.LINRWD(linconfig.ADPowerSignalName, linconfig.ADPowerSignalValue, linconfig.ADRWDSignalName, linconfig.ADSchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                {
                    CH2lin.LINRWD(linconfig.BEPowerSignalName, linconfig.BEPowerSignalValue, linconfig.BERWDSignalName, linconfig.BESchedule_tables);
                }
                if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                {
                    CH2lin.LINRWD(linconfig.CFPowerSignalName, linconfig.CFPowerSignalValue, linconfig.CFRWDSignalName, linconfig.CFSchedule_tables);
                }
            }
            int i = 0;
            if (Convert.ToDouble(CH2RTADC.Text) == 0)
            {
                while (i++ < 10)
                {
                    //System.Threading.Thread.Sleep(50);
                    if (i > 5)
                        i++;
                }

            }
        }

        //读取注册表
        /// <summary>
        /// 切换参数及启动仪器,i为参数组号，1是流量，2是仪器气密，3是阀泵气密，CH为通道
        /// </summary>
        private void ReadParameters(int i, int CH)
        {
            try
            {
                System.Threading.Thread.Sleep(50);
                int step = i;
                //if (i == 4)
                if (i == 14)
                {
                    step = 2;
                }
                Model.CH_PARAMS ch_params = new Model.CH_PARAMS();
                ReadConfig con = new ReadConfig();
                ch_params = con.ReadParameters(CH, step);


                double full = Convert.ToDouble(ch_params.FullTime) * 10;
                double balan = Convert.ToDouble(ch_params.BalanTime) * 10;
                double testtime = Convert.ToDouble(ch_params.TestTime1) * 10;
                double exhaust = Convert.ToDouble(ch_params.ExhaustTime) * 10;
                double relieve = Convert.ToDouble(ch_params.RelieveDelay) * 10;
                double delay1 = Convert.ToDouble(ch_params.DelayTime1) * 10;
                double delay2 = Convert.ToDouble(ch_params.DelayTime2) * 10;

                string hex_full = Convert.ToInt32(full).ToString("x4");
                string hex_balan = Convert.ToInt32(balan).ToString("x4");
                string hex_testtime = Convert.ToInt32(testtime).ToString("x4");
                string hex_exhaust = Convert.ToInt32(exhaust).ToString("x4");
                string hex_relieve = Convert.ToInt32(relieve).ToString("x4");
                string hex_delay1 = Convert.ToInt32(delay1).ToString("x4");
                string hex_delay2 = Convert.ToInt32(delay2).ToString("x4");

                string fptop = ch_params.FPtoplimit;
                string fplow = ch_params.FPlowlimit;
                string bptop = ch_params.BalanPreMax;
                string bplow = ch_params.BalanPreMin;
                string leaktop = ch_params.Leaktoplimit;
                string leaklow = ch_params.Leaklowlimit;
                if (ch_params.CHKUnit)
                {
                    fptop = (Convert.ToDouble(ch_params.FPtoplimit) * 98).ToString();
                    fplow = (Convert.ToDouble(ch_params.FPlowlimit) * 98).ToString();
                    leaktop = (Convert.ToDouble(ch_params.Leaktoplimit) * 98).ToString();
                    leaklow = (Convert.ToDouble(ch_params.Leaklowlimit) * 98).ToString();
                }
                //bptop = (Convert.ToDouble(ch_params.BalanPreMax)).ToString();
                //bplow = (Convert.ToDouble(ch_params.BalanPreMin)).ToString();
                //bptop = ch_params.BalanPreMax;
                //bplow = ch_params.BalanPreMin;

                byte[] fpmax1 = BitConverter.GetBytes(Convert.ToSingle(fptop));//将字符串转换成字节数组
                string fpmax2 = BitConverter.ToString(fpmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string fptoplimit = fpmax2.Substring(4, 4) + fpmax2.Substring(0, 4);

                byte[] fpmin1 = BitConverter.GetBytes(Convert.ToSingle(fplow));//将字符串转换成字节数组
                string fpmin2 = BitConverter.ToString(fpmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string fplowlimit = fpmin2.Substring(4, 4) + fpmin2.Substring(0, 4);

                byte[] bpmax1 = BitConverter.GetBytes(Convert.ToSingle(ch_params.BalanPreMax));//将字符串转换成字节数组
                string bpmax2 = BitConverter.ToString(bpmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string balanpremax = bpmax2.Substring(4, 4) + bpmax2.Substring(0, 4);

                byte[] bpmin1 = BitConverter.GetBytes(Convert.ToSingle(ch_params.BalanPreMin));//将字符串转换成字节数组
                string bpmin2 = BitConverter.ToString(bpmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string balanpremin = bpmin2.Substring(4, 4) + bpmin2.Substring(0, 4);

                byte[] leakmax1 = BitConverter.GetBytes(Convert.ToSingle(leaktop));//将字符串转换成字节数组
                string leakmax2 = BitConverter.ToString(leakmax1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string leaktoplimit = leakmax2.Substring(4, 4) + leakmax2.Substring(0, 4);

                byte[] leakmin1 = BitConverter.GetBytes(Convert.ToSingle(leaklow));//将字符串转换成字节数组
                string leakmin2 = BitConverter.ToString(leakmin1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string leaklowlimit = leakmin2.Substring(4, 4) + leakmin2.Substring(0, 4);

                //上传等效容积
                byte[] evol1 = BitConverter.GetBytes(Convert.ToSingle(ch_params.Evolume));//将字符串转换成字节数组
                string evol2 = BitConverter.ToString(evol1.Reverse().ToArray()).Replace("-", "");//将字节数组转换为十六进制字符串
                string evolume = evol2.Substring(4, 4) + evol2.Substring(0, 4);

                int punit = ch_params.PUnit_index;
                int lunit = ch_params.LUnit_index;

                string hex_punit = Convert.ToInt32(punit).ToString("x4");
                string hex_lunit = Convert.ToInt32(lunit).ToString("x4");

                string sendtext = "10 03 EE 00 17 2E" + hex_full + hex_balan + hex_testtime + hex_exhaust
                    + hex_relieve + hex_delay1 + hex_delay2 + fptoplimit + fplowlimit + balanpremax + balanpremin
                    + leaktoplimit + leaklowlimit + evolume + hex_punit + hex_lunit;

                switch (CH)
                {
                    case 1:
                        //string seed1 = "01 05 0002 ff00";
                        //ch1client.btnSendData(seed1);
                        //Thread.Sleep(200);
                        string ch1sendstr = "01 " + sendtext;
                        ch1client.btnSendData(ch1sendstr);

                        ch1stage = 10;
                        chXstartflag[1] = 1;
                        CH1IsRun.Stop();
                        MachineStart.Interval = 400;
                        MachineStart.Start();
                        CH1ParamIndex.Text = i.ToString();
                        ch1_1params.CHKUnit = ch_params.CHKUnit;
                        if (step == 1)
                        {
                            CH1_1presstext.Text = I18N.GetLangText(dicLang, "输出压力");
                        }
                        //if (i == 4)
                        if (i == 14)
                        {
                            CH1RTStep = "";
                            ch1write = 1;
                        }
                        break;

                    case 2:
                        //string seed2 = "02 05 0002 ff00";
                        //ch2client.btnSendData(seed2);
                        //Thread.Sleep(200);
                        string ch2sendstr = "02 " + sendtext;
                        //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                        ch2client.btnSendData(ch2sendstr);
                        ch2stage = 10;
                        chXstartflag[2] = 1;
                        CH2IsRun.Stop();
                        MachineStart.Interval = 400;
                        MachineStart.Start();
                        CH2ParamIndex.Text = i.ToString();
                        ch1_2params.CHKUnit = ch_params.CHKUnit;
                        if (step == 1)
                        {
                            CH1_2presstext.Text = I18N.GetLangText(dicLang, "输出压力");
                        }
                        //if (i == 4)
                        if (i == 14)
                        {
                            CH1RTStep = "";
                            ch2write = 1;
                        }
                        break;

                    case 3:
                        string ch3sendstr = "03 " + sendtext;
                        //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                        ch3client.btnSendData(ch3sendstr);
                        ch3stage = 10;
                        chXstartflag[3] = 1;
                        CH3IsRun.Stop();
                        MachineStart.Interval = 200;
                        MachineStart.Start();
                        CH3ParamIndex.Text = i.ToString();
                        ch2_1params.CHKUnit = ch_params.CHKUnit;
                        if (step == 1)
                        {
                            CH2_1presstext.Text = I18N.GetLangText(dicLang, "输出压力");
                        }
                        //if (i == 4)
                        if (i == 14)
                        {
                            CH2RTStep = "";
                            ch3write = 1;
                        }
                        break;

                    case 4:
                        string ch4sendstr = "04 " + sendtext;
                        //Form1.f1.left_ch2tcp.ClientSendMsgAsync(ch2sendstr);
                        ch4client.btnSendData(ch4sendstr);
                        ch4stage = 10;
                        chXstartflag[4] = 1;
                        CH4IsRun.Stop();
                        MachineStart.Interval = 200;
                        MachineStart.Start();
                        CH4ParamIndex.Text = i.ToString();
                        ch2_2params.CHKUnit = ch_params.CHKUnit;
                        if (step == 1)
                        {
                            CH2_2presstext.Text = I18N.GetLangText(dicLang, "输出压力");
                        }
                        //if (i == 4)
                        if (i == 14)
                        {
                            CH2RTStep = "";
                            ch4write = 1;
                        }
                        break;
                }
                //regName.Close();
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "切换参数") + ":" + ex.Message);
                //MessageBox.Show("切换参数：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "切换参数") + ":" + ex.Message);
            }
        }

        /// <summary>
        /// 通过流量计读取流量
        /// </summary>
        /// <param name="CH"></param>
        private OperateResult<float> FlowSend(int CH)
        {
            /*        string str = "610D";
                    byte[] senddata = new byte[2];
                    int i;
                    for (i = 0; i < senddata.Length; i++)
                    {
                        senddata[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
                    }*/
            ushort[] registerBuffer = new ushort[2];
            OperateResult<float> result = new OperateResult<float>();
            switch (CH)
            {
                case 1:
                    //CH1FlowPort.Write(senddata, 0, senddata.Length);
                    // registerBuffer = masterCH1FlowPort.ReadHoldingRegisters(byte.Parse("01"), ushort.Parse("04"), ushort.Parse("02"));
                    busRtuClientCH1.Station = 2;
                    result = busRtuClientCH1.ReadFloat("02");
                    break;

                case 2:
                    //CH2FlowPort.Write(senddata, 0, senddata.Length);
                    //registerBuffer = masterCH2FlowPort.ReadHoldingRegisters(byte.Parse("01"), ushort.Parse("04"), ushort.Parse("02"));
                    //  result = busRtuClientCH2.ReadFloat("02");

                    busRtuClientCH1.Station = 1;
                    result = busRtuClientCH1.ReadFloat("02");

                    break;

                case 3:
                    //CH3FlowPort.Write(senddata, 0, senddata.Length);
                    // registerBuffer = masterCH3FlowPort.ReadHoldingRegisters(byte.Parse("01"), ushort.Parse("04"), ushort.Parse("02"));
                    //result = busRtuClientCH3.ReadFloat("02");
                    busRtuClientCH1.Station = 3;
                    result = busRtuClientCH1.ReadFloat("02");
                    break;

                case 4:
                    //CH4FlowPort.Write(senddata, 0, senddata.Length);
                    //registerBuffer = masterCH4FlowPort.ReadHoldingRegisters(byte.Parse("01"), ushort.Parse("04"), ushort.Parse("02"));
                    // result = busRtuClientCH4.ReadFloat("02");
                    busRtuClientCH1.Station = 4;
                    result = busRtuClientCH1.ReadFloat("02");
                    break;
            }
            //ushort[] newRegisterBuffer = new ushort[2] { registerBuffer[1], registerBuffer[0] };
            //float value = NModbusConvert.GetReal(newRegisterBuffer, 0);
            return result;
        }

        /// <summary>
        /// 接收流量计的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CH1FlowPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void CH2FlowPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void CH3FlowPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }

        private void CH4FlowPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

        }


        //流量变量
        public static double CH1Flow_read;
        public static double CH2Flow_read;
        public static double CH3Flow_read;
        public static double CH4Flow_read;

        //FLOW 读取
        public void ReadFlowTask()
        {
            try
            {
                Task.Run(() =>
                {
                    while (true)
                    {

                        var result1 = FlowSend(1);
                        if (result1.IsSuccess) CH1Flow_read = Math.Round(Convert.ToDouble(result1.Content), 2);
                        Thread.Sleep(300);
                        var result2 = FlowSend(2);
                        if (result2.IsSuccess) CH2Flow_read = Math.Round(Convert.ToDouble(result2.Content) / 1000, 2);
                        Thread.Sleep(300);
                        var result3 = FlowSend(3);
                        if (result3.IsSuccess) CH3Flow_read = Math.Round(Convert.ToDouble(result3.Content) / 1000, 2);
                        Thread.Sleep(300);
                        var result4 = FlowSend(4);
                        if (result4.IsSuccess) CH4Flow_read = Math.Round(Convert.ToDouble(result4.Content), 2);
                        Thread.Sleep(300);
                    }
                   
                    
                });
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }

        //public void ReadADCTask()
        //{
        //    try
        //    {
        //        Task.Run(() =>
        //        {
        //            while (true)
        //            {

        //                var result1 = FlowSend(1);
        //                if (result1.IsSuccess) CH1Flow_read = Math.Round(Convert.ToDouble(result1.Content), 2);
        //                Thread.Sleep(200);
        //                var result2 = FlowSend(2);
        //                if (result2.IsSuccess) CH2Flow_read = Math.Round(Convert.ToDouble(result2.Content) / 1000, 2);
        //                Thread.Sleep(200);
        //                var result3 = FlowSend(3);
        //                if (result3.IsSuccess) CH3Flow_read = Math.Round(Convert.ToDouble(result3.Content) / 1000, 2);
        //                Thread.Sleep(200);
        //                var result4 = FlowSend(4);
        //                if (result4.IsSuccess) CH4Flow_read = Math.Round(Convert.ToDouble(result4.Content), 2);
        //                Thread.Sleep(200);
        //            }


        //        });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}


        private void CH1ReadFlowT_Tick(object sender, EventArgs e)
        {
            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                try
                {
                    ////泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    //JudgeCH1ADC = true;

                    
                    double flow = 0.0D;
                      flow = CH1Flow_read;
                    //计算时间
                    ch1fullend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch1fullstart);
                    TimeSpan ts2 = new TimeSpan(ch1fullend);
                    TimeSpan ftime = ts2.Subtract(ts1).Duration();
                    double fulltime = ftime.TotalSeconds;
                    if (flow > CH1Q)
                    {
                        CH1Q = flow;
                        CH1_1flow.Text = CH1Q.ToString();
                        //7.26
                        down_upFlow = flow;
                    }
                    //如果此时步骤是下充，则由下充流量定时器控制停止
                    if ((fulltime > Flow.CH1OverTime) && CH1RTStep == "DOWN")
                    {
                        CH1ReadFlowT.Stop();
                        //将作为保持连接的定时器给停止
                        CH1IsRun.Stop();
                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        CH1_1flow.Text = CH1Q.ToString();
                        //流量测试完之后，需要读取压力
                        CH1ReadPress.Interval = 1000;
                        CH1ReadPress.Start();
                        ch1pressstart = System.DateTime.Now.Ticks;
                        Invoke((new System.Action(() =>
                        {
                            CH1_1flow.Text = flow.ToString();

                            if (flow < Flow.CH1_1FlowMin || flow > Flow.CH1_1FlowMax)
                            {
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), flow.ToString(), "plm", Flow.CH1_1FlowMax.ToString(), Flow.CH1_1FlowMin.ToString(), "NG");
                                plc.CH1DOWNFLOWNG();
                                FlowNG(1);
                            }
                        })));
                    }
                    if ((fulltime > Flow.CH1OverTime) && ((CH1RTStep == "UP")|| (CH1RTStep == "RWD")))
                    {
                        CH1ReadFlowT.Stop();
                        //将作为保持连接的定时器给停止
                        CH1IsRun.Stop();
                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        CH1_1flow.Text = CH1Q.ToString();
                        //流量测试完之后，需要读取压力
                        CH1ReadPress.Interval = 1000;
                        CH1ReadPress.Start();
                        ch1pressstart = System.DateTime.Now.Ticks;
                    }
                    if ((fulltime > elec.CH1FWDFlowTime) && (CH1RTStep == "FWD"))
                    {
                        CH1ReadFlowT.Stop();
                        //将作为保持连接的定时器给停止
                        CH1IsRun.Stop();
                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        
                        //流量测试完之后，需要读取压力
                        CH1ReadPress.Interval = 1000;
                        CH1ReadPress.Start();
                        ch1pressstart = System.DateTime.Now.Ticks;

                        Invoke((new System.Action(() =>
                        {
                            CH1_1flow.Text = flow.ToString();

                                              })));


       
                    }
                }
                catch (Exception ex)
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1流量计时") + ":" + ex.Message);
                    //CH1ReadFlowT.Stop();
                    //MessageBox.Show("CH1流量计时：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH1流量计时") + ":" + ex.Message);
                }
            //break;
            //}
            //CH1ReadFlowT.Stop();
        }

        private void CH2ReadFlowT_Tick(object sender, EventArgs e)
        {
            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                try
                {
                    ////泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    //JudgeCH1ADC = true;
                    //CH2flowtest = true;

                    double flow = 0.0D;
                    flow = CH2Flow_read;
;
                    //计算时间
                    ch2fullend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch2fullstart);
                    TimeSpan ts2 = new TimeSpan(ch2fullend);
                    TimeSpan ftime = ts2.Subtract(ts1).Duration();
                    double fulltime = ftime.TotalSeconds;
                    if (flow > CH2Q)
                    {
                        CH2Q = flow;
                        CH1_2flow.Text = CH2Q.ToString();
                    }
                    if ((fulltime > Flow.CH2OverTime) && (CH1RTStep == "DOWN"|| CH1RTStep == "RWD"))
                    {
                        CH2ReadFlowT.Stop();
                        CH2IsRun.Stop();
                        plc.CH2valveclose();
                        plc.CH1valveclose();
                        CH1_2flow.Text = CH2Q.ToString();
                        //流量测试完之后，需要读取压力
                        CH2ReadPress.Interval = 1000;
                        CH2ReadPress.Start();
                        ch2pressstart = System.DateTime.Now.Ticks;
                    }
                    if ((fulltime > Flow.CH2OverTime) && CH1RTStep == "UP")
                    {
                        CH2ReadFlowT.Stop();
                        CH2IsRun.Stop();
                        plc.CH2valveclose();
                        plc.CH1valveclose();
                        CH1_2flow.Text = CH2Q.ToString();
                        //流量测试完之后，需要读取压力
                        CH2ReadPress.Interval = 1000;
                        CH2ReadPress.Start();
                        ch2pressstart = System.DateTime.Now.Ticks;
                        Invoke((new System.Action(() =>
                        {
                           
                            up_downFlow = flow;
                            if (flow < Flow.CH1_2FlowMin || flow > Flow.CH1_2FlowMax)
                            {
                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), flow.ToString(), "lpm", Flow.CH1_2FlowMax.ToString(), Flow.CH1_2FlowMin.ToString(), "NG");
                                plc.CH1UPFLOWNG();
                                FlowNG(1);
                            }
                        })));
                    }
                    if ((fulltime > elec.CH1FWDFlowTime) && CH1RTStep == "FWD")
                    {
                        CH2ReadFlowT.Stop();
                        //CH2IsRun.Stop();
                        plc.CH2valveclose();
                        plc.CH1valveclose();
                        CH1_2flow.Text = CH2Q.ToString();
                        //流量测试完之后，需要读取压力
                        CH2ReadPress.Interval = 1000;
                        CH2ReadPress.Start();
                        ch2pressstart = System.DateTime.Now.Ticks;
                    }
                }
                catch (Exception ex)
                {
                    // CH2ReadFlowT.Stop();
                    //MessageBox.Show("CH2流量计时：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH2流量计时") + ":" + ex.Message);
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2流量计时") + ":" + ex.Message);
                }
        }

        //7.26 定义流量变量
        public static double up_downFlow;
        public static double down_UPPre;
        public static double down_upFlow;
        public static double up_downFlow2;
        public static double down_UPPre2;
        public static double down_upFlow2;
        private void CH3ReadFlowT_Tick(object sender, EventArgs e)
        {
            try
            {
                ////泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后

                
                double flow = 0.0D;
                flow = CH3Flow_read;
                //计算时间
                ch3fullend = System.DateTime.Now.Ticks;
                TimeSpan ts1 = new TimeSpan(ch3fullstart);
                TimeSpan ts2 = new TimeSpan(ch3fullend);
                TimeSpan ftime = ts2.Subtract(ts1).Duration();
                double fulltime = ftime.TotalSeconds;
                //如果此时步骤是下充，则由下充流量定时器控制停止
                if (flow > CH3Q)
                {
                    CH3Q = flow;
                }
                double CH3flowtime;
                switch (CH2RTStep)
                {
                    case "DOWN":
                        CH3flowtime = Flow.CH3OverTime;
                        break;
                    case "UP":
                        CH3flowtime = Flow.CH3OverTime;
                        break;
                    case "FWD":
                        CH3flowtime = elec.CH2FWDFlowTime;
                        break;
                    case "RWD":
                        CH3flowtime = 0;
                        break;
                    default: CH3flowtime = 0; break;
                }
                if (fulltime> CH3flowtime) 
                {
                    CH3ReadFlowT.Stop();
                    //将作为保持连接的定时器给停止
                    CH3IsRun.Stop();//ERIC
                    plc.CH3valveclose();
                    plc.CH4valveclose();
                    CH2_1flow.Text = CH3Q.ToString();
                    //流量测试完之后，需要读取压力
                    CH3ReadPress.Interval = 1000;
                    CH3ReadPress.Start();
                    ch3pressstart = System.DateTime.Now.Ticks;
                    CH2_1flow.Text = flow.ToString();
                    down_upFlow2 = flow;
                }
               
            }
            catch (Exception ex)
            {
                //CH3ReadFlowT.Stop();
                //MessageBox.Show("CH3流量计时：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH3流量计时") + ":" + ex.Message);
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH3流量计时") + ":" + ex.Message);
            }
        }

        private void CH4ReadFlowT_Tick(object sender, EventArgs e)
        {
            try
            {
                ////泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                //JudgeCH1ADC = true;
                //CH2flowtest = true;

              
                double flow = 0.0D;
                flow = CH4Flow_read;
                //计算时间
                ch4fullend = System.DateTime.Now.Ticks;
                TimeSpan ts1 = new TimeSpan(ch4fullstart);
                TimeSpan ts2 = new TimeSpan(ch4fullend);
                TimeSpan ftime = ts2.Subtract(ts1).Duration();
                double fulltime = ftime.TotalSeconds;
                if (flow > CH4Q)
                {
                    CH4Q = flow;
                }
                double CH4flowtime;
                switch (CH2RTStep)
                {
                    case "DOWN":
                        CH4flowtime = Flow.CH3OverTime;
                        break;
                    case "UP":
                        CH4flowtime = Flow.CH3OverTime;
                        break;
                    case "FWD":
                        CH4flowtime = elec.CH2FWDFlowTime;
                        break;
                    case "RWD":
                        CH4flowtime = 0;
                        break;
                    default: CH4flowtime = 0; break;
                }
                if (fulltime > CH4flowtime)
                {
                    CH4ReadFlowT.Stop();
                    CH4IsRun.Stop();
                    plc.CH4valveclose();
                    plc.CH3valveclose();
                    CH2_2flow.Text = CH4Q.ToString();
                    //流量测试完之后，需要读取压力
                    CH4ReadPress.Interval = 1000;
                    CH4ReadPress.Start();
                    ch4pressstart = System.DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH4流量计时") + ":" + ex.Message);
                // CH4ReadFlowT.Stop();
                //MessageBox.Show("CH4流量计时：" + ex.Message);
                Logger.Log(I18N.GetLangText(dicLang, "CH4流量计时") + ":" + ex.Message);
            }
        }

        private int CHpreflag = 0;
        private int CHpreflag2 = 0;

        private void CH1ReadPress_Tick(object sender, EventArgs e)
        {

            ReadConfig con = new ReadConfig();
            Model.Flow flow;
            flow = con.ReadFlow();

            //ERIC
            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                try
                {
                    //泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    JudgeCH1ADC = true;
                    CH1IsRun.Stop();
                    CH1ReadPress.Interval = 200;
                    ch1client.btnSendData("01 03 04 03 00 17 ");
                    //leftclient.btnSendData("01 03 04 18 00 02 ");
                    ch1stage = 7;
                    //计算时间
                    ch1pressend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch1pressstart);
                    TimeSpan ts2 = new TimeSpan(ch1pressend);
                    TimeSpan ptime = ts2.Subtract(ts1).Duration();
                    double presstime = ptime.TotalSeconds;
                    double pressovertime;
                    if (CH1RTStep == "RWD")
                    {
                        pressovertime = Flow.CH1RWDOverTime;
                        //plc.CH1RWDStart();
                    }
                    else
                    {
                        if (CH1RTStep == "FWD")
                        {
                            pressovertime=elec.CH1FWDFlowTime;
                        }
                        else
                        pressovertime = Flow.CH1Press_OverTime;
                    }
                    if (presstime > pressovertime)
                    {
                        CH1ReadPress.Stop();
                        CH1_1FullPress.Text = CH1PressMax.ToString();
                        CH1flowtest = false;
                        CHpreflag++;
                        if (CHpreflag >= 2)
                        {
                            CH1ReadPress.Stop();
                            //两台仪器都没有在测试，则停止lin通讯
                            CH1LinUP.Stop();
                            CH1ReadFlowT.Stop();
                            plc.CH1PLCValveBreak();

                            //流量测试完之后，需要把测试仪复位，避免后续步骤启动失败
                            {
                                ch1client.btnSendData("01 05 00 01 FF 00");
                                ch2client.btnSendData("02 05 00 01 FF 00");
                                Thread.Sleep(50);
                                ch1client.btnSendData("01 05 00 01 FF 00");
                                ch2client.btnSendData("02 05 00 01 FF 00");
                            }

                            ch1stage = 10;
                            CHXProBarFlag[1] = 0;
                            CH1IsRun.Interval = 500;
                            CH1IsRun.Start();
                            ch1_1step = 1;
                            ch1write = 1;
                            ch2stage = 10;
                            CHXProBarFlag[2] = 0;
                            CH2IsRun.Interval = 500;
                            CH2IsRun.Start();
                            ch1_2step = 1;
                            ch2write = 1;

                            if (plc.CH1LIN)
                            {
                                plc.CH1LinFinish();
                            }

                            if (!CH1ReadElecResult())
                            {
                                return;
                            }
                            CH1VDCresult = "OK";
                            CH1ADCresult = "OK";
                            Invoke((new System.Action(() =>
                            {
                                if (CH1RTStep == "UP")
                                {
                                    plc.CH1UPPreOK();
                                    plc.CH1UPADCOK();
                                    plc.CH1UPVDCOK();
                                    plc.CH1uAOK();//增加一个BUG
                                    CH1_2FullPress.Text = CH2PressMax.ToString();
                                    CH1TestResult.UP_ADCMAX = CH1ADCMax;
                                    CH1TestResult.UP_VDCMAX = CH1VDCMax;
                                    CH1TestResult.UP_Flow = CH1Q;
                                    CH1TestResult.UP_Pre = CH1PressMax;
                                    CH1TestResult.UP_Prezuo = CH2PressMax;

                                    //CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, "-", "-", "OK");
                                    //CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH1_2FullPress.Text.ToString(), CH2PressureUnit.Text, Flow.CH1_2PreMax.ToString(), Flow.CH1_2PreMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), CH1PressMax.ToString(), PressureUnit.Text, "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH2PressMax.ToString(), CH2PressureUnit.Text, Flow.CH1_2PreMax.ToString(), Flow.CH1_2PreMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH1ADCMax.ToString(), "A", elec.CH1UPADCMax.ToString(), elec.CH1UPADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH1VDCMax.ToString(), "V", elec.CH1UPVDCMax.ToString(), elec.CH1UPVDCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), CH1Q.ToString(), "lpm", "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), CH2Q.ToString(), "lpm", Flow.CH1_2FlowMax.ToString(), Flow.CH1_2FlowMin.ToString(), "OK");


                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"))
                                        {
                                            CH1lastelec = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_elec = CH1ADCMax / CH1lastelec;
                                            CH1cont_elec += Flow.CH1Cont_Elec_Compen;
                                            CH1cont_elec = Math.Round(CH1cont_elec, 2);
                                            CH1TestResult.ElecRatio = CH1cont_elec;
                                            if (CH1cont_elec < Flow.CH1Cont_ElecMin || CH1cont_elec > Flow.CH1Cont_ElecMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "OK");
                                            if (CH1lastpress == 0)
                                            {
                                                CH1lastpress = 0.1;
                                            }
                                            break;
                                        }
                                    }

                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"))
                                        {
                                            CH1lastpress = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_press = CH1PressMax / CH1lastpress;
                                            CH1cont_press += Flow.CH1Cont_Pre_Compen;
                                            CH1cont_press = Math.Round(CH1cont_press, 2);
                                            CH1TestResult.PressRatio = CH1cont_press;
                                            if (CH1cont_press < Flow.CH1Cont_PressMin || CH1cont_press > Flow.CH1Cont_PressMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "OK");
                                            plc.CH1RatioOK();
                                            break;
                                        }
                                    }

                                    plc.CH1UPFlowEnd();
                                }
                                if (CH1RTStep == "FWD")
                                {
                                    plc.CH1FWDADCOK();
                                    plc.CH1FWDVDCOK();
                                    plc.CH1FWDFlowEnd();
                                    plc.CH1uAOK();//增加一个BUG
                                    CH1TestResult.FWD_ADCMAX = CH1ADCMax;
                                    CH1TestResult.FWD_VDCMAX = CH1VDCMax;
                                    CH1TestResult.FWD_Flow1 = Convert.ToDouble(CH1_1flow.Text);
                                    CH1TestResult.FWD_Flow2 = Convert.ToDouble(CH1_2flow.Text);
                                    CH1TestResult.FWD_Pre1 = Convert.ToDouble(CH1_1FullPress.Text);
                                    CH1TestResult.FWD_Pre2 = Convert.ToDouble(CH1_2FullPress.Text);
                                    CH1TestResult.FWD_FlowSumzuo = CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2;
                                    /////新加240801
                                    ///
                                    if (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2 > elec.TotalFlowMax|| CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2 < elec.TotalFlowMin)
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");

                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "OK");
                           }

                                    double preEROR = Math.Abs(CH1TestResult.FWD_Pre1 - CH1TestResult.FWD_Pre2);

                        
                                    if (preEROR > Convert.ToDouble(elec.TotalPreMax))
                                    {
                                        CH1TestResult.FWD_PreSumzuo = preEROR;
                                        //     CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), preEROR.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "NG");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH1TestResult.FWD_PreSumzuo.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    { 

                                        CH1TestResult.FWD_PreSumzuo = preEROR;
                                        //   CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), preEROR.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "OK");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH1TestResult.FWD_PreSumzuo.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "OK");
                                    }

                                    /////////240801
                                    //1.444   ch_params.FPlowlimit, ch_params.FPtoplimit flow.CH1_1FlowMax.ToString(), flow.CH1_1FlowMin.ToString()
                                    //6.18
                                    if (CH1Q > elec.CH1FwdFlowMax || CH1Q < elec.CH1FwdFlowMin)
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH1Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH1Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "OK");
                                    }
                                    if (CH2Q > elec.CH1FwdFlowMax || CH2Q < elec.CH1FwdFlowMin)
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "OK");
                                    }
                                    if ((Convert.ToDouble(CH1_1FullPress.Text) > elec.CH1FwdPreMax) || (Convert.ToDouble(CH1_1FullPress.Text) < elec.CH1FwdPreMin))
                                    {
                                        // log.MES_Logmsg(DateTime.Now.ToString() + "进入NG判断");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "OK");
                                        //  log.MES_Logmsg(DateTime.Now.ToString() + "进入OK判断");
                                    }
                                    if ((Convert.ToDouble(CH1_2FullPress.Text) > elec.CH1FwdPreMax) || (Convert.ToDouble(CH1_2FullPress.Text) < elec.CH1FwdPreMin))
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH1_2FullPress.Text.ToString(), CH2PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        //  log.MES_Logmsg(DateTime.Now.ToString() + "进入OK判断");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH1_2FullPress.Text.ToString(), CH2PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "OK");
                                    }


                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH1ADCMax.ToString(), "A", elec.CH1FWDADCMax.ToString(), elec.CH1FWDADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH1VDCMax.ToString(), "V", elec.CH1FWDVDCMax.ToString(), elec.CH1FWDVDCMin.ToString(), "OK");

                                }

                                if (CH1RTStep == "RWD")
                                {
                                    if (Flow.CH1RWDOverTime == 0)
                                    {
                                        CH1PressMax = CH1TestResult.FWD_Pre1;
                                        CH2PressMax = CH1TestResult.FWD_Pre2;
                                    }
                                    CH1TestResult.RWD_Pre1 = Convert.ToDouble(CH1_1FullPress.Text);
                                    CH1TestResult.RWD_Pre2 = CH2PressMax;
                                    if (Convert.ToDouble(CH1_1FullPress.Text) > Flow.CH1RWDPressMax || Convert.ToDouble(CH1_1FullPress.Text) < Flow.CH1RWDPressMin || CH2PressMax > Flow.CH1RWDPressMax || CH2PressMax < Flow.CH1RWDPressMin)
                                    {
                                        if (Convert.ToDouble(CH1_1FullPress.Text) > Flow.CH1RWDPressMax || Convert.ToDouble(CH1_1FullPress.Text) < Flow.CH1RWDPressMin)
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "NG");

                                            //6.18NG
                                            FlowNG(1);
                                        }
                                        else
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        }
                                        if (CH2PressMax > Flow.CH1RWDPressMax || CH2PressMax < Flow.CH1RWDPressMin)
                                        {

                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "NG");
                                            //6.18NG
                                            FlowNG(1);
                                        }
                                        else
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        }
                                        plc.CH1RWDPressNG();
                                        plc.CH1RWDFlowEnd();
                                        FlowNG(1);
                                        //6-28

                                        return;
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        plc.CH1RWDPressOK();
                                        plc.CH1RWDADCOK();
                                        plc.CH1RWDVDCOK();
                                        plc.CH1RWDFlowEnd();
                                        plc.CH1uAOK();//增加一个BUG
                                        CH1TestResult.RWD_ADCMAX = CH1ADCMax;
                                        CH1TestResult.RWD_VDCMAX = CH1VDCMax;
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH1ADCMax.ToString(), "A", elec.CH1RWDADCMax.ToString(), elec.CH1RWDADCMin.ToString(), "OK");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH1VDCMax.ToString(), "V", elec.CH1RWDVDCMax.ToString(), elec.CH1RWDVDCMin.ToString(), "OK");
                                    }
                                }
                                if (CH1RTStep == "DOWN")
                                {
                                    plc.CH1DOWNPreOK();
                                    CH1_1FullPress.Text = CH1PressMax.ToString();
                                    CH1ReadPress.Stop();
                                    CH1TestResult.DOWN_Pre = CH2PressMax;
                                    CH1TestResult.DOWN_ADCMAX = CH1ADCMax;
                                    CH1TestResult.DOWN_VDCMAX = CH1VDCMax;
                                    CH1TestResult.DOWN_Flow = CH2Q;
                                    CH1TestResult.DOWN_Flowzuo = CH1Q;


                                    plc.CH1DOWNADCOK();
                                    plc.CH1DOWNVDCOK();
                                    plc.CH1PLCValveBreak();
                                    plc.CH1uAOK();//增加一个BUG
                                                  //7.26下充
                                    down_UPPre = CH1PressMax;
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, Flow.CH1_1PreMax.ToString(), Flow.CH1_1PreMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), CH2PressMax.ToString(), CH2PressureUnit.Text, "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH1ADCMax.ToString(), "A", elec.CH1DOWNADCMax.ToString(), elec.CH1DOWNADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH1VDCMax.ToString(), "V", elec.CH1DOWNVDCMax.ToString(), elec.CH1DOWNVDCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), CH2Q.ToString(), "lpm", "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), CH1Q.ToString(), "lpm", Flow.CH1_1FlowMax.ToString(), Flow.CH1_1FlowMin.ToString(), "OK");


                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"))
                                        {
                                            CH1lastelec = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_elec = CH1ADCMax / CH1lastelec;
                                            CH1cont_elec += Flow.CH1Cont_Elec_Compen;
                                            CH1cont_elec = Math.Round(CH1cont_elec, 2);
                                            CH1TestResult.ElecRatio = CH1cont_elec;
                                            if (CH1cont_elec < Flow.CH1Cont_ElecMin || CH1cont_elec > Flow.CH1Cont_ElecMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "OK");
                                            if (CH1lastpress == 0)
                                            {
                                                CH1lastpress = 0.1;
                                            }
                                            break;
                                        }
                                    }
                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"))
                                        {
                                            CH1lastpress = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_press = CH1lastpress / CH2PressMax;
                                            CH1cont_press += Flow.CH1Cont_Pre_Compen;
                                            CH1cont_press = Math.Round(CH1cont_press, 2);
                                            CH1TestResult.PressRatio = CH1cont_press;
                                            if (CH1cont_press < Flow.CH1Cont_PressMin || CH1cont_press > Flow.CH1Cont_PressMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "OK");
                                            plc.CH1RatioOK();
                                            break;
                                        }
                                    }

                                    plc.CH1DOWNFlowEnd();
                                }
                            })));
   
                            if (CH1Tlight.Text != "NG")
                                if (CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD" || CH1RTStep == "UP")
                                {
                                    CH1Step += 1;
                                    CH1Method(CH1Step);
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1-1读取输出压力") + ":" + ex.Message);
                    //MessageBox.Show("CH1-1读取输出压力：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH1-1读取输出压力") + ":" + ex.Message);
                    Logger.Log(ex.StackTrace);
                }


        }

        private void CH2ReadPress_Tick(object sender, EventArgs e)
        {
            ReadConfig con = new ReadConfig();
            Model.Flow flow;
            flow = con.ReadFlow();

            //ERIC
            if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD")
                try
                {
                    //泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    JudgeCH1ADC = true;
                    CH2IsRun.Stop();
                    CH2ReadPress.Interval = 200;
                    ch2client.btnSendData("02 03 04 03 00 17 ");
                    //leftclient.btnSendData("02 03 04 21 00 02 ");
                    ch2stage = 7;
                    //计算时间
                    ch2pressend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch2pressstart);
                    TimeSpan ts2 = new TimeSpan(ch2pressend);
                    TimeSpan ptime = ts2.Subtract(ts1).Duration();
                    double presstime = ptime.TotalSeconds;
                    double pressovertime;
                    if (CH1RTStep == "RWD")
                    {
                        pressovertime = Flow.CH1RWDOverTime;
                        //plc.CH1RWDStart();
                    }
                    else
                    {
                        if (CH1RTStep == "FWD")
                            pressovertime = elec.CH1FWDFlowTime;
                        else
                             pressovertime = Flow.CH2Press_OverTime;
                    }
                    if (presstime > pressovertime)
                    {
                        CH2ReadPress.Stop();
                        CH1_2FullPress.Text = CH2PressMax.ToString();
                        CH2flowtest = false;
                        CHpreflag++;
                        if (CHpreflag >= 2)
                        {
                            CH1ReadPress.Stop();
                            //两台仪器都没有在测试，则停止lin通讯
                            CH1LinUP.Stop();
                            CH1ReadFlowT.Stop();
                            plc.CH1PLCValveBreak();

                            //流量测试完之后，需要把测试仪复位，避免后续步骤启动失败
                            {
                                ch1client.btnSendData("01 05 00 01 FF 00");
                                ch2client.btnSendData("02 05 00 01 FF 00");
                                Thread.Sleep(50);
                                ch1client.btnSendData("01 05 00 01 FF 00");
                                ch2client.btnSendData("02 05 00 01 FF 00");
                            }

                            ch1stage = 10;
                            CHXProBarFlag[1] = 0;
                            CH1IsRun.Interval = 500;
                            CH1IsRun.Start();
                            ch1_1step = 1;
                            ch1write = 1;
                            ch2stage = 10;
                            CHXProBarFlag[2] = 0;
                            CH2IsRun.Interval = 500;
                            CH2IsRun.Start();
                            ch1_2step = 1;
                            ch2write = 1;

                            if (plc.CH1LIN)
                            {
                                plc.CH1LinFinish();
                            }

                            if (!CH1ReadElecResult())
                            {
                                return;
                            }
                            CH1VDCresult = "OK";
                            CH1ADCresult = "OK";
                            Invoke((new System.Action(() =>
                            { 
                                if (CH1RTStep == "UP")
                                {
                                    plc.CH1UPPreOK();
                                    plc.CH1UPADCOK();
                                    plc.CH1UPVDCOK();
                                    plc.CH1uAOK();//增加一个BUG
                                    CH1_2FullPress.Text = CH2PressMax.ToString();
                                    CH1TestResult.UP_ADCMAX = CH1ADCMax;
                                    CH1TestResult.UP_VDCMAX = CH1VDCMax;
                                    CH1TestResult.UP_Flow = CH1Q;
                                    CH1TestResult.UP_Pre = CH1PressMax;
                                    CH1TestResult.UP_Prezuo = CH2PressMax;
                                    CH1TestResult.UP_Prezuo =double.Parse( CH1_2FullPress.Text);
                                    CH1TestResult.UP_Flowzuo = CH2Q;


                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), CH1PressMax.ToString(), PressureUnit.Text, "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH2PressMax.ToString(), CH2PressureUnit.Text, Flow.CH1_2PreMax.ToString(), Flow.CH1_2PreMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH1ADCMax.ToString(), "A", elec.CH1UPADCMax.ToString(), elec.CH1UPADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH1VDCMax.ToString(), "V", elec.CH1UPVDCMax.ToString(), elec.CH1UPVDCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), CH1Q.ToString(), "lpm", "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), CH2Q.ToString(), "lpm", Flow.CH1_2FlowMax.ToString(), Flow.CH1_2FlowMin.ToString(), "OK");


                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"))
                                        {
                                            CH1lastelec = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_elec = CH1ADCMax / CH1lastelec;
                                            CH1cont_elec += Flow.CH1Cont_Elec_Compen;
                                            CH1cont_elec = Math.Round(CH1cont_elec, 2);
                                            CH1TestResult.ElecRatio = CH1cont_elec;
                                            if (CH1cont_elec < Flow.CH1Cont_ElecMin || CH1cont_elec > Flow.CH1Cont_ElecMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "OK");
                                            if (CH1lastpress == 0)
                                            {
                                                CH1lastpress = 0.1;
                                            }
                                            break;
                                        }
                                    }

                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"))
                                        {
                                            CH1lastpress = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_press = CH1PressMax / CH1lastpress;
                                            CH1cont_press += Flow.CH1Cont_Pre_Compen;
                                            CH1cont_press = Math.Round(CH1cont_press, 2);
                                            CH1TestResult.PressRatio = CH1cont_press;
                                            if (CH1cont_press < Flow.CH1Cont_PressMin || CH1cont_press > Flow.CH1Cont_PressMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "OK");
                                            plc.CH1RatioOK();
                                            break;
                                        }
                                    }

                                    plc.CH1UPFlowEnd();
                                }
                                if (CH1RTStep == "FWD")
                                {
                                    plc.CH1FWDADCOK();
                                    plc.CH1FWDVDCOK();
                                    plc.CH1FWDFlowEnd();
                                    plc.CH1uAOK();//增加一个BUG
                                    CH1TestResult.FWD_ADCMAX = CH1ADCMax;
                                    CH1TestResult.FWD_VDCMAX = CH1VDCMax;
                                    CH1TestResult.FWD_Flow1 = Convert.ToDouble(CH1_1flow.Text);
                                    CH1TestResult.FWD_Flow2 = Convert.ToDouble(CH1_2flow.Text); ;
                                    CH1TestResult.FWD_Pre1 = Convert.ToDouble(CH1_1FullPress.Text);
                                    CH1TestResult.FWD_Pre2 = Convert.ToDouble(CH1_2FullPress.Text);
                                    CH1TestResult.FWD_FlowSumzuo = CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2;
                                    /////新加240801
                                    ///
                                    if (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2 > elec.TotalFlowMax || CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2 < elec.TotalFlowMin)
                                    {
                                     
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");

                                        FlowNG(1);
                                    }
                                    else
                                    {

                                        
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH1TestResult.FWD_Flow1 + CH1TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "OK");

                                    }
                                    double preEROR = Math.Abs(Convert.ToDouble(CH1_2FullPress.Text) - Convert.ToDouble(CH1_1FullPress.Text));


                                    if (preEROR > Convert.ToDouble(elec.TotalPreMax))
                                    {
                                        CH1TestResult.FWD_PreSumzuo = preEROR;
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH1TestResult.FWD_PreSumzuo.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "NG");
                                        FlowNG(1);
                                    }
                                    else {
                                        CH1TestResult.FWD_PreSumzuo = preEROR;
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH1TestResult.FWD_PreSumzuo.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "OK");
                                    }
                                    /////////240801
                                    //1.444   ch_params.FPlowlimit, ch_params.FPtoplimit flow.CH1_1FlowMax.ToString(), flow.CH1_1FlowMin.ToString()
                                    //6.18
                                    if (CH1Q > elec.CH1FwdFlowMax || CH1Q < elec.CH1FwdFlowMin)
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH1Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH1Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "OK");
                                    }
                                    if (CH2Q > elec.CH1FwdFlowMax || CH2Q < elec.CH1FwdFlowMin)
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2Q.ToString(), "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "OK");
                                    }
                                    if ((Convert.ToDouble(CH1_1FullPress.Text) > elec.CH1FwdPreMax) || (Convert.ToDouble(CH1_1FullPress.Text) < elec.CH1FwdPreMin))
                                    {
                                        // log.MES_Logmsg(DateTime.Now.ToString() + "进入NG判断");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "OK");
                                        //  log.MES_Logmsg(DateTime.Now.ToString() + "进入OK判断");
                                    }
                                    if ((Convert.ToDouble(CH1_2FullPress.Text) > elec.CH1FwdPreMax) || (Convert.ToDouble(CH1_2FullPress.Text) < elec.CH1FwdPreMin))
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH1_2FullPress.Text.ToString(), CH2PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "NG");
                                        FlowNG(1);
                                    }
                                    else
                                    {
                                        //  log.MES_Logmsg(DateTime.Now.ToString() + "进入OK判断");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH1_2FullPress.Text.ToString(), CH2PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "OK");
                                    }


                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH1ADCMax.ToString(), "A", elec.CH1FWDADCMax.ToString(), elec.CH1FWDADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH1VDCMax.ToString(), "V", elec.CH1FWDVDCMax.ToString(), elec.CH1FWDVDCMin.ToString(), "OK");

                                }

                                if (CH1RTStep == "RWD")
                                {
                                    if (Flow.CH1RWDOverTime == 0)
                                    {
                                        CH1PressMax = CH1TestResult.FWD_Pre1;
                                        CH2PressMax = CH1TestResult.FWD_Pre2;
                                    }
                                    CH1TestResult.RWD_Pre1 = Convert.ToDouble(CH1_1FullPress.Text);
                                    CH1TestResult.RWD_Pre2 = CH2PressMax;
                                    if (Convert.ToDouble(CH1_1FullPress.Text) > Flow.CH1RWDPressMax || Convert.ToDouble(CH1_1FullPress.Text) < Flow.CH1RWDPressMin || CH2PressMax > Flow.CH1RWDPressMax || CH2PressMax < Flow.CH1RWDPressMin)
                                    {
                                        if (Convert.ToDouble(CH1_1FullPress.Text) > Flow.CH1RWDPressMax || Convert.ToDouble(CH1_1FullPress.Text) < Flow.CH1RWDPressMin)
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "NG");

                                            //6.18NG
                                            FlowNG(1);
                                        }
                                        else
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        }
                                        if (CH2PressMax > Flow.CH1RWDPressMax || CH2PressMax < Flow.CH1RWDPressMin)
                                        {

                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "NG");
                                            //6.18NG
                                            FlowNG(1);
                                        }
                                        else
                                        {
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        }
                                        plc.CH1RWDPressNG();
                                        plc.CH1RWDFlowEnd();
                                        FlowNG(1);
                                        //6-28

                                        return;
                                    }
                                    else
                                    {
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), Convert.ToDouble(CH1_1FullPress.Text).ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH2PressMax.ToString(), PressureUnit.Text, Flow.CH1RWDPressMax.ToString(), Flow.CH1RWDPressMin.ToString(), "OK");
                                        plc.CH1RWDPressOK();
                                        plc.CH1RWDADCOK();
                                        plc.CH1RWDVDCOK();
                                        plc.CH1RWDFlowEnd();
                                        plc.CH1uAOK();//增加一个BUG
                                        CH1TestResult.RWD_ADCMAX = CH1ADCMax;
                                        CH1TestResult.RWD_VDCMAX = CH1VDCMax;
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH1ADCMax.ToString(), "A", elec.CH1RWDADCMax.ToString(), elec.CH1RWDADCMin.ToString(), "OK");
                                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH1VDCMax.ToString(), "V", elec.CH1RWDVDCMax.ToString(), elec.CH1RWDVDCMin.ToString(), "OK");
                                    }
                                }
                                if (CH1RTStep == "DOWN")
                                {
                                    plc.CH1DOWNPreOK();
                                    CH1_1FullPress.Text = CH1PressMax.ToString();
                                    CH1ReadPress.Stop();
                                    CH1TestResult.DOWN_Pre = CH2PressMax;
                                    CH1TestResult.DOWN_Prezuo = double.Parse(CH1_1FullPress.Text);

                                    CH1TestResult.DOWN_ADCMAX = CH1ADCMax;
                                    CH1TestResult.DOWN_VDCMAX = CH1VDCMax;
                                    CH1TestResult.DOWN_Flow = CH2Q;
                                    plc.CH1DOWNADCOK();
                                    plc.CH1DOWNVDCOK();
                                    plc.CH1PLCValveBreak();
                                    plc.CH1uAOK();//增加一个BUG
                                                  //7.26下充
                                    down_UPPre = CH1PressMax;
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), CH1_1FullPress.Text.ToString(), PressureUnit.Text, Flow.CH1_1PreMax.ToString(), Flow.CH1_1PreMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), CH2PressMax.ToString(), CH2PressureUnit.Text, "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH1ADCMax.ToString(), "A", elec.CH1DOWNADCMax.ToString(), elec.CH1DOWNADCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH1VDCMax.ToString(), "V", elec.CH1DOWNVDCMax.ToString(), elec.CH1DOWNVDCMin.ToString(), "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), CH2Q.ToString(), "lpm", "-", "-", "OK");
                                    CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), CH1Q.ToString(), "lpm", Flow.CH1_1FlowMax.ToString(), Flow.CH1_1FlowMin.ToString(), "OK");


                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"))
                                        {
                                            CH1lastelec = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_elec = CH1ADCMax / CH1lastelec;
                                            CH1cont_elec += Flow.CH1Cont_Elec_Compen;
                                            CH1cont_elec = Math.Round(CH1cont_elec, 2);
                                            CH1TestResult.ElecRatio = CH1cont_elec;
                                            if (CH1cont_elec < Flow.CH1Cont_ElecMin || CH1cont_elec > Flow.CH1Cont_ElecMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH1cont_elec.ToString(""), "-", Flow.CH1Cont_ElecMax.ToString(), Flow.CH1Cont_ElecMin.ToString(), "OK");
                                            if (CH1lastpress == 0)
                                            {
                                                CH1lastpress = 0.1;
                                            }
                                            break;
                                        }
                                    }
                                    for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView1.Rows[i].Cells[1].Value.ToString() == $"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"))
                                        {
                                            CH1lastpress = Convert.ToDouble(this.DataGridView1.Rows[i].Cells[2].Value.ToString());
                                            CH1cont_press = CH1lastpress / CH2PressMax;
                                            CH1cont_press += Flow.CH1Cont_Pre_Compen;
                                            CH1cont_press = Math.Round(CH1cont_press, 2);
                                            CH1TestResult.PressRatio = CH1cont_press;
                                            if (CH1cont_press < Flow.CH1Cont_PressMin || CH1cont_press > Flow.CH1Cont_PressMax)
                                            {
                                                CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "NG");
                                                plc.CH1RatioNG();
                                                return;
                                            }
                                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH1cont_press.ToString(), "-", Flow.CH1Cont_PressMax.ToString(), Flow.CH1Cont_PressMin.ToString(), "OK");
                                            plc.CH1RatioOK();
                                            break;
                                        }
                                    }

                                    plc.CH1DOWNFlowEnd();
                                }
                            })));

                            if (CH1Tlight.Text != "NG")
                                if (CH1RTStep == "DOWN" || CH1RTStep == "FWD" || CH1RTStep == "RWD" || CH1RTStep == "UP")
                                {
                                    CH1Step += 1;
                                    CH1Method(CH1Step);
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH1-2读取输出压力") + ":" + ex.Message);
                    //MessageBox.Show("CH1-2读取输出压力：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH1-2读取输出压力") + ":" + ex.Message);
                    Logger.Log(ex.StackTrace);
                }


        }

        private void CH3ReadPress_Tick(object sender, EventArgs e)
        {
            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD")
                try
                {
                    ReadConfig con = new ReadConfig();
                    Model.Flow flow;
                    flow = con.ReadFlow();

                    //泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    JudgeCH2ADC = true;
                    CH3IsRun.Stop();
                    ch2_1step = 10;
                    CH3ReadPress.Interval = 300;
                    ch3client.btnSendData("03 03 04 03 00 17 ");
                    //leftclient.btnSendData("01 03 04 18 00 02 ");
                    ch3stage = 7;
                    //计算时间
                    ch3pressend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch3pressstart);
                    TimeSpan ts2 = new TimeSpan(ch3pressend);
                    TimeSpan ptime = ts2.Subtract(ts1).Duration();
                    double presstime = ptime.TotalSeconds;
                    double pressovertime;
                    if (CH2RTStep == "RWD")
                    {
                        pressovertime = Flow.CH2RWDOverTime;
                        //plc.CH2RWDStart();
                    }
                    else
                    {
                        if (CH2RTStep == "FWD")
                        {
                            pressovertime = elec.CH2FwdpreTime;
                        }
                        else
                            pressovertime = Flow.CH3Press_OverTime;
                    }

                    if (presstime > pressovertime)
                    {
                        CHpreflag2++;
                        CH3ReadPress.Stop();
                        CH2_1FullPress.Text = CH3PressMax.ToString();
                        CH3flowtest = false;
                        //CH1IsRun.Interval = 500;
                        //CH1IsRun.Start();
                        //ch1_1step = 5;//5-27

                        /////////////////////////////
                        //if (CHpreflag2 >= 2)
                        ////  if (!CH4flowtest)
                        //{
                        //    //CH4ReadFlowT.Stop();
                        //    CH3ReadPress.Stop();
                        //    //两台仪器都没有在测试，则停止lin通讯
                        //    CH2LinUP.Stop();
                        //    CH3ReadFlowT.Stop();
                        //    Thread.Sleep(200);
                        //    //plc.CH2PowerClose();
                        //    plc.CH2PLCValveBreak();

                        //    //流量测试完之后，需要把测试仪复位，避免后续步骤启动失败
                        //    {
                        //        ch3client.btnSendData("03 05 00 01 FF 00");
                        //        ch4client.btnSendData("04 05 00 01 FF 00");
                        //        Thread.Sleep(50);
                        //        ch3client.btnSendData("03 05 00 01 FF 00");
                        //        ch4client.btnSendData("04 05 00 01 FF 00");
                        //        Thread.Sleep(50);
                        //        ch3client.btnSendData("03 05 00 01 FF 00");
                        //        ch4client.btnSendData("04 05 00 01 FF 00");
                        //    }

                        //    ch3stage = 10;
                        //    CHXProBarFlag[3] = 0;
                        //    CH3IsRun.Interval = 500;
                        //    CH3IsRun.Start();
                        //    ch2_1step = 4;
                        //    ch3write = 1;
                        //    ch4stage = 10;
                        //    CHXProBarFlag[4] = 0;
                        //    CH4IsRun.Interval = 500;
                        //    CH4IsRun.Start();
                        //    ch2_2step = 4;
                        //    ch4write = 1;
                        //    if (plc.CH2LIN)
                        //    {
                        //        plc.CH2LinFinish();
                        //    }
                        //    if (!CH2ReadElecResult())
                        //    {
                        //        return;
                        //    }

                        //    CH2VDCresult = "OK";
                        //    CH2ADCresult = "OK";
                        //    if (CH2RTStep == "UP")
                        //    {
                        //        plc.CH2UPPreOK();//电流电压ok M
                        //        plc.CH2UPADCOK();//
                        //        plc.CH2UPVDCOK();//
                        //        plc.CH2uAOK();//增加一个BUG
                        //        CH2_2FullPress.Text = CH4PressMax.ToString();
                        //        CH2TestResult.UP_ADCMAX = CH2ADCMax;
                        //        CH2TestResult.UP_VDCMAX = CH2VDCMax;
                        //        CH2TestResult.UP_Flow = CH3Q;
                        //        CH2TestResult.UP_Pre = CH3PressMax;
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), CH3PressMax.ToString(), CH3PressureUnit.Text, "-", "-", "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2_2PreMax.ToString(), Flow.CH2_2PreMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH2ADCMax.ToString(), "A", elec.CH2UPADCMax.ToString(), elec.CH2UPADCMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH2VDCMax.ToString(), "V", elec.CH2UPVDCMax.ToString(), elec.CH2UPVDCMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), CH3Q.ToString(), "lpm", "-", "-", "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), CH4Q.ToString(), "lpm", Flow.CH2_2FlowMax.ToString(), Flow.CH2_2FlowMin.ToString(), "OK");


                        //        for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                        //        {
                        //            if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"))
                        //            {
                        //                CH2lastelec = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                        //                CH2cont_elec = CH2ADCMax / CH2lastelec;
                        //                CH2cont_elec += Flow.CH2Cont_Elec_Compen;
                        //                CH2cont_elec = Math.Round(CH2cont_elec, 2);
                        //                CH2TestResult.ElecRatio = CH2cont_elec;
                        //                if (CH2cont_elec < Flow.CH2Cont_ElecMin || CH2cont_elec > Flow.CH2Cont_ElecMax)
                        //                {
                        //                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "NG");
                        //                    plc.CH2RatioNG();
                        //                    FlowNG(2);
                        //                    return;
                        //                }

                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "OK");
                        //                if (CH2lastpress == 0)
                        //                {
                        //                    CH2lastpress = 0.1;
                        //                }
                        //                break;
                        //            }
                        //        }

                        //        for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                        //        {
                        //            if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"))
                        //            {
                        //                CH2lastpress = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                        //                CH2cont_press = CH3PressMax / CH2lastpress;
                        //                CH2cont_press += Flow.CH2Cont_Pre_Compen;
                        //                CH2cont_press = Math.Round(CH2cont_press, 2);
                        //                CH2TestResult.PressRatio = CH2cont_press;

                        //                if (CH2cont_press < Flow.CH2Cont_PressMin || CH2cont_press > Flow.CH2Cont_PressMax)
                        //                {
                        //                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "NG");
                        //                    plc.CH2RatioNG();
                        //                    FlowNG(2);
                        //                    return;
                        //                }
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "OK");
                        //                plc.CH2RatioOK();
                        //                break;
                        //            }
                        //        }
                        //        plc.CH2UPFlowEnd();
                        //    }
                        //    if (CH2RTStep == "FWD")
                        //    {

                        //        plc.CH2FWDVDCOK();
                        //        plc.CH2FWDFlowEnd();
                        //        plc.CH2uAOK();//增加一个BUG
                        //        CH2TestResult.FWD_ADCMAX = CH2ADCMax;
                        //        CH2TestResult.FWD_VDCMAX = CH2VDCMax;
                        //        CH2TestResult.FWD_Flow1 = Convert.ToDouble(CH2_1flow.Text);
                        //        CH2TestResult.FWD_Flow2 = Convert.ToDouble(CH2_2flow.Text);
                        //        CH2TestResult.FWD_Pre1 = Convert.ToDouble(CH2_1FullPress.Text);
                        //        CH2TestResult.FWD_Pre2 = Convert.ToDouble(CH2_2FullPress.Text);
                        //        ////////////24080101添加////////////////
                        //        ///
                        //        if (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2 > elec.TotalFlowMax || CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2 < elec.TotalFlowMin)
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");
                        //            FlowNG(2);
                        //        }
                        //        else
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "OK");
                        //        }
                        //        double preEROR = Math.Abs(CH2TestResult.FWD_Pre1 - CH2TestResult.FWD_Pre2);
                        //        if (preEROR > Convert.ToDouble(elec.TotalPreMax))
                        //        {
                        //            CH3Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), preEROR.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "NG");
                        //            FlowNG(2);
                        //        }
                        //        else
                        //            CH3Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), preEROR.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "OK");



                        //        ////////////
                        //        //6.18c
                        //        if (CH3PressMax > elec.CH2FwdPreMax || CH3PressMax < elec.CH2FwdPreMin)
                        //        {
                        //            log.MES_Logmsg(DateTime.Now.ToString() + "进入CH3Ng");

                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, elec.CH2FwdPreMax.ToString(),
                        //                elec.CH2FwdPreMin.ToString(), "NG");

                        //            FlowNG(2);
                        //        }
                        //        else
                        //        {
                        //            log.MES_Logmsg(DateTime.Now.ToString() + "进入CH3ok");
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, elec.CH2FwdPreMax.ToString(),
                        //              elec.CH2FwdPreMin.ToString(), "OK");
                        //        }
                        //        if (CH4PressMax > elec.CH2FwdPreMax || CH4PressMax < elec.CH2FwdPreMin)
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH2FwdPreMin.ToString(), "NG");
                        //            FlowNG(2);
                        //        }
                        //        else
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH2FwdPreMin.ToString().ToString(), "OK");

                        //        }

                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH2ADCMax.ToString(), "A", elec.CH2FWDADCMax.ToString(), elec.CH2FWDADCMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH2VDCMax.ToString(), "V", elec.CH2FWDVDCMax.ToString(), elec.CH2FWDVDCMin.ToString(), "OK");

                        //        if (CH3Q > flow.CH1_1FlowMax || CH3Q < flow.CH1_1FlowMin)
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH3Q.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "NG");
                        //            FlowNG(2);
                        //        }
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH3Q.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "OK");

                        //        if (CH4Q > flow.CH1_1FlowMax || CH4Q < flow.CH1_1FlowMin)
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH4Q.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "NG");
                        //            FlowNG(2);
                        //        }
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH4Q.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "OK");

                        //    }
                        //    if (CH2RTStep == "RWD")
                        //    {
                        //        if (Flow.CH2RWDOverTime == 0)
                        //        {
                        //            CH3PressMax = CH2TestResult.FWD_Pre1;
                        //            CH4PressMax = CH2TestResult.FWD_Pre2;
                        //        }
                        //        CH2TestResult.RWD_Pre1 = CH3PressMax;
                        //        CH2TestResult.RWD_Pre2 = CH4PressMax;
                        //        if (CH3PressMax > Flow.CH2RWDPressMax || CH3PressMax < Flow.CH2RWDPressMin || CH4PressMax > Flow.CH2RWDPressMax || CH4PressMax < Flow.CH2RWDPressMin)
                        //        {
                        //            if (CH3PressMax > Flow.CH2RWDPressMax || CH3PressMax < Flow.CH2RWDPressMin)
                        //            {
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "NG");
                        //                //6.18NG
                        //                FlowNG(2);
                        //            }
                        //            else
                        //            {
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                        //            }
                        //            if (CH4PressMax > Flow.CH2RWDPressMax || CH4PressMax < Flow.CH2RWDPressMin)
                        //            {
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "NG");
                        //                FlowNG(2);
                        //            }
                        //            else
                        //            {
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                        //            }
                        //            plc.CH2RWDPressNG();
                        //            plc.CH2RWDFlowEnd();
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                        //            plc.CH2RWDPressOK();
                        //            plc.CH2RWDADCOK();
                        //            plc.CH2RWDVDCOK();
                        //            plc.CH2uAOK();//增加一个BUG
                        //            plc.CH2RWDFlowEnd();
                        //            CH2TestResult.RWD_ADCMAX = CH2ADCMax;
                        //            CH2TestResult.RWD_VDCMAX = CH2VDCMax;
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH2ADCMax.ToString(), "A", elec.CH2RWDADCMax.ToString(), elec.CH2RWDADCMin.ToString(), "OK");
                        //            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH2VDCMax.ToString(), "V", elec.CH2RWDVDCMax.ToString(), elec.CH2RWDVDCMin.ToString(), "OK");
                        //        }
                        //    }
                        //    if (CH2RTStep == "DOWN")
                        //    {
                        //        CH3ReadPress.Stop();
                        //        plc.CH2DOWNPreOK();
                        //        CH2_1FullPress.Text = CH2PressMax.ToString();
                        //        //CH2ReadPress.Stop();
                        //        CH2TestResult.DOWN_Pre = CH4PressMax;
                        //        CH2TestResult.DOWN_ADCMAX = CH2ADCMax;
                        //        CH2TestResult.DOWN_VDCMAX = CH2VDCMax;
                        //        CH2TestResult.DOWN_Flow = CH4Q;
                        //        plc.CH2DOWNADCOK();
                        //        plc.CH2DOWNVDCOK();
                        //        plc.CH2PLCValveBreak();
                        //        plc.CH2uAOK();//增加一个BUG
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2_1PreMax.ToString(), Flow.CH2_1PreMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), CH4PressMax.ToString(), CH4PressureUnit.Text, "-", "-", "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH2ADCMax.ToString(), "A", elec.CH2DOWNADCMax.ToString(), elec.CH2DOWNADCMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH2VDCMax.ToString(), "V", elec.CH2DOWNVDCMax.ToString(), elec.CH2DOWNVDCMin.ToString(), "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), CH4Q.ToString(), "lpm", "-", "-", "OK");
                        //        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), CH3Q.ToString(), "lpm", Flow.CH2_1FlowMax.ToString(), Flow.CH2_1FlowMin.ToString(), "OK");

                        //        for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                        //        {
                        //            if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"))
                        //            {
                        //                CH2lastelec = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());

                        //                CH2cont_elec = CH2lastelec / CH2ADCMax;
                        //                CH2cont_elec += Flow.CH2Cont_Elec_Compen;
                        //                CH2cont_elec = Math.Round(CH2cont_elec, 2);

                        //                CH2TestResult.ElecRatio = CH2cont_elec;
                        //                if (CH2cont_elec < Flow.CH2Cont_ElecMin || CH2cont_elec > Flow.CH2Cont_ElecMax)
                        //                {
                        //                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "NG");
                        //                    plc.CH2RatioNG();
                        //                    return;
                        //                }
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "OK");
                        //                if (CH4PressMax == 0)
                        //                {
                        //                    CH4PressMax = 0.1;
                        //                }
                        //                break;
                        //            }
                        //        }

                        //        for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                        //        {
                        //            if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"))
                        //            {
                        //                CH2lastpress = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                        //                CH2cont_press = CH2lastpress / CH4PressMax;
                        //                CH2cont_press += Flow.CH2Cont_Pre_Compen;
                        //                CH2cont_press = Math.Round(CH2cont_press, 2);
                        //                CH2TestResult.PressRatio = CH2cont_press;
                        //                if (CH2cont_press < Flow.CH2Cont_PressMin || CH2cont_press > Flow.CH2Cont_PressMax)
                        //                {
                        //                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "NG");
                        //                    plc.CH2RatioNG();
                        //                    FlowNG(2);
                        //                    return;
                        //                }
                        //                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "OK");
                        //                plc.CH2RatioOK();
                        //                break;
                        //            }
                        //        }

                        //        plc.CH2DOWNFlowEnd();
                        //    }

                        //    if (CH2Tlight.Text != "NG")
                        //        if (CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD" || CH2RTStep == "UP")
                        //        {
                        //            CH2Step += 1;
                        //            CH2Method(CH2Step);
                        //        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2-1读取输出压力") + ":" + ex.Message);
                    //MessageBox.Show("CH2-1读取输出压力：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH2-1读取输出压力") + ":" + ex.Message);
                }
        }

        private void CH4ReadPress_Tick(object sender, EventArgs e)
        {
            if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD")
                try
                {
                    ReadConfig con = new ReadConfig();
                    Model.Flow flow;
                    flow = con.ReadFlow();

                    // 泄气会有误差，所以把电流电压读取放后面一点，在lin通讯后
                    JudgeCH2ADC = true;
                    CH4IsRun.Stop();
                    ch2_2step = 10;
                    CH4ReadPress.Interval = 300;
                    ch4client.btnSendData("04 03 04 03 00 17 ");
                    //leftclient.btnSendData("02 03 04 21 00 02 ");
                    ch4stage = 7;
                    //计算时间
                    ch4pressend = System.DateTime.Now.Ticks;
                    TimeSpan ts1 = new TimeSpan(ch4pressstart);
                    TimeSpan ts2 = new TimeSpan(ch4pressend);
                    TimeSpan ptime = ts2.Subtract(ts1).Duration();
                    double presstime = ptime.TotalSeconds;
                    double pressovertime;
                    if (CH2RTStep == "RWD")
                    {
                        pressovertime = Flow.CH2RWDOverTime;
                        //plc.CH2RWDStart();
                    }
                    else
                    {
                        pressovertime = Flow.CH4Press_OverTime;
                    }
                    if (presstime > pressovertime)
                    {
                       
                        CH2_2FullPress.Text = CH4PressMax.ToString();
                        CH4flowtest = false;
                        CHpreflag2++;
                        Invoke((new System.Action(() =>
                        {
                            if (CHpreflag2 >= 2)
                            {
                                CH4ReadPress.Stop();
                                //CH4ReadFlowT.Stop();
                                CH3ReadPress.Stop();
                                //两台仪器都没有在测试，则停止lin通讯
                                CH2LinUP.Stop();
                                CH3ReadFlowT.Stop();
                                Thread.Sleep(200);
                                //plc.CH2PowerClose();
                                plc.CH2PLCValveBreak();

                                //流量测试完之后，需要把测试仪复位，避免后续步骤启动失败
                                {
                                    ch3client.btnSendData("03 05 00 01 FF 00");
                                    ch4client.btnSendData("04 05 00 01 FF 00");
                                    Thread.Sleep(50);
                                    ch3client.btnSendData("03 05 00 01 FF 00");
                                    ch4client.btnSendData("04 05 00 01 FF 00");
                                    Thread.Sleep(50);
                                    ch3client.btnSendData("03 05 00 01 FF 00");
                                    ch4client.btnSendData("04 05 00 01 FF 00");
                                }

                                ch3stage = 10;
                                CHXProBarFlag[3] = 0;
                                CH3IsRun.Interval = 500;
                                CH3IsRun.Start();
                                ch2_1step = 4;
                                ch3write = 1;
                                ch4stage = 10;
                                CHXProBarFlag[4] = 0;
                                CH4IsRun.Interval = 500;
                                CH4IsRun.Start();
                                ch2_2step = 4;
                                ch4write = 1;
                                if (plc.CH2LIN)
                                {
                                    plc.CH2LinFinish();
                                }
                                if (!CH2ReadElecResult())
                                {
                                    return;
                                }

                                CH2VDCresult = "OK";
                                CH2ADCresult = "OK";
                                if (CH2RTStep == "UP")
                                {
                                    plc.CH2UPPreOK();//电流电压ok M
                                    plc.CH2UPADCOK();//
                                    plc.CH2UPVDCOK();//
                                    plc.CH2uAOK();//增加一个BUG
                                    CH2_2FullPress.Text = CH4PressMax.ToString();
                                    //8.7
                                    CH2TestResult.UP_ADCMAX = CH2ADCMax;
                                    CH2TestResult.UP_VDCMAX = CH2VDCMax;
                                    CH2TestResult.UP_Flow = CH3Q;
                                    CH2TestResult.UP_Flow1 = CH4Q;

                                    CH2TestResult.UP_Pre = CH3PressMax;
                                     CH2TestResult.UP_Pre1 = CH4PressMax;

                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), CH3PressMax.ToString(), CH3PressureUnit.Text, "-", "-", "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2_2PreMax.ToString(), Flow.CH2_2PreMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), CH2ADCMax.ToString(), "A", elec.CH2UPADCMax.ToString(), elec.CH2UPADCMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), CH2VDCMax.ToString(), "V", elec.CH2UPVDCMax.ToString(), elec.CH2UPVDCMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), CH3Q.ToString(), "lpm", "-", "-", "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), CH4Q.ToString(), "lpm", Flow.CH2_2FlowMax.ToString(), Flow.CH2_2FlowMin.ToString(), "OK");


                                    for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"))
                                        {
                                            CH2lastelec = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                                            CH2cont_elec = CH2ADCMax / CH2lastelec;
                                            CH2cont_elec += Flow.CH2Cont_Elec_Compen;
                                            CH2cont_elec = Math.Round(CH2cont_elec, 2);
                                            CH2TestResult.ElecRatio = CH2cont_elec;
                                            if (CH2cont_elec < Flow.CH2Cont_ElecMin || CH2cont_elec > Flow.CH2Cont_ElecMax)
                                            {
                                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "NG");
                                                plc.CH2RatioNG();
                                                FlowNG(2);
                                                return;
                                            }

                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "OK");
                                            if (CH2lastpress == 0)
                                            {
                                                CH2lastpress = 0.1;
                                            }
                                            break;
                                        }
                                    }

                                    for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"))
                                        {
                                            CH2lastpress = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                                            CH2cont_press = CH3PressMax / CH2lastpress;
                                            CH2cont_press += Flow.CH2Cont_Pre_Compen;
                                            CH2cont_press = Math.Round(CH2cont_press, 2);
                                            CH2TestResult.PressRatio = CH2cont_press;

                                            if (CH2cont_press < Flow.CH2Cont_PressMin || CH2cont_press > Flow.CH2Cont_PressMax)
                                            {
                                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "NG");
                                                plc.CH2RatioNG();
                                                FlowNG(2);
                                                return;
                                            }
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "OK");
                                            plc.CH2RatioOK();
                                            break;
                                        }
                                    }
                                    plc.CH2UPFlowEnd();
                                }
                                if (CH2RTStep == "FWD")
                                {

                                    plc.CH2UPPreOK();//电流电压ok M
                                    plc.CH2UPADCOK();//
                                    plc.CH2UPVDCOK();//
                                    plc.CH2uAOK();//增加一个BUG
                                    CH2TestResult.FWD_ADCMAX = CH2ADCMax;
                                    CH2TestResult.FWD_VDCMAX = CH2VDCMax;
                                    CH2TestResult.FWD_Flow1 = Convert.ToDouble(CH2_1flow.Text);
                                    CH2TestResult.FWD_Flow2 = Convert.ToDouble(CH2_2flow.Text);
                                    CH2TestResult.FWD_Pre1 = Convert.ToDouble(CH2_1FullPress.Text);
                                    CH2TestResult.FWD_Pre2 = Convert.ToDouble(CH2_2FullPress.Text);
                                    ////////////24080101添加////////////////
                                    


                                    if (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2 > elec.TotalFlowMax || CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2 < elec.TotalFlowMin)
                                    {
                                        CH2TestResult.FWD_FlowSum = CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2;
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "NG");
                                     
                                        FlowNG(2);
                                    }
                                    else
                                    {
                                        CH2TestResult.FWD_FlowSum = CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2;
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), (CH2TestResult.FWD_Flow1 + CH2TestResult.FWD_Flow2).ToString(), "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "OK");
                                    }
                                    double preEROR = Math.Abs(CH2TestResult.FWD_Pre1 - CH2TestResult.FWD_Pre2);

                                    if (preEROR > Convert.ToDouble(elec.TotalPreMax))
                                    {
                                        CH2TestResult.FWD_PreSum = preEROR;
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH2TestResult.FWD_PreSum.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "NG");
                                        FlowNG(2);
                                    }
                                    else
                                    {
                                        CH2TestResult.FWD_PreSum = preEROR;
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), CH2TestResult.FWD_PreSum.ToString(), "Kpa", elec.TotalPreMax.ToString(), "0", "OK");

                                    }

                                    //////////
                                    //6.18c
                                    if (CH2TestResult.FWD_Pre1 > elec.CH2FwdPreMax || CH2TestResult.FWD_Pre1 < elec.CH2FwdPreMin)
                                    {

                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH2TestResult.FWD_Pre1.ToString(), CH3PressureUnit.Text, elec.CH2FwdPreMax.ToString(),
                                            elec.CH2FwdPreMin.ToString(), "NG");
                                        FlowNG(2);
                                    }
                                    else
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), CH2TestResult.FWD_Pre1.ToString(), CH3PressureUnit.Text, elec.CH2FwdPreMax.ToString(),
                                          elec.CH2FwdPreMin.ToString(), "OK");
                                    }
                                    if (CH2TestResult.FWD_Pre2 > elec.CH2FwdPreMax || CH2TestResult.FWD_Pre2 < elec.CH2FwdPreMin)
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH2TestResult.FWD_Pre2.ToString(), CH4PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH2FwdPreMin.ToString(), "NG");
                                        FlowNG(2);
                                    }
                                    else
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), CH2TestResult.FWD_Pre2.ToString(), CH4PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH2FwdPreMin.ToString().ToString(), "OK");

                                    }

                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), CH2ADCMax.ToString(), "A", elec.CH2FWDADCMax.ToString(), elec.CH2FWDADCMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), CH2VDCMax.ToString(), "V", elec.CH2FWDVDCMax.ToString(), elec.CH2FWDVDCMin.ToString(), "OK");

                                    if (CH2TestResult.FWD_Flow1 > elec.CH2FwdFlowMax || CH2TestResult.FWD_Flow1 < elec.CH2FwdFlowMin)
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH2TestResult.FWD_Flow1.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "NG");
                                        FlowNG(2);
                                    }
                                    else
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), CH2TestResult.FWD_Flow1.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "OK");

                                    if (CH2TestResult.FWD_Flow2 > elec.CH2FwdFlowMax || CH2TestResult.FWD_Flow2 < elec.CH2FwdFlowMin)
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2TestResult.FWD_Flow2.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "NG");
                                        FlowNG(2);
                                    }
                                    else
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), CH2TestResult.FWD_Flow2.ToString(), "lpm", elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "OK");

                                }
                                if (CH2RTStep == "RWD")
                                {
                                    if (Flow.CH2RWDOverTime == 0)
                                    {
                                        CH3PressMax = CH2TestResult.FWD_Pre1;
                                        CH4PressMax = CH2TestResult.FWD_Pre2;
                                    }
                                    CH2TestResult.RWD_Pre1 = CH3PressMax;
                                    CH2TestResult.RWD_Pre2 = CH4PressMax;
                                    if (CH3PressMax > Flow.CH2RWDPressMax || CH3PressMax < Flow.CH2RWDPressMin || CH4PressMax > Flow.CH2RWDPressMax || CH4PressMax < Flow.CH2RWDPressMin)
                                    {
                                        if (CH3PressMax > Flow.CH2RWDPressMax || CH3PressMax < Flow.CH2RWDPressMin)
                                        {
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "NG");
                                            //6.18NG
                                            FlowNG(2);
                                        }
                                        else
                                        {
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                                        }
                                        if (CH4PressMax > Flow.CH2RWDPressMax || CH4PressMax < Flow.CH2RWDPressMin)
                                        {
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "NG");
                                            FlowNG(2);
                                        }
                                        else
                                        {
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                                        }
                                        plc.CH2RWDPressNG();
                                        plc.CH2RWDFlowEnd();
                                        return;
                                    }
                                    else
                                    {
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), CH4PressMax.ToString(), CH4PressureUnit.Text, Flow.CH2RWDPressMax.ToString(), Flow.CH2RWDPressMin.ToString(), "OK");
                                        plc.CH2RWDPressOK();
                                        plc.CH2RWDADCOK();
                                        plc.CH2RWDVDCOK();
                                        plc.CH2uAOK();//增加一个BUG
                                        plc.CH2RWDFlowEnd();
                                        CH2TestResult.RWD_ADCMAX = CH2ADCMax;
                                        CH2TestResult.RWD_VDCMAX = CH2VDCMax;
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), CH2ADCMax.ToString(), "A", elec.CH2RWDADCMax.ToString(), elec.CH2RWDADCMin.ToString(), "OK");
                                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), CH2VDCMax.ToString(), "V", elec.CH2RWDVDCMax.ToString(), elec.CH2RWDVDCMin.ToString(), "OK");
                                    }
                                }
                                if (CH2RTStep == "DOWN")
                                {
                                    CH3ReadPress.Stop();
                                    plc.CH2DOWNPreOK();
                                    CH2_1FullPress.Text = CH2PressMax.ToString();
                                    //CH2ReadPress.Stop();
                                    CH2TestResult.DOWN_Pre = CH4PressMax;
                                    CH2TestResult.DOWN_Pre1 = CH3PressMax;
                                    CH2TestResult.DOWN_ADCMAX = CH2ADCMax;
                                    CH2TestResult.DOWN_VDCMAX = CH2VDCMax;
                                    CH2TestResult.DOWN_Flow = CH4Q;
                                    CH2TestResult.DOWN_Flow1 = CH3Q;
                                    plc.CH2DOWNADCOK();
                                    plc.CH2DOWNVDCOK();
                                    plc.CH2PLCValveBreak();
                                    plc.CH2uAOK();//增加一个BUG
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), CH3PressMax.ToString(), CH3PressureUnit.Text, Flow.CH2_1PreMax.ToString(), Flow.CH2_1PreMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), CH4PressMax.ToString(), CH4PressureUnit.Text, "-", "-", "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), CH2ADCMax.ToString(), "A", elec.CH2DOWNADCMax.ToString(), elec.CH2DOWNADCMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), CH2VDCMax.ToString(), "V", elec.CH2DOWNVDCMax.ToString(), elec.CH2DOWNVDCMin.ToString(), "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), CH4Q.ToString(), "lpm", "-", "-", "OK");
                                    CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), CH3Q.ToString(), "lpm", Flow.CH2_1FlowMax.ToString(), Flow.CH2_1FlowMin.ToString(), "OK");

                                    for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"))
                                        {
                                            CH2lastelec = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());

                                            CH2cont_elec = CH2lastelec / CH2ADCMax;
                                            CH2cont_elec += Flow.CH2Cont_Elec_Compen;
                                            CH2cont_elec = Math.Round(CH2cont_elec, 2);

                                            CH2TestResult.ElecRatio = CH2cont_elec;
                                            if (CH2cont_elec < Flow.CH2Cont_ElecMin || CH2cont_elec > Flow.CH2Cont_ElecMax)
                                            {
                                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "NG");
                                                plc.CH2RatioNG();
                                                return;
                                            }
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "电流比值"), CH2cont_elec.ToString(), "-", Flow.CH2Cont_ElecMax.ToString(), Flow.CH2Cont_ElecMin.ToString(), "OK");
                                            if (CH4PressMax == 0)
                                            {
                                                CH4PressMax = 0.1;
                                            }
                                            break;
                                        }
                                    }

                                    for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
                                    {
                                        if (this.DataGridView2.Rows[i].Cells[1].Value.ToString() == $"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"))
                                        {
                                            CH2lastpress = Convert.ToDouble(this.DataGridView2.Rows[i].Cells[2].Value.ToString());
                                            CH2cont_press = CH2lastpress / CH4PressMax;
                                            CH2cont_press += Flow.CH2Cont_Pre_Compen;
                                            CH2cont_press = Math.Round(CH2cont_press, 2);
                                            CH2TestResult.PressRatio = CH2cont_press;
                                            if (CH2cont_press < Flow.CH2Cont_PressMin || CH2cont_press > Flow.CH2Cont_PressMax)
                                            {
                                                CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "NG");
                                                plc.CH2RatioNG();
                                                FlowNG(2);
                                                return;
                                            }
                                            CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "压力比值"), CH2cont_press.ToString(), "-", Flow.CH2Cont_PressMax.ToString(), Flow.CH2Cont_PressMin.ToString(), "OK");
                                            plc.CH2RatioOK();
                                            break;
                                        }
                                    }

                                    plc.CH2DOWNFlowEnd();
                                }

                                if (CH2Tlight.Text != "NG")
                                    if (CH2RTStep == "DOWN" || CH2RTStep == "FWD" || CH2RTStep == "RWD" || CH2RTStep == "UP")
                                    {
                                        CH2Step += 1;
                                        CH2Method(CH2Step);
                                    }
                            }
                        })));
                       
                    }
                }
                catch (Exception ex)
                {
                    wa.InsertWarningData(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "-", I18N.GetLangText(dicLang, "CH2-2读取输出压力") + ":" + ex.Message);
                    //MessageBox.Show("CH2-2读取输出压力：" + ex.Message);
                    Logger.Log(I18N.GetLangText(dicLang, "CH2-2读取输出压力") + ":" + ex.Message);
                }
        }

        ///// <summary>
        ///// 读取测试单位
        ///// </summary>
        ///// <param name="CH"></param>
        //private void ReadPressConfig()
        //{
        //    RegistryKey regName;

        //    regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + machine, true);

        //    if (regName is null)
        //    {
        //        regName = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + machine);
        //    }
        //    if (regName.GetValue("1unit") != null)
        //    {
        //        regName.OpenSubKey("User");
        //        ch1chkunit = Convert.ToBoolean(regName.GetValue("1unit"));
        //    }
        //    if (regName.GetValue("2unit") != null)
        //    {
        //        regName.OpenSubKey("User");
        //        ch2chkunit = Convert.ToBoolean(regName.GetValue("2unit"));
        //    }
        //    if (regName.GetValue("3unit") != null)
        //    {
        //        regName.OpenSubKey("User");
        //        ch3chkunit = Convert.ToBoolean(regName.GetValue("3unit"));
        //    }
        //    if (regName.GetValue("4unit") != null)
        //    {
        //        regName.OpenSubKey("User");
        //        ch4chkunit = Convert.ToBoolean(regName.GetValue("4unit"));
        //    }
        //}
        /// <summary>
        /// 读取PLC的设置并进行写入
        /// </summary>
        private void ReadPLC()
        {
            if (plc.IsConnect)
            {
                PLCSignal.Stop();
                string dialog;
                //dialog = "PLC.ini";
                dialog = Form1.f1.machine;
                ConfigINI mesconfig = new ConfigINI("Model", dialog);
                string ch1pre = mesconfig.IniReadValue("PLC", "CH1Pressure");
                if (!String.IsNullOrEmpty(ch1pre))
                {
                    plc.ch1pre = Convert.ToInt16(ch1pre);
                    plc.CH1Writevalve();
                }
                string ch2pre = mesconfig.IniReadValue("PLC", "CH2Pressure");
                if (!String.IsNullOrEmpty(ch2pre))
                {
                    plc.ch2pre = Convert.ToInt16(ch2pre);
                    plc.CH2Writevalve();
                }
                string ch3pre = mesconfig.IniReadValue("PLC", "CH3Pressure");
                if (!String.IsNullOrEmpty(ch3pre))
                {
                    plc.ch3pre = Convert.ToInt16(ch3pre);
                    plc.CH3Writevalve();
                }
                string ch4pre = mesconfig.IniReadValue("PLC", "CH4Pressure");
                if (!String.IsNullOrEmpty(ch4pre))
                {
                    plc.ch4pre = Convert.ToInt16(ch4pre);
                    plc.CH4Writevalve();
                }
                string ch1vol = mesconfig.IniReadValue("PLC", "CH1Vol");
                if (!String.IsNullOrEmpty(ch1vol))
                {
                    Form1.f1.plc.ch1vol = Convert.ToInt32(Convert.ToDouble(ch1vol) * 1000);
                    Form1.f1.plc.CH1WriteVol();
                }
                string ch2vol = mesconfig.IniReadValue("PLC", "CH2Vol");
                if (!String.IsNullOrEmpty(ch2vol))
                {
                    Form1.f1.plc.ch2vol = Convert.ToInt32(Convert.ToDouble(ch2vol) * 1000);
                    Form1.f1.plc.CH2WriteVol();
                }
                string ch1elec = mesconfig.IniReadValue("PLC", "CH1Elect");
                if (!String.IsNullOrEmpty(ch1elec))
                {
                    Form1.f1.plc.ch1elec = Convert.ToInt32(Convert.ToDouble(ch1elec) * 10000);
                    Form1.f1.plc.CH1WriteElec();
                }
                string ch2elec = mesconfig.IniReadValue("PLC", "CH2Elect");
                if (!String.IsNullOrEmpty(ch2elec))
                {
                    Form1.f1.plc.ch2elec = Convert.ToInt32(Convert.ToDouble(ch2elec) * 10000);
                    Form1.f1.plc.CH2WriteElec();
                }
                //读取PLC勾选的配置
                string safetydoor = mesconfig.IniReadValue("PLC", "safetydoor");
                if (!String.IsNullOrEmpty(safetydoor))
                {
                    if (Convert.ToBoolean(safetydoor))
                    {
                        plc.SafetyDoorClose();
                    }
                    else
                    {
                        plc.SafetyDoorOpen();
                    }
                }
                string ch1shiel = mesconfig.IniReadValue("PLC", "CH1shieldbee");
                if (!String.IsNullOrEmpty(ch1shiel))
                {
                    if (Convert.ToBoolean(ch1shiel))
                    {
                        plc.CH1BeeOpen();
                    }
                    else
                    {
                        plc.CH1BeeClose();
                    }
                }
                string ch2shiel = mesconfig.IniReadValue("PLC", "CH2shieldbee");
                if (!String.IsNullOrEmpty(ch2shiel))
                {
                    if (Convert.ToBoolean(ch1shiel))
                    {
                        plc.CH2BeeOpen();
                    }
                    else
                    {
                        plc.CH2BeeClose();
                    }
                }
                string ch1openclose = mesconfig.IniReadValue("PLC", "CH1openclose");
                if (!String.IsNullOrEmpty(ch1openclose))
                {
                    if (Convert.ToBoolean(ch1openclose))
                    {
                        plc.CH1Open();
                    }
                    else
                    {
                        plc.CH1Close();
                    }
                }
                string ch2openclose = mesconfig.IniReadValue("PLC", "CH2openclose");
                if (!String.IsNullOrEmpty(ch2openclose))
                {
                    if (Convert.ToBoolean(ch2openclose))
                    {
                        plc.CH2Open();
                    }
                    else
                    {
                        plc.CH2Close();
                    }
                }
                string ch3openclose = mesconfig.IniReadValue("PLC", "CH3openclose");
                if (!String.IsNullOrEmpty(ch3openclose))
                {
                    if (Convert.ToBoolean(ch3openclose))
                    {
                        plc.CH3Open();
                    }
                    else
                    {
                        plc.CH3Close();
                    }
                }
                string ch4openclose = mesconfig.IniReadValue("PLC", "CH4openclose");
                if (!String.IsNullOrEmpty(ch4openclose))
                {
                    if (Convert.ToBoolean(ch4openclose))
                    {
                        plc.CH4Open();
                    }
                    else
                    {
                        plc.CH4Close();
                    }
                }
                PLCSignal.Start();
            }
        }

        /// <summary>
        /// CH1流量方法,i为集合索引
        /// </summary>
        /// <param name="i"></param>
        private void CH1Method(int i)
        {
            //启动前清空参数 避免写入上传数据
         

            ReadConfig con = new ReadConfig();
            Model.Flow flow;
            flow = con.ReadFlow();
            elec = con.ReadElectricity();
            Fwdjg = 0;
            Fwdjg2 = 0;
            CH1ADCList.Clear();
            CH1VDCList.Clear();
            CH1ReadElecCount = 0;
            CH1ReadElec.Stop();
            //计算电压和电流是否超过上下限
            if (CH1POWER._serialPort.IsOpen)
                Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
            System.Threading.Thread.Sleep(100);
            if (CH1POWER._serialPort.IsOpen)
                Form1.CH1POWER._serialPort.WriteLine("OUTP 0");
            System.Threading.Thread.Sleep(1000);
            //LeftCH1Status.Text = "待机";
            //LeftCH2Status.Text = "待机";
            {
                plc.WriteCH1SC(false);
                plc.WriteCH1XC(false);
                plc.WriteCH1TC(false);
                plc.WriteCH1XQ(false);
                plc.WriteCH1QC(false);

                // plc.CH1HLevelFlase(false);
                //Thread.Sleep(50);
            }

            if (i < CH1OrderTemp.Count)
            {

                CHpreflag = 0;
                if (CH1POWER._serialPort.IsOpen)
                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                System.Threading.Thread.Sleep(100);
                if (CH1POWER._serialPort.IsOpen)
                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                CH1ADCMax = 0;
                CH1VDCMax = 0;
                //CH1ADC = 0;
                //CH1VDC = 0;
                CH1Q = 0;
                CH2Q = 0;
                CH1PressMax = 0;
                CH2PressMax = 0;
                CH1ADCresult = "-";
                CH1VDCresult = "-";
                CH1IsRun.Stop();
                CH2IsRun.Stop();
                CH1flowtest = false;
                CH2flowtest = false;
                ch1fullend = 0;
                ch2fullend = 0;
                //CH1timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //LIN
                if (plc.CH1LIN)
                {
                    //CH1lin = new LIN(1, CH1LinBaudrate);
                    //CH1lin.LinComm();
                    CH1lin = new LIN_LDFParser(1);
                    CH1lin.Main(linconfig.LDFFileName);
                    //  CH1lin.MainThread();//开其他线程发送

                }
                CH1RTStep = CH1OrderTemp[i];
                CH1timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //3.9

                Model.CH_PARAMS ch_params;
                ch_params = con.ReadParameters(1, 2);
                string Leaklowlimit = ch_params.Leaklowlimit;
                switch (CH1OrderTemp[i])
                {
                    case "UP":
                        //切换参数并启动
                        plc.CH1valveopen();
                        plc.CH2valveopen();
                        plc.CH1UPStart();

                        ReadParameters(1, 1);
                        ReadParameters(1, 2);

                        CH1flowtest = true;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), "", "A", elec.CH1UPADCMax.ToString(), elec.CH1UPADCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), "", "V", elec.CH1UPVDCMax.ToString(), elec.CH1UPVDCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), "", PressureUnit.Text, "-", "-", "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), "", CH2PressureUnit.Text, "-", "-", "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), "", "lpm", "-", "-".ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), "", "lpm", "-", "-".ToString(), "");
                        //plc.CH1PowerOpen();
                        // if (!CH1change1UpDownChange)
                        if (!CH1change)
                            plc.WriteCH1SC(true);
                        else
                            plc.WriteCH1XC(true);

                        break;

                    case "DOWN":
                        //切换参数并启动
                        plc.CH1valveopen();
                        plc.CH2valveopen();
                        plc.CH1DOWNStart();

                        ReadParameters(1, 1);
                        ReadParameters(1, 2);

                        CH2flowtest = true;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), "", "A", elec.CH1DOWNADCMax.ToString(), elec.CH1DOWNADCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), "", "V", elec.CH1DOWNVDCMax.ToString(), elec.CH1DOWNVDCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), "", CH2PressureUnit.Text, "-", "-", "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), "", PressureUnit.Text, "-", "-", "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), "", "lpm", "-", "-".ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), "", "lpm", "-", "-".ToString(), "");
                        //plc.CH1PowerOpen();
                        //
                        //if (!Electricity.ord.CH1UpDownChange)
                        if (!CH1change)
                            plc.WriteCH1XC(true);
                        else
                            plc.WriteCH1SC(true);
                        break;

                    case "FWD":
                        plc.CH1valveopen();
                        plc.CH2valveopen();
                        plc.CH1FWDStart();
                        ReadParameters(1, 1);
                        ReadParameters(1, 2);

                        CH1flowtest = true;
                        CH2flowtest = true;
                        Invoke((new System.Action(() =>
                        {
                            //240801新增
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), "", "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "");
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), "", "Kpa", elec.TotalPreMax.ToString(), "0", "");


                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), "", "A", elec.CH1FWDADCMax.ToString(), elec.CH1FWDADCMin.ToString(), "");
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), "", "V", elec.CH1FWDVDCMax.ToString(), elec.CH1FWDVDCMin.ToString(), "");

                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), "", PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "");
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), "", CH2PressureUnit.Text, elec.CH1FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "");
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), "", "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "");
                            CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), "", "lpm", elec.CH1FwdFlowMax.ToString(), elec.CH1FwdFlowMin.ToString(), "");
                            

                        })));
                                              //plc.CH1PowerOpen();
                        plc.WriteCH1TC(true);

                        break;

                    case "RWD":
                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        plc.CH1RWDStart();

                        ReadParameters(1, 1);
                        ReadParameters(1, 2);

                        CH1ReadPress.Interval = 1500;
                        CH1ReadPress.Start();
                        CH2ReadPress.Interval = 1500;
                        CH2ReadPress.Start();
                        CH1flowtest = true;
                        CH2flowtest = true;
                        ch1pressstart = DateTime.Now.Ticks;
                        ch2pressstart = DateTime.Now.Ticks;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), "", "A", elec.CH1RWDADCMax.ToString(), elec.CH1RWDADCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), "", "V", elec.CH1RWDVDCMax.ToString(), elec.CH1RWDVDCMin.ToString(), "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), "", PressureUnit.Text, "-", "-", "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), "", CH2PressureUnit.Text, "-", "-", "");
                        //plc.CH1PowerOpen();
                        plc.WriteCH1XQ(true);
                        break;

                    case "UPLeak":
                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        plc.CH1UPStart();
                        plc.CH1UPLeak();

                        System.Threading.Thread.Sleep(300);
                        if (CH1Pump)
                        {
                            //  if (!Electricity.ord.CH1UpDownChange)
                            if (!CH1change)
                                plc.WriteCH1SC(true);
                            else
                                plc.WriteCH1XC(true);

                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(3, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(4, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(6, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(8, 1);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(2, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(4, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(6, 1);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(8, 1);
                            }
                        }
                        CH1IsRun.Interval = 300;
                        CH1IsRun.Start();
                        ch1_1step = 1;
                        CH2IsRun.Interval = 300;
                        CH2IsRun.Start();
                        //ch1_2step = 5;//5-27
                        ch1_2step = 1;//ERIC
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-1上充气密"), "", LeakUnit.Text, ch1_1params.Leaktoplimit, ch1_1params.Leaklowlimit, "");
                        //plc.CH1PowerOpen();
                        break;

                    case "DOWNLeak":

                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        plc.CH1DOWNStart();
                        plc.CH1DOWNLeak();


                        if (CH1Pump)
                        {
                            //  if (!Electricity.ord.CH1UpDownChange)
                            if (!CH1change)
                                plc.WriteCH1XC(true);
                            else
                                plc.WriteCH1SC(true);
                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(3, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(5, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(7, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(9, 2);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(2, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(5, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(7, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(9, 2);
                            }
                        }
                        CH1IsRun.Interval = 300;
                        CH1IsRun.Start();
                        //ch1_1step = 5;//5-27
                        ch1_1step = 1;//ERIC
                        CH2IsRun.Interval = 300;
                        CH2IsRun.Start();
                        ch1_2step = 1;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-2下充气密"), "", CH2LeakUnit.Text, ch1_2params.Leaktoplimit, ch1_2params.Leaklowlimit, "");
                        //plc.CH1PowerOpen();
                        break;

                    case "FWDLeak":

                        plc.CH1valveclose();
                        plc.CH2valveclose();
                        plc.CH1FWDStart();
                        plc.CH1FWDLeakTrue();
                        System.Threading.Thread.Sleep(300);
                        if (CH1Pump)
                        {
                            plc.WriteCH1TC(true);
                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(3, 1);
                                ReadParameters(3, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(4, 1);
                                ReadParameters(5, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(6, 1);
                                ReadParameters(7, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(8, 1);
                                ReadParameters(9, 2);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH1RunName))
                            {
                                ReadParameters(2, 1);
                                ReadParameters(2, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "A通道")))
                            {
                                ReadParameters(4, 1);
                                ReadParameters(5, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "B通道")))
                            {
                                ReadParameters(6, 1);
                                ReadParameters(7, 2);
                            }
                            if (CH1RunName.Equals(I18N.GetLangText(dicLang, "C通道")))
                            {
                                ReadParameters(8, 1);
                                ReadParameters(9, 2);
                            }
                        }
                        CH1IsRun.Interval = 1300;
                        CH1IsRun.Start();
                        ch1_1step = 1;
                        CH2IsRun.Interval = 1300;
                        CH2IsRun.Start();
                        ch1_2step = 1;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-1同充气密"), "", LeakUnit.Text, ch1_1params.Leaktoplimit, Leaklowlimit, "");
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "CH1-2同充气密"), "", CH2LeakUnit.Text, ch1_2params.Leaktoplimit, Leaklowlimit, "");
                        //plc.CH1PowerOpen();
                        break;

                    case "QC":
                        //Electricity静态电流
                        CH1ReaduA.Interval = 200;
                        CH1ReaduA.Start();
                        ch1uAstarttime = System.DateTime.Now.Ticks;
                        plc.WriteCH1QC(true);
                        if (CH1POWER._serialPort.IsOpen)
                            Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
                        CH1uAarray.Clear();
                        plc.CH1uA = 0;
                        CH1Display($"{CH1RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), "", "uA", elec.CH1ElecMax.ToString(), elec.CH1ElecMin.ToString(), "");
                        //plc.CH1PowerOpen();
                        break;
                }
                if (plc.CH1LIN)
                {
                    CH1LinUP.Interval = 100;
                    CH1LinUP.Start();
                }
                if (CH1RTStep == "UP" || CH1RTStep == "DOWN" || CH1RTStep == "FWD")
                {
                    //ch11iread = 0;
                    CH1ReadFlowT.Interval = 300;
                    CH1ReadFlowT.Start();
                    ch1fullstart = DateTime.Now.Ticks;
                    CH2ReadFlowT.Interval = 300;
                    CH2ReadFlowT.Start();
                    ch2fullstart = DateTime.Now.Ticks;
                }
                CH1ReadElec.Interval = 1000;
                CH1ReadElec.Start();
                if (CH1POWER._serialPort.IsOpen)
                    Form1.CH1POWER._serialPort.WriteLine("OUTP 1");
            }
            else
            {
                //步骤都成功完成，且后面没有步骤需要切换，则输出工位完成信号
                {
                    timerCH1CT.Stop();
                    CH1IsStart = false;
                    if (CH1POWER._serialPort.IsOpen)
                        CH1POWER._serialPort.WriteLine("OUTP 0");
                    System.Threading.Thread.Sleep(100);
                    if (CH1POWER._serialPort.IsOpen)
                        CH1POWER._serialPort.WriteLine("OUTP 0");
                    plc.WriteCH1RatioOK();
                }
                plc.CH1FlowEnd();
                CH1RTStep = "";
            }
        }


        



        /// <summary>
        /// CH2流量方法,i为集合索引
        /// </summary>
        /// <param name="i"></param>
        private void CH2Method(int i)
        {
            //启动前清空参数 避免写入上传数据
      
            
            ReadConfig con = new ReadConfig();
            Model.Flow flow;
            flow = con.ReadFlow();
            elec = con.ReadElectricity();
            CH2JGFLAG = 0;
            CH2ReadElec.Stop();
            CH2ADCList.Clear();
            CH2VDCList.Clear();
            CH2ReadElecCount = 0;
            //RightCH1Status.Text = "待机";
            //RightCH2Status.Text = "待机";
            CH2POWER.Write("OUTP 0");
            CH2POWER.Write("OUTP 0");
            System.Threading.Thread.Sleep(1000);
            {
                plc.WriteCH2SC(false);
                plc.WriteCH2XC(false);
                plc.WriteCH2TC(false);
                plc.WriteCH2XQ(false);
                plc.WriteCH2QC(false);
                //  Thread.Sleep(50);
            }

            if (i < CH2OrderTemp.Count)
            {
                CHpreflag2 = 0;
                CH2POWER.Write("OUTP 1");
                System.Threading.Thread.Sleep(100);
                CH2POWER.Write("OUTP 1");
                CH2ADCMax = 0;
                CH2VDCMax = 0;
                //CH2ADC = 0;
                //CH2VDC = 0;
                CH3Q = 0;
                CH4Q = 0;
                CH3PressMax = 0;
                CH4PressMax = 0;
                CH2ADCresult = "-";
                CH2VDCresult = "-";
                CH3IsRun.Stop();
                CH4IsRun.Stop();
                CH3flowtest = false;
                CH4flowtest = false;
                ch3fullend = 0;
                ch4fullend = 0;
                //CH2timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //LIN
                if (plc.CH2LIN)
                {
                    //CH2lin = new LIN(1, CH2LinBaudrate);
                    //CH2lin.LinComm();
                    CH2lin = new LIN_LDFParser(2);
                    CH2lin.Main(linconfig.LDFFileName);
                }
                CH2RTStep = CH2OrderTemp[i];
                CH2timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //3.9
                //ReadConfig con = new ReadConfig();
                Model.CH_PARAMS ch_params;
                ch_params = con.ReadParameters(1, 2);
                string Leaklowlimit = ch_params.Leaklowlimit;

                switch (CH2OrderTemp[i])
                {
                    case "UP":
                        //切换参数并启动
                        plc.CH3valveopen();
                        plc.CH4valveopen();
                        plc.CH2UPStart();//2032 up process

                        ReadParameters(1, 3);
                        ReadParameters(1, 4);

                        CH3flowtest = true;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(UP)"), "", "A", elec.CH2UPADCMax.ToString(), elec.CH2UPADCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(UP)"), "", "V", elec.CH2UPVDCMax.ToString(), elec.CH2UPVDCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP)"), "", CH3PressureUnit.Text, "-", "-", "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(UP-DOWN)"), "", CH4PressureUnit.Text, "-", "-", "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP)"), "", "lpm", "-", "-".ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(UP-DOWN)"), "", "lpm", "-", "-".ToString(), "");
                        //plc.CH2PowerOpen();
                        // if (!Electricity.ord.CH2UpDownChange)
                        if (!CH2change)
                            plc.WriteCH2SC(true);
                        else

                            plc.WriteCH2XC(true);
                        break;

                    case "DOWN":
                        //切换参数并启动
                        plc.CH3valveopen();
                        plc.CH4valveopen();
                        ReadParameters(1, 3);
                        ReadParameters(1, 4);

                        CH4flowtest = true;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(DOWN)"), "", "A", elec.CH2DOWNADCMax.ToString(), elec.CH2DOWNADCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(DOWN)"), "", "V", elec.CH2DOWNVDCMax.ToString(), elec.CH2DOWNVDCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN)"), "", CH4PressureUnit.Text, "-", "-", "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(DOWN-UP)"), "", CH3PressureUnit.Text, "-", "-", "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN)"), "", "lpm", "-", "-".ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(DOWN-UP)"), "", "lpm", "-", "-".ToString(), "");
                        plc.CH2DOWNStart();
                        // if (!Electricity.ord.CH2UpDownChange)
                        if (!CH2change)
                            plc.WriteCH2XC(true);
                        else

                            plc.WriteCH2SC(true);
                        //plc.CH2PowerOpen();
                        break;

                    case "FWD":
                        plc.CH3valveopen();
                        plc.CH4valveopen();
                        plc.CH2FWDStart();

                        ReadParameters(1, 3);
                        ReadParameters(1, 4);

                        CH3flowtest = true;
                        CH4flowtest = true;
                        //240802
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD总)"), "", "lpm", elec.TotalFlowMax.ToString(), elec.TotalFlowMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max压力差(FWD)"), "", "Kpa", elec.TotalPreMax.ToString(), "0", "");
                        //240802end

                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(FWD)"), "", "A", elec.CH2FWDADCMax.ToString(), elec.CH2FWDADCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(FWD)"), "", "V", elec.CH2FWDVDCMax.ToString(), elec.CH2FWDVDCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD上)"), "", CH3PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(FWD下)"), "", CH4PressureUnit.Text, elec.CH2FwdPreMax.ToString(), elec.CH1FwdPreMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD上)"), "", "lpm",  elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max流量(FWD下)"), "", "lpm",  elec.CH2FwdFlowMax.ToString(), elec.CH2FwdFlowMin.ToString(), "");
                        //plc.CH2PowerOpen();
                        plc.WriteCH2TC(true);
                        break;

                    case "RWD":
                        plc.CH3valveclose();
                        plc.CH4valveclose();
                        plc.CH2RWDStart();
                        CH2POWER.Write("OUTP 1");
                        ReadParameters(1, 3);
                        ReadParameters(1, 4);

                        CH3flowtest = true;
                        CH4flowtest = true;
                        ch3pressstart = DateTime.Now.Ticks;
                        ch4pressstart = DateTime.Now.Ticks;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电流(RWD)"), "", "A", elec.CH2RWDADCMax.ToString(), elec.CH2RWDADCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "max电压(RWD)"), "", "V", elec.CH2RWDVDCMax.ToString(), elec.CH2RWDVDCMin.ToString(), "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD上)"), "", CH3PressureUnit.Text, "-", "-", "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "输出压力(RWD下)"), "", CH4PressureUnit.Text, "-", "-", "");
                        CH3ReadPress.Interval = 1300;
                        CH3ReadPress.Start();
                        CH4ReadPress.Interval = 1300;
                        CH4ReadPress.Start();
                        //plc.CH2PowerOpen();
                        plc.WriteCH2XQ(true);
                        break;

                    case "UPLeak":
                        plc.CH3valveclose();
                        plc.CH4valveclose();
                        System.Threading.Thread.Sleep(300);
                        //plc.CH2PowerOpen();
                        plc.CH2UPStart();//M2032 Ture
                        plc.CH2UPLeakTrue();//M4013 Ture
                        if (CH2Pump)
                        {
                            // if (!Electricity.ord.CH2UpDownChange)
                            if (!CH2change)
                                plc.WriteCH2SC(true);
                            else
                                plc.WriteCH2XC(true);

                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(3, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(4, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(6, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(8, 3);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(2, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(4, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(6, 3);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(8, 3);
                            }
                        }
                        CH3IsRun.Interval = 1300;
                        CH3IsRun.Start();
                        ch2_1step = 1;
                        CH4IsRun.Interval = 1300;
                        CH4IsRun.Start();
                        ch2_2step = 1;
                        //  ch2_2step = 5;   //5-27
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-1上充气密"), "", CH3LeakUnit.Text, ch2_1leakparams.Leaktoplimit, ch2_1leakparams.Leaklowlimit, "");
                        //plc.CH2PowerOpen();
                        break;

                    case "DOWNLeak":
                        plc.CH3valveclose();
                        plc.CH4valveclose();
                        System.Threading.Thread.Sleep(200);
                        plc.CH2DOWNStart();
                        plc.CH2DOWNLeak();

                        if (CH2Pump)
                        {
                            // if (!Electricity.ord.CH2UpDownChange)
                            if (!CH2change)
                                plc.WriteCH2XC(true);
                            else
                                plc.WriteCH2SC(true);

                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(3, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(5, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(7, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(9, 4);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(2, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(5, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(7, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(9, 4);
                            }
                        }
                        CH3IsRun.Interval = 1300;
                        CH3IsRun.Start();
                        ch2_1step = 1;//5-27
                        CH4IsRun.Interval = 1300;
                        CH4IsRun.Start();
                        ch2_2step = 1;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-2下充气密"), "", CH4LeakUnit.Text, ch2_2leakparams.Leaktoplimit, ch2_2leakparams.Leaklowlimit, "");
                        //plc.CH2PowerOpen();
                        break;

                    case "FWDLeak":
                        plc.CH3valveclose();
                        plc.CH4valveclose();
                        System.Threading.Thread.Sleep(300);
                        //plc.CH2PowerOpen();

                        plc.CH2FWDStart();
                        plc.CH2FWDLeakTrue();
                        if (CH2Pump)
                        {
                            plc.WriteCH2TC(true);
                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(3, 3);
                                ReadParameters(3, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(4, 3);
                                ReadParameters(5, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(6, 3);
                                ReadParameters(7, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(8, 3);
                                ReadParameters(9, 4);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(CH2RunName))
                            {
                                ReadParameters(2, 3);
                                ReadParameters(2, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "D通道")))
                            {
                                ReadParameters(4, 3);
                                ReadParameters(5, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "E通道")))
                            {
                                ReadParameters(6, 3);
                                ReadParameters(7, 4);
                            }
                            if (CH2RunName.Equals(I18N.GetLangText(dicLang, "F通道")))
                            {
                                ReadParameters(8, 3);
                                ReadParameters(9, 4);
                            }
                        }

                        CH3IsRun.Interval = 1300;
                        CH3IsRun.Start();
                        ch2_1step = 1;
                        CH4IsRun.Interval = 1300;
                        CH4IsRun.Start();
                        ch2_2step = 1;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-1同充气密"), "", CH3LeakUnit.Text, ch2_1leakparams.Leaktoplimit, Leaklowlimit, "");
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "CH2-2同充气密"), "", CH4LeakUnit.Text, ch2_2leakparams.Leaktoplimit, Leaklowlimit, "");
                        //plc.CH2PowerOpen();
                        break;

                    case "QC":
                        // Electricity静态电流
                        CH2ReaduA.Interval = 200;
                        CH2POWER.Write("OUTP 1");
                        plc.WriteCH2QC(true);
                        CH2ReaduA.Start();
                        ch2uAstarttime = System.DateTime.Now.Ticks;
                        CH2uAarray.Clear();
                        plc.CH2uA = 0;
                        CH3Display($"{CH2RunName}" + I18N.GetLangText(dicLang, "Electricity静态电流"), "", "uA", elec.CH2ElecMax.ToString(), elec.CH2ElecMin.ToString(), "");
                        break;
                }
                if (plc.CH2LIN)
                {
                    if (CH2OrderTemp[i] != "QC")
                    {
                        CH2LinUP.Interval = 102;
                        CH2LinUP.Start();
                    }
                    else
                        CH2LinUP.Stop();


                }
                if (CH2RTStep == "UP" || CH2RTStep == "DOWN" || CH2RTStep == "FWD")
                {
                    CH3ReadFlowT.Interval = 300;
                    CH3ReadFlowT.Start();
                    ch3fullstart = DateTime.Now.Ticks;
                    CH4ReadFlowT.Interval = 300;
                    CH4ReadFlowT.Start();
                    ch4fullstart = DateTime.Now.Ticks;
                }


                CH2ReadElec.Interval = 1000;
                CH2ReadElec.Start();
            }
            else
            {
                //步骤都成功完成，且后面没有步骤需要切换，则输出工位完成信号
                {
                    timerCH2CT.Stop();
                    CH2IsStart = false;
                    CH2POWER.Write("OUTP 0");
                    plc.WriteCH2RatioOK();
                }
                plc.CH2FlowEnd();
                CH2RTStep = "";
            }
        }

        /// <summary>
        /// 复位需要操作的步骤
        /// </summary>
        /// <param name="CH"></param>
        private void Reset(int CH)
        {
            if (CH == 1)
            {
                if (ch1client.IsConnect)
                {
                 //   CH1IsRun.Stop();
                    CHXProBarFlag[1] = 0;
                    //ProBarRun.Stop();
                    CH1IsRun.Interval = 400;
                    CH1IsRun.Start();
                    // ch1_1step = 5;
                    ch1write = 1;
                }
                if (ch2client.IsConnect)
                {
                    CH2IsRun.Stop();
                    CHXProBarFlag[2] = 0;
                    //ProBarRun.Stop();
                    CH2IsRun.Interval = 400;
                    CH2IsRun.Start();
                    //  ch1_2step = 5;
                    ch2write = 1;
                }
                CH1LinUP.Stop();
                CH1ReadFlowT.Stop();
                CH1ReadPress.Stop();
                CH2ReadFlowT.Stop();
                CH2ReadPress.Stop();
                JudgeCH1ADC = false;
                CH1Status.Text = I18N.GetLangText(dicLang, "待机");
                CH1Status.ForeColor = Color.Black;
                //LeftCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                //LeftCH1Status.ForeColor = Color.Black;
                //LeftCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                //LeftCH2Status.ForeColor = Color.Black;
                CH1Tlight.Text = "";
                left_CH1Code.Text = "";

                {
                    plc.WriteCH1SC(false);
                    plc.WriteCH1XC(false);
                    plc.WriteCH1TC(false);
                    plc.WriteCH1XQ(false);
                }
            }
            else if (CH == 2)
            {
                if (ch3client.IsConnect)
                {
                    CH3IsRun.Stop();
                    CHXProBarFlag[3] = 0;
                    //ProBarRun.Stop();
                    CH3IsRun.Interval = 400;
                    CH3IsRun.Start();
                    ch2_1step = 5;
                    ch3write = 1;
                }
                if (ch4client.IsConnect)
                {
                    CH4IsRun.Stop();
                    CHXProBarFlag[4] = 0;
                    //ProBarRun.Stop();
                    CH4IsRun.Interval = 400;
                    CH4IsRun.Start();
                    ch2_2step = 5;
                    ch4write = 1;
                }
                CH2LinUP.Stop();
                CH3ReadFlowT.Stop();
                CH4ReadFlowT.Stop();
                CH3ReadPress.Stop();
                CH4ReadPress.Stop();
                JudgeCH2ADC = false;
                CH2Status.Text = I18N.GetLangText(dicLang, "待机");
                CH2Status.ForeColor = Color.Black;

                //RightCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                //RightCH1Status.ForeColor = Color.Green;
                //RightCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                //RightCH2Status.ForeColor = Color.Green;

                CH2Tlight.Text = "";
                right_CH1Code.Text = "";

                {
                    plc.WriteCH2SC(false);
                    plc.WriteCH2XC(false);
                    plc.WriteCH2TC(false);
                    plc.WriteCH2XQ(false);
                }
            }
        }

        private List<int> chXstartflag = new List<int> { 0, 0, 0, 0, 0 };

        private void MachineStart_Tick(object sender, EventArgs e)
        {
            int a, b;
            b = 0;
            ///判断搜友状态位是否要关闭定时器
            for (a = 1; a <= 4; a++)
            {
                if (chXstartflag[a] == 1)
                    b = 1;
            }
            if (b == 0)
            {
                MachineStart.Stop();
                return;
            }
            if (chXstartflag[1] == 1)
            {
                chXstartflag[1] = 0;
                ch1client.btnSendData("01 05 00 00 FF 00");
                CH1IsRun.Interval = 1000;
                CH1IsRun.Start();
                ch1_1step = 1;
            }
            if (chXstartflag[2] == 1)
            {
                chXstartflag[2] = 0;
                ch2client.btnSendData("02 05 00 00 FF 00");
                CH2IsRun.Interval = 1000;
                CH2IsRun.Start();
                ch1_2step = 1;
            }
            if (chXstartflag[3] == 1)
            {
                chXstartflag[3] = 0;
                ch3client.btnSendData("03 05 00 00 FF 00");
                CH3IsRun.Interval = 1000;
                CH3IsRun.Start();
                ch2_1step = 1;
            }
            if (chXstartflag[4] == 1)
            {
                chXstartflag[4] = 0;
                ch4client.btnSendData("04 05 00 00 FF 00");
                CH4IsRun.Interval = 1000;
                CH4IsRun.Start();
                ch2_2step = 1;
            }
        }

        /// <summary>
        /// 测试过程中由于某一项NG导致测试终止,CH只有12
        /// </summary>
        private void FlowNG(int CH)
        {
            if (CH == 1)
            {
                CH1Tlight.Text = "NG";
                CH1Tlight.ForeColor = Color.Red;
                LeftCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                LeftCH1Status.ForeColor = Color.Black;
                LeftCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                LeftCH2Status.ForeColor = Color.Black;
                plc.CH1TCNG();
               // CH1IsRun.Stop();
             //  CH2IsRun.Stop();
                CHXProBarFlag[1] = 0;
                CHXProBarFlag[2] = 0;
                CH1ReadPress.Stop();
                CH2ReadPress.Stop();
                CH1ReadFlowT.Stop();
                CH2ReadFlowT.Stop();
                CH1LinUP.Stop();
                plc.CH1FlowEnd();
                CH1IsRun.Interval = 1000;
                ch1_1step = 5;//5-27
                CH2IsRun.Interval = 1000;
                // ch1_2step = 5;//5-27
                {
                    plc.WriteCH1SC(false);
                    plc.WriteCH1XC(false);
                    plc.WriteCH1TC(false);

                    plc.WriteCH1XQ(false);
                }
            }
            if (CH == 2)
            {
                CH2Tlight.Text = "NG";
                CH2Tlight.ForeColor = Color.Red;
                RightCH1Status.Text = I18N.GetLangText(dicLang, "待机");
                RightCH1Status.ForeColor = Color.Black;
                RightCH2Status.Text = I18N.GetLangText(dicLang, "待机");
                RightCH2Status.ForeColor = Color.Black;
                plc.CH2TCNG();
                CH3IsRun.Stop();
                CH4IsRun.Stop();
                CHXProBarFlag[3] = 0;
                CHXProBarFlag[4] = 0;
                CH3ReadPress.Stop();
                CH4ReadPress.Stop();
                CH3ReadFlowT.Stop();
                CH4ReadFlowT.Stop();
                CH2LinUP.Stop();
                plc.CH2FlowEnd();
                CH3IsRun.Interval = 1000;
                ch2_1step = 5;////5-27
                CH4IsRun.Interval = 1000;
                //  ch2_2step = 5;//5-27
                {
                    plc.WriteCH2SC(false);
                    plc.WriteCH2XC(false);
                    plc.WriteCH2TC(false);
                    plc.WriteCH2XQ(false);
                }
            }
        }

        private void uiNavBar1_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {
            if (!String.IsNullOrEmpty(Characters) && Characters != I18N.GetLangText(dicLang, "操作员"))
            {
                if (itemText == "基本设置" || itemText == "Basic Settings"|| itemText == "Configuración básica")
                {
                    Config c1 = new Config();
                    OpenForm(c1);
                }
                if (itemText == "测试参数" || itemText == "Testing Parameters"|| itemText == "Parámetros de prueba")
                {
                    Electricity elec = new Electricity();
                    OpenForm(elec);
                }
                if (itemText == "LIN设置" || itemText == "LIN"|| itemText == "LIN")
                {
                    LinConfig lin = new LinConfig();
                    OpenForm(lin);
                }
                if (itemText == "存储设置" || itemText == "Storage settings" || itemText == "Configuración de almacenamiento")
                {
                    Save save = new Save();
                    OpenForm(save);
                }
                if (itemText == "网口设置" || itemText == "Ethernet settings"|| itemText == "Configuración Ethernet")
                {
                    ConfigTCP tcp = new ConfigTCP();
                    OpenForm(tcp);
                }
                if (itemText == "串口设置" || itemText == "Serial port settings" || itemText == "Configuración del puerto serie")
                {
                    Port port = new Port();
                    OpenForm(port);
                }
                if (itemText == "权限设置" || itemText == "Permission setting" || itemText == "Configuración de permisos")
                {
                    UserManagement user = new UserManagement();
                    OpenForm(user);
                }
                if (itemText == "报警记录" || itemText == "Alarm record" || itemText == "Registro de alarma")
                {
                    Warning warn = new Warning();
                    OpenForm(warn);
                }

                if (itemText == "打开文件" || itemText == "Open File" || itemText == "Abrir archivo")
                {
                    //切换文件
                    DateZero();
                    DateZero2();

                    OpenMachineINI.CheckFileExists = true;
                    OpenMachineINI.Multiselect = false;
                    OpenMachineINI.CheckPathExists = true;
                    OpenMachineINI.DefaultExt = ".ini";
                    OpenMachineINI.Filter = "File(*.ini)|*.ini";
                    OpenMachineINI.ShowDialog();
                    string machine_path = OpenMachineINI.FileName;
                    if (!String.IsNullOrEmpty(machinepath) && (machine_path != "openFileDialog1"))
                    {
                        machine = OpenMachineINI.SafeFileName;
                        machinepath = OpenMachineINI.FileName;
                        CH1Order.Clear();
                        CH2Order.Clear();
                        //ReadMultimeterPort();
                        ReadFlow();
                        ReadLin();
                        ReadAllConfig();
                        Setup.PLCPress press = new Setup.PLCPress();
                        string dialog = Form1.f1.machine;
                        ConfigINI config = new ConfigINI("Model", dialog);
                        press.CH1Pressure = config.IniReadValue("PLC", "CH1Pressure");
                        press.CH2Pressure = config.IniReadValue("PLC", "CH2Pressure");
                        press.CH3Pressure = config.IniReadValue("PLC", "CH3Pressure");
                        press.CH4Pressure = config.IniReadValue("PLC", "CH4Pressure");
                        //press.CH1Vol = config.IniReadValue("PLC", "CH1Vol");
                        //press.CH2Vol = config.IniReadValue("PLC", "CH2Vol");
                        //press.CH1Elect = config.IniReadValue("PLC", "CH1Elect");
                        //press.CH2Elect = config.IniReadValue("PLC", "CH2Elect");
                        press.CKCH1Vol = config.IniReadValue("IPC", "CKCH1Vol");
                        press.CKCH2Vol = config.IniReadValue("IPC", "CKCH2Vol");
                        press.CKCH1Current = config.IniReadValue("IPC", "CKCH1Current");
                        press.CKCH2Current = config.IniReadValue("IPC", "CKCH2Current");

                        press.ChkPLCPress = Convert.ToBoolean(config.IniReadValue("PLC", "ChkPLCPress"));

  
                        short ch1pressure;
                        if (press.ChkPLCPress)
                        {
                            ch1pressure = Convert.ToInt16(Convert.ToDouble(press.CH1Pressure) * 98);
                        }
                        else
                        {
                            ch1pressure = Convert.ToInt16(Convert.ToDouble(press.CH1Pressure));
                        }
                        if (String.IsNullOrEmpty(press.CH1Pressure.Trim()))
                        {
                            //MessageBox.Show("调压阀压力数值需填写！");
                            Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                        }
                        else
                        {
                            if (ch1pressure > 100)
                            {
                                //MessageBox.Show("调压阀压力数值不可以超过100！");
                                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                            }
                            else
                            {
                                Form1.f1.plc.ch1pre = ch1pressure;
                                Form1.f1.plc.CH1Writevalve();
                                //WritePLCConfig();
                            }
                        }

                        short ch2pressure;
                        if (press.ChkPLCPress)
                        {
                            ch2pressure = Convert.ToInt16(Convert.ToDouble(press.CH2Pressure) * 98);
                        }
                        else
                        {
                            ch2pressure = Convert.ToInt16(Convert.ToDouble(press.CH2Pressure));
                        }
                        if (String.IsNullOrEmpty(press.CH2Pressure.Trim()))
                        {
                            // MessageBox.Show("调压阀压力数值需填写！");
                            Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                        }
                        else
                        {
                            if (ch2pressure > 100)
                            {
                                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                            }
                            else
                            {
                                Form1.f1.plc.ch2pre = ch2pressure;
                                Form1.f1.plc.CH2Writevalve();
                                //WritePLCConfig();
                            }
                        }

                        short ch3pressure;
                        if (press.ChkPLCPress)
                        {
                            ch3pressure = Convert.ToInt16(Convert.ToDouble(press.CH3Pressure) * 98);
                        }
                        else
                        {
                            ch3pressure = Convert.ToInt16(Convert.ToDouble(press.CH3Pressure));
                        }
                        if (String.IsNullOrEmpty(press.CH3Pressure.Trim()))
                        {
                            //MessageBox.Show("调压阀压力数值需填写！");
                            Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                        }
                        else
                        {
                            if (ch3pressure > 100)
                            {
                                //MessageBox.Show("调压阀压力数值不可以超过100！");
                                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                            }
                            else
                            {
                                Form1.f1.plc.ch3pre = ch3pressure;
                                Form1.f1.plc.CH3Writevalve();
                                //WritePLCConfig();
                            }
                        }

                        short ch4pressure;
                        if (press.ChkPLCPress)
                        {
                            ch4pressure = Convert.ToInt16(Convert.ToDouble(press.CH4Pressure) * 98);
                        }
                        else
                        {
                            ch4pressure = Convert.ToInt16(Convert.ToDouble(press.CH4Pressure));
                        }
                        if (String.IsNullOrEmpty(press.CH4Pressure.Trim()))
                        {
                            // MessageBox.Show("调压阀压力数值需填写！");
                            Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值需填写"));
                        }
                        else
                        {
                            if (ch4pressure > 100)
                            {
                                //MessageBox.Show("调压阀压力数值不可以超过100！");
                                Logger.Log(I18N.GetLangText(dicLang, "调压阀压力数值不可以超过100"));
                            }
                            else
                            {
                                Form1.f1.plc.ch4pre = ch4pressure;
                                Form1.f1.plc.CH4Writevalve();
                                //WritePLCConfig();
                            }
                        }
                        //  config.IniWriteValue("PLC", "ChkPLCPress", press.ChkPLCPress.Checked.ToString());
                        //ReadOrder();
                        //ReadSetting();
                    }
                }
                if (itemText == "左工位1通道自检" || itemText == "Left station channel 1 self check" || itemText == "Autoexamen del canal 1 de la estación izquierda")
                {
                    //ReadParameters(4, 1);
                    ReadParameters(14, 1);
                }
                if (itemText == "左工位2通道自检" || itemText == "Left station channel 2 self check" || itemText == "Autoexamen del canal 2 de la estación izquierda")
                {
                    //ReadParameters(4, 2);
                    ReadParameters(14, 2);
                }
                if (itemText == "右工位1通道自检" || itemText == "Right station channel 1 self check" || itemText == "Autoexamen del canal 1 de la estación derecha")
                {
                    //ReadParameters(4, 3);
                    ReadParameters(14, 3);
                }
                if (itemText == "右工位2通道自检" || itemText == "Right station channel 2 self check" || itemText == "Autoexamen del canal 2 de la estación derecha")
                {
                    //ReadParameters(4, 4);
                    ReadParameters(14, 4);
                }
            }
            if (itemText == "简体中文" || itemText == "Simplified Chinese" || itemText == "Chino simplificado")
            {
                I18N.Language = "zh-CN";
                dicLang = I18N.LoadLanguage(this);
                this.ChangeLanguage(I18N.Language);
                this.LoadZHCNLang();
                //LanguageHelper.SetLang("zh-CN", this, typeof(Form1));
                //setTag(this);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            if (itemText == "英语" || itemText == "English" || itemText == "Inglés")
            {
                I18N.Language = "en-Us";
                dicLang = I18N.LoadLanguage(this);
                this.ChangeLanguage(I18N.Language);
                //LanguageHelper.SetLang("en-Us", this, typeof(Form1));
                this.LoadENUSLang();
                //setTag(this);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }

            if (itemText == "墨西哥" || itemText == "English")
            {
                I18N.Language = "en-Es";
                dicLang = I18N.LoadLanguage(this);
                this.ChangeLanguage(I18N.Language);
                //LanguageHelper.SetLang("en-Us", this, typeof(Form1));
                this.LoadENUSLang();
                //setTag(this);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                this.WindowState = FormWindowState.Maximized;
            }
            if (itemText == "登录" || itemText == "Sign in"|| itemText == "Iniciar sesión")
            {
                LogOn l1 = new LogOn();
                OpenForm(l1);
            }
            if (itemText == "注销" || itemText == "Sign out" || itemText == "Salida")
            {
                Characters = "";
                Admin.Text = "";
            }
        }

        private void LoadENUSLang()
        {
            uiNavBar1.Nodes[0].Text = I18N.GetLangText(dicLang, "语言设置");
            uiNavBar1.Nodes[0].Nodes[0].Text = I18N.GetLangText(dicLang, "简体中文");
            uiNavBar1.Nodes[0].Nodes[1].Text = I18N.GetLangText(dicLang, "英语");

            uiNavBar1.Nodes[1].Text = I18N.GetLangText(dicLang, "参数设置");
            uiNavBar1.Nodes[1].Nodes[0].Text = I18N.GetLangText(dicLang, "基本设置");
            uiNavBar1.Nodes[1].Nodes[1].Text = I18N.GetLangText(dicLang, "测试参数");
            uiNavBar1.Nodes[1].Nodes[2].Text = I18N.GetLangText(dicLang, "LIN设置");
            uiNavBar1.Nodes[1].Nodes[3].Text = I18N.GetLangText(dicLang, "存储设置");

            uiNavBar1.Nodes[2].Text = I18N.GetLangText(dicLang, "通讯设置");
            uiNavBar1.Nodes[2].Nodes[0].Text = I18N.GetLangText(dicLang, "网口设置");
            uiNavBar1.Nodes[2].Nodes[1].Text = I18N.GetLangText(dicLang, "串口设置");

            uiNavBar1.Nodes[3].Text = I18N.GetLangText(dicLang, "用户操作");
            uiNavBar1.Nodes[3].Nodes[0].Text = I18N.GetLangText(dicLang, "登录");
            uiNavBar1.Nodes[3].Nodes[1].Text = I18N.GetLangText(dicLang, "注销");
            uiNavBar1.Nodes[3].Nodes[2].Text = I18N.GetLangText(dicLang, "权限设置");

            uiNavBar1.Nodes[4].Text = I18N.GetLangText(dicLang, "文件管理");
            uiNavBar1.Nodes[4].Nodes[0].Text = I18N.GetLangText(dicLang, "打开文件");
            uiNavBar1.Nodes[4].Nodes[1].Text = I18N.GetLangText(dicLang, "报警记录");

            uiNavBar1.Nodes[5].Text = I18N.GetLangText(dicLang, "自检启动");
            uiNavBar1.Nodes[5].Nodes[0].Text = I18N.GetLangText(dicLang, "左工位1通道自检");
            uiNavBar1.Nodes[5].Nodes[1].Text = I18N.GetLangText(dicLang, "左工位2通道自检");
            uiNavBar1.Nodes[5].Nodes[2].Text = I18N.GetLangText(dicLang, "右工位1通道自检");
            uiNavBar1.Nodes[5].Nodes[3].Text = I18N.GetLangText(dicLang, "右工位2通道自检");

            DataGridView1.Columns[0].HeaderCell.Value = I18N.GetLangText(dicLang, "时间");
            DataGridView1.Columns[1].HeaderCell.Value = I18N.GetLangText(dicLang, "测试项目");
            DataGridView1.Columns[2].HeaderCell.Value = I18N.GetLangText(dicLang, "测试数据");
            DataGridView1.Columns[3].HeaderCell.Value = I18N.GetLangText(dicLang, "单位");
            DataGridView1.Columns[4].HeaderCell.Value = I18N.GetLangText(dicLang, "上限");
            DataGridView1.Columns[5].HeaderCell.Value = I18N.GetLangText(dicLang, "下限");
            DataGridView1.Columns[6].HeaderCell.Value = I18N.GetLangText(dicLang, "结果");

            DataGridView2.Columns[0].HeaderCell.Value = I18N.GetLangText(dicLang, "时间");
            DataGridView2.Columns[1].HeaderCell.Value = I18N.GetLangText(dicLang, "测试项目");
            DataGridView2.Columns[2].HeaderCell.Value = I18N.GetLangText(dicLang, "测试数据");
            DataGridView2.Columns[3].HeaderCell.Value = I18N.GetLangText(dicLang, "单位");
            DataGridView2.Columns[4].HeaderCell.Value = I18N.GetLangText(dicLang, "上限");
            DataGridView2.Columns[5].HeaderCell.Value = I18N.GetLangText(dicLang, "下限");
            DataGridView2.Columns[6].HeaderCell.Value = I18N.GetLangText(dicLang, "结果");
        }

        //文件转换参数至0 避免重复写入
        public void DateZero()
        {
            CH1TestResult.UP_ADCMAX = 0;
            CH1TestResult.UP_VDCMAX = 0;
            CH1TestResult.UP_Pre = 0;
            CH1TestResult.UP_Flow = 0;
            CH1TestResult.DOWN_ADCMAX = 0;
            CH1TestResult.DOWN_VDCMAX = 0;
            CH1TestResult.DOWN_Pre = 0;
            CH1TestResult.DOWN_Flow = 0;
            CH1TestResult.ElecRatio = 0;
            CH1TestResult.PressRatio = 0;
            CH1TestResult.FWD_ADCMAX = 0;
            CH1TestResult.FWD_VDCMAX = 0;
            CH1TestResult.FWD_Pre1 = 0;
            CH1TestResult.FWD_Pre2 = 0;
            CH1TestResult.FWD_Flow1 = 0;
            CH1TestResult.FWD_Flow2 = 0;

            CH1TestResult.FWD_PreSumzuo = 0;
            CH1TestResult.FWD_FlowSumzuo = 0;
            CH1TestResult.DOWN_Flowzuo = 0;
            CH1TestResult.UP_Flowzuo = 0;

            CH1TestResult.RWD_ADCMAX = 0;
            CH1TestResult.RWD_VDCMAX = 0;

            CH1TestResult.FWD_FullPre1 = "0";
            CH1TestResult.BalanPre1 = "0";
            CH1TestResult.Leak1 = "0";
            CH1TestResult.FWD_FullPre2 = "0";
            CH1TestResult.BalanPre2 = "0";
            CH1TestResult.Leak2 = "0";
            CH1TestResult.FWD_FullPre1 = "0";
            CH1TestResult.FWD_BalanPre1 = "0";
            CH1TestResult.FWD_Leak1 = "0";

            CH1TestResult.FWD_FullPre2 = "0";
            CH1TestResult.FWD_BalanPre2 = "0";
            CH1TestResult.FWD_Leak2 = "0";

            CH1RTElec.Text = "";


            log.PLC_Logmsg(DateTime.Now.ToString() + "CH1启动前清空测试数据");
        }



        //文件转换参数至0 避免重复写入
        public  void DateZero2()
        {

     CH2TestResult.UP_ADCMAX = 0;
            CH2TestResult.UP_VDCMAX = 0;
            CH2TestResult.UP_Pre = 0;
            CH2TestResult.UP_Flow = 0;
            CH2TestResult.DOWN_ADCMAX = 0;
            CH2TestResult.DOWN_VDCMAX = 0;
            CH2TestResult.DOWN_Pre = 0;
            CH2TestResult.DOWN_Flow = 0;
            CH2TestResult.ElecRatio = 0;
            CH2TestResult.PressRatio = 0;
            CH2TestResult.FWD_ADCMAX = 0;
            CH2TestResult.FWD_VDCMAX = 0;
            CH2TestResult.FWD_Pre1 = 0;
            CH2TestResult.FWD_Pre2 = 0;
            CH2TestResult.FWD_Flow1 = 0;
            CH2TestResult.FWD_Flow2 = 0;

            CH2TestResult.FWD_PreSum = 0;
            CH2TestResult.FWD_FlowSum = 0;
            CH2TestResult.DOWN_Flowzuo = 0;
            CH2TestResult.UP_Flowzuo = 0;

            CH2TestResult.RWD_ADCMAX = 0;
            CH2TestResult.RWD_VDCMAX = 0;


            CH2TestResult.FWD_FullPre1 = "0";
            CH2TestResult.BalanPre1 = "0";
            CH2TestResult.Leak1 = "0";
            CH2TestResult.FWD_FullPre2 = "0";
            CH2TestResult.BalanPre2 = "0";
            CH2TestResult.Leak2 = "0";
            CH2TestResult.FWD_FullPre1 = "0";
            CH2TestResult.FWD_BalanPre1 = "0";
            CH2TestResult.FWD_Leak1 = "0";

            CH2TestResult.FWD_FullPre2 = "0";
            CH2TestResult.FWD_BalanPre2 = "0";
            CH2TestResult.FWD_Leak2 = "0";
            CH2RTElec.Text = "";

            log.PLC_Logmsg(DateTime.Now.ToString() + "CH2启动前清空测试数据");

        }

        private void LoadZHCNLang()
        {
            uiNavBar1.Nodes[0].Text = "语言设置";
            uiNavBar1.Nodes[0].Nodes[0].Text = "简体中文";
            uiNavBar1.Nodes[0].Nodes[1].Text = "英语";

            uiNavBar1.Nodes[1].Text = "参数设置";
            uiNavBar1.Nodes[1].Nodes[0].Text = "基本设置";
            uiNavBar1.Nodes[1].Nodes[1].Text = "测试参数";
            uiNavBar1.Nodes[1].Nodes[2].Text = "LIN设置";
            uiNavBar1.Nodes[1].Nodes[3].Text = "存储设置";

            uiNavBar1.Nodes[2].Text = "通讯设置";
            uiNavBar1.Nodes[2].Nodes[0].Text = "网口设置";
            uiNavBar1.Nodes[2].Nodes[1].Text = "串口设置";

            uiNavBar1.Nodes[3].Text = "用户操作";
            uiNavBar1.Nodes[3].Nodes[0].Text = "登录";
            uiNavBar1.Nodes[3].Nodes[1].Text = "注销";
            uiNavBar1.Nodes[3].Nodes[2].Text = "权限设置";

            uiNavBar1.Nodes[4].Text = "文件管理";
            uiNavBar1.Nodes[4].Nodes[0].Text = "打开文件";
            uiNavBar1.Nodes[4].Nodes[1].Text = "报警记录";

            uiNavBar1.Nodes[5].Text = "自检启动";
            uiNavBar1.Nodes[5].Nodes[0].Text = "左工位1通道自检";
            uiNavBar1.Nodes[5].Nodes[1].Text = "左工位2通道自检";
            uiNavBar1.Nodes[5].Nodes[2].Text = "右工位1通道自检";
            uiNavBar1.Nodes[5].Nodes[3].Text = "右工位2通道自检";

            DataGridView1.Columns[0].HeaderCell.Value = "时间";
            DataGridView1.Columns[1].HeaderCell.Value = "测试项目";
            DataGridView1.Columns[2].HeaderCell.Value = "测试数据";
            DataGridView1.Columns[3].HeaderCell.Value = "单位";
            DataGridView1.Columns[4].HeaderCell.Value = "上限";
            DataGridView1.Columns[5].HeaderCell.Value = "下限";
            DataGridView1.Columns[6].HeaderCell.Value = "结果";

            DataGridView2.Columns[0].HeaderCell.Value = "时间";
            DataGridView2.Columns[1].HeaderCell.Value = "测试项目";
            DataGridView2.Columns[2].HeaderCell.Value = "测试数据";
            DataGridView2.Columns[3].HeaderCell.Value = "单位";
            DataGridView2.Columns[4].HeaderCell.Value = "上限";
            DataGridView2.Columns[5].HeaderCell.Value = "下限";
            DataGridView2.Columns[6].HeaderCell.Value = "结果";
        }

        private void PLCControl_Click(object sender, EventArgs e)
        {
            //CH2ADC_PORT.Write(right_CH1Code.Text);
            PLCConfig plc = new PLCConfig();
            OpenForm(plc);
        }

        private void LeftReset_Click(object sender, EventArgs e)
        {
            //ConfigLogOn config = new ConfigLogOn(1);
            //OpenForm(config);
            Form1.f1.plc.CH1MachineReset();
        }

        private void RightReset_Click(object sender, EventArgs e)
        {
            //ConfigLogOn config = new ConfigLogOn(2);
            //OpenForm(config);
            Form1.f1.plc.CH2MachineReset();

            //ReadConfig con = new ReadConfig();
            //Model.CH_PARAMS ch_params;
            //ch_params = con.ReadParameters(1, 2);
            //string Leaklowlimit = ch_params.Leaklowlimit;
            //MessageBox.Show(Leaklowlimit);
        }

        #region 表格根据结果变色

        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < this.DataGridView1.Rows.Count - 1; i++)
            {
                if (this.DataGridView1.Rows[i].Cells["Column7"].Value.ToString() == "OK")
                {
                    this.DataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }
                if (this.DataGridView1.Rows[i].Cells["Column7"].Value.ToString() == "NG")
                {
                    this.DataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                if ((this.DataGridView1.Rows[i].Cells["Column7"].Value.ToString() != "NG") && (this.DataGridView1.Rows[i].Cells["Column7"].Value.ToString() != "OK"))
                {
                    this.DataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void DataGridView2_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < this.DataGridView2.Rows.Count - 1; i++)
            {
                if (this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn7"].Value.ToString() == "OK")
                {
                    this.DataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                }
                if (this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn7"].Value.ToString() == "NG")
                {
                    this.DataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                if ((this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn7"].Value.ToString() != "NG") && (this.DataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn7"].Value.ToString() != "OK"))
                {
                    this.DataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
            }
        }

        private void uiHeaderButton1_Click(object sender, EventArgs e)
        {
            DialogResult DR = MessageBox.Show(I18N.GetLangText(dicLang, "确认退出系统吗"), I18N.GetLangText(dicLang, "提示"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DR == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
        }

        #endregion 表格根据结果变色

        /// <summary>
        /// 将气密参数进行比较，看是否对参数有做修改，有的话则写入csv的报头
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <returns></returns>
        public bool Equals(Model.CH_PARAMS param1, Model.CH_PARAMS param2)
        {
            if (param1 == null || param2 == null)
                return false;
            if (param1.ParaName == param2.ParaName && param1.FullTime == param2.FullTime && param1.BalanTime == param2.BalanTime &&
                param1.TestTime1 == param2.TestTime1 && param1.ExhaustTime == param2.ExhaustTime && param1.FPtoplimit == param2.FPtoplimit &&
                param1.FPlowlimit == param2.FPlowlimit && param1.BalanPreMax == param2.BalanPreMax &&
                param1.BalanPreMin == param2.BalanPreMin && param1.Leaktoplimit == param2.Leaktoplimit && param1.Leaklowlimit == param2.Leaklowlimit &&
                param1.PUnit == param2.PUnit && param1.LUnit == param2.LUnit && param1.PUnit_index == param2.PUnit_index &&
                param1.LUnit_index == param2.LUnit_index && param1.progressBar_value == param2.progressBar_value &&
                param1.CHKUnit == param2.CHKUnit)
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int a = 0;
            if (a > 2)
            {
                winforclose.Stop();
            }
            a++;
            PostMessage(flagtime, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void CH1RTVDC_Click(object sender, EventArgs e)
        {
        }

        //防止打开多个相同的窗口
        public void OpenForm(System.Windows.Forms.Form frm)
        {
            if (frm == null) return;
            foreach (System.Windows.Forms.Form f in System.Windows.Forms.Application.OpenForms)
            {
                if (f.Name == frm.Name)
                {
                    f.Activate();
                    f.Show();
                    frm.Dispose();
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    return;
                }
            }
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frm.Show();
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

        private void ChangeLanguage(string language)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            ApplyResource();
            ApplyResource(this, GetCRMByLanguageName(typeof(Form1)));
        }

        private void ApplyResource()
        {
            System.ComponentModel.ComponentResourceManager res = new ComponentResourceManager(typeof(Form1));
            bool enable = false;
            foreach (Control ctl in Controls)
            {
                enable = ctl.Enabled;
                res.ApplyResources(ctl, ctl.Name);
                ctl.Enabled = enable;
            }

            this.ResumeLayout(false);
            this.PerformLayout();
            res.ApplyResources(this, "$this");
        }

        public void ApplyResource(Control ThisControls, ComponentResourceManager CRM_Language)
        {
            bool enable = false;
            foreach (Control ctl in ThisControls.Controls)
            {
                enable = ctl.Enabled;
                CRM_Language.ApplyResources(ctl, ctl.Name);
                ctl.Enabled = enable;
                if (ctl.HasChildren)
                {
                    ApplyResource(ctl, CRM_Language);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult DR = MessageBox.Show(I18N.GetLangText(dicLang, "确认退出系统吗"), I18N.GetLangText(dicLang, "提示"), MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DR == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
            else return;
        }

        private void label229_Click(object sender, EventArgs e)
        {
            AddCSV2(1);
        }

        private void CH1RTElec_Click(object sender, EventArgs e)
        {

        }

      

        private void label6_Click(object sender, EventArgs e)
        {
             
        }

        private void label35_Click(object sender, EventArgs e)
        {
            plc.WriteCH1QC(false);
        }

        public static ComponentResourceManager GetCRMByLanguageName(Type formclass)
        {
            ComponentResourceManager res = new ComponentResourceManager(formclass);
            return res;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            I18N.Language = "es-ES";
            dicLang = I18N.LoadLanguage(this);
            this.ChangeLanguage(I18N.Language);
            //LanguageHelper.SetLang("en-Us", this, typeof(Form1));
            this.LoadENUSLang();
            //setTag(this);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            this.WindowState = FormWindowState.Maximized;
        }

        private void uiGroupBox1_Click(object sender, EventArgs e)
        {

        }

     
    }

    public class ValueClass
    {
        public double Value { get; set; }
    }
}