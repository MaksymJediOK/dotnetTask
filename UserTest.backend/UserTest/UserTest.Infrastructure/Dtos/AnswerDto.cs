using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTest.Infrastructure.Dtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswer { get; set; }
    }
}
