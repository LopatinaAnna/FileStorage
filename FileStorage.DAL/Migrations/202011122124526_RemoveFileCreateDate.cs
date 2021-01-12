namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemoveFileCreateDate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "CreationDate");
        }

        public override void Down()
        {
            AddColumn("dbo.Files", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}