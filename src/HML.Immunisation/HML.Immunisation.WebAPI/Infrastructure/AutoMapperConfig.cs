using System;
using System.Linq;
using AutoMapper;
using HML.Immunisation.WebAPI.Mappers;

namespace HML.Immunisation.WebAPI.Infrastructure
{
	public class AutoMapperConfig
	{
		public static MapperConfiguration GetMapperConfiguration()
		{
			var profiles = from t in typeof(EmployeeDiseaseRiskStatusProfile).Assembly.GetTypes()
						   where typeof(Profile).IsAssignableFrom(t)
						   select (Profile)Activator.CreateInstance(t);

			//For each Profile, include that profile in the MapperConfiguration
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				foreach (var profile in profiles)
				{
					cfg.AddProfile(profile);
				}
			});

			return mapperConfig;
		}
	}
}