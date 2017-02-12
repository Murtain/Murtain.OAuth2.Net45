namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccounts", "LoginProvider", c => c.String(maxLength: 50));
            AddColumn("dbo.UserAccounts", "LoginProviderId", c => c.String(maxLength: 50));
            DropColumn("dbo.UserAccounts", "OpenId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccounts", "OpenId", c => c.String(maxLength: 50));
            DropColumn("dbo.UserAccounts", "LoginProviderId");
            DropColumn("dbo.UserAccounts", "LoginProvider");
        }
    }
}
