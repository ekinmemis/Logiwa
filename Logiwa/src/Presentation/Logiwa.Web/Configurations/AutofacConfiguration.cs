using Autofac;
using Autofac.Integration.Mvc;

using Logiwa.Core;
using Logiwa.Core.Domain.ApplicationUsers;
using Logiwa.Data;
using Logiwa.Services.ApplicationUserServices;
using Logiwa.Services.Authentication;
using Logiwa.Services.Catalog;
using Logiwa.Web.Factories;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logiwa.Web.Configurations
{
    public class AutofacConfiguration
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationUserService>().As<IApplicationUserService>();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<ProductModelFactory>().As<IProductModelFactory>();
            builder.RegisterType<WebHelper>().As<IWebHelper>();

            builder.RegisterType<EfDataContext>().As<IDbContext>();
            builder.RegisterType<ApplicationUser>();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>));

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                       .Where(t => t.Name.EndsWith("Controller"));

            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
