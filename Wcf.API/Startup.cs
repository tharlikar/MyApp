using Owin;

namespace com.minsoehanwin.sample.Wcf.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
