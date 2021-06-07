using Syriatel.NPM.WebAPI.Models.BoModels;
using Syriatel.NPM.WebAPI.Models.Helper;
using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.NeTypes;
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
    public class GetAllNeTypesController : ApiController
    {
        // GET api/GetAllNeTypes
        public ResponseJson Get()
        {
            try
            {
                //List<GroupAttributeModelView> AttributesGroups = new List<GroupAttributeModelView>();
                //AttributesGroups = OracleHelper.GetAttributesGroup(CategoryId);
                //AttributeTreeCreation attributeTreeCreation = new AttributeTreeCreation(CategoryId);
                NeTypesData neTypes = new NeTypesData("Transmission");

                return new ResponseJson(Constants.SUCCESSED, Constants.Messages.EMPTY_MESSAGE, neTypes.returnTree);

            }
            catch (Exception ex)
            {
                return new ResponseJson(Constants.FAILED, ex.Message, null);
            }
        }
    }
}