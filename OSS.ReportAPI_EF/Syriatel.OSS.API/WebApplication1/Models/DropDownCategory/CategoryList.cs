using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.DropDownCategory
{
    public class CategoryList
    {
        private DataLookup OracleHelper = new DataLookup();

        public List<CategoryItem> DropDownCategoryList { get; set; }

        public CategoryList(string moduleName)
        {

            int moduleId = OracleHelper.GetModuleID(moduleName);

            if (moduleId != -1)
            {
                this.DropDownCategoryList = OracleHelper.GetDropDownListCategory(moduleId);
                this.DropDownCategoryList.Sort(delegate(CategoryItem c1, CategoryItem c2)
                {
                    if (c1.Order == null)
                        return 1;
                    if (c2.Order == null)
                        return -1;
                    return ((int)c1.Order).CompareTo((int)c2.Order);
                });
            }

        }
    }
}