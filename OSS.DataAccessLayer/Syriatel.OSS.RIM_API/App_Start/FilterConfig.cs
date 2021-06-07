using System.Web;
using System.Web.Mvc;

namespace Syriatel.OSS.RIM_API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
