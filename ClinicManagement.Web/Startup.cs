using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicManagement.Web.Startup))]
namespace ClinicManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
