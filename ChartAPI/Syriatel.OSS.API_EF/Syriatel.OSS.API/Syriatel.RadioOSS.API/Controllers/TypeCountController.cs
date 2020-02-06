using Syriatel.RadioOSS.API.Data;
using Syriatel.RadioOSS.API.Models.CategoryTypesCount;
using Syriatel.RadioOSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Syriatel.RadioOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TypeCountController : ApiController
    {
        //http://localhost:64035/api/TypeCount
        [System.Web.Http.HttpPost]
        public ResponseJson Post(CountTypePostObject Obj)
      
        {
            try
            {

                CategoryTypesCount result = new CategoryTypesCount(Obj);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, result.Result);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
