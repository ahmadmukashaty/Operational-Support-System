using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.MAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;


namespace Syriatel.OSS.API.Controllers.Map
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SiteNameByCodeController : ApiController
    {
        // GET: api/    
        private DataLookup OracleHelper = new DataLookup();

        [HttpGet]
        public ResponseJson Get(string SiteCode)
        {
            try
            {
                string SiteName = OracleHelper.GetSiteNameBySiteCode(SiteCode);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, SiteName);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }


    }
}
