using System;
using System.Web.Http.ExceptionHandling;
using HML.Employee.Common;
using HML.Employee.Common.Interfaces;
using HML.Employee.Providers;
using HML.Employee.Providers.Interfaces;
using HML.Employee.WebAPI.Infrastructure;
using HML.Employee.WebAPI.Validators;
using Unity;

namespace HML.Employee.WebAPI
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

			// TODO: Register your type's mappings here.
			
			container.RegisterType<ILogger, NLogger>();
			container.RegisterType<IConfiguration, Infrastructure.Configuration>();
			container.RegisterType<IEmployeeProvider, EmployeeProvider>();
			container.RegisterType<IUsernameProvider, WebUsernameProvider>();
			container.RegisterType<IClientSpecificFieldConfigProvider, ClientSpecificFieldConfigProvider>();
			container.RegisterType<IExceptionLogger, WebApiExceptionLogger>();
			//register all validators
			FluentValidation.AssemblyScanner.FindValidatorsInAssemblyContaining<AddressRecordValidator>()
				.ForEach(result =>
				{
					container.RegisterType(result.InterfaceType, result.ValidatorType);

				});
		}
	}
}