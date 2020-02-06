
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Syriatel.OSS.API.Models;
using Syriatel.OSS.API.NeTreeModeView;
using Syriatel.OSS.API.Models.Views;

namespace Syriatel.OSS.API.Data
{
    public class DataLookup
    {
        DataContext _context;
        public DataLookup()
        {
            _context = new DataContext();
        }

        public List<Instance> getTransmitionPath(int categoryId)
        {
            var levels = (from c in _context.RIM_LEVELS
                          join r in _context.RIM_CATEGORY
                           on c.CATEGORY_ID equals r.Id
                          orderby c.PARENT_ID
                          where c.CATEGORY_ID == categoryId
                          where c.PARENT_ID != null
                          select new
                          {
                              c.ID,
                              c.NAME
                          }
             ).ToList();
            List<Instance> Values = new List<Instance>();
            foreach (var obj in levels)
            {
                if (Values == null)
                    Values = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.NAME;

                Values.Add(instance);
            }
            //return  _context.RIM_LEVELS.Where(r => r.CATEGORY_ID == categoryId).OrderBy(r => r.PARENT_ID).ToList();
            return Values;
        }
        public List<Instance> getRoots(int rootId)
        {
            var modules = _context.RIM_MODULES.Where(f => f.ID == rootId).ToList();
            List<Instance> Values = new List<Instance>();
            foreach (var obj in modules)
            {
                if (Values == null)
                    Values = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.NAME;

                Values.Add(instance);
            }
            return Values;
        }
        public List<RIM_CATEGORY> getAllCategories()
        {
            List<RIM_CATEGORY> categories = _context.RIM_CATEGORY.ToList();
            return categories;
        }
        public List<Instance> getCategories(int ModuleId)
        {
            //List<RIM_SUBCATEGORY> subcategories = 
            //    _context.RIM_SUBCATEGORY
            //    .Include(x => x.RIM_CATEGORY)
            //    .Where(x => x.categoryId == categoryId)
            //    .ToList();
            var categories = (from c in _context.RIM_MODULES
                              join s in _context.RIM_CATEGORY
                                  on c.ID equals s.MODULE_ID
                              where s.MODULE_ID == ModuleId
                              select new
                              {
                                  NAME = s.Name,
                                  s.Id
                              }).ToList();

            List<Instance> Values = new List<Instance>();
            foreach (var obj in categories)
            {
                if (Values == null)
                    Values = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.Id;
                instance.Name = obj.NAME;

                Values.Add(instance);
            }

            return Values;
        }
        public List<Instance> getAllSubCategories(int categoryId)
        {
            //List<RIM_SUBCATEGORY> subcategories = 
            //    _context.RIM_SUBCATEGORY
            //    .Include(x => x.RIM_CATEGORY)
            //    .Where(x => x.categoryId == categoryId)
            //    .ToList();
            var subcategories = (from c in _context.RIM_CATEGORY
                                 join s in _context.RIM_SUBCATEGORY
                                     on c.Id equals s.CATEGORY_ID
                                 where s.CATEGORY_ID == categoryId
                                 select new
                                 {
                                     s.Name,
                                     s.Id
                                 }).ToList();

            List<Instance> NeValues = new List<Instance>();
            foreach (var obj in subcategories)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.Id;
                instance.Name = obj.Name;

                NeValues.Add(instance);
            }

            return NeValues;
        }


        public object getNEDetails1(int urefId, int categoryId, int SubCategory)
        {
            if (categoryId == 1)
            {
                var q = (from c in _context.RIM_CATEGORY
                         join s in _context.RIM_SUBCATEGORY
                         on c.Id equals s.CATEGORY_ID
                         join ne in _context.DATACOM_NE
                         on s.Id equals ne.SUB_CATEGORY_ID
                         join net in _context.DATACOM_NE_Type
                         on ne.TYPE_ID equals net.ID
                         where ne.U2000_REF_ID == urefId
                         where c.Id == categoryId
                         where s.Id == SubCategory

                         select new
                         {

                             ne.U2000_REF_ID,
                             ne.NAME,
                             ne.ALIAS_NAME,
                             ne.SUBRACK_TYPE,
                             ne.IP,
                             ne.MAC_ADDRESS,
                             ne.SOFTWARE_VERSION,
                             ne.PATCH_VERSION,
                             ne.LSR_ID,
                             ne.REMARKS,
                             ne.DEPLOYMENT_STATUS,
                             ne.CREATE_DATE,
                             ne.FREE_BOARD,
                             net.VENDOR,
                             net.CLASS,
                             net.SERIES,
                             net.MODEL,
                             net.TOTAL_CHILD_NUMBER
                         }

                         ).ToList();



                return q;
            }
            else
                return null;
        }

        public object getNEDetails(int Id)
        {

