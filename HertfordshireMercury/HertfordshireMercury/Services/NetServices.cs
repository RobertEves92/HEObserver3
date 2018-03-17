using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HertfordshireMercury.Services
{
    public static class NetServices
    {
        public static string GetWebpageFromUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);


            request.Method = "GET";
            request.Timeout = 5000;//stop trying after 5s

            WebResponse response = request.GetResponse();

            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();

            return result;
        }
    }
}
