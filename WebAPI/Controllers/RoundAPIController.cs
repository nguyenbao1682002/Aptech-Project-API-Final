using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoundAPIController : ControllerBase
    {
        private readonly IRoundRepository roundRepository;
        public RoundAPIController()
        {
            roundRepository = new RoundRepository();
        }
        // GET: api/<RoundAPIController>
        [HttpGet]
        public IEnumerable<Round> Get()
        {
            return roundRepository.GetRounds();
        }

        // GET api/<RoundAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Round> Get(int id)
        {
            var check = roundRepository.GetRoundById(id);
            if (check == null)
            {
                return NotFound();
            }
            return check;
        }

        // POST api/<RoundAPIController>
        [HttpPost]
        public void Post(Round round)
        {
            roundRepository.InsertRound(round);
        }

        // PUT api/<RoundAPIController>/5
        [HttpPut("{id}")]
        public void Put(Round round)
        {
            roundRepository.UpdateRound(round);
        }

        // DELETE api/<RoundAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            roundRepository.DeleteRound(id);
        }
    }
}
