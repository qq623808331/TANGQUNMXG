using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Mes : Form
    {
        public Mes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dialog = "mesLog";
            ConfigINI config = new ConfigINI("Model", dialog);
            config.IniWriteValue("TQmes", "URL", this.URL.Text);
            config.IniWriteValue("TQmes", "inbordURL", this.inbordURL.Text);
            config.IniWriteValue("TQmes", "yanzhengUrl", this.yanzhengUrl.Text);
            config.IniWriteValue("TQmes", "LineNum", this.LineNum.Text);
            config.IniWriteValue("TQmes", "CommandCard", this.CommandCard.Text);
            Form1.f1.ZhiLing.Text=CommandCard.Text;
            config.IniWriteValue("TQmes", "DeviceCode", this.DeviceCode.Text);
            config.IniWriteValue("TQmes", "ProcessCode", this.ProcessCode.Text);
            config.IniWriteValue("TQmes", "LineCode", this.LineCode.Text);

            config.IniWriteValue("TQmes", "NGCode", this.NGCode.Text);
            config.IniWriteValue("TQmes", "CheckBy", this.CheckBy.Text);

            config.IniWriteValue("TQmes", "IsOnlyRecord", this.IsOnlyRecord.Text);
            Form1.URL = this.URL.Text;
            Form1.inbordURL = this.inbordURL.Text;
            Form1.yanzhengUrl = this.yanzhengUrl.Text;


            Form1.LineNum = this.LineNum.Text;
            Form1.CommandCard = this.CommandCard.Text;
            Form1.NGCode = NGCode.Text;
            Form1.DeviceCode = this.DeviceCode.Text;
            Form1.ProcessCode = this.ProcessCode.Text;
            Form1.LineCode = this.LineCode.Text;
            Form1.CheckBy = this.CheckBy.Text;
            Form1.IsOnlyRecord = Convert.ToInt32(this.IsOnlyRecord.Text);
            base.Close();
        }

        private void Mes_Load(object sender, EventArgs e)
        {
            string dialog = "mesLog";
            ConfigINI config = new ConfigINI("Model", dialog);
            this.URL.Text = config.IniReadValue("TQmes", "URL");
            this.inbordURL.Text = config.IniReadValue("TQmes", "inbordURL");
            this.yanzhengUrl.Text = config.IniReadValue("TQmes", "yanzhengUrl");
            this.LineNum.Text = config.IniReadValue("TQmes", "LineNum");
            this.CommandCard.Text = config.IniReadValue("TQmes", "CommandCard");
            this.DeviceCode.Text = config.IniReadValue("TQmes", "DeviceCode");
            this.ProcessCode.Text = config.IniReadValue("TQmes", "ProcessCode");
            this.LineCode.Text = config.IniReadValue("TQmes", "LineCode");
            this.CheckBy.Text = config.IniReadValue("TQmes", "CheckBy");
            this.NGCode.Text = config.IniReadValue("TQmes", "NGCode");
            this.IsOnlyRecord.Text = config.IniReadValue("TQmes", "IsOnlyRecord");
        }
    }
}
