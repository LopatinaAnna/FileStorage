namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNameToProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientProfiles", "Name", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ClientProfiles", "Name");
        }
    }
}