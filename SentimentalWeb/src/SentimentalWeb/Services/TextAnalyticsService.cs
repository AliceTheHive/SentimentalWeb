using Newtonsoft.Json;
using SentimentalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SentimentalWeb.Services
{
    public static class TextAnalyticsService
    {

        //_apiKey: Replace this with your own Cognitive Services API key, please do not use my key. I include it here so you can get up and running quickly but you can get your own key for free at https://www.microsoft.com/cognitive-services
        private const string _apiKey = "c1715e2472b04c1e99ad75ee6a459d64";

        //_apiUrl: The base URL for the API. Find out what this is for other APIs via the API documentation
        private const string _apiUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/";

        private static string exampleText = "Sentimental tells you about the sentiment and key phrases in text. It is really quite awesome.";

        public static async Task<double> GetSentimentScore(string inputText)
        {
            if (inputText.ToLower() == exampleText.ToLower())
            {
                //return an example value to avoid hitting the live API. We only get 1000 hits a month. Fake a delay to simulate a real API call
                Thread.Sleep(2000);
                return 0.88;
            }

            var data = await GetData(inputText, "sentiment");

            if (!string.IsNullOrEmpty(data))
            {
                var sentiment = JsonConvert.DeserializeObject<SentimentResponse>(data);
                return sentiment.documents.FirstOrDefault().score;
            }
            else
            {
                return 0.0;
            }

        }

        public static async Task<List<string>> GetKeyPhrases(string inputText)
        {
            if (inputText.ToLower() == exampleText.ToLower())
            {
                //return an example value to avoid hitting the live API. We only get 1000 hits a month. Fake a delay to simulate a real API call
                Thread.Sleep(2000);
                var keyPhrases = new List<string>();
                keyPhrases.Add("sentiment");
                keyPhrases.Add("awesome");
                return keyPhrases;
            }

            var data = await GetData(inputText, "keyPhrases");

            if (!string.IsNullOrEmpty(data))
            {
                var keyPhrasesResponse = JsonConvert.DeserializeObject<KeyPhrasesResponse>(data);
                return keyPhrasesResponse.documents.FirstOrDefault().keyPhrases;
            }
            else
            {
                return new List<string>();
            }

        }

        private static async Task<string> GetData(string inputText, string apiOperation)
        {
            using (var client = new HttpClient())
            {

                //setup HttpClient
                var fullApiUrl = _apiUrl + apiOperation;
                client.BaseAddress = new Uri(fullApiUrl);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //setup data object
                var documents = new List<Document>();
                documents.Add(new Document()
                {
                    id = Guid.NewGuid().ToString(),
                    language = "en",
                    text = inputText
                });
                var dataObject = new APIRequest()
                {
                    documents = documents
                };

                //setup httpContent object
                var dataJson = JsonConvert.SerializeObject(dataObject);
                HttpResponseMessage response;
                using (HttpContent content = new StringContent(dataJson))
                {
                    content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    response = await client.PostAsync(fullApiUrl, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
