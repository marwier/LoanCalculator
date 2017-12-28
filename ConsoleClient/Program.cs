using InterviewTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GetRequest().Wait();
        }

        static async Task GetRequest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55735/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response;

                response = await client.GetAsync("api/Loan/GetLoanTypes");
            }
        }
    }
}
