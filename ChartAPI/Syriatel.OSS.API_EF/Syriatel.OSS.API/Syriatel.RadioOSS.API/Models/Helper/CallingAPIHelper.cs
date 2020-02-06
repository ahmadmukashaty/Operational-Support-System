using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Syriatel.RadioOSS.API.Models.Helper
{
    public class CallingAPIHelper
    {
        public DynamicJoinReturnedData PostBasicAsync(DynamicApiInfo content)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://seserv112/RIM_API_ReportFilter/");

                var json = new JavaScriptSerializer().Serialize(content);
                System.Diagnostics.Debug.WriteLine(json);

                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {


                    var result = client.PostAsync("api/DynamicJoinQuery", stringContent).Result.Content.ReadAsStringAsync().Result;

                    
                    ResponseJsonFromDynamicReport response = new ResponseJsonFromDynamicReport();
                    response = JsonConvert.DeserializeObject<ResponseJsonFromDynamicReport>(result);

                    
                    return response.data;
                }
            }
        }
    }
}