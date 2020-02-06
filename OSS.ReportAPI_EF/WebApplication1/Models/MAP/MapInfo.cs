using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.MAP
{
    public class MapInfo
    {
        public int Id { get; set; }

        public string siteCode { get; set; }

        public string EnglishName { get; set; }

        public string LATITUDE_N { get; set; }

        public string LONGITUDE_E { get; set; }
    }
}