using System;

namespace HertfordshireMercury.Models
{
    public class Item: CodeHollow.FeedReader.FeedItem
    {
        public string WrittenBy => "Written By: " + Author;
        public string Published => "Published: " + PublishingDate.ToString();
    }
}