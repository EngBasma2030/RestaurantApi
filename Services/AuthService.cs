using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public AuthService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _mapper = mapper;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("User with this email already exists.");

            var user = _mapper.Map<User>(dto);
            user.PasswordHash = dto.Password; 

            // 👇 تأكدي إن الدور بياخد قيمة افتراضية
            user.Role = string.IsNullOrEmpty(user.Role) ? "User" : user.Role;

            await _userRepository.AddAsync(user);

            return new AuthResponseDto
            {
                Username = user.FullName,
                Token = GenerateToken(user)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByFullNameAsync(dto.Username);
            if (user == null || dto.Password != user.PasswordHash)
                throw new Exception("Invalid name or password.");

            return new AuthResponseDto
            {
                Username = user.FullName,
                Token = GenerateToken(user)
            };


        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWTOptions:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["JWTOptions:Issuer"],
                Audience = _config["JWTOptions:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}
