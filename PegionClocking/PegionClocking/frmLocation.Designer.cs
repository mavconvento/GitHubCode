namespace PegionClocking
{
    partial class frmLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocation));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDistanceLongSeconds = new System.Windows.Forms.TextBox();
            this.txtDistanceLongMinutes = new System.Windows.Forms.TextBox();
            this.txtDistanceLatMinutes = new System.Windows.Forms.TextBox();
            this.txtDistanceLatSeconds = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbLongSign = new System.Windows.Forms.ComboBox();
            this.txtDistanceLatDegree = new System.Windows.Forms.TextBox();
            this.cmbLatSign = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDistanceLongDegree = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocationName = new System.Windows.Forms.TextBox();
            this.txtLocationID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.txtDistanceLongSeconds);
            this.groupBox1.Controls.Add(this.txtDistanceLongMinutes);
            this.groupBox1.Controls.Add(this.txtDistanceLatMinutes);
            this.groupBox1.Controls.Add(this.txtDistanceLatSeconds);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.cmbLongSign);
            this.groupBox1.Controls.Add(this.txtDistanceLatDegree);
            this.groupBox1.Controls.Add(this.cmbLatSign);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtDistanceLongDegree);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 99);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coordinates";
            // 
            // txtDistanceLongSeconds
            // 
            this.txtDistanceLongSeconds.Location = new System.Drawing.Point(194, 55);
            this.txtDistanceLongSeconds.Name = "txtDistanceLongSeconds";
            this.txtDistanceLongSeconds.Size = new System.Drawing.Size(49, 20);
            this.txtDistanceLongSeconds.TabIndex = 9;
            this.txtDistanceLongSeconds.Text = "0";
            this.txtDistanceLongSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDistanceLongMinutes
            // 
            this.txtDistanceLongMinutes.Location = new System.Drawing.Point(139, 55);
            this.txtDistanceLongMinutes.Name = "txtDistanceLongMinutes";
            this.txtDistanceLongMinutes.Size = new System.Drawing.Size(44, 20);
            this.txtDistanceLongMinutes.TabIndex = 8;
            this.txtDistanceLongMinutes.Text = "0";
            this.txtDistanceLongMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDistanceLatMinutes
            // 
            this.txtDistanceLatMinutes.Location = new System.Drawing.Point(139, 27);
            this.txtDistanceLatMinutes.Name = "txtDistanceLatMinutes";
            this.txtDistanceLatMinutes.Size = new System.Drawing.Size(44, 20);
            this.txtDistanceLatMinutes.TabIndex = 4;
            this.txtDistanceLatMinutes.Text = "0";
            this.txtDistanceLatMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDistanceLatSeconds
            // 
            this.txtDistanceLatSeconds.Location = new System.Drawing.Point(194, 27);
            this.txtDistanceLatSeconds.Name = "txtDistanceLatSeconds";
            this.txtDistanceLatSeconds.Size = new System.Drawing.Size(49, 20);
            this.txtDistanceLatSeconds.TabIndex = 5;
            this.txtDistanceLatSeconds.Text = "0";
            this.txtDistanceLatSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(29, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 13);
            this.label21.TabIndex = 42;
            this.label21.Text = "Latitude :";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(19, 57);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(60, 13);
            this.label22.TabIndex = 43;
            this.label22.Text = "Longitude :";
            // 
            // cmbLongSign
            // 
            this.cmbLongSign.FormattingEnabled = true;
            this.cmbLongSign.Location = new System.Drawing.Point(262, 55);
            this.cmbLongSign.Name = "cmbLongSign";
            this.cmbLongSign.Size = new System.Drawing.Size(51, 21);
            this.cmbLongSign.Sorted = true;
            this.cmbLongSign.TabIndex = 10;
            // 
            // txtDistanceLatDegree
            // 
            this.txtDistanceLatDegree.Location = new System.Drawing.Point(85, 28);
            this.txtDistanceLatDegree.Name = "txtDistanceLatDegree";
            this.txtDistanceLatDegree.Size = new System.Drawing.Size(40, 20);
            this.txtDistanceLatDegree.TabIndex = 3;
            this.txtDistanceLatDegree.Text = "0";
            this.txtDistanceLatDegree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbLatSign
            // 
            this.cmbLatSign.FormattingEnabled = true;
            this.cmbLatSign.Location = new System.Drawing.Point(262, 27);
            this.cmbLatSign.Name = "cmbLatSign";
            this.cmbLatSign.Size = new System.Drawing.Size(51, 21);
            this.cmbLatSign.Sorted = true;
            this.cmbLatSign.TabIndex = 6;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(243, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 16);
            this.label17.TabIndex = 32;
            this.label17.Text = "\"";
            // 
            // txtDistanceLongDegree
            // 
            this.txtDistanceLongDegree.Location = new System.Drawing.Point(85, 56);
            this.txtDistanceLongDegree.Name = "txtDistanceLongDegree";
            this.txtDistanceLongDegree.Size = new System.Drawing.Size(40, 20);
            this.txtDistanceLongDegree.TabIndex = 7;
            this.txtDistanceLongDegree.Text = "0";
            this.txtDistanceLongDegree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(123, 52);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 16);
            this.label18.TabIndex = 36;
            this.label18.Text = "o";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(181, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(12, 16);
            this.label16.TabIndex = 30;
            this.label16.Text = "\'";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(181, 54);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(12, 16);
            this.label19.TabIndex = 37;
            this.label19.Text = "\'";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(123, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 16);
            this.label15.TabIndex = 28;
            this.label15.Text = "o";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(243, 54);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 16);
            this.label20.TabIndex = 38;
            this.label20.Text = "\"";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Location Name";
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new System.Drawing.Point(98, 39);
            this.txtLocationName.MaxLength = 15;
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Size = new System.Drawing.Size(242, 20);
            this.txtLocationName.TabIndex = 1;
            // 
            // txtLocationID
            // 
            this.txtLocationID.BackColor = System.Drawing.Color.White;
            this.txtLocationID.Location = new System.Drawing.Point(98, 13);
            this.txtLocationID.Name = "txtLocationID";
            this.txtLocationID.ReadOnly = true;
            this.txtLocationID.Size = new System.Drawing.Size(100, 20);
            this.txtLocationID.TabIndex = 50;
            this.txtLocationID.TabStop = false;
            this.txtLocationID.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "LocationID";
            this.label2.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Teal;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(44, 220);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Teal;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(125, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "&Clear";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Teal;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(206, 220);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "&Delete";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(359, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(514, 218);
            this.dataGridView1.TabIndex = 54;
            this.dataGridView1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(356, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Location List";
            // 
            // cmbRegion
            // 
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(98, 71);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(197, 21);
            this.cmbRegion.Sorted = true;
            this.cmbRegion.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(13, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Region Name";
            // 
            // frmLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PegionClocking.Properties.Resources.background2;
            this.ClientSize = new System.Drawing.Size(885, 255);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbRegion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtLocationID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocationName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLocation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Location";
            this.Load += new System.EventHandler(this.frmLocation_Load);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocationName;
        private System.Windows.Forms.TextBox txtLocationID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label label4;
    }
}