namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSharedLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "SharedLink", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Files", "SharedLink");
        }
    }
}