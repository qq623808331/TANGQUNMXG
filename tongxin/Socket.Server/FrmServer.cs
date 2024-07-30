using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetWorkHelper.IModels;
using NetWorkHelper.TCP;

namespace Socket.Server
{
    public partial class FrmServer : Form
    {
        /// <summary>
        /// 服务端通讯组件
        /// </summary>
        ITcpServer _server { get; set; }

        /// <summary>
        /// 客户端列表
        /// </summary>
        List<IClient> _clientSocketList { get; set; }

        public FrmServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void FrmServer_Load(object sender, EventArgs e)
        {
            _server = new ITcpServer();

            _server.OnErrorMsg += _server_OnErrorMsg;
            _server.OnGetLog += _server_OnGetLog;
            _server.OnOfflineClient += _server_OnOfflineClient;
            _server.OnOnlineClient += _server_OnOnlineClient;
            _server.OnRecevice += _server_OnRecevice;
            _server.OnReturnClientCount += _server_OnReturnClientCount;
            _server.OnSendDateSuccess += _server_OnSendDateSuccess;
            _server.OnStateInfo += _server_OnStateInfo;

            _clientSocketList = new List<IClient>();


        }

        /// <summary>
        /// 监听状态改变时返回监听状态事件
        /// </summary>
        private void _server_OnStateInfo(object sender, NetWorkHelper.ICommond.TcpServerStateEventArgs e)
        {
            txtMessage.AppendText(e.Msg + Environment.NewLine);
        }

        /// <summary>
        /// 发送消息成功时返回成功消息事件
        /// </summary>
        private void _server_OnSendDateSuccess(object sender, NetWorkHelper.ICommond.TcpServerSendReturnEventArgs e)
        {

        }

        /// <summary>
        /// 用户上线下线时更新客户端在线数量事件
        /// </summary>
        private void _server_OnReturnClientCount(object sender, NetWorkHelper.ICommond.TcpServerReturnClientCountEventArgs e)
        {
            lblClientCount.Text = e.ClientCount.ToString();
        }

        /// <summary>
        /// 接收数据事件
        /// </summary>
        private void _server_OnRecevice(object sender, NetWorkHelper.ICommond.TcpServerReceviceaEventArgs e)
        {
            string receviceData = Encoding.Default.GetString(e.Data);
            txtReceviceData.AppendText(receviceData + Environment.NewLine);
        }

        /// <summary>
        /// 新客户端上线时返回客户端事件
        /// </summary>
        private void _server_OnOnlineClient(object sender, NetWorkHelper.ICommond.TcpServerClientEventArgs e)
        {
            ChangeClientList(e.IClient, true);
        }

        /// <summary>
        /// 改变客户端列表
        /// </summary>
        /// <param name="client"></param>
        void ChangeClientList(IClient client, bool isAdd)
        {
            //新增
            if (isAdd)
            {
                //是否存在于队列中
                bool state = _clientSocketList.Exists(p => p.Ip == client.Ip);

                if (!state)
                {
                    _clientSocketList.Add(client);
                    DataGridViewRow dr = new DataGridViewRow();

                    dr.CreateCells(dgvClientView);

                    //设置列的值
                    dr.Cells[0].Value = dgvClientView.Rows.Count;
                    dr.Cells[1].Value = client.Ip;
                    dr.Cells[2].Value = client.Port;
                    dr.Cells[3].Value = client.ClientInfo.SitState.ToString();

                    dr.Tag = client.Ip;

                    dgvClientView.Rows.Add(dr);
                }
            }
            else
            {
                var removeList = new List<DataGridViewRow>();

                foreach (DataGridViewRow row in dgvClientView.Rows)
                {
                    var rowClient = (string)row.Tag;

                    if (rowClient == client.Ip)
                    {
                        removeList.Add(row);

                        _clientSocketList.RemoveAll(p => p.Ip == rowClient);
                    }
                }

                //删除列表，避免删除导致的索引错误，另起删除循环
                foreach (var row in removeList)
                {
                    dgvClientView.Rows.Remove(row);
                }
            }
        }

        /// <summary>
        /// 客户端下线时返回客户端事件
        /// </summary>
        private void _server_OnOfflineClient(object sender, NetWorkHelper.ICommond.TcpServerClientEventArgs e)
        {
            ChangeClientList(e.IClient, false);
        }

        /// <summary>
        /// 服务端读写操作时返回日志消息
        /// </summary>
        private void _server_OnGetLog(object sender, NetWorkHelper.ICommond.TcpServerLogEventArgs e)
        {
            txtMessage.AppendText(e.LogMsg + Environment.NewLine);
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        private void _server_OnErrorMsg(object sender, NetWorkHelper.ICommond.TcpServerErrorEventArgs e)
        {
            txtMessage.AppendText(e.ErrorMsg + Environment.NewLine);
        }

        /// <summary>
        /// 是否开启心跳检测
        /// </summary>
        private void chBoxIsHeartCheck_CheckedChanged(object sender, EventArgs e)
        {
            _server.IsHeartCheck = chBoxIsHeartCheck.Checked;
        }

        /// <summary>
        /// 开启监听
        /// </summary>
        private void btnListeningStart_Click(object sender, EventArgs e)
        {
            if (!_server.IsStartListening)
            {
                if (string.IsNullOrEmpty(txtServerIp.Text) || string.IsNullOrEmpty(txtServerPort.Text) || string.IsNullOrEmpty(txtInterval.Text))
                {
                    MessageBox.Show("参数不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _server.CheckTime = int.Parse(txtInterval.Text.Trim());
                _server.ServerIp = txtServerIp.Text.Trim();
                _server.ServerPort = int.Parse(txtServerPort.Text.Trim());

                _server.Start();

                btnListeningStart.Enabled = false;
                btnListeningStop.Enabled = true;
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        private void btnListeningStop_Click(object sender, EventArgs e)
        {
            if (_server.IsStartListening)
            {
                _server.Stop();

                btnListeningStart.Enabled = true;
                btnListeningStop.Enabled = false;
            }
        }

        /// <summary>
        /// 发送数据按钮
        /// </summary>
        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSendData.Text.Trim()))
            {
                byte[] sendData = Encoding.Default.GetBytes(txtSendData.Text.Trim());

                var selectedList = dgvClientView.SelectedRows;

                if (selectedList.Count == 0)
                {
                    MessageBox.Show("请选择要发送的客户端！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                foreach (DataGridViewRow row in selectedList)
                {
                    var rowIp = (string)row.Tag;

                    var rowClient = _clientSocketList.Find(p => p.Ip == rowIp);

                    //发送数据
                    _server.SendData(rowClient, sendData);
                }

                txtSendData.Text = string.Empty;
            }
        }
    }
}
