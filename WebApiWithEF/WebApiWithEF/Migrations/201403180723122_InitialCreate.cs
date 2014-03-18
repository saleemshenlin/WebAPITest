namespace WebApiWithEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.POI",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        C_ID = c.Int(nullable: false),
                        Name = c.String(),
                        D_Name = c.String(),
                        Address = c.String(),
                        Time = c.String(),
                        Tele = c.String(),
                        Abstract = c.String(),
                        Ticket = c.String(),
                        Type = c.String(),
                        Geometry = c.String(),
                        Status = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.POI");
        }
    }
}
