using Syriatel.OSS.API.Models.DropDownCategory;
using Syriatel.OSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DropDownCategoryController : ApiController
    {
        // GET api/<controller>
        public ResponseJson Get(string moduleName)
        {
            try
            {
                CategoryList Cat_list = new CategoryList(moduleName.ToLower());
                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, Cat_list.DropDownCategoryList);
            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.ToString(), null);
            }
        }
    }
}