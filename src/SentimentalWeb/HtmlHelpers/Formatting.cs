using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentalWeb.HtmlHelpers
{
    public static class Formatting
    {
        public static string FormatAsPercentage(double value)
        {
            return Math.Round((value * 100),0).ToString();
        }

        public static string StripPunctuation(string orginal)
        {
            return new string(orginal.Where(c => !char.IsPunctuation(c)).ToArray());
        }

    }
}
