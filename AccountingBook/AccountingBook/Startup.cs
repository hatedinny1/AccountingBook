using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AccountingBook.Startup))]
namespace AccountingBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
