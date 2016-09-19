using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Classroom.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Enable Cors
            var policy = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(policy);
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Turn on Camel Case contract resolver
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = 
                new CamelCasePropertyNamesContractResolver();


            // Ignore circular reference errors
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
