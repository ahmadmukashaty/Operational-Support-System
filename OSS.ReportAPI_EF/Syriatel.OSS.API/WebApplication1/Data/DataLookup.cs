
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Syriatel.OSS.API.Models;
using Syriatel.OSS.API.Models.Trees.SubCategoryTree;
using Syriatel.OSS.API.Models.DropDownCategory;
using Syriatel.OSS.API.Models.Site;
using Syriatel.OSS.API.Models1;
using Syriatel.OSS.API.Models.Trees.CategoryRanTypeTree;
using Syriatel.OSS.API.Models.Trees.AttributesRanTree.ModelViews;
using Syriatel.OSS.API.Models.DynamicReport;
using System.Data;
using Syriatel.OSS.API.DbLayer;
using Syriatel.OSS.API.Models.MAP;
using Syriatel.OSS.API.Models.TreeDetails;

namespace Syriatel.OSS.API.Data
{
    public class DataLookup
    {
        Entities5 _context;
        public DataLookup()
        {
            _context = new Entities5();
        }

        public int GetClassificationID(string classificationName)
        {
            return (int) _context.RIM_CLASSIFICATION
                .Where(a => a.NAME.ToLower() == classificationName.ToLower())
                .Select(c => c.ID)
                .FirstOrDefault();
        }

        public int GetModuleID(string moduleName)
        {
            return (int)_context.RIM_MODULES
                .Where(a => a.NAME.ToLower() == moduleName.ToLower())
                .Select(c => c.ID)
                .FirstOrDefault();
        }
        
        //Category Dropdown API
        public List<CategoryItem> GetDropDownListCategory(int moduleId)
        {
            var categoryModelView = _context.RIM_CATEGORY
                .Where(a => a.MODULE_ID == moduleId)
                .Select(c => new CategoryItem()
                {
                    Id = c.ID,
                    Name = c.NAME,
                    Order = c.CAT_ORDER
                })
                .ToList();

            if (categoryModelView.Count == 0)
                return null;

            return categoryModelView;
        }


        //Get Site Filter API
        public List<LocationSiteModelView> GetSites()
        {
            var sites = (from re in _context.REGIONs
                                      join ar in _context.AREAs on re.ID equals ar.REGION_ID
                                      join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                                      join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                                      join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                                      select new LocationSiteModelView()
                                      {
                                          Region = re.NAME,
                                          Area = ar.NAME,
                                          Zone = zo.NAME,
                                          SubArea = su.NAME,
                                          Site = new SiteData()
                                          {
                                              ID = sc.ID,
                                              CODE = sc.SITE_CODE,
                                              ENGLISH_NAME = sc.ENGLISH_NAME
                                          }
                                      }).ToList();

            if (sites.Count == 0)
                return null;

            return sites;
        }


        //SubCategory Tree APIs

