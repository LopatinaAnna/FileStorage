namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddFileProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Type", c => c.String());
            AddColumn("dbo.Files", "Length", c => c.Int(nullable: false));
            AddColumn("dbo.Files", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Storages", "UserName", c => c.String());
            DropColumn("dbo.Storages", "UserId");
        }

        public override void Down()
        {
            AddColumn("dbo.Storages", "UserId", c => c.String());
            DropColumn("dbo.Storages", "UserName");
            DropColumn("dbo.Files", "CreationDate");
            DropColumn("dbo.Files", "Length");
            DropColumn("dbo.Files", "Type");
        }
    }
}