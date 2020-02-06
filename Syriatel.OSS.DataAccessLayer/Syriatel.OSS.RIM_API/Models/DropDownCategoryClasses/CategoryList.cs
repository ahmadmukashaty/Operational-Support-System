using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.DropDownCategoryClasses
{
    public class CategoryList
    {
        public List<CategoryItem> DropDownCategoryList { get; set; }

        public CategoryList(string ModelName)
        {
            //call oracle helper function
            this.DropDownCategoryList = OracleHelper.GetDropDownListCategory(ModelName);//oracle helper function

        }
        
    }
}