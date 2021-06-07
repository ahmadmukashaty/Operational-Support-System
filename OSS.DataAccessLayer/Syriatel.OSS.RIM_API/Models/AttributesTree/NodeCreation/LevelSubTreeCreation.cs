using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.AttributesTree
{
    public class LevelTreeCreation
    {
        public AttributeTree Tree { get; set; }

        public List<LevelNodeModelView> levelNodes { get; set; }

        public LevelTreeCreation(string levelName, int levelId)
        {
            this.levelNodes = OracleHelper.GetLevelNodes(levelId);

            init(levelName, levelId);
            GenerateLevelNodeTree();
        }

        public void init(string levelName, int levelId)
        {
            NodeData rootAttributes = new NodeData(levelId, false, levelName, null, "Level", null, null);
            this.Tree = new AttributeTree(levelName, rootAttributes, false);
        }

        private void GenerateLevelNodeTree()
        {
            this.levelNodes.Sort(delegate(LevelNodeModelView c1, LevelNodeModelView c2)
            {
                if (c1.Order == null)
                    return 1;
                if (c2.Order == null)
                    return -1;
                return ((int)c1.Order).CompareTo((int)c2.Order);
            });

            foreach(LevelNodeModelView levelNode in levelNodes)
            {
                LevelNodeTreeCreation levelNodeTree = new LevelNodeTreeCreation(levelNode.Name, levelNode.Id, levelNode.TableName);
                if (this.Tree.children == null)
                    this.Tree.children = new List<AttributeTree>();
                this.Tree.children.Add(levelNodeTree.Tree);
            }
        }
    }
}