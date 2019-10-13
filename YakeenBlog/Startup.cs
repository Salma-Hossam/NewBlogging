using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YakeenBlog.Startup))]
namespace YakeenBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
