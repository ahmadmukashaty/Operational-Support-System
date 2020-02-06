using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Trees.AttributesRanTree.ModelViews;
using Syriatel.OSS.API.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.AttributesRanTree.NodeCreation
{
    public class AttributeTreeCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        public TreeModel Tree { get; set; }

        private List<LevelModelView> Levels { get; set; }

        public AttributeTreeCreation(string classificationName)
        {
            int classificationId = OracleHelper.GetClassificationID(classificationName);

            if (classificationId != 0)
            {
                this.Levels = OracleHelper.GetClassificationLevels(classificationId);
            }

            if (this.Levels != null)
            {
                init();
                GenerateLevelTree(null, this.Tree);
            }
        }

        public void init()
        {
            AttributeNodeData rootAttributes = new AttributeNodeData();
            this.Tree = new TreeModel("Attributes", rootAttributes, false);
            this.Tree.parent = null;
        }

        private void GenerateLevelTree(int? parent, TreeModel tree)
        {
            List<LevelModelView> levels = GetChildrenLevels(parent);
            if (levels != null)
            {
                foreach (LevelModelView level in levels)
                {
                    LevelTreeCreation levelTree = new LevelTreeCreation(level.Name, level.TableName, level.LevelId, level.Id);
                    if (levelTree.Tree != null)
                    {
                        if (tree.children == null)
                            tree.children = new List<TreeModel>();

                        tree.children.Add(levelTree.Tree);
                    }
                }

                foreach (TreeModel child in tree.children)
                {
                    if (child.leaf == false && child.label != "Attributes")
                    {
                        AttributeNodeData nodeAttribute = (AttributeNodeData)child.data;

                        GenerateLevelTree(nodeAttribute.Id, child);
                    }

                }
            }
        }

        private List<LevelModelView> GetChildrenLevels(int? parent)
        {
            List<LevelModelView> children = null;

            foreach (LevelModelView lmv in Levels)
            {
                if (lmv.ParentId == parent)
                {
                    if (children == null)
                        children = new List<LevelModelView>();
                    children.Add(lmv);
                }
            }
            if (children != null)
            {
                children.Sort(delegate (LevelModelView c1, LevelModelView c2)
                {
                    if (c1.Order == null)
                        return 1;
                    if (c2.Order == null)
                        return -1;
                    return ((int)c1.Order).CompareTo((int)c2.Order);
                });
            }


            return children;
        }
    }
}