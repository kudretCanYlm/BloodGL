using BloodGL.Core.Database.Options;
using BloodGL.Core.Database.Repository;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Options;

namespace BloodGL.Infrastructure.Repositories
{
	public class UserAndAuthRepository : RepositoryBase<UserEntity>, IUserAndAuthRepository
	{
		public UserAndAuthRepository(IOptions<MongoDbOptions> options) : base(options)
		{
		}

		public async Task AddUser(UserEntity user)
		{
			await Add(user);
		}

		public async Task<UserEntity> SignIn(string username, string password)
		{
			var user = await Get(x => x.Username == username && x.Password == password);

			return user;
		}

	}
}
