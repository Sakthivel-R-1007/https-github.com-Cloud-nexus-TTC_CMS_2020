using System.Web.Mvc;

namespace Tampines.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            #region Banner

            context.MapRoute(
                      name: "ViewBanners",
                      url: "ViewBanners",
                      defaults: new { controller = "Banner", action = "ViewBanners" },
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
                     name: "EditHomeBanner",
                     url: "Admin/EditHomeBanner/{EncryptedId}",
                     defaults: new { controller = "Banner", action = "EditHomeBanner" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
                   name: "EditAboutUsBanner",
                   url: "Admin/EditAboutUsBanner/{EncryptedId}",
                   defaults: new { controller = "Banner", action = "EditAboutUsBanner" },
                   namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
                  name: "EditOurTownBanner",
                  url: "Admin/EditOurTownBanner/{EncryptedId}",
                  defaults: new { controller = "Banner", action = "EditOurTownBanner" },
                  namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
                 name: "EditResidentServicesBanner",
                 url: "Admin/EditResidentServicesBanner/{EncryptedId}",
                 defaults: new { controller = "Banner", action = "EditResidentServicesBanner" },
                 namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );
            context.MapRoute(
               name: "EditTEMPOBanner",
               url: "Admin/EditTEMPOBanner/{EncryptedId}",
               defaults: new { controller = "Banner", action = "EditTEMPOBanner" },
               namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
              name: "EditEventBanner",
              url: "Admin/EditEventBanner/{EncryptedId}",
              defaults: new { controller = "Banner", action = "EditEventBanner" },
              namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
              name: "EditContactUsBanner",
              url: "Admin/EditContactUsBanner/{EncryptedId}",
              defaults: new { controller = "Banner", action = "EditContactUsBanner" },
              namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            #endregion

            #region AboutUs

            context.MapRoute(
                     name: "AboutUsIntroduction",
                     url: "Admin/AboutUs/Introduction",
                     defaults: new { controller = "AboutUs", action = "Introduction" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                    name: "OurMPs",
                    url: "Admin/AboutUs/OurMPs",
                    defaults: new { controller = "AboutUs", action = "OurMPs" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
               name: "EditOurMPs",
               url: "Admin/EditMPs/{EncryptedId}",
               defaults: new { controller = "AboutUs", action = "EditMPs", EncryptedId = UrlParameter.Optional },
               namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
       );

            context.MapRoute(
                     name: "OrganisationChart",
                     url: "Admin/AboutUs/OrganisationChart",
                     defaults: new { controller = "AboutUs", action = "OrganisationChart" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "ViewPublications",
                     url: "Admin/AboutUs/ViewPublications",
                     defaults: new { controller = "AboutUs", action = "ViewPublications" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "AddAnnualReports",
                    url: "Admin/AboutUs/AddAnnualReports",
                    defaults: new { controller = "AboutUs", action = "AddAnnualReports" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                  name: "EditAnnualReports",
                  url: "Admin/AboutUs/EditAnnualReports/{EncryptedId}",
                  defaults: new { controller = "AboutUs", action = "EditAnnualReports" },
                  namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
          );

            context.MapRoute(
                 name: "EditPressRelease",
                 url: "Admin/AboutUs/EditPressRelease/{EncryptedId}",
                 defaults: new { controller = "AboutUs", action = "EditPressRelease" },
                 namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
         );

            #endregion

            #region OurTown

            context.MapRoute(
                     name: "OurTownIntroduction",
                     url: "Admin/OurTown/Introduction",
                     defaults: new { controller = "OurTown", action = "Introduction" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "OurTownPreview",
                    url: "Admin/OurTown/Preview",
                    defaults: new { controller = "OurTown", action = "Preview" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                     name: "TownMap",
                     url: "Admin/OurTown/TownMap",
                     defaults: new { controller = "OurTown", action = "TownMap" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );


            #endregion

            #region MasterPlan

            context.MapRoute(
                      name: "MasterPlanIndex",
                      url: "Admin/MasterPlan/Index",
                      defaults: new { controller = "MasterPlan", action = "Index" },
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            #endregion

            #region ResidentServices

            context.MapRoute(
                     name: "UseOfCommoAreaOpenSpaces",
                     url: "Admin/ResidentServices/UseOfCommoAreaOpenSpaces",
                     defaults: new { controller = "ResidentServices", action = "UseOfCommoAreaOpenSpaces" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "BulkyItemRemovalServices",
                     url: "Admin/ResidentServices/BulkyItemRemovalServices",
                     defaults: new { controller = "ResidentServices", action = "BulkyItemRemovalServices" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "ServiceConservancyCharges",
                     url: "Admin/ResidentServices/ServiceConservancyCharges",
                     defaults: new { controller = "ResidentServices", action = "ServiceConservancyCharges" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "Downloads",
                     url: "Admin/ResidentServices/Downloads",
                     defaults: new { controller = "ResidentServices", action = "Downloads" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "AddAccordion",
                     url: "Admin/ResidentServices/AddAccordion",
                     defaults: new { controller = "ResidentServices", action = "AddAccordion" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "EditAccordion",
                     url: "Admin/ResidentServices/EditAccordion/{EncDetail}",
                     defaults: new { controller = "ResidentServices", action = "EditAccordion" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                     name: "EditAccordionPdfPartialView",
                     url: "Admin/ResidentServices/EditAccordionPdfPartialView/{EncDetail}/{EncGUID}",
                     defaults: new { controller = "ResidentServices", action = "EditAccordionPdfPartialView" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            #endregion


            #region TEMPO

            context.MapRoute(
                      name: "ViewTempo",
                      url: "Admin/TEMPO/ViewTempo",
                      defaults: new { controller = "TEMPO", action = "ViewTempo" },
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            #endregion

            #region Newsletter

            context.MapRoute(
                      name: "ViewNewsletters",
                      url: "Admin/Newsletter/ViewNewsletters",
                      defaults: new { controller = "Newsletter", action = "ViewNewsletters" },
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            #endregion

            #region Event

            context.MapRoute(
                      name: "EventIndex",
                      url: "Admin/Event/Index",
                      defaults: new { controller = "Event", action = "Index" },
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            context.MapRoute(
                    name: "AdminPartialVieweEvents",
                    url: "Admin/Event/PartialVieweEvents/{PageIndex}",
                    defaults: new { controller = "Event", action = "PartialVieweEvents" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                    name: "EventEdit",
                    url: "Admin/Event/Edit/{EncDetail}",
                    defaults: new { controller = "Event", action = "Edit" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
           );
            context.MapRoute(
                      name: "CheckEventTitle",
                      url: "Admin/Event/CheckEventTitle/{EventTitle}/{EncDetail}",
                      defaults: new { controller = "Event", action = "CheckEventTitle" , EncDetail = UrlParameter.Optional},
                      namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
             );

            #endregion

            #region ContactUs

            context.MapRoute(
                     name: "PartialViewLinksContents",
                     url: "Admin/ContactUs/PartialViewLinksContents/{PageIndex}",
                     defaults: new { controller = "ContactUs", action = "PartialViewLinksContents" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                    name: "UsefulLinksContacts",
                    url: "Admin/ContactUs/UsefulLinksContacts",
                    defaults: new { controller = "ContactUs", action = "UsefulLinksContacts" },
                    namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
           );

            context.MapRoute(
                   name: "EditLinksContacts",
                   url: "Admin/ContactUs/EditLinksContacts/{EncryptedId}",
                   defaults: new { controller = "ContactUs", action = "EditLinksContacts" },
                   namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
          );

            context.MapRoute(
                     name: "PDPAPublicPolicy",
                     url: "Admin/ContactUs/PDPAPublicPolicy",
                     defaults: new { controller = "ContactUs", action = "PDPAPublicPolicy" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "WhistleBlowingPolicy",
                     url: "Admin/ContactUs/WhistleBlowingPolicy",
                     defaults: new { controller = "ContactUs", action = "WhistleBlowingPolicy" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                     name: "ViewFAQ",
                     url: "Admin/ContactUs/ViewFAQ",
                     defaults: new { controller = "ContactUs", action = "ViewFAQ" },
                     namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "Subscription",
                   url: "Admin/ContactUs/Subscription",
                   defaults: new { controller = "ContactUs", action = "Subscription" },
                   namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "AddQuestion",
                   url: "Admin/ContactUs/AddQuestion",
                   defaults: new { controller = "ContactUs", action = "AddQuestion" },
                   namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                   name: "EditQuestion",
                   url: "Admin/ContactUs/EditQuestion/{EncryptedId}",
                   defaults: new { controller = "ContactUs", action = "EditQuestion" },
                   namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            #endregion


            context.MapRoute(
                      name: "ResetPassword",
                       url: "ResetPassword/{EncDetail}",
                  defaults: new { controller = "Login", action = "ResetPassword" },
                namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );


            context.MapRoute(
                       name: "ForgotPassword",
                        url: "ForgotPassword",
                   defaults: new { controller = "Login", action = "ForgotPassword" },
                 namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                  name: "Login",
                  url: "Login/Index",
                  defaults: new { controller = "Login", action = "Index" },
                  namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Tampines.Web.Areas.Admin.Controllers" }
            );
        }
    }
}