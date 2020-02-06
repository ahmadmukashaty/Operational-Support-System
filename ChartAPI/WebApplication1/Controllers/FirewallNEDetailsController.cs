using Syriatel.OSS.API.Data;
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
    public class FirewallNEDetailsController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //
        // GET: /FirewallNEDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object NE = data.getFirewallNEDetails(Id);
            return NE;
        }
	}
}