using Syriatel.NPM.WebAPI.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class TabTreeCreation
    {
        public Tab tree { get; set; }

        Dictionary<int, List<TabModelView>> subCategories { get; set; }

        public TabTreeCreation(int rootId, List<TabModelView> tabsModelView)
        {
            InitSubCategoriesDictionary(tabsModelView);

            CreateTree(rootId);
        }

        private void InitSubCategoriesDictionary(List<TabModelView> tabModelView)
        {
            this.subCategories = new Dictionary<int, List<TabModelView>>();

            foreach (TabModelView tmv in tabModelView)
            {
                //Dictionary.add(key, value)
                if (!this.subCategories.ContainsKey(tmv.Level))
                {
                    this.subCategories.Add(tmv.Level, null);
                }

                if (this.subCategories[tmv.Level] == null)
                {
                    this.subCategories[tmv.Level] = new List<TabModelView>();
                }

                this.subCategories[tmv.Level].Add(tmv);
            }
        }

        private void CreateTree(int rootId)
        {
            TabModelView tmv = GetTabModelView(rootId);

            if (tmv != null)
            {
                this.tree = new Tab(tmv);
                int level = 0;
                GererateTree(this.tree, level);
            }
        }

        private TabModelView GetTabModelView(int rootId, int Level = 0)
        {
            foreach (TabModelView tmv in subCategories[Level])
            {
                if (tmv.Id == rootId)
                    return tmv;
            }
            return null;
        }

        private void GererateTree(Tab tree, int level)
        {
            List<TabModelView> children = GetNodeChildren(tree.Id, level + 1);
            if (children != null)
            {
                foreach (TabModelView child in children)
                {
                    if (tree.children == null)
                    {
                        tree.Type = Constants.TAB_TYPE_SUB;
                        tree.children = new List<Tab>();
                    }
                    tree.children.Add(new Tab(child));
                }
                foreach (Tab child in tree.children)
                {
                    GererateTree(child, level + 1);
                }
            }
        }

        private List<TabModelView> GetNodeChildren(int parentId, int level)
        {
            List<TabModelView> children = null;

            if (this.subCategories.ContainsKey(level))
            {
                foreach (TabModelView scmv in subCategories[level])
                {
                    if (scmv.ParentId == parentId)
                    {
                        if (children == null)
                        {
                            children = new List<TabModelView>();
                        }
                        children.Add(scmv);
                    }
                }
            }

            if (children != null)
            {
                children.Sort(delegate(TabModelView c1, TabModelView c2) { return c1.Order.CompareTo(c2.Order); });
            }

            return children;
        }
    }
}