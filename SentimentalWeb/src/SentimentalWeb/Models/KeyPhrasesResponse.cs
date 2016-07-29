using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.Models
{
    public class KeyPhrasesResponse
    {
        public List<KeyPhrasesResponseDocument> documents { get; set; }
        public List<object> errors { get; set; }
    }

    public class KeyPhrasesResponseDocument
    {
        public List<string> keyPhrases { get; set; }
        public string id { get; set; }
    }
}
