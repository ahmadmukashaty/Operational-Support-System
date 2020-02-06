using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.NPM.WebAPI.Models.Helper
{
    public static class Constants
    {
        public static class Messages
        {
            public const string EMPTY_MESSAGE = "";
            public const string UNEXPECTED_ERROR_MESSAGE = "Unexpected Error";
        }

        public const int SUCCESSED = 1;
        public const int FAILED = -1;

        public const string TAB_TYPE_LINK = "link";
        public const string TAB_TYPE_SUB = "sub";

        public const int REGION_FILTER = 1;
        public const int AREA_FILTER = 2;
        public const int ZONE_FILTER = 3;
        public const int SUBAREA_FILTER = 4;
        public const int SITE_FILTER = 5;
    }
}