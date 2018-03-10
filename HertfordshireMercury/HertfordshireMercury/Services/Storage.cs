using System;
using System.Collections.Generic;
using System.Text;

namespace HertfordshireMercury.Services
{
    public static class Storage
    {
        public static string StoragePath => Environment.GetFolderPath(Environment.SpecialFolder.Personal);
    }
}
