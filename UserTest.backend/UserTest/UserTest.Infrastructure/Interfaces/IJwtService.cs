using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;
using UserTest.Infrastructure.Dtos;

namespace UserTest.Domain.Interfaces
{
    public interface IJwtService
    {
        public Task<RegistrationAnswerDto> Register(UserCreateDto userCreateDto);

        public Task<LoginAnswerDto> Login(UserLoginDto loginDto);

    }
}
