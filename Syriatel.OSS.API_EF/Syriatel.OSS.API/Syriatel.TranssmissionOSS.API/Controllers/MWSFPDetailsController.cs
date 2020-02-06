using Syriatel.TranssmissionOSS.API.Data;
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
    public class MWSFPDetailsController : ApiController
    {
        //
        // GET: /MWSFPDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object port = data.getMWSFPDetails(Id);
            return port;
        }
    }
}