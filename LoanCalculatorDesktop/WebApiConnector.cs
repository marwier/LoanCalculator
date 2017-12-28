
namespace LoanCalculatorDesktop
{
    using InterviewTask.Models;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    class WebApiConnector
    {
        private HttpClient _client = new HttpClient();

        private LoanCalcDesktop _currentForm;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="form">Used to get current form fields</param>
        /// <param name="webApiLink">Link to web server - by now it's localhost</param>
        public WebApiConnector(LoanCalcDesktop form, string webApiLink)
        {
            _currentForm = form;

            _client.BaseAddress = new Uri(webApiLink);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Seds web request to get list of payments for specified loan.
        /// Used to fill interest list view in main view.
        /// </summary>
        /// <param name="LoanTypeID">ID of loan</param>
        /// <param name="TotalAmount">Loan amount</param>
        /// <param name="NumberOfYears">Loan years</param>
        public async Task GetPayments(UInt16 LoanTypeID, Decimal TotalAmount, UInt16 NumberOfYears)
        {
            var response = await _client.GetAsync($"api/Loan/ReturnPayments?LoanTypeID={LoanTypeID}&TotalAmount={TotalAmount}&NumberOfYears={NumberOfYears}");

            if (response.IsSuccessStatusCode)
                _currentForm.PaymentList = await response.Content.ReadAsAsync<List<Payment>>();
            //currentForm.PaymentList = response.Content.ReadAsAsync<List<Payment>>().Result; // sync
            else
                throw new HttpRequestException("Could not connect to server!");
        }

        /// <summary>
        /// Seds web request to get interest of specified loan. 
        /// Used to fill interest textbox in main view.
        /// </summary>
        /// <param name="LoanTypeID">ID of loan</param>
        /// <returns>Interest</returns>
        public async Task<decimal> GetInterest(UInt16 LoanTypeID)
        {
            var response = await _client.GetAsync($"api/Loan/GetInterest?LoanTypeID={LoanTypeID}");

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<Decimal>();
            else
                throw new HttpRequestException("Could not connect to server!");
        }

        /// <summary>
        /// Sends web request to get loan types from server. 
        /// Used to populate combobox when desktop app starts
        /// </summary>
        public async Task GetLoanTypes()
        {
            var response = await _client.GetAsync($"api/Loan/GetLoanTypes");

            if (response.IsSuccessStatusCode)
                _currentForm.LoanTypeList = await response.Content.ReadAsAsync<List<LoanType>>();
            else
                throw new HttpRequestException("Could not connect to server!");
        }
    }
}
