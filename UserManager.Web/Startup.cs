using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserManager.Web.Startup))]
namespace UserManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
