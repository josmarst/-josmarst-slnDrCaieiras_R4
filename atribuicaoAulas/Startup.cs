using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(atribuicaoAulas.Startup))]
namespace atribuicaoAulas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
