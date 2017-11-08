using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S01_PersonalBlog.Startup))]
namespace S01_PersonalBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateAdminUser();
        }

    }
}
