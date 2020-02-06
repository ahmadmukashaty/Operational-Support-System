using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.DropDownCategoryClasses;
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
    public class DropDownCategoryController : ApiController
    {
        // GET api/DropDownCategory
        public ResponseJson Get(string ModelName)
        {
            try
            {
                CategoryList Cat_list = new CategoryList(ModelName);
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE,Cat_list.DropDownCategoryList);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.ToString(), null);
            }
        }

        
    }
}