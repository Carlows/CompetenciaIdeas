using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using InscripcionCompetenciaFacebookLogin.Migrations;

namespace InscripcionCompetenciaFacebookLogin.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Votos = new List<Voto>();
        }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public virtual Registro Registro { get; set; }
        public virtual List<Voto> Votos { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            // Enable-Migrations -EnableAutomaticMigrations
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            // We swap to this if we want to re-create the database (yeah, not a great way of doing it)
            //Database.SetInitializer<ApplicationDbContext>(new DbInitializer());
        }

        public DbSet<Registro> Registros { get; set; }
        public DbSet<Voto> Votos { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);
        }
    }
}