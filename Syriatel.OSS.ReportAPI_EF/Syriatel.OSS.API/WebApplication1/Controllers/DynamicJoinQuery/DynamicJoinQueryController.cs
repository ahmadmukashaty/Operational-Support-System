using Syriatel.OSS.API.Models;
using Syriatel.OSS.API.Models.DynamicJoinService;
using Syriatel.OSS.API.Models.Helper;
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
    public class DynamicJoinQueryController : ApiController
    {
        // POST: api/DynamicJoinQuery
        [HttpPost]
        public ResponseJson Post([FromBody]DynamicJoinData response)
        {
            try
            {
                DynamicJoinCreation dynamicJoinCreation = new DynamicJoinCreation(response);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, dynamicJoinCreation.Data);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
