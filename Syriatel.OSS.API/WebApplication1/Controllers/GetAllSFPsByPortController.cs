using Syriatel.OSS.API.Data;
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
    public class GetAllSFPsByPortController : ApiController
    {
        //
        // GET: /GetAllSFPsByPort/
        public object GET(int portid)
        {
            DataLookup data = new DataLookup();
            return data.getAllSFPsByPort(portid);
        }
	}
}