using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TigerPaws.Startup))]
namespace TigerPaws
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
