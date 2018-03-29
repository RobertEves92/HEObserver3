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
        public string KeyWords
        {
            get
            {
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

                foreach (var cat in Categories)
                {
                    stringBuilder.Append(cat + ", ");
                }

                return stringBuilder.ToString();
            }

            set
            {
                try
                {
                    string itemKeywords = value;
                    itemKeywords = itemKeywords.Replace("<media:keywords><![CDATA[", "");
                    itemKeywords = itemKeywords.Replace("]]></media:keywords>", "");
                    string[] cats = itemKeywords.Split(',');

                    Categories = new List<string>();
                    if (cats.Length == 1 && cats[0] == "")
                    {
                        Categories.Add("Uncatagorised");
                    }
                    else
                    {
                        foreach (var cat in cats)
                        {
                            Categories.Add(cat.Trim());
                        }
                    }
                }
                catch
                {
                    Categories.Add("Uncatagorised");
                }
            }
        }

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

                articleText = Regexes.FormTag.Replace(articleText, "");

                articleText = Regexes.Hyperlinks.Replace(articleText, "");
                articleText = articleText.Replace("</a>", "");
                articleText = Regexes.Gallery.Replace(articleText, "");

                articleText = Regexes.Paragraphs.Replace(articleText, "\r\n\r\n");

                articleText = Regexes.Button.Replace(articleText, "");
                articleText = Regexes.Headers.Replace(articleText, "");
                articleText = articleText.Replace("poll loading", "");

                articleText = Regexes.Images.Replace(articleText, "");
                articleText = Regexes.Labels.Replace(articleText, "");
                articleText = Regexes.Spans.Replace(articleText, "");

                articleText = Regexes.Tags.Replace(articleText, "");

                articleText = Unescape.UnescapeHtml(articleText);

                articleText = Regexes.Video.Replace(articleText, "");

                articleText = Regexes.Whitespace.Replace(articleText, "\r\n\r\n");

                return articleText;
            }
        }
    }
}