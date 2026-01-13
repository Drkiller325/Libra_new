namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correct_Issue_Issuetype_relation : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.IssueTypes", new[] { "ParentIssueId" });
            DropColumn("dbo.Issues", "SubTypeId");
            RenameColumn(table: "dbo.Issues", name: "ParentIssueId", newName: "SubTypeId");
            AlterColumn("dbo.Issues", "ProblemId", c => c.Int());
            CreateIndex("dbo.Issues", "ProblemId");
            AddForeignKey("dbo.Issues", "ProblemId", "dbo.IssueTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "ProblemId", "dbo.IssueTypes");
            DropIndex("dbo.Issues", new[] { "ProblemId" });
            AlterColumn("dbo.Issues", "ProblemId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Issues", name: "SubTypeId", newName: "ParentIssueId");
            AddColumn("dbo.Issues", "SubTypeId", c => c.Int());
            CreateIndex("dbo.IssueTypes", "ParentIssueId");
        }
    }
}
