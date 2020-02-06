using Syriatel.RadioOSS.API.Data;
using Syriatel.RadioOSS.API.Models.Helper;
using Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CellCount
{
    public class CellCount
    {
        private DataLookup data { get; set; }
        public CellCountReturn result { get; set; }
        public CallingAPIHelper helper { get; set; }

        public CellCount(CellCountPostObject Obj)
        {
            this.data = new DataLookup();
            this.helper = new CallingAPIHelper();
            this.result = this.GetCellCounts(Obj);
        }

        public CellCountReturn GetCellCounts(CellCountPostObject Obj)
        {
            DB_Source BaseClass = this.getBaseClassName(Obj.dbsource, Obj.LoctionValues);
            String where1 = this.getLocationInfo(Obj.dbsource, Obj.LoctionValues);
            DynamicApiInfo info = this.getDynamicAPIInfo(Obj);
            DynamicJoinReturnedData api_result = this.helper.PostBasicAsync(info);
            String JoinString = api_result.JoinClause;
            String where2 = api_result.WhereClause;

            String Select = "Select distinct " + BaseClass.TableName + "." + BaseClass.ColumnName + " as LOCATION " + ", " + Constants.BAND_Table + "." + Constants.BANS_Value + " as BAND " + ", " + "count(distinct " + Constants.CELL_Table + "." + Constants.CELL_ID + ")" + " as COUNT ";
            String From = "from " + JoinString;
            String Where = null;
            if (where1 != null)
            {
                Where = " where " + where1 + " and " + where2 + " ";
            }else
            {
                Where = " where " + where2;
            }
            
            String GroupBy = "group by(" + BaseClass.TableName + "." + BaseClass.ColumnName + ", " + Constants.BAND_Table + "." + Constants.BANS_Value + ")";

            return this.data.getCellCounts(Select, From, Where, GroupBy);
        }
        private DB_Source getBaseClassName(LocationSite_DB_Source dbsource, LocationFilterValues FilterVals)
        {
            if (FilterVals.Region == null) //All Syria
            {
                return dbsource.RegionSource;
            }
            else
            {
                if (FilterVals.Area == null)
                {
                    return dbsource.AreaSource;
                }
                else
                {
                    if (FilterVals.Zone == null)
                    {
                        return dbsource.ZoneSource;
                    }
                    else
                    {
                        if (FilterVals.SubArea == null)
                        {
                            return dbsource.SubAreaSource;
                        }
                        else
                            return null;
                    }
                }
            }
        }//end getBaseClassName
        private String getLocationInfo(LocationSite_DB_Source dbsource, LocationFilterValues FilterVals)
        {
            String where = null;
            if (FilterVals.Region != null)
            {
                where = where + dbsource.RegionSource.TableName + "." + dbsource.RegionSource.ColumnName + " = " + "'" + FilterVals.Region + "'";

                if (FilterVals.Area != null)
                {
                    where = where + " and " + dbsource.AreaSource.TableName + "." + dbsource.AreaSource.ColumnName + " = " + "'" + FilterVals.Area + "'";
                    if (FilterVals.Zone != null)
                    {
                        where = where + " and " + dbsource.ZoneSource.TableName + "." + dbsource.ZoneSource.ColumnName + " = " + "'" + FilterVals.Zone + "'";
                        if (FilterVals.SubArea != null)
                        {
                            where = where + " and " + dbsource.SubAreaSource.TableName + "." + dbsource.SubAreaSource.ColumnName + " = " + "'" + FilterVals.SubArea + "'";
                        }
                    }
                }
            }
            return where;
        }
        private DynamicApiInfo getDynamicAPIInfo(CellCountPostObject obj)
        {

            DynamicApiInfo info = new DynamicApiInfo();
            info.CategoryName = obj.CategoryName.ToUpper();
            info.ModelName = obj.Model.ToUpper();
            info.JoinType = Constants.InnerJoin;
            info.EfectedTabels = new List<string>();
            info.EfectedTabels = this.getEfectedTabels(obj);
            
            return info;
        }
        private List<String> getEfectedTabels(CellCountPostObject obj)
        {
            List<String> effectedTabels = new List<string>();
            if (obj.LoctionValues.Region != null)
            {
                effectedTabels.Add(obj.dbsource.RegionSource.TableName.ToUpper());
                if (obj.LoctionValues.Area != null)
                {
                    effectedTabels.Add(obj.dbsource.AreaSource.TableName.ToUpper());
                    if (obj.LoctionValues.Zone != null)
                    {
                        effectedTabels.Add(obj.dbsource.ZoneSource.TableName.ToUpper());
                        if (obj.LoctionValues.SubArea != null)
                            effectedTabels.Add(obj.dbsource.SubAreaSource.TableName.ToUpper());
                    }
                }
                
            }
            else
            {
                effectedTabels.Add(obj.dbsource.RegionSource.TableName.ToUpper());
            }

            effectedTabels.Add(Constants.CELL_Table);
            effectedTabels.Add(Constants.BAND_Table);

            return effectedTabels;
        }
    }
}