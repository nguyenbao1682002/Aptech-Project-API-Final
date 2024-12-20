using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;
using static System.Net.WebRequestMethods;

namespace WebMVC.Areas.Admin.Controllers
{
    public class QuestionController : Controller
    {
        #region Variable
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "";
        Queue<string> questionsQueue = new Queue<string>();
        #endregion

        #region Constructor
        public QuestionController()
        {
            questionsQueue.Enqueue("Câu hỏi 1");
            questionsQueue.Enqueue("Câu hỏi 2");
            questionsQueue.Enqueue("Câu hỏi 3");
            httpClient = new HttpClient();
            var typeMedia = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(typeMedia);
            ApiUrl = "https://localhost:7274/api/QuestionAPI";
        }
        #endregion

        #region Index
        // GET: QuestionController
        public async Task<ActionResult> Index(int? page)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(ApiUrl);
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            IPagedList<Question> listQuestions = JsonSerializer.Deserialize<List<Question>>(data, options).ToPagedList(page ?? 1, 5);
            return View(listQuestions);
        }
        #endregion

        #region StartQuiz View
        public async Task<ActionResult> StartQuiz(int id, int? page)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://localhost:7274/api/QuestionAPI/GetQuestionByExam/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            IPagedList<Question> questions = JsonSerializer.Deserialize<List<Question>>(data, options).ToPagedList(page ?? 1, 1);
            if (questions.Count == 0)
            {
                ViewBag.Message = "No question.";
            }
            if (questions.Count > 0)
            {
                ViewBag.Count = questions.Count;
            }
            return View(questions);
        }
        #endregion

        #region StartQuiz Post
        [HttpPost]
        public async Task<ActionResult> StartQuiz(Question question)
        {
            if (questionsQueue.Count > 0)
            {
                string nextQuestion = questionsQueue.Dequeue(); // Lấy câu hỏi ở đầu Queue
                ViewBag.CurrentQuestion = nextQuestion; // Truyền câu hỏi cho view
                return View(); // Trả về view để hiển thị câu hỏi
            }
            else
            {
                ViewBag.CurrentQuestion = "Không còn câu hỏi nào"; // Hiển thị thông báo khi hết câu hỏi
                return View(); // Trả về view để hiển thị thông báo
            }
        }
        #endregion
    }
}
