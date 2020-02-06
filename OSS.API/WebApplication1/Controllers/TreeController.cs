using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Helper;
using Syriatel.OSS.API.Models;
using Syriatel.OSS.API.NeTreeModeView;
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
    public class TreeController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        // GET: /Tree/
        public ResponseJson GET()
        {
            try
            {
                TreeData td = new TreeData();

                return new ResponseJson(Constants.FAILED, Constants.EMPTY_MESSAGE, td.tree);
            }
            catch(Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
           
        }

        public object GET(int categoryId)
        {
            DataLookup dt = new DataLookup();
          return dt.getTransmitionPath(categoryId);
        }
    }
}