using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.NeTreeModeView
{
    public class TreeData
    {
        private string CATEGORY_NAME = "DataCom";
        private int Inc_Id { get; set; }
        public Tree tree { get; set; }

        private TreeNodes nodes { get; set; }

        public TreeData()
        {
            int subCategoryId = 1;
            this.nodes = new TreeNodes(subCategoryId);
            Init("CATEGORY", "SUBCATEGORY", subCategoryId);
        }

        private void Init(string Type, string childType, int subCategoryId)
        {
            this.Inc_Id = 1;
            this.tree = new Tree();
            this.tree.NodeId = this.Inc_Id++;
            this.tree.InstanceId = 0;
            this.tree.Name = this.CATEGORY_NAME;
            this.tree.CategoryName = CATEGORY_NAME;
            this.tree.Type = Type;
            this.tree.ChildType = childType;
            this.tree.children = null;
            AddSubCategories(this.tree, subCategoryId, childType, "NE");
        }

        private void AddSubCategories(Tree subTree,int CategoryId,string Type,string childType)
        {
            foreach (Instance subCat in this.nodes.SubCategories)
            {
                Tree node = new Tree();
                node.NodeId = this.Inc_Id++;
                node.InstanceId = subCat.Id;
                node.Name = subCat.Name;
                node.CategoryName = CATEGORY_NAME;
                node.Type = Type;
                node.ChildType = childType;
                node.children = null;
                AddNEs(node, subCat.Id, childType, "BOARD");
                if (subTree.children == null)
                    subTree.children = new List<Tree>();

                subTree.children.Add(node);
            }
        }

        private void AddNEs(Tree subTree, int? SubCategoryId, string Type, string childType)
        {
            List<Instance> SubCategoryNEs = this.nodes.GetSubCategoryNEs(SubCategoryId);
            if (SubCategoryNEs != null)
            {
                foreach (Instance Ne in SubCategoryNEs)
                {
                    //condition
                    Tree node = new Tree();
                    node.NodeId = this.Inc_Id++;
                    node.InstanceId = Ne.Id;
                    node.Name = Ne.Name;
                    node.CategoryName = CATEGORY_NAME;
                    node.Type = Type;
                    node.ChildType = childType;
                    node.children = null;
                    if (subTree.children == null)
                        subTree.children = new List<Tree>();

                    subTree.children.Add(node);
                }
            }

        }
    }
}