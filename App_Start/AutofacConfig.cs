using Autofac;
using Autofac.Integration.WebApi;
using Fiveways.Insight.Model.Mapper.Interface;
using Fiveways.Insight.Model.Repository.Interface;
using Fiveways.Insight.Model.UnitOfWork;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace Fiveways.Audit.API
{
    public class AutofacConfig
    {
        public static IContainer Container;
        public static void RegisterAutoFac(HttpConfiguration config)
        {
            // Setup the Container Builder
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            var modelAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.Split(',')[0] == "Fiveways.Insight.Model");
            builder.RegisterAssemblyTypes(modelAssembly);

            //  Register UnitOfWork
            builder.RegisterAssemblyTypes(modelAssembly)//.AsImplementedInterfaces()
                .As(typeof(IUnitOfWork)).InstancePerRequest();

            //  Register repositories
            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerActivityRepository)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(IReportGroupRepository)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerReportGroupRepository)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerSearchRepository)).InstancePerRequest();

            //  Register Mappers
            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerActivityHistoryMapper)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(IReportGroupMapper)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerReportGroupMapper)).InstancePerRequest();

            builder.RegisterAssemblyTypes(modelAssembly).AsImplementedInterfaces()
                .As(typeof(ICustomerSearchMapper)).InstancePerRequest();

            // Build the container
            Container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
        }
    }
}