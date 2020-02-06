using Syriatel.OSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Cors;
using Syriatel.OSS.API.NeTreeModeView;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetTreeRootController : ApiController
    {
        //
        // GET: /GetAllNEs/
        public object GET()
        {
            DataLookup data = new DataLookup();
            return data.getRoots();
        }
    }
}