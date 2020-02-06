using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.Attributes;
using Syriatel.OSS.RIM_API.Models.Helper;
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
    public class GetAttributesController : ApiController
    {
        // GET api/GetAttributes
        public ResponseJson Get(int CategoryId)
        {
            try
            {
                //List<GroupAttributeModelView> AttributesGroups = new List<GroupAttributeModelView>();
                //AttributesGroups = OracleHelper.GetAttributesGroup(CategoryId);
                AttributeTreeCreation attributeTreeCreation = new AttributeTreeCreation(CategoryId);

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, attributeTreeCreation.tree2);

            }
            catch(Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}