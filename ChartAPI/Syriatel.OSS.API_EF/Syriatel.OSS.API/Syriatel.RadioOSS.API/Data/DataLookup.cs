using Syriatel.RadioOSS.API.Models;
using Syriatel.RadioOSS.API.Models.Buysness;
using Syriatel.RadioOSS.API.Models.CategoryTypesCount;
using Syriatel.RadioOSS.API.Models.CellCount;
using Syriatel.RadioOSS.API.Models.ChartTypeLevels;
using Syriatel.RadioOSS.API.Models.Helper;
using Syriatel.RadioOSS.API.Models.Helper.BusynessHelperClass;
using Syriatel.RadioOSS.API.Models.Helper.LocationHelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Data
{
    public class DataLookup
    {
        Entities4 _context;
        public DataLookup()
        {
            _context = new Entities4();
        }
        public object getAreas()
        {
            //List<AREA> levels = _context.AREAs.ToList();

            var levels = (from n in _context.AREAs
                          select new { n.ARABIC_NAME, n.CODE, n.NAME }).ToList();

            //return  _context.RIM_LEVELS.Where(r => r.CATEGORY_ID == categoryId).OrderBy(r => r.PARENT_ID).ToList();
            return levels;
        }
        public TypesReturnResult getTypesCounts(String Select,String From, String Where, String GroupBy)
        {
            
            String myQuery = Select + From + Where + GroupBy;
            String TypesQuery = "select distinct type from ( " + myQuery + " )";
         

            DbHelper helper = new DbHelper();

            DataTable dt = helper.ExcuteQuery(myQuery);
            List<DbReturn> dbResult =  dt.SerializeType<DbReturn>().ToList();

            dt = helper.ExcuteQuery(TypesQuery);
            List<dbType> AllTypes = dt.SerializeType<dbType>().ToList();

            TypesReturnResult ApiResult = new TypesReturnResult();

            if(dbResult != null)
            {
                if (ApiResult.LocationNames == null)
                    ApiResult.LocationNames = new List<string>();
                
                if (ApiResult.TotalLocationTypes == null)
                    ApiResult.TotalLocationTypes = new List<TotalTypeCounts>();

              
                foreach (DbReturn record in dbResult)
                {
                    string exist = ApiResult.LocationNames.Find(y => y.Contains(record.LOCATION));
                    if (exist != null) // location exist
                    {
                        int index = ApiResult.LocationNames.IndexOf(record.LOCATION);
                        TypeCount t = ApiResult.TotalLocationTypes[index].LocTypesList.Find(y => y.Type.Equals(record.TYPE));
                        if (t != null)
                        {
                            t.Count=(int)record.COUNT;
                        }
                        
                    }
                    else
                    {
                        ApiResult.LocationNames.Add(record.LOCATION);//didn't found location
                        TotalTypeCounts LT_List = new TotalTypeCounts();
                        LT_List.LocTypesList = new List<TypeCount>();
                        foreach (dbType type in AllTypes)//inistialize all types
                        {
                            TypeCount TC = new TypeCount();
                            TC.Type = type.TYPE;
                            TC.Count = 0;
                            LT_List.LocTypesList.Add(TC);
                        }

                        TypeCount t = LT_List.LocTypesList.Find(y => y.Type.Equals(record.TYPE));
                        
                        if (t != null)
                        {
                            t.Count = (int)record.COUNT;
                        }

                        
                        ApiResult.TotalLocationTypes.Add(LT_List);
                    }
                    

                }
            }
            

            return ApiResult;

        }

        public BusynessReturn GetBusynessData(String SelectSource, String SelectDestination, String Where, String From, String GroupBy)
        {
            String myQuerySource="";
            String myQueryDestination = "";
            if (Where != null)
            {
                myQuerySource = SelectSource + From + Where + GroupBy;
                myQueryDestination = SelectDestination + From + Where + GroupBy;
            }
            else
            {
                myQuerySource = SelectSource + From + GroupBy;
                myQueryDestination = SelectDestination + From + GroupBy;
            }

            DbHelper helper = new DbHelper();

            DataTable SourceData = helper.ExcuteQuery(myQuerySource);
            DataTable DestinationData = helper.ExcuteQuery(myQueryDestination);
            List<BuysnessDbReturn> SourceDbResult = new List<BuysnessDbReturn>();
            List<BuysnessDbReturn> DestinationDbResult = new List<BuysnessDbReturn>();
            if (SourceData != null)
                SourceDbResult = SourceData.SerializeType<BuysnessDbReturn>().ToList();
            if(DestinationData != null)
                DestinationDbResult = DestinationData.SerializeType<BuysnessDbReturn>().ToList();

            BusynessReturn result = new BusynessReturn();
            result.LocationNames = new List<string>();
            result.Capacity = new List<int>();
            result.Free = new List<int>();
            result.Busy = new List<int>();

            if (SourceDbResult.Count() >= 1)//there is records in slots
            {
                foreach (BuysnessDbReturn SourceRec in SourceDbResult)
                {
                    //1) take location name
                    result.LocationNames.Add(SourceRec.LOCATION);

                    BuysnessDbReturn DectinationRecord = DestinationDbResult.Find(x => x.LOCATION == SourceRec.LOCATION);
                    
                    if (DectinationRecord != null)
                    {
                        result.Capacity.Add((int)SourceRec.COUNT);
                        result.Busy.Add((int)DectinationRecord.COUNT);
                        result.Free.Add(((int)SourceRec.COUNT) - ((int)DectinationRecord.COUNT));
                    }
                    else
                    {
                        result.Capacity.Add((int)SourceRec.COUNT);
                        result.Busy.Add(0);
                        result.Free.Add((int)SourceRec.COUNT);
                    }
                }
            }
           
            
            return result;
        }




        public List<Level> GetChartLevel (String CategoryName, String ModelName)
        {
            /*
                select distinct(rim_is_main.id), rim_category_level.display_name as DisplayName, rim_level.table_name as TableName,
                rim_attribute.name as ColumnName ,rim_attribute.attr_type as ColumnType,rim_category_level_type.LEVEL_ORDER,rim_category_level_type.id
                from rim_modules 
                inner join  rim_category on rim_modules.id=rim_category.module_id
                inner join rim_category_level on rim_category_level.rim_category_id=rim_category.id
                inner join rim_level on rim_level.id = rim_category_level.rim_level_id
                inner join rim_category_level_type on rim_category_level_type.rim_category_level_id = rim_category_level.id
                inner join rim_attribute on rim_attribute.rim_level_id=rim_level.id
                inner join rim_is_main on rim_is_main.rim_attribute_id=rim_attribute.id
                inner join rim_type on rim_is_main.rim_type_id=rim_type.id
                inner join rim_category_level_type on rim_type.id=rim_category_level_type.rim_type_id
                where rim_type.id=4 and rim_category_level_type.rim_type_id=4 and rim_category.name='Radio';
            */

            /*SELECT* FROM RIM_CATEGORY RC
            INNER JOIN RIM_CATEGORY_LEVEL RCL ON RC.ID = RCL.RIM_CATEGORY_ID
            INNER JOIN RIM_LEVEL RL ON RL.ID = RCL.RIM_LEVEL_ID
            INNER JOIN RIM_CATEGORY_LEVEL_TYPE RCLTT ON RCLTT.RIM_CATEGORY_LEVEL_ID = RCL.ID
            INNER JOIN RIM_TYPE RTT ON RCLTT.RIM_TYPE_ID = RTT.ID
            INNER JOIN RIM_IS_MAIN RIS ON RIS.RIM_CATEGORY_LEVEL_TYPE_ID = RCLTT.ID
            INNER JOIN RIM_ATTRIBUTE RAA ON RAA.ID = RIS.RIM_ATTRIBUTE_ID
            WHERE RC.NAME = 'Controller' AND RTT.NAME = 'TYPE'*/


            var levels = (from rim_modules              in _context.RIM_MODULES
                          join rim_category             in _context.RIM_CATEGORY                on rim_modules.ID equals rim_category.MODULE_ID
                          join rim_category_level       in _context.RIM_CATEGORY_LEVEL          on rim_category.ID equals rim_category_level.RIM_CATEGORY_ID
                          join rim_level                in _context.RIM_LEVEL                   on rim_category_level.RIM_LEVEL_ID equals rim_level.ID
                          join rim_category_level_type  in _context.RIM_CATEGORY_LEVEL_TYPE     on rim_category_level.ID equals rim_category_level_type.RIM_CATEGORY_LEVEL_ID
                          join rim_type                 in _context.RIM_TYPE on rim_category_level_type.RIM_TYPE_ID equals rim_type.ID
                          join rim_is_main              in _context.RIM_IS_MAIN on rim_category_level_type.ID equals rim_is_main.RIM_CATEGORY_LEVEL_TYPE_ID
                          join rim_attribute            in _context.RIM_ATTRIBUTE on rim_is_main.RIM_ATTRIBUTE_ID equals rim_attribute.ID
                          where rim_type.ID == 4 && rim_category_level_type.RIM_TYPE_ID == 4 && rim_category.NAME.ToUpper() == CategoryName.ToUpper() && rim_modules.NAME.ToUpper() == ModelName.ToUpper()
                          //orderby rim_category_level_type.LEVEL_ORDER //descending
                          select new
                          {
                              DisplayName = rim_category_level.DISPLAY_NAME,
                              TableName = rim_level.TABLE_NAME,
                              ColumnName = rim_attribute.NAME,
                              ColumnType = rim_attribute.ATTR_TYPE,
                              Order = rim_category_level_type.LEVEL_ORDER
                             
                          }).Distinct().ToList();

            List<Level> result = null;
            foreach (var record in levels)
            {
                if (result == null)
                {
                    result = new List<Level>();
                }
                Level lev = new Level();
                lev.DISPLAYNAME = record.DisplayName;
                lev.TABLENAME = record.TableName;
                lev.COLUMNTYPE = record.ColumnType;
                lev.COLUMNNAME = record.ColumnName;
                lev.ORDER = (int)record.Order;
                result.Add(lev);
            }
          
            result.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
            return result;
        }

        public CellCountReturn getCellCounts(String select , String from , String where, String groupby)
        {
            String myQuery = select + from + where + groupby;
            String BandsQuery = "select distinct BAND from ( " + myQuery + " )";


            DbHelper helper = new DbHelper();

            DataTable dt = helper.ExcuteQuery(myQuery);
            List<DbCellCountReturn> dbResult = dt.SerializeType<DbCellCountReturn>().ToList();

            dt = helper.ExcuteQuery(BandsQuery);
            List<dbBand> AllBand = dt.SerializeType<dbBand>().ToList();

            CellCountReturn ApiResult = new CellCountReturn();

            if (dbResult != null)
            {
                if (ApiResult.LocationNames == null)
                    ApiResult.LocationNames = new List<string>();

                if (ApiResult.TotalLocationCells == null)
                    ApiResult.TotalLocationCells = new List<TotalBandCounts>();


                foreach (DbCellCountReturn record in dbResult)
                {
                    string exist = ApiResult.LocationNames.Find(y => y.Contains(record.LOCATION));
                    if (exist != null) // location exist
                    {
                        int index = ApiResult.LocationNames.IndexOf(record.LOCATION);
                        Band t = ApiResult.TotalLocationCells[index].LocBandList.Find(y => y.BAND.Equals(record.BAND));
                        if (t != null)
                        {
                            t.COUNT=(int)record.COUNT;
                        }

                    }
                    else
                    {
                        ApiResult.LocationNames.Add(record.LOCATION);//didn't found location
                        TotalBandCounts LT_List = new TotalBandCounts();
                        LT_List.LocBandList = new List<Band>();
                        foreach (dbBand band in AllBand)//inistialize all types
                        {
                            Band TC = new Band();
                            TC.BAND = band.Band;
                            TC.COUNT = 0;
                            LT_List.LocBandList.Add(TC);
                        }

                        Band t = LT_List.LocBandList.Find(y => y.BAND.Equals(record.BAND));

                        if (t != null)
                        {
                            t.COUNT=(int)record.COUNT;
                        }


                        ApiResult.TotalLocationCells.Add(LT_List);
                    }


                }
            }

            return ApiResult;
            
        }

    }
}