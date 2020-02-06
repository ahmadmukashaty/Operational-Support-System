using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.TranssmissionOSS.API.NeTreeModeView
{
    public class Instance
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public decimal? ParentId { get; set; }

        public string DisName { get; set; }
    }
}