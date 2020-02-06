using Syriatel.OSS.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.TreeDetails
{
    public class RanTreeDetailsCreation
    {
        private DataLookup OracleHelper = new DataLookup();

        private NodeDataModule Module = null;

        private List<NodeDataCategory> Categories = null;

        private List<NodeDataSubCategory> SubCategories = null;

        private List<NodeDataSite> Sites = null;

        private List<NodeDataController> Controllers = null;

        private List<NodeDataAbstract> AbstractLevels = null;

        public TreeDetailsNode Tree = null;

        private TreeDetailsNode RadioAbsractLevel = null;

        private TreeDetailsNode ControllerAbsractLevel = null;

        public RanTreeDetailsCreation(string moduleName)
        {
            Init(moduleName);
            GenerateTree();
        }

        private void Init(string moduleName)
        {
            int moduleId = OracleHelper.GetModuleID(moduleName);

            if (moduleId != 0)
            {
                this.Module = new NodeDataModule(moduleId, moduleName.ToUpper());
                this.Categories = OracleHelper.GetAllCategories(moduleId);                
                this.Sites = OracleHelper.GetAllSites();
                this.Controllers = OracleHelper.GetAllControllers();
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
                if (c.Name.ToLower() == "radio")
                {
                    subTree.children = GetRadioTree(c.Id);
                    if (subTree.children == null)
                        subTree.leaf = true;
                }
                else if (c.Name.ToLower() == "controller")
                {
                    this.SubCategories = OracleHelper.GetAllSubCategories(c.Id);
                    if (this.SubCategories != null)
                    {
                        subTree.children = GetRanTree(c.Id);
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

        private List<TreeDetailsNode> GetRanTree(int category_Id)
        {
            List<TreeDetailsNode> RanTree = new List<TreeDetailsNode>();
            foreach(NodeDataSubCategory sub in this.SubCategories)
            {
                TreeDetailsNode subCategory = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                subCategory.label = sub.Name;
                subCategory.data = sub.Id;
                subCategory.children = GetControllerTree(sub.Id, category_Id);
                if (subCategory.children == null)
                    subCategory.leaf = true;
                RanTree.Add(subCategory);
            }

            if (RanTree.Count > 0)
                return RanTree;

            return null;
        }

        private List<TreeDetailsNode> GetControllerTree(int SubCategory_id, int category_Id)
        {
            List<TreeDetailsNode> ControllerTree = new List<TreeDetailsNode>();

            foreach (NodeDataController con in this.Controllers)
            {
                if (con.SubCategory_Id == SubCategory_id)
                {
                    TreeDetailsNode controller = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                    controller.label = con.Name;
                    controller.data = con.Id;
                    controller.type = "Controller";
                    GenerateAbstractControllerTree(category_Id, con.Id);
                    controller.children = this.ControllerAbsractLevel.children;
                    if (controller.children == null)
                        controller.leaf = true;
                    ControllerTree.Add(controller);
                }
            }

            if (ControllerTree.Count > 0)
                return ControllerTree;

            return null;
        }

        private List<TreeDetailsNode> GetRadioTree(int category_id)
        {
            List<TreeDetailsNode> RadioTree = new List<TreeDetailsNode>();
            
            foreach(NodeDataSite site in this.Sites)
            {
                TreeDetailsNode SiteTree = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                SiteTree.label = site.Name;
                SiteTree.data = site.Id;
                SiteTree.type = "Site";
                GenerateAbstractRadioTree(category_id, site.Id);
                SiteTree.children = this.RadioAbsractLevel.children;
                if (SiteTree.children == null)
                    SiteTree.leaf = true;
                RadioTree.Add(SiteTree);
            }

            if (RadioTree.Count > 0)
                return RadioTree;

            return null;
        }

        private void GenerateAbstractRadioTree(int category_id, int site_Id)
        {
            NodeDataAbstract parent = GetParentId(category_id);

            if (parent != null)
            {
                this.RadioAbsractLevel = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                RadioAbsractLevel.label = parent.Name;
                RadioAbsractLevel.data = site_Id;
                RadioAbsractLevel.type = parent.Type;
                GenerateTreeAbsract(parent.Id, this.RadioAbsractLevel, category_id, site_Id);
                if (this.RadioAbsractLevel.children == null)
                    this.RadioAbsractLevel.leaf = true;
            }
        }

        private void GenerateAbstractControllerTree(int category_id, int controller_Id)
        {
            NodeDataAbstract parent = GetParentId(category_id);

            if (parent != null)
            {
                this.ControllerAbsractLevel = new TreeDetailsNode(false, "fa-folder", "fa-folder-open");
                ControllerAbsractLevel.label = parent.Name;
                ControllerAbsractLevel.data = parent.Id;
                ControllerAbsractLevel.type = parent.Type;
                GenerateTreeAbsract(parent.Id, this.ControllerAbsractLevel, category_id, controller_Id);
                if (this.ControllerAbsractLevel.children == null)
                    this.ControllerAbsractLevel.leaf = true;
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
                children.Sort(delegate (NodeDataAbstract c1, NodeDataAbstract c2)
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