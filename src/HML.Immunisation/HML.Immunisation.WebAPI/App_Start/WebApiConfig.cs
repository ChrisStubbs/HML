using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Validation.Providers;
using FluentValidation;
using FluentValidation.WebApi;
using HML.Immunisation.WebAPI.Infrastructure;
using Unity;
namespace HML.Immunisation.WebAPI
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
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			config.Formatters.JsonFormatter
			  .SerializerSettings
			  .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

			config.Services.Add(typeof(IExceptionLogger), UnityConfig.Container.Resolve<IExceptionLogger>());
			FluentValidationModelValidatorProvider.Configure(config, x => x.ValidatorFactory = new UnityValidatorFactory(config));
		}
    }
}
