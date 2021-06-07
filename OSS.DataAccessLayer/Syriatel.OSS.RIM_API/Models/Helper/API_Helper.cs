using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Syriatel.OSS.RIM_API.Models.Attributes;

namespace Syriatel.OSS.RIM_API.Models.Helper
{
    public class API_Helper
    {
        public static bool SearchFlagStatus(short searchFlag)
        {
            return (searchFlag > 0);
        }

        internal static bool CategoryOpeningStatus(short categoryIsOpen)
        {
            return (categoryIsOpen > 0);
        }

        public static bool TabTarget(short target)
        {
            return (target > 0);
        }

        public static bool IsMainAttribute(string value)
        {
            if (value == "1")
                return true;
            return false;
        }
    }
}