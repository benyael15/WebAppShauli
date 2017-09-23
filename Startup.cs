using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShauliFinalProject.Startup))]
namespace ShauliFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
