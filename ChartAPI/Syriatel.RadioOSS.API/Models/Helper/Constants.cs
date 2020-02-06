using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Helper
{
    public class Constants
    {
        public static class Messages
        {
            public const string EMPTY_MESSAGE = "";
            public const string UNEXPECTED_ERROR_MESSAGE = "Unexpected Error";
        }

        public const int SUCCESSED = 1;
        public const int FAILED = -1;


        public const String RANCategory = "RAN";
        public const String RADIOCategory = "RADIO";

        public const String CELL_Table = "CELL";
        public const String BAND_Table = "BAND";
        public const String BANS_Value = "value";
        public const String CELL_ID = "ID";
        public const String InnerJoin = "InnerJoin";
        public const String LeftOuterJoin = "LeftOuterJoin";

       
    }
}