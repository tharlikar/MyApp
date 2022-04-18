using Owin;

namespace com.minsoehanwin.sample.Wcf.RestfulService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
