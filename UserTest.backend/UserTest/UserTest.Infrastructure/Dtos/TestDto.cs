using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTest.Infrastructure.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string TestName { get; set; } = string.Empty;
        public List<QuestionDto>? Questions { get; set; }
    }
}
