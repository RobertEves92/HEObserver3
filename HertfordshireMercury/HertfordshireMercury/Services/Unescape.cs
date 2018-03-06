using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HertfordshireMercury.Services
{
    public static class Unescape
    {
        /// <summary>
        /// Returns an unescaped string replacing any hexadecimal character references with their ASCII counterparts
        /// </summary>
        /// <param name="escaped">Escaped string to unescape</param>
        /// <returns>Unescaped string</returns>
        public static string UnescapeHtml(string escaped)
        {
            return WebUtility.HtmlDecode(escaped);
        }
    }
}
