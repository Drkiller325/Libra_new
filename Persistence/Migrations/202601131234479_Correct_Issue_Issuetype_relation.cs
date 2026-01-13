namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correct_Issue_Issuetype_relation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IssueTypes", new[] { "ParentIssueId" });
            RenameColumn(table: "dbo.Issues", name: "ParentIssueId", newName: "IssueType_Id1");
            AddColumn("dbo.Issues", "IssueType_Id", c => c.Int());
            AlterColumn("dbo.Issues", "ProblemId", c => c.Int());
            CreateIndex("dbo.Issues", "ProblemId");
            CreateIndex("dbo.Issues", "IssueType_Id");
            CreateIndex("dbo.Issues", "IssueType_Id1");
            AddForeignKey("dbo.Issues", "IssueType_Id", "dbo.IssueTypes", "Id");
            AddForeignKey("dbo.Issues", "ProblemId", "dbo.IssueTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "ProblemId", "dbo.IssueTypes");
            DropForeignKey("dbo.Issues", "IssueType_Id", "dbo.IssueTypes");
            DropIndex("dbo.Issues", new[] { "IssueType_Id1" });
            DropIndex("dbo.Issues", new[] { "IssueType_Id" });
            DropIndex("dbo.Issues", new[] { "ProblemId" });
            AlterColumn("dbo.Issues", "ProblemId", c => c.Int(nullable: false));
            DropColumn("dbo.Issues", "IssueType_Id");
            RenameColumn(table: "dbo.Issues", name: "IssueType_Id1", newName: "ParentIssueId");
            CreateIndex("dbo.IssueTypes", "ParentIssueId");
        }
    }
}
