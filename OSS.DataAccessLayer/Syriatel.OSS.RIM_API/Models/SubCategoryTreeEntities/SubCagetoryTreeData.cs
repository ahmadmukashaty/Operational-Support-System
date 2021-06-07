using Syriatel.OSS.RIM_API.Models.CategoryEntities;
using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.Sheard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SubCategoryTreeEntities
{
    public class SubCagetoryTreeData
    {
        public SubCategoryTree subCategoryTree { get; set; }

        private List<TreeModelView> TreeNodes { get; set; }
        public SubCagetoryTreeData(int subCategoryId)
        {
            this.subCategoryTree = new SubCategoryTree();
            this.subCategoryTree.SbTreeBasic = OracleHelper.GetSubCategoryRoot(subCategoryId);

            if (this.subCategoryTree.SbTreeBasic != null)
            {
                this.TreeNodes = new List<TreeModelView>();
                this.TreeNodes = OracleHelper.GetSubCategoryTrees(this.subCategoryTree.SbTreeBasic.Id);
            }

            if(this.TreeNodes != null)
            {
                List<TreeModelView> parentTrees = GetAllParents();

                if (parentTrees != null)
                {
                    parentTrees.Sort(delegate(TreeModelView c1, TreeModelView c2) { return c1.Order.CompareTo(c2.Order); });
                }

                foreach(TreeModelView treeRoot in parentTrees)
                {
                    TreeCreation tc = new TreeCreation(treeRoot.Id, this.TreeNodes);
                    if(this.subCategoryTree.Trees == null)
                        this.subCategoryTree.Trees = new List<Tree>();

                    subCategoryTree.Trees.Add(tc.tree);
                }
            }
            //use foreach to create tree for each parent
        }

        private List<TreeModelView> GetAllParents()
        {
            List<TreeModelView> parents = null;
            foreach(TreeModelView tmv in this.TreeNodes)
            {
                if(tmv.ParentId == null)
                {
                    if (parents == null)
                        parents = new List<TreeModelView>();

                    parents.Add(tmv);
                }
            }

            return parents;
        }
    }
}