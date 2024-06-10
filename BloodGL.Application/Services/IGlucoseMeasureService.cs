using BloodGL.Application.Dtos;

namespace BloodGL.Application.Services
{
	public interface IGlucoseMeasureService
	{
		Task Add(AddGlucoseMeasureDto addGlucoseMeasureDto, string userId);
		Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasures(string userId);
		Task<GlucoseMeasureDto> GetMeasure(string measureId);
		Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastDay(string userId);
		Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastWeek(string userId);
		Task<IReadOnlyList<GlucoseMeasureDto>> GetMeasuresInlastMonth(string userId);
	}
}
