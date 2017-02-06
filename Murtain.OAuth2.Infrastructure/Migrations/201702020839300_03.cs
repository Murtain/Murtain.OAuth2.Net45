namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "Mobile", c => c.String(maxLength: 50));
            AddColumn("dbo.UserAccounts", "Avatar", c => c.String(maxLength: 2000));
            AddColumn("dbo.UserAccounts", "SubjectId", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.UserAccounts", "Telphone");
            DropColumn("dbo.UserAccounts", "Headimageurl");
            DropColumn("dbo.UserAccounts", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccounts", "Subject", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.UserAccounts", "Headimageurl", c => c.String(maxLength: 2000));
            AddColumn("dbo.UserAccounts", "Telphone", c => c.String(maxLength: 50));
            DropColumn("dbo.UserAccounts", "SubjectId");
            DropColumn("dbo.UserAccounts", "Avatar");
            DropColumn("dbo.UserAccounts", "Mobile");
        }
    }
}
