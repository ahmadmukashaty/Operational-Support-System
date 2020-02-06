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
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_CANDIDATE> SITE_CANDIDATE { get; set; }
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
        
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_RACK_SUBRACK> RADIO_RACK_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SUBRACK> RADIO_SUBRACK { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SLOT> RADIO_SLOT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_BOARD> RADIO_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_SLOT_BOARD> RADIO_SLOT_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_PORT> RADIO_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.ANTENNA> ANTENNA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_ANTENNA> SITE_IDENTITY_ANTENNA { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.CELL> CELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.GCELL> GCELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.UCELL> UCELL { get; set; }

        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.ANTENNA_CELL> ANTENNA_CELL { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RADIO_HOSTVER> RADIO_HOSTVER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.SITE_IDENTITY_RADIO_HOSTVER> SITE_IDENTITY_RADIO_HOSTVER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_CONTROLLER> RAN_CONTROLLER { get; set; }
        public System.Data.Entity.DbSet<Syriatel.RadioOSS.API.Models.RAN_NETYPE> RAN_NETYPE { get; set; }


       





    }
}