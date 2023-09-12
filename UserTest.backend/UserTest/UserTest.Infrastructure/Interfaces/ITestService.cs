using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;
using UserTest.Infrastructure.Dtos;

namespace UserTest.Infrastructure.Interfaces
{
    public interface ITestService
    {
        public Task<List<TestShowDto>> GetAllTests();
        public Task<List<TheTest>> GetAssignedTests(int userId);
        public Task<TestDto> GetTest(int userId, int testId);
        public Task<double> SubmitAnswers(int userId, int testId, List<int> userAnswers);
        public Task<IEnumerable<TestShowDto>> ShowPassedTest(int userId);
    }
}
