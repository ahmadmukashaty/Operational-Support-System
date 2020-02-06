using Syriatel.RadioOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Syriatel.RadioOSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class getAllSiteCandidatesController : ApiController
    {
      
        // GET: api/getAllSiteCandidates
        public object Get()
        {
            DataLookup data = new DataLookup();
            return data.getAllSiteCandidates();
        }

        // GET: api/getAllSiteCandidates/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/getAllSiteCandidates
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/getAllSiteCandidates/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/getAllSiteCandidates/5
        public void Delete(int id)
        {
        }
    }
}
