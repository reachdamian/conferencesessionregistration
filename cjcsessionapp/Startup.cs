using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cjcsessionapp.Startup))]
namespace cjcsessionapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
