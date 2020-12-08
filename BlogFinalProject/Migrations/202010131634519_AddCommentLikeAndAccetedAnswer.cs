namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentLikeAndAccetedAnswer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Answers", "Like", c => c.Boolean());
            AddColumn("dbo.Answers", "AcceptedAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Comments", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "AnswerId" });
            DropIndex("dbo.Comments", new[] { "QuestionId" });
            DropColumn("dbo.Answers", "AcceptedAnswer");
            DropColumn("dbo.Answers", "Like");
            DropTable("dbo.Comments");
        }
    }
}
