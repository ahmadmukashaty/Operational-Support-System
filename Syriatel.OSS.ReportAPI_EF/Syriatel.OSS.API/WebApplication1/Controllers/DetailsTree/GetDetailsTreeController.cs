using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.TreeDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetDetailsTreeController : ApiController
    {
        // GET: api/GetDetailsTree
        public ResponseJson Get(string moduleName)
        {
            try
            {
                RanTreeDetailsCreation detailsTree = new RanTreeDetailsCreation(moduleName.ToLower());

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, detailsTree.Tree);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
