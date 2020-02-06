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
    public class getAllFirewallBoardByNEController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //
        // GET: /getAllFirewallBoardByNE/
        public object GET(int neid)
        {
            DataLookup data = new DataLookup();
            return data.getAllFirewallBoardByNE(neid);
        }
	}
}