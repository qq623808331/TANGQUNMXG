using ADOX;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Machine : Form
    {
        private Dictionary<string, string[]> dicLang = new Dictionary<string, string[]>();
        private string identity;

        public Machine(string character)
        {
            InitializeComponent();
            identity = character;
        }

        private void Machine_Load(object sender, EventArgs e)
        {
            try
            {
                dicLang = I18N.LoadLanguage(this);
                Global.i18n.ChangLangNotify += ChangLang;

                if (identity == "操作员")
                {
                    Add.Enabled = false;
                    //Update.Enabled = false;
                    Delete.Enabled = false;
                }

                string filepath = System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb";

                if (File.Exists(filepath) == true)//判断所选路径是否有文件
                {
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb;";
                    OleDbConnection con = new OleDbConnection(constr);
                    con.Open();
                    string sql2 = "SELECT * FROM MachineType";
                    OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                    OleDbDataReader information = cmd2.ExecuteReader();
                    //下移游标，读取一行，如果没有数据了则返回false
                    while (information.Read())
                    {
                        MachineType.Items.Add(Convert.ToString(information["Machine"]));
                    }

                    information.Close();
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
            }
        }

        private void Define_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb";

                if (File.Exists(filepath) == false)//判断所选路径是否有文件
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "请先新建机种"));
                }
                else
                {
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb;";
                    OleDbConnection con = new OleDbConnection(constr);
                    con.Open();

                    string sql = "SELECT * FROM MachineType WHERE Machine='" + MachineType.Text + "'";

                    OleDbCommand cmd2 = new OleDbCommand(sql, con);
                    object result = cmd2.ExecuteScalar();
                    con.Close();

                    if (result != null)
                    {
                        Form1.f1.machine = MachineType.Text;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(I18N.GetLangText(dicLang, "机种不存在"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(MachineType.Text))
            {
                Boolean add = true;

                //遍历账户，查看是否有重复的账号
                foreach (string oldmachine in MachineType.Items)
                {
                    if (MachineType.Text == oldmachine)
                    {
                        MessageBox.Show(I18N.GetLangText(dicLang, "机种名称已存在"));
                        add = false;
                    }
                }
                if (add)
                {
                    try
                    {
                        string filepath = System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb";

                        string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb;";
                        OleDbConnection con = new OleDbConnection(constr);
                        if (File.Exists(filepath) == false)//判断所选路径是否有文件
                        {
                            string filepath2 = System.Environment.CurrentDirectory + "\\Config\\Machine\\";
                            Directory.CreateDirectory(filepath2);//新建文件夹

                            Catalog Product = new Catalog();
                            Product.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb;Jet OLEDB:Engine Type=5;");

                            con.Open();

                            string sql = "CREATE TABLE MachineType([Machine] VarChar(100))";
                            OleDbCommand cmd = new OleDbCommand(sql, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        con.Open();
                        string sql2 = " INSERT INTO MachineType(Machine) VALUES('" + MachineType.Text + "')";

                        OleDbCommand cmd2 = new OleDbCommand(sql2, con);

                        cmd2.ExecuteNonQuery();

                        con.Close();
                        MachineType.Items.Add(MachineType.Text);
                        MessageBox.Show(I18N.GetLangText(dicLang, "新建机种成功"));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show(I18N.GetLangText(dicLang, "机种名称输入非法"));
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MachineType.Text.Length > 0)
                {
                    // Logger.Log("确认删除此机种？所有配置会一起删除! 删除机种");
                    DialogResult delete = MessageBox.Show(I18N.GetLangText(dicLang, "确认删除此机种？所有配置会一起删除"), I18N.GetLangText(dicLang, "删除机种"), MessageBoxButtons.OKCancel);

                    if (delete == DialogResult.OK)
                    {
                        string filepath = System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb";

                        if (File.Exists(filepath) == false)//判断所选路径是否有文件
                        {
                            MessageBox.Show("请先新建用户");
                        }
                        else
                        {
                            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\Machine\\MachineType.mdb;";
                            OleDbConnection con = new OleDbConnection(constr);
                            con.Open();
                            string sql2 = "DELETE FROM MachineType WHERE Machine='" + MachineType.Text + "'";
                            OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                            cmd2.ExecuteNonQuery();
                            con.Close();
                            //删除相关配置
                            RegistryKey regName;
                            regName = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine, true);
                            if (regName != null)
                            {
                                Registry.CurrentUser.DeleteSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Set\\" + Form1.f1.machine);
                                Registry.CurrentUser.DeleteSubKey("SOFTWARE\\Microsoft\\PMD\\1.0\\User-LL28-Param\\" + Form1.f1.machine);
                            }
                            Directory.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "Config\\" + MachineType.Text, true);

                            MachineType.Items.Remove(MachineType.Text);
                            MessageBox.Show(I18N.GetLangText(dicLang, "删除机种名称成功"));
                        }
                    }
                }
                else
                {
                    MessageBox.Show(I18N.GetLangText(dicLang, "请选择需要被删除的机种名称"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Machine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (String.IsNullOrEmpty(Form1.f1.machine))
            {
                System.Environment.Exit(0);
            }
        }

        private void MachineType_KeyDown(object sender, KeyEventArgs e)
        {
            if (identity == "操作员")
            {
                e.Handled = true;
            }
        }
    }
}