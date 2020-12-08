namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangeIntToStringUserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Answers", "UserId");
            DropColumn("dbo.Comments", "UserId");
            RenameColumn(table: "dbo.Answers", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Answers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Answers", "UserId");
            CreateIndex("dbo.Comments", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Answers", new[] { "UserId" });
            AlterColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Answers", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Answers", "User_Id");
        }
    }
}
