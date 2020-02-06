using Newtonsoft.Json;
using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.SubCategoryTree
{
    public class SubCategoryTreeCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        public TreeModel Tree { get; set; }

        private List<SubCategoryModelView> SubCategories { get; set; }

        private List<ModuleModelView> Modules { get; set; }

        private List<CategoryModelView> Categories { get; set; }

        public SubCategoryTreeCreation(string classificationName)
        {
            int classificationId = OracleHelper.GetClassificationID(classificationName);

            if(classificationId != -1)
            {
                this.Modules = OracleHelper.GetClassificationModules(classificationName);
                this.Categories = OracleHelper.GetClassificationCategories(classificationName);
                this.SubCategories = OracleHelper.GetClassificationSubCategories(classificationName);
                init();
            }

            if (this.SubCategories != null && this.Categories != null && this.Modules != null)
            {
                if (this.Modules.Count > 1)
                {
                    GenerateModuleTree(this.Tree);
                }
                else
                {
                    if (this.Categories.Count > 1)
                    {
                        GenerateCategoryTree(this.Tree, this.Modules[0].ID);
                    }
                    else
                    {
                        var rootTreeSerialized = JsonConvert.SerializeObject(this.Tree);
                        TreeModel Parent = JsonConvert.DeserializeObject<TreeModel>(rootTreeSerialized);

                        GenerateSubCategoryTree(this.Tree, this.Categories[0].ID, Parent);
                    }
                }

            }
        }

        private void GenerateModuleTree(TreeModel tree)
        {
            foreach (ModuleModelView Module in this.Modules)
            {
                TreeModel moduleTree = new TreeModel(Module.Name, null, false);
                moduleTree.parent = null;

                List<CategoryModelView> Categories = GetModuleCategories(Module.ID);

                GenerateCategoryTree(moduleTree, Module.ID);

                if (tree.children == null)
                    tree.children = new List<TreeModel>();
                tree.children.Add(moduleTree);
            }
        }

        private List<CategoryModelView> GetModuleCategories(int ModuleID)
        {
            List<CategoryModelView> Categories = new List<CategoryModelView>();

            foreach (CategoryModelView Category in this.Categories)
            {
                if (Category.ModuleID == ModuleID)
                    Categories.Add(Category);

                Categories.Sort(delegate (CategoryModelView c1, CategoryModelView c2)
                {
                    if (c1.Order == null)
                        return 1;
                    if (c2.Order == null)
                        return -1;
                    return ((int)c1.Order).CompareTo((int)c2.Order);
                });

            }

            if (Categories.Count == 0)
                return null;

            return Categories;
        }

        private void GenerateCategoryTree(TreeModel tree, int ModuleID)
        {
            SubCategoryData data = new SubCategoryData();
            data.TableName = Constants.SUB_CATEGORY_TABLE_NAME;
            data.ColumnName = Constants.SUB_CATEGORY_COLUMN_NAME;
            data.ColumnType = Constants.SUB_CATEGORY_COLUMN_TYPE;

            List<CategoryModelView> Categories = GetModuleCategories(ModuleID);

            foreach (CategoryModelView category in Categories)
            {
                TreeModel categoryTree = new TreeModel(category.Name, data, false);
                categoryTree.parent = null;

                var rootTreeSerialized = JsonConvert.SerializeObject(categoryTree);
                TreeModel Parent = JsonConvert.DeserializeObject<TreeModel>(rootTreeSerialized);

                List<SubCategoryModelView> subCategories = GetCategorySubCategories(category.ID);

                foreach (SubCategoryModelView subCategory in subCategories)
                {
                    TreeModel subCategoryTree = new TreeModel(subCategory.Name, null, true);
                    subCategoryTree.parent = Parent;

                    if (categoryTree.children == null)
                        categoryTree.children = new List<TreeModel>();
                    categoryTree.children.Add(subCategoryTree);
                }

                if (tree.children == null)
                    tree.children = new List<TreeModel>();
                tree.children.Add(categoryTree);
            }

        }

        public void init()
        {
            SubCategoryData data = new SubCategoryData();
            data.TableName = Constants.SUB_CATEGORY_TABLE_NAME;
            data.ColumnName = Constants.SUB_CATEGORY_COLUMN_NAME;
            data.ColumnType = Constants.SUB_CATEGORY_COLUMN_TYPE;

            this.Tree = new TreeModel("Sub Category Tree", data, false);
            this.Tree.parent = null;
        }

        private List<SubCategoryModelView> GetCategorySubCategories(int CategoryID)
        {
            List<SubCategoryModelView> subCategories = new List<SubCategoryModelView>();

            foreach (SubCategoryModelView subCategory in this.SubCategories)
            {
                if (subCategory.CategoryID == CategoryID)
                    subCategories.Add(subCategory);
            }

            subCategories.Sort(delegate (SubCategoryModelView c1, SubCategoryModelView c2)
            {
                if (c1.Order == null)
                    return 1;
                if (c2.Order == null)
                    return -1;
                return ((int)c1.Order).CompareTo((int)c2.Order);
            });

            if (subCategories.Count == 0)
                return null;

            return subCategories;
        }

        private void GenerateSubCategoryTree(TreeModel tree, int CategoryId, TreeModel Parent)
        {
            List<SubCategoryModelView> subCategories = GetCategorySubCategories(CategoryId);

            foreach (SubCategoryModelView subCategory in subCategories)
            {
                TreeModel subCategoryTree = new TreeModel(subCategory.Name, null, true);
                subCategoryTree.parent = Parent;

                if (tree.children == null)
                    tree.children = new List<TreeModel>();
                tree.children.Add(subCategoryTree);
            }
        }
    }
}