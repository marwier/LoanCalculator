
namespace InterviewTask
{
    using System.Web;
    using System.Web.Http;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
