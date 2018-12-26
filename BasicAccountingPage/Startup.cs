using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicAccountingPage.Startup))]
namespace BasicAccountingPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
