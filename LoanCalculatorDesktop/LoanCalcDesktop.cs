
namespace LoanCalculatorDesktop
{
    using InterviewTask.Models;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class LoanCalcDesktop : Form
    {
        private WebApiConnector _connector;

        public string ServerUrl
        {
            get => "http://localhost:55735/";
        }

        public List<LoanType> LoanTypeList
        {
            set => this.populateComboBox(value);
        }

        public List<Payment> PaymentList
        {
            set => this.populateListView(value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LoanCalcDesktop()
        {
            _connector = new WebApiConnector(this, ServerUrl);
            InitializeComponent();
        }

        /// <summary>
        /// On load action
        /// </summary>
        private async void loanCalcDesktop_Load(object sender, EventArgs e)
        {
            try
            {
                await _connector.GetLoanTypes();
                serverConnectingLabel.Hide();
            }
            catch
            {
                MessageBox.Show($"Could not connect with server {ServerUrl}. Check if server is running and try again.",
                    "Connection failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }

        // Combobox related method(s)

        private void populateComboBox(List<LoanType> loanTypes)
        {
            loanTypeComboBox.Items.AddRange(loanTypes.ToArray());
        }

        private async void comboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            decimal interest = await _connector.GetInterest(
                (loanTypeComboBox.SelectedItem as LoanType).LoanTypeID); // async

            interestTextBox.Text = interest.ToString("P");
        }

        // Listview related method(s)

        private void populateListView(List<Payment> payments)
        {
            if (paymentListView.Items.Count != 0)
                paymentListView.Items.Clear();

            foreach (var payment in payments)
            {
                paymentListView.Items.Add(populateListViewRow(payment));
            }
        }

        private ListViewItem populateListViewRow(Payment payment)
        {
            // Payment's ID starts from 0 - incrementing only for display purposes.
            var newRow = new ListViewItem((payment.PaymentID + 1).ToString());

            newRow.SubItems.AddRange(new string[]
            {
                payment.Capital.ToString("0.00"),
                payment.Interest.ToString("0.00"),
                payment.Total.ToString("0.00")
            });

            return newRow;
        }

        // Button related method(s)

        private async void button_CalculateAction(object sender, EventArgs e)
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

            await _connector.GetPayments(loanTypeID, totalAmount, numberOfYears);
        }
    }
}