namespace SLC1_N
{
    partial class MESConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MESConfig));
            this.FolderPath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Config = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.BtnPath = new System.Windows.Forms.Button();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // FolderPath
            // 
            resources.ApplyResources(this.FolderPath, "FolderPath");
            this.FolderPath.Name = "FolderPath";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // Config
            // 
            resources.ApplyResources(this.Config, "Config");
            this.Config.Name = "Config";
            this.Config.UseVisualStyleBackColor = true;
            this.Config.Click += new System.EventHandler(this.Config_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // FileName
            // 
            resources.ApplyResources(this.FileName, "FileName");
            this.FileName.Name = "FileName";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // BtnPath
            // 
            resources.ApplyResources(this.BtnPath, "BtnPath");
            this.BtnPath.Name = "BtnPath";
            this.BtnPath.UseVisualStyleBackColor = true;
            this.BtnPath.Click += new System.EventHandler(this.BtnPath_Click);
            // 
            // FolderBrowserDialog1
            // 
            resources.ApplyResources(this.FolderBrowserDialog1, "FolderBrowserDialog1");
            // 
            // MESConfig
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnPath);
            this.Controls.Add(this.FolderPath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Config);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.label19);
            this.Name = "MESConfig";
            this.Load += new System.EventHandler(this.MESConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FolderPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Config;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button BtnPath;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
    }
}