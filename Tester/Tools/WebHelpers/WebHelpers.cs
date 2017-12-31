using System.IO;
using System.Net;
using Newtonsoft.Json;
using Tester.Tools.Logs;

namespace Tester.Tools.WebHelpers
{
    public class WebHelpers
    {
        public static bool VerifyEndpoints(string urlToTest, HttpStatusCode expectedStatusCode)
        {
            TestLog.AddMessage($"Attempting to open url: {urlToTest}");

            try
            {
                using (var response = WebRequest.Create(urlToTest).GetResponse())
                {
                    return VerifyResponseStatusCode(response, expectedStatusCode);
                }
            }
            catch (WebException e)
            {
                return VerifyResponseStatusCode(e.Response, expectedStatusCode);
            }
        }


        public static bool VerifyResponseStatusCode(WebResponse response, HttpStatusCode expectedCode)
        {
            if (((HttpWebResponse)response).StatusCode == expectedCode)
            {
                TestLog.AddMessage(
                    $"Correct status code ({(int)expectedCode}: {expectedCode}) were returned. Service is working as expected.",
                    TestLog.LogResult.Passed);
                return true;
            }
            else
            {
                TestLog.AddMessage(
                    $"Incorrect status code ({(int)((HttpWebResponse)response).StatusCode}: {((HttpWebResponse)response).StatusCode}) were returned. Service is not working as expected.",
                    TestLog.LogResult.Failed);
                return false;
            }
        }

        public static T GetJsonPageContent<T>(WebResponse response)
        {
            using (var data = response.GetResponseStream())
            {
                using (var reader = new StreamReader(data))
                {
                    return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                }
            }
        }
    }
}
