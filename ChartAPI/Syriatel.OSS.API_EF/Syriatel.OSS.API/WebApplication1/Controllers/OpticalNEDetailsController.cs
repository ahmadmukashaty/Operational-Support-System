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
    public class OpticalNEDetailsController : ApiController
    {
        //
        // GET: /OpticalNEDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object NE = data.getOpticalNEDetails(Id);
            return NE;
        }
	}
}