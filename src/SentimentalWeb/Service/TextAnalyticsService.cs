using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SentimentalWeb.Models;
using System.Net.Http;

namespace SentimentalWeb.Service
{
    public static class TextAnalyticsService
    {
        private static string exampleText = "Sentimental tells you about the sentiment and key phrases in text. It is really quite awesome.";

        public static async Task<double> GetSentimentScore(string inputText)
        {
            if (inputText.ToLower() == exampleText.ToLower())
            {
                //return an example value to avoid hitting the live API. We only get 1000 hits a month. Fake a delay to simulate a real API call
                Thread.Sleep(2000);
                return 0.88;
            }

            var data = await GetData(inputText, "GetSentiment");

            if (!string.IsNullOrEmpty(data))
            {
                var sentiment = JsonConvert.DeserializeObject<SentimentResponse>(data);
                return sentiment.Score;
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

            var data = await GetData(inputText, "GetKeyPhrases");

            if (!string.IsNullOrEmpty(data))
            {
                var keyPhrasesResponse = JsonConvert.DeserializeObject<KeyPhrasesResponse>(data);
                return keyPhrasesResponse.KeyPhrases;
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
                client.BaseAddress = new Uri("https://api.datamarket.azure.com/data.ashx/amla/text-analytics/v1/" + apiOperation);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Basic TWFjaGluZUxlYXJuaW5nVGV4dEFuYWx5dGljc1NlcnZpY2VTZW50aW1lbnRBbmFseXNpczp2QUsxMkxkWWNaN21QQnFlQ3VMVFZrQVBIQVZ6MGw1ZkpXbFZpbTVUSEJzPSA=");

                var response = await client.GetAsync(client.BaseAddress + "?Text=" + inputText);

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
