namespace SLC1_N
{
    partial class Machine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Machine));
            this.MachineType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.Define = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MachineType
            // 
            resources.ApplyResources(this.MachineType, "MachineType");
            this.MachineType.FormattingEnabled = true;
            this.MachineType.Name = "MachineType";
            this.MachineType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MachineType_KeyDown);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Add
            // 
            resources.ApplyResources(this.Add, "Add");
            this.Add.Name = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Delete
            // 
            resources.ApplyResources(this.Delete, "Delete");
            this.Delete.Name = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Define
            // 
            resources.ApplyResources(this.Define, "Define");
            this.Define.Name = "Define";
            this.Define.UseVisualStyleBackColor = true;
            this.Define.Click += new System.EventHandler(this.Define_Click);
            // 
            // Machine
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Define);
            this.Controls.Add(this.MachineType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.Delete);
            this.Name = "Machine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Machine_FormClosing);
            this.Load += new System.EventHandler(this.Machine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox MachineType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button Define;
    }
}