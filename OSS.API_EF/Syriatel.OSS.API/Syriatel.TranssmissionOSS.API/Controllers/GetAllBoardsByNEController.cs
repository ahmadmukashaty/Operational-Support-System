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
    public class GetAllBoardsByNEController : ApiController
    {
        DataLookup data;
        public GetAllBoardsByNEController()
        {
            data = new DataLookup();
        }
        //
        // GET: /GetAllBoardsByNE/
        public object GET(int neid)
        {
            DataLookup data = new DataLookup();
            return data.getAllBoardsByNE(neid);
        }

        //public void GET(string category , string type , string elementId)
        //{ 
        //     data.getChildrenOfElementTree( category ,  type ,  elementId);
        //}
        
	}
}