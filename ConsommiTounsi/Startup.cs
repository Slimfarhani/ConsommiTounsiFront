using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsommiTounsi.Startup))]
namespace ConsommiTounsi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
