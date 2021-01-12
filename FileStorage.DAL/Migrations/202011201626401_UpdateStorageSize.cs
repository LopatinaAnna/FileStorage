namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateStorageSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.Int());
        }
    }
}