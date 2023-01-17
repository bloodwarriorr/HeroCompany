using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HeroCompany
{
    public class FilterConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}
