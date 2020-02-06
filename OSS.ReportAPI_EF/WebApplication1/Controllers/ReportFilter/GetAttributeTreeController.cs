using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.Trees.AttributesRanTree.NodeCreation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Syriatel.OSS.API.Controllers.CategoryReport
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetAttributeTreeController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string ClassificationName)
        {
            try
            {
                AttributeTreeCreation attributeTree = new AttributeTreeCreation(ClassificationName.ToLower());

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, attributeTree.Tree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
