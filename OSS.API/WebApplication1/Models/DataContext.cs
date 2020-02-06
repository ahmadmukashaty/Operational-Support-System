using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Syriatel.OSS.API.Models;

namespace Syriatel.OSS.API.Models
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
            : base("name=WebApplication2Context")
        {
            Database.SetInitializer<DataContext>(null);
            this.Configuration.ProxyCreationEnabled = false;
       //     this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("OSS_TEST");
            base.OnModelCreating(modelBuilder);
        }


        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_CATEGORY> RIM_CATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_BOARD_SUBBOARD> DATACOM_BOARD_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_BOARD_TYPE> DATACOM_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_NE_BOARD> DATACOM_NE_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_NE_SITE> DATACOM_NE_SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_NE_Type> DATACOM_NE_Type { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_PORT_SFP> DATACOM_PORT_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_PORT_TYPE> DATACOM_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_SUBBOARD> DATACOM_SUBBOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_SUBBOARD_TYPE> DATACOM_SUBBOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOME_BOARD> DATACOME_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_SUBCATEGORY> RIM_SUBCATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_NE> DATACOM_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_PORT> DATACOM_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.DATACOM_SFP> DATACOM_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_BOARD> MW_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_BOARD_TYPE> MW_BOARD_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_NE> MW_NE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_NE_BOARD> MW_NE_BOARD { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_NE_SITE> MW_NE_SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_NE_TYPE> MW_NE_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_PORT> MW_PORT { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_PORT_SFP> MW_PORT_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_PORT_TYPE> MW_PORT_TYPE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_SFP> MW_SFP { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.MW_SUBRACK_TYPE> MW_SUBRACK_TYPE { get; set; } 
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.SITE> SITE { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_CATEGORIES> RIM_CATEGORIES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_SUBCATEGORIES> RIM_SUBCATEGORIES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_MODULES> RIM_MODULES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_NODE_TABLES> RIM_NODE_TABLES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_LEVELS> RIM_LEVELS { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_LEVEL_NODES> RIM_LEVEL_NODES { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.CATEGORY> CATEGORY { get; set; }
        public System.Data.Entity.DbSet<Syriatel.OSS.API.Models.RIM_ATTRIBUTES> RIM_ATTRIBUTES { get; set; } 

        

    }

}
