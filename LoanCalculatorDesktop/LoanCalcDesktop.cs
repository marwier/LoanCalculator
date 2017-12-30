
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

        public LoanCalcDesktop()
        {
            _connector = new WebApiConnector(this, ServerUrl);
            InitializeComponent();
        }

        private async void loanCalcDesktop_Load(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
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

            interestTextBox.Text = (await _connector.GetInterest(loanTypeID)).ToString("P");
            loanAmountBox.Text = (await _connector.GetAmount(loanTypeID)).ToString("0.00");

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
            catch (Exception ex)
            {
                serverConnectingLabel.Text = ex.Message;
            }
            finally
            {
                calculateButton.Enabled = true;
                calculateButton.Text = "Calculate!";
            }
        }

        // input box related method(s)

        private void loanYearsBox_keyPressed(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void loanYearsBox_valueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loanYearsBox.Text) || !int.TryParse(loanYearsBox.Text, out int value))
                calculateButton.Enabled = false;
            else if (loanTypeComboBox.SelectedItem != null)
                calculateButton.Enabled = true;
        }
    }
}