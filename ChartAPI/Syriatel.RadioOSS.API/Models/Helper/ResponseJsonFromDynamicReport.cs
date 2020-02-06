using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Helper
{
    public class ResponseJsonFromDynamicReport
    {
        public int success { get; set; }

        public string errorMessage { get; set; }

        public DynamicJoinReturnedData data { get; set; }

        public ResponseJsonFromDynamicReport()
        {
            data = new DynamicJoinReturnedData();
        }
    }
}