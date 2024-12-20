using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamAPIController : ControllerBase
    {
        private readonly IExamRepository examRepository;
        public ExamAPIController()
        {
            examRepository = new ExamRepository();
        }
        // GET: api/<ExamAPIController>
        [HttpGet]
        public IEnumerable<Exam> Get()
        {
            return examRepository.GetExams();
        }
        [Route("Search")]
        [HttpGet]
        public IEnumerable<Exam> Search(string SearchString, string? sortBy)
        {
            return examRepository.SearchOrSortByExam(SearchString, sortBy);
        }

        [Route("GetAllExamEnd")]
        [HttpGet]
        public IEnumerable<Exam> GetAllExamEnd()
        {
            return examRepository.GetAllExamEnd();
        }
        [Route("DeleteExamEnd")]
        [HttpDelete]
        public void DeleteExamEnd()
        {
            examRepository.DeleteExamEnd();
        }

        // GET api/<ExamAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Exam> Get(int id)
        {
            var check = examRepository.GetExamById(id);
            if (check != null)
            {
                return check;
            }
            return NotFound();
        }
        [HttpGet("GetRoom/{id}")]
        public ActionResult<Exam> GetRoom(int id)
        {
            var check = examRepository.GetRoom(id);
            if (check == null)
            {
                return NotFound();
            }
            return check;
        }
        [Route("PostRoom")]
        [HttpPost]
        public ActionResult<Exam> GetRoom()
        {
            //var check = examRepository.GetRoom(room);
            return NotFound();
        }

        // POST api/<ExamAPIController>
        [HttpPost]
        public void Post(Exam exam)
        {
            examRepository.InsertExam(exam);
        }

        // PUT api/<ExamAPIController>/5
        [HttpPut("{id}")]
        public void Put(Exam exam)
        {
            examRepository.UpdateExam(exam);
        }

        // DELETE api/<ExamAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            examRepository.DeleteExam(id);
        }
    }
}
