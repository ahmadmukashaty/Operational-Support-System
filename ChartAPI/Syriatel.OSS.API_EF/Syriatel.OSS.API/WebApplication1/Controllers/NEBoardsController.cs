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
    public class NEBoardsController : ApiController
    {
        //
        // GET: /NEBoards/
        public object Get(string NENAME)
        {
            DataLookup data = new DataLookup();
            return data.getAllBoardsForNE(NENAME);
           
        }
	}
} 