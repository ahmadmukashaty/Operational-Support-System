using Syriatel.TranssmissionOSS.API.Data;
using Syriatel.TranssmissionOSS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Syriatel.TranssmissionOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class getAllFirewallNEsForSubCategoryController : ApiController
    {
        //
        // GET: /getAllFirewallNEsForSubCategory/
        public object GET(int categoryId, int SubCategoryId)
        {
            DataLookup data = new DataLookup();
            return data.getAllFirewallNEsForSubCategory(categoryId, SubCategoryId);


        }
    }
}