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
    public class SiteCodeByNameController : ApiController
    {
        // GET: api/SiteCodeByName
        private DataLookup OracleHelper = new DataLookup();

        [HttpGet]
        public ResponseJson Get(string Area, string Zone, string SubArea, string SiteName)
        {
            try
            {
                string SiteCode = OracleHelper.GetSiteCodeBySiteName(Area, Zone, SubArea, SiteName);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, SiteCode);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
