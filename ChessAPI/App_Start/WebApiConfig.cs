﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Chess2API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{move}",
                defaults: new {
                    id = RouteParameter.Optional,
                    move = RouteParameter.Optional
                }
            );
        }
    }
}
