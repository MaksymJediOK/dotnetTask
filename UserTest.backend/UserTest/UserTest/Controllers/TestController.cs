using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserTest.Infrastructure.Dtos;
using UserTest.Infrastructure.Services;

namespace UserTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TheTestService _testService;
        private readonly InitService _initService;
        public TestController(TheTestService theTestService, InitService initService)
        {
            _testService = theTestService;
            _initService = initService;

        }

        /// <summary>
        /// Get all tests (only name)
        /// </summary> 
        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _testService.GetAllTests();

            return Ok(tests);
        }

        /// <summary>
        /// Get all Tests that are assigned to specific user
        /// </summary> 
        [HttpGet("assigned")]
        [Authorize]
        public async Task<IActionResult> AssignedTests()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var tests = await _testService.GetAssignedTests(userId);

            return Ok(tests);
        }
        /// <summary>
        /// Get test info, including questions and answers
        /// </summary> 
        /// /// <param name="id">id of test</param>
        [HttpGet("take/{id}")]
        [Authorize]
        public async Task<IActionResult> TakeTest(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var test = await _testService.GetTest(userId, id);

            return Ok(test);
        }

        /// <summary>
        /// Return user score, create record about test passing
        /// </summary> 
        /// /// <param name="testId">id of the test</param>
        /// /// <param name="userAnswers">answer ids selected user(Comes via frontend form)</param>
        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> SubmitAnswers(int testId, [FromBody] List<int> userAnswers)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var userScore = await _testService.SubmitAnswers(userId, testId, userAnswers);

            return Ok(userScore);
        }

        /// <summary>
        /// Get test that wass passed by a user
        /// </summary> 
        [HttpGet("passed")]
        [Authorize]
        public async Task<IActionResult> GetPassedTest()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var res = await _testService.ShowPassedTest(userId);

            return Ok(res);
        }
        /// <summary>
        /// Controller that creates mock data for test
        /// </summary> 
        [HttpGet("init")]
        public async Task<IActionResult> InitTests()
        {
            await _initService.Init();

            return Ok("init was done");
        }
    }
}
