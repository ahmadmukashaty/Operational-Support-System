using Syriatel.OSS.API.Data;
using Syriatel.OSS.API.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.OSS.API.Models.Site
{
    public class LocationSiteProcess
    {
        private DataLookup OracleHelper = new DataLookup();

        private List<LocationSiteModelView> records { get; set; }

        public LocationSiteData Data { get; set; }
        public LocationSiteProcess()
        {
            // get db data from oracle helper
            this.records = OracleHelper.GetSites();

            this.Data = new LocationSiteData();


            foreach (LocationSiteModelView record in records)
            {

                if (!this.Data.Region.Contains(record.Region))
                {
                    //Console.Out('')
                    this.Data.Region.Add(record.Region);
                }
                if (!this.Data.Area.Contains(record.Area))
                {
                    //Console.Out('')
                    this.Data.Area.Add(record.Area);
                }
                if (!this.Data.Zone.Contains(record.Zone))
                {
                    //Console.Out('')
                    this.Data.Zone.Add(record.Zone);
                }
                if (!this.Data.SubArea.Contains(record.SubArea))
                {
                    //Console.Out('')
                    this.Data.SubArea.Add(record.SubArea);
                }

                SiteData site = new SiteData();
                site.ID = record.Site.ID;
                site.CODE = record.Site.CODE;
                site.ENGLISH_NAME = record.Site.ENGLISH_NAME;

                this.Data.Site.Add(site);

            }

            GenerateHierarchy();


        }

        public void GenerateHierarchy()
        {
            if (Data.DataHierarchy == null)
                Data.DataHierarchy = new LocationSiteHierarchy();

            foreach (string region in this.Data.Region)
            {
                if (Data.DataHierarchy.Regions == null)
                    Data.DataHierarchy.Regions = new List<Region>();

                //function get all model views which have the determined region
                List<LocationSiteModelView> sites = FilterByName(region, records, Constants.REGION_FILTER);

                Region rg = new Region();
                rg.REGION = region;

                GetRegionAreas(rg, sites);

                Data.DataHierarchy.Regions.Add(rg);
            }
        }



        private void GetRegionAreas(Region region, List<LocationSiteModelView> sites)
        {
            foreach (string area in this.Data.Area)
            {
                if (region.Areas == null)
                    region.Areas = new List<Area>();

                //function get all model views which have the determined area
                List<LocationSiteModelView> subSites = FilterByName(area, sites, Constants.AREA_FILTER);

                if (subSites != null)
                {
                    Area ar = new Area();
                    ar.AREA = area;

                    GetAreaZones(ar, subSites);

                    region.Areas.Add(ar);
                }
            }
        }

        private void GetAreaZones(Area area, List<LocationSiteModelView> sites)
        {
            foreach (string zone in this.Data.Zone)
            {
                if (area.Zones == null)
                    area.Zones = new List<Zone>();

                //function get all model views which have the determined area 
                //comment comment
                List<LocationSiteModelView> subSites = FilterByName(zone, sites, Constants.ZONE_FILTER);

                if (subSites != null)
                {
                    Zone zn = new Zone();
                    zn.ZONE = zone;

                    GetZoneSubArea(zn, subSites);

                    area.Zones.Add(zn);
                }

            }
        }

        private void GetZoneSubArea(Zone zone, List<LocationSiteModelView> sites)
        {
            foreach (string subArea in this.Data.SubArea)
            {
                if (zone.SubAreas == null)
                    zone.SubAreas = new List<SubArea>();

                //function get all model views which have the determined area
                List<LocationSiteModelView> subSites = FilterByName(subArea, sites, Constants.SUBAREA_FILTER);

                if (subSites != null)
                {
                    SubArea sa = new SubArea();
                    sa.SUBAREA = subArea;

                    GetSubAreaSites(sa, subSites);

                    zone.SubAreas.Add(sa);
                }

            }
        }

        private void GetSubAreaSites(SubArea subArea, List<LocationSiteModelView> sites)
        {
            foreach (SiteData site in this.Data.Site)
            {
                if (subArea.Sites == null)
                    subArea.Sites = new List<SiteData>();

                List<LocationSiteModelView> subSites = FilterByName(site.ENGLISH_NAME, sites, Constants.SITE_FILTER);
                if (subSites != null)
                {
                    subArea.Sites.Add(site);
                }

            }
        }

        private List<LocationSiteModelView> FilterByName(string name, List<LocationSiteModelView> sites, int filterType)
        {
            List<LocationSiteModelView> filteredSite = new List<LocationSiteModelView>();

            foreach (LocationSiteModelView site in sites)
            {
                switch (filterType)
                {
                    case Constants.REGION_FILTER:
                        {
                            if (site.Region == name)
                                filteredSite.Add(site);
                            break;
                        }

                    case Constants.AREA_FILTER:
                        {
                            if (site.Area == name)
                                filteredSite.Add(site);
                            break;
                        }
                    case Constants.ZONE_FILTER:
                        {
                            if (site.Zone == name)
                                filteredSite.Add(site);
                            break;
                        }
                    case Constants.SUBAREA_FILTER:
                        {
                            if (site.SubArea == name)
                                filteredSite.Add(site);
                            break;
                        }

                    case Constants.SITE_FILTER:
                        {
                            if (site.Site.ENGLISH_NAME == name)
                                filteredSite.Add(site);
                            break;
                        }
                }
            }

            if (filteredSite.Count == 0)
                filteredSite = null;

            return filteredSite;
        }
    }
}