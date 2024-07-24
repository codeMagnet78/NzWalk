using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NzWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string[] _employees = {"A", "M", "S"};

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employees);
        }

        [HttpPost]
        public IActionResult PostEmployees()
        {
            return Ok("This is post test");
        }

    }
}
