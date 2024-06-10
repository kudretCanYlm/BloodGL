namespace BloodGL.Application.Services
{
	public interface IUserDeviceService
	{
		Task Add(string userId, string token);
		Task Remove(string userId, string token);
	}
}
