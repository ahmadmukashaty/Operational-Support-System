using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class MenuItem
    {
        public int Category_Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsOpen { get; set; }

        public string Title { get; set; }

        public List<Tab> Tabs { get; set; }

        private List<TabModelView> subCategories { get; set; }

        public void SetTabs()
        {
            this.subCategories = OracleHelper.GetTabs(this.Category_Id);

            this.subCategories.Sort(delegate(TabModelView c1, TabModelView c2) { return c1.Order.CompareTo(c2.Order); });

            SetSubCategoriesInfo();
        }

        private void SetSubCategoriesInfo()
        {
            foreach (TabModelView tmv in subCategories)
            {
                if (tmv.ParentId == null)
                {
                    if (this.Tabs == null)
                    {
                        this.Tabs = new List<Tab>();
                    }

                    TabTreeCreation treeCreation = new TabTreeCreation(tmv.Id, subCategories);

                    Tab tab = treeCreation.tree;

                    this.Tabs.Add(tab);
                }
            }
        }
    }
}