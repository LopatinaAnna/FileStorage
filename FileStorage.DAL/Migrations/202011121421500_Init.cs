namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Path = c.String(),
                    StorageId = c.Int(nullable: false),
                    Storage_Id = c.Int(),
                    Storage_Id1 = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Storages", t => t.Storage_Id)
                .ForeignKey("dbo.Storages", t => t.Storage_Id1)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.Storage_Id)
                .Index(t => t.Storage_Id1);

            CreateTable(
                "dbo.Storages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Files", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.Files", "Storage_Id1", "dbo.Storages");
            DropForeignKey("dbo.Files", "Storage_Id", "dbo.Storages");
            DropIndex("dbo.Files", new[] { "Storage_Id1" });
            DropIndex("dbo.Files", new[] { "Storage_Id" });
            DropIndex("dbo.Files", new[] { "StorageId" });
            DropTable("dbo.Storages");
            DropTable("dbo.Files");
        }
    }
}