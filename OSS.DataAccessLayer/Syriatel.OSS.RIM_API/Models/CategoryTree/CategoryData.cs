using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ResponseTree;
using Syriatel.OSS.RIM_API.Models.SubCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.CategoryTree
{
    public class CategoryData
    {
        public TreeJason return_tree { get; set; }
        public CategoryTree CategoryTreeData { get; set; }

        private List<string> subCategories { get; set; }

        private string categoryName { get; set; }

        private int staticId { get; set; }

        public CategoryData(string ModelName)
        {
            Init();
            List<int> categoriesIds = OracleHelper.GetCategoryIds(ModelName);
            foreach (int CategoryId in categoriesIds)
            {
                this.subCategories = OracleHelper.GetSubCategories(CategoryId);
                this.categoryName = OracleHelper.GetCategoryName(CategoryId);
                if (subCategories != null && categoryName != null)
                {
                    AddSubCategories();
                }
            }

            // TODO: Complete member initialization
            
        }

        private TreeJason InitCategoryTree()
        {
            //CategoryTree CategoryTree = new CategoryTree();
            //CategoryTree.id = this.staticId++;
            //CategoryTree.name = this.categoryName;

            //return CategoryTree;


            TreeJason tr = new TreeJason();
            tr.label = this.categoryName;
            return tr;
        }


        private void Init()
        {
            //this.CategoryTreeData = new CategoryTree();
            //this.CategoryTreeData.id = this.staticId++;
            //this.CategoryTreeData.name = "NE Categories";

            this.return_tree = new TreeJason();
            this.return_tree.label = "NE Categories";
            this.return_tree.selectable = true;
            
            this.return_tree.parent = null;
        }

        private void AddSubCategories()
        {
            //CategoryTree CategoryTree = InitCategoryTree();
            TreeJason tr = InitCategoryTree();
            foreach(string subCategory in subCategories)
            {
                if (tr.children == null)
                    tr.children = new List<TreeJason>();

                //CategoryTree subCategoryTree = new CategoryTree();
                //subCategoryTree.id = this.staticId++;
                //subCategoryTree.name = subCategory;
                //subCategoryTree.children = null;


                TreeJason subcategory = new TreeJason();
                subcategory.label = subCategory;
                subcategory.selectable = true;
                subcategory.leaf = true;
                subcategory.children = null;
                subcategory.parent = new TreeJason();
                subcategory.parent.label = tr.label;
                subcategory.parent.data = tr.data;
                subcategory.parent.children = null;
                

                tr.children.Add(subcategory);
                tr.selectable = true;
                //CategoryTree.children.Add(subCategoryTree);
            }

            if (this.return_tree.children == null)
                this.return_tree.children = new List<TreeJason>();

            tr.parent = new TreeJason();
            tr.parent.label = this.return_tree.label;
            tr.parent.data = this.return_tree.data;
            tr.parent.children = null;

            this.return_tree.children.Add(tr);
        }
    }
}