using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTest.Domain.Entities;

namespace UserTest.Infrastructure.Services
{
    public class InitService
    {
        private readonly DataContext _dataContext;
        public InitService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Init()
        {
            var user = _dataContext.Users.First();

            var MathQ1 = new Question
            {
                Title = "What is 2+2?",
                Answers = new List<Answer> {
                    new Answer { AnswerText="4", IsAnswer=true},
                    new Answer { AnswerText = "8", IsAnswer = false }
                },
            };
            var MathQ2 = new Question
            {
                Title = "What is 5*5?",
                Answers = new List<Answer> {
                    new Answer { AnswerText="14", IsAnswer=false},
                    new Answer { AnswerText = "25", IsAnswer = true }
                },
            };

            var MathTest = new TheTest
            {
                TestName = "Math v2.0",
                Questions = new List<Question> { MathQ1, MathQ2 },
                AssignedUsers = new List<User> { user }
            };
            // Geo Test
            var GeoQ1 = new Question
            {
                Title = "What is the Capital of USA",
                Answers = new List<Answer> {
                    new Answer { AnswerText="Boston", IsAnswer=true},
                    new Answer { AnswerText = "Tokyo", IsAnswer = false }
                },
            };
            var GeoQ2 = new Question
            {
                Title = "Where is Germany located",
                Answers = new List<Answer> {
                    new Answer { AnswerText="Asia", IsAnswer=false},
                    new Answer { AnswerText = "Europe", IsAnswer = true }
                },
            };
            var GeoTest = new TheTest
            {
                TestName = "Geography test1",
                Questions = new List<Question> { GeoQ1, GeoQ2 }
            };

            await _dataContext.AddRangeAsync(MathQ1, MathQ2, MathTest, GeoQ1, GeoQ2, GeoTest);
            await _dataContext.SaveChangesAsync();
        }
    }
}
