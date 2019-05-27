namespace Eclock
{
    partial class frmMainMenuMemberRaceMode
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClubList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLiberationPoint = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtReleaseTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMinimumSpeed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCutoff = new System.Windows.Forms.TextBox();
            this.txtCoordinates = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNoOfEntry = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtEclockRaceResult = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnGetRaceInfo = new System.Windows.Forms.Button();
            this.btnStartEclock = new System.Windows.Forms.Button();
            this.btnViewResult = new System.Windows.Forms.Button();
            this.txtReleaseDate = new System.Windows.Forms.TextBox();
            this.txtTotalSMSClock = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStopTimeTo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStopTimeFrom = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDistance = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Club :";
            // 
            // cmbClubList
            // 
            this.cmbClubList.FormattingEnabled = true;
            this.cmbClubList.Location = new System.Drawing.Point(16, 36);
            this.cmbClubList.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClubList.Name = "cmbClubList";
            this.cmbClubList.Size = new System.Drawing.Size(491, 24);
            this.cmbClubList.TabIndex = 1;
            this.cmbClubList.SelectedIndexChanged += new System.EventHandler(this.cmbClubList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 142);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Race Release Date :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(529, 36);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(392, 460);
            this.dataGridView1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Liberation Point :";
            // 
            // txtLiberationPoint
            // 
            this.txtLiberationPoint.BackColor = System.Drawing.Color.White;
            this.txtLiberationPoint.Location = new System.Drawing.Point(15, 109);
            this.txtLiberationPoint.Margin = new System.Windows.Forms.Padding(4);
            this.txtLiberationPoint.Name = "txtLiberationPoint";
            this.txtLiberationPoint.ReadOnly = true;
            this.txtLiberationPoint.Size = new System.Drawing.Size(494, 22);
            this.txtLiberationPoint.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(526, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Entry List";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(496, 10);
            this.label7.TabIndex = 13;
            this.label7.Text = "label7";
            // 
            // txtReleaseTime
            // 
            this.txtReleaseTime.BackColor = System.Drawing.Color.White;
            this.txtReleaseTime.Location = new System.Drawing.Point(193, 204);
            this.txtReleaseTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtReleaseTime.Name = "txtReleaseTime";
            this.txtReleaseTime.ReadOnly = true;
            this.txtReleaseTime.Size = new System.Drawing.Size(314, 22);
            this.txtReleaseTime.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 207);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Release Time :";
            // 
            // txtMinimumSpeed
            // 
            this.txtMinimumSpeed.BackColor = System.Drawing.Color.White;
            this.txtMinimumSpeed.Location = new System.Drawing.Point(193, 236);
            this.txtMinimumSpeed.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinimumSpeed.Name = "txtMinimumSpeed";
            this.txtMinimumSpeed.ReadOnly = true;
            this.txtMinimumSpeed.Size = new System.Drawing.Size(314, 22);
            this.txtMinimumSpeed.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(68, 236);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Minimum Speed :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 268);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 17);
            this.label10.TabIndex = 18;
            this.label10.Text = "Cut-Off :";
            // 
            // txtCutoff
            // 
            this.txtCutoff.BackColor = System.Drawing.Color.White;
            this.txtCutoff.Location = new System.Drawing.Point(193, 268);
            this.txtCutoff.Margin = new System.Windows.Forms.Padding(4);
            this.txtCutoff.Name = "txtCutoff";
            this.txtCutoff.ReadOnly = true;
            this.txtCutoff.Size = new System.Drawing.Size(314, 22);
            this.txtCutoff.TabIndex = 19;
            // 
            // txtCoordinates
            // 
            this.txtCoordinates.BackColor = System.Drawing.Color.White;
            this.txtCoordinates.Location = new System.Drawing.Point(193, 172);
            this.txtCoordinates.Margin = new System.Windows.Forms.Padding(4);
            this.txtCoordinates.Name = "txtCoordinates";
            this.txtCoordinates.ReadOnly = true;
            this.txtCoordinates.Size = new System.Drawing.Size(316, 22);
            this.txtCoordinates.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(92, 175);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 17);
            this.label11.TabIndex = 20;
            this.label11.Text = "Coordinates :";
            // 
            // txtNoOfEntry
            // 
            this.txtNoOfEntry.BackColor = System.Drawing.Color.White;
            this.txtNoOfEntry.Location = new System.Drawing.Point(193, 395);
            this.txtNoOfEntry.Margin = new System.Windows.Forms.Padding(4);
            this.txtNoOfEntry.Name = "txtNoOfEntry";
            this.txtNoOfEntry.ReadOnly = true;
            this.txtNoOfEntry.Size = new System.Drawing.Size(119, 22);
            this.txtNoOfEntry.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(94, 398);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(91, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "No. of Entry :";
            // 
            // txtEclockRaceResult
            // 
            this.txtEclockRaceResult.BackColor = System.Drawing.Color.White;
            this.txtEclockRaceResult.Location = new System.Drawing.Point(193, 426);
            this.txtEclockRaceResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtEclockRaceResult.Name = "txtEclockRaceResult";
            this.txtEclockRaceResult.ReadOnly = true;
            this.txtEclockRaceResult.Size = new System.Drawing.Size(119, 22);
            this.txtEclockRaceResult.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(0, 429);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(184, 17);
            this.label13.TabIndex = 24;
            this.label13.Text = "Total Bird Arrived (E-Clock):";
            // 
            // btnGetRaceInfo
            // 
            this.btnGetRaceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetRaceInfo.Location = new System.Drawing.Point(178, 504);
            this.btnGetRaceInfo.Margin = new System.Windows.Forms.Padding(4);
            this.btnGetRaceInfo.Name = "btnGetRaceInfo";
            this.btnGetRaceInfo.Size = new System.Drawing.Size(241, 28);
            this.btnGetRaceInfo.TabIndex = 26;
            this.btnGetRaceInfo.Text = "GET RACE INFO";
            this.btnGetRaceInfo.UseVisualStyleBackColor = true;
            this.btnGetRaceInfo.Click += new System.EventHandler(this.btnGetRaceInfo_Click);
            // 
            // btnStartEclock
            // 
            this.btnStartEclock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartEclock.Location = new System.Drawing.Point(593, 504);
            this.btnStartEclock.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartEclock.Name = "btnStartEclock";
            this.btnStartEclock.Size = new System.Drawing.Size(160, 28);
            this.btnStartEclock.TabIndex = 28;
            this.btnStartEclock.Text = "START ECLOCK";
            this.btnStartEclock.UseVisualStyleBackColor = true;
            this.btnStartEclock.Click += new System.EventHandler(this.btnStartEclock_Click);
            // 
            // btnViewResult
            // 
            this.btnViewResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewResult.Location = new System.Drawing.Point(761, 504);
            this.btnViewResult.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewResult.Name = "btnViewResult";
            this.btnViewResult.Size = new System.Drawing.Size(160, 28);
            this.btnViewResult.TabIndex = 30;
            this.btnViewResult.Text = "VIEW RESULT";
            this.btnViewResult.UseVisualStyleBackColor = true;
            this.btnViewResult.Click += new System.EventHandler(this.btnViewResult_Click);
            // 
            // txtReleaseDate
            // 
            this.txtReleaseDate.BackColor = System.Drawing.Color.White;
            this.txtReleaseDate.Location = new System.Drawing.Point(193, 139);
            this.txtReleaseDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtReleaseDate.Name = "txtReleaseDate";
            this.txtReleaseDate.ReadOnly = true;
            this.txtReleaseDate.Size = new System.Drawing.Size(316, 22);
            this.txtReleaseDate.TabIndex = 32;
            // 
            // txtTotalSMSClock
            // 
            this.txtTotalSMSClock.BackColor = System.Drawing.Color.White;
            this.txtTotalSMSClock.Location = new System.Drawing.Point(193, 460);
            this.txtTotalSMSClock.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalSMSClock.Name = "txtTotalSMSClock";
            this.txtTotalSMSClock.ReadOnly = true;
            this.txtTotalSMSClock.Size = new System.Drawing.Size(119, 22);
            this.txtTotalSMSClock.TabIndex = 34;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 463);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 17);
            this.label5.TabIndex = 33;
            this.label5.Text = "Total Bird Arrived (SMS):";
            // 
            // txtStopTimeTo
            // 
            this.txtStopTimeTo.BackColor = System.Drawing.Color.White;
            this.txtStopTimeTo.Location = new System.Drawing.Point(193, 365);
            this.txtStopTimeTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtStopTimeTo.Name = "txtStopTimeTo";
            this.txtStopTimeTo.ReadOnly = true;
            this.txtStopTimeTo.Size = new System.Drawing.Size(314, 22);
            this.txtStopTimeTo.TabIndex = 38;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(81, 365);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 17);
            this.label6.TabIndex = 37;
            this.label6.Text = "Stop Time To :";
            // 
            // txtStopTimeFrom
            // 
            this.txtStopTimeFrom.BackColor = System.Drawing.Color.White;
            this.txtStopTimeFrom.Location = new System.Drawing.Point(193, 333);
            this.txtStopTimeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtStopTimeFrom.Name = "txtStopTimeFrom";
            this.txtStopTimeFrom.ReadOnly = true;
            this.txtStopTimeFrom.Size = new System.Drawing.Size(314, 22);
            this.txtStopTimeFrom.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(68, 333);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 17);
            this.label14.TabIndex = 35;
            this.label14.Text = "Stop Time From :";
            // 
            // txtDistance
            // 
            this.txtDistance.BackColor = System.Drawing.Color.White;
            this.txtDistance.Location = new System.Drawing.Point(193, 301);
            this.txtDistance.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistance.Name = "txtDistance";
            this.txtDistance.ReadOnly = true;
            this.txtDistance.Size = new System.Drawing.Size(314, 22);
            this.txtDistance.TabIndex = 40;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(77, 301);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 17);
            this.label15.TabIndex = 39;
            this.label15.Text = "Distance (KM) :";
            // 
            // frmMainMenuMemberRaceMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 545);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtStopTimeTo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStopTimeFrom);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtTotalSMSClock);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtReleaseDate);
            this.Controls.Add(this.btnViewResult);
            this.Controls.Add(this.btnStartEclock);
            this.Controls.Add(this.btnGetRaceInfo);
            this.Controls.Add(this.txtEclockRaceResult);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtNoOfEntry);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtCoordinates);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCutoff);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtMinimumSpeed);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtReleaseTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLiberationPoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbClubList);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMainMenuMemberRaceMode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Race Mode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRaceMode_FormClosed);
            this.Load += new System.EventHandler(this.frmRaceMode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClubList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLiberationPoint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtReleaseTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMinimumSpeed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCutoff;
        private System.Windows.Forms.TextBox txtCoordinates;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNoOfEntry;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtEclockRaceResult;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnGetRaceInfo;
        private System.Windows.Forms.Button btnStartEclock;
        private System.Windows.Forms.Button btnViewResult;
        private System.Windows.Forms.TextBox txtReleaseDate;
        private System.Windows.Forms.TextBox txtTotalSMSClock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStopTimeTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStopTimeFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtDistance;
        private System.Windows.Forms.Label label15;
    }
}