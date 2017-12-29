﻿
namespace LoanCalculatorDesktop
{
    using InterviewTask.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class LoanCalcDesktop : Form
    {
        private WebApiConnector _connector;

        public string ServerUrl
        {
            get => "http://localhost:55735/";
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
                this.UseWaitCursor = false;
            }
            catch
            {
                MessageBox.Show($"Could not connect with server {ServerUrl}. Check if server is running and try again.",
                    "Connection failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }

        // Combobox related method(s)

        public void PopulateComboBox(List<LoanType> loanTypes)
        {
            loanTypeComboBox.Items.AddRange(loanTypes.ToArray());
        }

        private async void comboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            var loanTypeID = (loanTypeComboBox.SelectedItem as LoanType).LoanTypeID;

            decimal interest = await _connector.GetInterest(loanTypeID);
            decimal amount = await _connector.GetAmount(loanTypeID);

            interestTextBox.Text = interest.ToString("P");
            loanAmountBox.Text = amount.ToString("0.00");
        }

        // Listview related method(s)

        public async Task PopulateListView(List<Payment> payments)
        {
            var listViewRows = new List<ListViewItem>();

            await Task.Factory.StartNew(() =>
            {
                foreach (var payment in payments)
                {
                    listViewRows.Add(populateListViewRow(payment));
                }
            });

            paymentListView.Items.AddRange(listViewRows.ToArray());
        }

        private ListViewItem populateListViewRow(Payment payment)
        {
            // Payment's ID starts from 0 - incrementing only for display purposes.
            var newRow = new ListViewItem((payment.PaymentID + 1).ToString());

            newRow.SubItems.AddRange(new string[]
            {
                payment.Total.ToString("0.00"),
                payment.Capital.ToString("0.00"),
                payment.Interest.ToString("0.00")
            });

            return newRow;
        }

        // Button related method(s)

        private async void button_CalculateAction(object sender, EventArgs e)
        {
            // preparation part
            UInt16 loanTypeID;
            UInt16 numberOfYears;

            if (paymentListView.Items.Count != 0)
                paymentListView.Items.Clear();

            calculateButton.Enabled = false;
            calculateButton.Text = "Calculating...";

            // validation part
            try
            {
                loanTypeID = (loanTypeComboBox.SelectedItem as LoanType).LoanTypeID;
                numberOfYears = UInt16.Parse(loanYearsBox.Text);

                calculateValidation.Hide();
            }
            catch
            {
                calculateValidation.Show();
                serverConnectingLabel.Hide();
                calculateButton.Enabled = true;
                calculateButton.Text = "Calculate!";
                return;
            }

            // waiting for response
            serverConnectingLabel.Text = "Waiting for server response.\nPlease wait";
            serverConnectingLabel.Show();

            try
            {
                await _connector.GetPayments(loanTypeID, numberOfYears);

                serverConnectingLabel.Hide();
            }
            catch
            {
                serverConnectingLabel.Text = "Too long response time";
            }
            finally
            {
                calculateButton.Enabled = true;
                calculateButton.Text = "Calculate!";
            }
        }
    }
}