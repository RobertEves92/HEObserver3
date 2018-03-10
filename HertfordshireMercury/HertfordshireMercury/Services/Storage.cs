using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HertfordshireMercury.Services
{
    public static class Storage
    {
        public static string StoragePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static void SaveXmlDoc(string DocSource, string Path)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(DocSource);
            xml.Save(StoragePath + Path);
        }
    }
}
