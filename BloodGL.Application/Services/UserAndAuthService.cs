using AutoMapper;
using BloodGL.Application.Dtos;
using BloodGL.Domain.Interfaces;
using BloodGL.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloodGL.Application.Services
{
    public class UserAndAuthService : IUserAndAuthService
    {
        private readonly IUserAndAuthRepository userAndAuthRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserAndAuthService(IUserAndAuthRepository userAndAuthRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userAndAuthRepository = userAndAuthRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task AddUser(AddUserDto addUserDto, RoleEnum role = RoleEnum.User)
        {
            var userEntity = mapper.Map<UserEntity>(addUserDto);
            userEntity.Role = role;

            await userAndAuthRepository.AddUser(userEntity);
        }

		public async Task<UserDto> Get(string id)
		{
			var userEntity=await userAndAuthRepository.GetById(id);

            var userDto=mapper.Map<UserDto>(userEntity);

            return userDto;
		}

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var userEntities=await userAndAuthRepository.GetMany(x=>x.Role==RoleEnum.User);
            var userDtos=mapper.Map<IEnumerable<UserDto>>(userEntities);

            return userDtos;
		}

		public async Task<LoginJwtDto> LoginAndGetUser(LoginDto loginDto)
        {
            var user = await userAndAuthRepository.SignIn(loginDto.Username, loginDto.Password);

            // return null if user not found
            if (user == null)
                return null;

            var userJwt = mapper.Map<LoginJwtDto>(user);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userJwt.Id),
                    new Claim(ClaimTypes.Role,userJwt.Role.ToString()),
                    new Claim(ClaimTypes.DateOfBirth,DateTime.Now.Ticks.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userJwt.Token = tokenHandler.WriteToken(token);

            // remove password before returning and saved to token repository
            userJwt.Password = null;

            return userJwt;
        }
    }
}
