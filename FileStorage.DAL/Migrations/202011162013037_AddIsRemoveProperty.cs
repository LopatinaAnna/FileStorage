namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsRemoveProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "IsRemove", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Files", "IsRemove");
        }
    }
}