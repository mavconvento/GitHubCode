namespace Eclock
{
    partial class frmMainMenuAdminImportData
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
            this.btnStartImport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pbMemberList = new System.Windows.Forms.ProgressBar();
            this.pbRegisterRFID = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.pbRegisterBandNumber = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartImport
            // 
            this.btnStartImport.Location = new System.Drawing.Point(78, 169);
            this.btnStartImport.Name = "btnStartImport";
            this.btnStartImport.Size = new System.Drawing.Size(133, 39);
            this.btnStartImport.TabIndex = 0;
            this.btnStartImport.Text = "Start Import";
            this.btnStartImport.UseVisualStyleBackColor = true;
            this.btnStartImport.Click += new System.EventHandler(this.btnStartImport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Member List";
            // 
            // pbMemberList
            // 
            this.pbMemberList.Location = new System.Drawing.Point(22, 30);
            this.pbMemberList.Name = "pbMemberList";
            this.pbMemberList.Size = new System.Drawing.Size(535, 14);
            this.pbMemberList.TabIndex = 2;
            // 
            // pbRegisterRFID
            // 
            this.pbRegisterRFID.Location = new System.Drawing.Point(22, 81);
            this.pbRegisterRFID.Name = "pbRegisterRFID";
            this.pbRegisterRFID.Size = new System.Drawing.Size(535, 14);
            this.pbRegisterRFID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Register RFID";
            // 
            // pbRegisterBandNumber
            // 
            this.pbRegisterBandNumber.Location = new System.Drawing.Point(22, 137);
            this.pbRegisterBandNumber.Name = "pbRegisterBandNumber";
            this.pbRegisterBandNumber.Size = new System.Drawing.Size(535, 16);
            this.pbRegisterBandNumber.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Register Band Number w/ RFID ";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 169);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 39);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel Import";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 39);
            this.button1.TabIndex = 8;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMainMenuAdminImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 220);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbRegisterBandNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbRegisterRFID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pbMemberList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartImport);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMainMenuAdminImportData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Admin Import Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMainMenuAdminImportData_FormClosed);
            this.Load += new System.EventHandler(this.frmMainMenuAdminImportData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartImport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pbMemberList;
        private System.Windows.Forms.ProgressBar pbRegisterRFID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbRegisterBandNumber;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
    }
}