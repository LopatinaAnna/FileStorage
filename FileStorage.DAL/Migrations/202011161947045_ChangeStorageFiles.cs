namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeStorageFiles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "Storage_Id", "dbo.Storages");
            DropForeignKey("dbo.Files", "Storage_Id1", "dbo.Storages");
            DropIndex("dbo.Files", new[] { "Storage_Id" });
            DropIndex("dbo.Files", new[] { "Storage_Id1" });
            DropColumn("dbo.Files", "Storage_Id");
            DropColumn("dbo.Files", "Storage_Id1");
        }

        public override void Down()
        {
            AddColumn("dbo.Files", "Storage_Id1", c => c.Int());
            AddColumn("dbo.Files", "Storage_Id", c => c.Int());
            CreateIndex("dbo.Files", "Storage_Id1");
            CreateIndex("dbo.Files", "Storage_Id");
            AddForeignKey("dbo.Files", "Storage_Id1", "dbo.Storages", "Id");
            AddForeignKey("dbo.Files", "Storage_Id", "dbo.Storages", "Id");
        }
    }
}