            var q = (from ne in _context.DATACOM_NE
                     join net in _context.DATACOM_NE_Type
                     on ne.TYPE_ID equals net.ID
                     join dns in _context.DATACOM_NE_SITE
                     on ne.ID equals dns.NE_ID
                     join s in _context.SITE
                     on dns.SITE_ID equals s.ID
                     where ne.ID == Id

                     select new
                     {
                         ne.U2000_REF_ID,
                         ne.NAME,
                         ne.ALIAS_NAME,
                         ne.SUBRACK_TYPE,
                         ne.IP,
                         ne.MAC_ADDRESS,
                         ne.SOFTWARE_VERSION,
                         ne.PATCH_VERSION,
                         ne.LSR_ID,
                         ne.REMARKS,
                         ne.DEPLOYMENT_STATUS,
                         ne.CREATE_DATE,
                         ne.FREE_BOARD,
                         net.VENDOR,
                         net.CLASS,
                         net.SERIES,
                         net.MODEL,
                         net.TOTAL_CHILD_NUMBER,
                         s.AREA,
                         s.CODE,
                         s.ENGLISH_NAME,
                         s.REGION,
                         s.SUBAREA,
                         s.ZONE
                     }

                     ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["U2000_REF_ID"], item.U2000_REF_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_TYPE"], item.SUBRACK_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IP"], item.IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MAC_ADDRESS"], item.MAC_ADDRESS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PATCH_VERSION"], item.PATCH_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LSR_ID"], item.LSR_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATE_DATE"], item.CREATE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_BOARD"], item.FREE_BOARD);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR"], item.VENDOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLASS"], item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIES"], item.SERIES);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MODEL"], item.MODEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_CHILD_NUMBER"], item.TOTAL_CHILD_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA"], item.AREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CODE"], item.CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION"], item.REGION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBAREA"], item.SUBAREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ZONE"], item.ZONE);
                dictionary.Add(d);
            }

            return dictionary;
        }
        //getOpticalNEDetails
        public object getOpticalNEDetails(int Id)
        {

            var q = (from ne in _context.OPTICAL_NE
                     join net in _context.OPTICAL_NE_TYPE
                     on ne.TYPE_ID equals net.ID
                     where ne.ID == Id


                     select new
                     {
                         ne.ALIAS_NAME,
                         ne.CREATED_DATE,
                         ne.DEPLOYMENT_STATUS,
                         ne.FIBER_COUNT,
                         ne.GETWAY_TYPE,
                         ne.IP,
                         ne.LSR_ID,
                         ne.MAC_ADDRESS,
                         ne.MODEL,
                         ne.NAME,
                         ne.PATCH_VERSION,
                         ne.REMARKS,
                         ne.SHELF_TYPE,
                         ne.SOFTWARE_VERSION,
                         ne.U2000_REF_ID,
                         net.CLASS,
                         net.VENDOR,
                         net.TYPE,
                         net.SERIES,
                         net.TOTAL_SUBRACK_NUMBER
                     }

                     ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATE_DATE"], item.CREATED_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIBER_COUNT"], item.FIBER_COUNT);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["GATEWAY_TYPE"], item.GETWAY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IP"], item.IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LSR_ID"], item.LSR_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MAC_ADDRESS"], item.MAC_ADDRESS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MODEL"], item.MODEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PATCH_VERSION"], item.PATCH_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SHELF_TYPE"], item.SHELF_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["U2000_REF_ID"], item.U2000_REF_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLASS"], item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR"], item.VENDOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIES"], item.SERIES);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_SUBRACK_NUMBER"], item.TOTAL_SUBRACK_NUMBER);
                dictionary.Add(d);
            }

            return dictionary;
        }
        //getMWNEDetails
        public object getMWNEDetails(int Id)
        {

            var q = (from ne in _context.MW_NE_DETAILS
                     //join net in _context.MW_NE_TYPE
                     //on ne.TYPE_ID equals net.ID
                     //join mns in _context.MW_NE_SITE
                     //on ne.ID equals mns.NE_ID
                     //join s in _context.SITE 
                     //on mns.SITE_ID  equals s.ID
                     where ne.ID == Id

                     select new
                     {

                         ne.CLASS,
                         ne.CREATE_DATE,
                         ne.DEPLOYMENT_STATUS,
                         ne.GATEWAY_TYPE,
                         ne.IP,
                         ne.LSR_ID,
                         ne.MAC_ADDRESS,
                         ne.NAME,
                         ne.NE_ID,
                         ne.PATCH_VERSION,
                         ne.REGION,
                         ne.REMARKS,
                         ne.SERIES,
                         ne.SITE_MODEL,
                         ne.SOFTWARE_VERSION,
                         ne.SUBRACK_TYPE,
                         ne.TERMINAL_CLASS,
                         ne.TOTAL_CHILD_NUMBER,
                         ne.TYPE,
                         ne.U2000_REF_ID,
                         ne.VENDOR,
                         ne.CODE,
                         ne.ENGLISH_NAME,
                         ne.ZONE,
                         ne.AREA,
                         ne.SUBAREA

                     }

                     ).ToList();


            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLASS"], item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATE_DATE"], item.CREATE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["GATEWAY_TYPE"], item.GATEWAY_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IP"], item.IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LSR_ID"], item.LSR_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MAC_ADDRESS"], item.MAC_ADDRESS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_ID"], item.NE_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PATCH_VERSION"], item.PATCH_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIES"], item.SERIES);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SITE_MODEL"], item.SITE_MODEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_TYPE"], item.SUBRACK_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TERMINAL_CLASS"], item.TERMINAL_CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_CHILD_NUMBER"], item.TOTAL_CHILD_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["U2000_REF_ID"], item.U2000_REF_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR"], item.VENDOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CODE"], item.CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION"], item.REGION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ZONE"], item.ZONE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA"], item.AREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBAREA"], item.SUBAREA);
                dictionary.Add(d);
            }

            return dictionary;
        }
        // getFirewallNEDetails
        public object getFirewallNEDetails(int Id)
        {

            var q = (from ne in _context.FIREWALL_NE
                     join net in _context.FIREWALL_NE_TYPE
                     on ne.TYPE_ID equals net.ID
                     join fns in _context.FIREWALL_NE_SITE
                     on ne.ID equals fns.NE_ID
                     join s in _context.SITE
                     on fns.SITE_ID equals s.ID
                     where ne.ID == Id

                     select new
                     {
                         ne.ALIAS_NAME,
                         ne.CREATE_DATE,
                         ne.IP,
                         ne.MAC_ADDRESS,
                         ne.NAME,
                         ne.PATCH_VERSION,
                         ne.REMARKS,
                         ne.SOFTWARE_VERSION,
                         ne.U2000_REF_ID,
                         net.CLASS,
                         net.MODEL,
                         net.SERIES,
                         net.TOTAL_SLOTS_NUMBERS,
                         net.VENDOR,
                         s.CODE,
                         s.ENGLISH_NAME,
                         s.REGION,
                         s.AREA,
                         s.SUBAREA,
                         s.ZONE

                     }

                     ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CREATE_DATE"], item.CREATE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IP"], item.IP);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MAC_ADDRESS"], item.MAC_ADDRESS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PATCH_VERSION"], item.PATCH_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["U2000_REF_ID"], item.U2000_REF_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLASS"], item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MODEL"], item.MODEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIES"], item.SERIES);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_SLOTS_NUMBERS"], item.TOTAL_SLOTS_NUMBERS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["VENDOR"], item.VENDOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CODE"], item.CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ENGLISH_NAME"], item.ENGLISH_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REGION"], item.REGION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AREA"], item.AREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBAREA"], item.SUBAREA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ZONE"], item.ZONE);
                dictionary.Add(d);
            }

            return dictionary;

        }


        public object getBoardDetails1(int urefId, int slotId)
        {
            //    var res = _context.DATACOM_NE
            //        .Include(ne => ne.DATACOM_NE_BOARD.Select(s => s.DATACOM_BOARD)
            //        .Where(s => s.slotId == slotId).ToList())
            //        .AsNoTracking()
            //        .Where(ne => ne.refId == urefId).ToList()
            //          ;

            var q = (from ne in _context.DATACOM_NE
                     join nb in _context.DATACOM_NE_BOARD
                     on ne.ID equals nb.PARENT_ID
                     join db in _context.DATACOME_BOARD
                     on nb.CHILD_ID equals db.ID
                     join dbt in _context.DATACOM_BOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where ne.U2000_REF_ID == urefId
                     where db.SLOT_ID == slotId
                     where nb.RETIRE_DATE == null
                     select new
                     {
                         db.SUBRACK_ID,
                         db.SLOT_ID,
                         db.HARDWARE_VERSION,
                         db.SOFTWARE_VERSION,
                         db.SERIAL_NUMBER,
                         db.REMARKS,
                         db.BARCODE,
                         db.BIOS_VERSION,
                         db.FPGA_VERSION,
                         db.STATUS,
                         db.BOM_ITEM,
                         db.DESCRIPTION,
                         db.DEPLOYMENT_STATUS,
                         db.FREE_SUBBOARD,
                         db.MANUFACTURE_DATE,

                         dbt.TYPE,
                         dbt.ALIAS_NAME,
                         dbt.TOTAL_CHILD_NUMBER

                     }

                      ).ToList();



            return q;
        }

        public object getBoardDetails(int Id)
        {

            var q = (from db in _context.DATACOME_BOARD
                     join dbt in _context.DATACOM_BOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id

                     select new
                     {
                         db.SUBRACK_ID,
                         db.SLOT_ID,
                         db.HARDWARE_VERSION,
                         db.SOFTWARE_VERSION,
                         db.SERIAL_NUMBER,
                         db.REMARKS,
                         db.BARCODE,
                         db.BIOS_VERSION,
                         db.FPGA_VERSION,
                         db.STATUS,
                         db.BOM_ITEM,
                         db.DESCRIPTION,
                         db.DEPLOYMENT_STATUS,
                         db.FREE_SUBBOARD,
                         dbt.TYPE,
                         dbt.ALIAS_NAME,
                         dbt.TOTAL_CHILD_NUMBER

                     }

                      ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_ID"], item.SUBRACK_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_ID"], item.SLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VERSION"], item.BIOS_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FPGA_VERSION"], item.FPGA_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["STATUS"], item.STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_ITEM"], item.BOM_ITEM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_SUBBOARD"], item.FREE_SUBBOARD);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_CHILD_NUMBER"], item.TOTAL_CHILD_NUMBER);
                dictionary.Add(d);
            }

            return dictionary;
        }

        public object getSubBoadDetils1(int urefId, int slotId, int subSlotId)
        {
            var subBoards = (from dn in _context.DATACOM_NE
                             join dnb in _context.DATACOM_NE_BOARD
                             on dn.ID equals dnb.PARENT_ID
                             join db in _context.DATACOME_BOARD
                             on dnb.CHILD_ID equals db.ID
                             join dbs in _context.DATACOM_BOARD_SUBBOARD
                             on db.ID equals dbs.PARENT_ID
                             join ds in _context.DATACOM_SUBBOARD
                             on dbs.CHILD_ID equals ds.ID
                             join dst in _context.DATACOM_SUBBOARD_TYPE
                             on ds.TYPE_ID equals dst.ID
                             where dn.U2000_REF_ID == urefId
                             where db.SLOT_ID == slotId
                             where ds.SUBSLOT_ID == subSlotId
                             where dnb.RETIRE_DATE == null
                             where dbs.RETIRE_DATE == null
                             select new
                             {
                                 ds.ID,
                                 ds.TYPE_ID,
                                 ds.SUBSLOT_ID,
                                 ds.HARDWARE_VERSION,
                                 ds.SOFTWARE_VERSION,
                                 ds.SERIAL_NUMBER,
                                 ds.REMARKS,
                                 ds.BARCODE,
                                 ds.STATUS,
                                 ds.MANUFACTURE_DATA,
                                 ds.DEPLOYMENT_STATUS,
                                 ds.FREE_PORT,
                                 ds.IS_VIRTUAL,
                                 ds.DESCRIPTION,
                                 typeId = dst.ID,
                                 dst.ALIAS_NAME,
                                 dst.TYPE,
                                 dst.TOTAL_CHILD_NUMBER

                             }).ToList();
            return subBoards;
        }
        public object getSubBoadDetils(int Id)
        {
            var subBoards = (from ds in _context.DATACOM_SUBBOARD
                             join dst in _context.DATACOM_SUBBOARD_TYPE
                             on ds.TYPE_ID equals dst.ID
                             where ds.ID == Id
                             select new
                             {
                                 //ds.ID,
                                 //ds.TYPE_ID,
                                 ds.SUBSLOT_ID,
                                 ds.HARDWARE_VERSION,
                                 ds.SOFTWARE_VERSION,
                                 ds.SERIAL_NUMBER,
                                 ds.REMARKS,
                                 ds.BARCODE,
                                 ds.STATUS,
                                 ds.MANUFACTURE_DATA,
                                 ds.DEPLOYMENT_STATUS,
                                 ds.FREE_PORT,
                                 ds.IS_VIRTUAL,
                                 ds.DESCRIPTION,
                                 //typeId = dst.ID,
                                 dst.ALIAS_NAME,
                                 dst.TYPE,
                                 dst.TOTAL_CHILD_NUMBER

                             }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in subBoards)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBSLOT_ID"], item.SUBSLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["STATUS"], item.STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATA);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_PORT"], item.FREE_PORT);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IS_VIRTUAL"], item.IS_VIRTUAL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_CHILD_NUMBER"], item.TOTAL_CHILD_NUMBER);
                dictionary.Add(d);
            }

            return dictionary;
        }
        public object getPortDetails1(int urefId, int slotId, int subSlotId, int portId)
        {
            var ports = (from dn in _context.DATACOM_NE
                         join dnb in _context.DATACOM_NE_BOARD
                         on dn.ID equals dnb.PARENT_ID
                         join db in _context.DATACOME_BOARD
                         on dnb.CHILD_ID equals db.ID
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on db.ID equals dbs.PARENT_ID
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.CHILD_ID equals ds.ID
                         join dst in _context.DATACOM_SUBBOARD_TYPE
                         on ds.TYPE_ID equals dst.ID
                         join p in _context.DATACOM_PORT
                         on ds.ID equals p.PARENT_ID
                         join pt in _context.DATACOM_PORT_TYPE
                         on p.PORT_ID equals pt.ID
                         where dn.U2000_REF_ID == urefId
                         where db.SLOT_ID == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dnb.RETIRE_DATE == null
                         where dbs.RETIRE_DATE == null
                         select new
                         {
                             p.PORT_ID,
                             p.PORT_LEVEL,
                             p.PORT_NAME,
                             p.RATE_BPS,
                             p.SUB_TYPE,
                             p.DESCRIPTION,
                             p.DEPLOYMENT_STATUS,
                             pt.TYPE

                         }).ToList();
            return ports;
        }
        public object getPortDetails(int Id)
        {
            var ports = (from p in _context.DATACOM_PORT
                         join pt in _context.DATACOM_PORT_TYPE
                         on p.TYPE_ID equals pt.ID
                         where p.ID == Id
                         select new
                         {
                             p.PORT_ID,
                             p.PORT_LEVEL,
                             p.PORT_NAME,
                             p.RATE_BPS,
                             p.SUB_TYPE,
                             p.DESCRIPTION,
                             p.DEPLOYMENT_STATUS,
                             pt.TYPE
                         }).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in ports)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_ID"], item.PORT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_LEVEL"], item.PORT_LEVEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NAME"], item.PORT_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RATE_BPS"], item.RATE_BPS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUB_TYPE"], item.SUB_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }
        public object getSFPDetails1(int urefId, int slotId, int subSlotId, int portId)
        {
            var ports = (from dn in _context.DATACOM_NE
                         join dnb in _context.DATACOM_NE_BOARD
                         on dn.ID equals dnb.PARENT_ID
                         join db in _context.DATACOME_BOARD
                         on dnb.CHILD_ID equals db.ID
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on db.ID equals dbs.PARENT_ID
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.CHILD_ID equals ds.ID
                         join dst in _context.DATACOM_SUBBOARD_TYPE
                         on ds.TYPE_ID equals dst.ID
                         join p in _context.DATACOM_PORT
                         on ds.ID equals p.PARENT_ID
                         join dps in _context.DATACOM_PORT_SFP
                         on p.ID equals dps.PARENT_ID
                         join dsf in _context.DATACOM_SFP
                         on dps.CHILD_ID equals dsf.ID
                         where dn.U2000_REF_ID == urefId
                         where db.SLOT_ID == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dnb.RETIRE_DATE == null
                         where dbs.RETIRE_DATE == null
                         where dps.RETIRE_DATE == null
                         select new
                         {
                             dsf.LOWER_RECEIVE_POWER,
                             dsf.LOWER_TRANSMIT_POWER,
                             dsf.MANUFACTURE,
                             dsf.RECEIVE_POWER,
                             dsf.REFERENCE_RECEIVE_POWER,
                             dsf.REFERENCE_RECEIVE_TIME,
                             dsf.REFERENCE_TRANSMIT_POWER,
                             dsf.REFERENCE_TRANSMIT_TIME,
                             dsf.SERIAL_NUMBER,
                             dsf.SM_MM,
                             dsf.SPEED,
                             dsf.TRANSMISSION_DISTANCE,
                             dsf.TRANSMIT_POWER,
                             dsf.TYPE,
                             dsf.UPPER_RECEIVE_POWER,
                             dsf.UPPER_TRANSMIT_POWER,
                             dsf.WAVELENGTH,
                         }).ToList();
            return ports;
        }
        public object getSFPDetails(int Id)
        {
            var ports = (from dsf in _context.DATACOM_SFP

                         where dsf.ID == Id

                         select new
                         {
                             dsf.LOWER_RECEIVE_POWER,
                             dsf.LOWER_TRANSMIT_POWER,
                             dsf.MANUFACTURE,
                             dsf.RECEIVE_POWER,
                             dsf.REFERENCE_RECEIVE_POWER,
                             dsf.REFERENCE_RECEIVE_TIME,
                             dsf.REFERENCE_TRANSMIT_POWER,
                             dsf.REFERENCE_TRANSMIT_TIME,
                             dsf.SERIAL_NUMBER,
                             dsf.SM_MM,
                             dsf.SPEED,
                             dsf.TRANSMISSION_DISTANCE,
                             dsf.TRANSMIT_POWER,
                             dsf.TYPE,
                             dsf.UPPER_RECEIVE_POWER,
                             dsf.UPPER_TRANSMIT_POWER,
                             dsf.WAVELENGTH,
                         }).ToList();//FirstOrDefault

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in ports)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LOWER_RECEIVE_POWER"], item.LOWER_RECEIVE_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["LOWER_TRANSMIT_POWER"], item.LOWER_TRANSMIT_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE"], item.MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RECEIVE_POWER"], item.RECEIVE_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REFERENCE_RECEIVE_POWER"], item.REFERENCE_RECEIVE_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REFERENCE_RECEIVE_TIME"], item.REFERENCE_RECEIVE_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REFERENCE_TRANSMIT_POWER"], item.REFERENCE_TRANSMIT_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REFERENCE_TRANSMIT_TIME"], item.REFERENCE_TRANSMIT_TIME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SM_MM"], item.SM_MM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SPEED"], item.SPEED);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TRANSMISSION_DISTANCE"], item.TRANSMISSION_DISTANCE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TRANSMIT_POWER"], item.TRANSMIT_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UPPER_RECEIVE_POWER"], item.UPPER_RECEIVE_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["UPPER_TRANSMIT_POWER"], item.UPPER_TRANSMIT_POWER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["WAVELENGTH"], item.WAVELENGTH);
                dictionary.Add(d);

            }

            return dictionary;
        }

        //getOpticalSubRackDetails
        public object getOpticalSubRackDetails(int Id)
        {

            var q = (from db in _context.OPTICAL_SUBRACK
                     join dbt in _context.OPTICAL_SUBRACK_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.ALIAS,
                         db.DEPLOYMENT_STATUS,
                         db.FREE_BOARD,
                         db.NAME,
                         db.RACK_ID,
                         db.REMARKS,
                         db.SOFTWARE_VERSION,
                         dbt.AVAILABLE_SLOTS_FOR_SERVICES_BOARDS,
                         dbt.TOTAL_SLOTS_NUMBER,
                         dbt.TYPE
                     }

                      ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS"], item.ALIAS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_BOARD"], item.FREE_BOARD);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RACK_ID"], item.RACK_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["AVAILABLE_SLOTS_FOR_SERVICES_BOARDS"], item.AVAILABLE_SLOTS_FOR_SERVICES_BOARDS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_SLOTS_NUMBER"], item.TOTAL_SLOTS_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getOpticalBoardDetails
        public object getOpticalBoardDetails(int Id)
        {

            var q = (from db in _context.OPTICAL_BOARD
                     join dbt in _context.OPTICAL_BOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.BARCODE,
                         db.BIOS_VERSION,
                         db.BOM_ITEM,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.FPGA_VERSION,
                         db.FREE_PORT,
                         db.HARDWARE_VERSION,
                         db.MANUFACTURE_DATE,
                         db.NE_ID,
                         db.REMARKS,
                         db.SERIAL_NUMBER,
                         db.SLOT_ID,
                         db.SOFTWARE_VERSION,
                         db.STATUS,
                         dbt.ALIAS,
                         dbt.NAME,
                         dbt.RESERVED_SLOTS,
                         dbt.TOTAL_PORTS_NUMBER,
                         dbt.TYPE
                     }

                      ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VERSION"], item.BIOS_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_ITEM"], item.BOM_ITEM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FPGA_VERSION"], item.FPGA_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_PORT"], item.FREE_PORT);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NE_ID"], item.NE_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_ID"], item.SLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["STATUS"], item.STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS"], item.ALIAS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RESERVED_SLOTS"], item.RESERVED_SLOTS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_PORTS_NUMBER"], item.TOTAL_PORTS_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }
        //getOpticalPortDetails
        public object getOpticalPortDetails(int Id)
        {

            var q = (from db in _context.OPTICAL_PORT
                     join dbt in _context.OPTICAL_PORT_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.PORT_ID,
                         db.ALIAS,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.FIXED_ATTENUATOR_DB,
                         db.FIXED_OPTICAL_ATTENUATOR,
                         db.PORT_LEVEL,
                         db.PORT_NAME,
                         db.RATE_BPS,
                         db.REMARKS,
                         dbt.SUBTYPE,
                         dbt.TYPE
                     }

                      ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_ID"], item.PORT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS"], item.ALIAS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIXED_ATTENUATOR_DB"], item.FIXED_ATTENUATOR_DB);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIXED_OPTICAL_ATTENUATOR"], item.FIXED_OPTICAL_ATTENUATOR);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_LEVEL"], item.PORT_LEVEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NAME"], item.PORT_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RATE_BPS"], item.RATE_BPS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBTYPE"], item.SUBTYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }
        //getOpticalSFPDetails
        public object getOpticalSFPDetails(int Id)
        {

            var q = (from db in _context.OPTICAL_SFP
                     //join dbt in _context.optical_
                     //on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.BOM_CODES,
                         db.CLEI_CODE,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.FIBER_CABLE_TYPE,
                         db.MANUFACTURE,
                         db.MANUFACTURE_DATE,
                         db.SERIAL_NUMBER,
                         db.TYPE
                     }).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODES"], item.BOM_CODES);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLEI_CODE"], item.CLEI_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIBER_CABLE_TYPE"], item.FIBER_CABLE_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE"], item.MANUFACTURE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getMWBoardDetails
        public object getMWBoardDetails(int Id)
        {

            var q = (from db in _context.MW_BOARD
                     join dbt in _context.MW_BOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.BARCODE,
                         db.BIOS_VERSION,
                         db.BOM_ITEM,
                         db.DESCRIPTION,
                         db.FPGA_VERSION,
                         db.HARDWARE_VERSION,
                         db.MANUFACTURE_DATE,
                         db.REMARK,
                         db.SERIAL_NUMBER,
                         db.SLOT_ID,
                         db.SOFTWARE_VERSION,
                         dbt.NAME,
                         dbt.TYPE
                     }

                      ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VERSION"], item.BIOS_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_ITEM"], item.BOM_ITEM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FPGA_VERSION"], item.FPGA_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARK);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_ID"], item.SLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["NAME"], item.NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getMWPortDetails
        public object getMWPortDetails(int Id)
        {

            var q = (from db in _context.MW_PORT
                     join dbt in _context.MW_PORT_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     where dbt.ID != null
                     select new
                     {
                         db.PORT_ID,
                         db.DEPLOYMENT_STATUS,
                         db.PORT_LEVEL,
                         db.PORT_NAME,
                         db.RATE_BPS,
                         db.REMARK,
                         dbt.TYPE
                     }

                      ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_ID"], item.PORT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_LEVEL"], item.PORT_LEVEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NAME"], item.PORT_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RATE_BPS"], item.RATE_BPS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARK);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        ////getMWSFPDetails
        public object getMWSFPDetails(int Id)
        {

            var q = (from db in _context.MW_SFP
                     //join dbt in _context.MW_PORT_TYPE
                     //on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.BOM_CODE,
                         db.CLEI_CODE,
                         db.DESCRIPTION,
                         db.FIBER_CABLE_TYPE,
                         db.MANUFACTURE_DATE,
                         db.MANUFACTURER,
                         db.SERIAL_NUMBER,
                         db.TYPE
                     }

                      ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_CODES"], item.BOM_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["CLEI_CODE"], item.CLEI_CODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FIBER_CABLE_TYPE"], item.FIBER_CABLE_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURER"], item.MANUFACTURER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getFirewallBoardDetails
        public object getFirewallBoardDetails(int Id)
        {

            var q = (from db in _context.FIREWALL_BOARD
                     join dbt in _context.FIREWALL_BOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.BARCODE,
                         db.BIOS_VERSION,
                         db.BOM_ITEM,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.FPGA_VERSION,
                         db.HARDWARE_VERSION,
                         db.MANUFACTURE_DATE,
                         db.REMARKS,
                         db.SERIAL_NUMBER,
                         db.SLOT_ID,
                         db.SOFTWARE_VERSION,
                         db.STATUS,
                         db.SUBRACK_ID,
                         dbt.ALIAS_NAME,
                         dbt.TOTAL_SUBSLOTS_NUMBER,
                         dbt.TYPE
                     }

                      ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BIOS_VERSION"], item.BIOS_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_ITEM"], item.BOM_ITEM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FPGA_VERSION"], item.FPGA_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SLOT_ID"], item.SLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["STATUS"], item.STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBRACK_ID"], item.SUBRACK_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.ALIAS_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_SUBSLOTS_NUMBER"], item.TOTAL_SUBSLOTS_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getFirewallSubBoardDetails
        public object getFirewallSubBoardDetails(int Id)
        {

            var q = (from db in _context.FIREWALL_SUBBOARD
                     join dbt in _context.FIREWALL_SUBBOARD_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.ALIAS_NAME,
                         db.BARCODE,
                         db.BOM_ITEM,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.FREE_PORT,
                         db.HARDWARE_VERSION,
                         db.IS_VIRTUAL,
                         db.MANUFACTURE_DATE,
                         db.REMARKS,
                         db.SERIAL_NUMBER,
                         db.SOFTWARE_VERSION,
                         db.STATUS,
                         db.SUBSLOT_ID,
                         ALIAS_NAMEType = dbt.ALIAS_NAME,
                         dbt.PORT_TYPE,
                         dbt.TOTAL_PORTS_NUMBER,
                         dbt.TYPE,

                     }

                      ).ToList();
            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                //d.Add(ConfigurationFile.CLASS, item.CLASS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAME"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BARCODE"], item.BARCODE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["BOM_ITEM"], item.BOM_ITEM);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["FREE_PORT"], item.FREE_PORT);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["HARDWARE_VERSION"], item.HARDWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["IS_VIRTUAL"], item.IS_VIRTUAL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["MANUFACTURE_DATE"], item.MANUFACTURE_DATE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["REMARKS"], item.REMARKS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SERIAL_NUMBER"], item.SERIAL_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SOFTWARE_VERSION"], item.SOFTWARE_VERSION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["STATUS"], item.STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["SUBSLOT_ID"], item.SUBSLOT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["ALIAS_NAMEType"], item.ALIAS_NAMEType);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_TYPE"], item.PORT_TYPE);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TOTAL_PORTS_NUMBER"], item.TOTAL_PORTS_NUMBER);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }

        //getFirewallPortDetails
        public object getFirewallPortDetails(int Id)
        {

            var q = (from db in _context.FIREWALL_PORT
                     join dbt in _context.FIREWALL_PORT_TYPE
                     on db.TYPE_ID equals dbt.ID
                     where db.ID == Id
                     select new
                     {
                         db.PORT_ID,
                         db.DEPLOYMENT_STATUS,
                         db.DESCRIPTION,
                         db.PORT_LEVEL,
                         db.PORT_NAME,
                         db.RATE_BPS,
                         dbt.TYPE

                     }

                      ).ToList();

            List<Dictionary<string, object>> dictionary = new List<Dictionary<string, object>>();
            foreach (var item in q)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_ID"], item.PORT_ID);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DEPLOYMENT_STATUS"], item.DEPLOYMENT_STATUS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["DESCRIPTION"], item.DESCRIPTION);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_LEVEL"], item.PORT_LEVEL);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["PORT_NAME"], item.PORT_NAME);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["RATE_BPS"], item.RATE_BPS);
                d.Add(System.Configuration.ConfigurationManager.AppSettings["TYPE"], item.TYPE);
                dictionary.Add(d);
            }

            return dictionary;
        }
        public List<Instance> getAllNEsForSubCategory(int CategoryId, int SubCategoryId)
        {

            List<Instance> NeValues = null;
            if (CategoryId == 1)
            {
                List<DATACOM_NE> DataComNE = new List<DATACOM_NE>();
                Dictionary<int, string> NEDetails = new Dictionary<int, string>();

                var routers = (from c in _context.RIM_CATEGORY
                               join s in _context.RIM_SUBCATEGORY
                                   on c.Id equals s.CATEGORY_ID
                               join ne in _context.DATACOM_NE
                               on s.Id equals ne.SUB_CATEGORY_ID
                               where c.Id == CategoryId
                               where s.Id == SubCategoryId
                               select new
                               {
                                   ne.ID,
                                   ne.NAME
                               }
                                ).ToList();
                NeValues = new List<Instance>();
                if (routers.Count > 0 && routers != null)
                {
                    foreach (var obj in routers)
                    {
                        if (NeValues == null)
                            NeValues = new List<Instance>();

                        Instance instance = new Instance();
                        instance.Id = obj.ID;
                        instance.Name = obj.NAME;

                        NeValues.Add(instance);
                    }
                }
            }
            //}
            //else if (CategoryId == 2)
            //{
            //    List<OPTICAL_NE> OpticalNE = new List<OPTICAL_NE>();
            //    Dictionary<int, string> NEDetails = new Dictionary<int, string>();

            //    var routers = (from c in _context.RIM_CATEGORY
            //                   join s in _context.RIM_SUBCATEGORY
            //                       on c.Id equals s.categoryId
            //                   join ne in _context.OPTICAL_NE
            //                   on s.Id equals ne.SUB_CATEGORY_ID
            //                   where c.Id == CategoryId
            //                   where s.Id == SubCategoryId
            //                   select new
            //                   {
            //                       ne.ID,
            //                       ne.NAME
            //                   }
            //                    ).ToList();

            //    NeValues = new List<Instance>();
            //    foreach (var obj in routers)
            //    {
            //        if (NeValues == null)
            //            NeValues = new List<Instance>();

            //        Instance instance = new Instance();
            //        instance.Id = obj.ID;
            //        instance.Name = obj.NAME;

            //        NeValues.Add(instance);
            //    }
            //}
            //else if (CategoryId == 3)
            //{
            //    List<MW_NE> MWNE = new List<MW_NE>();
            //    Dictionary<int, string> NEDetails = new Dictionary<int, string>();

            //    var routers = (from c in _context.RIM_CATEGORY
            //                   join s in _context.RIM_SUBCATEGORY
            //                       on c.Id equals s.categoryId
            //                   join ne in _context.MW_NE
            //                   on s.Id equals ne.SUB_CATEGORY_ID
            //                   where c.Id == CategoryId
            //                   where s.Id == SubCategoryId
            //                   select new
            //                   {
            //                       ne.ID,
            //                       ne.NAME
            //                   }
            //                    ).ToList();

            //    NeValues = new List<Instance>();
            //    foreach (var obj in routers)
            //    {
            //        if (NeValues == null)
            //            NeValues = new List<Instance>();

            //        Instance instance = new Instance();
            //        instance.Id = obj.ID;
            //        instance.Name = obj.NAME;

            //        NeValues.Add(instance);
            //    }
            //}
            //else if (CategoryId == 4)
            //{
            //    List<FIREWALL_NE> FIREWALLNE = new List<FIREWALL_NE>();
            //    Dictionary<int, string> NEDetails = new Dictionary<int, string>();

            //    var routers = (from c in _context.RIM_CATEGORY
            //                   join s in _context.RIM_SUBCATEGORY
            //                       on c.Id equals s.categoryId
            //                   join ne in _context.FIREWALL_NE
            //                   on s.Id equals ne.SUB_CATEGORY_ID
            //                   where c.Id == CategoryId
            //                   where s.Id == SubCategoryId
            //                   select new
            //                   {
            //                       ne.ID,
            //                       ne.NAME
            //                   }
            //                    ).ToList();

            //    NeValues = new List<Instance>();
            //    foreach (var obj in routers)
            //    {
            //        if (NeValues == null)
            //            NeValues = new List<Instance>();

            //        Instance instance = new Instance();
            //        instance.Id = obj.ID;
            //        instance.Name = obj.NAME;

            //        NeValues.Add(instance);
            //    }
            //}
            return NeValues;

        }
        //OpticalNES
        public List<Instance> getAllOpticalNEsForSubCategory(int CategoryId, int SubCategoryId)
        {
            List<Instance> NeValues = null;
            List<OPTICAL_NE> OpticalNE = new List<OPTICAL_NE>();
            Dictionary<int, string> NEDetails = new Dictionary<int, string>();
            if (CategoryId == 2)
            {
                var routers = (from c in _context.RIM_CATEGORY
                               join s in _context.RIM_SUBCATEGORY
                                   on c.Id equals s.CATEGORY_ID
                               join ne in _context.OPTICAL_NE
                               on s.Id equals ne.SUB_CATEGORY_ID
                               where c.Id == CategoryId
                               where s.Id == SubCategoryId
                               select new
                               {
                                   ne.ID,
                                   ne.NAME
                               }
                                ).ToList();

                NeValues = new List<Instance>();
                if (routers.Count > 0 && routers != null)
                {
                    foreach (var obj in routers)
                    {
                        if (NeValues == null)
                            NeValues = new List<Instance>();

                        Instance instance = new Instance();
                        instance.Id = obj.ID;
                        instance.Name = obj.NAME;

                        NeValues.Add(instance);
                    }
                }
            }
            return NeValues;

        }
        public List<Instance> getAllMWNEsForSubCategory(int CategoryId, int SubCategoryId)
        {
            List<Instance> NeValues = null;
            List<MW_NE> MWNE = new List<MW_NE>();
            if (CategoryId == 3)
            {

                Dictionary<int, string> NEDetails = new Dictionary<int, string>();
                var routers = (from c in _context.RIM_CATEGORY
                               join s in _context.RIM_SUBCATEGORY
                                   on c.Id equals s.CATEGORY_ID
                               join ne in _context.MW_NE
                               on s.Id equals ne.SUB_CATEGORY_ID
                               where c.Id == CategoryId
                               where s.Id == SubCategoryId
                               select new
                               {
                                   ne.ID,
                                   ne.NAME
                               }
                               ).ToList();


                NeValues = new List<Instance>();
                if (routers.Count > 0 && routers != null)
                {
                    foreach (var obj in routers)
                    {
                        if (NeValues == null)
                            NeValues = new List<Instance>();

                        Instance instance = new Instance();
                        instance.Id = obj.ID;
                        instance.Name = obj.NAME;

                        NeValues.Add(instance);
                    }
                }
            }
            return NeValues;
        }
        public List<Instance> getAllFirewallNEsForSubCategory(int CategoryId, int SubCategoryId)
        {
            List<Instance> NeValues = null;
            List<MW_NE> MWNE = new List<MW_NE>();
            if (CategoryId == 4)
            {

                Dictionary<int, string> NEDetails = new Dictionary<int, string>();


                var routers = (from c in _context.RIM_CATEGORY
                               join s in _context.RIM_SUBCATEGORY
                                   on c.Id equals s.CATEGORY_ID
                               join ne in _context.FIREWALL_NE
                               on s.Id equals ne.SUB_CATEGORY_ID
                               where c.Id == CategoryId
                               where s.Id == SubCategoryId
                               select new
                               {
                                   ne.ID,
                                   ne.NAME
                               }
                       ).ToList();

                NeValues = new List<Instance>();
                if (routers.Count > 0 && routers != null)
                {
                    foreach (var obj in routers)
                    {
                        if (NeValues == null)
                            NeValues = new List<Instance>();

                        Instance instance = new Instance();
                        instance.Id = obj.ID;
                        instance.Name = obj.NAME;

                        NeValues.Add(instance);
                    }
                }
            }
            return NeValues;
        }


        public object getAllBoardsForNE(string NEName)
        {

            var Boards = (from ne in _context.DATACOM_NE
                          join nb in _context.DATACOM_NE_BOARD
                          on ne.ID equals nb.PARENT_ID
                          join b in _context.DATACOME_BOARD
                          on nb.CHILD_ID equals b.ID
                          where ne.NAME == NEName
                          where nb.RETIRE_DATE == null
                          select new
                          {
                              b.ID,
                              b.SLOT_ID
                          }).ToList();




            return Boards;


        }
        public object getAllSUBBoardsForNE(string NEName, int slotId)
        {

            var SUBBoards = (from ne in _context.DATACOM_NE
                             join nb in _context.DATACOM_NE_BOARD
                             on ne.ID equals nb.PARENT_ID
                             join b in _context.DATACOME_BOARD
                             on nb.CHILD_ID equals b.ID
                             join dbs in _context.DATACOM_BOARD_SUBBOARD
                             on b.ID equals dbs.PARENT_ID
                             join ds in _context.DATACOM_SUBBOARD
                             on dbs.CHILD_ID equals ds.ID
                             where ne.NAME == NEName
                             where b.SLOT_ID == slotId
                             where dbs.RETIRE_DATE == null
                             where nb.RETIRE_DATE == null
                             select new
                             {
                                 ds.ID,
                                 ds.SUBSLOT_ID
                             }).ToList();

            return SUBBoards;

        }
        public object getAllPortsForSubBoard(string NEName, int slotId, int subSlotId)
        {
            var ports = (from ne in _context.DATACOM_NE
                         join nb in _context.DATACOM_NE_BOARD
                         on ne.ID equals nb.PARENT_ID
                         join b in _context.DATACOME_BOARD
                         on nb.CHILD_ID equals b.ID
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on b.ID equals dbs.PARENT_ID
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.CHILD_ID equals ds.ID
                         join dp in _context.DATACOM_PORT
                         on ds.ID equals dp.PARENT_ID
                         where ne.NAME == NEName
                         where b.SLOT_ID == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dbs.RETIRE_DATE == null
                         where nb.RETIRE_DATE == null
                         select new
                         {
                             dp.ID,
                             dp.PORT_ID,
                             dp.PORT_NAME
                         }).ToList();
            return ports;
        }
        public object getAllPortsSFP(string NEName, int slotId, int subSlotId, int portId)
        {
            var portsSFP = (from ne in _context.DATACOM_NE
                            join nb in _context.DATACOM_NE_BOARD
                            on ne.ID equals nb.PARENT_ID
                            join b in _context.DATACOME_BOARD
                            on nb.CHILD_ID equals b.ID
                            join dbs in _context.DATACOM_BOARD_SUBBOARD
                            on b.ID equals dbs.PARENT_ID
                            join ds in _context.DATACOM_SUBBOARD
                            on dbs.CHILD_ID equals ds.ID
                            join dp in _context.DATACOM_PORT
                            on ds.ID equals dp.PARENT_ID
                            join dpsf in _context.DATACOM_PORT_SFP
                            on dp.ID equals dpsf.PARENT_ID
                            join dsf in _context.DATACOM_SFP
                            on dpsf.CHILD_ID equals dsf.ID
                            where ne.NAME == NEName
                            where b.SLOT_ID == slotId
                            where ds.SUBSLOT_ID == subSlotId
                            where dbs.RETIRE_DATE == null
                            where dpsf.RETIRE_DATE == null
                            where nb.RETIRE_DATE == null
                            select new
                            {
                                dsf.ID,
                                dsf.SPEED,
                                dsf.SERIAL_NUMBER,

                            }).ToList();

            return portsSFP;
        }

        public List<Instance> getAllNEs()
        {
            var NEs = (from ne in _context.DATACOM_NE

                       select new
                       {
                           ne.ID,
                           ne.NAME,
                           ne.SUB_CATEGORY_ID
                       }).ToList();

            List<Instance> NeValues = new List<Instance>();
            foreach (var obj in NEs)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.NAME;
                instance.ParentId = (int)obj.SUB_CATEGORY_ID;

                NeValues.Add(instance);
            }

            return NeValues;
        }

        public BoardInstances getAllBoards()
        {
            var Boards = (from board in _context.DATACOME_BOARD

                          select new
                          {
                              board.ID,
                              board.SLOT_ID
                          }).ToList();

            BoardInstances BoardValues = new BoardInstances();
            BoardValues.TableName = "DATACOM_NE";
            foreach (var obj in Boards)
            {
                if (BoardValues.Boards == null)
                    BoardValues.Boards = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SLOT_ID.ToString();

                BoardValues.Boards.Add(instance);
            }

            return BoardValues;
        }

        public SubBoardInstances getAllSubBoards()
        {
            var SubBoards = (from subboard in _context.DATACOM_SUBBOARD

                             select new
                             {
                                 subboard.ID,
                                 subboard.SUBSLOT_ID
                             }).ToList();

            SubBoardInstances SubBoardValues = new SubBoardInstances();
            SubBoardValues.TableName = "DATACOM_NE";
            foreach (var obj in SubBoards)
            {
                if (SubBoardValues.SubBoards == null)
                    SubBoardValues.SubBoards = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SUBSLOT_ID.ToString();

                SubBoardValues.SubBoards.Add(instance);
            }

            return SubBoardValues;
        }

        public PortInstances getAllPorts()
        {
            var Ports = (from ports in _context.DATACOM_PORT

                         select new
                         {
                             ports.ID,
                             ports.PORT_ID
                         }).ToList();

            PortInstances PortValues = new PortInstances();
            PortValues.TableName = "DATACOM_NE";
            foreach (var obj in Ports)
            {
                if (PortValues.Ports == null)
                    PortValues.Ports = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.PORT_ID.ToString();

                PortValues.Ports.Add(instance);
            }


            return PortValues;
        }

        public SFPInstances getAllSFPs()
        {
            var SFPs = (from sfp in _context.DATACOM_SFP

                        select new
                        {
                            sfp.ID,
                            sfp.SERIAL_NUMBER
                        }).ToList();

            SFPInstances SFPValues = new SFPInstances();
            SFPValues.TableName = "DATACOM_NE";
            foreach (var obj in SFPs)
            {
                if (SFPValues.SFPs == null)
                    SFPValues.SFPs = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SERIAL_NUMBER.ToString();

                SFPValues.SFPs.Add(instance);
            }


            return SFPValues;
        }

        public List<Instance> getAllBoardsByNE(int NeId)
        {
            List<Instance> NeValues = null;
            var Boards = (from ne in _context.DATACOM_NE
                          join nb in _context.DATACOM_NE_BOARD
                          on ne.ID equals nb.PARENT_ID
                          join b in _context.DATACOME_BOARD
                          on nb.CHILD_ID equals b.ID
                          where ne.ID == NeId
                          where nb.RETIRE_DATE == null
                          select new
                          {
                              b.ID,
                              b.SLOT_ID,
                              b.SERIAL_NUMBER
                          }).ToList();
            NeValues = new List<Instance>();
            foreach (var obj in Boards)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.SLOT_ID;
                instance.DisName = obj.SERIAL_NUMBER;


                NeValues.Add(instance);
            }
            return NeValues;
        }

        //OpticalSubRackByNE
        public List<Instance> getAllOpticalSubRackByNE(int NeId)
        {
            List<Instance> NeValues = null;
            var Boards = (from ne in _context.OPTICAL_NE
                          join nb in _context.OPTICAL_NE_SUBRACK
                          on ne.ID equals nb.NE_ID
                          join b in _context.OPTICAL_SUBRACK
                          on nb.SUBRACK_ID equals b.ID
                          where ne.ID == NeId
                          where nb.RETIRE_DATE == null
                          select new
                          {
                              b.ID,
                              b.ALIAS,
                              b.NAME
                          }).ToList();
            NeValues = new List<Instance>();
            foreach (var obj in Boards)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.DisName = obj.ALIAS;
                instance.Name = obj.NAME;

                NeValues.Add(instance);
            }
            return NeValues;
        }

        //FirewallBoardByNE
        public List<Instance> getAllFirewallBoardByNE(int NeId)
        {
            List<Instance> NeValues = null;
            var Boards = (from ne in _context.FIREWALL_NE
                          join nb in _context.FIREWALL_NE_BOARD
                          on ne.ID equals nb.PARENT_ID
                          join b in _context.FIREWALL_BOARD
                          on nb.CHILD_ID equals b.ID
                          where ne.ID == NeId
                          where nb.RETIRE_DATE == null
                          select new
                          {
                              b.ID,
                              b.SLOT_ID,
                              b.SERIAL_NUMBER
                          }).ToList();
            NeValues = new List<Instance>();
            foreach (var obj in Boards)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.SLOT_ID;
                instance.Name = obj.SERIAL_NUMBER;

                NeValues.Add(instance);
            }
            return NeValues;
        }

        //MWBoardByNE
        public List<Instance> getAllMWBoardByNE(int NeId)
        {
            List<Instance> NeValues = null;
            var Boards = (from ne in _context.MW_NE
                          join nb in _context.MW_NE_BOARD
                          on ne.ID equals nb.PARENT_ID
                          join b in _context.MW_BOARD
                          on nb.CHILD_ID equals b.ID
                          where ne.ID == NeId
                          where nb.RETIRE_DATE == null
                          select new
                          {
                              b.ID,
                              b.SLOT_ID,
                              b.SERIAL_NUMBER
                          }).ToList();
            NeValues = new List<Instance>();
            foreach (var obj in Boards)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SLOT_ID;
                instance.DisName = obj.SERIAL_NUMBER;


                NeValues.Add(instance);
            }
            return NeValues;
        }
        public List<Instance> getAllSubBoardsByBoard(int BoardId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from board in _context.DATACOME_BOARD
                             join bs in _context.DATACOM_BOARD_SUBBOARD
                             on board.ID equals bs.PARENT_ID
                             join s in _context.DATACOM_SUBBOARD
                             on bs.CHILD_ID equals s.ID
                             where board.ID == BoardId
                             where bs.RETIRE_DATE == null
                             select new
                             {
                                 s.ID,
                                 s.SUBSLOT_ID
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.SUBSLOT_ID;

                BValues.Add(instance);
            }
            return BValues;
        }

        public List<Instance> getAllSubBoardsByNE(int NeId)
        {
            List<Instance> BValues = null;
            //var SubBoards = _context.RouterSubBoards
            //   .Where(r => r.NE_ID == NeId)
            //  . ToList();

            var SubBoards = (from RS in _context.RouterSubBoards
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.SUBSLOT_ID,
                                 RS.SERIAL_NUMBER
                             }).ToList();


            //List<Instance> BValues = null;
            //var SubBoards = (from ne in _context.DATACOM_NE
            //                 join nb in _context.DATACOM_NE_BOARD
            //                 on ne.Id equals nb.parentId
            //                 join b in _context.DATACOME_BOARD
            //                 on nb.childId equals b.Id
            //                 join bs in _context.DATACOM_BOARD_SUBBOARD
            //                 on b.Id equals bs.parentId
            //                  join s in _context.DATACOM_SUBBOARD                           
            //                 on bs.childId equals s.ID
            //                 where ne.Id == NeId
            //                 where bs.retireDate == null
            //                 where nb.retireDate == null
            //                 select new
            //                 {
            //                     s.ID,
            //                     s.SUBSLOT_ID
            //                 }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.SUBSLOT_ID;
                instance.Name = obj.SERIAL_NUMBER;

                BValues.Add(instance);
            }
            return BValues;
        }
        public List<Instance> getAllPortsBySubBoard(int SubBoardId)
        {
            List<Instance> SValues = null;
            var Ports = (from ports in _context.DATACOM_PORT
                         where ports.PARENT_ID == SubBoardId
                         select new
                         {
                             ports.ID,
                             ports.PORT_NAME
                         }).ToList();
            SValues = new List<Instance>();
            foreach (var obj in Ports)
            {
                if (SValues == null)
                    SValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.PORT_NAME;

                SValues.Add(instance);
            }
            return SValues;
        }

        public List<Instance> getAllPortsByNE(int NeId)
        {
            List<Instance> SValues = null;
            var Ports = (from RP in _context.ROUTER_PORTS
                         where RP.NE_ID == NeId
                         select new
                         {
                             RP.ID,
                             RP.PORT_ID,
                             RP.PORT_NAME
                         }).ToList();
            //var Ports = (from ne in _context.DATACOM_NE
            //             join nb in _context.DATACOM_NE_BOARD
            //             on ne.Id equals nb.parentId
            //             join b in _context.DATACOME_BOARD
            //             on nb.childId equals b.Id
            //             join bs in _context.DATACOM_BOARD_SUBBOARD
            //             on b.Id equals bs.parentId
            //             join s in _context.DATACOM_SUBBOARD
            //             on bs.childId equals s.ID
            //             join p in _context.DATACOM_PORT
            //             on s.ID equals p.PARENT_ID
            //             where ne.Id == NeId
            //             where bs.retireDate == null
            //             where nb.retireDate == null
            //             select new
            //             {
            //                 p.ID,
            //                 p.PORT_NAME
            //             }).ToList();
            SValues = new List<Instance>();
            foreach (var obj in Ports)
            {
                if (SValues == null)
                    SValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.PORT_ID;
                instance.Name = obj.PORT_NAME;

                SValues.Add(instance);
            }
            return SValues;
        }

        public List<Instance> getAllSFPsByPort(int PortId)
        {
            List<Instance> PValues = null;
            var SFPs = (from p in _context.DATACOM_PORT
                        join sp in _context.DATACOM_PORT_SFP
                        on p.ID equals sp.PARENT_ID
                        join sf in _context.DATACOM_SFP
                        on sp.CHILD_ID equals sf.ID
                        where p.ID == PortId
                        where sp.RETIRE_DATE == null
                        select new
                        {
                            sf.ID,
                            sf.SERIAL_NUMBER
                        }).ToList();
            PValues = new List<Instance>();
            foreach (var obj in SFPs)
            {
                if (PValues == null)
                    PValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SERIAL_NUMBER;

                PValues.Add(instance);
            }
            return PValues;
        }

        public List<Instance> getAllSFPsByNE(int NeId)
        {
            List<Instance> PValues = null;
            var SFPs = (from RSF in _context.ROUTER_SFPS
                        where RSF.NE_ID == NeId
                        select new
                        {
                            RSF.ID,
                            RSF.TYPE,
                            RSF.SERIAL_NUMBER
                        }).ToList();

            //var SFPs = (from ne in _context.DATACOM_NE
            //            join nb in _context.DATACOM_NE_BOARD
            //            on ne.Id equals nb.parentId
            //            join b in _context.DATACOME_BOARD
            //            on nb.childId equals b.Id
            //            join bs in _context.DATACOM_BOARD_SUBBOARD
            //            on b.Id equals bs.parentId
            //            join s in _context.DATACOM_SUBBOARD
            //            on bs.childId equals s.ID
            //            join p in _context.DATACOM_PORT
            //            on s.ID equals p.PARENT_ID
            //            join sp in _context.DATACOM_PORT_SFP
            //            on p.ID equals sp.PARENT_ID
            //            join sf in _context.DATACOM_SFP
            //            on sp.CHILD_ID equals sf.ID
            //            where ne.Id == NeId
            //            where bs.retireDate == null
            //            where nb.retireDate == null
            //            where sp.RETIRE_DATE == null
            //            select new
            //            {
            //                sf.ID,
            //                sf.SERIAL_NUMBER
            //            }).ToList();
            PValues = new List<Instance>();
            foreach (var obj in SFPs)
            {
                if (PValues == null)
                    PValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.Name = obj.SERIAL_NUMBER;
                instance.DisName = obj.TYPE;

                PValues.Add(instance);
            }
            return PValues;
        }

        //getAllOpticalBoardsByNE
        public List<Instance> getAllOpticalBoardsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.OPTICAL_NE_BOARDS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.SLOT_ID,
                                 RS.SERIAL_NUMBER
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.SLOT_ID;
                instance.Name = obj.SERIAL_NUMBER;

                BValues.Add(instance);
            }
            return BValues;
        }

        //getAllOpticalPortsByNE
        public List<Instance> getAllOpticalPortsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.OPTICAL_NE_PORTS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.PORT_ID,
                                 RS.PORT_NAME
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.PORT_ID;
                instance.Name = obj.PORT_NAME;

                BValues.Add(instance);
            }
            return BValues;
        }

        //getAllOpticalSfpsByNE
        public List<Instance> getAllOpticalSfpsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.OPTICAL_NE_SFPS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.TYPE,
                                 RS.SERIAL_NUMBER
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.SERIAL_NUMBER;
                instance.DisName = obj.TYPE;
                BValues.Add(instance);
            }
            return BValues;
        }

        //getAllMWPortsByNE
        public List<Instance> getAllMWPortsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.MW_NE_PORTS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.PORT_ID,
                                 RS.PORT_NAME
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.PORT_ID;
                instance.Name = obj.PORT_NAME;

                BValues.Add(instance);
            }
            return BValues;
        }

        //getAllMWSfpsByNE
        public List<Instance> getAllMWSfpsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.MW_NE_SFP
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.TYPE,
                                 RS.SERIAL_NUMBER
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.Name = obj.SERIAL_NUMBER;
                instance.DisName = obj.TYPE;
                BValues.Add(instance);
            }
            return BValues;
        }

        //getAllFirewallSubBoardsByNE
        public List<Instance> getAllFirewallSubBoardsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.FIREWALL_NE_SUBBOARDS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.SUBSLOT_ID,
                                 RS.SERIAL_NUMBER
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();

                instance.Id = obj.ID;
                instance.ParentId = obj.SUBSLOT_ID;
                instance.Name = obj.SERIAL_NUMBER;
                BValues.Add(instance);
            }
            return BValues;
        }
        //getAllFirewallPortsByNE
        public List<Instance> getAllFirewallPortsByNE(int NeId)
        {
            List<Instance> BValues = null;
            var SubBoards = (from RS in _context.FIREWALL_NE_PORTS
                             where RS.NE_ID == NeId
                             select new
                             {
                                 RS.ID,
                                 RS.PORT_ID,
                                 RS.PORT_NAME
                             }).ToList();
            BValues = new List<Instance>();
            foreach (var obj in SubBoards)
            {
                if (BValues == null)
                    BValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.ID;
                instance.ParentId = obj.PORT_ID;
                instance.Name = obj.PORT_NAME;

                BValues.Add(instance);
            }
            return BValues;
        }

        //public object getCategories()
        //{
        //    var Category = (from category in _context.RIM_CATEGORY

        //                    select new
        //                    {
        //                        category.Id,
        //                        category.Name
        //                    }).ToList();
        //    return Category;
        //}

        public object getSubCategories()
        {
            var SubCategory = (from subcategory in _context.RIM_SUBCATEGORY

                               select new
                               {
                                   subcategory.Id,
                                   subcategory.Name,
                                   subcategory.CATEGORY_ID
                               }).ToList();
            return SubCategory;
        }

        public object getChilds(String Category, String Type, int ParentId)
        {
            if (Category == "DATACOM")
            {
                if (Type == "BOARD")
                {
                    var result = (from dn in _context.DATACOM_NE
                                  join dnb in _context.DATACOM_NE_BOARD
                                  on dn.ID equals dnb.PARENT_ID
                                  join db in _context.DATACOME_BOARD
                                  on dnb.CHILD_ID equals db.ID
                                  where dnb.RETIRE_DATE == null && dnb.PARENT_ID == ParentId

                                  select new
                                  {
                                      dn.ID
                                  }).ToList();
                    return result;
                }

            }
            return null;
        }
        public void getChildrenOfElementTree(string category, string type, string elementId)
        {

            if (category == "MW")
            {
                if (type == "NE")
                {
                    _context.MW_NE
                     .Include(m => m.MW_NE_BOARD)
                     .Where(m => m.U2000_REF_ID == elementId)
                     .ToList();

                }
                else
                    if (type == "BOARD")
                    {

                    }
            }
        }
    }
}