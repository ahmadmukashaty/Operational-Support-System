using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.Site;
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
    public class LocationSiteController : ApiController
    {
        // GET api/<controller>
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