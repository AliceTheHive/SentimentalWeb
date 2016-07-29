using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.Models
{
    public class SentimentResponse
    {
        public List<SentimentResponseDocument> documents { get; set; }
        public List<object> errors { get; set; }
    }

    public class SentimentResponseDocument
    {
        public double score { get; set; }
        public string id { get; set; }
    }

}
