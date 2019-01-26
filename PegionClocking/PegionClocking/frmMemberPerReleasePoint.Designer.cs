namespace PegionClocking
{
    partial class frmMemberPerReleasePoint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberPerReleasePoint));
            this.dtMemberList = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbLongSign = new System.Windows.Forms.ComboBox();
            this.txtDistanceLatDegree = new System.Windows.Forms.TextBox();
            this.txtDistanceLongSeconds = new System.Windows.Forms.TextBox();
            this.txtDistanceLongMinutes = new System.Windows.Forms.TextBox();
            this.cmbLatSign = new System.Windows.Forms.ComboBox();
            this.txtDistanceLongDegree = new System.Windows.Forms.TextBox();
            this.txtDistanceLatMinutes = new System.Windows.Forms.TextBox();
            this.txtDistanceLatSeconds = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocationID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtMemberList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtMemberList
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dtMemberList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtMemberList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtMemberList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dtMemberList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtMemberList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtMemberList.Location = new System.Drawing.Point(12, 145);
            this.dtMemberList.Name = "dtMemberList";
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dtMemberList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtMemberList.Size = new System.Drawing.Size(811, 439);
            this.dtMemberList.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(593, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate to Excel File";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(118, 12);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(264, 21);
            this.cmbLocation.Sorted = true;
            this.cmbLocation.TabIndex = 57;
            this.cmbLocation.SelectedIndexChanged += new System.EventHandler(this.cmbLocation_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Location Name :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cmbLongSign);
            this.groupBox1.Controls.Add(this.txtDistanceLatDegree);
            this.groupBox1.Controls.Add(this.txtDistanceLongSeconds);
            this.groupBox1.Controls.Add(this.txtDistanceLongMinutes);
            this.groupBox1.Controls.Add(this.cmbLatSign);
            this.groupBox1.Controls.Add(this.txtDistanceLongDegree);
            this.groupBox1.Controls.Add(this.txtDistanceLatMinutes);
            this.groupBox1.Controls.Add(this.txtDistanceLatSeconds);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(530, 74);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coordinates";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(9, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Latitude :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(290, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(78, 13);
            this.label22.TabIndex = 43;
            this.label22.Text = "Longhitude :";
            // 
            // cmbLongSign
            // 
            this.cmbLongSign.Enabled = false;
            this.cmbLongSign.FormattingEnabled = true;
            this.cmbLongSign.Location = new System.Drawing.Point(471, 34);
            this.cmbLongSign.Name = "cmbLongSign";
            this.cmbLongSign.Size = new System.Drawing.Size(51, 21);
            this.cmbLongSign.Sorted = true;
            this.cmbLongSign.TabIndex = 9;
            // 
            // txtDistanceLatDegree
            // 
            this.txtDistanceLatDegree.BackColor = System.Drawing.Color.White;
            this.txtDistanceLatDegree.Location = new System.Drawing.Point(12, 37);
            this.txtDistanceLatDegree.Name = "txtDistanceLatDegree";
            this.txtDistanceLatDegree.ReadOnly = true;
            this.txtDistanceLatDegree.Size = new System.Drawing.Size(32, 20);
            this.txtDistanceLatDegree.TabIndex = 2;
            // 
            // txtDistanceLongSeconds
            // 
            this.txtDistanceLongSeconds.BackColor = System.Drawing.Color.White;
            this.txtDistanceLongSeconds.Location = new System.Drawing.Point(387, 35);
            this.txtDistanceLongSeconds.Name = "txtDistanceLongSeconds";
            this.txtDistanceLongSeconds.ReadOnly = true;
            this.txtDistanceLongSeconds.Size = new System.Drawing.Size(64, 20);
            this.txtDistanceLongSeconds.TabIndex = 8;
            // 
            // txtDistanceLongMinutes
            // 
            this.txtDistanceLongMinutes.BackColor = System.Drawing.Color.White;
            this.txtDistanceLongMinutes.Location = new System.Drawing.Point(340, 35);
            this.txtDistanceLongMinutes.Name = "txtDistanceLongMinutes";
            this.txtDistanceLongMinutes.ReadOnly = true;
            this.txtDistanceLongMinutes.Size = new System.Drawing.Size(32, 20);
            this.txtDistanceLongMinutes.TabIndex = 7;
            // 
            // cmbLatSign
            // 
            this.cmbLatSign.Enabled = false;
            this.cmbLatSign.FormattingEnabled = true;
            this.cmbLatSign.Location = new System.Drawing.Point(175, 37);
            this.cmbLatSign.Name = "cmbLatSign";
            this.cmbLatSign.Size = new System.Drawing.Size(51, 21);
            this.cmbLatSign.Sorted = true;
            this.cmbLatSign.TabIndex = 5;
            // 
            // txtDistanceLongDegree
            // 
            this.txtDistanceLongDegree.BackColor = System.Drawing.Color.White;
            this.txtDistanceLongDegree.Location = new System.Drawing.Point(293, 35);
            this.txtDistanceLongDegree.Name = "txtDistanceLongDegree";
            this.txtDistanceLongDegree.ReadOnly = true;
            this.txtDistanceLongDegree.Size = new System.Drawing.Size(32, 20);
            this.txtDistanceLongDegree.TabIndex = 6;
            // 
            // txtDistanceLatMinutes
            // 
            this.txtDistanceLatMinutes.BackColor = System.Drawing.Color.White;
            this.txtDistanceLatMinutes.Location = new System.Drawing.Point(59, 37);
            this.txtDistanceLatMinutes.Name = "txtDistanceLatMinutes";
            this.txtDistanceLatMinutes.ReadOnly = true;
            this.txtDistanceLatMinutes.Size = new System.Drawing.Size(32, 20);
            this.txtDistanceLatMinutes.TabIndex = 3;
            // 
            // txtDistanceLatSeconds
            // 
            this.txtDistanceLatSeconds.BackColor = System.Drawing.Color.White;
            this.txtDistanceLatSeconds.Location = new System.Drawing.Point(106, 37);
            this.txtDistanceLatSeconds.Name = "txtDistanceLatSeconds";
            this.txtDistanceLatSeconds.ReadOnly = true;
            this.txtDistanceLatSeconds.Size = new System.Drawing.Size(55, 20);
            this.txtDistanceLatSeconds.TabIndex = 4;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(160, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 16);
            this.label17.TabIndex = 32;
            this.label17.Text = "\"";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(323, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 16);
            this.label18.TabIndex = 36;
            this.label18.Text = "o";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(89, 36);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 16);
            this.label16.TabIndex = 30;
            this.label16.Text = "\'";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(370, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(12, 16);
            this.label19.TabIndex = 37;
            this.label19.Text = "\'";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(42, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 16);
            this.label15.TabIndex = 28;
            this.label15.Text = "o";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(451, 34);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 16);
            this.label20.TabIndex = 38;
            this.label20.Text = "\"";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 18);
            this.label1.TabIndex = 60;
            this.label1.Text = "Member List per Release Point";
            // 
            // txtLocationID
            // 
            this.txtLocationID.BackColor = System.Drawing.Color.White;
            this.txtLocationID.Location = new System.Drawing.Point(771, 25);
            this.txtLocationID.Name = "txtLocationID";
            this.txtLocationID.ReadOnly = true;
            this.txtLocationID.Size = new System.Drawing.Size(52, 20);
            this.txtLocationID.TabIndex = 64;
            this.txtLocationID.TabStop = false;
            this.txtLocationID.Text = "0";
            this.txtLocationID.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(755, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 63;
            this.label6.Text = "Location ID :";
            this.label6.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(388, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 65;
            this.button2.Text = "VIEW";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmMemberPerReleasePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PegionClocking.Properties.Resources.background2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(835, 596);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtLocationID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtMemberList);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMemberPerReleasePoint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member Distance per Release Point";
            this.Load += new System.EventHandler(this.frmMemberPerReleasePoint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtMemberList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtMemberList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cmbLongSign;
        private System.Windows.Forms.TextBox txtDistanceLatDegree;
        private System.Windows.Forms.TextBox txtDistanceLongSeconds;
        private System.Windows.Forms.TextBox txtDistanceLongMinutes;
        private System.Windows.Forms.ComboBox cmbLatSign;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDistanceLongDegree;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDistanceLatMinutes;
        private System.Windows.Forms.TextBox txtDistanceLatSeconds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocationID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}