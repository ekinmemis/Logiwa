using Logiwa.Web.Configurations;
using Logiwa.Web.Infrastructure;

using System.Web.Mvc;
using System.Web.Routing;

using static Logiwa.Web.Infrastructure.CustomSearchModelBinding;

namespace Logiwa.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ModelBinderProviders.BinderProviders.Insert(0, new DataTablesToObjectModelBinderProvider());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            AutofacConfiguration.Configure();
            AutoMapperConfiguration.Configure();
        }
    }
}
