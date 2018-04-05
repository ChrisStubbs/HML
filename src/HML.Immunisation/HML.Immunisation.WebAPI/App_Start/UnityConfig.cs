using System;
using System.Web.Http.ExceptionHandling;
using AutoMapper;
using HML.Immunisation.Common;
using HML.Immunisation.Common.Interfaces;
using HML.Immunisation.Providers;
using HML.Immunisation.Providers.Interfaces;
using HML.Immunisation.WebAPI.Infrastructure;
using HML.Immunisation.WebAPI.Mappers;
using Unity;
using Unity.Injection;

namespace HML.Immunisation.WebAPI
{
	/// <summary>
	/// Specifies the Unity configuration for the main container.
	/// </summary>
	public static class UnityConfig
	{
		#region Unity Container
		private static Lazy<IUnityContainer> container =
		  new Lazy<IUnityContainer>(() =>
		  {
			  var container = new UnityContainer();
			  RegisterTypes(container);
			  return container;
		  });

		/// <summary>
		/// Configured Unity Container.
		/// </summary>
		public static IUnityContainer Container => container.Value;
		#endregion

		/// <summary>
		/// Registers the type mappings with the Unity container.
		/// </summary>
		/// <param name="container">The unity container to configure.</param>
		/// <remarks>
		/// There is no need to register concrete types such as controllers or
		/// API controllers (unless you want to change the defaults), as Unity
		/// allows resolving a concrete type even if it was not previously
		/// registered.
		/// </remarks>
		public static void RegisterTypes(IUnityContainer container)
		{
			// NOTE: To load from web.config uncomment the line below.
			// Make sure to add a Unity.Configuration to the using statements.
			// container.LoadConfiguration();

		
			container.RegisterType<ILogger, NLogger>();
			container.RegisterType<IConfiguration, Configuration>();
			container.RegisterType<IUsernameProvider, WebUsernameProvider>();
			container.RegisterType<IExceptionLogger, WebApiExceptionLogger>();
			container.RegisterType<ICacheService, InMemoryCache>();

			container.RegisterType<IClientSettingsProvider, ClientSettingsProvider>();
			container.RegisterType<IDiseaseRiskProvider, DiseaseRiskProvider>("DiseaseRiskProvider");
			container.RegisterType<IDiseaseRiskProvider, CachedDiseaseRiskProvider>(
				new InjectionConstructor(new ResolvedParameter<ICacheService>(),
										 new ResolvedParameter<IDiseaseRiskProvider>("DiseaseRiskProvider")));

            container.RegisterType<IImmunisationStatusProvider, ImmunisationStatusProvider>();

            container.RegisterType<ILookupsProvider, LookupsProvider>("LookupsProvider");
			container.RegisterType<ILookupsProvider, CachedLookupProvider>(
				new InjectionConstructor(new ResolvedParameter<ICacheService>(),
										 new ResolvedParameter<ILookupsProvider>("LookupsProvider")));

			container.RegisterType<IEmployeeDiseaseRiskStatusProvider, EmployeeDiseaseRiskStatusProvider>();
			container.RegisterType<IEmployeeDiseaseRiskStatusMapper, EmployeeDiseaseRiskStatusMapper>();

			//register all validators
			FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<UnityValidatorFactory>()
				.ForEach(result =>
				{
					container.RegisterType(result.InterfaceType, result.ValidatorType);

				});
			container.RegisterType<Mapper, Mapper>();

			container.RegisterType<ICacheService, HttpRequestCache>("HttpRequestCache");
			container.RegisterType<IMapper>(new InjectionFactory(c => AutoMapperConfig.GetMapperConfiguration().CreateMapper()));
			container.RegisterType<ICachedEmployeeDiseaseRiskStatusProvider,CachedEmployeeDiseaseRiskStatusProvider>(
				new InjectionConstructor(new ResolvedParameter<ICacheService>("HttpRequestCache"),
										new ResolvedParameter<IEmployeeDiseaseRiskStatusProvider>()));

		}


	}
}