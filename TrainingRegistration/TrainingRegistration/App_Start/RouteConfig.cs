using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrainingRegistration
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                   name: "RegistrationRoute",
                   url: "Registration/{trainingId}",
                   constraints: new { trainingId = @"\d+" },
                   defaults: new { controller = "Registration", action = "Execute" }
               );

            routes.MapRoute(
                   name: "RegistrationRouteFalse",
                   url: "Registration/{*pathInfo}",
                   defaults: new { controller = "Registration", action = "Index" }
               );

            routes.MapRoute(
                   name: "TrainingRoute",
                   url: "TrainingInfo/{trainingId}",
                   constraints: new { trainingId = @"\d+"},
                   defaults: new { controller = "TrainingInfo", action = "Execute" }
               );

            routes.MapRoute(
                   name: "TrainingRouteFalse",
                   url: "TrainingInfo/{*pathInfo}",
                   defaults: new { controller = "TrainingInfo", action = "Index" }
               );

            routes.MapRoute(
                  name: "HomeFalse",
                  url: "Home/{*pathInfo}",
                  defaults: new { controller = "Home", action = "Index" }
              );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index"}
            );
        }
    }
}
