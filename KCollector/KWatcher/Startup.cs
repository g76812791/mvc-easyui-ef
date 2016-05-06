using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KWatcher.Startup))]
namespace KWatcher
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
