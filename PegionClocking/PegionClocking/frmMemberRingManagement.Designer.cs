namespace PegionClocking
{
    partial class frmMemberRingManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberRingManagement));
            this.cmbRaceSchedule = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtMemberCoordinates = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMemberName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnGO = new System.Windows.Forms.Button();
            this.txtMemberIDNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRingNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtBandID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRFIDSerialNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRange2 = new System.Windows.Forms.TextBox();
            this.txtRange1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBandFormat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbBatch = new System.Windows.Forms.RadioButton();
            this.rbIndividual = new System.Windows.Forms.RadioButton();
            this.cmbRaceScheduleCategory = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRaceSchedule
            // 
            this.cmbRaceSchedule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaceSchedule.FormattingEnabled = true;
            this.cmbRaceSchedule.Location = new System.Drawing.Point(167, 28);
            this.cmbRaceSchedule.Name = "cmbRaceSchedule";
            this.cmbRaceSchedule.Size = new System.Drawing.Size(261, 21);
            this.cmbRaceSchedule.TabIndex = 1;
            this.cmbRaceSchedule.SelectedIndexChanged += new System.EventHandler(this.cmbRaceSchedule_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Race Schedule :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(453, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(287, 331);
            this.dataGridView1.TabIndex = 14;
            // 
            // txtMemberCoordinates
            // 
            this.txtMemberCoordinates.BackColor = System.Drawing.Color.White;
            this.txtMemberCoordinates.Location = new System.Drawing.Point(102, 97);
            this.txtMemberCoordinates.Name = "txtMemberCoordinates";
            this.txtMemberCoordinates.ReadOnly = true;
            this.txtMemberCoordinates.Size = new System.Drawing.Size(237, 20);
            this.txtMemberCoordinates.TabIndex = 41;
            this.txtMemberCoordinates.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(15, 101);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(82, 13);
            this.label21.TabIndex = 44;
            this.label21.Text = "Coordinates :";
            // 
            // txtMemberName
            // 
            this.txtMemberName.BackColor = System.Drawing.Color.White;
            this.txtMemberName.Location = new System.Drawing.Point(102, 70);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.ReadOnly = true;
            this.txtMemberName.Size = new System.Drawing.Size(237, 20);
            this.txtMemberName.TabIndex = 40;
            this.txtMemberName.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(51, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 43;
            this.label10.Text = "Name :";
            // 
            // btnGO
            // 
            this.btnGO.BackColor = System.Drawing.Color.Teal;
            this.btnGO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGO.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGO.Location = new System.Drawing.Point(251, 39);
            this.btnGO.Name = "btnGO";
            this.btnGO.Size = new System.Drawing.Size(51, 23);
            this.btnGO.TabIndex = 3;
            this.btnGO.Text = "&GO!";
            this.btnGO.UseVisualStyleBackColor = false;
            this.btnGO.Click += new System.EventHandler(this.btnGO_Click);
            // 
            // txtMemberIDNo
            // 
            this.txtMemberIDNo.BackColor = System.Drawing.Color.White;
            this.txtMemberIDNo.Location = new System.Drawing.Point(102, 41);
            this.txtMemberIDNo.Name = "txtMemberIDNo";
            this.txtMemberIDNo.Size = new System.Drawing.Size(143, 20);
            this.txtMemberIDNo.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 42;
            this.label9.Text = "Member ID :";
            // 
            // txtRingNumber
            // 
            this.txtRingNumber.BackColor = System.Drawing.Color.White;
            this.txtRingNumber.Enabled = false;
            this.txtRingNumber.Location = new System.Drawing.Point(14, 172);
            this.txtRingNumber.Name = "txtRingNumber";
            this.txtRingNumber.Size = new System.Drawing.Size(143, 20);
            this.txtRingNumber.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(75, 13);
            this.label14.TabIndex = 46;
            this.label14.Text = "Ring Number :";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Teal;
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(231, 322);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(119, 37);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete Ring Number";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Teal;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(106, 322);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 39);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save Ring Number";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Teal;
            this.btnNew.Enabled = false;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(16, 322);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(87, 40);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "&New Member";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(450, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Ring Number Enrolled :";
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(219, 2);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(50, 20);
            this.txtMemberID.TabIndex = 63;
            this.txtMemberID.Text = "0";
            this.txtMemberID.Visible = false;
            this.txtMemberID.TextChanged += new System.EventHandler(this.txtMemberID_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(148, 5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 13);
            this.label17.TabIndex = 62;
            this.label17.Text = "Member ID :";
            this.label17.Visible = false;
            // 
            // txtBandID
            // 
            this.txtBandID.Location = new System.Drawing.Point(333, 2);
            this.txtBandID.Name = "txtBandID";
            this.txtBandID.Size = new System.Drawing.Size(50, 20);
            this.txtBandID.TabIndex = 65;
            this.txtBandID.Text = "0";
            this.txtBandID.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(275, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Band ID :";
            this.label3.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtRFIDSerialNo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtRange2);
            this.groupBox1.Controls.Add(this.txtRange1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBandFormat);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rbBatch);
            this.groupBox1.Controls.Add(this.rbIndividual);
            this.groupBox1.Controls.Add(this.txtRingNumber);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMemberIDNo);
            this.groupBox1.Controls.Add(this.btnGO);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtMemberName);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtMemberCoordinates);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(16, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 220);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // txtRFIDSerialNo
            // 
            this.txtRFIDSerialNo.BackColor = System.Drawing.Color.White;
            this.txtRFIDSerialNo.Enabled = false;
            this.txtRFIDSerialNo.Location = new System.Drawing.Point(186, 173);
            this.txtRFIDSerialNo.Name = "txtRFIDSerialNo";
            this.txtRFIDSerialNo.Size = new System.Drawing.Size(177, 20);
            this.txtRFIDSerialNo.TabIndex = 74;
            this.txtRFIDSerialNo.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(185, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "RFID Serial No. :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(263, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 25);
            this.label7.TabIndex = 73;
            this.label7.Text = "-";
            // 
            // txtRange2
            // 
            this.txtRange2.BackColor = System.Drawing.Color.White;
            this.txtRange2.Enabled = false;
            this.txtRange2.Location = new System.Drawing.Point(283, 173);
            this.txtRange2.Name = "txtRange2";
            this.txtRange2.Size = new System.Drawing.Size(80, 20);
            this.txtRange2.TabIndex = 72;
            this.txtRange2.Text = "0";
            this.txtRange2.Visible = false;
            // 
            // txtRange1
            // 
            this.txtRange1.BackColor = System.Drawing.Color.White;
            this.txtRange1.Enabled = false;
            this.txtRange1.Location = new System.Drawing.Point(186, 173);
            this.txtRange1.Name = "txtRange1";
            this.txtRange1.Size = new System.Drawing.Size(80, 20);
            this.txtRange1.TabIndex = 71;
            this.txtRange1.Text = "0";
            this.txtRange1.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(183, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Range :";
            // 
            // txtBandFormat
            // 
            this.txtBandFormat.BackColor = System.Drawing.Color.White;
            this.txtBandFormat.Enabled = false;
            this.txtBandFormat.Location = new System.Drawing.Point(14, 172);
            this.txtBandFormat.Name = "txtBandFormat";
            this.txtBandFormat.Size = new System.Drawing.Size(165, 20);
            this.txtBandFormat.TabIndex = 68;
            this.txtBandFormat.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 69;
            this.label4.Text = "Band Format :";
            // 
            // rbBatch
            // 
            this.rbBatch.AutoSize = true;
            this.rbBatch.BackColor = System.Drawing.Color.Transparent;
            this.rbBatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBatch.Location = new System.Drawing.Point(125, 132);
            this.rbBatch.Name = "rbBatch";
            this.rbBatch.Size = new System.Drawing.Size(121, 17);
            this.rbBatch.TabIndex = 67;
            this.rbBatch.Text = "Batch Enrollment";
            this.rbBatch.UseVisualStyleBackColor = false;
            this.rbBatch.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbIndividual
            // 
            this.rbIndividual.AutoSize = true;
            this.rbIndividual.BackColor = System.Drawing.Color.Transparent;
            this.rbIndividual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbIndividual.Location = new System.Drawing.Point(14, 132);
            this.rbIndividual.Name = "rbIndividual";
            this.rbIndividual.Size = new System.Drawing.Size(80, 17);
            this.rbIndividual.TabIndex = 66;
            this.rbIndividual.Text = "Individual";
            this.rbIndividual.UseVisualStyleBackColor = false;
            this.rbIndividual.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // cmbRaceScheduleCategory
            // 
            this.cmbRaceScheduleCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaceScheduleCategory.FormattingEnabled = true;
            this.cmbRaceScheduleCategory.Location = new System.Drawing.Point(167, 58);
            this.cmbRaceScheduleCategory.Name = "cmbRaceScheduleCategory";
            this.cmbRaceScheduleCategory.Size = new System.Drawing.Size(261, 21);
            this.cmbRaceScheduleCategory.TabIndex = 70;
            this.cmbRaceScheduleCategory.Visible = false;
            this.cmbRaceScheduleCategory.SelectedIndexChanged += new System.EventHandler(this.cmbRaceScheduleCategory_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 69;
            this.label5.Text = "Race Schedule Category :";
            this.label5.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(356, 322);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 37);
            this.button1.TabIndex = 71;
            this.button1.Text = "D&one";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmMemberRingManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PegionClocking.Properties.Resources.background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(751, 374);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbRaceScheduleCategory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBandID);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cmbRaceSchedule);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtMemberID);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMemberRingManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member Ring Enrollment";
            this.Load += new System.EventHandler(this.frmMemberRingManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRaceSchedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtMemberCoordinates;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtMemberName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnGO;
        private System.Windows.Forms.TextBox txtMemberIDNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRingNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtBandID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbRaceScheduleCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBandFormat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbBatch;
        private System.Windows.Forms.RadioButton rbIndividual;
        private System.Windows.Forms.TextBox txtRange1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRange2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRFIDSerialNo;
        private System.Windows.Forms.Label label8;
    }
}