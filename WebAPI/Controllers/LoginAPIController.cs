using Microsoft.AspNetCore.Mvc;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        public LoginAPIController()
        {
            _loginRepository = new LoginRepository();
        }
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{username}/{password}")]
        public IActionResult Get(string username, string password)
        {
            var login = _loginRepository.SignIn(username, password);
            if (login == true)
            {
                return Ok(login);
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
