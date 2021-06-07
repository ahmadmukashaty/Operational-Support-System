using Syriatel.TranssmissionOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Http.Cors;

namespace Syriatel.TranssmissionOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetAllSubBoardsByBoardController : ApiController
    {
        //
        // GET: /GetAllSubBoardsByBoard/
        //public object GET(int boardid)
        //{
        //    DataLookup data = new DataLookup();
        //    return data.getAllSubBoardsByBoard(boardid);
        //}
        public object GET(int NeId)
        {
            DataLookup data = new DataLookup();
            return data.getAllSubBoardsByNE(NeId);
        }
	}
}