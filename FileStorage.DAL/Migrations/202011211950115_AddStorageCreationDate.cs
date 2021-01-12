namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddStorageCreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Storages", "CreationDate", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Storages", "CreationDate");
        }
    }
}