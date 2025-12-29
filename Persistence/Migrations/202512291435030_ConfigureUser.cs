namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigureUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 128),
                        Login = c.String(nullable: false, maxLength: 10),
                        Telephone = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Login, unique: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Users", new[] { "Login" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropTable("dbo.UserTypes");
            DropTable("dbo.Users");
        }
    }
}
