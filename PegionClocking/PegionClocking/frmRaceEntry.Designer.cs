namespace PegionClocking
{
    partial class frmRaceEntry
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRaceEntry));
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRaceScheduleCategory = new System.Windows.Forms.ComboBox();
            this.cmbRaceSchedule = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtReleasePoint = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtReleasePoint)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "Race Schedule Category :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Race Schedule :";
            // 
            // cmbRaceScheduleCategory
            // 
            this.cmbRaceScheduleCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaceScheduleCategory.FormattingEnabled = true;
            this.cmbRaceScheduleCategory.Location = new System.Drawing.Point(219, 65);
            this.cmbRaceScheduleCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRaceScheduleCategory.Name = "cmbRaceScheduleCategory";
            this.cmbRaceScheduleCategory.Size = new System.Drawing.Size(472, 24);
            this.cmbRaceScheduleCategory.TabIndex = 19;
            this.cmbRaceScheduleCategory.SelectedIndexChanged += new System.EventHandler(this.cmbRaceScheduleCategory_SelectedIndexChanged);
            // 
            // cmbRaceSchedule
            // 
            this.cmbRaceSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaceSchedule.FormattingEnabled = true;
            this.cmbRaceSchedule.Location = new System.Drawing.Point(219, 27);
            this.cmbRaceSchedule.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRaceSchedule.Name = "cmbRaceSchedule";
            this.cmbRaceSchedule.Size = new System.Drawing.Size(472, 24);
            this.cmbRaceSchedule.TabIndex = 11;
            this.cmbRaceSchedule.SelectedIndexChanged += new System.EventHandler(this.cmbRaceSchedule_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "List of Release Point";
            // 
            // dtReleasePoint
            // 
            this.dtReleasePoint.AllowUserToAddRows = false;
            this.dtReleasePoint.AllowUserToDeleteRows = false;
            this.dtReleasePoint.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtReleasePoint.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtReleasePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtReleasePoint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtReleasePoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtReleasePoint.Location = new System.Drawing.Point(39, 135);
            this.dtReleasePoint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtReleasePoint.Name = "dtReleasePoint";
            this.dtReleasePoint.ReadOnly = true;
            this.dtReleasePoint.Size = new System.Drawing.Size(896, 319);
            this.dtReleasePoint.TabIndex = 21;
            // 
            // frmRaceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 469);
            this.Controls.Add(this.dtReleasePoint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRaceScheduleCategory);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbRaceSchedule);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRaceEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Race Entry";
            this.Load += new System.EventHandler(this.frmRaceEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtReleasePoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRaceScheduleCategory;
        private System.Windows.Forms.ComboBox cmbRaceSchedule;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtReleasePoint;
    }
}