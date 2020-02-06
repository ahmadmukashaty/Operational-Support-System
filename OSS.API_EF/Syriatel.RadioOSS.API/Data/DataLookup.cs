using Syriatel.RadioOSS.API.Models;
using Syriatel.RadioOSS.API.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Data
{
    public class DataLookup
    {
        DataContext _context;
        public DataLookup()
        {
            _context = new DataContext();
        }
        public List<AREA> getAreas()
        {
            List<AREA> levels = _context.AREA.ToList();


            //return  _context.RIM_LEVELS.Where(r => r.CATEGORY_ID == categoryId).OrderBy(r => r.PARENT_ID).ToList();
            return levels;
        }

        public List<Instance> getSitesGlbalInfo()
        {
            var modules = _context.SITE_GLOBAL_INFO.ToList();
            List<Instance> Values = new List<Instance>();
            foreach (var obj in modules)
            {
                if (Values == null)
                    Values = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.DisName = obj.ENGLISH_NAME;
                instance.Name = obj.CODE;

                Values.Add(instance);
            }
            return Values;
        }

        public object getSiteGlobalInfoDetails(int Id)
        {

            var q = (from s in _context.SITE_GLOBAL_INFO
                     where s.ID == Id

                     select new
                     {
                         s.AMSL_M,
                         s.ARABIC_NAME,
                         s.CODE,
                         s.ENGLISH_NAME,
                         s.LATITUDE,
                         s.LOGITUDE
                     }

                     ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AMSL_M"], item.AMSL_M);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ARABIC_NAME"], item.ARABIC_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CODE"], item.CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LATITUDE"], item.LATITUDE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LOGITUDE"], item.LOGITUDE);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getSiteCandidates(int siteGlobalInfoId)
        {
            var SiteCandidates = (from s in _context.SITE_GLOBAL_INFO
                                  join sc in _context.SITE_CANDIDATE
                                  on s.ID equals sc.SITE_GLOBAL_INFO_ID into scc
                                  from sc in scc.DefaultIfEmpty()
                                  where s.ID == siteGlobalInfoId
                                  where sc.IS_ACTIVE == 1
                                  select new
                                  {
                                      sc.ID,
                                      sc.ENGLISH_NAME,
                                      sc.SITE_CODE
                                  }).ToList();

            List<Instance> ScValues = new List<Instance>();
            foreach (var obj in SiteCandidates)
            {
                if (ScValues == null)
                    ScValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.DisName = obj.ENGLISH_NAME;
                instance.Name = obj.SITE_CODE;

                ScValues.Add(instance);
            }

            return ScValues;
        }

        public List<Instance> getAllSiteCandidates()
        {
            var SiteCandidates = (from sc in _context.SITE_CANDIDATE
                                  where sc.IS_ACTIVE == 1
                                  select new
                                  {
                                      sc.ID,
                                      sc.ENGLISH_NAME,
                                      sc.SITE_CODE
                                  }).ToList();

            List<Instance> ScValues = new List<Instance>();
            foreach (var obj in SiteCandidates)
            {
                if (ScValues == null)
                    ScValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.DisName = obj.ENGLISH_NAME;
                instance.Name = obj.SITE_CODE;

                ScValues.Add(instance);
            }

            return ScValues;
        }
        //public object getSiteCandidatesDetails(int Id)
        //{
        //    var SiteCandidates = (from s in _context.SITE_CANDIDATE
        //                          join sc in _context.SITE_CATEGORY
        //                          on s.SITE_CATEGORY_ID equals sc.ID into scc
        //                          from sc in scc.DefaultIfEmpty()
        //                          join scl in _context.SITE_CO_LOCATED
        //                          on s.SITE_CO_LOCATED_ID equals scl.ID into scll
        //                          from scl in scll.DefaultIfEmpty()
        //                          join sco in _context.SITE_CONFIGURATION
        //                          on s.SITE_CONFIGURATION_ID equals sco.ID into scoo
        //                          from sco in scoo.DefaultIfEmpty()
        //                          join scn in _context.SITE_CONTACT_PERSON
        //                          on s.SITE_CONTACT_PERSON_ID equals scn.ID into scnn
        //                          from scn in scnn.DefaultIfEmpty()
        //                          join sct in _context.SITE_COVERAGE_TYPE
        //                          on s.SITE_COVERAGE_TYPE_ID equals sct.ID into sctt
        //                          from sct in sctt.DefaultIfEmpty()
        //                          join sk in _context.SITE_KIND_OF_SUPPORT
        //                          on s.SITE_KIND_OF_SUPPORT_ID equals sk.ID into skk
        //                          from sk in skk.DefaultIfEmpty()
        //                          join slt in _context.SITE_LOCATION_TYPE
        //                          on s.SITE_LOCATION_TYPE_ID equals slt.ID into sltt
        //                          from slt in sltt.DefaultIfEmpty()
        //                          join spt in _context.SITE_PROPERTY_TYPE
        //                          on s.SITE_PROPERTY_TYPE_ID equals spt.ID into sptt
        //                          from spt in sptt.DefaultIfEmpty()
        //                          join sub in _context.SUBAREA
        //                          on s.SUBAREA_ID equals sub.ID into subb
        //                          from sub in subb.DefaultIfEmpty()
        //                          join z in _context.ZONE
        //                          on sub.ZONE_ID equals z.ID into zz
        //                          from z in zz.DefaultIfEmpty()
        //                          join ar in _context.AREA
        //                          on z.AREA_ID equals ar.ID into arr
        //                          from ar in arr.DefaultIfEmpty()
        //                          join r in _context.REGION
        //                          on ar.REGION_ID equals r.ID into rr
        //                          from r in rr.DefaultIfEmpty()
        //                          where s.ID == Id
        //                          select new
        //                          {
        //                              s.AMSL_M,
        //                              s.APPROVEDBY_DATE,
        //                              s.APPROVEDBY_UID,
        //                              s.ARABIC_NAME,
        //                              s.CANDIDATE_ADDRESS,
        //                              s.CANDIDATE_RANKING,
        //                              s.COVERED_AREA,
        //                              s.CRITICAL_SITE,
        //                              s.ENGLISH_NAME,
        //                              s.IS_ACTIVE,
        //                              s.LATITUDE_N,
        //                              s.LONGITUDE_E,
        //                              s.PREPAREDBY_DATE,
        //                              s.PREPAREDBY_UID,
        //                              s.REVIEWEDBY_DATE,
        //                              s.REVIEWEDBY_UID,
        //                              s.SHELTER_REQUIREMENTS,
        //                              sc.NAME,
        //                              SITE_CATEGORY_NAME = sc.NAME,
        //                              s.SITE_CODE,
        //                              SITE_CONFIGURATION_NAME = sco.NAME,
        //                              sco.VALUE,
        //                              scn.CONTACT_PERSON_ARABIC_NAME,
        //                              scn.CONTACT_PERSON_ENGLISH_NAME,
        //                              scn.CONTACT_PERSON_PHONE,
        //                              SITE_COVERAGE_TYPE_NAME = sct.NAME,
        //                              SITE_KIND_OF_SUPPORT_NAME = sk.NAME,
        //                              SITE_LOCATION_TYPE_NAME = slt.NAME,
        //                              SITE_PROPERTY_TYPE_NAME = spt.NAME,
        //                              SUBAREA_NAME = sub.NAME,
        //                              ZONE_NAME = z.NAME,
        //                              AREA_NAME = ar.NAME,
        //                              AREA_ARABIC_NAME = ar.ARABIC_NAME,
        //                              AREA_CODE = ar.CODE,
        //                              REGION_NAME = r.NAME,
        //                              REGION_CODE = r.CODE,
        //                              r.DISPLAY_NAME
        //                          }).ToList();

        //    List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
        //    foreach (var item in SiteCandidates)
        //    {
        //        Dictionary<string, object> d = new Dictionary<string, object>();
        //        //d.Add(ConfigurationFile.CLASS, item.CLASS);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["AMSL_M"], item.AMSL_M);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["APPROVEDBY_DATE"], item.APPROVEDBY_DATE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["APPROVEDBY_UID"], item.APPROVEDBY_UID);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["ARABIC_NAME"], item.ARABIC_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CANDIDATE_ADDRESS"], item.CANDIDATE_ADDRESS);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CANDIDATE_RANKING"], item.CANDIDATE_RANKING);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_ARABIC_NAME"], item.CONTACT_PERSON_ARABIC_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_ENGLISH_NAME"], item.CONTACT_PERSON_ENGLISH_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_PHONE"], item.CONTACT_PERSON_PHONE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["COVERED_AREA"], item.COVERED_AREA);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["CRITICAL_SITE"], item.CRITICAL_SITE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["IS_ACTIVE"], item.IS_ACTIVE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["LATITUDE_N"], item.LATITUDE_N);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["LONGITUDE_E"], item.LONGITUDE_E);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["PREPAREDBY_DATE"], item.PREPAREDBY_DATE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["PREPAREDBY_UID"], item.PREPAREDBY_UID);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["REVIEWEDBY_DATE"], item.REVIEWEDBY_DATE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["REVIEWEDBY_UID"], item.REVIEWEDBY_UID);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SHELTER_REQUIREMENTS"], item.SHELTER_REQUIREMENTS);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CATEGORY_NAME"], item.SITE_CATEGORY_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CONFIGURATION_NAME"], item.SITE_CONFIGURATION_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_COVERAGE_TYPE_NAME"], item.SITE_COVERAGE_TYPE_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_KIND_OF_SUPPORT_NAME"], item.SITE_KIND_OF_SUPPORT_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_LOCATION_TYPE_NAME"], item.SITE_LOCATION_TYPE_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_PROPERTY_TYPE_NAME"], item.SITE_PROPERTY_TYPE_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["VALUE"], item.VALUE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBAREA_NAME"], item.SUBAREA_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["ZONE_NAME"], item.ZONE_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_NAME"], item.AREA_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_ARABIC_NAME"], item.AREA_ARABIC_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_CODE"], item.AREA_CODE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION_NAME"], item.REGION_NAME);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION_CODE"], item.REGION_CODE);
        //        d.Add(System.Configuration.ConfigurationManager.AppSettings["DISPLAY_NAME"], item.DISPLAY_NAME);

        //        dictionary.Add(d);
        //    }

        //    return dictionary;
        //}

        public object getSiteCandidatesDetails(int Id)
        {
            var SiteCandidates = (from s in _context.SITE_CANDIDATE_DETAILS
                                  where s.ID == Id
                                  select new
                                  {
                                      s.AMSL_M,
                                      s.APPROVEDBY_DATE,
                                      s.APPROVEDBY_UID,
                                      s.ARABIC_NAME,
                                      s.CANDIDATE_ADDRESS,
                                      s.CANDIDATE_RANKING,
                                      s.COVERED_AREA,
                                      s.CRITICAL_SITE,
                                      s.ENGLISH_NAME,
                                      s.IS_ACTIVE,
                                      s.LATITUDE_N,
                                      s.LONGITUDE_E,
                                      s.PREPAREDBY_DATE,
                                      s.PREPAREDBY_UID,
                                      s.REVIEWEDBY_DATE,
                                      s.REVIEWEDBY_UID,
                                      s.SHELTER_REQUIREMENTS,
                                      s.SITE_CATEGORY_NAME,
                                      s.SITE_CODE,
                                      s.SITE_CONFIGURATION_NAME,
                                      s.VALUE,
                                      s.CONTACT_PERSON_ARABIC_NAME,
                                      s.CONTACT_PERSON_ENGLISH_NAME,
                                      s.CONTACT_PERSON_PHONE,
                                      s.SITE_COVERAGE_TYPE_NAME,
                                      s.SITE_KIND_OF_SUPPORT_NAME,
                                      s.SITE_LOCATION_TYPE_NAME,
                                      s.SITE_PROPERTY_TYPE_NAME,
                                      s.NENAME,
                                      s.SUBAREA_NAME,
                                      s.ZONE_NAME,
                                      s.AREA_NAME,
                                      s.AREA_ARABIC_NAME,
                                      s.AREA_CODE,
                                      s.REGION_NAME,
                                      s.REGION_CODE,
                                      s.DISPLAY_NAME
                                  }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in SiteCandidates)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AMSL_M"], item.AMSL_M);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["APPROVEDBY_DATE"], item.APPROVEDBY_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["APPROVEDBY_UID"], item.APPROVEDBY_UID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ARABIC_NAME"], item.ARABIC_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CANDIDATE_ADDRESS"], item.CANDIDATE_ADDRESS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CANDIDATE_RANKING"], item.CANDIDATE_RANKING);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_ARABIC_NAME"], item.CONTACT_PERSON_ARABIC_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_ENGLISH_NAME"], item.CONTACT_PERSON_ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CONTACT_PERSON_PHONE"], item.CONTACT_PERSON_PHONE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["COVERED_AREA"], item.COVERED_AREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CRITICAL_SITE"], item.CRITICAL_SITE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IS_ACTIVE"], item.IS_ACTIVE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LATITUDE_N"], item.LATITUDE_N);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LONGITUDE_E"], item.LONGITUDE_E);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PREPAREDBY_DATE"], item.PREPAREDBY_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PREPAREDBY_UID"], item.PREPAREDBY_UID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REVIEWEDBY_DATE"], item.REVIEWEDBY_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REVIEWEDBY_UID"], item.REVIEWEDBY_UID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SHELTER_REQUIREMENTS"], item.SHELTER_REQUIREMENTS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CATEGORY_NAME"], item.SITE_CATEGORY_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CONFIGURATION_NAME"], item.SITE_CONFIGURATION_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_COVERAGE_TYPE_NAME"], item.SITE_COVERAGE_TYPE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_KIND_OF_SUPPORT_NAME"], item.SITE_KIND_OF_SUPPORT_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_LOCATION_TYPE_NAME"], item.SITE_LOCATION_TYPE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_PROPERTY_TYPE_NAME"], item.SITE_PROPERTY_TYPE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VALUE"], item.VALUE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBAREA_NAME"], item.SUBAREA_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ZONE_NAME"], item.ZONE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_NAME"], item.AREA_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_ARABIC_NAME"], item.AREA_ARABIC_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA_CODE"], item.AREA_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION_NAME"], item.REGION_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION_CODE"], item.REGION_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DISPLAY_NAME"], item.DISPLAY_NAME);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getSiteIdentities(int siteCandidateId)
        {
            var SiteCandidates = (from sc in _context.SITE_CANDIDATE
                                  join s in _context.SITE_IDENTITY
                                  on sc.ID equals s.SITE_CANDIDATE_ID into ss
                                  from s in ss.DefaultIfEmpty()
                                  where sc.ID == siteCandidateId
                                  where sc.IS_ACTIVE == 1
                                  select new
                                  {
                                      s.ID,
                                      s.NENAME
                                  }).ToList();

            List<Instance> ScValues = new List<Instance>();
            foreach (var obj in SiteCandidates)
            {
                if (ScValues == null)
                    ScValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.NENAME;

                ScValues.Add(instance);
            }

            return ScValues;
        }


        public object getSiteIdentityDetails(int Id)
        {
            var SiteCandidates = (from s in _context.SITE_IDENTITY
                                  join rnt in _context.RADIO_NETYPE
                                  on s.RADIO_NETYPE_ID equals rnt.ID into rntt
                                  from rnt in rntt.DefaultIfEmpty()
                                  join sm in _context.SITE_MODE
                                  on s.SITE_MODE_ID equals sm.ID into smm
                                  from sm in smm.DefaultIfEmpty()
                                  join sc in _context.SITE_CANDIDATE
                                  on s.SITE_CANDIDATE_ID equals sc.ID into scc
                                  from sc in scc.DefaultIfEmpty()
                                  where s.ID == Id
                                  where sc.IS_ACTIVE == 1
                                  select new
                                  {
                                      s.CREATION_TIME,
                                      s.DEPLOYMENT_STATUS,
                                      s.DN,
                                      s.FIRST_CONN_TIME,
                                      s.GBTS_TYPE,
                                      s.MBTS_NAME,
                                      s.NENAME,
                                      s.NOTE,
                                      s.OM_IP,
                                      s.SITE_BANDS,
                                      s.SITE_VERSION,
                                      s.VENDOR,
                                      RADIO_NETYPE_SITE_VERSION = rnt.SITE_VERSION,
                                      rnt.TYPE,
                                      rnt.TYPE_NOTE,
                                      sc.ENGLISH_NAME,
                                      sc.SITE_CODE,
                                      SITE_MODE = sm.S_MODE
                                  }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in SiteCandidates)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATION_TIME"], item.CREATION_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DN"], item.DN);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIRST_CONN_TIME"], item.FIRST_CONN_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["GBTS_TYPE"], item.GBTS_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MBTS_NAME"], item.MBTS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NOTE"], item.NOTE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["OM_IP"], item.OM_IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_BANDS"], item.SITE_BANDS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_MODE"], item.SITE_MODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_VERSION"], item.SITE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR"], item.VENDOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RADIO_NETYPE_SITE_VERSION"], item.RADIO_NETYPE_SITE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE_NOTE"], item.TYPE_NOTE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);

                dictionary.Add(d);
            }

            return dictionary;
        }


        //Static Tree of rack-->subrack-->slot==>board-->port in Transmission
        public List<Instance> getRadioRacks(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var RadioRacks = (from sc in _context.SITE_CANDIDATE
                              join scr in _context.SITE_CANDIDATE_RACK
                              on sc.ID equals scr.SITE_CANDIDATE_ID into scrr
                              from scr in scrr.DefaultIfEmpty()
                              join r in _context.RADIO_RACK
                              on scr.RADIO_RACK_ID equals r.ID into rr
                              from r in rr.DefaultIfEmpty()
                              where sc.ID == siteCandidateId
                              where scr.RETIRE_DATE == null
                              where sc.IS_ACTIVE == 1
                              select new
                              {
                                  r.ID,
                                  r.CABINET_NUMBER,
                                  sc.ENGLISH_NAME
                              }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in RadioRacks)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.CABINET_NUMBER;
                instance.Name = obj.ENGLISH_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getSubRadioRacks(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from sc in _context.SITE_CANDIDATE
                     join scr in _context.SITE_CANDIDATE_RACK
                     on sc.ID equals scr.SITE_CANDIDATE_ID into scrr
                     from scr in scrr.DefaultIfEmpty()
                     join r in _context.RADIO_RACK
                     on scr.RADIO_RACK_ID equals r.ID into rr
                     from r in rr.DefaultIfEmpty()
                     join rrs in _context.RADIO_RACK_SUBRACK
                     on r.ID equals rrs.RADIO_RACK_ID into rrss
                     from rrs in rrss.DefaultIfEmpty()
                     join rs in _context.RADIO_SUBRACK
                     on rrs.RADIO_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where scr.RETIRE_DATE == null
                     where rrs.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (rs.ID == null) ? 0 : rs.ID,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         //rs.ID,
                         //rs.SUBRACK_NUMBER,
                         sc.ENGLISH_NAME,
                         CABINET_NUMBER = (r.CABINET_NUMBER == null) ? 0 : r.CABINET_NUMBER
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.ParentId = obj.CABINET_NUMBER;
                instance.Name = obj.ENGLISH_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }


        public List<Instance> getRadioSlots(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from sc in _context.SITE_CANDIDATE
                     join scr in _context.SITE_CANDIDATE_RACK
                     on sc.ID equals scr.SITE_CANDIDATE_ID into scrr
                     from scr in scrr.DefaultIfEmpty()
                     join r in _context.RADIO_RACK
                     on scr.RADIO_RACK_ID equals r.ID into rr
                     from r in rr.DefaultIfEmpty()
                     join rrs in _context.RADIO_RACK_SUBRACK
                     on r.ID equals rrs.RADIO_RACK_ID into rrss
                     from rrs in rrss.DefaultIfEmpty()
                     join rs in _context.RADIO_SUBRACK
                     on rrs.RADIO_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RADIO_SLOT
                     on rs.ID equals sl.RADIO_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where scr.RETIRE_DATE == null
                     where rrs.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (sl.ID == null) ? 0 : sl.ID,
                         SLOT_NUMBER = (sl.SLOT_NUMBER == null) ? 0 : sl.SLOT_NUMBER,
                         //sl.ID,
                         //sl.SLOT_NUMBER,
                         sc.ENGLISH_NAME,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         CABINET_NUMBER = (r.CABINET_NUMBER == null) ? 0 : r.CABINET_NUMBER
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.CABINET_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.Name = obj.ENGLISH_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getRadioBoards(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from sc in _context.SITE_CANDIDATE
                     join scr in _context.SITE_CANDIDATE_RACK
                     on sc.ID equals scr.SITE_CANDIDATE_ID into scrr
                     from scr in scrr.DefaultIfEmpty()
                     join r in _context.RADIO_RACK
                     on scr.RADIO_RACK_ID equals r.ID into rr
                     from r in rr.DefaultIfEmpty()
                     join rrs in _context.RADIO_RACK_SUBRACK
                     on r.ID equals rrs.RADIO_RACK_ID into rrss
                     from rrs in rrss.DefaultIfEmpty()
                     join rs in _context.RADIO_SUBRACK
                     on rrs.RADIO_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RADIO_SLOT
                     on rs.ID equals sl.RADIO_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RADIO_SLOT_BOARD
                     on sl.ID equals rsb.RADIO_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RADIO_BOARD
                     on rsb.RADIO_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where scr.RETIRE_DATE == null
                     where rrs.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (rb.ID == null) ? 0 : rb.ID,
                         //SERIAL_NUMBER = (rb.SERIAL_NUMBER == null) ? 0 : rb.SERIAL_NUMBER,
                         //rb.ID,
                         SLOT_NUMBER = (rb.SLOT_NUMBER == null) ? 0 : rb.SLOT_NUMBER,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         CABINET_NUMBER = (r.CABINET_NUMBER == null) ? 0 : r.CABINET_NUMBER,
                         sc.ENGLISH_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.CABINET_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.Name = obj.ENGLISH_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getRadioPorts(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from sc in _context.SITE_CANDIDATE
                     join scr in _context.SITE_CANDIDATE_RACK
                     on sc.ID equals scr.SITE_CANDIDATE_ID into scrr
                     from scr in scrr.DefaultIfEmpty()
                     join r in _context.RADIO_RACK
                     on scr.RADIO_RACK_ID equals r.ID into rr
                     from r in rr.DefaultIfEmpty()
                     join rrs in _context.RADIO_RACK_SUBRACK
                     on r.ID equals rrs.RADIO_RACK_ID into rrss
                     from rrs in rrss.DefaultIfEmpty()
                     join rs in _context.RADIO_SUBRACK
                     on rrs.RADIO_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RADIO_SLOT
                     on rs.ID equals sl.RADIO_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RADIO_SLOT_BOARD
                     on sl.ID equals rsb.RADIO_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RADIO_BOARD
                     on rsb.RADIO_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     join rp in _context.RADIO_PORT
                     on rb.ID equals rp.RADIO_BOARD_ID into rpp
                     from rp in rpp.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where scr.RETIRE_DATE == null
                     where rrs.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (rp.ID == null) ? 0 : rp.ID,
                         PORT_NUMBER = (rp.PORT_NUMBER == null) ? 0 : rp.PORT_NUMBER,
                         SLOT_NUMBER = (rb.SLOT_NUMBER == null) ? 0 : rb.SLOT_NUMBER,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         CABINET_NUMBER = (r.CABINET_NUMBER == null) ? 0 : r.CABINET_NUMBER,
                         //rp.ID,
                         //rp.PORT_NUMBER,
                         sc.ENGLISH_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.CABINET_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.PId = obj.PORT_NUMBER;
                instance.Name = obj.ENGLISH_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        //details of rack subrack slot board port hostver antenna cell
        public object getRadioRackDetails(int Id)
        {
            var q = (from rr in _context.RADIO_RACK_DETAILS
                     where rr.ID == Id
                     select new
                     {
                         rr.ENGLISH_NAME,
                         rr.SITE_CODE,
                         rr.NENAME,
                         rr.CABINET_NUMBER,
                         rr.SERIAL_NUMBER,
                         rr.DATE_OF_MANUFACTURE,
                         rr.MANUFACTURER_DATA,
                         rr.BOM_RACK_TYPE,
                         rr.BOM_CODE,
                         rr.VENDOR_NAME,
                         rr.HARDWARE_VERSION,
                         rr.INVENTORY_UNIT_ID,
                         rr.INVENTORY_UNIT_TYPE,
                         rr.DATE_OF_LAST_SERVICE,
                         rr.UNIT_POSITION,
                         rr.ISSUE_NUMBER,
                         rr.UNIT_POSITION_FULL,
                         rr.DEPLOYMENT_STATUS,
                         rr.TYPE,
                         rr.TYPE_NOTE
                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CABINET_NUMBER"], item.CABINET_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_RACK_TYPE"], item.BOM_RACK_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ISSUE_NUMBER"], item.ISSUE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_FULL"], item.UNIT_POSITION_FULL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE_NOTE"], item.TYPE_NOTE);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioSUBRackDetails(int Id)
        {
            var q = (from rs in _context.RADIO_SUBRACK_DETAILS
                     where rs.ID == Id
                     select new
                     {
                         rs.ENGLISH_NAME,
                         rs.SITE_CODE,
                         rs.NENAME,
                         rs.BOM_CODE,
                         rs.BOM_FRAME_TYPE,
                         rs.DATE_OF_MANUFACTURE,
                         rs.DEPLOYMENT_STATUS,
                         rs.INVENTORY_UNIT_ID,
                         rs.INVENTORY_UNIT_TYPE,
                         rs.MANUFACTURER_DATA,
                         rs.SERIAL_NUMBER,
                         rs.SUBRACK_NUMBER,
                         rs.UNIT_POSITION,
                         rs.UNIT_POSITION_FULL,
                         rs.VENDOR_NAME,
                         rs.NE_NAME,
                         rs.TYPE,
                         rs.TYPE_NOTE

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_FRAME_TYPE"], item.BOM_FRAME_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_NUMBER"], item.SUBRACK_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE_NOTE"], item.TYPE_NOTE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_FULL"], item.UNIT_POSITION_FULL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioSlotDetails(int Id)
        {
            var q = (from rs in _context.RADIO_SLOT_DETAILS
                     where rs.ID == Id
                     select new
                     {
                         rs.ENGLISH_NAME,
                         rs.SITE_CODE,
                         rs.NENAME,
                         rs.DATE_OF_LAST_SERVICE,
                         rs.DATE_OF_MANUFACTURE,
                         rs.INVENTORY_UNIT_ID,
                         rs.MANUFACTURER_DATA,
                         rs.SERIAL_NUMBER,
                         rs.SITE_INDEX,
                         rs.SLOT_NUMBER,
                         rs.SLOT_POSITION,
                         rs.UNIT_POSITION,
                         rs.UNIT_POSITION_FULL,
                         rs.VENDOR_NAME,
                         rs.VENDOR_UNIT_FAMILY_TYPE,
                         rs.VENDOR_UNIT_TYPE_NUMBER,
                         rs.VERSION_NUMBER

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_INDEX"], item.SITE_INDEX);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_NUMBER"], item.SLOT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_POSITION"], item.SLOT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_FULL"], item.UNIT_POSITION_FULL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioBoardDetails(int Id)
        {
            var q = (from rb in _context.RADIO_BOARD_DETAILS
                     where rb.ID == Id
                     select new
                     {
                         rb.ENGLISH_NAME,
                         rb.SITE_CODE,
                         rb.NENAME,
                         rb.BIOS_VEREX,
                         rb.BIOSVER,
                         rb.BOM_CODE,
                         rb.DATE_OF_MANUFACTURE,
                         rb.DEPLOYMENT_STATUS,
                         rb.HARDWARE_VERSION,
                         rb.MANUFACTURER_DATA,
                         //rb.NAME,
                         rb.SLOT_NUMBER,
                         rb.SERIAL_NUMBER,
                         rb.SOFTWARE_VERSION,
                         rb.VENDOR_NAME,
                         rb.RADIO_BOARD_TYPE_Name,
                         rb.NAME_NOTE

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VEREX"], item.BIOS_VEREX);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOSVER"], item.BIOSVER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                //d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_NUMBER"], item.SLOT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME_NOTE"], item.NAME_NOTE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RADIO_BOARD_TYPE_Name"], item.RADIO_BOARD_TYPE_Name);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioPortDetails(int Id)
        {
            var q = (from rp in _context.RADIO_PORT_DETAILS
                     where rp.ID == Id
                     select new
                     {
                         rp.ENGLISH_NAME,
                         rp.SITE_CODE,
                         rp.NENAME,
                         rp.DATE_OF_LAST_SERVICE,
                         rp.DATE_OF_MANUFACTURE,
                         rp.INVENTORY_UNIT_ID,
                         rp.INVENTORY_UNIT_TYPE,
                         rp.MANUFACTURER_DATA,
                         rp.PORT_NUMBER,
                         rp.SERIAL_NUMBER,
                         rp.SITE_INDEX,
                         rp.SLOT_POS,
                         rp.UNIT_POSITION,
                         rp.UNIT_POSITION_FULL,
                         rp.VENDOR_NAME,
                         rp.VENDOR_UNIT_FAMILY_TYPE,
                         rp.VENDOR_UNIT_TYPE_NUMBER,
                         rp.VERSION_NUMBER

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NUMBER"], item.PORT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_INDEX"], item.SITE_INDEX);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_POS"], item.SLOT_POS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_FULL"], item.UNIT_POSITION_FULL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioHostverDetails(int Id)
        {
            var q = (from sc in _context.SITE_CANDIDATE
                     join s in _context.SITE_IDENTITY
                     on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     from s in ss.DefaultIfEmpty()
                     join srh in _context.SITE_IDENTITY_RADIO_HOSTVER
                     on s.ID equals srh.SITE_IDENTITY_ID into srhh
                     from srh in srhh.DefaultIfEmpty()
                     join rh in _context.RADIO_HOSTVER
                     on srh.RADIO_HOSTVER_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     where srh.RETIRE_DATE == null
                     where rh.ID == Id
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         sc.ENGLISH_NAME,
                         sc.SITE_CODE,
                         s.NENAME,
                         rh.DESCRIPTION,
                         rh.DETAILS,
                         rh.HOST_VER,
                         rh.TYPE

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DETAILS"], item.DETAILS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HOST_VER"], item.HOST_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioAntennaDetails(int Id)
        {
            var q = (from sc in _context.SITE_CANDIDATE
                     join s in _context.SITE_IDENTITY
                     on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sa in _context.SITE_IDENTITY_ANTENNA
                     on s.ID equals sa.SITE_IDENTITY_ID into saa
                     from sa in saa.DefaultIfEmpty()
                     join a in _context.ANTENNA
                     on sa.ANTENNA_ID equals a.ID into aa
                     from a in aa.DefaultIfEmpty()
                     where sa.RETIRE_DATE == null
                     where a.ID == Id
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         sc.ENGLISH_NAME,
                         sc.SITE_CODE,
                         s.NENAME,
                         a.DATE_OF_LAST_SERVICE,
                         a.DATE_OF_MANUFACTURE,
                         a.DEPLOYMENT_STATUS,
                         a.INVENTORY_UNIT_ID,
                         a.INVENTORY_UNIT_TYPE,
                         a.MANUFACTURER_DATA,
                         a.NE_NAME,
                         a.SERIAL_NUMBER,
                         a.UNIT_POSITION,
                         a.UNIT_POSITION_FULL,
                         a.VENDOR_NAME,
                         a.VENDOR_UNIT_FAMILY_TYPE,
                         a.VENDOR_UNIT_TYPE_NUMBER,
                         a.VERSION_NUMBER

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_FULL"], item.UNIT_POSITION_FULL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);

                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getRadioCellDetails(int Id)
        {
            var ddd = _context.CELL.Where(cq => cq.ID == Id).Select(cq => cq.TYPE).FirstOrDefault();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            if (ddd == "U")
            {
                var q = (from sc in _context.SITE_CANDIDATE
                         join s in _context.SITE_IDENTITY
                         on sc.ID equals s.SITE_CANDIDATE_ID into ss
                         from s in ss.DefaultIfEmpty()
                         join sic in _context.SITE_IDENTITY_CHILD
                         on s.ID equals sic.SITE_IDENTITY_ID into sicc
                         from sic in sicc.DefaultIfEmpty()
                         join c in _context.CELL
                         on sic.ID equals c.SITE_IDENTITY_CHILD_ID into cc
                         from c in cc.DefaultIfEmpty()
                         join gc in _context.GCELL
                         on c.ID equals gc.ID into gcc
                         from gc in gcc.DefaultIfEmpty()
                         join uc in _context.UCELL
                         on c.ID equals uc.ID into ucc
                         from uc in ucc.DefaultIfEmpty()
                         join b in _context.BAND
                         on c.BAND_ID equals b.ID into bb
                         from b in bb.DefaultIfEmpty()
                         where c.ID == Id
                         where sc.IS_ACTIVE == 1
                         select new
                         {
                             sc.ENGLISH_NAME,
                             sc.SITE_CODE,
                             s.NENAME,
                             CELL_TYPE = c.TYPE,
                             c.CELL_NAME,
                             c.CGI,
                             //GCELL_BCC = gc.BCC,
                             //GCELL_BCCHNO = gc.BCCHNO,
                             //GCEL_CI = gc.CI,
                             //GCELL_DETAILS = gc.DETAILS,
                             //GCEL_LAC = gc.LAC,
                             //GCELL_MCC = gc.MCC,
                             //GCELL_MNC = gc.MNC,
                             //GCELL_NAME = gc.NAME,
                             //GCELL_NCC = gc.NCC,
                             //GCELL_TYPE = gc.TYPE,
                             UCELL_NAME = uc.CELL_NAME,
                             UCELL_DETAILS = uc.DETAILS,
                             UCELL_LAC = uc.LAC,
                             UCELL_LOCELL = uc.LOCELL,
                             UCELL_MANUFACTURER_DATA = uc.MANUFACTURER_DATA,
                             UCELL_MAXTX_POWER = uc.MAXTX_POWER,
                             UCELL_NODEB_NAME = uc.NODEB_NAME,
                             UCELL_PSCRAMB_CODE = uc.PSCRAMB_CODE,
                             UCELL_RAC = uc.RAC,
                             UCELL_SAC = uc.SAC,
                             UCELL_UARFCN_DOWN_LINK = uc.UARFCN_DOWN_LINK,
                             UCELL_UARFCN_UP_LINK = uc.UARFCN_UP_LINK,
                             UCELL_USER_LABEL = uc.USER_LABEL,
                             BAND_CODE = b.CODE,
                             BAND_VALUE = b.VALUE

                         }).ToList();


                foreach (var item in q)
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["BAND_CODE"], item.BAND_CODE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["BAND_VALUE"], item.BAND_VALUE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CELL_TYPE"], item.CELL_TYPE);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCEL_CI"], item.GCEL_CI);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCEL_LAC"], item.GCEL_LAC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_BCC"], item.GCELL_BCC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_BCCHNO"], item.GCELL_BCCHNO);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_DETAILS"], item.GCELL_DETAILS);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_MCC"], item.GCELL_MCC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_MNC"], item.GCELL_MNC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_NAME"], item.GCELL_NAME);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_NCC"], item.GCELL_NCC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_TYPE"], item.GCELL_TYPE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_DETAILS"], item.UCELL_DETAILS);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_LAC"], item.UCELL_LAC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_LOCELL"], item.UCELL_LOCELL);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_MANUFACTURER_DATA"], item.UCELL_MANUFACTURER_DATA);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_MAXTX_POWER"], item.UCELL_MAXTX_POWER);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_NAME"], item.UCELL_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_NODEB_NAME"], item.UCELL_NODEB_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_PSCRAMB_CODE"], item.UCELL_PSCRAMB_CODE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_RAC"], item.UCELL_RAC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_SAC"], item.UCELL_SAC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_UARFCN_DOWN_LINK"], item.UCELL_UARFCN_DOWN_LINK);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_UARFCN_UP_LINK"], item.UCELL_UARFCN_UP_LINK);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_USER_LABEL"], item.UCELL_USER_LABEL);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CELL_NAME"], item.CELL_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CGI"], item.CGI);

                    dictionary.Add(d);
                }


            }
            else if (ddd == "G")
            {
                var q = (from sc in _context.SITE_CANDIDATE
                         join s in _context.SITE_IDENTITY
                         on sc.ID equals s.SITE_CANDIDATE_ID into ss
                         from s in ss.DefaultIfEmpty()
                         join sic in _context.SITE_IDENTITY_CHILD
                         on s.ID equals sic.SITE_IDENTITY_ID into sicc
                         from sic in sicc.DefaultIfEmpty()
                         join c in _context.CELL
                         on sic.ID equals c.SITE_IDENTITY_CHILD_ID into cc
                         from c in cc.DefaultIfEmpty()
                         join gc in _context.GCELL
                         on c.ID equals gc.ID into gcc
                         from gc in gcc.DefaultIfEmpty()
                         join uc in _context.UCELL
                         on c.ID equals uc.ID into ucc
                         from uc in ucc.DefaultIfEmpty()
                         join b in _context.BAND
                         on c.BAND_ID equals b.ID into bb
                         from b in bb.DefaultIfEmpty()
                         where c.ID == Id
                         where sc.IS_ACTIVE == 1
                         select new
                         {
                             sc.ENGLISH_NAME,
                             sc.SITE_CODE,
                             s.NENAME,
                             CELL_TYPE = c.TYPE,
                             c.CELL_NAME,
                             c.CGI,
                             GCELL_BCC = gc.BCC,
                             GCELL_BCCHNO = gc.BCCHNO,
                             GCEL_CI = gc.CI,
                             GCELL_DETAILS = gc.DETAILS,
                             GCEL_LAC = gc.LAC,
                             GCELL_MCC = gc.MCC,
                             GCELL_MNC = gc.MNC,
                             GCELL_NAME = gc.NAME,
                             GCELL_NCC = gc.NCC,
                             GCELL_TYPE = gc.TYPE,
                             //UCELL_NAME = uc.CELL_NAME,
                             //UCELL_DETAILS = uc.DETAILS,
                             //UCELL_LAC = uc.LAC,
                             //UCELL_LOCELL = uc.LOCELL,
                             //UCELL_MANUFACTURER_DATA = uc.MANUFACTURER_DATA,
                             //UCELL_MAXTX_POWER = uc.MAXTX_POWER,
                             //UCELL_NODEB_NAME = uc.NODEB_NAME,
                             //UCELL_PSCRAMB_CODE = uc.PSCRAMB_CODE,
                             //UCELL_RAC = uc.RAC,
                             //UCELL_SAC = uc.SAC,
                             //UCELL_UARFCN_DOWN_LINK = uc.UARFCN_DOWN_LINK,
                             //UCELL_UARFCN_UP_LINK = uc.UARFCN_UP_LINK,
                             //UCELL_USER_LABEL = uc.USER_LABEL,
                             BAND_CODE = b.CODE,
                             BAND_VALUE = b.VALUE

                         }).ToList();


                foreach (var item in q)
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_CODE"], item.SITE_CODE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["NENAME"], item.NENAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["BAND_CODE"], item.BAND_CODE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["BAND_VALUE"], item.BAND_VALUE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CELL_TYPE"], item.CELL_TYPE);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCEL_CI"], item.GCEL_CI);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCEL_LAC"], item.GCEL_LAC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_BCC"], item.GCELL_BCC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_BCCHNO"], item.GCELL_BCCHNO);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_DETAILS"], item.GCELL_DETAILS);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_MCC"], item.GCELL_MCC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_MNC"], item.GCELL_MNC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_NAME"], item.GCELL_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_NCC"], item.GCELL_NCC);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["GCELL_TYPE"], item.GCELL_TYPE);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_DETAILS"], item.UCELL_DETAILS);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_LAC"], item.UCELL_LAC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_LOCELL"], item.UCELL_LOCELL);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_MANUFACTURER_DATA"], item.UCELL_MANUFACTURER_DATA);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_MAXTX_POWER"], item.UCELL_MAXTX_POWER);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_NAME"], item.UCELL_NAME);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_NODEB_NAME"], item.UCELL_NODEB_NAME);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_PSCRAMB_CODE"], item.UCELL_PSCRAMB_CODE);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_RAC"], item.UCELL_RAC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_SAC"], item.UCELL_SAC);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_UARFCN_DOWN_LINK"], item.UCELL_UARFCN_DOWN_LINK);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_UARFCN_UP_LINK"], item.UCELL_UARFCN_UP_LINK);
                    //d.Add(System.Configuration.ConfigurationManager.AppSettings["UCELL_USER_LABEL"], item.UCELL_USER_LABEL);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CELL_NAME"], item.CELL_NAME);
                    d.Add(System.Configuration.ConfigurationManager.AppSettings["CGI"], item.CGI);

                    dictionary.Add(d);
                }

            }
            return dictionary;
        }
        //output ???getSiteAntennas???getSiteCells???getCellAntennas???getAntennaCells???getSiteHostVer
        public List<Instance> getSiteAntennas(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from a in _context.ANTENNA
                     join sa in _context.SITE_IDENTITY_CHILD_ANTENNA
                     on a.ID equals sa.ANTENNA_ID into saa
                     from sa in saa.DefaultIfEmpty()
                     join sic in _context.SITE_IDENTITY_CHILD
                     on sa.SITE_IDENTITY_CHILD_ID equals sic.ID into sicc
                     from sic in sicc.DefaultIfEmpty()
                     join s in _context.SITE_IDENTITY
                      on sic.SITE_IDENTITY_ID equals s.ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sc in _context.SITE_CANDIDATE
                      on s.SITE_CANDIDATE_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where sa.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (a.ID == null) ? 0 : a.ID,
                         a.INVENTORY_UNIT_ID,
                         a.SERIAL_NUMBER
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.INVENTORY_UNIT_ID;
                instance.DisName = obj.SERIAL_NUMBER;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getSiteCells(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from c in _context.CELL
                     join sic in _context.SITE_IDENTITY_CHILD
                     on c.SITE_IDENTITY_CHILD_ID equals sic.ID into sicc
                     from sic in sicc.DefaultIfEmpty()
                     join s in _context.SITE_IDENTITY
                     on sic.SITE_IDENTITY_ID equals s.ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sc in _context.SITE_CANDIDATE
                     on s.SITE_CANDIDATE_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where sc.IS_ACTIVE == 1
                     //from sc in _context.SITE_CANDIDATE
                     //join s in _context.SITE_IDENTITY
                     //on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     //from s in ss.DefaultIfEmpty()
                     //join sic in _context.SITE_IDENTITY_CHILD
                     //on s.ID equals sic.SITE_IDENTITY_ID into sicc
                     //from sic in sicc.DefaultIfEmpty()
                     //join c in _context.CELL
                     //on sic.ID equals c.SITE_IDENTITY_CHILD_ID into cc
                     //from c in cc.DefaultIfEmpty()
                     //where sc.ID == siteCandidateId
                     //where sc.IS_ACTIVE == 1
                     select new
                     {

                         ID = (c.ID == null) ? 0 : c.ID,
                         c.TYPE,
                         c.CELL_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.TYPE;
                instance.DisName = obj.CELL_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getCellAntennas(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from a in _context.ANTENNA
                     join ac in _context.ANTENNA_CELL
                     on a.ID equals ac.ANTENNA_ID into acc
                     from ac in acc.DefaultIfEmpty()
                     join c in _context.CELL
                     on ac.CELL_ID equals c.ID into cc
                     from c in cc.DefaultIfEmpty()
                     join sic in _context.SITE_IDENTITY_CHILD
                     on c.SITE_IDENTITY_CHILD_ID equals sic.ID into sicc
                     from sic in sicc.DefaultIfEmpty()
                     join s in _context.SITE_IDENTITY
                     on sic.SITE_IDENTITY_ID equals s.ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sc in _context.SITE_CANDIDATE
                     on s.SITE_CANDIDATE_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where ac.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     //from sc in _context.SITE_CANDIDATE
                     //join s in _context.SITE_IDENTITY
                     //on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     //from s in ss.DefaultIfEmpty()
                     //join sic in _context.SITE_IDENTITY_CHILD
                     //on s.ID equals sic.SITE_IDENTITY_ID into sicc
                     //from sic in sicc.DefaultIfEmpty()
                     //join c in _context.CELL
                     //on sic.ID equals c.SITE_IDENTITY_CHILD_ID into cc
                     //from c in cc.DefaultIfEmpty()
                     //join ac in _context.ANTENNA_CELL
                     //on c.ID equals ac.CELL_ID into acc
                     //from ac in acc.DefaultIfEmpty()
                     //join a in _context.ANTENNA
                     //on ac.ANTENNA_ID equals a.ID into aa
                     //from a in aa.DefaultIfEmpty()
                     //where sc.ID == siteCandidateId
                     //where ac.RETIRE_DATE == null
                     //where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (a.ID == null) ? 0 : a.ID,
                         a.INVENTORY_UNIT_ID,
                         a.SERIAL_NUMBER
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.INVENTORY_UNIT_ID;
                instance.DisName = obj.SERIAL_NUMBER;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getAntennaCells(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from c in _context.CELL
                     join ac in _context.ANTENNA_CELL
                     on c.ID equals ac.CELL_ID into acc
                     from ac in acc.DefaultIfEmpty()
                     join a in _context.ANTENNA
                     on ac.ANTENNA_ID equals a.ID into aa
                     from a in aa.DefaultIfEmpty()
                     join sa in _context.SITE_IDENTITY_ANTENNA
                     on a.ID equals sa.ANTENNA_ID into saa
                     from sa in saa.DefaultIfEmpty()
                     join s in _context.SITE_IDENTITY
                     on sa.SITE_IDENTITY_ID equals s.ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sc in _context.SITE_CANDIDATE
                     on s.SITE_CANDIDATE_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where ac.RETIRE_DATE == null
                     where sa.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     //from sc in _context.SITE_CANDIDATE
                     //join s in _context.SITE_IDENTITY
                     //on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     //from s in ss.DefaultIfEmpty()
                     //join sa in _context.SITE_IDENTITY_ANTENNA
                     //on s.ID equals sa.SITE_IDENTITY_ID into saa
                     //from sa in saa.DefaultIfEmpty()
                     //join a in _context.ANTENNA
                     //on sa.ANTENNA_ID equals a.ID into aa
                     //from a in aa.DefaultIfEmpty()
                     //join ac in _context.ANTENNA_CELL
                     //on a.ID equals ac.ANTENNA_ID into acc
                     //from ac in acc.DefaultIfEmpty()
                     //join c in _context.CELL
                     //on ac.CELL_ID equals c.ID into cc
                     //from c in cc.DefaultIfEmpty()
                     //where sc.ID == siteCandidateId
                     //where ac.RETIRE_DATE == null
                     //where sa.RETIRE_DATE == null
                     //where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (c.ID == null) ? 0 : c.ID,
                         c.TYPE
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.TYPE;
                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getSiteHostVer(int siteCandidateId)
        {
            List<Instance> BValues = null;
            var q = (from rh in _context.RADIO_HOSTVER
                     join srh in _context.SITE_IDENTITY_RADIO_HOSTVER
                     on rh.ID equals srh.RADIO_HOSTVER_ID into srhh
                     from srh in srhh.DefaultIfEmpty()
                     join s in _context.SITE_IDENTITY
                     on srh.SITE_IDENTITY_ID equals s.ID into ss
                     from s in ss.DefaultIfEmpty()
                     join sc in _context.SITE_CANDIDATE
                     on s.SITE_CANDIDATE_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sc.ID == siteCandidateId
                     where srh.RETIRE_DATE == null
                     where sc.IS_ACTIVE == 1
                     //from sc in _context.SITE_CANDIDATE
                     //join s in _context.SITE_IDENTITY
                     //on sc.ID equals s.SITE_CANDIDATE_ID into ss
                     //from s in ss.DefaultIfEmpty()
                     //join srh in _context.SITE_IDENTITY_RADIO_HOSTVER
                     //on s.ID equals srh.SITE_IDENTITY_ID into srhh
                     //from srh in srhh.DefaultIfEmpty()
                     //join rh in _context.RADIO_HOSTVER
                     //on srh.RADIO_HOSTVER_ID equals rh.ID into rhh
                     //from rh in rhh.DefaultIfEmpty()
                     //where sc.ID == siteCandidateId
                     //where srh.RETIRE_DATE == null
                     //where sc.IS_ACTIVE == 1
                     select new
                     {
                         ID = (rh.ID == null) ? 0 : rh.ID,
                         rh.HOST_VER,
                         rh.TYPE
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.TYPE;
                instance.DisName = obj.HOST_VER;
                BValues.Add(instance);
            }
            return BValues;
        }


        //// RAN //////
        //public List<Instance> getAllRANNEs(int RANTypeId)
        //{
        //    List<Instance> BValues = null;
        //    var q = (from s in _context.RAN_CONTROLLER
        //             join sr in _context.RAN_NE_TYPE
        //             on s.RAN_NETYPE_ID equals sr.ID
        //             where sr.ID == RANTypeId
        //             select new
        //             {
        //                 s.ID,
        //                 s.NE_NAME
        //             }).ToList();
        //    BValues = new List<Instance>();
        //    foreach (var obj in q)
        //    {
        //        if (BValues == null)
        //            BValues = new List<Instance>();

        //        Instance instance = new Instance();

        //        instance.Id = obj.ID;
        //        instance.Name = obj.NE_NAME;
        //        BValues.Add(instance);
        //    }
        //    return BValues;
        //}
        public List<Instance> getAllRANNEs(int subCategoryId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RAN_NE_TYPE
                     on s.RAN_NETYPE_ID equals sr.ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join sc in _context.RIM_SUBCATEGORY
                     on sr.RIM_SUBCATEGORY_ID equals sc.ID into scc
                     from sc in scc.DefaultIfEmpty()
                     where sr.RIM_SUBCATEGORY_ID == subCategoryId
                     select new
                     {
                         s.ID,
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.NE_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANNEDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RAN_NE_TYPE
                     on s.RAN_NETYPE_ID equals sr.ID into srr
                     from sr in srr.DefaultIfEmpty()
                     where s.ID == Id
                     select new
                     {
                         s.CREATION_TIME,
                         s.DEPLOYMENT_STATUS,
                         s.FIRST_CONNECTION_TIME,
                         s.INTERNAL_ID,
                         s.IP,
                         s.NE_DESCRIPTION,
                         s.NE_DN,
                         s.NE_NAME,
                         s.PHISICAL_LOCATION,
                         s.SOFTWARE_VERSION,
                         s.TIME_ZONE,
                         s.VENDOR_NAME,
                         sr.NAME

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATION_TIME"], item.CREATION_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIRST_CONNECTION_TIME"], item.FIRST_CONNECTION_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INTERNAL_ID"], item.INTERNAL_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IP"], item.IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_DESCRIPTION"], item.NE_DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_DN"], item.NE_DN);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PHISICAL_LOCATION"], item.PHISICAL_LOCATION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TIME_ZONE"], item.TIME_ZONE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);


                dictionary.Add(d);
            }

            return dictionary;
        }
        public List<Instance> getRANHostVers(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RAN_CONTROLLER_RAN_HOSTVER
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_HOST_VER
                     on sr.RAN_HOSTVER_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     where s.ID == RANControllerId
                     where sr.RETIRE_DATE == null
                     select new
                     {
                         rh.ID,
                         rh.NAME,
                         rh.TYPE
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.NAME;
                instance.DisName = obj.TYPE;
                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANHostverDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RAN_CONTROLLER_RAN_HOSTVER
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_HOST_VER
                     on sr.RAN_HOSTVER_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     where sr.RETIRE_DATE == null
                     where rh.ID == Id
                     select new
                     {
                         rh.DESCRIPTION,
                         rh.HOST_VER,
                         rh.NAME,
                         rh.TYPE,
                         s.NE_NAME

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HOST_VER"], item.HOST_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getRANRacks(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     where s.ID == RANControllerId
                     where sr.RETIRE_DATE == null
                     select new
                     {
                         ID = (rh.ID == null) ? 0 : rh.ID,
                         //rh.INVENTORY_UNIT_ID,
                         RACK_NUMBER = (rh.RACK_NUMBER == null) ? 0 : rh.RACK_NUMBER,
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                //instance.ParentId = obj.INVENTORY_UNIT_ID;
                instance.ParentId = obj.RACK_NUMBER;
                instance.Name = obj.NE_NAME;

                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANRACKDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     where sr.RETIRE_DATE == null
                     where rh.ID == Id
                     select new
                     {
                         rh.BOM_CODE,
                         rh.BOM_RACK_TYPE,
                         rh.DATE_OF_LAST_SERVICE,
                         rh.DATE_OF_MANUFACTURE,
                         rh.DEPLOYMENT_STATUS,
                         rh.INVENTORY_UNIT_ID,
                         rh.INVENTORY_UNIT_TYPE,
                         rh.ISSUE_NUMBER,
                         rh.MANUFACTURER_DATA,
                         rh.RACK_NUMBER,
                         rh.RACK_TYPE,
                         rh.SERIAL_NUMBER,
                         rh.UNIT_POSITION,
                         rh.UNIT_POSITION_DETAIL,
                         rh.VENDOR_NAME,
                         rh.VENDOR_UNIT_FAMILY_TYPE,
                         rh.VENDOR_UNIT_TYPE_NUMBER,
                         rh.VERSION_NUMBER,
                         s.NE_NAME


                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_RACK_TYPE"], item.BOM_RACK_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ISSUE_NUMBER"], item.ISSUE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RACK_NUMBER"], item.RACK_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RACK_TYPE"], item.RACK_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_DETAIL"], item.UNIT_POSITION_DETAIL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getRANSUBRacks(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     where s.ID == RANControllerId
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     select new
                     {
                         ID = (rs.ID == null) ? 0 : rs.ID,
                         //rs.INVENTORY_UNIT_ID,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         RACK_NUMBER = (rh.RACK_NUMBER == null) ? 0 : rh.RACK_NUMBER,
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                //instance.Name = obj.INVENTORY_UNIT_ID;
                instance.ParentId = obj.RACK_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.Name = obj.NE_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANSUBRACKDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rrc in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rrc.ID into rrcc
                     from rrc in rrcc.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rrc.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rh in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK_HW_TYPE
                     on rh.RAN_SUBRACK_HW_TYPE_ID equals rs.ID into rsss
                     //if null in the another tablre
                     from rs in rsss.DefaultIfEmpty()
                     join rst in _context.RAN_SCU_TYPE
                     on rh.RAN_SCU_TYPE_ID equals rst.ID into rstt
                     from rst in rstt.DefaultIfEmpty()
                     join rss in _context.RAN_SUBRACK_TYPE
                     on rh.RAN_SUBRACK_TYPE_ID equals rss.ID into rsst
                     from rss in rsst.DefaultIfEmpty()
                     where rh.ID == Id
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null


                     select new
                     {
                         rh.BOM_CODE,
                         rh.BOM_FRAME_TYPE,
                         rh.DATE_OF_LAST_SERVICE,
                         rh.DATE_OF_MANUFACTURE,
                         rh.DEPLOYMENT_STATUS,
                         rh.INVENTORY_UNIT_ID,
                         rh.INVENTORY_UNIT_TYPE,
                         rh.ISUUE_NUMBER,
                         rh.MANUFACTURER_DATA,
                         rh.MODULE_NO,
                         rh.RACK_FRAME_NO,
                         rh.SERIAL_NUMBER,
                         rh.SUBRACK_NAME,
                         rh.SUBRACK_NUMBER,
                         rh.UNIT_POSITION,
                         rh.UNIT_POSITION_DETAIL,
                         rh.VENDOR_NAME,
                         rh.VENDOR_UNIT_FAMILY_TYPE,
                         rh.VENDOR_UNIT_TYPE_NUMBER,
                         rh.VERSION_NUMBER,
                         rh.WORK_MODE_ID,
                         rs.SUBRACK_HW_TYPE,
                         rst.SCU_TYPE,
                         rss.TYPE,
                         s.NE_NAME

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_FRAME_TYPE"], item.BOM_FRAME_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ISUUE_NUMBER"], item.ISUUE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MODULE_NO"], item.MODULE_NO);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RACK_FRAME_NO"], item.RACK_FRAME_NO);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SCU_TYPE"], item.SCU_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_HW_TYPE"], item.SUBRACK_HW_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_NAME"], item.SUBRACK_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_NUMBER"], item.SUBRACK_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_DETAIL"], item.UNIT_POSITION_DETAIL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["WORK_MODE_ID"], item.WORK_MODE_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getRANSlots(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where s.ID == RANControllerId

                     select new
                     {
                         ID = (sl.ID == null) ? 0 : sl.ID,
                         SLOT_NUMBER = (sl.SLOT_NUMBER == null) ? 0 : sl.SLOT_NUMBER,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         RACK_NUMBER = (rh.RACK_NUMBER == null) ? 0 : rh.RACK_NUMBER,
                         //sl.ID,
                         //sl.SLOT_NUMBER,
                         //sl.INVENTORY_UNIT_ID 
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                //instance.Name = obj.INVENTORY_UNIT_ID;
                instance.ParentId = obj.RACK_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.Name = obj.NE_NAME;

                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANSlotDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where sl.ID == Id
                     select new
                     {
                         sl.DATE_OF_LAST_SERVICE,
                         sl.DATE_OF_MANUFACTURE,
                         sl.INVENTORY_UNIT_ID,
                         sl.INVENTORY_UNIT_TYPE,
                         sl.MANUFACTURER_DATA,
                         sl.SERIAL_NUMBER,
                         sl.SLOT_NUMBER,
                         sl.SLOT_POSITION,
                         sl.UNIT_POSITION,
                         sl.UNIT_POSITION_DETAIL,
                         sl.VENDOR_NAME,
                         sl.VENDOR_UNIT_FAMILY_TYPE,
                         sl.VENDOR_UNIT_TYPE_NUMBER,
                         sl.VERSION_NUMBER,
                         s.NE_NAME

                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_NUMBER"], item.SLOT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_POSITION"], item.SLOT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_DETAIL"], item.UNIT_POSITION_DETAIL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getRANBoards(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     //if null in the another table
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RAN_SLOT_BOARD
                     on sl.ID equals rsb.RAN_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RAN_BOARD
                     on rsb.RAN_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     where s.ID == RANControllerId
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     select new
                     {
                         ID = (rb.ID == null) ? 0 : rb.ID,
                         //INVENTORY_UNIT_ID = ( rb.INVENTORY_UNIT_ID == null) ?0 : rb.INVENTORY_UNIT_ID,
                         //rb.SUBSLOT_NO
                         SLOT_NUMBER = (rb.SLOT_NUMBER == null) ? 0 : sl.SLOT_NUMBER,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         RACK_NUMBER = (rh.RACK_NUMBER == null) ? 0 : rh.RACK_NUMBER,
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                //instance.ParentId = obj.INVENTORY_UNIT_ID;
                //instance.Name = obj.SUBSLOT_NO;
                instance.ParentId = obj.RACK_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.Name = obj.NE_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANBoardDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     //if null in the another table
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RAN_SLOT_BOARD
                     on sl.ID equals rsb.RAN_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RAN_BOARD
                     on rsb.RAN_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     join rt in _context.RAN_BOARD_TYPE
                     on rb.RAN_BOARD_TYPE_ID equals rt.ID into rtt
                     from rt in rtt.DefaultIfEmpty()
                     join rl in _context.RAN_LOGICAL_FUNCT_TYPE
                     on rb.RAN_LOGICAL_FUNCT_TYPE_ID equals rl.ID into rll
                     from rl in rll.DefaultIfEmpty()
                     join rbc in _context.RAN_BOARD_CLASS
                     on rb.RAN_BOARD_CLASS_ID equals rbc.ID into rbcc
                     from rbc in rbcc.DefaultIfEmpty()
                     join rtp in _context.RAN_TYPE_POUC_TDM
                     on rb.RAN_TYPE_POUC_TDM_ID equals rtp.ID into rtpp
                     from rtp in rtpp.DefaultIfEmpty()
                     join rp in _context.RAN_PORT_WORK_MODE
                     on rb.RAN_PORT_WORK_MODE_ID equals rp.ID into rpp
                     from rp in rpp.DefaultIfEmpty()
                     where rb.ID == Id
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     select new
                     {
                         rb.BIOS_VER,
                         rb.BIOS_VEREX,
                         rb.BOARD_TYPE,
                         rb.BOM_CODE,
                         rb.DATE_OF_LAST_SERVICE,
                         rb.DATE_OF_MANUFACTURE,
                         rb.DEPLOYMENT_STATUS,
                         rb.INVENTORY_UNIT_ID,
                         rb.INVENTORY_UNIT_TYPE,
                         rb.ISSUE_NUMBER,
                         rb.LAN_VER,
                         rb.LOGIC_VER,
                         rb.MANUFACTURER_DATA,
                         rb.MBUS_VER,
                         rb.MODULE_NO,
                         rb.SERIAL_NUMBER,
                         rb.SLOT_NUMBER,
                         rb.SLOT_POSITION,
                         rb.SOFTWARE_VERSION,
                         rb.SUBSLOT_NO,
                         rb.UNIT_POSITION,
                         rb.UNIT_POSITION_DETAIL,
                         rb.VENDOR_NAME,
                         rb.VENDOR_UNIT_FAMILY_TYPE,
                         rb.VENDOR_UNIT_TYPE_NUMBER,
                         rb.VERSION_NUMBER,
                         rt.NAME,
                         rt.NOTE,
                         rl.LOGICAL_FUNCTION_TYPE,
                         rbc.CLASS,
                         rtp.APPLICATION_TYPE_OF_POUC_TDM,
                         rp.PORT_WORK_MODE,
                         s.NE_NAME


                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["APPLICATION_TYPE_OF_POUC_TDM"], item.APPLICATION_TYPE_OF_POUC_TDM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VER"], item.BIOS_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VEREX"], item.BIOS_VEREX);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOARD_TYPE"], item.BOARD_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODE"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLASS"], item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ISSUE_NUMBER"], item.ISSUE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LAN_VER"], item.LAN_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LOGIC_VER"], item.LOGIC_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LOGICAL_FUNCTION_TYPE"], item.LOGICAL_FUNCTION_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MBUS_VER"], item.MBUS_VER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MODULE_NO"], item.MODULE_NO);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NOTE"], item.NOTE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_WORK_MODE"], item.PORT_WORK_MODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_NUMBER"], item.SLOT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_POSITION"], item.SLOT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBSLOT_NO"], item.SUBSLOT_NO);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_DETAIL"], item.UNIT_POSITION_DETAIL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_FAMILY_TYPE"], item.VENDOR_UNIT_FAMILY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public List<Instance> getRANPorts(int RANControllerId)
        {
            List<Instance> BValues = null;
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RAN_SLOT_BOARD
                     on sl.ID equals rsb.RAN_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RAN_BOARD
                     on rsb.RAN_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     join rp in _context.RAN_PORT
                     on rb.ID equals rp.RAN_BOARD_ID into rpp
                     from rp in rpp.DefaultIfEmpty()
                     where s.ID == RANControllerId
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     select new
                     {
                         ID = (rp.ID == null) ? 0 : rp.ID,
                         //SUBSLOT_NUMBER = (rp.SUBSLOT_NUMBER == null) ? 0: rp.SUBSLOT_NUMBER,
                         //rp.INVENTORY_UNIT_ID
                         PORT_NUMBER = (rp.PORT_NUMBER == null) ? 0 : rp.PORT_NUMBER,
                         SLOT_NUMBER = (rb.SLOT_NUMBER == null) ? 0 : sl.SLOT_NUMBER,
                         SUBRACK_NUMBER = (rs.SUBRACK_NUMBER == null) ? 0 : rs.SUBRACK_NUMBER,
                         RACK_NUMBER = (rh.RACK_NUMBER == null) ? 0 : rh.RACK_NUMBER,
                         s.NE_NAME
                     }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in q)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                //instance.Name = obj.INVENTORY_UNIT_ID;
                //instance.ParentId = obj.SUBSLOT_NUMBER;
                instance.ParentId = obj.RACK_NUMBER;
                instance.SId = obj.SUBRACK_NUMBER;
                instance.SlId = obj.SLOT_NUMBER;
                instance.PId = obj.PORT_NUMBER;
                instance.Name = obj.NE_NAME;
                BValues.Add(instance);
            }
            return BValues;
        }

        public object getRANPortDetails(int Id)
        {
            var q = (from s in _context.RAN_CONTROLLER
                     join sr in _context.RANCONTROLLER_RACK
                     on s.ID equals sr.RAN_CONTROLLER_ID into srr
                     from sr in srr.DefaultIfEmpty()
                     join rh in _context.RAN_RACK
                     on sr.RAN_RACK_ID equals rh.ID into rhh
                     from rh in rhh.DefaultIfEmpty()
                     join sc in _context.RAN_RACK_SUBRACK
                     on rh.ID equals sc.RAN_RACK_ID into scc
                     from sc in scc.DefaultIfEmpty()
                     join rs in _context.RAN_SUBRACK
                     on sc.RAN_SUBRACK_ID equals rs.ID into rss
                     from rs in rss.DefaultIfEmpty()
                     join sl in _context.RAN_SLOT
                     on rs.ID equals sl.RAN_SUBRACK_ID into sll
                     from sl in sll.DefaultIfEmpty()
                     join rsb in _context.RAN_SLOT_BOARD
                     on sl.ID equals rsb.RAN_SLOT_ID into rsbb
                     from rsb in rsbb.DefaultIfEmpty()
                     join rb in _context.RAN_BOARD
                     on rsb.RAN_BOARD_ID equals rb.ID into rbb
                     from rb in rbb.DefaultIfEmpty()
                     join rp in _context.RAN_PORT
                      on rb.ID equals rp.RAN_BOARD_ID into rpp
                     from rp in rpp.DefaultIfEmpty()
                     where rp.ID == Id
                     where sr.RETIRE_DATE == null
                     where sc.RETIRE_DATE == null
                     where rsb.RETIRE_DATE == null
                     select new
                     {
                         rp.DATE_OF_LAST_SERVICE,
                         rp.DATE_OF_MANUFACTURE,
                         rp.DEPLOYMENT_STATUS,
                         rp.INVENTORY_UNIT_ID,
                         rp.INVENTORY_UNIT_TYPE,
                         rp.MANUFACTURER_DATA,
                         rp.PORT_DESCRIPTION,
                         rp.PORT_NUMBER,
                         rp.PORT_TYPE,
                         rp.SERIAL_NUMBER,
                         rp.SLOT_POSITION,
                         rp.SUBSLOT_NUMBER,
                         rp.UNIT_POSITION,
                         rp.UNIT_POSITION_DETAIL,
                         rp.VENDOR_NAME,
                         rp.VENDOR_UNIT_TYPE_NUMBER,
                         rp.VERSION_NUMBER,
                         s.NE_NAME


                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();//to WebConfig
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_LAST_SERVICE"], item.DATE_OF_LAST_SERVICE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DATE_OF_MANUFACTURE"], item.DATE_OF_MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_ID"], item.INVENTORY_UNIT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["INVENTORY_UNIT_TYPE"], item.INVENTORY_UNIT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER_DATA"], item.MANUFACTURER_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_DESCRIPTION"], item.PORT_DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NUMBER"], item.PORT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_TYPE"], item.PORT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_POSITION"], item.SLOT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBSLOT_NUMBER"], item.SUBSLOT_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION"], item.UNIT_POSITION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UNIT_POSITION_DETAIL"], item.UNIT_POSITION_DETAIL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_NAME"], item.VENDOR_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR_UNIT_TYPE_NUMBER"], item.VENDOR_UNIT_TYPE_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VERSION_NUMBER"], item.VERSION_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_NAME"], item.NE_NAME);
                dictionary.Add(d);
            }

            return dictionary;
        }

    }
}