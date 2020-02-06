using Syriatel.RadioOSS.API.Data;
using Syriatel.RadioOSS.API.Models.CategoryTypesCount;
using Syriatel.RadioOSS.API.Models.Helper;
using Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Syriatel.RadioOSS.API.Models.Buysness
{
    public class Busyness
    {
        private DataLookup data { get; set; }
        public BusynessReturn result { get; set; }
        public CallingAPIHelper helper { get; set; }
        public Busyness(BusynessPostObject Obj)
        {
            this.data = new DataLookup();
            
            this.helper = new CallingAPIHelper();
            this.result =  getLevelLocationBusyness(Obj);
        }

        private BusynessReturn getLevelLocationBusyness(BusynessPostObject obj)
        {
            DynamicApiInfo info = this.getDynamicAPIInfo(obj);
            //call Ahmad API parameter(info); and the return value will be 2 string
           

            DynamicJoinReturnedData api_result= this.helper.PostBasicAsync(info);

            String miniWhereString = api_result.WhereClause;

            /*"rancontroller_rack.retire_date is null and " +
                                    "ran_rack_subrack.retire_date is null and " +
                                    "ran_controller_site_identity.retire_date is null and " +
                                    "ran_slot_board.retire_date is null ";*/
            String JoinString = api_result.JoinClause;
                
                /*"ran_controller left join rancontroller_rack on ran_controller.id= Rancontroller_rack.Ran_Controller_Id " +
                                    "left join ran_rack on ran_rack.id = Rancontroller_Rack.Ran_Rack_Id " +
                                    "left join ran_rack_subrack on ran_rack.id = ran_rack_subrack.ran_rack_id " +
                                    "left join ran_subrack on ran_subrack.id = ran_rack_subrack.ran_subrack_id " +
                                    "left join ran_slot on ran_slot.ran_subrack_id = ran_subrack.id " +
                                    "left join ran_slot_board on ran_slot_board.ran_slot_id = ran_slot.id " +
                                    "left join ran_board on ran_board.id = ran_slot_board.ran_board_id " +
                                    "left join ran_controller_site_identity on ran_controller.id = ran_controller_site_identity.ran_controller_id " +
                                    "left join site_identity on site_identity.id = ran_controller_site_identity.site_identity_id " +
                                    "left join site_candidate on site_candidate.id = site_identity.site_candidate_id " +
                                    "left join subarea on site_candidate.subarea_id = subarea.id " +
                                    "left join zone on zone.id = subarea.zone_id " +
                                    "left join area on area.id = zone.area_id " +
                                    "left join region on region.id = area.region_id ";*/
            String where = this.getLocationInfoWhereString(obj);
            DB_Source BaseLoc = this.getBaseClassName(obj);
            String Where = null;
            if (where == null)
            {
                Where = " where " + miniWhereString;
            }
            else
            {
                Where = " where " + miniWhereString + " and " + where + " ";
            }
            
            String SelectSource = "select " + BaseLoc.TableName + "." + BaseLoc.ColumnName + " as LOCATION, " + "count(" + obj.LevelSource.TableName + "." + obj.LevelSource.ColumnName + ") " + " as COUNT";
            String SelectDestination = "select " + BaseLoc.TableName + "." + BaseLoc.ColumnName + " as LOCATION, " + "count(" + obj.LevelDistination.TableName + "." + obj.LevelDistination.ColumnName + ") " + " as COUNT";
            String From = " From " + JoinString;
            String GroupBy = " group By(" + BaseLoc.TableName + "." + BaseLoc.ColumnName + ")";

            return this.data.GetBusynessData(SelectSource, SelectDestination, Where, From, GroupBy);

        }
        private DynamicApiInfo getDynamicAPIInfo(BusynessPostObject obj)
        {

            DynamicApiInfo info = new DynamicApiInfo();
            info.CategoryName = obj.CategoryName.ToUpper();
            info.ModelName = obj.Model.ToUpper();
            info.JoinType = Constants.InnerJoin;
            info.EfectedTabels = new List<string>();
            info.EfectedTabels = this.getEfectedTabels(obj);
            return info;
        }
        private List<String> getEfectedTabels(BusynessPostObject obj)
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

            effectedTabels.Add(obj.LevelSource.TableName.ToUpper());
            effectedTabels.Add(obj.LevelDistination.TableName.ToUpper());

            return effectedTabels;
        }
        private String getLocationInfoWhereString(BusynessPostObject obj)
        {
            string Where = null;
            if (obj.LoctionValues.Region != null)
            {
                Where = obj.dbsource.RegionSource.TableName + "." + obj.dbsource.RegionSource.ColumnName + " = " + "'" + obj.LoctionValues.Region + "' ";
                if (obj.LoctionValues.Area != null)
                {
                    Where = Where + " and " + obj.dbsource.AreaSource.TableName + "." + obj.dbsource.AreaSource.ColumnName + " = " + "'" + obj.LoctionValues.Area + "' ";
                    if (obj.LoctionValues.Zone != null)
                    {
                        Where = Where + " and " + obj.dbsource.ZoneSource.TableName + "." + obj.dbsource.ZoneSource.ColumnName + " = " + "'" + obj.LoctionValues.Zone + "' ";
                        if (obj.LoctionValues.SubArea != null)
                        {
                            Where = Where + " and " + obj.dbsource.SubAreaSource.TableName + "." + obj.dbsource.ZoneSource.ColumnName + " = " + "'" + obj.LoctionValues.SubArea + "' ";
                        }
                    }
                }
            }

            return Where;
        }
        private DB_Source getBaseClassName(BusynessPostObject obj)
        {
            if (obj.LoctionValues.Region == null) //All Syria
            {
                return obj.dbsource.RegionSource;
            }
            else
            {
                if (obj.LoctionValues.Area == null)
                {
                    return obj.dbsource.AreaSource;
                }
                else
                {
                    if (obj.LoctionValues.Zone == null)
                    {
                        return obj.dbsource.ZoneSource;
                    }
                    else
                    {
                        if (obj.LoctionValues.SubArea == null)
                        {
                            return obj.dbsource.SubAreaSource;
                        }
                        else
                            return null;
                    }
                }
            }
        }//end getBaseClassName
    }
}