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
    public class TEMPOController : Controller
    {
        // GET: Admin/TEMPO
        public ActionResult ViewTempo()
        {
            return View();
        }
        public ActionResult AddArticle()
        {
            return View();
        }
        public ActionResult EddArticle()
        {
            return View();
        }


        
    }
}