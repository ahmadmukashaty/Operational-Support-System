
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Syriatel.OSS.API.Models;
using Syriatel.OSS.API.NeTreeModeView;

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
                          join r in _context.RIM_CATEGORIES
                           on c.CATEGORY_ID equals r.ID
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
        public List<Instance> getRoots()
        {
            var modules = _context.RIM_MODULES.ToList();
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
                              join s in _context.RIM_CATEGORIES
                                  on c.ID equals s.MODULE_ID
                              where s.MODULE_ID == ModuleId
                              select new
                              {
                                  s.NAME,
                                  s.ID
                              }).ToList();

            List<Instance> Values = new List<Instance>();
            foreach (var obj in categories)
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
        public List<Instance> getAllSubCategories(int categoryId)
        {
            //List<RIM_SUBCATEGORY> subcategories = 
            //    _context.RIM_SUBCATEGORY
            //    .Include(x => x.RIM_CATEGORY)
            //    .Where(x => x.categoryId == categoryId)
            //    .ToList();
            var subcategories = (from c in _context.RIM_CATEGORY
                                 join s in _context.RIM_SUBCATEGORY
                                     on c.Id equals s.categoryId
                                 where s.categoryId == categoryId
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
                         on c.Id equals s.categoryId
                         join ne in _context.DATACOM_NE
                         on s.Id equals ne.subCategoryId
                         join net in _context.DATACOM_NE_Type
                         on ne.typeId equals net.Id
                         where ne.refId == urefId
                         where c.Id == categoryId
                         where s.Id == SubCategory

                         select new
                         {

                             ne.refId,
                             ne.Name,
                             ne.aliasName,
                             ne.subrackType,
                             ne.IP,
                             ne.macAddress,
                             ne.softwareVersion,
                             ne.patchVersion,
                             ne.lsrId,
                             ne.REMARKS,
                             ne.deploymentStatus,
                             ne.createDate,
                             ne.freeBoard,
                             net.Vendor,
                             net.Class,
                             net.Series,
                             net.Modle,
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
                     on ne.typeId equals net.Id
                     where ne.Id == Id

                     select new
                     {
                         ne.refId,
                         ne.Name,
                         ne.aliasName,
                         ne.subrackType,
                         ne.IP,
                         ne.macAddress,
                         ne.softwareVersion,
                         ne.patchVersion,
                         ne.lsrId,
                         ne.REMARKS,
                         ne.deploymentStatus,
                         ne.createDate,
                         ne.freeBoard,
                         net.Vendor,
                         net.Class,
                         net.Series,
                         net.Modle,
                         net.TOTAL_CHILD_NUMBER
                     }

                     ).FirstOrDefault();



            return q;
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
                     on ne.Id equals nb.parentId
                     join db in _context.DATACOME_BOARD
                     on nb.childId equals db.Id
                     join dbt in _context.DATACOM_BOARD_TYPE
                     on db.typeId equals dbt.Id
                     where ne.refId == urefId
                     where db.slotId == slotId
                     where nb.retireDate == null
                     select new
                     {
                         db.subRackId,
                         db.slotId,
                         db.hardwareVersion,
                         db.softwareVersion,
                         db.serialNumber,
                         db.Remarks,
                         db.Barcode,
                         db.biosVersion,
                         db.FPGAVersion,
                         db.Status,
                         db.BOMItem,
                         db.Description,
                         db.deploymentStatus,
                         db.freeSubBoard,
                         dbt.Type,
                         dbt.aliasName,
                         dbt.totalChildNumber

                     }

                      ).ToList();



            return q;



            //else if (categoryId == 2)
            //{
            //    OPTICAL_NE ONE = _context.OPTICAL_NES.FirstOrDefault(r => r.U2000_REF_ID == urefId.ToString());
            //    return ONE;
            //}
            //else if (categoryId == 3)
            //{
            //    MW_NE MNE = _context.MW_NES.FirstOrDefault(m => m.U2000_REF_ID == urefId.ToString());
            //    return MNE;
            //}


        }

        public object getBoardDetails(int Id)
        {

            var q = (from db in _context.DATACOME_BOARD
                     join dbt in _context.DATACOM_BOARD_TYPE
                     on db.typeId equals dbt.Id
                     where db.Id == Id
                     select new
                     {
                         db.subRackId,
                         db.slotId,
                         db.hardwareVersion,
                         db.softwareVersion,
                         db.serialNumber,
                         db.Remarks,
                         db.Barcode,
                         db.biosVersion,
                         db.FPGAVersion,
                         db.Status,
                         db.BOMItem,
                         db.Description,
                         db.deploymentStatus,
                         db.freeSubBoard,
                         dbt.Type,
                         dbt.aliasName,
                         dbt.totalChildNumber

                     }

                      ).ToList();



            return q;



            //else if (categoryId == 2)
            //{
            //    OPTICAL_NE ONE = _context.OPTICAL_NES.FirstOrDefault(r => r.U2000_REF_ID == urefId.ToString());
            //    return ONE;
            //}
            //else if (categoryId == 3)
            //{
            //    MW_NE MNE = _context.MW_NES.FirstOrDefault(m => m.U2000_REF_ID == urefId.ToString());
            //    return MNE;
            //}


        }

        public object getSubBoadDetils1(int urefId, int slotId, int subSlotId)
        {
            var subBoards = (from dn in _context.DATACOM_NE
                             join dnb in _context.DATACOM_NE_BOARD
                             on dn.Id equals dnb.parentId
                             join db in _context.DATACOME_BOARD
                             on dnb.childId equals db.Id
                             join dbs in _context.DATACOM_BOARD_SUBBOARD
                             on db.Id equals dbs.parentId
                             join ds in _context.DATACOM_SUBBOARD
                             on dbs.childId equals ds.ID
                             join dst in _context.DATACOM_SUBBOARD_TYPE
                             on ds.TYPE_ID equals dst.ID
                             where dn.refId == urefId
                             where db.slotId == slotId
                             where ds.SUBSLOT_ID == subSlotId
                             where dnb.retireDate == null
                             where dbs.retireDate == null
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
        public object getPortDetails1(int urefId, int slotId, int subSlotId, int portId)
        {
            var ports = (from dn in _context.DATACOM_NE
                         join dnb in _context.DATACOM_NE_BOARD
                         on dn.Id equals dnb.parentId
                         join db in _context.DATACOME_BOARD
                         on dnb.childId equals db.Id
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on db.Id equals dbs.parentId
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.childId equals ds.ID
                         join dst in _context.DATACOM_SUBBOARD_TYPE
                         on ds.TYPE_ID equals dst.ID
                         join p in _context.DATACOM_PORT
                         on ds.ID equals p.PARENT_ID
                         join pt in _context.DATACOM_PORT_TYPE
                         on p.PORT_ID equals pt.ID
                         where dn.refId == urefId
                         where db.slotId == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dnb.retireDate == null
                         where dbs.retireDate == null
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
            return ports;
        }
        public object getSFPDetails1(int urefId, int slotId, int subSlotId, int portId)
        {
            var ports = (from dn in _context.DATACOM_NE
                         join dnb in _context.DATACOM_NE_BOARD
                         on dn.Id equals dnb.parentId
                         join db in _context.DATACOME_BOARD
                         on dnb.childId equals db.Id
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on db.Id equals dbs.parentId
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.childId equals ds.ID
                         join dst in _context.DATACOM_SUBBOARD_TYPE
                         on ds.TYPE_ID equals dst.ID
                         join p in _context.DATACOM_PORT
                         on ds.ID equals p.PARENT_ID
                         join dps in _context.DATACOM_PORT_SFP
                         on p.ID equals dps.PARENT_ID
                         join dsf in _context.DATACOM_SFP
                         on dps.CHILD_ID equals dsf.ID
                         where dn.refId == urefId
                         where db.slotId == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dnb.retireDate == null
                         where dbs.retireDate == null
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
                         }).ToList();
            return ports;
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
                                   on c.Id equals s.categoryId
                               join ne in _context.DATACOM_NE
                               on s.Id equals ne.subCategoryId
                               where c.Id == CategoryId
                               where s.Id == SubCategoryId
                               select new
                               {
                                   ne.Id,
                                   ne.Name
                               }
                                ).ToList();

                NeValues = new List<Instance>();
                foreach (var obj in routers)
                {
                    if (NeValues == null)
                        NeValues = new List<Instance>();

                    Instance instance = new Instance();
                    instance.Id = obj.Id;
                    instance.Name = obj.Name;

                    NeValues.Add(instance);
                }
            }
            //else if (CategoryId == 2)
            //{
            //    List<Optical_NE> OpticalNE = new List<DATACOM_NE>();
            //    Dictionary<int, string> NEDetails = new Dictionary<int, string>();

            //    var routers = (from c in _context.RIM_CATEGORY
            //                   join s in _context.RIM_SUBCATEGORY
            //                       on c.Id equals s.categoryId
            //                   join ne in _context.DATACOM_NE
            //                   on s.Id equals ne.subCategoryId
            //                   where c.Id == CategoryId
            //                   where s.Id == SubCategoryId
            //                   select new
            //                   {
            //                       ne.Id,
            //                       ne.Name
            //                   }
            //                    ).ToList();

            //    NeValues = new List<Instance>();
            //    foreach (var obj in routers)
            //    {
            //        if (NeValues == null)
            //            NeValues = new List<Instance>();

            //        Instance instance = new Instance();
            //        instance.Id = obj.Id;
            //        instance.Name = obj.Name;

            //        NeValues.Add(instance);
            //    }
            //}
            return NeValues;


        }

        public object getAllBoardsForNE(string NEName)
        {

            var Boards = (from ne in _context.DATACOM_NE
                          join nb in _context.DATACOM_NE_BOARD
                          on ne.Id equals nb.parentId
                          join b in _context.DATACOME_BOARD
                          on nb.childId equals b.Id
                          where ne.Name == NEName
                          where nb.retireDate == null
                          select new
                          {
                              b.Id,
                              b.slotId
                          }).ToList();




            return Boards;


        }
        public object getAllSUBBoardsForNE(string NEName, int slotId)
        {

            var SUBBoards = (from ne in _context.DATACOM_NE
                             join nb in _context.DATACOM_NE_BOARD
                             on ne.Id equals nb.parentId
                             join b in _context.DATACOME_BOARD
                             on nb.childId equals b.Id
                             join dbs in _context.DATACOM_BOARD_SUBBOARD
                             on b.Id equals dbs.parentId
                             join ds in _context.DATACOM_SUBBOARD
                             on dbs.childId equals ds.ID
                             where ne.Name == NEName
                             where b.slotId == slotId
                             where dbs.retireDate == null
                             where nb.retireDate == null
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
                         on ne.Id equals nb.parentId
                         join b in _context.DATACOME_BOARD
                         on nb.childId equals b.Id
                         join dbs in _context.DATACOM_BOARD_SUBBOARD
                         on b.Id equals dbs.parentId
                         join ds in _context.DATACOM_SUBBOARD
                         on dbs.childId equals ds.ID
                         join dp in _context.DATACOM_PORT
                         on ds.ID equals dp.PARENT_ID
                         where ne.Name == NEName
                         where b.slotId == slotId
                         where ds.SUBSLOT_ID == subSlotId
                         where dbs.retireDate == null
                         where nb.retireDate == null
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
                            on ne.Id equals nb.parentId
                            join b in _context.DATACOME_BOARD
                            on nb.childId equals b.Id
                            join dbs in _context.DATACOM_BOARD_SUBBOARD
                            on b.Id equals dbs.parentId
                            join ds in _context.DATACOM_SUBBOARD
                            on dbs.childId equals ds.ID
                            join dp in _context.DATACOM_PORT
                            on ds.ID equals dp.PARENT_ID
                            join dpsf in _context.DATACOM_PORT_SFP
                            on dp.ID equals dpsf.PARENT_ID
                            join dsf in _context.DATACOM_SFP
                            on dpsf.CHILD_ID equals dsf.ID
                            where ne.Name == NEName
                            where b.slotId == slotId
                            where ds.SUBSLOT_ID == subSlotId
                            where dbs.retireDate == null
                            where dpsf.RETIRE_DATE == null
                            where nb.retireDate == null
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
                           ne.Id,
                           ne.Name,
                           ne.subCategoryId
                       }).ToList();

            List<Instance> NeValues = new List<Instance>();
            foreach (var obj in NEs)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.Id;
                instance.Name = obj.Name;
                instance.ParentId = (int)obj.subCategoryId;

                NeValues.Add(instance);
            }

            return NeValues;
        }

        public BoardInstances getAllBoards()
        {
            var Boards = (from board in _context.DATACOME_BOARD

                          select new
                          {
                              board.Id,
                              board.slotId
                          }).ToList();

            BoardInstances BoardValues = new BoardInstances();
            BoardValues.TableName = "DATACOM_NE";
            foreach (var obj in Boards)
            {
                if (BoardValues.Boards == null)
                    BoardValues.Boards = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.Id;
                instance.Name = obj.slotId.ToString();

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
                          on ne.Id equals nb.parentId
                          join b in _context.DATACOME_BOARD
                          on nb.childId equals b.Id
                          where ne.Id == NeId
                          where nb.retireDate == null
                          select new
                          {
                              b.Id,
                              b.slotId
                          }).ToList();
            NeValues = new List<Instance>();
            foreach (var obj in Boards)
            {
                if (NeValues == null)
                    NeValues = new List<Instance>();

                Instance instance = new Instance();
                instance.Id = obj.Id;
                instance.ParentId = obj.slotId;

                NeValues.Add(instance);
            }
            return NeValues;
        }

        public object getAllSubBoardsByBoard(int BoardId)
        {
            var SubBoards = (from subboard in _context.DATACOM_BOARD_SUBBOARD
                             where subboard.parentId == BoardId
                             select new
                             {
                                 subboard.childId
                             }).ToList();
            return SubBoards;
        }

        public object getAllPortsBySubBoard(int SubBoardId)
        {
            var Ports = (from ports in _context.DATACOM_PORT
                         where ports.PARENT_ID == SubBoardId
                         select new
                         {
                             ports.ID
                         }).ToList();
            return Ports;
        }

        public object getAllSFPsByPort(int PortId)
        {
            var SFPs = (from sfp in _context.DATACOM_PORT_SFP
                        where sfp.PARENT_ID == PortId
                        select new
                        {
                            sfp.ID
                        }).ToList();
            return SFPs;
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
                                   subcategory.categoryId
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
                                  on dn.Id equals dnb.parentId
                                  join db in _context.DATACOME_BOARD
                                  on dnb.childId equals db.Id
                                  where dnb.retireDate == null && dnb.parentId == ParentId

                                  select new
                                  {
                                      dn.Id
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