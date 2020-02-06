using Syriatel.OSS.RIM_API.Models.Helper;
using Syriatel.OSS.RIM_API.Models.ResponseTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Attributes
{
    public class AttributeTreeCreation
    {
        private List<GroupAttributeModelView> allAttributes { get; set; }

        private int Id { get; set; }

        public GroupNode tree { get; set; }

        public TreeJason tree2 { get; set; }

        public AttributeTreeCreation(int categoryId)
        {
            this.allAttributes = OracleHelper.GetAttributesGroup(categoryId);
            //processing
            Init();
            List<GroupAttributeModelView> roots = GetRootGroups();
            //GenerateTree(this.tree, roots);
            GenerateTree2(this.tree2, roots);
        }

        public void Init()
        {
            this.tree2 = new TreeJason();
            this.tree2.selectable = true;
            GroupNode gn = new GroupNode();
            gn.groupId = 0;
            gn.tableName = null;
            this.tree2.label = "Attributes";

            this.tree2.data = gn;
        }

        private List<GroupAttributeModelView> GetRootGroups()
        {
            List<GroupAttributeModelView> roots = null;

            foreach(GroupAttributeModelView attributeGroup in this.allAttributes)
            {
                if(attributeGroup.ParentId == null)
                {
                    if (roots == null)
                        roots = new List<GroupAttributeModelView>();

                    roots.Add(attributeGroup);
                }
            }

            return roots;
        }

        private List<GroupAttributeModelView> getBranchAttributes(int GroupId)
        {
            List<GroupAttributeModelView> groups = null;

            foreach (GroupAttributeModelView attributeGroup in this.allAttributes)
            {
                if (attributeGroup.ParentId == GroupId)
                {
                    if (groups == null)
                        groups = new List<GroupAttributeModelView>();

                    groups.Add(attributeGroup);
                }
            }

            return groups;
        }

        private void GenerateTree2(TreeJason tree, List<GroupAttributeModelView> children)
        {
            foreach (GroupAttributeModelView group in children)
            {
                if (tree.children == null)
                    tree.children = new List<TreeJason>();

                TreeJason branch = GenerateGroupTree(group);
                branch.parent = new TreeJason();
                branch.parent.label= tree.label;
                branch.parent.data = tree.data;
                branch.parent.children = null;
                
                if (branch != null)
                    tree.children.Add(branch);
            }

            foreach (TreeJason childTree in tree.children)
            {
                if (childTree.data.GetType() == typeof(GroupNode))
                {
                    List<GroupAttributeModelView> groups = getBranchAttributes(((GroupNode)(childTree.data)).groupId);
                    if (groups != null)
                    {
                        GenerateTree2(childTree, groups);
                    }

                }
            }
        }

        private TreeJason GenerateGroupTree(GroupAttributeModelView group)
        {
            TreeJason branch = new TreeJason();
            branch.label = group.GroupName;
            branch.selectable = true;
            GroupNode gn = new GroupNode();
            gn.groupId = group.Id;
            gn.tableName = group.TableName;

            branch.data = gn;

            foreach(Attributes.Attribute attr in group.Attributes)
            {
                if (branch.children == null)
                    branch.children = new List<TreeJason>();

                TreeJason attribute = new TreeJason();
                attribute.label = attr.DisplayName;
                attribute.leaf = true;
                attribute.selectable = true;

                AttributeNode attributeData = new AttributeNode();
                attributeData.AttributeId = attr.Id;
                attributeData.columnName = attr.ColumnName;
                attributeData.columnType = attr.ColumnType;

                attribute.data = attributeData;
                attribute.parent = new TreeJason();
                attribute.parent.label = branch.label;
                attribute.parent.data = branch.data;
                attribute.parent.children = null;

                attribute.parent = new TreeJason();
                attribute.parent.label = branch.label;
                attribute.parent.data = branch.data;
                attribute.parent.children = null;

                branch.children.Add(attribute);
            }

            return branch;

            //GroupNode branch = new GroupNode();
            //branch.id = this.Id++;
            //branch.groupId = group.Id;
            //branch.name = group.GroupName;
            //branch.tableName = group.TableName;
            //foreach(Attributes.Attribute attr in group.Attributes)
            //{
            //    if (branch.children == null)
            //        branch.children = new List<TreeNode>();

            //    AttributeNode attributeTree = new AttributeNode();
            //    attributeTree.id = this.Id++;
            //    attributeTree.AttributeId = attr.Id;
            //    attributeTree.name = attr.DisplayName;
            //    attributeTree.columnName = attr.ColumnName;
            //    attributeTree.columnType = attr.ColumnType;

            //    branch.children.Add(attributeTree);
            //}

            //return branch;
        }
    }
}