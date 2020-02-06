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
    public class SiteMapInfoController : ApiController
    {
        // GET: api/SiteMapInfo
        private DataLookup OracleHelper = new DataLookup();

        [HttpGet]
        [Route("api/SiteMapInfo")]
        public ResponseJson Get()
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetMapInfo();
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/ByRegion")]
        public ResponseJson GetByRegion(string Region)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationByRegion(Region);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/ByArea")]
        public ResponseJson GetByArea(string Area)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationByArea(Area);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/ByZone")]
        public ResponseJson GetByZone(string Area, string Zone)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationByZone(Area, Zone);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/BySubArea")]
        public ResponseJson GetBySubArea(string Area, string Zone, string SubArea)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationBySubArea(Area, Zone, SubArea);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/BySiteName")]
        public ResponseJson GetBySiteName(string Area, string Zone, string SubArea, string SiteName)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationBySiteName(Area, Zone, SubArea, SiteName);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }

        [HttpGet]
        [Route("api/SiteMapInfo/BySiteCode")]
        public ResponseJson GetBySiteCode(string SiteCode)
        {
            try
            {
                List<MapInfo> MapInfo = OracleHelper.GetSitesLocationBySiteCode(SiteCode);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, MapInfo);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
