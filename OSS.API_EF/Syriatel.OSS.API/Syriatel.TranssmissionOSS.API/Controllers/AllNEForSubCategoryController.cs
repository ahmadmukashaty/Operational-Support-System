﻿using Syriatel.TranssmissionOSS.API.Data;
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
    public class AllNEForSubCategoryController : ApiController
    { 
        //
        // GET: /AllNEForSubCategory/
        public object GET(int categoryId, int SubCategoryId)
        {
            DataLookup data = new DataLookup();
            return data.getAllNEsForSubCategory(categoryId, SubCategoryId);
             
           
        } 
	}
} 