using Syriatel.RadioOSS.API.Data;
using Syriatel.RadioOSS.API.Models.Helper;
using Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.CategoryTypesCount
{
    public class CategoryTypesCount
    {

        public TypeReturnForm Result { get; set; }
        private DataLookup data { get; set; }
        public CallingAPIHelper helper { get; set; }

        public CategoryTypesCount(CountTypePostObject Obj)
        {
            this.data = new DataLookup();
            this.helper = new CallingAPIHelper();
            this.Result = this.GetTypesCountData(Obj);
            
        }
        private TypeReturnForm GetTypesCountData(CountTypePostObject Obj) //TypeReturnForm
        {
            
            
            //String Category = Obj.CategoryName.ToUpper();

            DB_Source BaseClass=this.getBaseClassName(Obj.dbsource, Obj.LoctionValues);
            String whereString = this.getLocationInfo(Obj.dbsource, Obj.LoctionValues);

            DynamicApiInfo info = this.getDynamicAPIInfo(Obj);
            //call ahmad api (info) and it return (JoinString) + (where mini)
            //example

            DynamicJoinReturnedData api_result = this.helper.PostBasicAsync(info);

            String JoinString = api_result.JoinClause;
            String miniWhereString = api_result.WhereClause;


            /*"Ran_Controller inner join Ran_Netype on Ran_Netype.id=Ran_Controller.ran_netype_id " +
                                "inner join ran_controller_site_identity on ran_controller_site_identity.ran_controller_id = Ran_Controller.id " +
                                "inner join site_identity on site_identity.id = ran_controller_site_identity.site_identity_id " +
                                "inner join site_candidate on site_candidate.id = site_identity.site_candidate_id " +
                                "inner join subarea on subarea.id = site_candidate.subarea_id " +
                                "inner join zone on zone.id = subarea.zone_id " +
                                "inner join area on area.id = zone.area_id " +
                                 "inner join region on region.id = area.region_id ";*/


            String Select = "Select distinct " + BaseClass.TableName + "." + BaseClass.ColumnName + " as LOCATION " + ", " + Obj.Level.TableName + "." + Obj.Level.ColumnName + " as TYPE " + ", " + "count(" + Obj.Level.TableName + "." + Obj.Level.ColumnName + ")" + " as COUNT ";
            String From = "from " + JoinString;
            String Where = " where " + miniWhereString + " and " + whereString + " ";
            String GroupBy = "group by(" + BaseClass.TableName + "." + BaseClass.ColumnName + ", " + Obj.Level.TableName + "." + Obj.Level.ColumnName + ")";

            return this.ChangeDataForm(this.data.getTypesCounts(Select, From, Where, GroupBy));
            //return this.data.getTypesCounts(Select, From, Where, GroupBy);
        }
        private TypeReturnForm ChangeDataForm(TypesReturnResult data)
        {
            TypeReturnForm result = null;
            if (data != null)
            {
                result = new TypeReturnForm();
                result.LocationNames = data.LocationNames;
                if(data.TotalLocationTypes != null)
                {
                    foreach(TypeCount elem in data.TotalLocationTypes.First().LocTypesList)
                    {
                        if(result.Types == null)
                        {
                            result.Types = new List<string>();
                        }
                        result.Types.Add(elem.Type);
                        
                    }
                    int Typelength = result.Types.Count;
                    int j = 0;
                    
                    while(j< Typelength)
                    {
                        Counts count = null;
                        foreach (TotalTypeCounts elem in data.TotalLocationTypes)
                        {
                            if (count == null)
                            {
                                count = new Counts();
                                count.Values = new List<int>();
                            }

                         
                                count.Values.Add(elem.LocTypesList[j].Count);
                           
                        }
                        if (result.LocContValues == null)
                        {
                            result.LocContValues = new List<Counts>();
                        }
                        result.LocContValues.Add(count);
                        j++;
                    }

                    
                }
            }
            return result;
        }
        private DynamicApiInfo getDynamicAPIInfo(CountTypePostObject obj)
        {

            DynamicApiInfo info = new DynamicApiInfo();
            info.CategoryName = obj.CategoryName.ToUpper();
            info.ModelName = obj.Model.ToUpper();
            info.JoinType = Constants.InnerJoin;
            info.EfectedTabels = new List<string>();
            info.EfectedTabels = this.getEfectedTabels(obj);
            return info;
        }
        private List<String> getEfectedTabels(CountTypePostObject obj)
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

            effectedTabels.Add(obj.Level.TableName.ToUpper());

            return effectedTabels;
        }
        private String getLocationInfo(LocationSite_DB_Source dbsource, LocationFilterValues FilterVals)
        {
            String where = " ";
            if(FilterVals.Region != null)
            {
                where = where + dbsource.RegionSource.TableName + "." + dbsource.RegionSource.ColumnName + " = " +"'"+ FilterVals.Region+"'";

                if(FilterVals.Area != null)
                {
                    where = where + " and " + dbsource.AreaSource.TableName + "." + dbsource.AreaSource.ColumnName + " = " + "'" + FilterVals.Area+ "'" ;
                    if(FilterVals.Zone != null)
                    {
                        where = where + " and " + dbsource.ZoneSource.TableName + "." + dbsource.ZoneSource.ColumnName + " = " + "'" + FilterVals.Zone+"'" ;
                        if(FilterVals.SubArea != null)
                        {
                            where = where + " and " + dbsource.SubAreaSource.TableName + "." + dbsource.SubAreaSource.ColumnName + " = " + "'" + FilterVals.SubArea+ "'" ;
                        }
                    }
                }
            }
            return where;
        }

        private DB_Source getBaseClassName(LocationSite_DB_Source dbsource, LocationFilterValues FilterVals)
        {
            if (FilterVals.Region == null) //All Syria
            {
                return dbsource.RegionSource;
            }
            else
            {
                if(FilterVals.Area == null)
                {
                    return dbsource.AreaSource;
                }
                else
                {
                    if(FilterVals.Zone == null)
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
    }
}