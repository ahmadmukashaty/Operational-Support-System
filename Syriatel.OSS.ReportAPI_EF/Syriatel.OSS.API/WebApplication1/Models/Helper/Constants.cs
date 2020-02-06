using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Helper
{
    public class Constants
    {
        public static class Messages
        {
            public const string EMPTY_MESSAGE = "";
            public const string UNEXPECTED_ERROR_MESSAGE = "Unexpected Error";
        }

        public static class Relations
        {
            public const string INITIAL = "INITIAL";
            public const string ONE_TO_ONE = "ONE_TO_ONE";
            public const string ONE_TO_MANY = "ONE_TO_MANY";
            public const string MANY_TO_MANY = "MANY_TO_MANY";
        }

        public static class JoinType
        {
            public const string INNER_JOIN = " INNER JOIN ";
            public const string LEFT_OUTER_JOIN = " LEFT OUTER JOIN ";
        }

        public static class Types
        {
            public const string TYPE = "TYPE";
        }

        public const int SUCCESSED = 1;
        public const int FAILED = -1;

        public const int REGION_FILTER = 1;
        public const int AREA_FILTER = 2;
        public const int ZONE_FILTER = 3;
        public const int SUBAREA_FILTER = 4;
        public const int SITE_FILTER = 5;

        public const string REGION_TABLE = "REGION";
        public const string AREA_TABLE = "AREA";
        public const string ZONE_TABLE = "ZONE";
        public const string SUBAREA_TABLE = "SUBAREA";
        public const string SITE_TABLE = "SITE_CANDIDATE";

        public const string SITE_LOCATION_COLUMN_NAME = "NAME";
        public const string SITE_LOCATION_COLUMN_TYPE = "VARCHAR2";

        public const string SITE_CODE_COLUMN_NAME = "SITE_CODE";
        public const string SITE_ENGLISH_NAME_COLUMN_NAME = "ENGLISH_NAME";

        public const string SUB_CATEGORY_TABLE_NAME = "RIM_SUBCATEGORY";
        public const string SUB_CATEGORY_COLUMN_NAME = "NAME";
        public const string SUB_CATEGORY_COLUMN_TYPE = "VARCHAR2";

    }
}