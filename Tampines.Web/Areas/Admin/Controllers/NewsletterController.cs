using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines.Web.Filters;

namespace Tampines.Web.Areas.Admin.Controllers
{
    [AuthenticationFilter]
    [DisableCache]
    [HandleError]
    public class NewsletterController : Controller
    {
        // GET: Admin/Newsletter
        public ActionResult ViewNewsletters()
        {
            return View();
        }
    }
}