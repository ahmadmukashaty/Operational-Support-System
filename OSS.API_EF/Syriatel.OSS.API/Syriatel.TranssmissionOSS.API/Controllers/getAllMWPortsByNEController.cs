﻿using Syriatel.TranssmissionOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Cors;

namespace Syriatel.TranssmissionOSS.API.Controllers
{
     [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class getAllMWPortsByNEController : ApiController
    {
        //
        // GET: /getAllMWPortsByNE/
         public object GET(int NeId)
         {
             DataLookup data = new DataLookup();
             return data.getAllMWPortsByNE(NeId);
         }
	}
}