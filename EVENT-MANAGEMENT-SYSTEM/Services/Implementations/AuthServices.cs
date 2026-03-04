using AutoMapper;
using EVENT_MANAGEMENT_SYSTEM.Data;
using EVENT_MANAGEMENT_SYSTEM.DTOs;
using EVENT_MANAGEMENT_SYSTEM.Models;
using EVENT_MANAGEMENT_SYSTEM.Repositories.Interfaces;
using EVENT_MANAGEMENT_SYSTEM.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EVENT_MANAGEMENT_SYSTEM.Services.Implementations
{
    public class AuthServices : IAuthService
    {
        //private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userrepo;
        private readonly IMapper _mapper;
        public AuthServices(AppDbContext context,IConfiguration configuration,IUserRepository userrepo,IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
            _userrepo = userrepo;
        }
        public async Task<string> Login(LoginDto request)
        {
            var user = await _userrepo.GetUserByEmail(request.Email);
            if(user == null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return TokenGeneration(user);
        }
        private string TokenGeneration(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescription = new JwtSecurityToken(
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Audience"],
                 claims: claims,
                 expires: DateTime.UtcNow.AddDays(1),
                 signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
        }
        public async  Task<UserResponseDto> Register(RegisterDto user)
        {
            var alreadyExists = await _userrepo.GetUserByEmail(user.Email);

            if (alreadyExists != null) return null;

            var newUser = new User();

            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.PasswordHash = new PasswordHasher<User>().HashPassword(newUser, user.Password);
            newUser.RoleId= user.RoleId;

            await _userrepo.AddUser(newUser);

            var responseDto=_mapper.Map<UserResponseDto>(newUser);
            return responseDto;
            
        }
    }
}
