namespace Eclock
{
    partial class frmTakePicture
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
            this.btnTakePicture = new System.Windows.Forms.Button();
            this.pbPigeonPicture = new System.Windows.Forms.PictureBox();
            this.btnDone = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPigeonPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTakePicture
            // 
            this.btnTakePicture.Location = new System.Drawing.Point(212, 322);
            this.btnTakePicture.Name = "btnTakePicture";
            this.btnTakePicture.Size = new System.Drawing.Size(135, 29);
            this.btnTakePicture.TabIndex = 0;
            this.btnTakePicture.Text = "Get Picture";
            this.btnTakePicture.UseVisualStyleBackColor = true;
            this.btnTakePicture.Click += new System.EventHandler(this.btnTakePicture_Click);
            // 
            // pbPigeonPicture
            // 
            this.pbPigeonPicture.BackColor = System.Drawing.Color.White;
            this.pbPigeonPicture.Location = new System.Drawing.Point(12, 12);
            this.pbPigeonPicture.Name = "pbPigeonPicture";
            this.pbPigeonPicture.Size = new System.Drawing.Size(476, 304);
            this.pbPigeonPicture.TabIndex = 1;
            this.pbPigeonPicture.TabStop = false;
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(353, 322);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(135, 29);
            this.btnDone.TabIndex = 2;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // frmTakePicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 357);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.pbPigeonPicture);
            this.Controls.Add(this.btnTakePicture);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTakePicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Take Picture";
            ((System.ComponentModel.ISupportInitialize)(this.pbPigeonPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTakePicture;
        private System.Windows.Forms.PictureBox pbPigeonPicture;
        private System.Windows.Forms.Button btnDone;
    }
}