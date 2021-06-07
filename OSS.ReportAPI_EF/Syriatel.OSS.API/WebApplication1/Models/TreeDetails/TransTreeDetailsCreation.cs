using Syriatel.OSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.TreeDetails
{
    public class TransTreeDetailsCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        private NodeDataModule Module = null;

        private List<NodeDataCategory> Categories = null;

        private List<NodeDataSubCategory> SubCategories = null;

        private List<NodeDataNE> DatacomNEs = null;

        private List<NodeDataNE> OpticalNEs = null;

        private List<NodeDataNE> MwNEs = null;

        private List<NodeDataNE> FirewallNEs = null;

        private List<NodeDataAbstract> AbstractLevels = null;

        public TreeDetailsNode Tree = null;

        private TreeDetailsNode NEAbsractLevel = null;

        public TransTreeDetailsCreation(string moduleName)
        {
            Init(moduleName);
            GenerateTree();
        }

                private void Init(string moduleName)
        {
            int moduleId = OracleHelper.GetModuleID(moduleName);

            if (moduleId != 0)
            {
                this.Module = new NodeDataModule(moduleId, moduleName);
                this.Categories = OracleHelper.GetAllCategories(moduleId);                
                this.DatacomNEs = OracleHelper.GetAllDatacomNEs();
                this.OpticalNEs = OracleHelper.GetAllOpticalNEs();
                this.MwNEs = OracleHelper.GetAllMwNEs();
                this.FirewallNEs = OracleHelper.GetAllFirewallNEs();
                this.AbstractLevels = OracleHelper.GetAllAbstractData();
            }
        }

        private void GenerateTree()
        {
            GenerateModule();
        }

        private void GenerateModule()
        {
            this.Tree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
            this.Tree.label = this.Module.Name;
            this.Tree.data = this.Module.Id;
            this.Tree.children = GenerateCategories();
            if (this.Tree.children == null)
                this.Tree.leaf = true;
        }

        private List<TreeDetailsNode> GenerateCategories()
        {
            List<TreeDetailsNode> moduleTree = new List<TreeDetailsNode>();
            foreach(NodeDataCategory c in this.Categories)
            {
                TreeDetailsNode subTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subTree.label = c.Name;
                subTree.data = c.Id;
                if (c.Name.ToLower() == "datacom")
                {
                    this.SubCategories = OracleHelper.GetAllSubCategories(c.Id);
                    if (this.SubCategories != null)
                    {
                        subTree.children = GetDatacomTree(c.Id);
                        if (subTree.children == null)
                            subTree.leaf = true;
                    }
                }
                else if (c.Name.ToLower() == "optical")
                {
                    this.SubCategories = OracleHelper.GetAllSubCategories(c.Id);
                    if (this.SubCategories != null)
                    {
                        subTree.children = GetOpticalTree(c.Id);
                        if (subTree.children == null)
                            subTree.leaf = true;
                    }

                }
                else if (c.Name.ToLower() == "mw")
                {
                    this.SubCategories = OracleHelper.GetAllSubCategories(c.Id);
                    if (this.SubCategories != null)
                    {
                        subTree.children = GetMwTree(c.Id);
                        if (subTree.children == null)
                            subTree.leaf = true;
                    }

                }
                else if (c.Name.ToLower() == "firewall")
                {
                    this.SubCategories = OracleHelper.GetAllSubCategories(c.Id);
                    if (this.SubCategories != null)
                    {
                        subTree.children = GetFirewallTree(c.Id);
                        if (subTree.children == null)
                            subTree.leaf = true;
                    }

                }
                moduleTree.Add(subTree);
            }

            if (moduleTree.Count > 0)
                return moduleTree;

            return null;
        }
        private List<TreeDetailsNode> GetDatacomTree(int category_Id)
        {
            List<TreeDetailsNode> SubCategoryTree = new List<TreeDetailsNode>();
            foreach (NodeDataSubCategory sub in this.SubCategories)
            {
                TreeDetailsNode subCategory = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subCategory.label = sub.Name;
                subCategory.data = sub.Id;
                subCategory.children = GetDatacomTree(sub.Id, category_Id);
                if (subCategory.children == null)
                    subCategory.leaf = true;
                SubCategoryTree.Add(subCategory);
            }

            if (SubCategoryTree.Count > 0)
                return SubCategoryTree;

            return null;
        }

        private List<TreeDetailsNode> GetOpticalTree(int category_Id)
        {
            List<TreeDetailsNode> SubCategoryTree = new List<TreeDetailsNode>();
            foreach (NodeDataSubCategory sub in this.SubCategories)
            {
                TreeDetailsNode subCategory = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subCategory.label = sub.Name;
                subCategory.data = sub.Id;
                subCategory.children = GetOpticalTree(sub.Id, category_Id);
                if (subCategory.children == null)
                    subCategory.leaf = true;
                SubCategoryTree.Add(subCategory);
            }

            if (SubCategoryTree.Count > 0)
                return SubCategoryTree;

            return null;
        }

        private List<TreeDetailsNode> GetMwTree(int category_Id)
        {
            List<TreeDetailsNode> SubCategoryTree = new List<TreeDetailsNode>();
            foreach (NodeDataSubCategory sub in this.SubCategories)
            {
                TreeDetailsNode subCategory = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subCategory.label = sub.Name;
                subCategory.data = sub.Id;
                subCategory.children = GetMwTree(sub.Id, category_Id);
                if (subCategory.children == null)
                    subCategory.leaf = true;
                SubCategoryTree.Add(subCategory);
            }

            if (SubCategoryTree.Count > 0)
                return SubCategoryTree;

            return null;
        }

        private List<TreeDetailsNode> GetFirewallTree(int category_Id)
        {
            List<TreeDetailsNode> SubCategoryTree = new List<TreeDetailsNode>();
            foreach (NodeDataSubCategory sub in this.SubCategories)
            {
                TreeDetailsNode subCategory = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subCategory.label = sub.Name;
                subCategory.data = sub.Id;
                subCategory.children = GetFirewallTree(sub.Id, category_Id);
                if (subCategory.children == null)
                    subCategory.leaf = true;
                SubCategoryTree.Add(subCategory);
            }

            if (SubCategoryTree.Count > 0)
                return SubCategoryTree;

            return null;
        }

        private List<TreeDetailsNode> GetDatacomTree(int SubCategory_id, int category_Id)
        {
            List<TreeDetailsNode> DatacomTree = new List<TreeDetailsNode>();

            foreach (NodeDataNE ne in this.DatacomNEs)
            {
                TreeDetailsNode NeTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                NeTree.label = ne.Name;
                NeTree.data = ne.Id;
                NeTree.type = "Router";
                GenerateAbstractNETree(category_Id, ne.Id);
                NeTree.children = this.NEAbsractLevel.children;
                if (NeTree.children == null)
                    NeTree.leaf = true;
                DatacomTree.Add(NeTree);
            }

            if (DatacomTree.Count > 0)
                return DatacomTree;

            return null;
        }

        private List<TreeDetailsNode> GetOpticalTree(int SubCategory_id, int category_Id)
        {
            List<TreeDetailsNode> OpticalTree = new List<TreeDetailsNode>();

            foreach (NodeDataNE ne in this.OpticalNEs)
            {
                TreeDetailsNode NeTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                NeTree.label = ne.Name;
                NeTree.data = ne.Id;
                NeTree.type = "OPRouter";
                GenerateAbstractNETree(category_Id, ne.Id);
                NeTree.children = this.NEAbsractLevel.children;
                if (NeTree.children == null)
                    NeTree.leaf = true;
                OpticalTree.Add(NeTree);
            }

            if (OpticalTree.Count > 0)
                return OpticalTree;

            return null;
        }

        private List<TreeDetailsNode> GetMwTree(int SubCategory_id, int category_Id)
        {
            List<TreeDetailsNode> MwTree = new List<TreeDetailsNode>();

            foreach (NodeDataNE ne in this.MwNEs)
            {
                TreeDetailsNode NeTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                NeTree.label = ne.Name;
                NeTree.data = ne.Id;
                NeTree.type = "MWRouter";
                GenerateAbstractNETree(category_Id, ne.Id);
                NeTree.children = this.NEAbsractLevel.children;
                if (NeTree.children == null)
                    NeTree.leaf = true;
                MwTree.Add(NeTree);
            }

            if (MwTree.Count > 0)
                return MwTree;

            return null;
        }

        private List<TreeDetailsNode> GetFirewallTree(int SubCategory_id, int category_Id)
        {
            List<TreeDetailsNode> FirewallTree = new List<TreeDetailsNode>();

            foreach (NodeDataNE ne in this.FirewallNEs)
            {
                TreeDetailsNode NeTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                NeTree.label = ne.Name;
                NeTree.data = ne.Id;
                NeTree.type = "FRouter";
                GenerateAbstractNETree(category_Id, ne.Id);
                NeTree.children = this.NEAbsractLevel.children;
                if (NeTree.children == null)
                    NeTree.leaf = true;
                FirewallTree.Add(NeTree);
            }

            if (FirewallTree.Count > 0)
                return FirewallTree;

            return null;
        }

        private void GenerateAbstractNETree(int category_id, int controller_Id)
        {
            NodeDataAbstract parent = GetParentId(category_id);

            if (parent != null)
            {
                this.NEAbsractLevel = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                NEAbsractLevel.label = parent.Name;
                NEAbsractLevel.data = parent.Id;
                NEAbsractLevel.type = parent.Type;
                GenerateTreeAbsract(parent.Id, this.NEAbsractLevel, category_id, controller_Id);
                if (this.NEAbsractLevel.children == null)
                    this.NEAbsractLevel.leaf = true;
            }
        }

        private NodeDataAbstract GetParentId(int category_id)
        {
            foreach (NodeDataAbstract level in this.AbstractLevels)
            {
                if (level.Parent_Id == null && level.Category_Id == category_id)
                    return level;
            }
            return null;
        }

        private void GenerateTreeAbsract(int parentId, TreeDetailsNode radioAbsractLevel, int category_id, int site_or_controller_Id)
        {
            List<NodeDataAbstract> children = GetLevelChildren(parentId, category_id);
            if (children != null)
            {
                foreach (NodeDataAbstract child in children)
                {
                    TreeDetailsNode subTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                    subTree.label = child.Name;
                    subTree.data = site_or_controller_Id;
                    subTree.type = child.Type;
                    GenerateTreeAbsract(child.Id, subTree, category_id, site_or_controller_Id);
                    if (subTree.children == null)
                        subTree.leaf = true;
                    if (radioAbsractLevel.children == null)
                        radioAbsractLevel.children = new List<TreeDetailsNode>();
                    radioAbsractLevel.children.Add(subTree);
                }
            }
        }

        private List<NodeDataAbstract> GetLevelChildren(int parentId, int category_id)
        {
            List<NodeDataAbstract> children = new List<NodeDataAbstract>();
            foreach (NodeDataAbstract child in this.AbstractLevels)
            {
                if (child.Category_Id == category_id && child.Parent_Id == parentId)
                    children.Add(child);
            }
            if (children.Count > 0)
            {
                children.Sort(delegate(NodeDataAbstract c1, NodeDataAbstract c2)
                {
                    if (c1.Order == null)
                        return 1;
                    if (c2.Order == null)
                        return -1;
                    return ((int)c1.Order).CompareTo((int)c2.Order);
                });
                return children;
            }

            return null;
        }
    }
}