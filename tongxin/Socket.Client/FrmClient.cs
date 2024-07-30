using NetWorkHelper;
using NetWorkHelper.TCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FrmClient : Form
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        SocketState _connectState { get; set; }

        /// <summary>
        /// 连接对象
        /// </summary>
        ITcpClient _client { get; set; }

        System.Timers.Timer _timer { get; set; }

        System.Timers.Timer _timerPone { get; set; }

        private const string pingPong = "客户端心跳包";


        public FrmClient()
        {
            InitializeComponent();
            _timerPone = new System.Timers.Timer();
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            _client = new ITcpClient();
            _client.IsReconnection = true;

            _client.OnStateInfo += Client_OnStateInfo;
            _client.OnRecevice += Client_OnRecevice;
            _client.OnErrorMsg += Client_OnErrorMsg;
            _timerPone.Elapsed += CloseConnect;
            _timerPone.Interval = 2000;
            _timerPone.AutoReset = false;

            txtSendData.Focus();
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendData("客户端心跳包：" + pingPong);
            _timerPone.Start();
        }

        /// <summary>
        /// 连接按钮事件
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!_client.IsStart)
            {
                if (chBoxIsHeartCheck.Checked)
                {
                    if (string.IsNullOrEmpty(txtInterval.Text.Trim()))
                    {
                        MessageBox.Show("心跳间隔不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int interval = int.Parse(txtInterval.Text.Trim());

                    _timer = new System.Timers.Timer();
                    _timer.Interval = 1000 * interval;
                    _timer.Elapsed += _timer_Elapsed;
                    _timer.Start();
                }

                _client.ServerIp = txtServerIp.Text.Trim();
                _client.ServerPort = int.Parse(txtServerPort.Text.Trim());

                _client.StartConnect();
            }
        }

        /// <summary>
        /// 连接错误
        /// </summary>
        private void Client_OnErrorMsg(object sender, NetWorkHelper.ICommond.TcpClientErrorEventArgs e)
        {
            txtMessage.AppendText(e.ErrorMsg + Environment.NewLine);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void Client_OnRecevice(object sender, NetWorkHelper.ICommond.TcpClientReceviceEventArgs e)
        {
            string receviceData = Encoding.Default.GetString(e.Data);

            if (receviceData == pingPong)
            {
                _timerPone.Stop();
            }
            else
            {
                txtReceviceData.AppendText(receviceData + Environment.NewLine);
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        private void Client_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpClientStateEventArgs e)
        {
            _connectState = e.State;

            txtMessage.AppendText(e.StateInfo + Environment.NewLine);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        private void btnSendData_Click(object sender, EventArgs e)
        {
            SendData(txtSendData.Text.Trim());
        }

        void SendData(string data)
        {
            byte[] sendData = Encoding.Default.GetBytes(data);
            if (this.InvokeRequired)
            {
                this.Invoke((Action)delegate ()
                {
                    _client.SendData(sendData);
                    txtSendData.Text = string.Empty;
                });
            }
            else
            {
                _client.SendData(sendData);
                txtSendData.Text = string.Empty;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void ClientClose_Click(object sender, EventArgs e)
        {
            if (_client.IsStart)
            {
                _client.StopConnect();
                _client.Dispose();
            }
        }

        private void CloseConnect(object body, EventArgs e)
        { 

            _client.StopConnect();
            _client.StartConnect();
        }
    }
}
