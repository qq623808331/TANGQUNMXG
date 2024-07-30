using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class LogDisplay : UserControl
    {
        public LogDisplay()
        {
            InitializeComponent();
           	Logger.bind(this.listBox1);
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            	this.listBox1.Items.Clear();
        }
    }
}
