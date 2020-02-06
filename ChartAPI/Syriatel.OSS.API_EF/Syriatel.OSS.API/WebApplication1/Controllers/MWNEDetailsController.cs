﻿using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MWNEDetailsController : ApiController
    {
        //
        // GET: /MWNEDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object NE = data.getMWNEDetails(Id);
            return NE;
        }
	}
}