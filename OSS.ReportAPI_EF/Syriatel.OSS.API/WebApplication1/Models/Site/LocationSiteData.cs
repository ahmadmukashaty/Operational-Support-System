using Syriatel.OSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class LocationSiteData
    {
        public List<string> Region { get; set; }
        public List<string> Area { get; set; }
        public List<string> Zone { get; set; }
        public List<string> SubArea { get; set; }
        public List<SiteData> Site { get; set; }

        public LocationSiteHierarchy DataHierarchy { get; set; }

        public LocationSite_DB_Source DB_Source { get; set; }


        public LocationSiteData()
        {
            this.Region = new List<string>();
            this.Area = new List<string>();
            this.Zone = new List<string>();
            this.SubArea = new List<string>();
            this.Site = new List<SiteData>();
            this.DB_Source = new LocationSite_DB_Source();

            Get_DB_Sources();
        }

        private void Get_DB_Sources()
        {
            this.DB_Source.RegionSource.TableName = Constants.REGION_TABLE;
            this.DB_Source.RegionSource.ColumnName = Constants.SITE_LOCATION_COLUMN_NAME;
            this.DB_Source.RegionSource.ColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;

            this.DB_Source.AreaSource.TableName = Constants.AREA_TABLE;
            this.DB_Source.AreaSource.ColumnName = Constants.SITE_LOCATION_COLUMN_NAME;
            this.DB_Source.AreaSource.ColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;

            this.DB_Source.ZoneSource.TableName = Constants.ZONE_TABLE;
            this.DB_Source.ZoneSource.ColumnName = Constants.SITE_LOCATION_COLUMN_NAME;
            this.DB_Source.ZoneSource.ColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;

            this.DB_Source.SubAreaSource.TableName = Constants.SUBAREA_TABLE;
            this.DB_Source.SubAreaSource.ColumnName = Constants.SITE_LOCATION_COLUMN_NAME;
            this.DB_Source.SubAreaSource.ColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;

            this.DB_Source.SiteSource.TableName = Constants.SITE_TABLE;
            this.DB_Source.SiteSource.SiteCodeColumnName = Constants.SITE_CODE_COLUMN_NAME;
            this.DB_Source.SiteSource.SiteCodeColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;
            this.DB_Source.SiteSource.EnglishNameColumnName = Constants.SITE_ENGLISH_NAME_COLUMN_NAME;
            this.DB_Source.SiteSource.EnglishNameColumnType = Constants.SITE_LOCATION_COLUMN_TYPE;

        }
    }
}