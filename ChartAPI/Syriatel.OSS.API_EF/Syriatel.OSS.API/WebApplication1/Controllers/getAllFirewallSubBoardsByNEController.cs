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
    public class getAllFirewallSubBoardsByNEController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //
        // GET: /getAllFirewallSubBoardsByNE/
        public object GET(int NeId)
        {
            DataLookup data = new DataLookup();
            return data.getAllFirewallSubBoardsByNE(NeId);
        }
    }
}