using ADOX;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;

namespace SLC1_N
{
    internal class WarningInfo
    {
        public class WarningRecord
        {
            public string Time { get; set; }

            public string Channel { get; set; }

            public string Warning { get; set; }
        }

        //将报警和解锁结果数据存储本地mdb数据库
        public void InsertWarningData(string warningtime, string channel, string warning)
        {
            try
            {
                string filepath = System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb";
                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb;";
                OleDbConnection con = new OleDbConnection(constr);

                if (File.Exists(filepath) == false)//判断所选路径是否有文件
                {
                    string filepath2 = System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\";
                    Directory.CreateDirectory(filepath2);//新建文件夹
                    Catalog Product = new Catalog();
                    Product.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb;Jet OLEDB:Engine Type=5;");
                    con.Open();
                    string sql = "CREATE TABLE Warning([id] int identity(1,1),[Warningtime] VarChar(100),[Channel] VarChar(50),[Warning] VarChar(100))";
                    OleDbCommand cmd = new OleDbCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                con.Open();
                //string sql2 = " INSERT INTO Warning( Warningtime, Channel, Warning) VALUES('" + warningtime + "', '" + channel + "', '" + warning + "')";
                //OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                OleDbCommand command = new OleDbCommand("INSERT INTO Warning( Warningtime, Channel, Warning) VALUES(?, ?, ?)", con);
                OleDbParameterCollection paramCollection = command.Parameters;
                paramCollection.Add(new OleDbParameter("Warningtime", warningtime));
                paramCollection.Add(new OleDbParameter("Channel", channel));
                paramCollection.Add(new OleDbParameter("Warning", warning));
                command.ExecuteNonQuery();
                string sql2 = "DELETE FROM Warning WHERE ID NOT IN(SELECT TOP 50 ID FROM Warning ORDER BY ID DESC)";
                OleDbCommand cmd3 = new OleDbCommand(sql2, con);
                cmd3.ExecuteNonQuery();
                con.Close();

                //    return "OK";
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
        }

        //public static string SelectWarningByTime(string begintime, string endtime)
        //{
        //    try
        //    {
        //        //定义一个集合，用于存放对象
        //        List<WarningRecord> Record = new List<WarningRecord>();

        //        string filepath = System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb";

        //        if (File.Exists(filepath) == true)//判断所选路径是否有文件
        //        {
        //            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb;";
        //            OleDbConnection con = new OleDbConnection(constr);
        //            con.Open();

        //            string sql2 = "SELECT * FROM Warning WHERE Unlocktime BETWEEN  '" + begintime + "' and '" + endtime + "'";

        //            OleDbCommand cmd2 = new OleDbCommand(sql2, con);
        //            OleDbDataReader information = cmd2.ExecuteReader();

        //            //下移游标，读取一行，如果没有数据了则返回false
        //            while (information.Read())
        //            {
        //                WarningRecord rec = new WarningRecord();

        //                rec.报警时间 = Convert.ToString(information["Warningtime"]);
        //                rec.通道 = Convert.ToString(information["Channel"]);
        //                rec.报警内容 = Convert.ToString(information["Warning"]);

        //                Record.Add(rec);
        //            }

        //            SelectWarning.sw.DataGridView1.DataSource = Record;
        //            information.Close();
        //            con.Close();
        //        }

        //        return "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public static List<WarningRecord> SelectWarning()
        {
            //try
            //{
            //定义一个集合，用于存放对象
            List<WarningRecord> Record = new List<WarningRecord>();
            string filepath = System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb";
            if (File.Exists(filepath) == true)//判断所选路径是否有文件
            {
                string constr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + System.Environment.CurrentDirectory + "\\Config\\WarningRecord\\Warning.mdb;";
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                //string sql1= "Delete FROM Warning Where Warningtime< (DateDiff('d',Warningtime,Date())=25)";
                string sql1 = "Delete FROM Warning Where   DateDiff('d',Warningtime,Now()) >30";
                OleDbCommand comm = new OleDbCommand(sql1, con);
                comm.ExecuteNonQuery();
                con.Close();
                con.Open();
                string sql2 = "SELECT TOP 1000 *  FROM Warning";
                OleDbCommand cmd2 = new OleDbCommand(sql2, con);
                OleDbDataReader information = cmd2.ExecuteReader();
                //下移游标，读取一行，如果没有数据了则返回false
                while (information.Read())
                {
                    WarningRecord rec = new WarningRecord();
                    rec.Time = Convert.ToString(information["Warningtime"]);
                    rec.Channel = Convert.ToString(information["Channel"]);
                    rec.Warning = Convert.ToString(information["Warning"]);
                    Record.Add(rec);
                }
                //Warning.wa.DataGridView1.DataSource = Record;
                information.Close();
                con.Close();
            }
            return Record;
            //    return "OK";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message;
            //}
        }
    }
}