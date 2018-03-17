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
#if !DEBUG
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
                    doc.DocumentNode.RemoveChild(node, false);
                }

                articleText = doc.DocumentNode.InnerHtml;

                articleText = Regex.Replace(articleText, ".*?<\\/form>", "");

                articleText = Regex.Replace(articleText, "<a href.*?\">", "");
                articleText = articleText.Replace("</a>", "");

                articleText = Regex.Replace(articleText, "<\\/p>.*?<.*?>", "\r\n\r\n");

                articleText = Regex.Replace(articleText, "<.*?>", "");

                articleText = Unescape.UnescapeHtml(articleText);

                return articleText;


#elif DEBUG
                return "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec dictum varius efficitur. Maecenas rutrum egestas aliquet. Vestibulum eu gravida nunc. Interdum et malesuada fames ac ante ipsum primis in faucibus. Integer in ultrices arcu, vel ornare dolor. Aliquam erat volutpat. Praesent euismod hendrerit hendrerit. Nunc scelerisque mattis urna consequat semper. Nunc eget pharetra nunc, non sollicitudin lacus. Praesent in ultricies lorem. Cras congue facilisis vehicula. Nunc ligula lectus, laoreet ac nunc a, mattis tincidunt ligula.\r\n\r\nCurabitur viverra non arcu eu interdum. Nulla justo dolor, lobortis eget odio non, ultricies molestie sem. Quisque ac libero nec lectus lacinia volutpat.Phasellus sed ipsum posuere, pretium elit vulputate, porta est.Cras eu imperdiet diam. Praesent commodo dui eu tortor tempus ultrices.Aliquam sed pretium elit. Mauris vulputate accumsan volutpat. In lobortis ut nisi vitae porttitor. Etiam fermentum laoreet erat, vitae imperdiet dolor iaculis ut. Integer a aliquet tortor. Maecenas risus sem, fermentum a malesuada eget, porttitor ac lectus. Nam elementum ornare condimentum. Nunc consectetur purus turpis. Phasellus ut nibh tortor.\r\n\r\nUt ante ipsum, porttitor et risus sed, lacinia interdum quam. Morbi sollicitudin nibh nec est tincidunt placerat.Curabitur fringilla metus viverra ornare porttitor. Aliquam massa urna, commodo sed metus at, tincidunt aliquam metus. Nam et semper eros. Aenean gravida congue lacus ut sagittis. Aliquam pharetra lectus a lorem consectetur, finibus dictum nisi euismod.Nullam non dignissim eros. Etiam bibendum interdum purus ut facilisis. Proin vel sapien vel lacus venenatis pellentesque.Nullam orci urna, cursus eget varius ut, hendrerit at lacus. Vivamus vitae lacinia ligula. Fusce sit amet dui purus.Quisque dignissim, magna vel malesuada consequat, lacus augue consectetur leo, a convallis eros diam vel dui.Pellentesque eget elit mi. Donec fringilla malesuada metus vel posuere.\r\n\r\nEtiam cursus eleifend tortor quis iaculis. Donec tincidunt pretium velit ut finibus. Praesent in auctor sem. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Proin a interdum nibh. Proin sagittis finibus est, sit amet iaculis nulla.Ut quis nisi sit amet nisl ultricies vehicula ut vitae neque.Aenean non nisl sit amet mi scelerisque semper. Nam at ornare justo, a consequat risus. Fusce euismod convallis tortor nec sodales. Aenean euismod, arcu eget vehicula pulvinar, augue nibh tempor enim, et cursus tellus lacus et libero.\r\n\r\nVestibulum vitae volutpat leo, non ullamcorper nibh. Nullam non blandit elit. Etiam molestie arcu nec consectetur pellentesque. Suspendisse eu nunc eget nisi maximus gravida.Duis finibus turpis vel scelerisque dapibus. Cras quis nisi in ligula semper laoreet.Etiam rutrum diam a metus aliquam, sit amet convallis lorem accumsan. Curabitur viverra, magna sit amet convallis convallis, turpis ligula semper enim, quis laoreet felis massa eget quam. Fusce laoreet lectus sed erat dictum, sit amet feugiat sapien consectetur.\r\n\r\nNunc vel urna vitae justo pellentesque pulvinar vel sit amet massa.Nulla facilisi. Nam pulvinar accumsan orci non consequat. Fusce molestie sollicitudin finibus. Donec tincidunt in odio porttitor lobortis.Mauris vitae ante sed est commodo lobortis vitae ut orci. Cras urna ante, tincidunt nec hendrerit vitae, condimentum non nisl. Aenean ut ante a.";
#endif


            }
        }
    }
}