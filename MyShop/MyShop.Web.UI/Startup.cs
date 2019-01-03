using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShop.Web.UI.Startup))]
namespace MyShop.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
