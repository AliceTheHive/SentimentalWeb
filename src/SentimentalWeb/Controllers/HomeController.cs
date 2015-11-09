using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SentimentalWeb.Service;
using SentimentalWeb.Models;

namespace SentimentalWeb.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(string inputText = "")
        {
            if (!string.IsNullOrEmpty(inputText))
            {
                var sentimentScore = await TextAnalyticsService.GetSentimentScore(inputText);
                var keyPhrases = await TextAnalyticsService.GetKeyPhrases(inputText);
                var keyPhrasesAsString = string.Empty;

                foreach (var keyPhrase in keyPhrases)
                {
                    keyPhrasesAsString += keyPhrase + " ";
                }

                var viewModel = new ResultViewModel()
                {
                    SentimentScore = sentimentScore,
                    KeyPhrases = keyPhrases,
                    OriginalText = inputText,
                    KeyPhrasesAsString = keyPhrasesAsString
                };

                return View(viewModel);
            }
            else
            {
                return View();
            }
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
