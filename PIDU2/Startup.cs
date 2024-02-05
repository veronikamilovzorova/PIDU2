using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PIDU2.Startup))]
namespace PIDU2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
