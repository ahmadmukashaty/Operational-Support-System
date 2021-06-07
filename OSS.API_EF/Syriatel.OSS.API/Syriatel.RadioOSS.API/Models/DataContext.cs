using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Syriatel.RadioOSS.API.Models
{
    public class DataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public DataContext()
            : base("name=Entities")
        {
            Database.SetInitializer<DataContext>(null);
            this.Configuration.ProxyCreationEnabled = false;
            //     this.Configuration.LazyLoadingEnabled = false;
        }

        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_GLOBAL_INFO> SITE_GLOBAL_INFO { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RIM_MODULES> RIM_MODULES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CANDIDATE_RACK> SITE_CANDIDATE_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CATEGORY> SITE_CATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CO_LOCATED> SITE_CO_LOCATED { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CONFIGURATION> SITE_CONFIGURATION { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CONTACT_PERSON> SITE_CONTACT_PERSON { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_COVERAGE_TYPE> SITE_COVERAGE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_KIND_OF_SUPPORT> SITE_KIND_OF_SUPPORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_LOCATION_TYPE> SITE_LOCATION_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_PROPERTY_TYPE> SITE_PROPERTY_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SUBAREA> SUBAREA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.ZONE> ZONE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.AREA> AREA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.REGION> REGION { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY> SITE_IDENTITY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_NETYPE> RADIO_NETYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_RACK> RADIO_RACK { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_RACK> SITE_IDENTITY_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_RACK_SUBRACK> RADIO_RACK_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SUBRACK> RADIO_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SLOT> RADIO_SLOT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_BOARD> RADIO_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SLOT_BOARD> RADIO_SLOT_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_PORT> RADIO_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.ANTENNA> ANTENNA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_ANTENNA> SITE_IDENTITY_ANTENNA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.CELL> CELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.ANTENNA_CELL> ANTENNA_CELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_HOSTVER> RADIO_HOSTVER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_HOSTVER> SITE_IDENTITY_RADIO_HOSTVER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_RACK_TYPE> RADIO_RACK_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SUBRACK_TYPE> RADIO_SUBRACK_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_BOARD_TYPE> RADIO_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_BBU> RADIO_BBU { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.GCELL> GCELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.UCELL> UCELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.BAND> BAND { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER_RACK> RANCONTROLLER_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER> RAN_COTNTROLLER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_RACK> RAN_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER> RAN_CONTROLLER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_NETYPE> RAN_NE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_HOSTVER> RAN_HOST_VER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER_SITE_IDENTITY> RAB_CONTROLLER_SITE_IDENTITY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER_RAN_HOSTVER> RAN_CONTROLLER_RAN_HOSTVER { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RANCONTROLLER_RACK> RANCONTROLLER_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SUBRACK_HW_TYPE> RAN_SUBRACK_HW_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SCU_TYPE> RAN_SCU_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SUBRACK> RAN_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SUBRACK_TYPE> RAN_SUBRACK_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_RACK_SUBRACK> RAN_RACK_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SLOT> RAN_SLOT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_SLOT_BOARD> RAN_SLOT_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_BOARD> RAN_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_PORT> RAN_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_BOARD_TYPE> RAN_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_LOGICAL_FUNCT_TYPE> RAN_LOGICAL_FUNCT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_BOARD_CLASS> RAN_BOARD_CLASS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_TYPE_POUC_TDM> RAN_TYPE_POUC_TDM { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_PORT_WORK_MODE> RAN_PORT_WORK_MODE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_BOARD_DETAILS> RADIO_BOARD_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_PORT_DETAILS> RADIO_PORT_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_RACK_DETAILS> RADIO_RACK_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SLOT_DETAILS> RADIO_SLOT_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SUBRACK_DETAILS> RADIO_SUBRACK_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CANDIDATE_DETAILS> SITE_CANDIDATE_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_MODE> SITE_MODE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_CHILD> SITE_IDENTITY_CHILD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_CHILD_ANTENNA> SITE_IDENTITY_CHILD_ANTENNA { get; set; }
        public DbSet<RIM_ATTRIBUTE> RIM_ATTRIBUTE { get; set; }
        public DbSet<RIM_ATTRIBUTES> RIM_ATTRIBUTES { get; set; }
        public DbSet<RIM_CATEGORY> RIM_CATEGORY { get; set; }
        //public DbSet<RIM_CATEGORY_TABLE> RIM_CATEGORY_TABLE { get; set; }
        //public DbSet<RIM_CATEGORY_TABLE_TYPE> RIM_CATEGORY_TABLE_TYPE { get; set; }
        public DbSet<RIM_TABLE> RIM_TABLE { get; set; }
        public DbSet<RIM_CLASSIFICATION> RIM_CLASSIFICATION { get; set; }
        public DbSet<RIM_CLASSIFICATION_CATEGORY> RIM_CLASSIFICATION_CATEGORY { get; set; }
        public DbSet<RIM_IS_MAIN> RIM_IS_MAIN { get; set; }
        public DbSet<RIM_LEVEL_NODES> RIM_LEVEL_NODES { get; set; }
        public DbSet<RIM_LEVELS> RIM_LEVELS { get; set; }
        ///public DbSet<RIM_MODULES> RIM_MODULES { get; set; }
        public DbSet<RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public DbSet<RIM_RELATION_TYPE> RIM_RELATION_TYPE { get; set; }
        public DbSet<RIM_SUBCATEGORY> RIM_SUBCATEGORY { get; set; }        
                   
            
            
    }
}
