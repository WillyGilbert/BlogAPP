namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameToQuesAnsComm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "UserName", c => c.String());
            AddColumn("dbo.Comments", "UserName", c => c.String());
            AddColumn("dbo.Questions", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "UserName");
            DropColumn("dbo.Comments", "UserName");
            DropColumn("dbo.Answers", "UserName");
        }
    }
}
