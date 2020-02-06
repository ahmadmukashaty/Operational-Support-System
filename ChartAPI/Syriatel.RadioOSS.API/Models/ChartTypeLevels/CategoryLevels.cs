using Syriatel.RadioOSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.ChartTypeLevels
{
    public class CategoryLevels
    {
        private DataLookup data { get; set; }
        public List<Level> result { get; set; }

        public CategoryLevels(String CategoryName, String ModelName)
        {
            this.data = new DataLookup();
            this.result = this.getChartCategoryLevels(CategoryName, ModelName);
        }

        public List<Level> getChartCategoryLevels (String CategoryName, String ModelName)
        {
            return this.data.GetChartLevel(CategoryName, ModelName);
            
        }
    }
}