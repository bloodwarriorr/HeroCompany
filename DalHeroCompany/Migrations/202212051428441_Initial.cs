namespace HeroCompany.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Heroes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Ability = c.String(),
                        TrainBeginning = c.DateTime(nullable: false),
                        SuitColors = c.String(),
                        StartingPower = c.Int(nullable: false),
                        CurrentPower = c.Int(nullable: false),
                        TrainerId = c.Guid(nullable: false),
                        TrainCount = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainers");
            DropTable("dbo.Heroes");
        }
    }
}
