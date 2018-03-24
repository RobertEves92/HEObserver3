using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HertfordshireMercury.Services
{
    public static class Regexes
    {
        public static Regex forms = new Regex(".*?<\\/form>", RegexOptions.Compiled);

        public static Regex hyperlinks = new Regex("<a href.*?\">", RegexOptions.Compiled);

        public static Regex gallery = new Regex("<a class=\\\"gallery-interaction.*?>", RegexOptions.Compiled);

        public static Regex paragraphs = new Regex("<\\/p>.*?<.*?>", RegexOptions.Compiled);

        public static Regex button = new Regex("<button.*?</button>", RegexOptions.Compiled);
        public static Regex headers = new Regex("<h\\d.*?</h\\d>", RegexOptions.Compiled);


        public static Regex images = new Regex("<img.*?>", RegexOptions.Compiled);
        public static Regex labels = new Regex("<span class=\\\"label.*?</span>", RegexOptions.Compiled);
        public static Regex spans = new Regex("<span>.*?</span>", RegexOptions.Compiled);

        public static Regex tags = new Regex("<.*?>", RegexOptions.Compiled);

        public static Regex video = new Regex(@"Video Loading\s+Video Unavailable\s+Click to play\s+Tap to play\s+The video will start in\s+Cancel\s+Play now", RegexOptions.Compiled);

        public static Regex whitespace = new Regex(@"\s\s+", RegexOptions.Compiled);

    }
}
