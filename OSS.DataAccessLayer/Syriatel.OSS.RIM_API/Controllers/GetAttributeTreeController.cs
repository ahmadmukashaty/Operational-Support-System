using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.AttributesTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Syriatel.OSS.RIM_API.Controllers
{
    public class GetAttributeTreeController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string moduleName, string categoryName)
        {
            try
            {
                AttributeTreeCreatrion attributeTree = new AttributeTreeCreatrion(moduleName, categoryName);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, attributeTree.Tree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}