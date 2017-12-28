using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoanCalculatorDesktop
{
    class WebApiConnector
    {
        private HttpClient client = new HttpClient();
        private Form1 currentForm;

        public WebApiConnector(Form1 form1, string webApiLink)
        {
            currentForm = form1;

            client.BaseAddress = new Uri(webApiLink);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task GetPayments(UInt16 LoanTypeID, Decimal TotalAmount, UInt16 NumberOfYears)
        {
            var response = await client.GetAsync($"api/Loan/ReturnPayments?LoanTypeID={LoanTypeID}&TotalAmount={TotalAmount}&NumberOfYears={NumberOfYears}");

            if (response.IsSuccessStatusCode)
                currentForm.PaymentList = await response.Content.ReadAsAsync<List<Payment>>();
                //currentForm.PaymentList = response.Content.ReadAsAsync<List<Payment>>().Result; // sync
            else
                throw new Exception("Could not connect to server!");
        }

        public async Task<decimal> GetInterest(UInt16 LoanTypeID)
        {
            var response = await client.GetAsync($"api/Loan/GetInterest?LoanTypeID={LoanTypeID}");
            decimal interest = 0M;

            if (response.IsSuccessStatusCode)
                interest = await response.Content.ReadAsAsync<Decimal>();
            else
                throw new Exception("Could not connect to server!");

            return interest;
        }

        public async Task GetLoanTypes()
        {
            var response = await client.GetAsync($"api/Loan/GetLoanTypes");

            if (response.IsSuccessStatusCode)
                currentForm.LoanTypeList = await response.Content.ReadAsAsync<List<LoanType>>();
            else
                throw new Exception("Could not connect to server!");

        }
    }
}
