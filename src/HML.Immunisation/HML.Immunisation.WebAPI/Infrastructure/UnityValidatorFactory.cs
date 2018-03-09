using System;
using System.Web.Http;
using FluentValidation;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class UnityValidatorFactory : ValidatorFactoryBase
	{
		private readonly HttpConfiguration _configuration;

		public UnityValidatorFactory(HttpConfiguration configuration)
		{
			_configuration = configuration;
		}

		public override IValidator CreateInstance(Type validatorType)
		{
			return _configuration.DependencyResolver.GetService(validatorType) as IValidator;
		}

	}
}