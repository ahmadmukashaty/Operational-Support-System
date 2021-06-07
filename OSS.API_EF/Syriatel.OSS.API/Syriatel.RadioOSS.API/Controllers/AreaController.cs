using Syriatel.RadioOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Syriatel.RadioOSS.API.Models;
using System.Web.Http.Cors;
namespace Syriatel.RadioOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AreaController : ApiController
    {
        //
        // GET: /Area/
        public List<AREA> GET(int Id)
        {
            DataLookup data = new DataLookup();
            List<AREA> port = data.getAreas();
            return port;
        }
	}
}


  