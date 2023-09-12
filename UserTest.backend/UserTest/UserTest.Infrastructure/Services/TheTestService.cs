using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserTest.Domain.Entities;
using UserTest.Infrastructure.Dtos;
using UserTest.Infrastructure.Interfaces;

namespace UserTest.Infrastructure.Services
{
    public class TheTestService: ITestService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public TheTestService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<TestShowDto>> GetAllTests()
        {
            var tests = await _dataContext.Tests
                 .Select(x => new TestShowDto
                 {
                     TestName = x.TestName
                 })
                 .ToListAsync();

            return tests;
        }

        public async Task<List<TheTest>> GetAssignedTests(int userId)
        {

            var UserTests = await _dataContext.Tests
                .Where(test => test.AssignedUsers
                .Any(user => user.Id == userId))
                .ToListAsync();

            return UserTests;
        }

        public async Task<TestDto> GetTest(int userId, int testId)
        {
            bool isUserAssigned = _dataContext.Tests
                .Any(ut => ut.Id == userId && ut.Id == testId);

            if (!isUserAssigned)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var test = await _dataContext.Tests.Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .FirstOrDefaultAsync(x => x.Id == testId);
            if (test == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var mappedTest = _mapper.Map<TestDto>(test);
            return mappedTest;
        }

        public async Task<double> SubmitAnswers(int userId, int testId, List<int> userAnswers)
        {
            var UserResult = _dataContext.Results.FirstOrDefault(x => x.UserId == userId);

            if (UserResult?.TestId == testId)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            var questionsForTest = _dataContext.Questions
                .Include(x => x.Answers)
                .Where(question => question.TheTest.Id == testId)
                .ToList();

            int score = 0;
            var CorrectIds = new List<int>();
            foreach (var question in questionsForTest)
            {
                var correctAnswerIds = question.Answers
                .Where(a => a.IsAnswer)
                .Select(a => a.Id)
                .ToList();

                foreach (var correctAnswer in correctAnswerIds)
                {
                    CorrectIds.Add(correctAnswer);
                }
            }

            foreach (var userAnswer in userAnswers)
            {
                if (CorrectIds.Contains(userAnswer))
                {
                    score++;
                }
            }

            var newUserResult = new UserTestResult { TestId = testId, UserId = userId, Score = score };
            await _dataContext.AddAsync(newUserResult);
            await _dataContext.SaveChangesAsync();

            return score;

        }

        public async Task<IEnumerable<TestShowDto>> ShowPassedTest(int userId)
        {
            var userResults = await _dataContext.Results.Where(x => x.UserId == userId).Select(x => x.TestId).ToListAsync();

            var passedTest = await _dataContext.Tests.Where(t => userResults.Contains(t.Id)).ToListAsync();

            var resultList = passedTest.Select(x => new TestShowDto
            {
                TestName = x.TestName
            });

            return resultList;
        }

    }
}
