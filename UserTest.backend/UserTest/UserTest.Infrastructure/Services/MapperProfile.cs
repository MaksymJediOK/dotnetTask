using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;
using UserTest.Infrastructure.Dtos;

namespace UserTest.Infrastructure.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TheTest, TestDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Answer, AnswerDto>();
        }
    }
}
