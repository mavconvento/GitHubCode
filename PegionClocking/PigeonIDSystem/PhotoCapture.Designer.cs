namespace PigeonIDSystem
{
    partial class PhotoCapture
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
            this.bntVideoSource = new System.Windows.Forms.Button();
            this.bntVideoFormat = new System.Windows.Forms.Button();
            this.bntSave = new System.Windows.Forms.Button();
            this.bntCapture = new System.Windows.Forms.Button();
            this.bntContinue = new System.Windows.Forms.Button();
            this.bntStop = new System.Windows.Forms.Button();
            this.bntStart = new System.Windows.Forms.Button();
            this.imgCapture = new System.Windows.Forms.PictureBox();
            this.imgVideo = new System.Windows.Forms.PictureBox();
            this.exit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMemberID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRingNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbmSex = new System.Windows.Forms.ComboBox();
            this.dtList = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).BeginInit();
            this.SuspendLayout();
            // 
            // bntVideoSource
            // 
            this.bntVideoSource.Location = new System.Drawing.Point(894, 79);
            this.bntVideoSource.Name = "bntVideoSource";
            this.bntVideoSource.Size = new System.Drawing.Size(147, 23);
            this.bntVideoSource.TabIndex = 17;
            this.bntVideoSource.TabStop = false;
            this.bntVideoSource.Text = "Video Source";
            this.bntVideoSource.UseVisualStyleBackColor = true;
            this.bntVideoSource.Click += new System.EventHandler(this.bntVideoSource_Click);
            // 
            // bntVideoFormat
            // 
            this.bntVideoFormat.Location = new System.Drawing.Point(894, 50);
            this.bntVideoFormat.Name = "bntVideoFormat";
            this.bntVideoFormat.Size = new System.Drawing.Size(147, 23);
            this.bntVideoFormat.TabIndex = 16;
            this.bntVideoFormat.TabStop = false;
            this.bntVideoFormat.Text = "Video Format";
            this.bntVideoFormat.UseVisualStyleBackColor = true;
            this.bntVideoFormat.Click += new System.EventHandler(this.bntVideoFormat_Click);
            // 
            // bntSave
            // 
            this.bntSave.Location = new System.Drawing.Point(942, 108);
            this.bntSave.Name = "bntSave";
            this.bntSave.Size = new System.Drawing.Size(79, 23);
            this.bntSave.TabIndex = 15;
            this.bntSave.TabStop = false;
            this.bntSave.Text = "Save Image";
            this.bntSave.UseVisualStyleBackColor = true;
            this.bntSave.Click += new System.EventHandler(this.bntSave_Click);
            // 
            // bntCapture
            // 
            this.bntCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntCapture.Location = new System.Drawing.Point(40, 186);
            this.bntCapture.Name = "bntCapture";
            this.bntCapture.Size = new System.Drawing.Size(163, 23);
            this.bntCapture.TabIndex = 7;
            this.bntCapture.Text = "Capture Pigeon Photo";
            this.bntCapture.UseVisualStyleBackColor = true;
            this.bntCapture.Click += new System.EventHandler(this.bntCapture_Click);
            // 
            // bntContinue
            // 
            this.bntContinue.Location = new System.Drawing.Point(894, 137);
            this.bntContinue.Name = "bntContinue";
            this.bntContinue.Size = new System.Drawing.Size(61, 23);
            this.bntContinue.TabIndex = 13;
            this.bntContinue.TabStop = false;
            this.bntContinue.Text = "Continue";
            this.bntContinue.UseVisualStyleBackColor = true;
            this.bntContinue.Click += new System.EventHandler(this.bntContinue_Click);
            // 
            // bntStop
            // 
            this.bntStop.Location = new System.Drawing.Point(894, 166);
            this.bntStop.Name = "bntStop";
            this.bntStop.Size = new System.Drawing.Size(61, 23);
            this.bntStop.TabIndex = 12;
            this.bntStop.TabStop = false;
            this.bntStop.Text = "Stop";
            this.bntStop.UseVisualStyleBackColor = true;
            this.bntStop.Click += new System.EventHandler(this.bntStop_Click);
            // 
            // bntStart
            // 
            this.bntStart.Location = new System.Drawing.Point(894, 108);
            this.bntStart.Name = "bntStart";
            this.bntStart.Size = new System.Drawing.Size(61, 23);
            this.bntStart.TabIndex = 11;
            this.bntStart.TabStop = false;
            this.bntStart.Text = "Start";
            this.bntStart.UseVisualStyleBackColor = true;
            this.bntStart.Click += new System.EventHandler(this.bntStart_Click);
            // 
            // imgCapture
            // 
            this.imgCapture.BackColor = System.Drawing.Color.White;
            this.imgCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgCapture.Location = new System.Drawing.Point(209, 215);
            this.imgCapture.Name = "imgCapture";
            this.imgCapture.Size = new System.Drawing.Size(163, 160);
            this.imgCapture.TabIndex = 10;
            this.imgCapture.TabStop = false;
            // 
            // imgVideo
            // 
            this.imgVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imgVideo.Location = new System.Drawing.Point(40, 215);
            this.imgVideo.Name = "imgVideo";
            this.imgVideo.Size = new System.Drawing.Size(163, 160);
            this.imgVideo.TabIndex = 9;
            this.imgVideo.TabStop = false;
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(942, 141);
            this.exit.Margin = new System.Windows.Forms.Padding(2);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(56, 19);
            this.exit.TabIndex = 18;
            this.exit.TabStop = false;
            this.exit.Text = "EXİT";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Member ID:";
            // 
            // txtMemberID
            // 
            this.txtMemberID.Location = new System.Drawing.Point(88, 34);
            this.txtMemberID.Name = "txtMemberID";
            this.txtMemberID.Size = new System.Drawing.Size(183, 20);
            this.txtMemberID.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(88, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Name :";
            // 
            // txtRingNumber
            // 
            this.txtRingNumber.Enabled = false;
            this.txtRingNumber.Location = new System.Drawing.Point(88, 86);
            this.txtRingNumber.Name = "txtRingNumber";
            this.txtRingNumber.Size = new System.Drawing.Size(183, 20);
            this.txtRingNumber.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Ring Number :";
            // 
            // txtColor
            // 
            this.txtColor.Enabled = false;
            this.txtColor.Location = new System.Drawing.Point(88, 139);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(183, 20);
            this.txtColor.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Sex :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Color :";
            // 
            // cbmSex
            // 
            this.cbmSex.Enabled = false;
            this.cbmSex.FormattingEnabled = true;
            this.cbmSex.Location = new System.Drawing.Point(88, 112);
            this.cbmSex.Name = "cbmSex";
            this.cbmSex.Size = new System.Drawing.Size(183, 21);
            this.cbmSex.TabIndex = 5;
            // 
            // dtList
            // 
            this.dtList.AllowUserToDeleteRows = false;
            this.dtList.AllowUserToOrderColumns = true;
            this.dtList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtList.Location = new System.Drawing.Point(398, 29);
            this.dtList.Name = "dtList";
            this.dtList.Size = new System.Drawing.Size(438, 377);
            this.dtList.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(395, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Pigeon List:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(209, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(163, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save Record";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(103, 412);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 23);
            this.btnNew.TabIndex = 9;
            this.btnNew.Text = "New Member";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(277, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "SET";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(270, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 20);
            this.label7.TabIndex = 31;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(368, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 20);
            this.label8.TabIndex = 32;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(370, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 20);
            this.label9.TabIndex = 33;
            this.label9.Text = "*";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(673, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "Generate List for Printing";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // PhotoCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 441);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.imgCapture);
            this.Controls.Add(this.cbmSex);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.txtRingNumber);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMemberID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.bntVideoSource);
            this.Controls.Add(this.bntVideoFormat);
            this.Controls.Add(this.bntSave);
            this.Controls.Add(this.bntCapture);
            this.Controls.Add(this.bntContinue);
            this.Controls.Add(this.bntStop);
            this.Controls.Add(this.bntStart);
            this.Controls.Add(this.imgVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "PhotoCapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PigeonID System";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imgCapture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntVideoSource;
        private System.Windows.Forms.Button bntVideoFormat;
        private System.Windows.Forms.Button bntSave;
        private System.Windows.Forms.Button bntCapture;
        private System.Windows.Forms.Button bntContinue;
        private System.Windows.Forms.Button bntStop;
        private System.Windows.Forms.Button bntStart;
        private System.Windows.Forms.PictureBox imgCapture;
        private System.Windows.Forms.PictureBox imgVideo;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMemberID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRingNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbmSex;
        private System.Windows.Forms.DataGridView dtList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
    }
}

