
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CommonModels;

namespace LoanCalculatorDesktop
{
    internal class WebApiConnector
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly LoanCalcDesktop _currentForm;

        public WebApiConnector(LoanCalcDesktop form, string webApiLink)
        {
            if (string.IsNullOrEmpty(webApiLink))
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(webApiLink));

            _currentForm = form ?? throw new ArgumentNullException(nameof(form));

            _client.BaseAddress = new Uri(webApiLink);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task GetPayments(ushort loanTypeId, decimal totalAmount, ushort numberOfYears)
        {
            await _currentForm.PopulateListView(
                await PerformActionAsync<List<Payment>>(
                    $"api/Loan/ReturnPayments?LoanTypeId={loanTypeId}&TotalAmount={totalAmount}&NumberOfYears={numberOfYears}"));
        }

        public async Task<decimal> GetInterest(ushort loanTypeId)
        {
            return await PerformActionAsync<decimal>(
                $"api/Loan/GetInterest?LoanTypeId={loanTypeId}");
        }

        public async Task GetLoanTypes()
        {
            _currentForm.PopulateComboBox(
                await PerformActionAsync<List<LoanType>>(
                    "api/Loan/GetLoanTypes"));
        }

        private async Task<T> PerformActionAsync<T>(string requestUrl)
        {
            var response = await _client.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<T>();
            throw new Exception($"Code {(int)response.StatusCode}: {response.ReasonPhrase}");
        }
    }
}
