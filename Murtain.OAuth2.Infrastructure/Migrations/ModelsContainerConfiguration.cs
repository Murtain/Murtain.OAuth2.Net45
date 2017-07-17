namespace Murtain.OAuth2.Infrastructure.Migrations
{
    using Murtain.Runtime.Security;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ModelsContainerConfiguration : DbMigrationsConfiguration<Murtain.OAuth2.Infrastructure.ModelsContainer>
    {
        public ModelsContainerConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Murtain.OAuth2.Infrastructure.ModelsContainer context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.UserAccount.AddOrUpdate(x => x.Mobile, new Domain.Entities.UserAccount
            {
                Mobile = "15618275259",
                Password = CryptoManager.EncryptMD5("qwe123").ToUpper(),
                Avatar = "https://s1.mi-img.com/mfsv2/avatar/fdsc3/p01ipyFBOr98/GOF2ZyTtU5ejqs_320.jpg",
                Age = 27,
                Birthday = "1991-06-09",
                City = "上海",
                Country = "中国",
                Email = "392327013@qq.com",
                Name = "穆轻寒",
                NickName = "Lara",
                Address = "上海市闵行区浦江镇江文路688弄",
                Gender = "male",
                Province = "上海",
                SubId = Guid.NewGuid().ToString("N").ToUpper()

            });
        }
    }
}
