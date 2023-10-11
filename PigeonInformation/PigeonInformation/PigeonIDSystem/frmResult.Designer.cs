namespace PigeonIDSystem
{
    partial class frmResult
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
            this.button7 = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button6 = new System.Windows.Forms.Button();
            this.lblcount = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dtList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.bntVideoSource = new System.Windows.Forms.Button();
            this.bntVideoFormat = new System.Windows.Forms.Button();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntContinue = new System.Windows.Forms.Button();
            this.bntStop = new System.Windows.Forms.Button();
            this.bntStart = new System.Windows.Forms.Button();
            this.btnSyncTraining = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).BeginInit();
            this.SuspendLayout();
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(334, 107);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(93, 23);
            this.button7.TabIndex = 108;
            this.button7.Text = "SET";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnSync
            // 
            this.btnSync.Enabled = false;
            this.btnSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSync.Location = new System.Drawing.Point(15, 475);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(182, 23);
            this.btnSync.TabIndex = 117;
            this.btnSync.Text = "SYNC E-CLOCK RACE";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Visible = false;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(392, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(93, 24);
            this.button3.TabIndex = 106;
            this.button3.Text = "SET";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(15, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(88, 13);
            this.label17.TabIndex = 138;
            this.label17.Text = "Release Date:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(106, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(280, 22);
            this.dateTimePicker1.TabIndex = 105;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(392, 446);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(380, 52);
            this.button6.TabIndex = 123;
            this.button6.Text = "SEND TOP PIGEON RESULT TO MAVC  DATABASE";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcount.Location = new System.Drawing.Point(622, 68);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(72, 13);
            this.lblcount.TabIndex = 137;
            this.lblcount.Text = "Total Birds:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(218, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 125;
            this.button2.Text = "PRINT LIST";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtName.Location = new System.Drawing.Point(115, 230);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(308, 20);
            this.txtName.TabIndex = 109;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(15, 446);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(182, 23);
            this.btnNew.TabIndex = 116;
            this.btnNew.Text = "NEW MEMBER";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 134;
            this.label6.Text = "Result List:";
            // 
            // dtList
            // 
            this.dtList.AllowUserToAddRows = false;
            this.dtList.AllowUserToDeleteRows = false;
            this.dtList.AllowUserToOrderColumns = true;
            this.dtList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtList.Location = new System.Drawing.Point(15, 84);
            this.dtList.Name = "dtList";
            this.dtList.Size = new System.Drawing.Size(757, 345);
            this.dtList.TabIndex = 118;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(63, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 130;
            this.label2.Text = "Name :";
            // 
            // txtMemberID
            // 
            this.txtMemberID.BackColor = System.Drawing.Color.White;
            this.txtMemberID.Enabled = false;
            this.txtMemberID.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMemberID.Location = new System.Drawing.Point(115, 109);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(213, 20);
            this.txtMemberID.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 129;
            this.label1.Text = "Member ID:";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(1146, 122);
            this.exit.Margin = new System.Windows.Forms.Padding(2);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(56, 19);
            this.exit.TabIndex = 128;
            this.exit.TabStop = false;
            this.exit.Text = "EXİT";
            this.exit.UseVisualStyleBackColor = true;
            // 
            // bntVideoSource
            // 
            this.bntVideoSource.Location = new System.Drawing.Point(1098, 60);
            this.bntVideoSource.Name = "bntVideoSource";
            this.bntVideoSource.Size = new System.Drawing.Size(147, 23);
            this.bntVideoSource.TabIndex = 127;
            this.bntVideoSource.TabStop = false;
            this.bntVideoSource.Text = "Video Source";
            this.bntVideoSource.UseVisualStyleBackColor = true;
            // 
            // bntVideoFormat
            // 
            this.bntVideoFormat.Location = new System.Drawing.Point(1098, 31);
            this.bntVideoFormat.Name = "bntVideoFormat";
            this.bntVideoFormat.Size = new System.Drawing.Size(147, 23);
            this.bntVideoFormat.TabIndex = 126;
            this.bntVideoFormat.TabStop = false;
            this.bntVideoFormat.Text = "Video Format";
            this.bntVideoFormat.UseVisualStyleBackColor = true;
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(1146, 89);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(79, 23);
            this.bntSave.TabIndex = 124;
            this.bntSave.TabStop = false;
            this.bntSave.Text = "Save Image";
            this.bntSave.UseVisualStyleBackColor = true;
            // 
            // bntContinue
            // 
            this.bntContinue.Location = new System.Drawing.Point(1098, 118);
            this.bntContinue.Name = "bntContinue";
            this.bntContinue.Size = new System.Drawing.Size(61, 23);
            this.bntContinue.TabIndex = 122;
            this.bntContinue.TabStop = false;
            this.bntContinue.Text = "Continue";
            this.bntContinue.UseVisualStyleBackColor = true;
            // 
            // bntStop
            // 
            this.bntStop.Location = new System.Drawing.Point(1098, 147);
            this.bntStop.Name = "bntStop";
            this.bntStop.Size = new System.Drawing.Size(61, 23);
            this.bntStop.TabIndex = 121;
            this.bntStop.TabStop = false;
            this.bntStop.Text = "Stop";
            this.bntStop.UseVisualStyleBackColor = true;
            // 
            // bntStart
            // 
            this.bntStart.Location = new System.Drawing.Point(1098, 89);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(61, 23);
            this.bntStart.TabIndex = 120;
            this.bntStart.TabStop = false;
            this.bntStart.Text = "Start";
            this.bntStart.UseVisualStyleBackColor = true;
            // 
            // btnSyncTraining
            // 
            this.btnSyncTraining.Enabled = false;
            this.btnSyncTraining.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncTraining.Location = new System.Drawing.Point(203, 475);
            this.btnSyncTraining.Name = "btnSyncTraining";
            this.btnSyncTraining.Size = new System.Drawing.Size(183, 23);
            this.btnSyncTraining.TabIndex = 139;
            this.btnSyncTraining.Text = "SYNC E-CLOCK TRAINING RESULT";
            this.btnSyncTraining.UseVisualStyleBackColor = true;
            this.btnSyncTraining.Visible = false;
            this.btnSyncTraining.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 529);
            this.Controls.Add(this.btnSyncTraining);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.lblcount);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.bntVideoSource);
            this.Controls.Add(this.bntVideoFormat);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntContinue);
            this.Controls.Add(this.bntStop);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.txtName);
            this.Name = "frmResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Result";
            this.Load += new System.EventHandler(this.Result_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dtList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button bntVideoSource;
        private System.Windows.Forms.Button bntVideoFormat;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntContinue;
        private System.Windows.Forms.Button bntStop;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.Button btnSyncTraining;
    }
}