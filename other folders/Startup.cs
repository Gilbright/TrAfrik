using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TurAfrikb.Startup))]
namespace TurAfrikb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
