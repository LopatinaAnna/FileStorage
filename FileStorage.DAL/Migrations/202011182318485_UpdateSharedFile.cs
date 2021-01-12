namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateSharedFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SharedFiles", "UserName", c => c.String());
            DropColumn("dbo.Files", "SharedLink");
        }

        public override void Down()
        {
            AddColumn("dbo.Files", "SharedLink", c => c.String());
            DropColumn("dbo.SharedFiles", "UserName");
        }
    }
}