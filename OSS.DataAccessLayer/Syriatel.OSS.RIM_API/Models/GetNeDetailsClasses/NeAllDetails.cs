using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.GetNeDetailsClasses
{
    public class NeAllDetails
    {
        public List<NeDetail> AllDetails { get; set; }

        public NeAllDetails(string Category, string Type, int id)
        {
            //call oracle helper function
            string TableName = string.Concat(string.Concat(Category, '_'), Type);
            string TableNameType = string.Concat(string.Concat(TableName, '_'), "TYPE");
            string TableNameSite = null;
            if (Type.Equals("NE"))
            {
                TableNameSite = string.Concat(string.Concat(TableName, '_'), "SITE");
            }
            this.AllDetails = OracleHelper.GetAllNeInstanceDetails(TableName, TableNameType, TableNameSite, id, Type, Category);
        }
    }
}