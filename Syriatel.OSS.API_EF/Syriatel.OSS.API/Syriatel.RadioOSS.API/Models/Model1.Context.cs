﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Syriatel.RadioOSS.API.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class Entities16 : DbContext
    {
        public Entities16()
            : base("name=Entities16")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public DbSet<ANTENNA> Antennae { get; set; }
        public DbSet<ANTENNA_CELL> ANTENNA_CELL { get; set; }
        public DbSet<AREA> AREAs { get; set; }
        public DbSet<BAND> BANDs { get; set; }
        public DbSet<CELL> CELLs { get; set; }
        public DbSet<DATACOM_BOARD> DATACOM_BOARD { get; set; }
        public DbSet<DATACOM_BOARD_OLD> DATACOM_BOARD_OLD { get; set; }
        public DbSet<DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        public DbSet<DATACOM_BOARD_SUBBOARD_OLD> DATACOM_BOARD_SUBBOARD_OLD { get; set; }
        public DbSet<DATACOM_BOARD_TYPE> DATACOM_BOARD_TYPE { get; set; }
        public DbSet<DATACOM_FINANCE_NE> DATACOM_FINANCE_NE { get; set; }
        public DbSet<DATACOM_FINANCE_TERM> DATACOM_FINANCE_TERM { get; set; }
        public DbSet<DATACOM_NE> DATACOM_NE { get; set; }
        public DbSet<DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
        public DbSet<DATACOM_NE_BOARD_OLD> DATACOM_NE_BOARD_OLD { get; set; }
        public DbSet<DATACOM_NE_OLD> DATACOM_NE_OLD { get; set; }
        public DbSet<DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public DbSet<DATACOM_NE_SITE_OLD> DATACOM_NE_SITE_OLD { get; set; }
        public DbSet<DATACOM_NE_TYPE> DATACOM_NE_TYPE { get; set; }
        public DbSet<DATACOM_PORT> DATACOM_PORT { get; set; }
        public DbSet<DATACOM_PORT_OLD> DATACOM_PORT_OLD { get; set; }
        public DbSet<DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
        public DbSet<DATACOM_PORT_SFP_OLD> DATACOM_PORT_SFP_OLD { get; set; }
        public DbSet<DATACOM_PORT_TYPE> DATACOM_PORT_TYPE { get; set; }
        public DbSet<DATACOM_SFP> DATACOM_SFP { get; set; }
        public DbSet<DATACOM_SFP_OLD> DATACOM_SFP_OLD { get; set; }
        public DbSet<DATACOM_SUBBOARD> DATACOM_SUBBOARD { get; set; }
        public DbSet<DATACOM_SUBBOARD_OLD> DATACOM_SUBBOARD_OLD { get; set; }
        public DbSet<DATACOM_SUBBOARD_TYPE> DATACOM_SUBBOARD_TYPE { get; set; }
        public DbSet<DATACOM_WO_NE> DATACOM_WO_NE { get; set; }
        public DbSet<DATACOM_WORK_ORDER> DATACOM_WORK_ORDER { get; set; }
        public DbSet<FIREWALL_BOARD> FIREWALL_BOARD { get; set; }
        public DbSet<FIREWALL_BOARD_SUBBOARD> FIREWALL_BOARD_SUBBOARD { get; set; }
        public DbSet<FIREWALL_BOARD_TYPE> FIREWALL_BOARD_TYPE { get; set; }
        public DbSet<FIREWALL_CPU> FIREWALL_CPU { get; set; }
        public DbSet<FIREWALL_NE> FIREWALL_NE { get; set; }
        public DbSet<FIREWALL_NE_BOARD> FIREWALL_NE_BOARD { get; set; }
        public DbSet<FIREWALL_NE_SITE> FIREWALL_NE_SITE { get; set; }
        public DbSet<FIREWALL_NE_TYPE> FIREWALL_NE_TYPE { get; set; }
        public DbSet<FIREWALL_PORT> FIREWALL_PORT { get; set; }
        public DbSet<FIREWALL_PORT_TYPE> FIREWALL_PORT_TYPE { get; set; }
        public DbSet<FIREWALL_SUBBOARD> FIREWALL_SUBBOARD { get; set; }
        public DbSet<FIREWALL_SUBBOARD_TYPE> FIREWALL_SUBBOARD_TYPE { get; set; }
        public DbSet<GCELL> GCELLs { get; set; }
        public DbSet<MW_BOARD> MW_BOARD { get; set; }
        public DbSet<MW_BOARD_OLD> MW_BOARD_OLD { get; set; }
        public DbSet<MW_BOARD_TYPE> MW_BOARD_TYPE { get; set; }
        public DbSet<MW_NE> MW_NE { get; set; }
        public DbSet<MW_NE_BOARD> MW_NE_BOARD { get; set; }
        public DbSet<MW_NE_BOARD_OLD> MW_NE_BOARD_OLD { get; set; }
        public DbSet<MW_NE_OLD> MW_NE_OLD { get; set; }
        public DbSet<MW_NE_SITE> MW_NE_SITE { get; set; }
        public DbSet<MW_NE_SITE_OLD> MW_NE_SITE_OLD { get; set; }
        public DbSet<MW_NE_TYPE> MW_NE_TYPE { get; set; }
        public DbSet<MW_PORT> MW_PORT { get; set; }
        public DbSet<MW_PORT_OLD> MW_PORT_OLD { get; set; }
        public DbSet<MW_PORT_SFP> MW_PORT_SFP { get; set; }
        public DbSet<MW_PORT_SFP_OLD> MW_PORT_SFP_OLD { get; set; }
        public DbSet<MW_PORT_TYPE> MW_PORT_TYPE { get; set; }
        public DbSet<MW_SFP> MW_SFP { get; set; }
        public DbSet<MW_SFP_OLD> MW_SFP_OLD { get; set; }
        public DbSet<OPTICAL_AREA_ID_MAPPING> OPTICAL_AREA_ID_MAPPING { get; set; }
        public DbSet<OPTICAL_BOARD> OPTICAL_BOARD { get; set; }
        public DbSet<OPTICAL_FINANCE_NE> OPTICAL_FINANCE_NE { get; set; }
        public DbSet<OPTICAL_FINANCE_TERM> OPTICAL_FINANCE_TERM { get; set; }
        public DbSet<OPTICAL_NE> OPTICAL_NE { get; set; }
        public DbSet<OPTICAL_NE_AREA> OPTICAL_NE_AREA { get; set; }
        public DbSet<OPTICAL_NE_SITE> OPTICAL_NE_SITE { get; set; }
        public DbSet<OPTICAL_NE_SUBRACK> OPTICAL_NE_SUBRACK { get; set; }
        public DbSet<OPTICAL_NE_TYPE> OPTICAL_NE_TYPE { get; set; }
        public DbSet<OPTICAL_PORT> OPTICAL_PORT { get; set; }
        public DbSet<OPTICAL_PORT_SFP> OPTICAL_PORT_SFP { get; set; }
        public DbSet<OPTICAL_PORT_TYPE> OPTICAL_PORT_TYPE { get; set; }
        public DbSet<OPTICAL_RACK> OPTICAL_RACK { get; set; }
        public DbSet<OPTICAL_SFP> OPTICAL_SFP { get; set; }
        public DbSet<OPTICAL_SUBRACK> OPTICAL_SUBRACK { get; set; }
        public DbSet<OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
        public DbSet<OPTICAL_SUBRACK_TYPE> OPTICAL_SUBRACK_TYPE { get; set; }
        public DbSet<OPTICAL_WO_NE> OPTICAL_WO_NE { get; set; }
        public DbSet<OPTICAL_WORK_ORDER> OPTICAL_WORK_ORDER { get; set; }
        public DbSet<RADIO_BBU> RADIO_BBU { get; set; }
        public DbSet<RADIO_BOARD> RADIO_BOARD { get; set; }
        public DbSet<RADIO_BOARD_TYPE> RADIO_BOARD_TYPE { get; set; }
        public DbSet<RADIO_HOSTVER> RADIO_HOSTVER { get; set; }
        public DbSet<RADIO_NETYPE> RADIO_NETYPE { get; set; }
        public DbSet<RADIO_PORT> RADIO_PORT { get; set; }
        public DbSet<RADIO_RACK> RADIO_RACK { get; set; }
        public DbSet<RADIO_RACK_SUBRACK> RADIO_RACK_SUBRACK { get; set; }
        public DbSet<RADIO_RACK_TYPE> RADIO_RACK_TYPE { get; set; }
        public DbSet<RADIO_SLOT> RADIO_SLOT { get; set; }
        public DbSet<RADIO_SLOT_BOARD> RADIO_SLOT_BOARD { get; set; }
        public DbSet<RADIO_SUBRACK> RADIO_SUBRACK { get; set; }
        public DbSet<RADIO_SUBRACK_TYPE> RADIO_SUBRACK_TYPE { get; set; }
        public DbSet<RAN_BOARD> RAN_BOARD { get; set; }
        public DbSet<RAN_BOARD_CLASS> RAN_BOARD_CLASS { get; set; }
        public DbSet<RAN_BOARD_TYPE> RAN_BOARD_TYPE { get; set; }
        public DbSet<RAN_CONTROLLER> RAN_CONTROLLER { get; set; }
        public DbSet<RAN_CONTROLLER_CANDIDATE> RAN_CONTROLLER_CANDIDATE { get; set; }
        public DbSet<RAN_CONTROLLER_RACK> RAN_CONTROLLER_RACK { get; set; }
        public DbSet<RAN_CONTROLLER_RAN_HOSTVER> RAN_CONTROLLER_RAN_HOSTVER { get; set; }
        public DbSet<RAN_CONTROLLER_SITE_IDENTITY> RAN_CONTROLLER_SITE_IDENTITY { get; set; }
        public DbSet<RAN_HOSTVER> RAN_HOSTVER { get; set; }
        public DbSet<RAN_LOGICAL_FUNCT_TYPE> RAN_LOGICAL_FUNCT_TYPE { get; set; }
        public DbSet<RAN_NETYPE> RAN_NETYPE { get; set; }
        public DbSet<RAN_PORT> RAN_PORT { get; set; }
        public DbSet<RAN_PORT_WORK_MODE> RAN_PORT_WORK_MODE { get; set; }
        public DbSet<RAN_RACK> RAN_RACK { get; set; }
        public DbSet<RAN_RACK_SUBRACK> RAN_RACK_SUBRACK { get; set; }
        public DbSet<RAN_SCU_TYPE> RAN_SCU_TYPE { get; set; }
        public DbSet<RAN_SLOT> RAN_SLOT { get; set; }
        public DbSet<RAN_SLOT_BOARD> RAN_SLOT_BOARD { get; set; }
        public DbSet<RAN_SUBRACK> RAN_SUBRACK { get; set; }
        public DbSet<RAN_SUBRACK_HW_TYPE> RAN_SUBRACK_HW_TYPE { get; set; }
        public DbSet<RAN_SUBRACK_TYPE> RAN_SUBRACK_TYPE { get; set; }
        public DbSet<RAN_TYPE_POUC_TDM> RAN_TYPE_POUC_TDM { get; set; }
        public DbSet<REGION> REGIONs { get; set; }
        public DbSet<RIM_ATTRIBUTE> RIM_ATTRIBUTE { get; set; }
        public DbSet<RIM_ATTRIBUTES> RIM_ATTRIBUTES { get; set; }
        public DbSet<RIM_CATEGORY> RIM_CATEGORY { get; set; }
        public DbSet<RIM_CLASSIFICATION> RIM_CLASSIFICATION { get; set; }
        public DbSet<RIM_CLASSIFICATION_CATEGORY> RIM_CLASSIFICATION_CATEGORY { get; set; }
        public DbSet<RIM_CLASSIFICATION_TABLE> RIM_CLASSIFICATION_TABLE { get; set; }
        public DbSet<RIM_CLASSIFICATION_TABLE_TYPE> RIM_CLASSIFICATION_TABLE_TYPE { get; set; }
        public DbSet<RIM_IS_MAIN> RIM_IS_MAIN { get; set; }
        public DbSet<RIM_LEVEL_NODES> RIM_LEVEL_NODES { get; set; }
        public DbSet<RIM_LEVELS> RIM_LEVELS { get; set; }
        public DbSet<RIM_MODULES> RIM_MODULES { get; set; }
        public DbSet<RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public DbSet<RIM_RELATION_TYPE> RIM_RELATION_TYPE { get; set; }
        public DbSet<RIM_SUBCATEGORY> RIM_SUBCATEGORY { get; set; }
        public DbSet<RIM_TABLE> RIM_TABLE { get; set; }
        public DbSet<RIM_TYPE> RIM_TYPE { get; set; }
        public DbSet<SITE> SITEs { get; set; }
        public DbSet<SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
        public DbSet<SITE_CANDIDATE_RACK> SITE_CANDIDATE_RACK { get; set; }
        public DbSet<SITE_CATEGORY> SITE_CATEGORY { get; set; }
        public DbSet<SITE_CO_LOCATED> SITE_CO_LOCATED { get; set; }
        public DbSet<SITE_CONFIGURATION> SITE_CONFIGURATION { get; set; }
        public DbSet<SITE_CONTACT_PERSON> SITE_CONTACT_PERSON { get; set; }
        public DbSet<SITE_COVERAGE_TYPE> SITE_COVERAGE_TYPE { get; set; }
        public DbSet<SITE_GLOBAL_INFO> SITE_GLOBAL_INFO { get; set; }
        public DbSet<SITE_IDENTITY> SITE_IDENTITY { get; set; }
        public DbSet<SITE_IDENTITY_ANTENNA> SITE_IDENTITY_ANTENNA { get; set; }
        public DbSet<SITE_IDENTITY_CHILD> SITE_IDENTITY_CHILD { get; set; }
        public DbSet<SITE_IDENTITY_CHILD_ANTENNA> SITE_IDENTITY_CHILD_ANTENNA { get; set; }
        public DbSet<SITE_IDENTITY_HOSTVER> SITE_IDENTITY_HOSTVER { get; set; }
        public DbSet<SITE_IDENTITY_WRONG> SITE_IDENTITY_WRONG { get; set; }
        public DbSet<SITE_KIND_OF_SUPPORT> SITE_KIND_OF_SUPPORT { get; set; }
        public DbSet<SITE_LOCATION_TYPE> SITE_LOCATION_TYPE { get; set; }
        public DbSet<SITE_MODE> SITE_MODE { get; set; }
        public DbSet<SITE_PROPERTY_TYPE> SITE_PROPERTY_TYPE { get; set; }
        public DbSet<SUBAREA> SUBAREAs { get; set; }
        public DbSet<UCELL> UCELLs { get; set; }
        public DbSet<ZONE> ZONEs { get; set; }
        public DbSet<CELL_TEST> CELL_TEST { get; set; }
        public DbSet<OPTICAL_BOARD_TYPE> OPTICAL_BOARD_TYPE { get; set; }
        public DbSet<SITE_TEST_MHD> SITE_TEST_MHD { get; set; }
        public DbSet<DATACOM_NE_DETAILS> DATACOM_NE_DETAILS { get; set; }
        public DbSet<FIREWALL_NE_DETAILS> FIREWALL_NE_DETAILS { get; set; }
        public DbSet<FIREWALL_NE_PORTS> FIREWALL_NE_PORTS { get; set; }
        public DbSet<FIREWALL_NE_SUBBOARDS> FIREWALL_NE_SUBBOARDS { get; set; }
        public DbSet<MW_NE_DETAILS> MW_NE_DETAILS { get; set; }
        public DbSet<MW_NE_PORTS> MW_NE_PORTS { get; set; }
        public DbSet<MW_NE_SFP> MW_NE_SFP { get; set; }
        public DbSet<OPTICAL_NE_BOARDS> OPTICAL_NE_BOARDS { get; set; }
        public DbSet<OPTICAL_NE_DETAILS> OPTICAL_NE_DETAILS { get; set; }
        public DbSet<OPTICAL_NE_PORTS> OPTICAL_NE_PORTS { get; set; }
        public DbSet<OPTICAL_NE_SFPS> OPTICAL_NE_SFPS { get; set; }
        public DbSet<OSS_SITE_CANDIDATE_DATA> OSS_SITE_CANDIDATE_DATA { get; set; }
        public DbSet<RADIO_BOARD_DETAILS> RADIO_BOARD_DETAILS { get; set; }
        public DbSet<RADIO_PORT_DETAILS> RADIO_PORT_DETAILS { get; set; }
        public DbSet<RADIO_RACK_DETAILS> RADIO_RACK_DETAILS { get; set; }
        public DbSet<RADIO_SLOT_DETAILS> RADIO_SLOT_DETAILS { get; set; }
        public DbSet<RADIO_SUBRACK_DETAILS> RADIO_SUBRACK_DETAILS { get; set; }
        public DbSet<ROUTER_PORTS> ROUTER_PORTS { get; set; }
        public DbSet<ROUTER_SFPS> ROUTER_SFPS { get; set; }
        public DbSet<ROUTER_SUBBOARDS> ROUTER_SUBBOARDS { get; set; }
        public DbSet<SITE_CANDIDATE_DETAILS> SITE_CANDIDATE_DETAILS { get; set; }
    
        public virtual int CORRECT_CANDIDATE_NAME()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CORRECT_CANDIDATE_NAME");
        }
    
        public virtual int FILL_BOARD_PORT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_BOARD_PORT");
        }
    
        public virtual int FILL_BOARD_SUBBOARD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_BOARD_SUBBOARD");
        }
    
        public virtual int FILL_DATACOME_NE_SUBCATEGORY(string nENAME)
        {
            var nENAMEParameter = nENAME != null ?
                new ObjectParameter("NENAME", nENAME) :
                new ObjectParameter("NENAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_DATACOME_NE_SUBCATEGORY", nENAMEParameter);
        }
    
        public virtual int FILL_DATACOM_FREE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_DATACOM_FREE");
        }
    
        public virtual int FILL_FW_NE_SUBCATEGORY(string u2000_REF_ID)
        {
            var u2000_REF_IDParameter = u2000_REF_ID != null ?
                new ObjectParameter("U2000_REF_ID", u2000_REF_ID) :
                new ObjectParameter("U2000_REF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_FW_NE_SUBCATEGORY", u2000_REF_IDParameter);
        }
    
        public virtual int FILL_MW_BOARD_PORT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_BOARD_PORT");
        }
    
        public virtual int FILL_MW_NE_BOARD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_NE_BOARD");
        }
    
        public virtual int FILL_MW_NE_SITE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_NE_SITE");
        }
    
        public virtual int FILL_MW_NE_SITEA_SITEB(string nAME)
        {
            var nAMEParameter = nAME != null ?
                new ObjectParameter("NAME", nAME) :
                new ObjectParameter("NAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_NE_SITEA_SITEB", nAMEParameter);
        }
    
        public virtual int FILL_MW_NE_SUBCATEGORY(string u2000_REF_ID)
        {
            var u2000_REF_IDParameter = u2000_REF_ID != null ?
                new ObjectParameter("U2000_REF_ID", u2000_REF_ID) :
                new ObjectParameter("U2000_REF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_NE_SUBCATEGORY", u2000_REF_IDParameter);
        }
    
        public virtual int FILL_MW_PORT_SFP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_MW_PORT_SFP");
        }
    
        public virtual int FILL_NE_BOARD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_NE_BOARD");
        }
    
        public virtual int FILL_NE_SUBCATEGORY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_NE_SUBCATEGORY");
        }
    
        public virtual int FILL_OP_NE_SUBCATEGORY(string u2000_REF_ID)
        {
            var u2000_REF_IDParameter = u2000_REF_ID != null ?
                new ObjectParameter("U2000_REF_ID", u2000_REF_ID) :
                new ObjectParameter("U2000_REF_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_OP_NE_SUBCATEGORY", u2000_REF_IDParameter);
        }
    
        public virtual int FILL_PORT_SFP()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_PORT_SFP");
        }
    
        public virtual int FILL_SUBBOARD_PORT()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FILL_SUBBOARD_PORT");
        }
    
        public virtual int IMPORTCELL_UCELL_GCELL()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IMPORTCELL_UCELL_GCELL");
        }
    
        public virtual int IMPORT_SITE_CANDIDATE()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IMPORT_SITE_CANDIDATE");
        }
    
        public virtual int IMPORT_SITE_GLOABLE_INFO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("IMPORT_SITE_GLOABLE_INFO");
        }
    }
}
