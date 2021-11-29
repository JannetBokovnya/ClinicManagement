using AutoMapper;
using ClinicManagement.Web.App_Start;
using ClinicManagement.Web.Controllers;
using ClinicManagement.Web.Models;
using ClinicManagement.Web.Repositories;
using ClinicManagement.Web.Services;
using ClinicManagement.Web.ViewModel;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace ClinicManagement.Web
{
    public static class UnityConfig
    {
        public static object AutoMapperService { get; private set; }

        public static IUnityContainer Initialise()
        {
            //var container = BuildUnityContainer();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //return container;
            IUnityContainer container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            //var configuration = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<Foo, Bar>();
            //    cfg.AddProfile<FooProfile>();
            //});

            //var mapper = MappingProfile.InitializeAutoMapper().CreateMapper();
            //container.RegisterInstance<IMapper>(mapper);
            //var UnityContainer = UnityConfig.GetConfiguredContainer();
            //Mapper.Initialize(cfg => cfg.ConstructServicesUsing(type => UnityContainer.Resolve(type)));
            //UnityContainer.Resolve<AutoMapperConfig>().MapAutoMapper();

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType(typeof(IClinicRepository<>), typeof(ClinicRepository<>));
            //container.RegisterType(typeof(IClinicService<>), typeof(ClinicService<>));
            container.RegisterType<IClinicService<City>, CityService>();
            container.RegisterType<IClinicService<Specialization>, SpecializationService>();
            container.RegisterType<IClinicService<Doctor>, DoctorService>();
            container.RegisterType<IClinicService<Patient>, PatientService>();
            container.RegisterType<IClinicService<Attendance>, AttendanceService>();
            container.RegisterType<IApplicationUserServise, ApplicationUserService>();
            container.RegisterType<IAppointmentService, AppointmentService>();
            


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            container.RegisterInstance(config.CreateMapper());

            //var ctr = new InjectionConstructor(typeof(IApplicationUserServise)));
            //container.RegisterType<AccountController>(ctr);
            
            //container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<AccountController>(
           new InjectionConstructor(typeof(IClinicService<Specialization>), typeof(IApplicationUserServise)));

            

            container.RegisterType<ManageController>();


            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {
            
        }
   //     public static void RegisterComponents()
   //     {
			//var container = new UnityContainer();
            
   //         // register all your components with the container here
   //         // it is NOT necessary to register your controllers
            
   //         // e.g. container.RegisterType<ITestService, TestService>();
            
   //         GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
   //     }
    }
}