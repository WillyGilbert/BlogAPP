namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeAsClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserName = c.String(),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                        IsLiked = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId);
            
            DropColumn("dbo.Answers", "Like");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Like", c => c.Boolean());
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Likes", "AnswerId", "dbo.Answers");
            DropIndex("dbo.Likes", new[] { "AnswerId" });
            DropIndex("dbo.Likes", new[] { "QuestionId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropTable("dbo.Likes");
        }
    }
}
