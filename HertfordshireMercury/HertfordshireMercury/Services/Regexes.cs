using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HertfordshireMercury.Services
{
    public static class Regexes
    {
        public static Regex FormTag = new Regex(".*?<\\/form>", RegexOptions.Compiled); //matches all text from start and form close tag

        public static Regex Hyperlinks = new Regex("<a href.*?\">", RegexOptions.Compiled); //matches all hyperlink open tags

        public static Regex Gallery = new Regex("<a class=\\\"gallery-interaction.*?>", RegexOptions.Compiled); //matches entire gallery sections

        public static Regex Paragraphs = new Regex("<\\/p>.*?<.*?>", RegexOptions.Compiled); //matches end of paragraph tags and anything up to and including the next html tag

        public static Regex Button = new Regex("<button.*?</button>", RegexOptions.Compiled); //matches entire poll button tags
        public static Regex Headers = new Regex("<h\\d.*?</h\\d>", RegexOptions.Compiled); //matches header formatting tags - e.g. H3 H2 etc


        public static Regex Images = new Regex("<img.*?>", RegexOptions.Compiled); //matches image tags
        public static Regex Labels = new Regex("<span class=\\\"label.*?</span>", RegexOptions.Compiled); //matches span tags with class label
        public static Regex Spans = new Regex("<span>.*?</span>", RegexOptions.Compiled); //matches other span tags

        public static Regex Tags = new Regex("<.*?>", RegexOptions.Compiled); //matches anything basically resembling a html tag (starts with < and ends with > with anything inbetween

        public static Regex Video = new Regex(@"Video Loading\s+Video Unavailable\s+Click to play\s+Tap to play\s+The video will start in\s+Cancel\s+Play now", RegexOptions.Compiled); //matches leftover video text

        public static Regex Whitespace = new Regex(@"\s\s+", RegexOptions.Compiled); //matches excessive whitespace
    }
}
