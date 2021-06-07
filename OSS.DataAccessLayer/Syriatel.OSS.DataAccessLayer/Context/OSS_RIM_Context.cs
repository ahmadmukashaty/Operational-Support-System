using Oracle.ManagedDataAccess.Client;
using Syriatel.OSS.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syriatel.OSS.Context
{
    public class OSS_RIM_Context : DbContext
    {
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleBar> ModuleBars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }


        public DbSet<SubCategoryTreeRoot> SubCategoryTreeRoots { get; set; }

        public DbSet<SubCategoryTreeNode> SubCategoryTreeNodes { get; set; }

        public DbSet<DataField> DataFields { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder = modelBuilder.HasDefaultSchema("OSS_TEST");
            modelBuilder.Types().Configure(entity => entity.ToTable("OSS_" + entity.ClrType.Name.ToUpper()));
            base.OnModelCreating(modelBuilder);
        }

        public OSS_RIM_Context(bool isReading = true)
            : base(new OracleConnection(@"Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.253.23.164)(PORT=1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME=testDB))); User Id = OSS_TEST; Password = oss_test_123"), true)
        {
            //Configuration.LazyLoadingEnabled = false;

            this.Configuration.ProxyCreationEnabled = isReading;

            // Database.SetInitializer<OSS_RIM_Context>(null);
        }


        public OSS_RIM_Context()
            : base(new OracleConnection(@"Data Source = (DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.253.23.164)(PORT=1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME=testDB))); User Id = OSS_TEST; Password = oss_test_123"), true)
        {

        }
    }
}
