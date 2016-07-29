using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.ViewModels
{
    public class ResultViewModel
    {
        public string OriginalText { get; set; }
        public double SentimentScore { get; set; }
        public List<string> KeyPhrases { get; set; }
    }
}
