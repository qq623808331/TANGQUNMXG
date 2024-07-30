namespace SLC1_N
{
    partial class Save
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Save));
            this.ChkCSV = new System.Windows.Forms.CheckBox();
            this.BtnConMES = new System.Windows.Forms.Button();
            this.Use_Set = new System.Windows.Forms.Button();
            this.ChkMES = new System.Windows.Forms.CheckBox();
            this.BtnPath = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.ChkExcel = new System.Windows.Forms.CheckBox();
            this.path = new System.Windows.Forms.TextBox();
            this.FolderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Warning = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChkCSV
            // 
            resources.ApplyResources(this.ChkCSV, "ChkCSV");
            this.ChkCSV.Name = "ChkCSV";
            this.ChkCSV.UseVisualStyleBackColor = true;
            // 
            // BtnConMES
            // 
            resources.ApplyResources(this.BtnConMES, "BtnConMES");
            this.BtnConMES.Name = "BtnConMES";
            this.BtnConMES.UseVisualStyleBackColor = true;
            this.BtnConMES.Click += new System.EventHandler(this.BtnConMES_Click);
            // 
            // Use_Set
            // 
            resources.ApplyResources(this.Use_Set, "Use_Set");
            this.Use_Set.Name = "Use_Set";
            this.Use_Set.UseVisualStyleBackColor = true;
            this.Use_Set.Click += new System.EventHandler(this.Use_Set_Click);
            // 
            // ChkMES
            // 
            resources.ApplyResources(this.ChkMES, "ChkMES");
            this.ChkMES.Name = "ChkMES";
            this.ChkMES.UseVisualStyleBackColor = true;
            // 
            // BtnPath
            // 
            resources.ApplyResources(this.BtnPath, "BtnPath");
            this.BtnPath.Name = "BtnPath";
            this.BtnPath.UseVisualStyleBackColor = true;
            this.BtnPath.Click += new System.EventHandler(this.BtnPath_Click);
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // ChkExcel
            // 
            resources.ApplyResources(this.ChkExcel, "ChkExcel");
            this.ChkExcel.Name = "ChkExcel";
            this.ChkExcel.UseVisualStyleBackColor = true;
            // 
            // path
            // 
            resources.ApplyResources(this.path, "path");
            this.path.Name = "path";
            // 
            // FolderBrowserDialog1
            // 
            resources.ApplyResources(this.FolderBrowserDialog1, "FolderBrowserDialog1");
            // 
            // Warning
            // 
            resources.ApplyResources(this.Warning, "Warning");
            this.Warning.Name = "Warning";
            this.Warning.UseVisualStyleBackColor = true;
            this.Warning.Click += new System.EventHandler(this.Warning_Click);
            // 
            // Save
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Warning);
            this.Controls.Add(this.ChkCSV);
            this.Controls.Add(this.BtnConMES);
            this.Controls.Add(this.Use_Set);
            this.Controls.Add(this.ChkMES);
            this.Controls.Add(this.BtnPath);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.ChkExcel);
            this.Controls.Add(this.path);
            this.Name = "Save";
            this.Load += new System.EventHandler(this.Save_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkCSV;
        private System.Windows.Forms.Button BtnConMES;
        private System.Windows.Forms.Button Use_Set;
        private System.Windows.Forms.CheckBox ChkMES;
        private System.Windows.Forms.Button BtnPath;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox ChkExcel;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog1;
        private System.Windows.Forms.Button Warning;
    }
}