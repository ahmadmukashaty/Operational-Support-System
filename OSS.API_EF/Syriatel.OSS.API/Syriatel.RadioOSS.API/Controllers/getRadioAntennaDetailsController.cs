﻿using Syriatel.RadioOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Syriatel.RadioOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class getRadioAntennaDetailsController : ApiController
    {
        //
        // GET: /getRadioAntennaDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object ob = data.getRadioAntennaDetails(Id);
            return ob;
        }
	}
}
