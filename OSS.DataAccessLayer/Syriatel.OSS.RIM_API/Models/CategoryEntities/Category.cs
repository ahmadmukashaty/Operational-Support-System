using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.Sheard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryEntities
{
    public class Category
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public bool IsOpen { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public List<SubCategory> subCategories { get; set; }

        private List<TreeModelView> subCategoryModelView { get; set; }

        public void SetListSubCategories()
        {
            subCategoryModelView = OracleHelper.GetCategorySubCategories(this.Id);

            SetSubCategoriesInfo();
        }

        private void SetSubCategoriesInfo()
        {
            foreach(TreeModelView scmv in subCategoryModelView)
            {
                if(scmv.ParentId == null)
                {
                    if(this.subCategories == null)
                    {
                        this.subCategories = new List<SubCategory>();
                    }                       
                    SubCategory sb = new SubCategory();
                    sb.Order = scmv.Order;

                    TreeCreation treeCreation = new TreeCreation(scmv.Id, subCategoryModelView);

                    sb.SubCategoryTree = treeCreation.tree;
                
                    this.subCategories.Add(sb);
                }
            }
        }
    }
}