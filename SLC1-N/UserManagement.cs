using ADOX;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class UserManagement : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        public UserManagement()
        {
            InitializeComponent();
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            try
            {
                dicLang = I18N.LoadLanguage(this);
                Global.i18n.ChangLangNotify += ChangLang;

                Characters.Items.Add(I18N.GetLangText(dicLang, "操作员"));
                Characters.Items.Add(I18N.GetLangText(dicLang, "技术员"));

                Characters.SelectedIndex = 0;
                string filepath = System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb";

                if (File.Exists(filepath) == true)//判断所选路径是否有文件
                {
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb;";
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
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangLang()
        {
            if (Global.i18n.ChangLangNotify != null)
            {
                dicLang = I18N.LoadLanguage(this);
                Characters.Items.Clear();
                Characters.Items.Add(I18N.GetLangText(dicLang, "操作员"));
                Characters.Items.Add(I18N.GetLangText(dicLang, "技术员"));
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (Password.TextLength > 0)
            {
                if (Characters.Text == I18N.GetLangText(dicLang, "操作员") ||
                   Characters.Text == I18N.GetLangText(dicLang, "技术员"))
                {
                    try
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

                            string sql2 = " UPDATE UserInfo SET 密码='" + Password.Text + "' where 账户='" + Account.Text + "'";

                            OleDbCommand cmd2 = new OleDbCommand(sql2, con);

                            cmd2.ExecuteNonQuery();

                            string sql3 = " UPDATE UserInfo SET 权限='" + Characters.Text + "' WHERE 账户='" + Account.Text + "'";

                            OleDbCommand cmd3 = new OleDbCommand(sql3, con);

                            cmd3.ExecuteNonQuery();

                            con.Close();

                            MessageBox.Show(I18N.GetLangText(dicLang, "修改成功"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "权限输入非法"));
                }
            }
            else
            {
                MessageBox.Show(I18N.GetLangText(dicLang, "密码输入非法"));
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (Password.TextLength > 0)
            {
                if ( Characters.Text==I18N.GetLangText(dicLang, "操作员")||
                    Characters.Text==I18N.GetLangText(dicLang, "技术员"))
                {
                    Boolean add = true;

                    //遍历账户，查看是否有重复的账号
                    foreach (string oldaccount in Account.Items)
                    {
                        if (Account.Text == oldaccount)
                        {
                            MessageBox.Show(I18N.GetLangText(dicLang, "账户名称已存在"));
                            add = false;
                        }
                    }

                    if (add is true)
                    {
                        try
                        {
                            string filepath = System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb";

                            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb;";
                            OleDbConnection con = new OleDbConnection(constr);

                            if (File.Exists(filepath) == false)//判断所选路径是否有文件
                            {
                                string filepath2 = System.Environment.CurrentDirectory + "\\Config\\Users\\";
                                Directory.CreateDirectory(filepath2);//新建文件夹

                                Catalog Product = new Catalog();
                                Product.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb;Jet OLEDB:Engine Type=5;");

                                con.Open();

                                string sql = "CREATE TABLE UserInfo ([账户] VarChar(50),[密码] VarChar(50),[权限] VarChar(50))";
                                OleDbCommand cmd = new OleDbCommand(sql, con);

                                cmd.ExecuteNonQuery();

                                con.Close();
                            }

                            con.Open();

                            string sql2 = " INSERT INTO UserInfo(账户, 密码, 权限) VALUES('" + Account.Text + "', '" + Password.Text + "', '" + Characters.Text + "')";

                            OleDbCommand cmd2 = new OleDbCommand(sql2, con);

                            cmd2.ExecuteNonQuery();

                            con.Close();

                            Account.Items.Add(Account.Text);

                            MessageBox.Show(I18N.GetLangText(dicLang, "新建用户成功"));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "权限输入非法"));
                }
            }
            else
            {
                MessageBox.Show(I18N.GetLangText(dicLang, "密码输入非法"));
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Account.Text.Length > 0)
                {
                    Logger.Log(I18N.GetLangText(dicLang, "确认删除此用户"));
                    DialogResult delete = MessageBox.Show(I18N.GetLangText(dicLang, "确认删除此用户"), I18N.GetLangText(dicLang, "删除用户"), MessageBoxButtons.OKCancel);

                    if (delete == DialogResult.OK)
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

                            string sql2 = "DELETE FROM UserInfo WHERE 账户='" + Account.Text + "'";

                            OleDbCommand cmd2 = new OleDbCommand(sql2, con);

                            cmd2.ExecuteNonQuery();

                            con.Close();

                            Account.Items.Remove(Account.Text);
                            Account.Text = "";
                            Password.Text = "";
                            Characters.SelectedIndex = 0;

                            MessageBox.Show(I18N.GetLangText(dicLang, "删除用户成功"));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "请选择需要被删除的账户"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Account_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string filepath = System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb";

                if (File.Exists(filepath) == false)//判断所选路径是否有文件
                {
                    MessageBox.Show( I18N.GetLangText(dicLang, "路径下不存在用户信息文件"));
                }
                else
                {
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Users\\UsersInfo.mdb;";
                    OleDbConnection con = new OleDbConnection(constr);
                    con.Open();

                    string sql2 = "SELECT * FROM UserInfo WHERE 账户='" + Account.Text + "'";

                    OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                    OleDbDataReader userinformation = cmd2.ExecuteReader();

                    while (userinformation.Read())
                    {
                        Account.Text = Convert.ToString(userinformation["账户"]);
                        Password.Text = Convert.ToString(userinformation["密码"]);
                        Characters.Text = I18N.GetLangText(dicLang, Convert.ToString(userinformation["权限"]));
                    }

                    userinformation.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Characters_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void UpdateReset_Click(object sender, EventArgs e)
        {
            UserPassword u1 = new UserPassword();
            OpenForm(u1);
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
    }
}