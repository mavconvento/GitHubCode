namespace PegionClocking
{
    partial class frmStickerGeneration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStickerGeneration));
            this.btnBrowseTemplate = new System.Windows.Forms.Button();
            this.txtTemplate = new System.Windows.Forms.TextBox();
            this.btnGenerateSticker = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFileCount = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBrowseTemplate
            // 
            this.btnBrowseTemplate.Location = new System.Drawing.Point(581, 44);
            this.btnBrowseTemplate.Name = "btnBrowseTemplate";
            this.btnBrowseTemplate.Size = new System.Drawing.Size(100, 23);
            this.btnBrowseTemplate.TabIndex = 0;
            this.btnBrowseTemplate.Text = "Browse Template";
            this.btnBrowseTemplate.UseVisualStyleBackColor = true;
            this.btnBrowseTemplate.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTemplate
            // 
            this.txtTemplate.Location = new System.Drawing.Point(67, 44);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(508, 20);
            this.txtTemplate.TabIndex = 1;
            // 
            // btnGenerateSticker
            // 
            this.btnGenerateSticker.Location = new System.Drawing.Point(210, 152);
            this.btnGenerateSticker.Name = "btnGenerateSticker";
            this.btnGenerateSticker.Size = new System.Drawing.Size(161, 32);
            this.btnGenerateSticker.TabIndex = 2;
            this.btnGenerateSticker.Text = "Generate Sticker for Printing";
            this.btnGenerateSticker.UseVisualStyleBackColor = true;
            this.btnGenerateSticker.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Template";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Destination";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(67, 70);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(508, 20);
            this.txtDestination.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "File Name";
            // 
            // txtFilename
            // 
            this.txtFilename.Location = new System.Drawing.Point(67, 96);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(327, 20);
            this.txtFilename.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "File Count";
            // 
            // txtFileCount
            // 
            this.txtFileCount.Location = new System.Drawing.Point(67, 18);
            this.txtFileCount.Name = "txtFileCount";
            this.txtFileCount.Size = new System.Drawing.Size(121, 20);
            this.txtFileCount.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(377, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 32);
            this.button1.TabIndex = 11;
            this.button1.Text = "Generate Card for Printing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmStickerGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 204);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFileCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerateSticker);
            this.Controls.Add(this.txtTemplate);
            this.Controls.Add(this.btnBrowseTemplate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStickerGeneration";
            this.Text = "Sticker / Card Generation";
            this.Load += new System.EventHandler(this.frmStickerGeneration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowseTemplate;
        private System.Windows.Forms.TextBox txtTemplate;
        private System.Windows.Forms.Button btnGenerateSticker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileCount;
        private System.Windows.Forms.Button button1;
    }
}