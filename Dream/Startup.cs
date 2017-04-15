using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dream.Startup))]
namespace Dream
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
