namespace PegionClocking
{
    partial class frmTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransaction));
            this.cmbClub = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtBillingSummary = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUnitPrize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalCost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBillingNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtTransactionDetailsList = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTransactionType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbParticular = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPreviousBalance = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPaymentAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbOrderStatus = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dpPaymentDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtBillingSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTransactionDetailsList)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbClub
            // 
            this.cmbClub.FormattingEnabled = true;
            this.cmbClub.Location = new System.Drawing.Point(155, 80);
            this.cmbClub.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClub.Name = "cmbClub";
            this.cmbClub.Size = new System.Drawing.Size(481, 24);
            this.cmbClub.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 78);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Club Name :";
            // 
            // dtBillingSummary
            // 
            this.dtBillingSummary.AllowUserToOrderColumns = true;
            this.dtBillingSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtBillingSummary.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dtBillingSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtBillingSummary.Location = new System.Drawing.Point(765, 15);
            this.dtBillingSummary.Margin = new System.Windows.Forms.Padding(4);
            this.dtBillingSummary.Name = "dtBillingSummary";
            this.dtBillingSummary.Size = new System.Drawing.Size(780, 572);
            this.dtBillingSummary.TabIndex = 4;
            this.dtBillingSummary.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Particulars :";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BackColor = System.Drawing.Color.White;
            this.txtQuantity.Location = new System.Drawing.Point(155, 144);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(212, 22);
            this.txtQuantity.TabIndex = 7;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Quantity :";
            // 
            // txtUnitPrize
            // 
            this.txtUnitPrize.BackColor = System.Drawing.Color.White;
            this.txtUnitPrize.Location = new System.Drawing.Point(155, 176);
            this.txtUnitPrize.Margin = new System.Windows.Forms.Padding(4);
            this.txtUnitPrize.Name = "txtUnitPrize";
            this.txtUnitPrize.Size = new System.Drawing.Size(212, 22);
            this.txtUnitPrize.TabIndex = 8;
            this.txtUnitPrize.TextChanged += new System.EventHandler(this.txtUnitPrize_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(72, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Unit Prize :";
            // 
            // txtTotalCost
            // 
            this.txtTotalCost.BackColor = System.Drawing.Color.White;
            this.txtTotalCost.Location = new System.Drawing.Point(155, 208);
            this.txtTotalCost.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalCost.Name = "txtTotalCost";
            this.txtTotalCost.Size = new System.Drawing.Size(212, 22);
            this.txtTotalCost.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(68, 212);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Total Cost :";
            // 
            // txtBillingNo
            // 
            this.txtBillingNo.BackColor = System.Drawing.Color.White;
            this.txtBillingNo.Location = new System.Drawing.Point(155, 17);
            this.txtBillingNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtBillingNo.Name = "txtBillingNo";
            this.txtBillingNo.Size = new System.Drawing.Size(153, 22);
            this.txtBillingNo.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Billing No. :";
            // 
            // dtTransactionDetailsList
            // 
            this.dtTransactionDetailsList.AllowUserToOrderColumns = true;
            this.dtTransactionDetailsList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtTransactionDetailsList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dtTransactionDetailsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtTransactionDetailsList.Location = new System.Drawing.Point(20, 293);
            this.dtTransactionDetailsList.Margin = new System.Windows.Forms.Padding(4);
            this.dtTransactionDetailsList.Name = "dtTransactionDetailsList";
            this.dtTransactionDetailsList.Size = new System.Drawing.Size(737, 199);
            this.dtTransactionDetailsList.TabIndex = 15;
            this.dtTransactionDetailsList.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 273);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Item Summary :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Transaction Type :";
            // 
            // cmbTransactionType
            // 
            this.cmbTransactionType.FormattingEnabled = true;
            this.cmbTransactionType.Location = new System.Drawing.Point(155, 47);
            this.cmbTransactionType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTransactionType.Name = "cmbTransactionType";
            this.cmbTransactionType.Size = new System.Drawing.Size(153, 24);
            this.cmbTransactionType.TabIndex = 4;
            this.cmbTransactionType.SelectedIndexChanged += new System.EventHandler(this.cmbTransactionType_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(423, 559);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Generate Billing Statement";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbParticular
            // 
            this.cmbParticular.FormattingEnabled = true;
            this.cmbParticular.Location = new System.Drawing.Point(155, 111);
            this.cmbParticular.Margin = new System.Windows.Forms.Padding(4);
            this.cmbParticular.Name = "cmbParticular";
            this.cmbParticular.Size = new System.Drawing.Size(381, 24);
            this.cmbParticular.TabIndex = 6;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(645, 559);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(317, 15);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 28);
            this.button3.TabIndex = 2;
            this.button3.Text = "Search";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(408, 15);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 28);
            this.button4.TabIndex = 3;
            this.button4.Text = "Get Billing No.";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.White;
            this.txtTotalAmount.Location = new System.Drawing.Point(23, 523);
            this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(153, 22);
            this.txtTotalAmount.TabIndex = 25;
            this.txtTotalAmount.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 503);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 17);
            this.label9.TabIndex = 24;
            this.label9.Text = "Total Amount :";
            // 
            // txtPayment
            // 
            this.txtPayment.BackColor = System.Drawing.Color.White;
            this.txtPayment.Location = new System.Drawing.Point(185, 523);
            this.txtPayment.Margin = new System.Windows.Forms.Padding(4);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.Size = new System.Drawing.Size(153, 22);
            this.txtPayment.TabIndex = 27;
            this.txtPayment.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 503);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "Payment";
            // 
            // txtBalance
            // 
            this.txtBalance.BackColor = System.Drawing.Color.White;
            this.txtBalance.Location = new System.Drawing.Point(348, 523);
            this.txtBalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(153, 22);
            this.txtBalance.TabIndex = 29;
            this.txtBalance.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(344, 503);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 17);
            this.label11.TabIndex = 28;
            this.label11.Text = "Balance";
            // 
            // txtPreviousBalance
            // 
            this.txtPreviousBalance.BackColor = System.Drawing.Color.White;
            this.txtPreviousBalance.Location = new System.Drawing.Point(596, 523);
            this.txtPreviousBalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtPreviousBalance.Name = "txtPreviousBalance";
            this.txtPreviousBalance.Size = new System.Drawing.Size(153, 22);
            this.txtPreviousBalance.TabIndex = 31;
            this.txtPreviousBalance.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(592, 503);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(118, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "Previous Balance";
            // 
            // txtPaymentAmount
            // 
            this.txtPaymentAmount.BackColor = System.Drawing.Color.White;
            this.txtPaymentAmount.Location = new System.Drawing.Point(491, 176);
            this.txtPaymentAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaymentAmount.Name = "txtPaymentAmount";
            this.txtPaymentAmount.Size = new System.Drawing.Size(265, 22);
            this.txtPaymentAmount.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(376, 180);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 17);
            this.label13.TabIndex = 32;
            this.label13.Text = "Total Payment :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(51, 241);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 17);
            this.label14.TabIndex = 33;
            this.label14.Text = "Order Status :";
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.FormattingEnabled = true;
            this.cmbOrderStatus.Location = new System.Drawing.Point(155, 238);
            this.cmbOrderStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(212, 24);
            this.cmbOrderStatus.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(375, 213);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(226, 17);
            this.label15.TabIndex = 35;
            this.label15.Text = "Transaction Date / Payment Date :";
            // 
            // dpPaymentDate
            // 
            this.dpPaymentDate.Location = new System.Drawing.Point(379, 241);
            this.dpPaymentDate.Margin = new System.Windows.Forms.Padding(4);
            this.dpPaymentDate.Name = "dpPaymentDate";
            this.dpPaymentDate.Size = new System.Drawing.Size(265, 22);
            this.dpPaymentDate.TabIndex = 36;
            // 
            // frmTransaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1561, 602);
            this.Controls.Add(this.dpPaymentDate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbOrderStatus);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtPaymentAmount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtPreviousBalance);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPayment);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbParticular);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbTransactionType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtTransactionDetailsList);
            this.Controls.Add(this.txtBillingNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTotalCost);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUnitPrize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtBillingSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbClub);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BillingStatement";
            this.Load += new System.EventHandler(this.frmTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtBillingSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTransactionDetailsList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClub;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtBillingSummary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUnitPrize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBillingNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dtTransactionDetailsList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTransactionType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbParticular;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPayment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPreviousBalance;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPaymentAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbOrderStatus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dpPaymentDate;
    }
}