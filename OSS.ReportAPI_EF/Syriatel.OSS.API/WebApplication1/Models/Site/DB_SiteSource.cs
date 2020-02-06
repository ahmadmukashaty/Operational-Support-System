using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.RAN_Site
{
    public class DB_SiteSource
    {
        public string TableName { get; set; }

        public string SiteCodeColumnName { get; set; }

        public string SiteCodeColumnType { get; set; }

        public string EnglishNameColumnName { get; set; }

        public string EnglishNameColumnType { get; set; }
    }
}