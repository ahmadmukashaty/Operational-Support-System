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
    public class GetTreeRootController : ApiController
    {
        //
        // GET: /GetTreeRoot/
        public object GET(int rootId)
        {
            DataLookup data = new DataLookup();
            return data.getRoots(rootId);
        }
	}
}


