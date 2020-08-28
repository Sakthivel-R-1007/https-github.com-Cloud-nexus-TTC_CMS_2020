using System.Web;
using System.Web.Mvc;
using Tampines.Web.CustomAttribute;

namespace Tampines.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorLoggerAttribute());
        }
    }
}
