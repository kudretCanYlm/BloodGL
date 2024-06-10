using AutoMapper;
using BloodGL.Application.Dtos;
using BloodGL.Domain.Models;

namespace BloodGL.Application
{
	public class ApplicationMapperProfile:Profile
	{
		public ApplicationMapperProfile()
		{
			//User
			CreateMap<UserEntity, LoginJwtDto>();
			CreateMap<UserEntity, UserDto>();
			CreateMap<AddUserDto, UserEntity>();

			//Glucose Measure
			CreateMap<AddGlucoseMeasureDto,GlucoseMeasureEntity>();
			CreateMap<GlucoseMeasureEntity, GlucoseMeasureDto>();

			//Glucose Measure Reply
			CreateMap<GlucoseMeasureReplyEntity,GlucoseMeasureReplyDto>();
		}
	}
}
