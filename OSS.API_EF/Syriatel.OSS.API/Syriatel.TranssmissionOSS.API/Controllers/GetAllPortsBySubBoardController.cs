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
    public class GetAllPortsBySubBoardController : ApiController
    {
        //
        // GET: /GetAllPortsBySubBoard/
        //public object GET(int subboardid)
        //{
        //    DataLookup data = new DataLookup();
        //    return data.getAllPortsBySubBoard(subboardid);
        //}
        public object GET(int NeId)
        {
            DataLookup data = new DataLookup();
            return data.getAllPortsByNE(NeId);
        }
        
	}
}