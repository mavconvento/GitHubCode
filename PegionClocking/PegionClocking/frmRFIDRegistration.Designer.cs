namespace PegionClocking
{
    partial class frmRFIDRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRFIDRegistration));
            this.txtClubID = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbClub = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClubAbbreviation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtClubID
            // 
            this.txtClubID.BackColor = System.Drawing.Color.White;
            this.txtClubID.Location = new System.Drawing.Point(366, 12);
            this.txtClubID.Name = "txtClubID";
            this.txtClubID.ReadOnly = true;
            this.txtClubID.Size = new System.Drawing.Size(56, 20);
            this.txtClubID.TabIndex = 82;
            this.txtClubID.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 81;
            this.label7.Text = "Club ID";
            this.label7.Visible = false;
            // 
            // cmbClub
            // 
            this.cmbClub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClub.FormattingEnabled = true;
            this.cmbClub.Location = new System.Drawing.Point(10, 37);
            this.cmbClub.Name = "cmbClub";
            this.cmbClub.Size = new System.Drawing.Size(415, 21);
            this.cmbClub.TabIndex = 79;
            this.cmbClub.SelectedIndexChanged += new System.EventHandler(this.cmbClub_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Club Name :";
            // 
            // txtClubAbbreviation
            // 
            this.txtClubAbbreviation.Location = new System.Drawing.Point(128, 67);
            this.txtClubAbbreviation.Margin = new System.Windows.Forms.Padding(2);
            this.txtClubAbbreviation.Name = "txtClubAbbreviation";
            this.txtClubAbbreviation.Size = new System.Drawing.Size(223, 20);
            this.txtClubAbbreviation.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Club Abbreviation :";
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(128, 96);
            this.txtReaderID.Margin = new System.Windows.Forms.Padding(2);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(223, 20);
            this.txtReaderID.TabIndex = 84;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "RFID :";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(169, 178);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 24);
            this.button1.TabIndex = 85;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(128, 132);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 86;
            this.checkBox1.Text = "Overwrite";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // frmRFIDRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PegionClocking.Properties.Resources.background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(440, 212);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtReaderID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClubID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbClub);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtClubAbbreviation);
            this.Controls.Add(this.label4);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRFIDRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RFID Registration";
            this.Load += new System.EventHandler(this.frmRFIDRegistration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtClubID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbClub;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClubAbbreviation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}