using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InscripcionCompetenciaFacebookLogin.Startup))]
namespace InscripcionCompetenciaFacebookLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
