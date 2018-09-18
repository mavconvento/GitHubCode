namespace PegionClocking
{
    partial class FrmCopyEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCopyEntry));
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBandNumber = new System.Windows.Forms.TextBox();
            this.txtStickerCode = new System.Windows.Forms.TextBox();
            this.labels = new System.Windows.Forms.Label();
            this.dtpDateRelease = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(536, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 67);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(679, 384);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Band Number :";
            // 
            // txtBandNumber
            // 
            this.txtBandNumber.BackColor = System.Drawing.Color.White;
            this.txtBandNumber.Location = new System.Drawing.Point(96, 35);
            this.txtBandNumber.Name = "txtBandNumber";
            this.txtBandNumber.ReadOnly = true;
            this.txtBandNumber.Size = new System.Drawing.Size(144, 20);
            this.txtBandNumber.TabIndex = 3;
            // 
            // txtStickerCode
            // 
            this.txtStickerCode.Location = new System.Drawing.Point(400, 34);
            this.txtStickerCode.Name = "txtStickerCode";
            this.txtStickerCode.Size = new System.Drawing.Size(130, 20);
            this.txtStickerCode.TabIndex = 5;
            // 
            // labels
            // 
            this.labels.AutoSize = true;
            this.labels.Location = new System.Drawing.Point(320, 37);
            this.labels.Name = "labels";
            this.labels.Size = new System.Drawing.Size(74, 13);
            this.labels.TabIndex = 4;
            this.labels.Text = "Sticker Code :";
            // 
            // dtpDateRelease
            // 
            this.dtpDateRelease.Location = new System.Drawing.Point(96, 9);
            this.dtpDateRelease.Name = "dtpDateRelease";
            this.dtpDateRelease.Size = new System.Drawing.Size(200, 20);
            this.dtpDateRelease.TabIndex = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 61;
            this.label5.Text = "Date Release :";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(302, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 62;
            this.button2.Text = "GO";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // FrmCopyEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 463);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dtpDateRelease);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStickerCode);
            this.Controls.Add(this.labels);
            this.Controls.Add(this.txtBandNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCopyEntry";
            this.Text = "Duplicate Entry";
            this.Load += new System.EventHandler(this.FrmCopyEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBandNumber;
        private System.Windows.Forms.TextBox txtStickerCode;
        private System.Windows.Forms.Label labels;
        private System.Windows.Forms.DateTimePicker dtpDateRelease;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
    }
}