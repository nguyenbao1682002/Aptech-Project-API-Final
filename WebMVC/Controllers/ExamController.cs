using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebMVC.Controllers
{
    public class ExamController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "";
        private readonly string ApiUrlRound = "";
        public ExamController()
        {
            httpClient = new HttpClient();
            var typeMedia = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(typeMedia);
            ApiUrl = "https://localhost:7274/api/ExamAPI";
            ApiUrlRound = "https://localhost:7274/api/RoundAPI";
        }
        // GET: ExamController
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(ApiUrl);
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Exam> listExams = JsonSerializer.Deserialize<List<Exam>>(data, options);
            return View(listExams);
        }
        public async Task<ActionResult> ExamDashboard(int id)
        {
            if (id != 0)
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://localhost:7274/api/ExamAPI/GetRoom/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("StartQuiz", "Question", new { id = id });
                }
                TempData["msg"] = "Room not found.";
                return View();
            }
            else
            {
                return View();
            }

        }
        // GET: ExamController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Exam exam, Round round)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    exam.ExamID = random.Next();
                    exam.DateCreateTest = DateTime.Now;

                    round.RoundID = random.Next();
                    //round.ExamID = exam.ExamID;

                    var data = JsonSerializer.Serialize(exam);
                    var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    var dataRound = JsonSerializer.Serialize(round);
                    var typeDataRound = new StringContent(dataRound, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await httpClient.PostAsync(ApiUrl, typeData);
                    HttpResponseMessage responseMessageRound = await httpClient.PostAsync(ApiUrlRound, typeDataRound);

                    //check successfully
                    if (responseMessage.IsSuccessStatusCode && responseMessageRound.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    throw new ArgumentException("Created failed.");
                }
                else
                {
                    ModelState.AddModelError("error", "Please complete information.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: ExamController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{ApiUrl}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Exam exam = JsonSerializer.Deserialize<Exam>(data, options);
            return View(exam);
        }

        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Exam exam)
        {
            try
            {
                var data = JsonSerializer.Serialize(exam);
                var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PutAsync($"{ApiUrl}/{id}", typeData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                throw new ArgumentException("Update failed!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: ExamController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{ApiUrl}/{id}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Exam exam = JsonSerializer.Deserialize<Exam>(data, options);
            return View(exam);
        }

        // POST: ExamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"{ApiUrl}/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
