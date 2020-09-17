using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileSystem_Finance_Department_.Startup))]
namespace FileSystem_Finance_Department_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
