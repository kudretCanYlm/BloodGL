using BloodGL.Core.Database.Repository;
using BloodGL.Domain.Models;

namespace BloodGL.Domain.Interfaces
{
	public interface IUserAndAuthRepository: IBaseRepository<UserEntity>
	{
		Task<UserEntity> SignIn(string username,string password);
		Task AddUser(UserEntity user);
	}

}
