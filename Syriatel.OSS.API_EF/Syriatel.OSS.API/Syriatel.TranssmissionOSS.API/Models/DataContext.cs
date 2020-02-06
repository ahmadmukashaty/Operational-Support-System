using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Syriatel.TranssmissionOSS.API.Models
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

        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_CATEGORY> RIM_CATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_BOARD_TYPE> DATACOM_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_NE_TYPE> DATACOM_NE_Type { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_PORT_TYPE> DATACOM_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_SUBBOARD> DATACOM_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_SUBBOARD_TYPE> DATACOM_SUBBOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_BOARD> DATACOME_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_SUBCATEGORY> RIM_SUBCATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_NE> DATACOM_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_PORT> DATACOM_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_SFP> DATACOM_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.SITE> SITE { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_CATEGORY> RIM_CATEGORIES { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_SUBCATEGORY> RIM_SUBCATEGORIES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_MODULES> RIM_MODULES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_LEVELS> RIM_LEVELS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_LEVEL_NODES> RIM_LEVEL_NODES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_CATEGORY> CATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.RIM_ATTRIBUTES> RIM_ATTRIBUTES { get; set; }

        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_AREA_ID_MAPPING> OPTICAL_AREA_ID_MAPPING { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_BOARD> OPTICAL_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_BOARD_TYPE> OPTICAL_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_FINANCE_NE> OPTICAL_FINANCE_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_FINANCE_TERM> OPTICAL_FINANCE_TERM { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE> OPTICAL_NE { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_AREA> OPTICAL_NE_AREA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_SUBRACK> OPTICAL_NE_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_TYPE> OPTICAL_NE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_PORT> OPTICAL_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_PORT_SFP> OPTICAL_PORT_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_PORT_TYPE> OPTICAL_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_RACK> OPTICAL_RACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_SFP> OPTICAL_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_SUBRACK> OPTICAL_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_SUBRACK_BOARD> OPTICAL_SUBRACK_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_SUBRACK_TYPE> OPTICAL_SUBRACK_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_WO_NE> OPTICAL_WO_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_WORK_ORDER> OPTICAL_WORK_ORDER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_BOARD> FIREWALL_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_BOARD_SUBBOARD> FIREWALL_BOARD_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_BOARD_TYPE> FIREWALL_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_CPU> FIREWALL_CPU { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE> FIREWALL_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_BOARD> FIREWALL_NE_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_SITE> FIREWALL_NE_SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_TYPE> FIREWALL_NE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_PORT> FIREWALL_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_PORT_TYPE> FIREWALL_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_SUBBOARD> FIREWALL_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_SUBBOARD_TYPE> FIREWALL_SUBBOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_BOARD> MW_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_BOARD_TYPE> MW_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE> MW_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_BOARD> MW_NE_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_SITE> MW_NE_SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_TYPE> MW_NE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_PORT> MW_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_PORT_SFP> MW_PORT_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_PORT_TYPE> MW_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_SFP> MW_SFP { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_SUBRACK_TYPE> MW_SUBRACK_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.AREA> AREA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.SUBAREA> SUBAREA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.REGION> REGION { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.ZONE> ZONE { get; set; }
        //public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.NeTreeModeView.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.ROUTER_SUBBOARDS> RouterSubBoards { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.ROUTER_PORTS> ROUTER_PORTS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.ROUTER_SFPS> ROUTER_SFPS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_BOARDS> OPTICAL_NE_BOARDS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_PORTS> OPTICAL_NE_PORTS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_SFPS> OPTICAL_NE_SFPS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_PORTS> MW_NE_PORTS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_SFP> MW_NE_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_PORTS> FIREWALL_NE_PORTS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_SUBBOARDS> FIREWALL_NE_SUBBOARDS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.MW_NE_DETAILS> MW_NE_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.DATACOM_NE_DETAILS> DATACOM_NE_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.OPTICAL_NE_DETAILS> OPTICAL_NE_DETAILS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.TranssmissionOSS.API.Models.FIREWALL_NE_DETAILS> FIREWALL_NE_DETAILS { get; set; }
    }
}