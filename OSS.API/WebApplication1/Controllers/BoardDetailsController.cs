﻿using Syriatel.OSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Syriatel.OSS.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BoardDetailsController : ApiController
    {
        //
        // GET: /BoardDetails/
        public object GET(int Id)
        {
            DataLookup data = new DataLookup();
            object Board = data.getBoardDetails(Id);
            return Board;
        }
	}
}