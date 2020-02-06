using Oracle.DataAccess.Client;
using Syriatel.OSS.DataAccessLayer.Models.LocationSiteClasses;
using Syriatel.OSS.RIM_API.Models.Attributes;
using Syriatel.OSS.RIM_API.Models.AttributesTree;
using Syriatel.OSS.RIM_API.Models.CategoryEntities;
using Syriatel.OSS.RIM_API.Models.DropDownCategoryClasses;
using Syriatel.OSS.RIM_API.Models.GetNeDetailsClasses;
using Syriatel.OSS.RIM_API.Models.LocationSiteClasses;
using Syriatel.OSS.RIM_API.Models.ModuleEnities;
using Syriatel.OSS.RIM_API.Models.NeTypes;
using Syriatel.OSS.RIM_API.Models.SidebarMenu;
using Syriatel.OSS.RIM_API.Models.SubCategoryTreeEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.RIM_API.Models.Helper
{
    public class OracleHelper
    {
        private static OracleConnection oraConnection = null;

        private static void OpenConnection()
        {
            if (oraConnection.State != ConnectionState.Open)
            {
                oraConnection.Open();
            }
        }

        private static void CloseConnection()
        {
            if (oraConnection.State == ConnectionState.Open)
            {
                oraConnection.Close();
            }
        }

        public static int GetModuleID(string moduleName)
        {
            const string DATA_SELECTED = "ID";
            const string TABLENAME = "RIM_MODULES";
            string CONDITION = "NAME = '" + moduleName + "'";

            int ModuleId = -1;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModuleId = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                        }
                    }
                }

                CloseConnection();

                return ModuleId;
            }
        }

        public static int GetCategoryID(string categoryName, int moduleId)
        {
            const string DATA_SELECTED = "ID";
            const string TABLENAME = "RIM_CATEGORIES";
            string CONDITION = "NAME = '" + categoryName + "' AND MODULE_ID = " + moduleId;

            int CategoryId = -1;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryId = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                        }
                    }
                }

                CloseConnection();

                return CategoryId;
            }
        }

        public static List<LevelModelView> GetCategoryLevels(int categoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_LEVELS";
            string CONDITION = "CATEGORY_ID = " + categoryId;

            List<LevelModelView> levels = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LevelModelView lmv = new LevelModelView();

                            lmv.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            lmv.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            lmv.Order = reader.IsDBNull(reader.GetOrdinal("LEVEL_ORDER")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("LEVEL_ORDER"));
                            lmv.ParentId = reader.IsDBNull(reader.GetOrdinal("PARENT_ID")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("PARENT_ID"));

                            if(levels == null)
                                levels = new List<LevelModelView>();

                            levels.Add(lmv);
                        }
                    }
                }

                CloseConnection();

                return levels;
            }
        }

        public static List<LevelNodeModelView> GetLevelNodes(int levelId)
        {
            const string DATA_SELECTED = " LN.ID, LN.NAME, LN.NODE_TYPE, LN.NODE_ORDER, NT.ID AS TABLEID, NT.NAME AS TABLENAME ";
            const string TABLENAME = " RIM_LEVEL_NODES LN LEFT JOIN RIM_NODE_TABLES NT ON LN.NODE_TABLE_ID = NT.LEVEL_NODE_ID ";
            string CONDITION = " LN.LEVEL_ID = " + levelId;

            List<LevelNodeModelView> levelNodes = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LevelNodeModelView lnmv = new LevelNodeModelView();

                            lnmv.Id = (int)reader.GetInt64(reader.GetOrdinal("TABLEID"));
                            lnmv.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            lnmv.Type = reader.IsDBNull(reader.GetOrdinal("NODE_TYPE")) ? null : reader.GetString(reader.GetOrdinal("NODE_TYPE"));
                            lnmv.Order = reader.IsDBNull(reader.GetOrdinal("NODE_ORDER")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("NODE_ORDER"));
                            lnmv.TableName = reader.IsDBNull(reader.GetOrdinal("TABLENAME")) ? null : reader.GetString(reader.GetOrdinal("TABLENAME"));

                            if (levelNodes == null)
                                levelNodes = new List<LevelNodeModelView>();

                            levelNodes.Add(lnmv);
                        }
                    }
                }

                CloseConnection();

                return levelNodes;
            }
        }

        public static List<AttributeModelView> GetTableAttributes(int tableId)
        {
            const string DATA_SELECTED = " * ";
            const string TABLENAME = " RIM_ATTRIBUTES ";
            string CONDITION = " NODE_ID = " + tableId;

            List<AttributeModelView> attributeModelView = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AttributeModelView amv = new AttributeModelView();

                            amv.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            amv.ColumnName = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            amv.DisplayName = reader.IsDBNull(reader.GetOrdinal("DISPLAY_NAME")) ? null : reader.GetString(reader.GetOrdinal("DISPLAY_NAME"));
                            amv.ColumnType = reader.IsDBNull(reader.GetOrdinal("ATTR_TYPE")) ? null : reader.GetString(reader.GetOrdinal("ATTR_TYPE"));
                            amv.Order = reader.IsDBNull(reader.GetOrdinal("ATTR_ORDER")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("ATTR_ORDER"));
                            amv.IsMain = reader.IsDBNull(reader.GetOrdinal("IS_MAIN")) ? false : API_Helper.IsMainAttribute(reader.GetString(reader.GetOrdinal("IS_MAIN")));

                            if (attributeModelView == null)
                                attributeModelView = new List<AttributeModelView>();

                            attributeModelView.Add(amv);
                        }
                    }
                }

                CloseConnection();

                return attributeModelView;
            }
        }


        public static List<ModuleModelView> GetAllModules()
        {

            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_MODULE M LEFT JOIN OSS_MODULEBAR MB ON M.ID = MB.MODULE_ID";

            List<ModuleModelView> modules = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME;

                    modules = new List<ModuleModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ModuleModelView md = new ModuleModelView();

                            md.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            md.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            md.RootName = reader.IsDBNull(reader.GetOrdinal("ROOT_NAME")) ? null : reader.GetString(reader.GetOrdinal("ROOT_NAME"));
                            md.SearchFlag = API_Helper.SearchFlagStatus(reader.GetInt16(reader.GetOrdinal("SEARCH_FLAG")));
                            md.ModuleBarId = (int)reader.GetInt64(reader.GetOrdinal("MODULE_ID"));
                            modules.Add(md);
                        }
                    }
                }

                CloseConnection();

                return modules;
            }
        }

        public static List<Category> GetModuleCategories(int moduleBarId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_CATEGORY";
            string CONDITION = "MODULEBAR_ID = " + moduleBarId;

            List<Category> categories = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    categories = new List<Category>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();

                            category.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            category.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            category.Order = (int)reader.GetInt64(reader.GetOrdinal("C_ORDER"));
                            category.IsOpen = API_Helper.CategoryOpeningStatus(reader.GetInt16(reader.GetOrdinal("IS_OPEN")));
                            category.Title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? null : reader.GetString(reader.GetOrdinal("TITLE"));
                            category.subCategories = null;

                            categories.Add(category);
                        }
                    }
                }

                CloseConnection();

                return categories;
            }
        }

        public static List<CategoryModelView> GetSidebarCategories(int moduleBarId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_CATEGORY";
            string CONDITION = "MODULEBAR_ID = " + moduleBarId;

            List<CategoryModelView> categories = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    categories = new List<CategoryModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryModelView category = new CategoryModelView();

                            category.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            category.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            category.Order = (int)reader.GetInt64(reader.GetOrdinal("C_ORDER"));
                            category.IsOpen = API_Helper.CategoryOpeningStatus(reader.GetInt16(reader.GetOrdinal("IS_OPEN")));
                            category.Title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? null : reader.GetString(reader.GetOrdinal("TITLE"));

                            categories.Add(category);
                        }
                    }
                }

                CloseConnection();

                return categories;
            }
        }

        public static List<TreeModelView> GetCategorySubCategories(int categoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_SUBCATEGORY";
            string CONDITION = "CATEGORY_ID = " + categoryId;

            List<TreeModelView> subCategories = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    subCategories = new List<TreeModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeModelView scmv = new TreeModelView();

                            scmv.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            scmv.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            scmv.Order = (int)reader.GetInt64(reader.GetOrdinal("SC_ORDER"));
                            scmv.Level = (int)reader.GetInt64(reader.GetOrdinal("SC_LEVEL"));
                            scmv.ParentId = reader.IsDBNull(reader.GetOrdinal("PARENT_ID")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("PARENT_ID"));

                            subCategories.Add(scmv);
                        }
                    }
                }

                CloseConnection();
     
                return subCategories ;
            }
        }

        public static List<TabModelView> GetTabs(int categoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_SUBCATEGORY";
            string CONDITION = "CATEGORY_ID = " + categoryId;

            List<TabModelView> tabs = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    tabs = new List<TabModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TabModelView tmv = new TabModelView();

                            tmv.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            tmv.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            tmv.Order = (int)reader.GetInt64(reader.GetOrdinal("SC_ORDER"));
                            tmv.Level = (int)reader.GetInt64(reader.GetOrdinal("SC_LEVEL"));
                            tmv.ParentId = reader.IsDBNull(reader.GetOrdinal("PARENT_ID")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("PARENT_ID"));
                            tmv.Icon = reader.IsDBNull(reader.GetOrdinal("ICON_NAME")) ? null : reader.GetString(reader.GetOrdinal("ICON_NAME"));
                            tmv.Route = reader.IsDBNull(reader.GetOrdinal("ROUTE_NAME")) ? null : reader.GetString(reader.GetOrdinal("ROUTE_NAME"));
                            tmv.Badge = reader.IsDBNull(reader.GetOrdinal("BADGE")) ? null : reader.GetString(reader.GetOrdinal("BADGE"));
                            tmv.Target = API_Helper.TabTarget(reader.GetInt16(reader.GetOrdinal("TARGET")));

                            tabs.Add(tmv);
                        }
                    }
                }

                CloseConnection();

                return tabs;
            }
        }

        public static SubCategoryTreeBasics GetSubCategoryRoot(int subCategoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_SUBCATEGORYTREEROOT";
            string CONDITION = "SUBCATEGORY_ID = " + subCategoryId;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                SubCategoryTreeBasics sctb = null;

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sctb = new SubCategoryTreeBasics();

                            sctb.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            sctb.SearchFlag = API_Helper.SearchFlagStatus(reader.GetInt16(reader.GetOrdinal("SEARCH_FLAG")));
                        }
                    }
                }

                CloseConnection();

                return sctb;
            }
        }

        public static List<TreeModelView> GetSubCategoryTrees(int subCategoryRootId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "OSS_SUBCATEGORYTREENODE";
            string CONDITION = "SUBCATEGORY_TREEROOT_ID = " + subCategoryRootId;

            List<TreeModelView> subCategoryTrees = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    subCategoryTrees = new List<TreeModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TreeModelView tmv = new TreeModelView();

                            tmv.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            tmv.Name = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            tmv.Order = (int)reader.GetInt64(reader.GetOrdinal("SCN_ORDER"));
                            tmv.Level = (int)reader.GetInt64(reader.GetOrdinal("SCN_LEVEL"));
                            tmv.ParentId = reader.IsDBNull(reader.GetOrdinal("PARENT_ID")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("PARENT_ID"));

                            subCategoryTrees.Add(tmv);
                        }
                    }
                }

                CloseConnection();

                return subCategoryTrees;
            }
        }

        public static List<GroupAttributeModelView> GetAttributesGroup(int CategoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_ATT_GROUP";
            string CONDITION = "CATEGORYID = " + CategoryId;

            List<GroupAttributeModelView> attributesGroups = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    attributesGroups = new List<GroupAttributeModelView>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GroupAttributeModelView attrGroup = new GroupAttributeModelView();

                            attrGroup.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            attrGroup.TableName = reader.IsDBNull(reader.GetOrdinal("TABLENAME")) ? null : reader.GetString(reader.GetOrdinal("TABLENAME"));
                            attrGroup.GroupName = reader.IsDBNull(reader.GetOrdinal("GROUPNAME")) ? null : reader.GetString(reader.GetOrdinal("GROUPNAME"));
                            attrGroup.ParentId = (int?)(reader.IsDBNull(reader.GetOrdinal("PARENTID")) ? null : (int?)reader.GetInt64(reader.GetOrdinal("PARENTID")));
                            attrGroup.Order = (int)reader.GetInt64(reader.GetOrdinal("ORDER_NUM"));

                            attrGroup.Attributes = GetAttributesByGroupId(attrGroup.Id);


                            attributesGroups.Add(attrGroup);
                        }
                    }
                }

                CloseConnection();

                return attributesGroups;
            }
        }

        public static List<Attributes.Attribute> GetAttributesByGroupId(int GroupeId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_ATTR2";
            string CONDITION = "GROUP_ID = " + GroupeId;

            List<Attributes.Attribute> attrs = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;             

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (attrs == null)
                                attrs = new List<Attributes.Attribute>();

                            Attributes.Attribute attr = new Attributes.Attribute();

                            attr.Id = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            attr.ColumnName = reader.IsDBNull(reader.GetOrdinal("COLUMNNAME")) ? null : reader.GetString(reader.GetOrdinal("COLUMNNAME"));
                            attr.ColumnType = reader.IsDBNull(reader.GetOrdinal("COLUMNTYPE")) ? null : reader.GetString(reader.GetOrdinal("COLUMNTYPE"));
                            attr.DisplayName = reader.IsDBNull(reader.GetOrdinal("DISPLAYNAME")) ? null : reader.GetString(reader.GetOrdinal("DISPLAYNAME"));
                            attr.Order = (int)reader.GetInt64(reader.GetOrdinal("ORDER_NUM"));

                            attrs.Add(attr);
                        }
                    }
                }

                CloseConnection();

                return attrs;
            }
        }

        public static List<LocationSiteModelView> GetSites()
        {
            List<LocationSiteModelView> records = null;
            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {

                    //cmd.InitialLONGFetchSize = 1000;
                    cmd.CommandText = "SELECT * from SITE";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (records == null)
                                records = new List<LocationSiteModelView>();

                            LocationSiteModelView obj = new LocationSiteModelView();
                            obj.Site = new Site();

                            obj.Region = reader.IsDBNull(reader.GetOrdinal("REGION")) ? null : reader.GetString(reader.GetOrdinal("REGION"));
                            obj.Area = reader.IsDBNull(reader.GetOrdinal("AREA")) ? null : reader.GetString(reader.GetOrdinal("AREA"));
                            obj.Zone = reader.IsDBNull(reader.GetOrdinal("ZONE")) ? null : reader.GetString(reader.GetOrdinal("ZONE"));
                            obj.SubArea = reader.IsDBNull(reader.GetOrdinal("SUBAREA")) ? null : reader.GetString(reader.GetOrdinal("SUBAREA"));

                            obj.Site.ID = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            obj.Site.ENGLISH_NAME = reader.IsDBNull(reader.GetOrdinal("ENGLISH_NAME")) ? null : reader.GetString(reader.GetOrdinal("ENGLISH_NAME"));
                            obj.Site.CODE = reader.IsDBNull(reader.GetOrdinal("CODE")) ? null : reader.GetString(reader.GetOrdinal("CODE"));
                            
                            records.Add(obj);
                        }

                    }
                    
                }

                CloseConnection();
            }
            return records;
        }

        public static List<NeTypeModelView> GetNeTables(int Category_Id)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_TYPES";
            string CONDITION = "CATEGORY_ID = " + Category_Id;

            List<NeTypeModelView> tables = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (tables == null)
                                tables = new List<NeTypeModelView>();

                            NeTypeModelView table = new NeTypeModelView();

                            table.TableName = reader.IsDBNull(reader.GetOrdinal("TABLE_NAME")) ? null : reader.GetString(reader.GetOrdinal("TABLE_NAME"));
                            table.ColumnName = reader.IsDBNull(reader.GetOrdinal("COLUMN_NAME")) ? null : reader.GetString(reader.GetOrdinal("COLUMN_NAME"));                
                            table.DisplayName = reader.IsDBNull(reader.GetOrdinal("DISPLAY_NAME")) ? null : reader.GetString(reader.GetOrdinal("DISPLAY_NAME"));
                            table.Order = (int)reader.GetInt64(reader.GetOrdinal("ORDER_NUM"));

                            tables.Add(table);
                        }
                    }
                }

                CloseConnection();

                return tables;
            }
        }


        public static List<string> GetNeTypeValues(string tableName, string columnName)
        {
            string DATA_SELECTED = columnName;
            string TABLENAME = tableName;

            List<string> typeValues = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (typeValues == null)
                                typeValues = new List<string>();

                            string value = reader.IsDBNull(reader.GetOrdinal(columnName)) ? null : reader.GetString(reader.GetOrdinal(columnName));
                            typeValues.Add(value);
                        }
                    }
                }

                CloseConnection();

                return typeValues;
            }
        }

        public  static List<string> GetSubCategories(int CategoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_SUBCATEGORY";
            string CONDITION = "CATEGORY_ID = " + CategoryId;

            List<string> subCategories = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (subCategories == null)
                                subCategories = new List<string>();

                            string subCategory = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                            subCategories.Add(subCategory);
                        }
                    }
                }

                CloseConnection();

                return subCategories;
            }
        }

        public static string GetCategoryName(int CategoryId)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_CATEGORY";
            string CONDITION = "ID = " + CategoryId;

            string CategoryName = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryName = reader.IsDBNull(reader.GetOrdinal("NAME")) ? null : reader.GetString(reader.GetOrdinal("NAME"));
                        }
                    }
                }

                CloseConnection();

                return CategoryName;
            }
        }


        public static List<CategoryItem> GetDropDownListCategory(string ModelName)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_CATEGORY";
            string CONDITION = "MODULE_NAME = '" + ModelName + "'";

            List<CategoryItem> Cat_List = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Cat_List == null)
                            {
                                Cat_List = new List<CategoryItem>();
                            }

                            CategoryItem category = new CategoryItem();

                            category.ItemId = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                            category.ItemName = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name"));
                            
                            Cat_List.Add(category);
                        }
                    }
                }

                CloseConnection();
                

                return Cat_List;
            }
        }

        public static List<int> GetCategoryIds(string ModelName)
        {
            const string DATA_SELECTED = "*";
            const string TABLENAME = "RIM_CATEGORY";
            string CONDITION = "MODULE_NAME = '" + ModelName + "'";

            List<int> Cat_Ids = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;



                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Cat_Ids == null)
                            {
                                Cat_Ids = new List<int>();
                            }

                            int categoryId = (int)reader.GetInt64(reader.GetOrdinal("ID"));

                            Cat_Ids.Add(categoryId);
                        }
                    }
                }

                CloseConnection();


                return Cat_Ids;
            }
        }


        public static List<NeDetail> GetAllNeInstanceDetails(string TableName, string TableNameType, string TableNameSite, int id ,string Type ,string Category)
        {
            List<string> tables = new List<string>();
            tables.Add(TableName);
            tables.Add(TableNameType);
            if (TableNameSite!=null)
                tables.Add(TableNameSite);

            List<InstanceProperty> InstanceProperty = OracleHelper.GetInstanceProperties(tables);


            
            const string DATA_SELECTED = "*";
            //string TABLENAME = TableName;
            string CONDITION = "ID = '" + id + "'";

            List<NeDetail> Alldetails = null;

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();

                using (var cmd = oraConnection.CreateCommand())
                {
                    if (Type.Equals("NE"))
                    {
                        //if (Category.Equals("MW"))
                        //{
                        //    cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TableName + " r LEFT JOIN " + TableNameSite + " rr ON r.ID = rr.NE_ID AND LEFT JOIN rr.TYPE = 'S' JOIN " + TableNameType + " rrr ON rrr.ID = r.TYPE_ID " + " WHERE r." + CONDITION;
                        //                                                                                                                                                                    //join Mw_Ne_Site smns
                        //                                                                                                                                                                    //    on smns.NE_id = mn.id and smns.type = 'S'
                        //                                                                                                                                                                    //  join site ss
                        //                                                                                                                                                                    //  on ss.id = smns.site_id
                        //                                                                                                                                                                    //join Mw_Ne_Site dmns
                        //                                                                                                                                                                    //    on dmns.NE_id = mn.id and dmns.type = 'D'
                        //                                                                                                                                                                    //  join site dsite
                        //                                                                                                                                                                    //    on dmns.site_id = dsite.id
                        //}
                        //else
                        //{
                            cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TableName + " r LEFT JOIN " + TableNameSite + " rr ON r.ID = rr.NE_ID LEFT JOIN " + TableNameType + " rrr ON rrr.ID = r.TYPE_ID " + " WHERE r." + CONDITION;

                            //SELECT * FROM DATACOM_NE r left join DATACOM_NE_SITE rr on r.ID = rr.NE_ID left join DATACOM_NE_TYPE rrr on rrr.id = r.type_id
                            //            WHERE r.ID = '2'

                       // }
                   
                    }
                    else
                    {
                        cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TableName + " n LEFT JOIN " + TableNameType + " nn ON n.TYPE_ID = nn.ID " + " WHERE n." + CONDITION;
                        //SELECT * FROM DATACOM_BOARD n left join DATACOM_BOARD_TYPE nn on n.type_id = nn.id
                        //    WHERE n.ID = '2'
                    }
                    //cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;

                    //int i = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Alldetails == null)
                            {
                                Alldetails = new List<NeDetail>();
                            }

                            foreach(InstanceProperty Property in InstanceProperty){
                                NeDetail detail = new NeDetail();
                                detail.ColumnName = Property.ColumnName;
                                if (Property.DataType.Equals("VARCHAR2"))
                                {
                                    detail.Value = (reader.IsDBNull(reader.GetOrdinal(detail.ColumnName)) ? null : reader.GetString(reader.GetOrdinal(detail.ColumnName)).ToString());
                                }
                                else if (Property.DataType.Equals("NUMBER"))
                                {
                                    detail.Value = (reader.IsDBNull(reader.GetOrdinal(detail.ColumnName)) ? null : reader.GetOracleValue(reader.GetOrdinal(detail.ColumnName)).ToString());
                                }
                                else if (Property.DataType.Equals("TIMESTAMP(6)"))
                                {
                                    detail.Value = (reader.IsDBNull(reader.GetOrdinal(detail.ColumnName)) ? null : (reader.GetOracleValue(reader.GetOrdinal(detail.ColumnName))).ToString());
                                }
                                else
                                {
                                    detail.Value = (reader.IsDBNull(reader.GetOrdinal(detail.ColumnName)) ? null : (reader.GetOracleValue(reader.GetOrdinal(detail.ColumnName)))).ToString();
                                }
                                // detail.Value = reader.IsDBNull(reader.GetOrdinal(detail.ColumnName)) ? null : reader.GetString(reader.GetOrdinal(detail.ColumnName));
                                Alldetails.Add(detail);
                            }
                            //int categoryId = (int)reader.GetInt64(reader.GetOrdinal("ID"));
                           
                            //i++;
                        }
                    }
                   
                }

                CloseConnection();


                return Alldetails;
            }

           
        }

        public static List<InstanceProperty> GetInstanceProperties(List<string> tables)
        {
            
            List<InstanceProperty> AllProperties = null;//from here edite

            using (oraConnection = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OSS_RIM"].ConnectionString))
            {
                OpenConnection();
                foreach (string table in tables)
                {
                    const string DATA_SELECTED = "COLUMN_NAME, DATA_TYPE";
                    const string TABLENAME = "USER_TAB_COLS";
                    string CONDITION = "TABLE_NAME = '" + table + "'";

                    using (var cmd = oraConnection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT " + DATA_SELECTED + " FROM " + TABLENAME + " WHERE " + CONDITION;



                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (AllProperties == null)
                                {
                                    AllProperties = new List<InstanceProperty>();
                                }
                                string test = reader.GetValue(reader.GetOrdinal("COLUMN_NAME")).ToString();
                                if (test  != "ID" && test != "TYPE_ID" && test != "NE_ID")
                                {
                                    InstanceProperty Property = new InstanceProperty();
                                    Property.ColumnName = reader.IsDBNull(reader.GetOrdinal("COLUMN_NAME")) ? null : reader.GetString(reader.GetOrdinal("COLUMN_NAME"));
                                    Property.DataType = reader.IsDBNull(reader.GetOrdinal("DATA_TYPE")) ? null : reader.GetString(reader.GetOrdinal("DATA_TYPE"));
                                    AllProperties.Add(Property);
                                }
                               
                                
                            }
                        }

                    }
                }

              

                CloseConnection();


                return AllProperties;
            }
        }
    }
}