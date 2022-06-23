namespace mewtApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusVal = new System.Windows.Forms.Label();
            this.lblVol = new System.Windows.Forms.Label();
            this.lblVolVal = new System.Windows.Forms.Label();
            this.lblLastRecd = new System.Windows.Forms.Label();
            this.lblLastRecdVal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(358, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Exit Application";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Status:";
            // 
            // lblStatusVal
            // 
            this.lblStatusVal.AutoSize = true;
            this.lblStatusVal.Location = new System.Drawing.Point(78, 9);
            this.lblStatusVal.Name = "lblStatusVal";
            this.lblStatusVal.Size = new System.Drawing.Size(0, 15);
            this.lblStatusVal.TabIndex = 2;
            // 
            // lblVol
            // 
            this.lblVol.AutoSize = true;
            this.lblVol.Location = new System.Drawing.Point(12, 24);
            this.lblVol.Name = "lblVol";
            this.lblVol.Size = new System.Drawing.Size(50, 15);
            this.lblVol.TabIndex = 3;
            this.lblVol.Text = "Volume:";
            // 
            // lblVolVal
            // 
            this.lblVolVal.AutoSize = true;
            this.lblVolVal.Location = new System.Drawing.Point(78, 24);
            this.lblVolVal.Name = "lblVolVal";
            this.lblVolVal.Size = new System.Drawing.Size(13, 15);
            this.lblVolVal.TabIndex = 4;
            this.lblVolVal.Text = "0";
            // 
            // lblLastRecd
            // 
            this.lblLastRecd.AutoSize = true;
            this.lblLastRecd.Location = new System.Drawing.Point(12, 39);
            this.lblLastRecd.Name = "lblLastRecd";
            this.lblLastRecd.Size = new System.Drawing.Size(60, 15);
            this.lblLastRecd.TabIndex = 5;
            this.lblLastRecd.Text = "Last Recd:";
            // 
            // lblLastRecdVal
            // 
            this.lblLastRecdVal.AutoSize = true;
            this.lblLastRecdVal.Location = new System.Drawing.Point(78, 39);
            this.lblLastRecdVal.Name = "lblLastRecdVal";
            this.lblLastRecdVal.Size = new System.Drawing.Size(0, 15);
            this.lblLastRecdVal.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 65);
            this.Controls.Add(this.lblLastRecdVal);
            this.Controls.Add(this.lblLastRecd);
            this.Controls.Add(this.lblVolVal);
            this.Controls.Add(this.lblVol);
            this.Controls.Add(this.lblStatusVal);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Mewt App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private Label lblStatus;
        private Label lblStatusVal;
        private Label lblVol;
        private Label lblVolVal;
        private Label lblLastRecd;
        private Label lblLastRecdVal;
    }
}