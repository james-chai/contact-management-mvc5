using Autofac;
using Autofac.Integration.Mvc;
using ContactManagement.Data;
using System.Web.Mvc;

namespace ContactManagement.MVC5.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Register MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register DbContext
            builder.RegisterType<ContactDbContext>()
                   .AsSelf()
                   .InstancePerRequest();

            // Register repository
            builder.RegisterType<SqlContactData>()
                   .As<IContactData>()
                   .InstancePerRequest();

            // Set the dependency resolver to be Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}