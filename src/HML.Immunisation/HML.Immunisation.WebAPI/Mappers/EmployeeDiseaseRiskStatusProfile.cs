using AutoMapper;
using HML.Immunisation.Models.Entities;
using HML.Immunisation.Models.ViewModels;

namespace HML.Immunisation.WebAPI.Mappers
{
	public class EmployeeDiseaseRiskStatusProfile : Profile
	{
		public EmployeeDiseaseRiskStatusProfile()
		{
			CreateMap<EmployeeDiseaseRiskStatusRecord, EmployeeDiseaseRiskStatus>();
		}
	}
}