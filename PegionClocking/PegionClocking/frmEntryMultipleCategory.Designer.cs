namespace PegionClocking
{
    partial class frmEntryMultipleCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEntryMultipleCategory));
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbCategoryList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBandNumber = new System.Windows.Forms.TextBox();
            this.txtStickerCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lstCategory = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(324, 130);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbCategoryList
            // 
            this.cmbCategoryList.FormattingEnabled = true;
            this.cmbCategoryList.Location = new System.Drawing.Point(25, 133);
            this.cmbCategoryList.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCategoryList.Name = "cmbCategoryList";
            this.cmbCategoryList.Size = new System.Drawing.Size(285, 24);
            this.cmbCategoryList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Band Number :";
            // 
            // txtBandNumber
            // 
            this.txtBandNumber.BackColor = System.Drawing.Color.White;
            this.txtBandNumber.Location = new System.Drawing.Point(125, 20);
            this.txtBandNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtBandNumber.Name = "txtBandNumber";
            this.txtBandNumber.ReadOnly = true;
            this.txtBandNumber.Size = new System.Drawing.Size(185, 22);
            this.txtBandNumber.TabIndex = 3;
            // 
            // txtStickerCode
            // 
            this.txtStickerCode.BackColor = System.Drawing.Color.White;
            this.txtStickerCode.Location = new System.Drawing.Point(125, 52);
            this.txtStickerCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtStickerCode.Name = "txtStickerCode";
            this.txtStickerCode.ReadOnly = true;
            this.txtStickerCode.Size = new System.Drawing.Size(185, 22);
            this.txtStickerCode.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 55);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sticker Number :";
            // 
            // lstCategory
            // 
            this.lstCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstCategory.FormattingEnabled = true;
            this.lstCategory.ItemHeight = 16;
            this.lstCategory.Location = new System.Drawing.Point(417, 130);
            this.lstCategory.Margin = new System.Windows.Forms.Padding(4);
            this.lstCategory.Name = "lstCategory";
            this.lstCategory.Size = new System.Drawing.Size(332, 196);
            this.lstCategory.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Select Group Category :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Bird Group Category :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(324, 166);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 28);
            this.button1.TabIndex = 11;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmEntryMultipleCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 335);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lstCategory);
            this.Controls.Add(this.txtStickerCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBandNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCategoryList);
            this.Controls.Add(this.btnAdd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEntryMultipleCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multiply Category";
            this.Load += new System.EventHandler(this.frmEntryMultipleCategory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbCategoryList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBandNumber;
        private System.Windows.Forms.TextBox txtStickerCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
    }
}