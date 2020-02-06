using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryEntities
{
    public class CategoryData
    {
        public List<Category>  categories { get; set; }

        public CategoryData(int moduleBarId)
        {
            categories = OracleHelper.GetModuleCategories(moduleBarId);
            

            foreach(Category category in categories)
            {
                category.SetListSubCategories();
            }
        }


    }
}