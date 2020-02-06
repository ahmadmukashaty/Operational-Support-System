using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.CategoryTree;
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
    public class GetCategoryTreeController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get()
        {
            try
            {
                CategoryData categoryTree = new CategoryData("Transmission");

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, categoryTree.return_tree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}