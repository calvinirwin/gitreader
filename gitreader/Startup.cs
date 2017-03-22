using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gitreader.Startup))]
namespace gitreader
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
