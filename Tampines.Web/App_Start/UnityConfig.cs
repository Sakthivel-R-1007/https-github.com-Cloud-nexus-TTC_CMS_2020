using System.Web.Mvc;
using Tampines_CMS.Persistence.Implementation;
using Tampines_CMS.Persistence.Interface;
using Tampines_CMS.Service.Implementations;
using Tampines_CMS.Service.Interfaces;
using Unity;
using Unity.Mvc5;

namespace Tampines.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUserAccountDao, UserAccountDao>();
            container.RegisterType<IContactsDao, ContactsDao>();
            container.RegisterType<IBannerDao, BannerDao>();
            container.RegisterType<IEventsDao, EventsDao>();
            container.RegisterType<IAboutUsDao, AboutUsDao>();
            container.RegisterType<IOurTownDao, OurTownDao>();
            container.RegisterType<IResidentServicesDao, ResidentServicesDao>();
            
            container.RegisterType<IUtilityService, UtilityService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}