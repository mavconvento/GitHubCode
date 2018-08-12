namespace PegionClocking
{
    partial class frmGenerateEclockInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerateEclockInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.txtAssignedTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClubAbbreviation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtClubID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbClub = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.rdbPlayer = new System.Windows.Forms.RadioButton();
            this.rdbClub = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "ReaderID :";
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(143, 151);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(296, 22);
            this.txtReaderID.TabIndex = 1;
            // 
            // txtAssignedTo
            // 
            this.txtAssignedTo.Location = new System.Drawing.Point(11, 207);
            this.txtAssignedTo.Name = "txtAssignedTo";
            this.txtAssignedTo.Size = new System.Drawing.Size(552, 22);
            this.txtAssignedTo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Assigned To :";
            // 
            // txtClubAbbreviation
            // 
            this.txtClubAbbreviation.Location = new System.Drawing.Point(143, 123);
            this.txtClubAbbreviation.Name = "txtClubAbbreviation";
            this.txtClubAbbreviation.Size = new System.Drawing.Size(296, 22);
            this.txtClubAbbreviation.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Club Abbreviation :";
            // 
            // txtClubID
            // 
            this.txtClubID.BackColor = System.Drawing.Color.White;
            this.txtClubID.Location = new System.Drawing.Point(486, 52);
            this.txtClubID.Margin = new System.Windows.Forms.Padding(4);
            this.txtClubID.Name = "txtClubID";
            this.txtClubID.ReadOnly = true;
            this.txtClubID.Size = new System.Drawing.Size(73, 22);
            this.txtClubID.TabIndex = 76;
            this.txtClubID.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(425, 55);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 75;
            this.label7.Text = "Club ID";
            this.label7.Visible = false;
            // 
            // cmbClub
            // 
            this.cmbClub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClub.FormattingEnabled = true;
            this.cmbClub.Location = new System.Drawing.Point(11, 82);
            this.cmbClub.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClub.Name = "cmbClub";
            this.cmbClub.Size = new System.Drawing.Size(552, 24);
            this.cmbClub.TabIndex = 73;
            this.cmbClub.SelectedIndexChanged += new System.EventHandler(this.cmbClub_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 65);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 17);
            this.label6.TabIndex = 74;
            this.label6.Text = "Club Name :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(230, 281);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 30);
            this.button1.TabIndex = 80;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rdbPlayer
            // 
            this.rdbPlayer.AutoSize = true;
            this.rdbPlayer.Checked = true;
            this.rdbPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbPlayer.Location = new System.Drawing.Point(128, 12);
            this.rdbPlayer.Name = "rdbPlayer";
            this.rdbPlayer.Size = new System.Drawing.Size(75, 21);
            this.rdbPlayer.TabIndex = 81;
            this.rdbPlayer.TabStop = true;
            this.rdbPlayer.Text = "Player";
            this.rdbPlayer.UseVisualStyleBackColor = true;
            this.rdbPlayer.CheckedChanged += new System.EventHandler(this.rdbPlayer_CheckedChanged);
            // 
            // rdbClub
            // 
            this.rdbClub.AutoSize = true;
            this.rdbClub.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbClub.Location = new System.Drawing.Point(314, 12);
            this.rdbClub.Name = "rdbClub";
            this.rdbClub.Size = new System.Drawing.Size(61, 21);
            this.rdbClub.TabIndex = 82;
            this.rdbClub.Text = "Club";
            this.rdbClub.UseVisualStyleBackColor = true;
            this.rdbClub.CheckedChanged += new System.EventHandler(this.rdbClub_CheckedChanged);
            // 
            // frmGenerateEclockInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 320);
            this.Controls.Add(this.rdbClub);
            this.Controls.Add(this.rdbPlayer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtClubID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbClub);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtClubAbbreviation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAssignedTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtReaderID);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmGenerateEclockInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmGenerateEclockInfo";
            this.Load += new System.EventHandler(this.frmGenerateEclockInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.TextBox txtAssignedTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClubAbbreviation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtClubID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbClub;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rdbPlayer;
        private System.Windows.Forms.RadioButton rdbClub;
    }
}