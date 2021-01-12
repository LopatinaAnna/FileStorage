namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddCreationDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "CreationDate", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Files", "CreationDate");
        }
    }
}