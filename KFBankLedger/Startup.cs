using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KFBankLedger.Startup))]
namespace KFBankLedger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
