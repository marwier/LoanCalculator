
// ReSharper disable CoVariantArrayConversion

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using InterviewTask.Models;

namespace LoanCalculatorDesktop
{
    using static UInt16;

    public partial class LoanCalcDesktop : Form
    {
        private readonly WebApiConnector _connector;
        private readonly string _serverUrl;

        public LoanCalcDesktop()
        {
            _serverUrl = @"http://localhost:55735/";
            _connector = new WebApiConnector(this, _serverUrl);

            InitializeComponent();
        }

        private async void LoanCalcDesktop_Load(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                await _connector.GetLoanTypes();

                serverConnectingLabel.Hide();
                UseWaitCursor = false;
            }
            catch
            {
                MessageBox.Show($@"Could not connect with server {_serverUrl}. Check if server is running and try again.",
                    @"Connection failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        // Combobox related method(s)

        public void PopulateComboBox(List<LoanType> loanTypes)
        {
            loanTypeComboBox.Items.AddRange(loanTypes.ToArray());
        }

        private async void ComboBox_SelectedItemChanged(object sender, EventArgs e)
        {
            var loanTypeId = ((LoanType)loanTypeComboBox.SelectedItem).LoanTypeId;

            interestTextBox.Text = (await _connector.GetInterest(loanTypeId)).ToString("P");
            loanAmountBox.Text = (await _connector.GetAmount(loanTypeId)).ToString("0.00");

            if (loanTypeComboBox.SelectedItem == null)
                calculateButton.Enabled = false;
            else if (!string.IsNullOrEmpty(loanYearsBox.Text))
                calculateButton.Enabled = true;
        }

        // Listview related method(s)

        public async Task PopulateListView(List<Payment> payments)
        {
            var listViewRows = new List<ListViewItem>();

            await Task.Factory.StartNew(() =>
            {
                foreach (var payment in payments)
                {
                    listViewRows.Add(PopulateListViewRow(payment));
                }
            });

            paymentListView.Items.AddRange(listViewRows.ToArray());
        }

        private ListViewItem PopulateListViewRow(Payment payment)
        {
            // Payment's ID starts from 0 - incrementing only for display purposes.
            var newRow = new ListViewItem((payment.PaymentId + 1).ToString());

            newRow.SubItems.AddRange(new[]
            {
                payment.Total.ToString("0.00"),
                payment.Capital.ToString("0.00"),
                payment.Interest.ToString("0.00")
            });

            return newRow;
        }

        // Button related method(s)

        private async void Button_CalculateAction(object sender, EventArgs e)
        {
            // preparation part
            ushort loanTypeId;
            ushort numberOfYears;

            if (paymentListView.Items.Count != 0)
                paymentListView.Items.Clear();

            calculateButton.Enabled = false;
            calculateButton.Text = @"Calculating...";

            // validation part
            try
            {
                loanTypeId = ((LoanType)loanTypeComboBox.SelectedItem).LoanTypeId;
                numberOfYears = Parse(loanYearsBox.Text);

                calculateValidation.Hide();
            }
            catch
            {
                calculateValidation.Show();
                serverConnectingLabel.Hide();
                calculateButton.Enabled = true;
                calculateButton.Text = @"Calculate!";
                return;
            }

            // waiting for response
            serverConnectingLabel.Text = "Waiting for server response.\nPlease wait";
            serverConnectingLabel.Show();

            try
            {
                await _connector.GetPayments(loanTypeId, numberOfYears);

                serverConnectingLabel.Hide();
            }
            catch (Exception ex)
            {
                serverConnectingLabel.Text = ex.Message;
            }
            finally
            {
                calculateButton.Enabled = true;
                calculateButton.Text = @"Calculate!";
            }
        }

        // input box related method(s)

        private void LoanYearsBox_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoanYearsBox_valueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loanYearsBox.Text) || !int.TryParse(loanYearsBox.Text, out var _))
                calculateButton.Enabled = false;
            else if (loanTypeComboBox.SelectedItem != null)
                calculateButton.Enabled = true;
        }
    }
}