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
    public class NESUBBoardsController : ApiController
    {
        //
        // GET: /NESUBBoards/
        public object Get(string NENAME, int slotId)
        {
            DataLookup data = new DataLookup();
            return data.getAllSUBBoardsForNE(NENAME, slotId);

        }
	}
}