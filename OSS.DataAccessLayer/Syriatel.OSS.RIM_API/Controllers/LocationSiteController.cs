using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.LocationSiteClasses;
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
    public class LocationSiteController : ApiController
    {

        // GET api/LocationSiteController
        public ResponseJson Get()
        {
            try
            {
                LocationSiteProcess locationSiteProcess = new LocationSiteProcess();

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, locationSiteProcess.Data);//null here is the data // object data
            }
            catch (Exception ex)
            {
                //exeption message & data=null will be change later
                return new ResponseJson(Constants.FAILED, ex.ToString(), null);
            }
        }

            
    }
}