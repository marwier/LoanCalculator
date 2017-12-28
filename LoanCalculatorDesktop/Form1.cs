using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanCalculatorDesktop
{
    public partial class Form1 : Form
    {
        public string ServerUrl
        {
            get
            {
                return "http://localhost:55735/";
            }
        }

        public List<LoanType> LoanTypeList
        {
            set
            {
                this.PopulateLoanTypeComboBox(value);
            }
        }

        private WebApiConnector Connector { get; set; }

        public List<Payment> PaymentList
        {
            set
            {
                this.PopulatePaymentListView(value);
            }
        }


        public Form1()
        {
            Connector = new WebApiConnector(this, ServerUrl);
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                await Connector.GetLoanTypes(); // async
                serverConnectingLabel.Hide();
            }
            catch
            {
                MessageBox.Show($"Could not connect with server {ServerUrl}. Check if server is running and try again.",
                    "Connection failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }

        private void PopulateLoanTypeComboBox(List<LoanType> loanTypes)
        {
            loanTypeComboBox.Items.AddRange(loanTypes.ToArray());
        }

        private void PopulatePaymentListView(List<Payment> payments)
        {
            if (paymentListView.Items.Count != 0)
                paymentListView.Items.Clear();

            foreach (var payment in payments)
            {
                paymentListView.Items.Add(PopulatePaymentRow(payment));
            }
        }

        private ListViewItem PopulatePaymentRow(Payment payment)
        {
            var newRow = new ListViewItem(payment.PaymentNo.ToString());

            newRow.SubItems.AddRange(new string[]
            {
                payment.Capital.ToString("0.00"),
                payment.Interest.ToString("0.00"),
                payment.Total.ToString("0.00")
            });

            return newRow;
        }

        private async void LoanTypeComboBox_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            decimal interest = await Connector.GetInterest(
                (loanTypeComboBox.SelectedItem as LoanType).LoanTypeID); // async

            interestTextBox.Text = interest.ToString("P");
        }

        private async void CalculateButton_Click(object sender, EventArgs e)
        {
            UInt16 loanTypeID;
            Decimal totalAmount;
            UInt16 numberOfYears;

            try
            {
                loanTypeID = (loanTypeComboBox.SelectedItem as LoanType).LoanTypeID;
                totalAmount = Decimal.Parse(loanAmountBox.Text);
                numberOfYears = UInt16.Parse(loanYearsBox.Text);

                calculateValidation.Visible = false;
            }
            catch
            {
                calculateValidation.Visible = true;
                return;
            }

            await Connector.GetPayments(loanTypeID, totalAmount, numberOfYears);
        }
    }
}