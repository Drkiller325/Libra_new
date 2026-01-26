namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditConfiguration : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Login" });
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Pos", "Telephone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pos", "Cellphone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Pos", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Pos", "Model", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Pos", "Brand", c => c.String(maxLength: 25));
            AlterColumn("dbo.ConnectionTypes", "ConnType", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.UserTypes", "Type", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.IssueStatus", "Status", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Users", "Email", unique: true);
            CreateIndex("dbo.Users", "Login", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Login" });
            DropIndex("dbo.Users", new[] { "Email" });
            AlterColumn("dbo.IssueStatus", "Status", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Login", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserTypes", "Type", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ConnectionTypes", "ConnType", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pos", "Brand", c => c.String(maxLength: 50));
            AlterColumn("dbo.Pos", "Model", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pos", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Pos", "Cellphone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Pos", "Telephone", c => c.String(maxLength: 50));
            AlterColumn("dbo.Cities", "CityName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Users", "Login", unique: true);
            CreateIndex("dbo.Users", "Email", unique: true);
        }
    }
}
