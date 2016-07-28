using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.Models
{
    public class APIRequest
    {
        public List<Document> documents { get; set; }
    }

    public class Document
    {
        public string language { get; set; }
        public string id { get; set; }
        public string text { get; set; }
    }


}
