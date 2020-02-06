using Syriatel.RadioOSS.API.Models.Buysness;
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
    public class BusynessController : ApiController
    {
        // GET: Busyness
        [System.Web.Http.HttpPost]
        public ResponseJson Post(BusynessPostObject Obj)
        {
            try
            {

                Busyness result = new Busyness(Obj);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, result.result);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}