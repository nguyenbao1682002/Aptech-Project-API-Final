using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamRegisterAPIController : ControllerBase
    {
        private readonly IExamRegisterRepository examRegisterRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IExamRepository examRepository;
        public ExamRegisterAPIController()
        {
            examRepository = new ExamRepository();
            accountRepository = new AccountRepository();
            examRegisterRepository = new ExamRegisterRepository();
        }
        // GET: api/<ExamRegisterAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [Route("GetExam")]
        [HttpGet]
        public IEnumerable<Exam> GetExams()
        {
            return examRepository.GetExams();
        }
        // GET api/<ExamRegisterAPIController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExamRegisterAPIController>
        [HttpPost]
        public void Post(ExamRegister examRegister)
        {
            examRegisterRepository.InsertExamRegister(examRegister);
        }

        // PUT api/<ExamRegisterAPIController>/5
        [HttpPut("{id}")]
        public void Put(ExamRegister examRegister)
        {
            examRegisterRepository.UpdateExamRegister(examRegister);
        }

        // DELETE api/<ExamRegisterAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            examRegisterRepository.DeleteExamRegister(id);
        }

        #region Account

        [Route("GetAllPassword")]
        [HttpGet]
        public IEnumerable<string> GetAllPassword()
        {
            return accountRepository.GetAllPassword();
        }

        [Route("PostAccount")]
        [HttpPost]
        public void PostAccount(Account account)
        {
            accountRepository.InsertAccount(account);
        }

        #endregion
    }
}
