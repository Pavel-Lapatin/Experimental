using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NetMastery.InventoryManager.Startup))]
namespace NetMastery.InventoryManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
