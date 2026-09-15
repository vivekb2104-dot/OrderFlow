using Microsoft.AspNetCore.Mvc;

namespace OrderFlow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestAPIController : ControllerBase
    {
        [HttpGet(Name = "GetTestAPI")]
        public IEnumerable<TestAPI> Get()
        {
            return Enumerable.Range(1, 2).Select(index => new TestAPI
            {
                DateTime = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Testmsg = "This is a test message"
            })
            .ToArray();
        }
    }
}

              
