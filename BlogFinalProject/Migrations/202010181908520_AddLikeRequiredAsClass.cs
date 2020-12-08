namespace BlogFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeRequiredAsClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Likes", "IsLiked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Likes", "IsLiked", c => c.Boolean());
        }
    }
}
