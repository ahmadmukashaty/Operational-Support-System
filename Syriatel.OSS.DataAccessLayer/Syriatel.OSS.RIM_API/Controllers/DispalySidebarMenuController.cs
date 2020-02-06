using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.SidebarMenu;
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
    public class DispalySidebarMenuController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(int moduleBarId)
        {
            SidebarTree sidebarTree = new SidebarTree(moduleBarId);

            return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, sidebarTree.menuItems);
        }

    }
}