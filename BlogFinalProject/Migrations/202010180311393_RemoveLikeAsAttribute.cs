namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLikeAsAttribute : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Questions", "Like");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Like", c => c.Boolean());
        }
    }
}
