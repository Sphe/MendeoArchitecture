using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mendeo.Mercurius.Bootstrap.Startup))]
namespace Mendeo.Mercurius.Bootstrap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
