using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tampines.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            #region Event

            routes.MapRoute(
                      name: "EventsIndex",
                      url: "Event/Index",
                      defaults: new { controller = "Event", action = "Index" },
                      namespaces: new[] { "Tampines.Web.Controllers" }
             );

            routes.MapRoute(
                     name: "EventPartialVieweEvents",
                     url: "Event/PartialVieweEvents/{PageIndex}",
                     defaults: new { controller = "Event", action = "PartialVieweEvents" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            #endregion

            #region AboutUs

            routes.MapRoute(
                      name: "Introduction",
                      url: "AboutUs/Introduction",
                      defaults: new { controller = "AboutUs", action = "Introduction" },
                      namespaces: new[] { "Tampines.Web.Controllers" }
             );

            routes.MapRoute(
                     name: "AboutUsOurMP",
                     url: "AboutUs/OurMPs",
                     defaults: new { controller = "AboutUs", action = "OurMPs" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                    name: "AboutUsOrganisationChart",
                    url: "AboutUs/OrganisationChart",
                    defaults: new { controller = "AboutUs", action = "OrganisationChart" },
                    namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                   name: "OurPublications",
                   url: "AboutUs/OurPublications",
                   defaults: new { controller = "AboutUs", action = "OurPublications" },
                   namespaces: new[] { "Tampines.Web.Controllers" }
           );

            #endregion

            #region OurTown

            routes.MapRoute(
                      name: "OurTown_Introduction",
                      url: "OurTown/Introduction",
                      defaults: new { controller = "OurTown", action = "Introduction" },
                      namespaces: new[] { "Tampines.Web.Controllers" }
             );

            routes.MapRoute(
                     name: "OurTown_CyclingTown",
                     url: "OurTown/CyclingTown",
                     defaults: new { controller = "OurTown", action = "CyclingTown" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                     name: "OurTown_EcoTown",
                     url: "OurTown/EcoTown",
                     defaults: new { controller = "OurTown", action = "EcoTown" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );


            #endregion



            #region ResidentServices

            routes.MapRoute(
                      name: "ServicesBulkyItemRemovalServices",
                      url: "ResidentServices/BulkyItemRemovalServices",
                      defaults: new { controller = "ResidentServices", action = "BulkyItemRemovalServices" },
                      namespaces: new[] { "Tampines.Web.Controllers" }
             );

            routes.MapRoute(
                     name: "ServicesServiceConservancyCharges",
                     url: "ResidentServices/ServiceConservancyCharges",
                     defaults: new { controller = "ResidentServices", action = "ServiceConservancyCharges" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                     name: "ServicesTownCouncilServices",
                     url: "ResidentServices/TownCouncilServices",
                     defaults: new { controller = "ResidentServices", action = "TownCouncilServices" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                     name: "ServicesMediateDisputes",
                     url: "ResidentServices/MediateDisputes",
                     defaults: new { controller = "ResidentServices", action = "MediateDisputes" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                     name: "ServicesCommonAreaSpaces",
                     url: "ResidentServices/CommonAreaSpaces",
                     defaults: new { controller = "ResidentServices", action = "CommonAreaSpaces" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                     name: "ServicesServicesDownloads",
                     url: "ResidentServices/Downloads",
                     defaults: new { controller = "ResidentServices", action = "Downloads" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );


            #endregion



            #region OurTown

            routes.MapRoute(
                      name: "TempoIndex",
                      url: "Tempo/Index",
                      defaults: new { controller = "Tempo", action = "Index" },
                      namespaces: new[] { "Tampines.Web.Controllers" }
             );




            #endregion

            #region ContactUs


            routes.MapRoute(
                    name: "ContactUsGettingHere",
                    url: "ContactUs/GettingHere",
                    defaults: new { controller = "ContactUs", action = "GettingHere" },
                    namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                   name: "ContactUsFeedback",
                   url: "ContactUs/Feedback",
                   defaults: new { controller = "ContactUs", action = "Feedback" },
                   namespaces: new[] { "Tampines.Web.Controllers" }
            );
            routes.MapRoute(
                     name: "ContactUsUsefulLinksContacts",
                     url: "ContactUs/UsefulLinksContacts",
                     defaults: new { controller = "ContactUs", action = "UsefulLinksContacts" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );
           
            routes.MapRoute(
                     name: "ContactUsFAQs",
                     url: "ContactUs/FAQs",
                     defaults: new { controller = "ContactUs", action = "FAQs" },
                     namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                   name: "ContactUsSubscription",
                   url: "ContactUs/Subscription",
                   defaults: new { controller = "ContactUs", action = "Subscription" },
                   namespaces: new[] { "Tampines.Web.Controllers" }
            );

            routes.MapRoute(
                  name: "RefreshCaptcha",
                  url: "ContactUs/RefreshCaptcha",
                  defaults: new { controller = "ContactUs", action = "RefreshCaptcha" },
                  namespaces: new[] { "Tampines.Web.Controllers" }
           );

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
