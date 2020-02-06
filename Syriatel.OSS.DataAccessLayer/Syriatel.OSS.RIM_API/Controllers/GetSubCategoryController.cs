using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ResponseTree;
using Syriatel.OSS.RIM_API.Models.SubCategories;
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
    public class GetSubCategoryController : ApiController
    {
        // GET localhost:35072/api/GetSubCategory

        public ResponseJson Get(int CategoryId)
        {
            try
            {
                

                SubCategoriesData subCategoryData = new SubCategoriesData(CategoryId);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, subCategoryData.ReturnTree.TreeData); //subCategoryData.subCategoryData

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}