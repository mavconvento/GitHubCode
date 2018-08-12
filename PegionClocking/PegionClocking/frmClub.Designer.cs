namespace PegionClocking
{
    partial class frmClub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClub));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbLongSign = new System.Windows.Forms.ComboBox();
            this.txtDistanceLatDegree = new System.Windows.Forms.TextBox();
            this.txtDistanceLongSeconds = new System.Windows.Forms.TextBox();
            this.cmbLatSign = new System.Windows.Forms.ComboBox();
            this.txtDistanceLongMinutes = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDistanceLongDegree = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDistanceLatMinutes = new System.Windows.Forms.TextBox();
            this.txtDistanceLatSeconds = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtClubID = new System.Windows.Forms.TextBox();
            this.txtClubName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClubAcronym = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsBackUp = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.cmbDateTimeSource = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkMAVCSticker = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpSubcriptionDate = new System.Windows.Forms.DateTimePicker();
            this.dtpLastSubcriptionDate = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cmbLongSign);
            this.groupBox1.Controls.Add(this.txtDistanceLatDegree);
            this.groupBox1.Controls.Add(this.txtDistanceLongSeconds);
            this.groupBox1.Controls.Add(this.cmbLatSign);
            this.groupBox1.Controls.Add(this.txtDistanceLongMinutes);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtDistanceLongDegree);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtDistanceLatMinutes);
            this.groupBox1.Controls.Add(this.txtDistanceLatSeconds);
            this.groupBox1.Location = new System.Drawing.Point(16, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(448, 114);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coordinates";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(29, 37);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(67, 17);
            this.label21.TabIndex = 42;
            this.label21.Text = "Latitude :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(19, 71);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(79, 17);
            this.label22.TabIndex = 43;
            this.label22.Text = "Longitude :";
            // 
            // cmbLongSign
            // 
            this.cmbLongSign.FormattingEnabled = true;
            this.cmbLongSign.Location = new System.Drawing.Point(365, 68);
            this.cmbLongSign.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLongSign.Name = "cmbLongSign";
            this.cmbLongSign.Size = new System.Drawing.Size(67, 24);
            this.cmbLongSign.Sorted = true;
            this.cmbLongSign.TabIndex = 9;
            // 
            // txtDistanceLatDegree
            // 
            this.txtDistanceLatDegree.Location = new System.Drawing.Point(107, 33);
            this.txtDistanceLatDegree.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLatDegree.Name = "txtDistanceLatDegree";
            this.txtDistanceLatDegree.Size = new System.Drawing.Size(49, 22);
            this.txtDistanceLatDegree.TabIndex = 2;
            this.txtDistanceLatDegree.Text = "0";
            this.txtDistanceLatDegree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDistanceLongSeconds
            // 
            this.txtDistanceLongSeconds.Location = new System.Drawing.Point(259, 68);
            this.txtDistanceLongSeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLongSeconds.Name = "txtDistanceLongSeconds";
            this.txtDistanceLongSeconds.Size = new System.Drawing.Size(76, 22);
            this.txtDistanceLongSeconds.TabIndex = 8;
            this.txtDistanceLongSeconds.Text = "0";
            this.txtDistanceLongSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLatSign
            // 
            this.cmbLatSign.FormattingEnabled = true;
            this.cmbLatSign.Location = new System.Drawing.Point(365, 33);
            this.cmbLatSign.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLatSign.Name = "cmbLatSign";
            this.cmbLatSign.Size = new System.Drawing.Size(67, 24);
            this.cmbLatSign.Sorted = true;
            this.cmbLatSign.TabIndex = 5;
            // 
            // txtDistanceLongMinutes
            // 
            this.txtDistanceLongMinutes.Location = new System.Drawing.Point(185, 66);
            this.txtDistanceLongMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLongMinutes.Name = "txtDistanceLongMinutes";
            this.txtDistanceLongMinutes.Size = new System.Drawing.Size(49, 22);
            this.txtDistanceLongMinutes.TabIndex = 7;
            this.txtDistanceLongMinutes.Text = "0";
            this.txtDistanceLongMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(336, 32);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 20);
            this.label17.TabIndex = 32;
            this.label17.Text = "\"";
            // 
            // txtDistanceLongDegree
            // 
            this.txtDistanceLongDegree.Location = new System.Drawing.Point(107, 68);
            this.txtDistanceLongDegree.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLongDegree.Name = "txtDistanceLongDegree";
            this.txtDistanceLongDegree.Size = new System.Drawing.Size(49, 22);
            this.txtDistanceLongDegree.TabIndex = 6;
            this.txtDistanceLongDegree.Text = "0";
            this.txtDistanceLongDegree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(155, 63);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 20);
            this.label18.TabIndex = 36;
            this.label18.Text = "o";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(233, 31);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 20);
            this.label16.TabIndex = 30;
            this.label16.Text = "\'";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(233, 65);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 20);
            this.label19.TabIndex = 37;
            this.label19.Text = "\'";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(155, 28);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 20);
            this.label15.TabIndex = 28;
            this.label15.Text = "o";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(336, 66);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(16, 20);
            this.label20.TabIndex = 38;
            this.label20.Text = "\"";
            // 
            // txtDistanceLatMinutes
            // 
            this.txtDistanceLatMinutes.Location = new System.Drawing.Point(185, 32);
            this.txtDistanceLatMinutes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLatMinutes.Name = "txtDistanceLatMinutes";
            this.txtDistanceLatMinutes.Size = new System.Drawing.Size(49, 22);
            this.txtDistanceLatMinutes.TabIndex = 3;
            this.txtDistanceLatMinutes.Text = "0";
            this.txtDistanceLatMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDistanceLatSeconds
            // 
            this.txtDistanceLatSeconds.Location = new System.Drawing.Point(259, 33);
            this.txtDistanceLatSeconds.Margin = new System.Windows.Forms.Padding(4);
            this.txtDistanceLatSeconds.Name = "txtDistanceLatSeconds";
            this.txtDistanceLatSeconds.Size = new System.Drawing.Size(76, 22);
            this.txtDistanceLatSeconds.TabIndex = 4;
            this.txtDistanceLatSeconds.Text = "0";
            this.txtDistanceLatSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(73, 421);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(181, 421);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 62;
            this.label2.Text = "ClubID";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(289, 421);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(483, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(640, 416);
            this.dataGridView1.TabIndex = 64;
            this.dataGridView1.TabStop = false;
            // 
            // txtClubID
            // 
            this.txtClubID.BackColor = System.Drawing.Color.White;
            this.txtClubID.Location = new System.Drawing.Point(119, 15);
            this.txtClubID.Margin = new System.Windows.Forms.Padding(4);
            this.txtClubID.Name = "txtClubID";
            this.txtClubID.ReadOnly = true;
            this.txtClubID.Size = new System.Drawing.Size(132, 22);
            this.txtClubID.TabIndex = 63;
            this.txtClubID.TabStop = false;
            // 
            // txtClubName
            // 
            this.txtClubName.Location = new System.Drawing.Point(119, 47);
            this.txtClubName.Margin = new System.Windows.Forms.Padding(4);
            this.txtClubName.Name = "txtClubName";
            this.txtClubName.Size = new System.Drawing.Size(344, 22);
            this.txtClubName.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 61;
            this.label1.Text = "Club Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(479, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 17);
            this.label3.TabIndex = 65;
            this.label3.Text = "Club List";
            // 
            // txtClubAcronym
            // 
            this.txtClubAcronym.Location = new System.Drawing.Point(119, 79);
            this.txtClubAcronym.Margin = new System.Windows.Forms.Padding(4);
            this.txtClubAcronym.Name = "txtClubAcronym";
            this.txtClubAcronym.Size = new System.Drawing.Size(132, 22);
            this.txtClubAcronym.TabIndex = 57;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 82);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 67;
            this.label4.Text = "Club Acronym";
            // 
            // chkIsBackUp
            // 
            this.chkIsBackUp.AutoSize = true;
            this.chkIsBackUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsBackUp.Location = new System.Drawing.Point(336, 233);
            this.chkIsBackUp.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsBackUp.Name = "chkIsBackUp";
            this.chkIsBackUp.Size = new System.Drawing.Size(103, 21);
            this.chkIsBackUp.TabIndex = 12;
            this.chkIsBackUp.Text = "IsBack-Up";
            this.chkIsBackUp.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 251);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 69;
            this.label5.Text = "Version :";
            // 
            // cmbVersion
            // 
            this.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(151, 241);
            this.cmbVersion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(132, 24);
            this.cmbVersion.TabIndex = 10;
            // 
            // cmbDateTimeSource
            // 
            this.cmbDateTimeSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateTimeSource.FormattingEnabled = true;
            this.cmbDateTimeSource.Location = new System.Drawing.Point(151, 277);
            this.cmbDateTimeSource.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDateTimeSource.Name = "cmbDateTimeSource";
            this.cmbDateTimeSource.Size = new System.Drawing.Size(160, 24);
            this.cmbDateTimeSource.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 281);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 17);
            this.label6.TabIndex = 71;
            this.label6.Text = "DateTime Source :";
            // 
            // chkMAVCSticker
            // 
            this.chkMAVCSticker.AutoSize = true;
            this.chkMAVCSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMAVCSticker.Location = new System.Drawing.Point(336, 265);
            this.chkMAVCSticker.Margin = new System.Windows.Forms.Padding(4);
            this.chkMAVCSticker.Name = "chkMAVCSticker";
            this.chkMAVCSticker.Size = new System.Drawing.Size(127, 21);
            this.chkMAVCSticker.TabIndex = 13;
            this.chkMAVCSticker.Text = "MAVC Sticker";
            this.chkMAVCSticker.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 352);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 17);
            this.label7.TabIndex = 72;
            this.label7.Text = "Subscription Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 384);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(141, 17);
            this.label8.TabIndex = 73;
            this.label8.Text = "Last Subription Date:";
            // 
            // dtpSubcriptionDate
            // 
            this.dtpSubcriptionDate.Location = new System.Drawing.Point(150, 352);
            this.dtpSubcriptionDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpSubcriptionDate.Name = "dtpSubcriptionDate";
            this.dtpSubcriptionDate.Size = new System.Drawing.Size(265, 22);
            this.dtpSubcriptionDate.TabIndex = 74;
            // 
            // dtpLastSubcriptionDate
            // 
            this.dtpLastSubcriptionDate.Location = new System.Drawing.Point(150, 384);
            this.dtpLastSubcriptionDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpLastSubcriptionDate.Name = "dtpLastSubcriptionDate";
            this.dtpLastSubcriptionDate.Size = new System.Drawing.Size(265, 22);
            this.dtpLastSubcriptionDate.TabIndex = 75;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(151, 309);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 24);
            this.comboBox1.TabIndex = 76;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(84, 313);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 17);
            this.label9.TabIndex = 77;
            this.label9.Text = "Server :";
            // 
            // frmClub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 465);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dtpLastSubcriptionDate);
            this.Controls.Add(this.dtpSubcriptionDate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chkMAVCSticker);
            this.Controls.Add(this.cmbDateTimeSource);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chkIsBackUp);
            this.Controls.Add(this.txtClubAcronym);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtClubID);
            this.Controls.Add(this.txtClubName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Club";
            this.Load += new System.EventHandler(this.frmClub_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbLongSign;
        private System.Windows.Forms.TextBox txtDistanceLatDegree;
        private System.Windows.Forms.TextBox txtDistanceLongSeconds;
        private System.Windows.Forms.ComboBox cmbLatSign;
        private System.Windows.Forms.TextBox txtDistanceLongMinutes;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDistanceLongDegree;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDistanceLatMinutes;
        private System.Windows.Forms.TextBox txtDistanceLatSeconds;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtClubID;
        private System.Windows.Forms.TextBox txtClubName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClubAcronym;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsBackUp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.ComboBox cmbDateTimeSource;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkMAVCSticker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpSubcriptionDate;
        private System.Windows.Forms.DateTimePicker dtpLastSubcriptionDate;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
    }
}