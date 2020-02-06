using Syriatel.OSS.RIM_API.Models.CategoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Sheard
{
    public class TreeCreation
    {
        public Tree tree { get; set; }

        Dictionary<int, List<TreeModelView>> subCategories { get; set; }
        public TreeCreation(int rootId, List<TreeModelView> subCategoryModelView)
        {
            InitSubCategoriesDictionary(subCategoryModelView);

            CreateTree(rootId);
        }

        private void CreateTree(int rootId)
        {
            TreeModelView scmv = GetSubCategoryModelView(rootId);

            if (scmv != null)
            {
                this.tree = new Tree(scmv.Id, scmv.Name, null);
                int level = 0;
                GererateTree(this.tree, level);
            }
        }

        private void InitSubCategoriesDictionary(List<TreeModelView> subCategoryModelView)
        {
            this.subCategories = new Dictionary<int, List<TreeModelView>>();

            foreach (TreeModelView scmv in subCategoryModelView)
            {
                //Dictionary.add(key, value)
                if (!this.subCategories.ContainsKey(scmv.Level))
                {
                    this.subCategories.Add(scmv.Level, null);
                }

                if (this.subCategories[scmv.Level] == null)
                {
                    this.subCategories[scmv.Level] = new List<TreeModelView>();
                }

                this.subCategories[scmv.Level].Add(scmv);
            }
        }

        private void GererateTree(Tree tree, int level)
        {
            List<TreeModelView> children = GetNodeChildren(tree.id, level + 1);
            if (children != null)
            {
                foreach (TreeModelView child in children)
                {
                    if (tree.children == null)
                        tree.children = new List<Tree>();
                    tree.children.Add(new Tree(child.Id, child.Name, null));
                }
                foreach (Tree child in tree.children)
                {
                    GererateTree(child, level + 1);
                }
            }
        }

        private List<TreeModelView> GetNodeChildren(int parentId, int level)
        {
            List<TreeModelView> children = null;

            if (this.subCategories.ContainsKey(level))
            {
                foreach (TreeModelView scmv in subCategories[level])
                {
                    if (scmv.ParentId == parentId)
                    {
                        if (children == null)
                        {
                            children = new List<TreeModelView>();
                        }
                        children.Add(scmv);
                    }
                }
            }

            if(children != null)
            {
                children.Sort(delegate(TreeModelView c1, TreeModelView c2) 
                {
                    return c1.Order.CompareTo(c2.Order);
                });
            }

            return children;
        }

        private TreeModelView GetSubCategoryModelView(int rootId, int Level = 0)
        {
            foreach(TreeModelView scmv in subCategories[Level])
            {
                if (scmv.Id == rootId)
                    return scmv;
            }
            return null;
        }
        


    }
}