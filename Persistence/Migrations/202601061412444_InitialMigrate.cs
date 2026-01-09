namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Telephone = c.String(maxLength: 50),
                        Cellphone = c.String(maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        Model = c.String(nullable: false, maxLength: 50),
                        Brand = c.String(maxLength: 50),
                        DaysClosed = c.String(),
                        MorningOpening = c.DateTime(nullable: false),
                        MorningClosing = c.DateTime(nullable: false),
                        AfternoonOpening = c.DateTime(nullable: false),
                        AfternoonClosing = c.DateTime(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        CityId = c.Int(nullable: false),
                        ConnectionTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.ConnectionTypes", t => t.ConnectionTypeId)
                .Index(t => t.CityId)
                .Index(t => t.ConnectionTypeId);
            
            CreateTable(
                "dbo.ConnectionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Priority = c.String(nullable: false, maxLength: 10),
                        Memo = c.String(maxLength: 128),
                        Description = c.String(maxLength: 300),
                        Solution = c.String(),
                        ProblemId = c.Int(nullable: false),
                        AssignedId = c.Int(nullable: false),
                        PosId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        SubTypeId = c.Int(),
                        StatusId = c.Int(nullable: false),
                        CreatedById = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        LastModifiedById = c.Int(),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.AssignedId)
                .ForeignKey("dbo.Users", t => t.CreatedById)
                .ForeignKey("dbo.Users", t => t.LastModifiedById)
                .ForeignKey("dbo.Pos", t => t.PosId)
                .ForeignKey("dbo.IssueStatus", t => t.StatusId)
                .ForeignKey("dbo.IssueTypes", t => t.SubTypeId)
                .ForeignKey("dbo.IssueTypes", t => t.TypeId)
                .Index(t => t.AssignedId)
                .Index(t => t.PosId)
                .Index(t => t.TypeId)
                .Index(t => t.SubTypeId)
                .Index(t => t.StatusId)
                .Index(t => t.CreatedById)
                .Index(t => t.LastModifiedById);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 128),
                        Login = c.String(nullable: false, maxLength: 10),
                        PasswordHash = c.String(),
                        Telephone = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        UserTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeId)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Login, unique: true)
                .Index(t => t.UserTypeId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Action = c.String(nullable: false, maxLength: 50),
                        Notes = c.String(maxLength: 300),
                        InsertDate = c.DateTime(nullable: false),
                        IssueId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.IssueId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.IssueId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IssueStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IssueTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueLevel = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        ParentIssueId = c.Int(),
                        InsertDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IssueTypes", t => t.ParentIssueId)
                .Index(t => t.ParentIssueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "TypeId", "dbo.IssueTypes");
            DropForeignKey("dbo.Issues", "SubTypeId", "dbo.IssueTypes");
            DropForeignKey("dbo.IssueTypes", "ParentIssueId", "dbo.IssueTypes");
            DropForeignKey("dbo.Issues", "StatusId", "dbo.IssueStatus");
            DropForeignKey("dbo.Issues", "PosId", "dbo.Pos");
            DropForeignKey("dbo.Issues", "LastModifiedById", "dbo.Users");
            DropForeignKey("dbo.Issues", "CreatedById", "dbo.Users");
            DropForeignKey("dbo.Issues", "AssignedId", "dbo.UserTypes");
            DropForeignKey("dbo.Users", "UserTypeId", "dbo.UserTypes");
            DropForeignKey("dbo.Logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Logs", "IssueId", "dbo.Issues");
            DropForeignKey("dbo.Pos", "ConnectionTypeId", "dbo.ConnectionTypes");
            DropForeignKey("dbo.Pos", "CityId", "dbo.Cities");
            DropIndex("dbo.IssueTypes", new[] { "ParentIssueId" });
            DropIndex("dbo.Logs", new[] { "UserId" });
            DropIndex("dbo.Logs", new[] { "IssueId" });
            DropIndex("dbo.Users", new[] { "UserTypeId" });
            DropIndex("dbo.Users", new[] { "Login" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Issues", new[] { "LastModifiedById" });
            DropIndex("dbo.Issues", new[] { "CreatedById" });
            DropIndex("dbo.Issues", new[] { "StatusId" });
            DropIndex("dbo.Issues", new[] { "SubTypeId" });
            DropIndex("dbo.Issues", new[] { "TypeId" });
            DropIndex("dbo.Issues", new[] { "PosId" });
            DropIndex("dbo.Issues", new[] { "AssignedId" });
            DropIndex("dbo.Pos", new[] { "ConnectionTypeId" });
            DropIndex("dbo.Pos", new[] { "CityId" });
            DropTable("dbo.IssueTypes");
            DropTable("dbo.IssueStatus");
            DropTable("dbo.Logs");
            DropTable("dbo.Users");
            DropTable("dbo.UserTypes");
            DropTable("dbo.Issues");
            DropTable("dbo.ConnectionTypes");
            DropTable("dbo.Pos");
            DropTable("dbo.Cities");
        }
    }
}
