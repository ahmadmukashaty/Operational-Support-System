using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.GetNeDetailsClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Syriatel.OSS.RIM_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetNeDetailsController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string Category,string Type,int id)
        {
            try
            {
                NeAllDetails AllData = new NeAllDetails(Category,Type, id);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, AllData.AllDetails);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.ToString(), null);
            }
        }
    }
}