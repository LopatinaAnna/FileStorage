namespace FileStorage.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddSharedFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SharedFiles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Path = c.String(),
                    Type = c.String(),
                    Length = c.Int(nullable: false),
                    SharedLink = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.SharedFiles");
        }
    }
}