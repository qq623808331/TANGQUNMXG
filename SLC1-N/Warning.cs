using System;
using System.Windows.Forms;

namespace SLC1_N
{
    public partial class Warning : Form
    {
        //public static Warning wa;
        private float X, Y;

        public Warning()
        {
            InitializeComponent();
            //wa = this;
        }

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

        private void SelectWarning_Resize(object sender, EventArgs e)
        {
            // throw new Exception("The method or operation is not implemented.");
            float newx = (this.Width) / X;
            // float newy = (this.Height - this.statusStrip1.Height) / (Y - y);
            float newy = this.Height / Y;
            setControls(newx, newy, this);
            //      this.Text = this.Width.ToString() + " " + this.Height.ToString();
        }

        private void Warning_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(SelectWarning_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            try
            {
                DataGridView1.DataSource = WarningInfo.SelectWarning();
            }
            catch (Exception ex)
            {
                Logger.Log("WarningInfo:" + ex.Message);
                Logger.Log("WarningInfo:" + ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void Check_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView1.DataSource = WarningInfo.SelectWarning();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}