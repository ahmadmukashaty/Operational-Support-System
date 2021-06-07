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
    public class getSiteAntennasController :  ApiController
    {
        //
        // GET: /getSiteAntennas/
        public object GET(int siteCandidateId)
        {
            DataLookup data = new DataLookup();
            return data.getSiteAntennas(siteCandidateId);
        }
	}
}


