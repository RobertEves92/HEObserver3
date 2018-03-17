using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace HertfordshireMercury.Services
{
    public static class Storage
    {
        private static string StoragePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public static void SaveTextDoc(string DocSource, string Path)
        {
            string savePath = $"{StoragePath}/{Path}";

            TextWriter textWriter = new StreamWriter(savePath);
            textWriter.WriteLine(DocSource);
            textWriter.Flush();
            textWriter.Close();
        }
    }
}
