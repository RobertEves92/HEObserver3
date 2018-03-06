using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HertfordshireMercury.Services
{
    public static class Unescape
    {
        /// <summary>
        /// Returns an unescaped string replacing any hexadecimal character references with their ASCII counterparts
        /// </summary>
        /// <param name="escaped">Escaped string to unescape</param>
        /// <returns>Unescaped string</returns>
        public static string UnescapeHexCharacters(string escaped)
        {
            string unescaped = escaped;
            MatchCollection hexmatchs = Regex.Matches(escaped, "&#x[0-9][0-9];");

            string[] unescapedMatches = new string[hexmatchs.Count];

            for (int i = 0; i < hexmatchs.Count; i++)
            {
                string s = hexmatchs[i].Value;
                s = s.Replace("&#x", "");
                s = s.Replace(";", "");
                s = Int16.Parse(s, System.Globalization.NumberStyles.AllowHexSpecifier).ToString();
                unescapedMatches[i] = s;
            }

            for (int i = 0; i < hexmatchs.Count; i++)
            {
                unescaped = escaped.Replace(hexmatchs[i].Value, ((char)int.Parse(unescapedMatches[i])).ToString());
            }

            return unescaped;
        }
    }
}
