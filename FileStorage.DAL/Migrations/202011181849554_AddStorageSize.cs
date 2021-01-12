namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddStorageSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storages", "StorageSize", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Storages", "StorageSize");
        }
    }
}