
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models;
using System.Web.Http.Cors;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        //
        // GET: /Category/
        public List<RIM_CATEGORY> Get()
        {
           DataLookup data = new DataLookup();
           List<RIM_CATEGORY> categories = data.getAllCategories();
            return categories;
        }
	}
}