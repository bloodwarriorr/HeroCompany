using log4net;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace HeroCompanyApi
{
    public class LoggingMiddleware : OwinMiddleware
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggingMiddleware));

        public LoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            try
            {
               
            Log.Info($"Received request: {context.Request.Method} {context.Request.Uri}\n Request created Successfully");
            }
            catch (Exception ex)
            {

                Log.Error("Request recived denied with the error "+ex.Message);
            }

            // Pass the request on to the next middleware in the pipeline
            await Next.Invoke(context);
        }
    }
}