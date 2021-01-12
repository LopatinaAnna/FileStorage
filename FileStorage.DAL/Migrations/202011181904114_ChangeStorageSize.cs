namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeStorageSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.Int());
        }

        public override void Down()
        {
            AlterColumn("dbo.Storages", "StorageSize", c => c.String());
        }
    }
}