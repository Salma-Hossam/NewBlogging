namespace YakeenBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makePostTextRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Post", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Comment", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comment", "Text", c => c.String());
            AlterColumn("dbo.Post", "Text", c => c.String());
            AlterColumn("dbo.Post", "Address", c => c.String());
        }
    }
}
