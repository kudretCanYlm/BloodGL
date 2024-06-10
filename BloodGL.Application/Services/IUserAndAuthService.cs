using BloodGL.Application.Dtos;
using BloodGL.Domain.Models;

namespace BloodGL.Application.Services
{
	public interface IUserAndAuthService
	{
		public Task<LoginJwtDto> LoginAndGetUser(LoginDto loginDto);
		public Task AddUser(AddUserDto addUserDto, RoleEnum role= RoleEnum.User);
		public Task<IEnumerable<UserDto>> GetAll();
		public Task<UserDto> Get(string id);
	}
}
