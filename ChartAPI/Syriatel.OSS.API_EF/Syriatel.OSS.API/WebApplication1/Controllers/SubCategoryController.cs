using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Cors;
namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SubCategoryController : ApiController
    {
        //
        // GET: /SubCategory/
        public object Get(int Id)
        {
            DataLookup data = new DataLookup();
            object subcategories = data.getAllSubCategories(Id);
            return subcategories;
        }
	}
}