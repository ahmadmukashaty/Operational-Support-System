using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.ModuleEnities
{
    public class ModuleData
    {
        public List<ModuleModelView> modules = null;

        public ModuleData()
        {
            modules = OracleHelper.GetAllModules();
        }
    }
}