
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

        public WebApiConnector(LoanCalcDesktop form, string webApiLink)
        {
            _currentForm = form;

            _client.BaseAddress = new Uri(webApiLink);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task GetPayments(UInt16 LoanTypeID, UInt16 NumberOfYears)
        {
            await _currentForm.PopulateListView(
                await PerformActionAsync<List<Payment>>(
                    $"api/Loan/CalculatePayments?LoanTypeID={LoanTypeID}&NumberOfYears={NumberOfYears}"));
        }

        public async Task<decimal> GetInterest(UInt16 LoanTypeID)
        {
            return await PerformActionAsync<decimal>(
                $"api/Loan/GetInterest?LoanTypeID={LoanTypeID}");
        }

        public async Task<decimal> GetAmount(UInt16 LoanTypeID)
        {
            return await PerformActionAsync<decimal>(
                $"api/Loan/GetAmount?LoanTypeID={LoanTypeID}");
        }

        public async Task GetLoanTypes()
        {
            _currentForm.PopulateComboBox(
                await PerformActionAsync<List<LoanType>>(
                    $"api/Loan/GetLoanTypes"));
        }

        private async Task<T> PerformActionAsync<T>(string requestUrl)
        {
            var response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            else
                throw new Exception($"Code {(int)response.StatusCode}: {response.ReasonPhrase.ToString()}");
        }
    }
}
