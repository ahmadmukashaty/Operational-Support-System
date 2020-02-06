//using Syriatel.RadioOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Syriatel.RadioOSS.API.Models;
using System.Web.Http.Cors;
using System.Data.Entity;
using Syriatel.RadioOSS.API.Data;
namespace Syriatel.RadioOSS.API.Controllers

{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AreaController : ApiController
    {
        //
        // GET: /Area/
        public object GET(int Id)
        {

            DataLookup data = new DataLookup();
            object port = data.getAreas();
            return port;
        }
	}
}


  