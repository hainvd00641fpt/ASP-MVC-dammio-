using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneCMVC.Startup))]
namespace OneCMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
