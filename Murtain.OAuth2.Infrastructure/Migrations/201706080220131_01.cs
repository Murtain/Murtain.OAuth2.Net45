namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UX_GLOBAL_SETTING",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NAME = c.String(nullable: false, maxLength: 256),
                        DISPLAY_NAME = c.String(nullable: false, maxLength: 256),
                        VALUE = c.String(),
                        DESCRIPTION = c.String(maxLength: 2000),
                        SCOPE = c.Int(nullable: false),
                        GROUP = c.String(maxLength: 250),
                        CREATE_TIME = c.DateTime(),
                        CREATE_USER = c.String(maxLength: 50),
                        CHANGE_USER = c.String(maxLength: 50),
                        CHANGE_TIME = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UX_USER_ACCOUNT",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        NAME = c.String(maxLength: 50),
                        NICK_NAME = c.String(maxLength: 50),
                        BIRTHDAY = c.String(maxLength: 50),
                        AGE = c.Int(nullable: false),
                        MOBILE = c.String(maxLength: 50),
                        EMAIL = c.String(maxLength: 50),
                        ADDRESS = c.String(maxLength: 250),
                        CITY = c.String(maxLength: 50),
                        PROVINCE = c.String(maxLength: 50),
                        COUNTRY = c.String(maxLength: 50),
                        GENDER = c.String(maxLength: 50),
                        AVATAR = c.String(maxLength: 2000),
                        PASSWORD = c.String(maxLength: 250),
                        SUB_ID = c.String(nullable: false, maxLength: 50),
                        LOGIN_PROVIDER = c.String(maxLength: 50),
                        LOGIN_PROVIDER_ID = c.String(maxLength: 50),
                        IS_DELETED = c.Boolean(nullable: false),
                        CREATE_TIME = c.DateTime(),
                        CREATE_USER = c.String(maxLength: 50),
                        CHANGE_USER = c.String(maxLength: 50),
                        CHANGE_TIME = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UX_USER_ACCOUNT");
            DropTable("dbo.UX_GLOBAL_SETTING");
        }
    }
}
