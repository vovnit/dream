namespace Dream.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "AuthorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "AuthorId", c => c.Int(nullable: false));
        }
    }
}
