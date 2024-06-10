using AutoMapper;
using BloodGL.Application.Dtos;
using BloodGL.Domain.Enums;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Configuration;

namespace BloodGL.Application.Services
{
	public class GlucoseMeasureService : IGlucoseMeasureService
	{
		private readonly IGlucoseMeasureRepository glucoseMeasureRepository;
		private readonly IMapper mapper;
		private readonly IConfiguration configuration;

		public GlucoseMeasureService(IGlucoseMeasureRepository glucoseMeasureRepository, IMapper mapper, IConfiguration configuration)
		{
			this.glucoseMeasureRepository = glucoseMeasureRepository;
			this.mapper = mapper;
			this.configuration = configuration;
		}

		public async Task Add(AddGlucoseMeasureDto addGlucoseMeasureDto, string userId)
		{
			var glucoseMeasureEntity = mapper.Map<GlucoseMeasureEntity>(addGlucoseMeasureDto);
			glucoseMeasureEntity.UserId = userId;
			//TODO : set by glucose level
			glucoseMeasureEntity.Level = ClucoseLevel.Medium;

			await glucoseMeasureRepository.Add(glucoseMeasureEntity);
		}

		public async Task<GlucoseMeasureDto> GetMeasure(string measureId)
		{
			var measureEntity=await glucoseMeasureRepository.GetById(measureId);
			var measure= mapper.Map<GlucoseMeasureDto>(measureEntity);

			return measure;
		}

		public async Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasures(string userId)
		{
			var measures = await glucoseMeasureRepository.GetGlucoseMeasures(userId);
			return ConvertViewDto(measures);
		}

		public async Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastDay(string userId)
		{
			var measures = await glucoseMeasureRepository.GetGlucoseMeasuresByDate(userId, MeasureTimeIntervalEnum.Today);
			return ConvertViewDto(measures);
		}

		public async Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastMonth(string userId)
		{
			var measures = await glucoseMeasureRepository.GetGlucoseMeasuresByDate(userId, MeasureTimeIntervalEnum.LastMonth);
			return ConvertViewDto(measures);
		}

		public async Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastWeek(string userId)
		{
			var measures = await glucoseMeasureRepository.GetGlucoseMeasuresByDate(userId, MeasureTimeIntervalEnum.LastWeek);
			return ConvertViewDto(measures);
		}

		private IReadOnlyList<GlucoseMeasureDto> ConvertViewDto(IEnumerable<GlucoseMeasureEntity> entites) => mapper.Map<IReadOnlyList<GlucoseMeasureDto>>(entites);
	}
}
