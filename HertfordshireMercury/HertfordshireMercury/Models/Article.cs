using System;
using System.Collections.Generic;
using System.Text;

namespace HertfordshireMercury.Models
{
    class Article
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Published { get; set; }
    }
}
