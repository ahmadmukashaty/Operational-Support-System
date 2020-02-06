using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ResponseTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SubCategories
{
    public class SubCategoriesData
    {
        public TreeResponseJason ReturnTree { get; set; }
        public SubCategoryTree subCategoryData { get; set; }

        private List<string> subCategories { get; set; }

        private string categoryName { get; set; }

        private int staticId { get; set; }

        public SubCategoriesData(int CategoryId)
        {
            this.subCategories = OracleHelper.GetSubCategories(CategoryId);
            this.categoryName = OracleHelper.GetCategoryName(CategoryId);
            Init();
            AddSubCategories();
            // TODO: Complete member initialization
            
        }

        private void Init()
        {
            //this.ReturnTree = new TreeResponseJason();
            this.subCategoryData = new SubCategoryTree();
            this.subCategoryData.id = this.staticId++;
            this.subCategoryData.name = this.categoryName;
        }

        private void AddSubCategories()
        {

            TreeJason treeRoot = new TreeJason();


            foreach(string subCategory in subCategories)
            {
                if (this.subCategoryData.children == null)
                    this.subCategoryData.children = new List<SubCategoryTree>();


                
                SubCategoryTree subCategoryTree = new SubCategoryTree();
                subCategoryTree.id = this.staticId++;
                subCategoryTree.name = subCategory;
                subCategoryTree.children = null;
                

                this.subCategoryData.children.Add(subCategoryTree);



                
                //TreeJason treeRoot = new TreeJason();
                if (this.ReturnTree == null)
                {
                    this.ReturnTree = new TreeResponseJason();
                    this.ReturnTree.TreeData = new List<TreeJason>();
                    treeRoot.label = this.categoryName;
                    treeRoot.children = new List<TreeJason>();
                    treeRoot.selectable = true;
                }
                TreeJason treeItem = new TreeJason();
                treeItem.label = subCategory;
                treeItem.leaf = true;
                treeItem.selectable = true;
                treeItem.parent = new TreeJason();
                treeItem.parent.label = treeRoot.label;
                treeItem.parent.data = treeRoot.data;
                treeItem.parent.children = null;

                //treeItem.data = this.subCategoryData;
                treeRoot.children.Add(treeItem);

                
                
               
            }
          
            this.ReturnTree.TreeData.Add(treeRoot);
        }

    }
}