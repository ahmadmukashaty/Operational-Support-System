using Syriatel.OSS.Context;
using Syriatel.OSS.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.Data
{
    public class DataBehaviour
    {
        private OSS_RIM_Context _context { get; set; }

        public DataBehaviour()
        {
            _context = new OSS_RIM_Context();
        }

        public List<Module> GetAllModules()
        {
            return _context.Modules.ToList();
        }

        //functions
    }
}
