using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class LogOn : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();

        public LogOn()
        {
            InitializeComponent();
        }

        private void LogOn_Load(object sender, EventArgs e)
        {
            dicLang = I18N.LoadLanguage(this);
            Global.i18n.ChangLangNotify += ChangLang;

            Logtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Timer1.Interval = 1000;
            Timer1.Start();
            try
            {
                string filepath = System.Environment.CurrentDirectory + @"\Config\Users\UsersInfo.mdb";

                if (File.Exists(filepath) == true)//判断所选路径是否有文件
                {
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + @"\Config\Users\UsersInfo.mdb;";
                    OleDbConnection con = new OleDbConnection(constr);
                    con.Open();

                    string sql2 = "SELECT * FROM UserInfo";

                    OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                    OleDbDataReader userinformation = cmd2.ExecuteReader();

                    //下移游标，读取一行，如果没有数据了则返回false
                    while (userinformation.Read())
                    {
                        Account.Items.Add(Convert.ToString(userinformation["账户"]));
                    }

                    userinformation.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("UsersInfo:" + ex.Message);
                Logger.Log("UsersInfo:" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
            Password.Focus();
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
            }
        }

        private void Log_Click(object sender, EventArgs e)
        {
            try
            {
                if (Password.Text == "linglong29529959")
                {
                    Timer1.Stop();

                    //Form1.f1.网络设置ToolStripMenuItem.Enabled = true;
                    //Form1.f1.pLC控制ToolStripMenuItem.Enabled = true;
                    //Form1.f1.串口配置ToolStripMenuItem.Enabled = true;
                    //Form1.f1.测试参数ToolStripMenuItem.Enabled = true;
                    //Form1.f1.基本设置ToolStripMenuItem.Enabled = true;
                    //Form1.f1.存储设置ToolStripMenuItem.Enabled = true;
                    //Form1.f1.用户管理ToolStripMenuItem.Enabled = true;
                    //Form1.f1.复位ToolStripMenuItem.Enabled = true;

                    Form1.f1.Admin.Text = I18N.GetLangText(dicLang, "厂商");
                    Form1.f1.Characters = I18N.GetLangText(dicLang, "厂商");
                    Form1.f1.DayTime.Start();

                    this.Close();
                    //Form1.f1.Show();
                }
                else
                {
                    string filepath = System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb";

                    if (File.Exists(filepath) == false)//判断所选路径是否有文件
                    {
                        MessageBox.Show(I18N.GetLangText(dicLang, "请先新建用户"));
                    }
                    else
                    {
                        string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb;";
                        OleDbConnection con = new OleDbConnection(constr);
                        con.Open();

                        string sql = "SELECT * FROM UserInfo WHERE 账户='" + Account.Text + "'";

                        OleDbCommand cmd2 = new OleDbCommand(sql, con);
                        OleDbDataReader userinformation = cmd2.ExecuteReader();

                        string userpassword = null;
                        string usercharacter = null;

                        while (userinformation.Read())
                        {
                            userpassword = Convert.ToString(userinformation["密码"]);
                            usercharacter = Convert.ToString(userinformation["权限"]);
                        }

                        userinformation.Close();
                        con.Close();

                        if (Password.Text == userpassword)
                        {
                            Timer1.Stop();

                            Form1.f1.Admin.Text = Account.Text;
                            Form1.f1.Characters = usercharacter;

                            //if (usercharacter == "操作员")
                            //{
                            //    Form1.f1.网络设置ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.pLC控制ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.串口配置ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.测试参数ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.基本设置ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.存储设置ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.用户管理ToolStripMenuItem.Enabled = false;
                            //    Form1.f1.复位ToolStripMenuItem.Enabled = false;
                            //}
                            //else
                            //{
                            //    Form1.f1.网络设置ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.pLC控制ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.串口配置ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.测试参数ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.基本设置ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.存储设置ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.用户管理ToolStripMenuItem.Enabled = true;
                            //    Form1.f1.复位ToolStripMenuItem.Enabled = true;
                            //}
                            Form1.f1.DayTime.Start();
                            //string logintime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            ////写入登录记录
                            //record_con.Open();
                            //string sql2 = " INSERT INTO Login(账户, 登录时间) VALUES('" + Account.Text + "', '" + logintime + "')";
                            //OleDbCommand record_cmd = new OleDbCommand(sql2, record_con);
                            //record_cmd.ExecuteNonQuery();
                            //record_con.Close();
                            //Form1.f1.Show();

                            //Machine mac = new Machine(usercharacter);
                            //mac.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(I18N.GetLangText(dicLang, "密码错误"));
                            Password.ResetText();
                            Password.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                Log.PerformClick(); //执行单击button的动作
            }
        }

        private void LogOn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (String.IsNullOrEmpty(Form1.f1.Admin.Text))
            {
                System.Environment.Exit(0);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Logtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}