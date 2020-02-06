using Syriatel.OSS.RIM_API.Models.Attributes;
using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class AttributeTreeCreatrion
    {
        public AttributeTree Tree { get; set; }

        private List<LevelModelView> Levels { get; set; }

        public AttributeTreeCreatrion(string moduleName, string categoryName)
        {
            int moduleId = OracleHelper.GetModuleID(moduleName);

            if(moduleId != -1)
            {
                int categoryId = OracleHelper.GetCategoryID(categoryName, moduleId);

                if(categoryId != -1)
                {
                    this.Levels = OracleHelper.GetCategoryLevels(categoryId);
                }
            }

            init();
            GenerateLevelTree(null, this.Tree);

        }

        public void init()
        {
            NodeData rootAttributes = new NodeData(0, true, null, null, "Root", null, null);
            this.Tree = new AttributeTree("Attributes", rootAttributes, false);
        }

        private void GenerateLevelTree(int? parent,AttributeTree tree)
        {
            List<LevelModelView> levels = GetChildrenLevels(parent);
            if(levels != null)
            {
                foreach(LevelModelView level in levels)
                {
                    LevelTreeCreation levelTree = new LevelTreeCreation(level.Name, level.Id);
                    if (tree.children == null)
                        tree.children = new List<AttributeTree>();

                    tree.children.Add(levelTree.Tree);
                }

                foreach(AttributeTree child in tree.children)
                {
                    NodeData nodeAttribute = (NodeData)child.data;
                    if(nodeAttribute.NodeType == "Level")
                    {
                        GenerateLevelTree(nodeAttribute.Id, child);
                    }
                }
            }
        }

        private List<LevelModelView> GetChildrenLevels(int? parent)
        {
            List<LevelModelView> children = null;

            foreach(LevelModelView lmv in Levels)
            {
                if(lmv.ParentId == parent)
                {
                    if (children == null)
                        children = new List<LevelModelView>();
                    children.Add(lmv);
                }
            }
            children.Sort(delegate(LevelModelView c1, LevelModelView c2) 
            {
                if (c1.Order == null)
                    return 1;
                if (c2.Order == null)
                    return -1;
                return ((int)c1.Order).CompareTo((int)c2.Order);
            });


            return children;
        }
    }
}