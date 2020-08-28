using System;
using System.Web;
using System.Web.Mvc;

namespace Tampines.Web
{
    public class DisableCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            if (filterContext.HttpContext.Response.ContentType != "image/jpeg" && filterContext.HttpContext.Response.ContentType != "application/vnd.ms-excel")
            {
                filterContext.HttpContext.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            }
        }
    }
}