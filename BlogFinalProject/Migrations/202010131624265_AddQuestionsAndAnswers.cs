﻿namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionsAndAnswers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.QuestionId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Like = c.Boolean(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
