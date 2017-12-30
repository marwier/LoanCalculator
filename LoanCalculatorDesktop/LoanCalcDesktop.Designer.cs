using System.ComponentModel;
using System.Windows.Forms;

namespace LoanCalculatorDesktop
{
    partial class LoanCalcDesktop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanCalcDesktop));
            this.loanTypeComboBox = new System.Windows.Forms.ComboBox();
            this.paymentListView = new System.Windows.Forms.ListView();
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Capital = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Interest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.interestTextBox = new System.Windows.Forms.TextBox();
            this.interestLabel = new System.Windows.Forms.Label();
            this.loanAmountBox = new System.Windows.Forms.TextBox();
            this.loanYearsBox = new System.Windows.Forms.TextBox();
            this.amountLabel = new System.Windows.Forms.Label();
            this.yearsLabel = new System.Windows.Forms.Label();
            this.calculateButton = new System.Windows.Forms.Button();
            this.serverConnectingLabel = new System.Windows.Forms.Label();
            this.calculateValidation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loanTypeComboBox
            // 
            this.loanTypeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loanTypeComboBox.FormattingEnabled = true;
            this.loanTypeComboBox.Location = new System.Drawing.Point(12, 12);
            this.loanTypeComboBox.Name = "loanTypeComboBox";
            this.loanTypeComboBox.Size = new System.Drawing.Size(203, 28);
            this.loanTypeComboBox.TabIndex = 0;
            this.loanTypeComboBox.Text = "Select loan type...";
            this.loanTypeComboBox.UseWaitCursor = true;
            this.loanTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedItemChanged);
            // 
            // paymentListView
            // 
            this.paymentListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.paymentListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.Total,
            this.Capital,
            this.Interest});
            this.paymentListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.paymentListView.Location = new System.Drawing.Point(12, 101);
            this.paymentListView.Name = "paymentListView";
            this.paymentListView.Size = new System.Drawing.Size(677, 371);
            this.paymentListView.TabIndex = 1;
            this.paymentListView.UseCompatibleStateImageBehavior = false;
            this.paymentListView.UseWaitCursor = true;
            this.paymentListView.View = System.Windows.Forms.View.Details;
            // 
            // No
            // 
            this.No.Text = "No.";
            // 
            // Total
            // 
            this.Total.Text = "Total";
            this.Total.Width = 120;
            // 
            // Capital
            // 
            this.Capital.Text = "Capital";
            this.Capital.Width = 120;
            // 
            // Interest
            // 
            this.Interest.Text = "Interest";
            this.Interest.Width = 120;
            // 
            // interestTextBox
            // 
            this.interestTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.interestTextBox.Location = new System.Drawing.Point(115, 53);
            this.interestTextBox.Name = "interestTextBox";
            this.interestTextBox.ReadOnly = true;
            this.interestTextBox.Size = new System.Drawing.Size(100, 30);
            this.interestTextBox.TabIndex = 2;
            this.interestTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.interestTextBox.UseWaitCursor = true;
            // 
            // interestLabel
            // 
            this.interestLabel.AutoSize = true;
            this.interestLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.interestLabel.Location = new System.Drawing.Point(35, 60);
            this.interestLabel.Name = "interestLabel";
            this.interestLabel.Size = new System.Drawing.Size(65, 20);
            this.interestLabel.TabIndex = 3;
            this.interestLabel.Text = "Interest";
            this.interestLabel.UseWaitCursor = true;
            // 
            // loanAmountBox
            // 
            this.loanAmountBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loanAmountBox.Location = new System.Drawing.Point(393, 12);
            this.loanAmountBox.Name = "loanAmountBox";
            this.loanAmountBox.ReadOnly = true;
            this.loanAmountBox.Size = new System.Drawing.Size(100, 30);
            this.loanAmountBox.TabIndex = 4;
            this.loanAmountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.loanAmountBox.UseWaitCursor = true;
            // 
            // loanYearsBox
            // 
            this.loanYearsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loanYearsBox.Location = new System.Drawing.Point(393, 53);
            this.loanYearsBox.Name = "loanYearsBox";
            this.loanYearsBox.Size = new System.Drawing.Size(100, 30);
            this.loanYearsBox.TabIndex = 5;
            this.loanYearsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.loanYearsBox.UseWaitCursor = true;
            this.loanYearsBox.TextChanged += new System.EventHandler(this.LoanYearsBox_valueChanged);
            this.loanYearsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoanYearsBox_keyPressed);
            // 
            // amountLabel
            // 
            this.amountLabel.AutoSize = true;
            this.amountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.amountLabel.Location = new System.Drawing.Point(258, 19);
            this.amountLabel.Name = "amountLabel";
            this.amountLabel.Size = new System.Drawing.Size(108, 20);
            this.amountLabel.TabIndex = 6;
            this.amountLabel.Text = "Total Amount";
            this.amountLabel.UseWaitCursor = true;
            // 
            // yearsLabel
            // 
            this.yearsLabel.AutoSize = true;
            this.yearsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.yearsLabel.Location = new System.Drawing.Point(272, 60);
            this.yearsLabel.Name = "yearsLabel";
            this.yearsLabel.Size = new System.Drawing.Size(94, 20);
            this.yearsLabel.TabIndex = 7;
            this.yearsLabel.Text = "Total Years";
            this.yearsLabel.UseWaitCursor = true;
            // 
            // calculateButton
            // 
            this.calculateButton.Enabled = false;
            this.calculateButton.Location = new System.Drawing.Point(528, 12);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(161, 71);
            this.calculateButton.TabIndex = 8;
            this.calculateButton.Text = "Calculate!";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.UseWaitCursor = true;
            this.calculateButton.Click += new System.EventHandler(this.Button_CalculateAction);
            // 
            // serverConnectingLabel
            // 
            this.serverConnectingLabel.AutoSize = true;
            this.serverConnectingLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.serverConnectingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.serverConnectingLabel.Location = new System.Drawing.Point(206, 260);
            this.serverConnectingLabel.Name = "serverConnectingLabel";
            this.serverConnectingLabel.Size = new System.Drawing.Size(251, 26);
            this.serverConnectingLabel.TabIndex = 9;
            this.serverConnectingLabel.Text = "Connecting with server...";
            this.serverConnectingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.serverConnectingLabel.UseWaitCursor = true;
            // 
            // calculateValidation
            // 
            this.calculateValidation.AutoSize = true;
            this.calculateValidation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.calculateValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.calculateValidation.ForeColor = System.Drawing.Color.Red;
            this.calculateValidation.Location = new System.Drawing.Point(194, 286);
            this.calculateValidation.Name = "calculateValidation";
            this.calculateValidation.Size = new System.Drawing.Size(280, 29);
            this.calculateValidation.TabIndex = 10;
            this.calculateValidation.Text = "Provided incorrect data!";
            this.calculateValidation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.calculateValidation.UseWaitCursor = true;
            this.calculateValidation.Visible = false;
            // 
            // LoanCalcDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 484);
            this.Controls.Add(this.calculateValidation);
            this.Controls.Add(this.serverConnectingLabel);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.yearsLabel);
            this.Controls.Add(this.amountLabel);
            this.Controls.Add(this.loanYearsBox);
            this.Controls.Add(this.loanAmountBox);
            this.Controls.Add(this.interestLabel);
            this.Controls.Add(this.interestTextBox);
            this.Controls.Add(this.paymentListView);
            this.Controls.Add(this.loanTypeComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoanCalcDesktop";
            this.Text = "Loan Calculator - Desktop";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.LoanCalcDesktop_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox loanTypeComboBox;
        private ListView paymentListView;
        private TextBox interestTextBox;
        private Label interestLabel;
        private TextBox loanAmountBox;
        private TextBox loanYearsBox;
        private Label amountLabel;
        private Label yearsLabel;
        private Button calculateButton;
        private Label serverConnectingLabel;
        private Label calculateValidation;
        private ColumnHeader No;
        private ColumnHeader Total;
        private ColumnHeader Capital;
        private ColumnHeader Interest;
    }
}

