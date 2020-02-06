using Syriatel.OSS.RIM_API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.SidebarMenu
{
    public class SidebarTree
    {
        public List<MenuItem> menuItems { get; set; }

        private List<CategoryModelView> Categories { get; set; }

        public SidebarTree(int moduleBarId)
        {
            Categories = OracleHelper.GetSidebarCategories(moduleBarId);

            Categories.Sort(delegate(CategoryModelView c1, CategoryModelView c2) { return c1.Order.CompareTo(c2.Order); });

            foreach(CategoryModelView cmv in Categories)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Category_Id = cmv.Id;
                menuItem.CategoryName = cmv.Name;
                menuItem.Title = cmv.Title;
                menuItem.IsOpen = cmv.IsOpen;
                menuItem.SetTabs();
                //menuItem.Tabs = null;

                if (this.menuItems == null)
                    this.menuItems = new List<MenuItem>();

                menuItems.Add(menuItem);
            }
        }
    }
}