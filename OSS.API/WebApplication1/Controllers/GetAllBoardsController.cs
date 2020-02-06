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
    public class GetAllBoardsController : ApiController
    {
        //
        // GET: /GetAllBoards/
        public object GET()
        {
            DataLookup data = new DataLookup();
            return data.getAllBoards();
        }
	}
}