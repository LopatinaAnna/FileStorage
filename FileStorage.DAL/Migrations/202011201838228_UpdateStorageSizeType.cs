namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateStorageSizeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.Long(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.Int(nullable: false));
        }
    }
}