using Oracle.DataAccess.Client;
using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ModuleEnities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
//using System.Web.Http.Cors;

namespace Syriatel.OSS.RIM_API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShowModulesController : ApiController
    {
        public ResponseJson Get()
        {
            ModuleData md = new ModuleData();

            return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, md.modules);
        }
    }
}