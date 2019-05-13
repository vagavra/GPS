namespace GPS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characteristics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ElementId = c.Int(nullable: false),
                        CharacteristicType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Element", t => t.ElementId, cascadeDelete: true)
                .Index(t => t.ElementId);
            
            CreateTable(
                "dbo.Element",
                c => new
                    {
                        ElementId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsNode = c.Boolean(nullable: false),
                        StartId = c.Int(),
                        EndId = c.Int(),
                        SingleDirection = c.Boolean(),
                        Distance = c.Double(),
                        X = c.Int(),
                        Y = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ElementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characteristics", "ElementId", "dbo.Element");
            DropIndex("dbo.Characteristics", new[] { "ElementId" });
            DropTable("dbo.Element");
            DropTable("dbo.Characteristics");
        }
    }
}
