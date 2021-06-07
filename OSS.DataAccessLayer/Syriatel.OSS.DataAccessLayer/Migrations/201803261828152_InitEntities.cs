namespace Syriatel.OSS.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OSS_TEST.OSS_CATEGORY",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Order = c.Decimal(nullable: false, precision: 10, scale: 0),
                        IsOpen = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Title = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                        ModuleBarId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_MODULEBAR", t => t.ModuleBarId, cascadeDelete: true)
                .Index(t => t.ModuleBarId);
            
            CreateTable(
                "OSS_TEST.OSS_MODULEBAR",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SearchFlag = c.Decimal(nullable: false, precision: 10, scale: 0),
                        ModuleId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_MODULE", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "OSS_TEST.OSS_MODULE",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        RootName = c.String(maxLength: 250),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "OSS_TEST.OSS_SUBCATEGORY",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Level = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Order = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 250),
                        ParentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        CategoryId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_CATEGORY", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "OSS_TEST.OSS_SUBCATEGORYTREEROOT",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SearchFlag = c.Decimal(nullable: false, precision: 10, scale: 0),
                        DesignFile = c.String(maxLength: 250),
                        SubCategoryId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_SUBCATEGORY", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "OSS_TEST.OSS_SUBCATEGORYTREENODE",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Level = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Order = c.Decimal(nullable: false, precision: 10, scale: 0),
                        Name = c.String(maxLength: 250),
                        ParentId = c.Decimal(nullable: false, precision: 10, scale: 0),
                        SubCategoryTreeRootId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_SUBCATEGORYTREEROOT", t => t.SubCategoryTreeRootId, cascadeDelete: true)
                .Index(t => t.SubCategoryTreeRootId);
            
            CreateTable(
                "OSS_TEST.OSS_DATAFIELD",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 10, scale: 0),
                        RelatedApi = c.String(maxLength: 250),
                        SubCategoryTreeNodeId = c.Decimal(nullable: false, precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OSS_TEST.OSS_SUBCATEGORYTREENODE", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("OSS_TEST.OSS_SUBCATEGORYTREENODE", "SubCategoryTreeRootId", "OSS_TEST.OSS_SUBCATEGORYTREEROOT");
            DropForeignKey("OSS_TEST.OSS_DATAFIELD", "Id", "OSS_TEST.OSS_SUBCATEGORYTREENODE");
            DropForeignKey("OSS_TEST.OSS_SUBCATEGORYTREEROOT", "Id", "OSS_TEST.OSS_SUBCATEGORY");
            DropForeignKey("OSS_TEST.OSS_SUBCATEGORY", "CategoryId", "OSS_TEST.OSS_CATEGORY");
            DropForeignKey("OSS_TEST.OSS_MODULEBAR", "Id", "OSS_TEST.OSS_MODULE");
            DropForeignKey("OSS_TEST.OSS_CATEGORY", "ModuleBarId", "OSS_TEST.OSS_MODULEBAR");
            DropIndex("OSS_TEST.OSS_DATAFIELD", new[] { "Id" });
            DropIndex("OSS_TEST.OSS_SUBCATEGORYTREENODE", new[] { "SubCategoryTreeRootId" });
            DropIndex("OSS_TEST.OSS_SUBCATEGORYTREEROOT", new[] { "Id" });
            DropIndex("OSS_TEST.OSS_SUBCATEGORY", new[] { "CategoryId" });
            DropIndex("OSS_TEST.OSS_MODULEBAR", new[] { "Id" });
            DropIndex("OSS_TEST.OSS_CATEGORY", new[] { "ModuleBarId" });
            DropTable("OSS_TEST.OSS_DATAFIELD");
            DropTable("OSS_TEST.OSS_SUBCATEGORYTREENODE");
            DropTable("OSS_TEST.OSS_SUBCATEGORYTREEROOT");
            DropTable("OSS_TEST.OSS_SUBCATEGORY");
            DropTable("OSS_TEST.OSS_MODULE");
            DropTable("OSS_TEST.OSS_MODULEBAR");
            DropTable("OSS_TEST.OSS_CATEGORY");
        }
    }
}
