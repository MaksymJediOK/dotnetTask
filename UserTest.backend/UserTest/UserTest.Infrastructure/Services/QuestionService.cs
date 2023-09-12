using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTest.Infrastructure.Services
{
    public class QuestionService
    {
        private readonly DataContext _dataContext;
        public QuestionService(DataContext dataContext)
        {

            _dataContext = dataContext;

        }

       
    }
}
