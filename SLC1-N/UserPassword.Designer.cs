namespace SLC1_N
{
    partial class UserPassword
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserPassword));
            this.label3 = new System.Windows.Forms.Label();
            this.New_Pwd2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.New_Pwd1 = new System.Windows.Forms.TextBox();
            this.Modify_pwd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // New_Pwd2
            // 
            resources.ApplyResources(this.New_Pwd2, "New_Pwd2");
            this.New_Pwd2.Name = "New_Pwd2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // New_Pwd1
            // 
            resources.ApplyResources(this.New_Pwd1, "New_Pwd1");
            this.New_Pwd1.Name = "New_Pwd1";
            // 
            // Modify_pwd
            // 
            resources.ApplyResources(this.Modify_pwd, "Modify_pwd");
            this.Modify_pwd.Name = "Modify_pwd";
            this.Modify_pwd.UseVisualStyleBackColor = true;
            this.Modify_pwd.Click += new System.EventHandler(this.Modify_pwd_Click);
            // 
            // UserPassword
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.New_Pwd2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.New_Pwd1);
            this.Controls.Add(this.Modify_pwd);
            this.Name = "UserPassword";
            this.Load += new System.EventHandler(this.UserPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox New_Pwd2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox New_Pwd1;
        private System.Windows.Forms.Button Modify_pwd;
    }
}