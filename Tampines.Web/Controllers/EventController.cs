using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Domain;

namespace Tampines.Web.Controllers
{
    public class EventController : Controller
    {


        #region Constructor And Private Members

        private IEventsDao _EventsDao;
        private IBannerDao _bannerDao;


        public EventController(IEventsDao EventsDao, IBannerDao bannerDao)
        {
            _EventsDao = EventsDao;
            _bannerDao = bannerDao;

        }

        #endregion
        // GET: Event
        public ActionResult Index()
        {
            EventViewModel eventView = new EventViewModel();
            ViewBannerModel viewBanner = _bannerDao.GetViewBanner();

            IList<Events> Events = _EventsDao.Get(1, 10);
            eventView.Events = Events;
            if (viewBanner != null && viewBanner.EventBanner != null && viewBanner.EventBanner.Count > 0)
                eventView.EventBanner = viewBanner.EventBanner;

            return View(eventView);
        }

        public ActionResult PartialVieweEvents(int PageIndex)
        {
            IList<Events> Events = _EventsDao.Get(PageIndex, 10);
            return View(Events);
        }
    }
}