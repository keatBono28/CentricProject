using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CentricProject.Startup))]
namespace CentricProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
