using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAPIController : ControllerBase
    {
        private readonly IQuestionRepository questionRepository;
        private readonly Queue<Question> q = new Queue<Question>();
        private readonly IExamRepository examRepository;
        private readonly PetroleumBusinessDBContext db;
        private readonly IRoundRepository roundRepository;
        Queue<Question> questionQueue = new Queue<Question>();
        public QuestionAPIController()
        {
            roundRepository = new RoundRepository();
            db = new PetroleumBusinessDBContext();
            examRepository = new ExamRepository();
            questionRepository = new QuestionRepository();
        }
        // GET: api/<QuestionAPIController>
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return questionRepository.GetQuestions();
        }
        [Route("Search")]
        [HttpGet]
        public IEnumerable<Question> Search(string name, string? sortBy)
        {
            return questionRepository.SearchByNameOrSortBy(name, sortBy);
        }
        // GET api/<QuestionAPIController>/5
        [HttpGet("{id}")]
        public ActionResult<Question> Get(int id)
        {
            var check = questionRepository.GetQuestionById(id);
            if (check == null)
            {
                return NotFound();
            }
            return check;
        }
        [HttpGet("GetQuestionByExam/{id}")]
        public IEnumerable<Question> GetQuestionByRound(int id)
        {
            var check = questionRepository.GetQuestionsByRound(id);
            if (check != null)
            {
                return check;
            }
            return Enumerable.Empty<Question>();
        }
        [Route("GetQueueQuestion")]
        [HttpGet]
        public IEnumerable<Question> GetQueueQuestion(int score)
        {
            //PetroleumBusinessDBContext db = new PetroleumBusinessDBContext();
            //if (q.Count == 0)
            //{
            //    foreach (var item in db.Questions.ToList())
            //    {
            //        q.Enqueue(item);
            //    }
            //}
            //if (q.Count > 0)
            //{
            //    Question question = q.Dequeue();
            //    return question;
            //}
            //else
            //{
            //    return NotFound();
            //}
            var check = questionRepository.GetQuestions();
            if(check != null)
            {
                return check;
            }
            else
            {
                return Enumerable.Empty<Question>();
            }

        }
        [Route("GetRoundID")]
        [HttpGet]
        public IEnumerable<Round> GetRoundID()
        {
            var rs = roundRepository.GetRoundId();
            return rs;
        }
        // POST api/<QuestionAPIController>
        [HttpPost]
        public void Post(Question question)
        {
            questionRepository.InsertQuestion(question);
        }
        [Route("CheckAnswer")]
        [HttpPost]
        public void CheckAnswer(Question question)
        {
            int score = 0;
            string correctAnswer = "";
            if (question.AnswerA != null)
            {
                correctAnswer = "A";
            }
            if (question.AnswerB != null)
            {
                correctAnswer = "B";
            }
            if (question.AnswerC != null)
            {
                correctAnswer = "C";
            }
            if (question.AnswerD != null)
            {
                correctAnswer = "D";
            }
            Queue<Question> q = new Queue<Question>();
            PetroleumBusinessDBContext db = new PetroleumBusinessDBContext();
            foreach (var item in db.Questions.ToList())
            {
                q.Enqueue(item);
            }
            question = q.Peek();
            q.Dequeue();
            if (correctAnswer.Equals(question.CorrectAnswer))
            {
                score += 1;
            }
            RedirectToAction("GetQueueQuestion", new { data = score });
        }

        // PUT api/<QuestionAPIController>/5
        [HttpPut("{id}")]
        public void Put(Question question)
        {
            questionRepository.UpdateQuestion(question);
        }

        // DELETE api/<QuestionAPIController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            questionRepository.DeleteQuestion(id);
        }
    }
}
