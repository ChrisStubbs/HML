using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HML.Employee.WebAPI;
using HML.Employee.WebAPI.Controllers;
using NUnit.Framework;
using Unity;

namespace HML.Employee.Test.WebApi
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

		private static List<Type> GetSubClasses<T>()
		{
			var mvcAssembly = typeof(VersionController).Assembly;
			return mvcAssembly.GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
		}
	}
}
