using Syriatel.OSS.API.Data;
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
    public class NEPortsController : ApiController
    {
        //
        // GET: /NEPorts/
        public object GET(string NEName , int slotId , int subSlotId)
        {
            DataLookup data = new DataLookup();
            return data.getAllPortsForSubBoard(NEName,slotId,subSlotId);
        }
	}
}