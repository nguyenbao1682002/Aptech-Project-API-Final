using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ExamAdminController : Controller
    {
        #region Variable
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "";
        private readonly string ApiUrlRound = "";
        #endregion

        #region Constructor
        public ExamAdminController()
        {
            httpClient = new HttpClient();
            var typeMedia = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(typeMedia);
            ApiUrl = "https://localhost:7274/api/ExamAPI";
            ApiUrlRound = "https://localhost:7274/api/RoundAPI";
        }
        #endregion

        #region Index
        // GET: ExamController
        public async Task<ActionResult> Index(string SearchString, int? page, string? sortBy)
        {
            if (SearchString == null)
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync(ApiUrl);
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                IPagedList<Exam> listExams = JsonSerializer.Deserialize<List<Exam>>(data, options).ToPagedList(page ?? 1, 5);

                HttpResponseMessage responseMessageEnd = await httpClient.GetAsync("https://localhost:7274/api/ExamAPI/GetAllExamEnd");
                var dataEnd = await responseMessage.Content.ReadAsStringAsync();
                var optionsEnd = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Exam> listExamsEnd = JsonSerializer.Deserialize<List<Exam>>(data, options);
                if (listExamsEnd != null)
                {
                    HttpResponseMessage responseMessageDelete = await httpClient.DeleteAsync("https://localhost:7274/api/ExamAPI/DeleteExamEnd");
                    if (responseMessageDelete.IsSuccessStatusCode == false)
                    {
                        throw new ArgumentException($"{responseMessageDelete.EnsureSuccessStatusCode}");
                    }
                }
                return View(listExams);
            }
            else
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://localhost:7274/api/ExamAPI/Search?SearchString={SearchString}");
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                IPagedList<Exam> listExams = JsonSerializer.Deserialize<List<Exam>>(data, options).ToPagedList(page ?? 1, 5);
                if (listExams.Count == 0)
                {
                    TempData["msgSearchNull"] = $"There is no data matching the keyword '{SearchString}'";
                }
                return View(listExams);
            }
        }
        #endregion

        #region ExamDashboard
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
        #endregion

        #region Details
        // GET: ExamController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create View
        // GET: ExamController/Create
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create Post
        // POST: ExamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Exam exam)
        {
            try
            {
                Round round = new Round();
                exam.Status = "Start";
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    exam.ExamID = random.Next();
                    exam.DateCreateTest = DateTime.Now;
                    var data = JsonSerializer.Serialize(exam);
                    var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage responseMessage = await httpClient.PostAsync(ApiUrl, typeData);
                    HttpResponseMessage responseMessageRound = null;

                    for (int i = 1; i < 4; i++)
                    {
                        if (i == 1)
                        {

                            round.RoundID = random.Next();
                            round.ExamID = exam.ExamID;
                            round.RoundNumber = 1;
                            round.RoundName = "Knowledge generality";
                            var dataRound = JsonSerializer.Serialize(round);
                            var typeDataRound = new StringContent(dataRound, System.Text.Encoding.UTF8, "application/json");

                            responseMessageRound = await httpClient.PostAsync(ApiUrlRound, typeDataRound);
                        }
                        if (i == 2)
                        {

                            round.RoundID = random.Next();
                            round.ExamID = exam.ExamID;
                            round.RoundNumber = 2;
                            round.RoundName = "Math";
                            var dataRound2 = JsonSerializer.Serialize(round);
                            var typeDataRound2 = new StringContent(dataRound2, System.Text.Encoding.UTF8, "application/json");

                            responseMessageRound = await httpClient.PostAsync(ApiUrlRound, typeDataRound2);
                        }
                        if (i == 3)
                        {
                            round.RoundID = random.Next();
                            round.ExamID = exam.ExamID;
                            round.RoundNumber = 3;
                            round.RoundName = "Computer technology";
                            var dataRound3 = JsonSerializer.Serialize(round);
                            var typeDataRound3 = new StringContent(dataRound3, System.Text.Encoding.UTF8, "application/json");

                            responseMessageRound = await httpClient.PostAsync(ApiUrlRound, typeDataRound3);
                            break;
                        }
                    }

                    //check successfully
                    if (responseMessage.IsSuccessStatusCode && responseMessageRound.IsSuccessStatusCode)
                    {
                        return Redirect("~/Admin/ExamAdmin");
                    }
                    throw new ArgumentException($"Created failed: {responseMessage.EnsureSuccessStatusCode} ,{responseMessageRound.EnsureSuccessStatusCode}");
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
        #endregion

        #region Edit View
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
            TempData["id"] = id;
            TempData.Keep();
            return View(exam);
        }
        #endregion

        #region Edit Post
        // POST: ExamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Exam exam)
        {
            try
            {
                //if (exam.Status == "End")
                //{
                //    return Redirect($"~/Admin/ExamAdmin/Confirm/{TempData["id"]}");
                //}
                var data = JsonSerializer.Serialize(exam);
                var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PutAsync($"{ApiUrl}/{id}", typeData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Redirect("~/Admin/ExamAdmin");
                }
                throw new ArgumentException("Update failed!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete View
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
        #endregion

        #region Delete Post
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
                    return Redirect("~/Admin/ExamAdmin");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Confirm View
        public async Task<ActionResult> Confirm(int id)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{ApiUrl}/{TempData["id"]}");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Exam exam = JsonSerializer.Deserialize<Exam>(data, options);
            return View(exam);
        }
        #endregion

        #region Confirm Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(int id, Exam exam)
        {
            var data = JsonSerializer.Serialize(exam);
            var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await httpClient.PutAsync($"{ApiUrl}/{id}", typeData);
            HttpResponseMessage responseMessageDelete = await httpClient.DeleteAsync("https://localhost:7274/api/ExamAPI/DeleteExamEnd");
            if (responseMessage.IsSuccessStatusCode)
            {
                return Redirect("~/Admin/ExamAdmin");
            }
            throw new ArgumentException("Confirm failed!");
        }
        #endregion

        #region Delete Id
        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = await httpClient.DeleteAsync($"{ApiUrl}/{id}");
                HttpResponseMessage responseMessageData = await httpClient.GetAsync(ApiUrl);
                var data = await responseMessageData.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<Exam> exam = JsonSerializer.Deserialize<List<Exam>>(data, options);
                if (exam == null)
                {
                    return Json(new { success = false, message = "No tests found" });
                }
                /*return Json(new { success = true, id = id});*/
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
