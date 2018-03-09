using System;
using System.Collections.Generic;
using System.Linq;
using HML.Immunisation.WebAPI;
using NUnit.Framework;
using System.Web.Http;
using HML.Immunisation.WebAPI.Controllers;
using Unity;

namespace HML.Immunisation.Test.WebApi
{
	[TestFixture]
	public class UnityContainerTests
	{
		[Test]
		public void WhenGettingControllers_ThenAllServicesAreRegistered()
		{
			foreach (Type controllerType in GetSubClasses<ApiController>())
			{
				Assert.DoesNotThrow(() => UnityConfig.Container.Resolve(controllerType));
			}
		}

		private static IEnumerable<Type> GetSubClasses<T>()
		{
			var mvcAssembly = typeof(VersionController).Assembly;
			return mvcAssembly.GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
		}
	}
}
