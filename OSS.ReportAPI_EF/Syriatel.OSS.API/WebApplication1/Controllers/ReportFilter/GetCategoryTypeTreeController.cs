using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.Trees.CategoryRanTypeTree;
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
    public class GetCategoryTypeTreeController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string ClassificationName)
        {
            try
            {
                CategoryTypeTreeCreation categoryTypeTree = new CategoryTypeTreeCreation(ClassificationName.ToLower());

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, categoryTypeTree.Tree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}
