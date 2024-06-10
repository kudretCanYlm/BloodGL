using BloodGL.Application.Dtos;

namespace BloodGL.Application.Services
{
	public interface IGlucoseMeasureReplyService
	{
		Task Add(string measureId, string reply);

		Task<IEnumerable<GlucoseMeasureReplyDto>> GetGlucoseMeasureReplies(string measureId);
	}
}
