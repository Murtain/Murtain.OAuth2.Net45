namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _01 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.GlobalSettings",
            //    c => new
            //        {
            //            Id = c.Long(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 256),
            //            DisplayName = c.String(nullable: false, maxLength: 256),
            //            Value = c.String(),
            //            Description = c.String(maxLength: 2000),
            //            Scope = c.Int(nullable: false),
            //            Group = c.String(maxLength: 250),
            //            CreateTime = c.DateTime(),
            //            CreateUser = c.String(maxLength: 50),
            //            ChangeUser = c.String(maxLength: 50),
            //            ChangeTime = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserAccounts",
            //    c => new
            //        {
            //            Id = c.Long(nullable: false, identity: true),
            //            OpenId = c.String(maxLength: 50),
            //            Name = c.String(maxLength: 50),
            //            NickName = c.String(maxLength: 50),
            //            Birthday = c.DateTime(),
            //            Telphone = c.String(maxLength: 50),
            //            Email = c.String(maxLength: 50),
            //            Street = c.String(maxLength: 250),
            //            City = c.String(maxLength: 50),
            //            Province = c.String(maxLength: 50),
            //            Country = c.String(maxLength: 50),
            //            Sex = c.Byte(),
            //            Headimageurl = c.String(maxLength: 2000),
            //            IdentityNo = c.String(maxLength: 50),
            //            Password = c.String(maxLength: 250),
            //            Salt = c.String(maxLength: 50),
            //            Subject = c.String(nullable: false, maxLength: 50),
            //            IsDeleted = c.Boolean(nullable: false),
            //            CreateTime = c.DateTime(),
            //            CreateUser = c.String(maxLength: 50),
            //            ChangeUser = c.String(maxLength: 50),
            //            ChangeTime = c.DateTime(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
            DropTable("dbo.GlobalSettings");
        }
    }
}
