using System;
using System.Collections.Generic;
using System.Linq;
using HeroCompanyApi;
using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;

[assembly: OwinStartup(typeof(HeroCompany.Startup))]

namespace HeroCompany
{
    //Class to specifcy components for the application pipeline-configure owinauth while app is running.
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<LoggingMiddleware>();
            ConfigureAuth(app);
            
        }
    }
}