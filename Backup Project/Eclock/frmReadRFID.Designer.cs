namespace Eclock
{
    partial class frmReadRFID
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
            this.txtRFID = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblTotalArrived = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRFID
            // 
            this.txtRFID.Location = new System.Drawing.Point(12, 12);
            this.txtRFID.Name = "txtRFID";
            this.txtRFID.ShortcutsEnabled = false;
            this.txtRFID.Size = new System.Drawing.Size(491, 22);
            this.txtRFID.TabIndex = 0;
            this.txtRFID.UseSystemPasswordChar = true;
            this.txtRFID.TextChanged += new System.EventHandler(this.txtRFID_TextChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // lblTotalArrived
            // 
            this.lblTotalArrived.AutoSize = true;
            this.lblTotalArrived.Font = new System.Drawing.Font("Arial Black", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalArrived.Location = new System.Drawing.Point(236, 42);
            this.lblTotalArrived.Name = "lblTotalArrived";
            this.lblTotalArrived.Size = new System.Drawing.Size(25, 27);
            this.lblTotalArrived.TabIndex = 4;
            this.lblTotalArrived.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Black", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 27);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total Bird Arrived : ";
            // 
            // frmReadRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 78);
            this.Controls.Add(this.lblTotalArrived);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtRFID);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReadRFID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "E-CLOCK RUNNING";
            this.Load += new System.EventHandler(this.frmReadRFID_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRFID;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lblTotalArrived;
        private System.Windows.Forms.Label label4;

    }
}