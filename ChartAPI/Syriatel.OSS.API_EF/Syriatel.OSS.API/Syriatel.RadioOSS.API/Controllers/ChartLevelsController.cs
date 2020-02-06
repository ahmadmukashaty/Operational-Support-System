using Syriatel.RadioOSS.API.Models.ChartTypeLevels;
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
    public class ChartLevelsController : ApiController
    {
        // GET: ChartTypeLevels
        //http://seserv112/RIM_Charts/api/ChartLevels?CategoryName=radio&ModelName=ran&typeID=1
        public ResponseJson Get(String CategoryName,String ModelName)//4 chart_type, 5 chart_buysiness

        {
            try
            {

                CategoryLevels Levels = new CategoryLevels(CategoryName, ModelName);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, Levels.result);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}