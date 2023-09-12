using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;
using UserTest.Infrastructure.Dtos;
using System.Web;
using Microsoft.EntityFrameworkCore;
using UserTest.Domain.Interfaces;

namespace UserTest.Infrastructure.Services
{
    public class JwtService: IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _dataContext;
        public JwtService(IConfiguration configuration, DataContext dataContext)
        {
            _configuration = configuration;
            _dataContext = dataContext;
        }
        public async Task<RegistrationAnswerDto> Register(UserCreateDto userCreateDto)
        {
            try
            {
                if (await _dataContext.Users.AnyAsync(u => u.Email == userCreateDto.Email))
                {
                    throw new Exception("User with this email already exists");
                }

                var newUser = new User
                {
                    Email = userCreateDto.Email,
                    HashPassword = HashPasswordBCrypt(userCreateDto.Password)
                };

                await _dataContext.AddAsync(newUser);
                await _dataContext.SaveChangesAsync();

                return new RegistrationAnswerDto { Message = "Registration successful" };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoginAnswerDto> Login(UserLoginDto loginDto)
        {
            var currentUser = _dataContext.Users.FirstOrDefault(x => x.Email == loginDto.UserName);

            if (currentUser == null)
            {
                throw new Exception("user not found");
            }

            var isCorrectPass = VerifyPassword(loginDto.Password, currentUser.HashPassword);
            if (!isCorrectPass) throw new Exception("wrong credentials");

            var token = await GenerateToken(currentUser);

            return new LoginAnswerDto { Key = token };
        }

        private async Task<string> GenerateToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var jwt = new JwtSecurityToken(
           issuer: _configuration["Jwt:Issuer"],
           audience: _configuration["Jwt:Audience"],
           claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
               SecurityAlgorithms.HmacSha256)
           );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
        private string HashPasswordBCrypt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hashedPassword;
        }
        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }
    }
}
