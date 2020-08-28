using Tampines.Web.CustomAttribute;
using System.Web;
using System.Web.Mvc;

namespace Tampines.Web.Filters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorLoggerAttribute());
        }
    }
}