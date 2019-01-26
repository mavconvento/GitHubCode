namespace PegionClocking
{
    partial class frmBirdEntryMasterlist
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBirdEntryMasterlist));
            this.dtEntryList = new System.Windows.Forms.DataGridView();
            this.lblLapNo = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblRaceScheduleCategory = new System.Windows.Forms.Label();
            this.lblReleaseTime = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblReleaseDate = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblCoordinates = new System.Windows.Forms.Label();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.lblLap = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRaceSchedule = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRecordEntry = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Button();
            this.cmbRaceCategory = new System.Windows.Forms.ComboBox();
            this.cmbGroupCategory = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryList)).BeginInit();
            this.SuspendLayout();
            // 
            // dtEntryList
            // 
            this.dtEntryList.AllowUserToAddRows = false;
            this.dtEntryList.AllowUserToDeleteRows = false;
            this.dtEntryList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dtEntryList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtEntryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtEntryList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtEntryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtEntryList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtEntryList.Location = new System.Drawing.Point(13, 184);
            this.dtEntryList.Name = "dtEntryList";
            this.dtEntryList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtEntryList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dtEntryList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtEntryList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtEntryList.Size = new System.Drawing.Size(813, 356);
            this.dtEntryList.TabIndex = 0;
            // 
            // lblLapNo
            // 
            this.lblLapNo.AutoSize = true;
            this.lblLapNo.BackColor = System.Drawing.Color.Transparent;
            this.lblLapNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLapNo.ForeColor = System.Drawing.Color.White;
            this.lblLapNo.Location = new System.Drawing.Point(539, 80);
            this.lblLapNo.Name = "lblLapNo";
            this.lblLapNo.Size = new System.Drawing.Size(38, 13);
            this.lblLapNo.TabIndex = 79;
            this.lblLapNo.Text = "value";
            this.lblLapNo.Click += new System.EventHandler(this.lblLapNo_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(477, 80);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(60, 13);
            this.label26.TabIndex = 78;
            this.label26.Text = "Lap No. :";
            this.label26.Click += new System.EventHandler(this.label26_Click);
            // 
            // lblRaceScheduleCategory
            // 
            this.lblRaceScheduleCategory.AutoSize = true;
            this.lblRaceScheduleCategory.BackColor = System.Drawing.Color.Transparent;
            this.lblRaceScheduleCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaceScheduleCategory.ForeColor = System.Drawing.Color.White;
            this.lblRaceScheduleCategory.Location = new System.Drawing.Point(173, 34);
            this.lblRaceScheduleCategory.Name = "lblRaceScheduleCategory";
            this.lblRaceScheduleCategory.Size = new System.Drawing.Size(38, 13);
            this.lblRaceScheduleCategory.TabIndex = 77;
            this.lblRaceScheduleCategory.Text = "value";
            this.lblRaceScheduleCategory.Click += new System.EventHandler(this.lblRaceScheduleCategory_Click);
            // 
            // lblReleaseTime
            // 
            this.lblReleaseTime.AutoSize = true;
            this.lblReleaseTime.BackColor = System.Drawing.Color.Transparent;
            this.lblReleaseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseTime.ForeColor = System.Drawing.Color.White;
            this.lblReleaseTime.Location = new System.Drawing.Point(539, 58);
            this.lblReleaseTime.Name = "lblReleaseTime";
            this.lblReleaseTime.Size = new System.Drawing.Size(38, 13);
            this.lblReleaseTime.TabIndex = 76;
            this.lblReleaseTime.Text = "value";
            this.lblReleaseTime.Click += new System.EventHandler(this.lblReleaseTime_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(434, 58);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 13);
            this.label19.TabIndex = 75;
            this.label19.Text = "Released Time  :";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.BackColor = System.Drawing.Color.Transparent;
            this.lblDistance.ForeColor = System.Drawing.Color.Black;
            this.lblDistance.Location = new System.Drawing.Point(506, 268);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(33, 13);
            this.lblDistance.TabIndex = 74;
            this.lblDistance.Text = "value";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(438, 268);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 73;
            this.label18.Text = "Distance :";
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AutoSize = true;
            this.lblReleaseDate.BackColor = System.Drawing.Color.Transparent;
            this.lblReleaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReleaseDate.ForeColor = System.Drawing.Color.White;
            this.lblReleaseDate.Location = new System.Drawing.Point(539, 34);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(38, 13);
            this.lblReleaseDate.TabIndex = 72;
            this.lblReleaseDate.Text = "value";
            this.lblReleaseDate.Click += new System.EventHandler(this.lblReleaseDate_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(434, 34);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 13);
            this.label23.TabIndex = 71;
            this.label23.Text = "Released Date  :";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // lblCoordinates
            // 
            this.lblCoordinates.AutoSize = true;
            this.lblCoordinates.BackColor = System.Drawing.Color.Transparent;
            this.lblCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoordinates.ForeColor = System.Drawing.Color.White;
            this.lblCoordinates.Location = new System.Drawing.Point(173, 80);
            this.lblCoordinates.Name = "lblCoordinates";
            this.lblCoordinates.Size = new System.Drawing.Size(38, 13);
            this.lblCoordinates.TabIndex = 70;
            this.lblCoordinates.Text = "value";
            this.lblCoordinates.Click += new System.EventHandler(this.lblCoordinates_Click);
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.BackColor = System.Drawing.Color.Transparent;
            this.lblLocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationName.ForeColor = System.Drawing.Color.White;
            this.lblLocationName.Location = new System.Drawing.Point(173, 58);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(38, 13);
            this.lblLocationName.TabIndex = 69;
            this.lblLocationName.Text = "value";
            this.lblLocationName.Click += new System.EventHandler(this.lblLocationName_Click);
            // 
            // lblLap
            // 
            this.lblLap.AutoSize = true;
            this.lblLap.BackColor = System.Drawing.Color.Transparent;
            this.lblLap.ForeColor = System.Drawing.Color.Black;
            this.lblLap.Location = new System.Drawing.Point(111, 202);
            this.lblLap.Name = "lblLap";
            this.lblLap.Size = new System.Drawing.Size(33, 13);
            this.lblLap.TabIndex = 68;
            this.lblLap.Text = "value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(30, 202);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 67;
            this.label2.Text = "Total Laps :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(173, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 66;
            this.label8.Text = "Race Schedule :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(14, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 13);
            this.label7.TabIndex = 65;
            this.label7.Text = "Race Schedule Category :";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblRaceSchedule
            // 
            this.lblRaceSchedule.AutoSize = true;
            this.lblRaceSchedule.BackColor = System.Drawing.Color.Transparent;
            this.lblRaceSchedule.ForeColor = System.Drawing.Color.Black;
            this.lblRaceSchedule.Location = new System.Drawing.Point(278, 243);
            this.lblRaceSchedule.Name = "lblRaceSchedule";
            this.lblRaceSchedule.Size = new System.Drawing.Size(33, 13);
            this.lblRaceSchedule.TabIndex = 64;
            this.lblRaceSchedule.Text = "value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(70, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Location Name :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(88, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Coordinates :";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 19;
            this.lineShape2.X2 = 819;
            this.lineShape2.Y1 = 118;
            this.lineShape2.Y2 = 118;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1,
            this.lineShape2});
            this.shapeContainer1.Size = new System.Drawing.Size(839, 548);
            this.shapeContainer1.TabIndex = 80;
            this.shapeContainer1.TabStop = false;
            this.shapeContainer1.Load += new System.EventHandler(this.shapeContainer1_Load);
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 17;
            this.lineShape1.X2 = 817;
            this.lineShape1.Y1 = 16;
            this.lineShape1.Y2 = 16;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(617, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(210, 23);
            this.button2.TabIndex = 83;
            this.button2.Text = "&GENERATE ENTRY(S)";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "Total Entry :";
            // 
            // lblRecordEntry
            // 
            this.lblRecordEntry.AutoSize = true;
            this.lblRecordEntry.BackColor = System.Drawing.Color.Transparent;
            this.lblRecordEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordEntry.ForeColor = System.Drawing.Color.White;
            this.lblRecordEntry.Location = new System.Drawing.Point(93, 169);
            this.lblRecordEntry.Name = "lblRecordEntry";
            this.lblRecordEntry.Size = new System.Drawing.Size(14, 13);
            this.lblRecordEntry.TabIndex = 85;
            this.lblRecordEntry.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(16, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 86;
            this.label4.Text = "Category :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(323, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 15);
            this.label9.TabIndex = 87;
            this.label9.Text = "Group :";
            // 
            // btn
            // 
            this.btn.BackColor = System.Drawing.Color.Transparent;
            this.btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn.Location = new System.Drawing.Point(616, 126);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(211, 26);
            this.btn.TabIndex = 88;
            this.btn.Text = "&VIEW ENTRY";
            this.btn.UseVisualStyleBackColor = false;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // cmbRaceCategory
            // 
            this.cmbRaceCategory.FormattingEnabled = true;
            this.cmbRaceCategory.Location = new System.Drawing.Point(89, 128);
            this.cmbRaceCategory.Name = "cmbRaceCategory";
            this.cmbRaceCategory.Size = new System.Drawing.Size(222, 21);
            this.cmbRaceCategory.TabIndex = 89;
            // 
            // cmbGroupCategory
            // 
            this.cmbGroupCategory.FormattingEnabled = true;
            this.cmbGroupCategory.Location = new System.Drawing.Point(377, 128);
            this.cmbGroupCategory.Name = "cmbGroupCategory";
            this.cmbGroupCategory.Size = new System.Drawing.Size(233, 21);
            this.cmbGroupCategory.TabIndex = 90;
            // 
            // frmBirdEntryMasterlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PegionClocking.Properties.Resources.background2;
            this.ClientSize = new System.Drawing.Size(839, 548);
            this.Controls.Add(this.cmbGroupCategory);
            this.Controls.Add(this.cmbRaceCategory);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRecordEntry);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblLapNo);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lblRaceScheduleCategory);
            this.Controls.Add(this.lblReleaseTime);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblReleaseDate);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.lblCoordinates);
            this.Controls.Add(this.lblLocationName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtEntryList);
            this.Controls.Add(this.lblLap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblRaceSchedule);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.shapeContainer1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBirdEntryMasterlist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bird Entry Summary";
            this.Load += new System.EventHandler(this.frmBirdEntryMasterlist_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtEntryList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtEntryList;
        private System.Windows.Forms.Label lblLapNo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblRaceScheduleCategory;
        private System.Windows.Forms.Label lblReleaseTime;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblReleaseDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblCoordinates;
        private System.Windows.Forms.Label lblLocationName;
        private System.Windows.Forms.Label lblLap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRaceSchedule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRecordEntry;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.ComboBox cmbRaceCategory;
        private System.Windows.Forms.ComboBox cmbGroupCategory;
    }
}