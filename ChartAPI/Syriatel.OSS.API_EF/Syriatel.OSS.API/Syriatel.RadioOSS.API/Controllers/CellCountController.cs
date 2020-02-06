using Syriatel.RadioOSS.API.Models.CellCount;
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
    public class CellCountController : ApiController
    {
        [System.Web.Http.HttpPost]
        public ResponseJson Post(CellCountPostObject Obj)

        {
            try
            {

                CellCount result = new CellCount(Obj);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, result.result);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

    }
}