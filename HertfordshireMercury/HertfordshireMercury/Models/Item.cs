using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HertfordshireMercury.Services;
using HtmlAgilityPack;

namespace HertfordshireMercury.Models
{
    public class Item : CodeHollow.FeedReader.FeedItem
    {
        public string WrittenBy => "Written By: " + Author;
        public string Published => "Published: " + PublishingDate.ToString();

        public string ArticleText
        {
            get
            {
                string articleText = "";
                string articleSrc = NetServices.GetWebpageFromUrl(Link);//download source

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(articleSrc);

                List<HtmlNode> divList = new List<HtmlNode>();
                foreach (HtmlNode div in doc.DocumentNode.SelectNodes("//div"))
                {
                    divList.Add(div);
                }

                HtmlNode articleBodyNode = null;
                foreach (HtmlNode div in divList)
                {
                    foreach (HtmlAttribute a in div.Attributes)
                    {
                        if (a.Name == "class" && a.Value == "article-body")
                        {
                            articleBodyNode = div;
                            break;
                        }
                    }

                    if (articleBodyNode != null)
                        break;
                }

                doc.LoadHtml(articleBodyNode.InnerHtml);
                List<HtmlNode> nodesToRemove = new List<HtmlNode>();
                foreach (HtmlNode node in doc.DocumentNode.DescendantNodes())
                {
                    if (node.Name.ToLower() == "form" || node.Name.ToLower() == "aside" || node.Name.ToLower() == "figure")
                    {
                        nodesToRemove.Add(node);
                    }
                }

                foreach (HtmlNode node in nodesToRemove)
                {
                    try
                    {
                        doc.DocumentNode.RemoveChild(node, false);
                    }
                    catch(Exception e)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(e.ToString());
#endif
                    }
                }

                articleText = doc.DocumentNode.InnerHtml;

                articleText = Regex.Replace(articleText, ".*?<\\/form>", "");

                articleText = Regex.Replace(articleText, "<a href.*?\">", "");
                articleText = articleText.Replace("</a>", "");

                articleText = Regex.Replace(articleText, "<\\/p>.*?<.*?>", "\r\n\r\n");

                articleText = Regex.Replace(articleText, "<.*?>", "");

                articleText = Unescape.UnescapeHtml(articleText);

                return articleText;
            }
        }
    }
}