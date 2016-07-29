using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SentimentalWeb.Services;
using SentimentalWeb.ViewModels;

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

                //Cast sentiment score as a percentage
                sentimentScore = Math.Round((sentimentScore * 100), 0);

                var viewModel = new ResultViewModel()
                {
                    SentimentScore = sentimentScore,
                    KeyPhrases = keyPhrases,
                    OriginalText = inputText,
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
            return View();
        }
    }
}
