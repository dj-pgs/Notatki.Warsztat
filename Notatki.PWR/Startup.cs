using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Notatki.PWR.Startup))]
namespace Notatki.PWR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
