using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogFinalProject.Startup))]
namespace BlogFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
