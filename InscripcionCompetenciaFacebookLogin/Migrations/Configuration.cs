namespace InscripcionCompetenciaFacebookLogin.Migrations
{
    using InscripcionCompetenciaFacebookLogin.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<InscripcionCompetenciaFacebookLogin.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "InscripcionCompetenciaFacebookLogin.Models.ApplicationDbContext";
        }

        protected override void Seed(InscripcionCompetenciaFacebookLogin.Models.ApplicationDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var rolestore = new RoleStore<IdentityRole>(context);
                var store = new UserStore<User>(context);
                var manager = new ApplicationUserManager(store);
                var roleManager = new ApplicationRoleManager(rolestore);

                var admin = new User { UserName = "admin@cit.com", Email = "admin@cit.com" };
                var rolename = "Admin";

                manager.Create(admin, "admin123");

                if (!roleManager.RoleExists(rolename))
                {
                    roleManager.Create(new IdentityRole(rolename));
                }

                if (!manager.IsInRole(admin.Id, rolename))
                {
                    manager.AddToRole(admin.Id, rolename);
                }

                for (int index = 1; index <= 10; index++)
                {
                    var user = new User { UserName = "test" + index, Nombre = "test", Apellido = "test" };
                    manager.Create(user, "test");

                    var registro = new Registro { Titulo = "idea" + index, Contenido = "un test", Usuario = user };
                    context.Registros.Add(registro);
                }

                context.SaveChanges();
            }
        }
    }
}
