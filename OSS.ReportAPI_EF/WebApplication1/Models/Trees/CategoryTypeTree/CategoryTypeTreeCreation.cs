using Newtonsoft.Json;
using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using Syriatel.OSS.API.Models.Trees.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Trees.CategoryRanTypeTree
{
    public class CategoryTypeTreeCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        public TreeModel Tree { get; set; }

        private List<IsMainModelView> Types { get; set; }


        public CategoryTypeTreeCreation(string classificationName)
        {
            int classificationId = OracleHelper.GetClassificationID(classificationName);

            if (classificationId != 0)
            {

                this.Types = OracleHelper.GetClassificationIsMainData(classificationId, Constants.Types.TYPE);
            }

            if (this.Types != null)
            {
                init();
                GenerateCategoryTypesTree();
            }
        }

        public void init()
        {
            TypeNodeData rootAttributes = new TypeNodeData();
            this.Tree = new TreeModel("Types Tree", rootAttributes, false);
            this.Tree.parent = null;
        }

        private void GenerateCategoryTypesTree()
        {
            foreach (IsMainModelView typeNode in this.Types)
            {
                List<string> typeData = OracleHelper.GetTableColumnData(typeNode.TableName, typeNode.ColumnName);

                //var rootTreeserialized = JsonConvert.SerializeObject(this.Tree);
                //TreeModel rootTree = JsonConvert.DeserializeObject<TreeModel>(rootTreeserialized);

                if (typeData != null)
                {
                    TypeNodeData rootAttributes = new TypeNodeData(typeNode.TableName, typeNode.ColumnName, typeNode.ColumnType);
                    TreeModel typeSubTree = new TreeModel(typeNode.Name, rootAttributes, false);


                    var SubTreeSerialized = JsonConvert.SerializeObject(typeSubTree);
                    TreeModel typeSubTreeClone = JsonConvert.DeserializeObject<TreeModel>(SubTreeSerialized);
                    typeSubTreeClone.children = null;

                    //typeSubTree.parent = rootTree;

                    foreach (string node in typeData)
                    {
                        if (typeSubTree.children == null)
                            typeSubTree.children = new List<TreeModel>();
                        TreeModel leafNode = new TreeModel(node, null, true);

                        leafNode.parent = typeSubTreeClone;
                        typeSubTree.children.Add(leafNode);
                    }

                    if (this.Tree.children == null)
                        this.Tree.children = new List<TreeModel>();

                    this.Tree.children.Add(typeSubTree);
                }
            }
        }
    }
}