        public List<ModuleModelView> GetClassificationModules(string classificationName)
        {
            var data = (from rc in _context.RIM_CLASSIFICATION
                        join rcc in _context.RIM_CLASSIFICATION_CATEGORY on rc.ID equals rcc.RIM_CLASSIFICATION_ID
                        join rca in _context.RIM_CATEGORY on rcc.RIM_CATEGORY_ID equals rca.ID
                        join rm in _context.RIM_MODULES on rca.MODULE_ID equals rm.ID
                        where rc.NAME.ToLower() == classificationName.ToLower()
                        select new ModuleModelView()
                        {
                            ID = rm.ID,
                            Name = rm.NAME
                        }).Distinct().ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<CategoryModelView> GetClassificationCategories(string classificationName)
        {
            var data = (from rc in _context.RIM_CLASSIFICATION
                        join rcc in _context.RIM_CLASSIFICATION_CATEGORY on rc.ID equals rcc.RIM_CLASSIFICATION_ID
                        join rca in _context.RIM_CATEGORY on rcc.RIM_CATEGORY_ID equals rca.ID
                        join rm in _context.RIM_MODULES on rca.MODULE_ID equals rm.ID
                        where rc.NAME.ToLower() == classificationName.ToLower()
                        select new CategoryModelView()
                        {
                            ID = rca.ID,
                            Name = rca.NAME,
                            ModuleID = rca.MODULE_ID,
                            Order = rca.CAT_ORDER
                        }).Distinct().ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<SubCategoryModelView> GetClassificationSubCategories(string classificationName)
        {
            var data = (from rc in _context.RIM_CLASSIFICATION
                        join rcc in _context.RIM_CLASSIFICATION_CATEGORY on rc.ID equals rcc.RIM_CLASSIFICATION_ID
                        join rca in _context.RIM_CATEGORY on rcc.RIM_CATEGORY_ID equals rca.ID
                        join rm in _context.RIM_MODULES on rca.MODULE_ID equals rm.ID
                        join rs in _context.RIM_SUBCATEGORY on rca.ID equals rs.CATEGORY_ID
                        where rc.NAME.ToLower() == classificationName.ToLower()
                        select new SubCategoryModelView()
                        {
                            ID = rs.ID,
                            Name = rs.NAME,
                            CategoryID = rs.CATEGORY_ID,
                            Order = rs.SUBCAT_ORDER
                        }).Distinct().ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        
        //Get Type Tree APIs
        public List<IsMainModelView> GetClassificationIsMainData(int classificationId, string rim_type)
        {
            var Types = (from rc in _context.RIM_CLASSIFICATION
                           join rcl in _context.RIM_CLASSIFICATION_TABLE on rc.ID equals rcl.RIM_CLASSIFICATION_ID
                           join rl in _context.RIM_TABLE on rcl.RIM_TABLE_ID equals rl.ID
                           join rclt in _context.RIM_CLASSIFICATION_TABLE_TYPE on  rcl.ID equals rclt.RIM_CLASSIFICATION_TABLE_ID
                         join rt in _context.RIM_TYPE on rclt.RIM_TYPE_ID equals rt.ID
                           join rism in _context.RIM_IS_MAIN on rclt.ID equals rism.RIM_CLASSIFICATION_TABLE_TYPE_ID
                         join ra in _context.RIM_ATTRIBUTE on rism.RIM_ATTRIBUTE_ID equals ra.ID
                           where rc.ID == classificationId
                           where rt.NAME == rim_type
                           select new IsMainModelView()
                           {
                               Name = rclt.DISPLAY_NAME,
                               TableName = rl.TABLE_NAME,
                               ColumnName = ra.NAME,
                               ColumnType = ra.ATTR_TYPE,
                               Order = rclt.LEVEL_ORDER                             
                           }).Distinct().OrderBy(c => c.Order).ToList();

            if (Types.Count == 0)
                return null;

            return Types;
        }

        public List<string> GetTableColumnData(string tableName, string columnName)
        {
            var typeData = _context.Database
                            .SqlQuery<string>(string.Format("SELECT {1} FROM {0}", tableName, columnName))
                            .ToList();

            if (typeData.Count == 0)
                return null;

            return typeData;
        }



        //Get Attribute Tree APIs
        public List<LevelModelView> GetClassificationLevels(int ClassificationId)
        {
            var levels = (from rc in _context.RIM_CLASSIFICATION
                          join rcl in _context.RIM_CLASSIFICATION_TABLE on rc.ID equals rcl.RIM_CLASSIFICATION_ID
                          join rl in _context.RIM_TABLE on rcl.RIM_TABLE_ID equals rl.ID
                          where rcl.RIM_CLASSIFICATION_ID == ClassificationId
                          select new LevelModelView()
                          {
                              Id = rcl.ID,
                              Name = rcl.DISPLAY_NAME,
                              Order = rcl.LEVEL_ORDER,
                              TableName = rl.TABLE_NAME,
                              ParentId = rcl.PARENT_ID,
                              LevelId = rl.ID
                          }).ToList();

            if (levels.Count == 0)
                return null;

            return levels;
        }

        public List<AttributeModelView> GetClassificationLevelAttributes(int LevelId)
        {
            var attributeModelView = _context.RIM_ATTRIBUTE
                .Where(a => a.RIM_TABLE_ID == LevelId)
                .Select(c => new AttributeModelView()
                {
                    ColumnName = c.NAME,
                    DisplayName = c.DISPLAY_NAME,
                    ColumnType = c.ATTR_TYPE,
                    Order = c.ATTR_ORDER
                })
                .ToList();

            if (attributeModelView.Count == 0)
                return null;

            return attributeModelView;
        }


        //Adhoc Report
        public List<ReportLevelsModelView> GetCategoryLevelsAllData(int classificationId)
        {
            var levels = (from rl in _context.RIM_TABLE
                          join rcl in _context.RIM_CLASSIFICATION_TABLE on rl.ID equals rcl.RIM_TABLE_ID
                          join rrt in _context.RIM_RELATION_TYPE on rcl.RIM_RELATION_TYPE_ID equals rrt.ID
                          where rcl.RIM_CLASSIFICATION_ID == classificationId
                          select new ReportLevelsModelView()
                          {
                              Id = rcl.ID,
                              TableName = rl.TABLE_NAME,
                              Order = rcl.LEVEL_ORDER,
                              ParentId = rcl.PARENT_ID,
                              JunctionTable = rcl.JUNCTION_TABLE,
                              RelationName = rrt.NAME,
                              LevelId = rl.ID

                          }).ToList();

            if (levels.Count == 0)
                return null;

            return levels;
        }

        public bool ColumnExistInTable(string tableName, string columnName)
        {
            var typeData = _context.Database
                .SqlQuery<string>(string.Format("Select COLUMN_NAME FROM user_tab_cols where table_name = '" + tableName + "'"))
                .ToList();

            if (typeData.Count == 0)
                return false;

            foreach(string value in typeData)
            {
                if (value == columnName)
                    return true;
            }

            return false;
        }

        public AdhocReportReturnedData ExecuteAdhocReportQuery(string query, List<SelectClause> selectClauses)
        {
            Helper _helper = new Helper();

            var queryResult =  _helper.ExcuteQuery(query).Serialize();

            if(queryResult != null)
            {
                AdhocReportReturnedData reportData = new AdhocReportReturnedData();
                reportData.value_array = queryResult;
                reportData.H_List = new List<ReportHeader>();
                Dictionary<string, bool> RepeatedKeys = new Dictionary<string, bool>();
                foreach(SelectClause select in selectClauses)
                {
                    if (RepeatedKeys.ContainsKey(select.Name))
                    {
                        RepeatedKeys[select.Name] = true;
                    }
                    else
                    {
                        RepeatedKeys.Add(select.Name, false);
                    }
                }
                foreach(SelectClause select in selectClauses)
                {
                    if (RepeatedKeys[select.Name])
                    {
                        string CombinedName = select.TableName + ":" + select.Name;
                        ReportHeader reportHeader = new ReportHeader(CombinedName, select.ColumnName, select.ColumnType);
                        reportData.H_List.Add(reportHeader);
                    }
                    else
                    {
                        ReportHeader reportHeader = new ReportHeader(select.Name, select.ColumnName, select.ColumnType);
                        reportData.H_List.Add(reportHeader);
                    }            
                }

                return reportData;
            }

            return null;
        }


        // Site APIs
        public List<MapInfo> GetMapInfo()
        {
            var attributeModelView = _context.SITE_CANDIDATE
                .Where(c => c.IS_ACTIVE == 1)
                .Select(c => new MapInfo()
                {
                    Id = c.ID,
                    siteCode = c.SITE_CODE,
                    EnglishName = c.ENGLISH_NAME,
                    LATITUDE_N = c.LATITUDE_N,
                    LONGITUDE_E = c.LONGITUDE_E
                })
                .ToList();

            if (attributeModelView.Count == 0)
                return null;

            return attributeModelView;
        }

        public List<MapInfo> GetSitesLocationByRegion(string RegionName)
        {
            var sites = (from re in _context.REGIONs
                         join ar in _context.AREAs on re.ID equals ar.REGION_ID
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where re.NAME == RegionName
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public List<MapInfo> GetSitesLocationByArea(string AreaName)
        {
            var sites = (from ar in _context.AREAs
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where ar.NAME == AreaName
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public List<MapInfo> GetSitesLocationByZone(string AreaName, string ZoneName)
        {
            var sites = (from ar in _context.AREAs
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where ar.NAME == AreaName
                         where zo.NAME == ZoneName
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public List<MapInfo> GetSitesLocationBySubArea(string AreaName, string ZoneName, string SubArea)
        {
            var sites = (from ar in _context.AREAs
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where ar.NAME == AreaName
                         where zo.NAME == ZoneName
                         where su.NAME == SubArea
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public List<MapInfo> GetSitesLocationBySiteCode(string SiteCode)
        {
            var sites = (from ar in _context.AREAs
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where sc.SITE_CODE == SiteCode
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public List<MapInfo> GetSitesLocationBySiteName(string AreaName, string ZoneName, string SubArea, string SiteName)
        {
            var sites = (from ar in _context.AREAs
                         join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                         join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                         join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                         where ar.NAME == AreaName
                         where zo.NAME == ZoneName
                         where su.NAME == SubArea
                         where sc.ENGLISH_NAME == SiteName
                         where sc.IS_ACTIVE == 1
                         select new MapInfo()
                         {
                             Id = sc.ID,
                             siteCode = sc.SITE_CODE,
                             EnglishName = sc.ENGLISH_NAME,
                             LATITUDE_N = sc.LATITUDE_N,
                             LONGITUDE_E = sc.LONGITUDE_E
                         }).ToList();

            if (sites.Count == 0)
                return null;
            return sites;
        }

        public string GetSiteNameBySiteCode(string SiteCode)
        {
            var siteName = (from ar in _context.AREAs
                            join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                            join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                            join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                            where sc.SITE_CODE == SiteCode
                            where sc.IS_ACTIVE == 1
                            select sc.ENGLISH_NAME).SingleOrDefault();
                         

            if (siteName.Equals(""))
                return null;
            return siteName.ToString();
        }

        public string GetSiteCodeBySiteName(string AreaName, string ZoneName, string SubArea, string SiteName)
        {
            var SiteCode = "";
            if (AreaName != null && ZoneName != null && SubArea != null)
            { 
             SiteCode = (from ar in _context.AREAs
                            join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                            join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                            join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                            where ar.NAME == AreaName
                            where zo.NAME == ZoneName
                            where su.NAME == SubArea
                            where sc.ENGLISH_NAME == SiteName
                            where sc.IS_ACTIVE == 1
                            select sc.SITE_CODE).SingleOrDefault();
            }
            else if (AreaName != null && ZoneName != null)
            {
                SiteCode = (from ar in _context.AREAs
                            join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                            join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                            join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                            where ar.NAME == AreaName
                            where zo.NAME == ZoneName
                            where sc.ENGLISH_NAME == SiteName
                            where sc.IS_ACTIVE == 1
                            select sc.SITE_CODE).SingleOrDefault();
            }
            else if (AreaName != null)
            {
                SiteCode = (from ar in _context.AREAs
                            join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                            join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                            join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                            where ar.NAME == AreaName
                            where sc.ENGLISH_NAME == SiteName
                            where sc.IS_ACTIVE == 1
                            select sc.SITE_CODE).SingleOrDefault();
           }
            else 
            {
                SiteCode = (from ar in _context.AREAs
                            join zo in _context.ZONEs on ar.ID equals zo.AREA_ID
                            join su in _context.SUBAREAs on zo.ID equals su.ZONE_ID
                            join sc in _context.SITE_CANDIDATE on su.ID equals sc.SUBAREA_ID
                            where sc.ENGLISH_NAME == SiteName
                            where sc.IS_ACTIVE == 1
                            select sc.SITE_CODE).SingleOrDefault();
            }   
            if (SiteCode.Equals(""))
                return null;
            return SiteCode.ToString();
        }


        //Tree Details APIs
        public List<NodeDataCategory> GetAllCategories(int moduleId)
        {
            var data = _context.RIM_CATEGORY
                .Where(a => (a.MODULE_ID == moduleId))
                .Select(c => new NodeDataCategory()
                {
                    Id = c.ID,
                    Name = c.NAME
                })
                .ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataSubCategory> GetAllSubCategories(int categoryId)
        {
            var data = _context.RIM_SUBCATEGORY
                .Where(c => c.CATEGORY_ID == categoryId)
                .Select(c => new NodeDataSubCategory()
                {
                    Id = c.ID,
                    Name = c.NAME,
                    Category_Id = (int)c.CATEGORY_ID
                })
                .ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataNE> GetAllDatacomNEs()
        {
            var data = (from rc in _context.DATACOM_NE
                        select new NodeDataNE()
                        {
                            Id = rc.ID,
                            Name = rc.NAME,
                            SubCategory_Id = (int?)rc.RIM_SUBCATEGORY_ID
                        }).ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataNE> GetAllOpticalNEs()
        {
            var data = (from rc in _context.OPTICAL_NE
                        select new NodeDataNE()
                        {
                            Id = rc.ID,
                            Name = rc.NAME,
                            SubCategory_Id = (int)rc.RIM_SUBCATEGORY_ID
                        }).ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataNE> GetAllMwNEs()
        {
            var data = (from rc in _context.MW_NE
                        select new NodeDataNE()
                        {
                            Id = rc.ID,
                            Name = rc.NAME,
                            SubCategory_Id = (int)rc.RIM_SUBCATEGORY_ID
                        }).ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataNE> GetAllFirewallNEs()
        {
            var data = (from rc in _context.FIREWALL_NE
                        select new NodeDataNE()
                        {
                            Id = rc.ID,
                            Name = rc.NAME,
                            SubCategory_Id = (int)rc.RIM_SUBCATEGORY_ID
                        }).ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataSite> GetAllSites()
        {
            var data = _context.SITE_CANDIDATE
                .Where(c => c.IS_ACTIVE == 1)
                .Select(c => new NodeDataSite()
                {
                    Id = c.ID,
                    Name = c.SITE_CODE
                })
                .ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataController> GetAllControllers()
        {
            var data = (from rc in _context.RAN_CONTROLLER
                        join rn in _context.RAN_NETYPE on rc.RAN_NETYPE_ID equals rn.ID
                         select new NodeDataController()
                         {
                             Id = rc.ID,
                             Name = rc.NE_NAME,
                             SubCategory_Id = (int)rn.RIM_SUBCATEGORY_ID
                         }).ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        public List<NodeDataAbstract> GetAllAbstractData()
        {
            var data = _context.RIM_LEVELS
                    .Select(c => new NodeDataAbstract()
                    {
                        Id = c.ID,
                        Name = c.NAME,
                        Order = c.LEVEL_ORDER,
                        Parent_Id = c.PARENT_ID,
                        Category_Id = (int)c.CATEGORY_ID,
                        Type = c.LEVEL_TYPE
                    })
                    .ToList();

            if (data.Count == 0)
                return null;

            return data;
        }

        //Union Report
        public int GetClassificationIsUnion(string classificationName)
        {
            return (int)_context.RIM_CLASSIFICATION
                .Where(a => a.NAME.ToLower() == classificationName.ToLower())
                .Select(c => c.IS_UNION)
                .FirstOrDefault();
        }




    }
}