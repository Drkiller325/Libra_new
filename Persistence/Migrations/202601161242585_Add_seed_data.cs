namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_seed_data : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Issues", new[] { "AssignedId" });
            AlterColumn("dbo.Pos", "MorningOpening", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Pos", "MorningClosing", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Pos", "AfternoonOpening", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Pos", "AfternoonClosing", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Issues", "Solution", c => c.String(maxLength: 300));
            AlterColumn("dbo.Issues", "AssignedId", c => c.Int());
            CreateIndex("dbo.Issues", "AssignedId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Issues", new[] { "AssignedId" });
            AlterColumn("dbo.Issues", "AssignedId", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "Solution", c => c.String());
            AlterColumn("dbo.Pos", "AfternoonClosing", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pos", "AfternoonOpening", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pos", "MorningClosing", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pos", "MorningOpening", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Issues", "AssignedId");
        }
    }
}
