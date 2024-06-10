using AutoMapper;
using BloodGL.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BloodGL.Application.Services
{
	public class UserDeviceService : IUserDeviceService
	{
		private readonly IUserDeviceRepository userDeviceRepository;
		private readonly IMapper mapper;
		private readonly IConfiguration configuration;

		public UserDeviceService(IUserDeviceRepository userDeviceRepository, IMapper mapper, IConfiguration configuration)
		{
			this.userDeviceRepository = userDeviceRepository;
			this.mapper = mapper;
			this.configuration = configuration;
		}

		public async Task Add(string userId, string token)
		{
			var any = await userDeviceRepository.Get(x => x.UserId == userId && x.Token == token);

			if (any != null) { return; }

			await userDeviceRepository.Add(new Domain.Models.UserDeviceEntity
			{
				UserId = userId,
				Token = token
			});
		}

		public async Task Remove(string userId, string token)
		{
			var device =await userDeviceRepository.Get(x => x.UserId == userId && x.Token == token);

			if (device == null) { return; }

			await userDeviceRepository.Delete(device);
		}
	}
}
