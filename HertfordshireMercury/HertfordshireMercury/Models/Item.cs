using System;

namespace HertfordshireMercury.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public String Author { get; set; }
        public DateTime DateTime { get; set; }
        public string Link { get; set; }
        public string WrittenBy => "Written By: " + Author;
        public string Published => "Published: " + DateTime.ToString();
    }
}