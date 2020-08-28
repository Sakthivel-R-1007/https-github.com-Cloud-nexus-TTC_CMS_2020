using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tampines.Web.Controllers
{
    public class NewslettersController : Controller
    {
        // GET: Newsletters
        public ActionResult viewNewsletters()
        {
            return View();
        }
    }
}