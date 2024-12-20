using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class QuestionAdminController : Controller
    {
        #region Variable
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "";
        #endregion

        #region Constructor
        public QuestionAdminController()
        {
            httpClient = new HttpClient();
            var typeMedia = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(typeMedia);
            ApiUrl = "https://localhost:7274/api/QuestionAPI";
        }
        #endregion

        #region Index
        // GET: QuestionController
        public async Task<ActionResult> Index(string SearchString, string sortBy, int? page)
        {
            if (string.IsNullOrEmpty(SearchString))
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
            else
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://localhost:7274/api/QuestionAPI/Search?name={SearchString}");
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                IPagedList<Question> questions = JsonSerializer.Deserialize<List<Question>>(data, options).ToPagedList(page ?? 1, 5);
                if (questions.Count == 0)
                {
                    TempData["msgSearchNull"] = $"There is no data matching the keyword '{SearchString}'";
                }
                return View(questions);
            }
        }
        #endregion

        #region Create View
        // GET: QuestionController/Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync("https://localhost:7274/api/QuestionAPI/GetRoundID");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<Round> listRound = JsonSerializer.Deserialize<List<Round>>(data, options);
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in listRound)
            {
                selectList.Add(new SelectListItem { Value = $"{item.RoundID}", Text = $"{item.RoundNumber} of Exam {item.ExamName}" });
            }
            if (selectList.Count > 0)
            {
                ViewBag.Items = selectList;
            }
            return View();
        }
        #endregion

        #region Create Post
        // POST: QuestionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Random random = new Random();
                    question.QuestionID = random.Next();
                    question.DateMake = DateTime.Now;
                    var data = JsonSerializer.Serialize(question);
                    var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await httpClient.PostAsync(ApiUrl, typeData);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return Redirect("~/Admin/QuestionAdmin");
                    }
                    throw new ArgumentException("Creat failed.");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Edit View
        // GET: QuestionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Question question = JsonSerializer.Deserialize<Question>(data, options);
                HttpResponseMessage responseMessageList = await httpClient.GetAsync("https://localhost:7274/api/QuestionAPI/GetRoundID");
                var dataList = await responseMessageList.Content.ReadAsStringAsync();
                var optionsList = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Round> listRound = JsonSerializer.Deserialize<List<Round>>(dataList, optionsList);
                List<SelectListItem> selectList = new List<SelectListItem>();
                foreach (var item in listRound)
                {
                    selectList.Add(new SelectListItem { Value = $"{item.RoundID}", Text = $"{item.RoundNumber} of Exam {item.ExamName}" });
                }
                if (selectList.Count > 0)
                {
                    ViewBag.Items = selectList;
                }
                return View(question);
            }
            return NotFound();
        }
        #endregion

        #region Edit Post
        // POST: QuestionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Question question)
        {
            try
            {
                var data = JsonSerializer.Serialize(question);
                var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PutAsync($"{ApiUrl}/{id}", typeData);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return Redirect("~/Admin/QuestionAdmin");
                }
                throw new ArgumentException("Edit failed!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Delete View
        // GET: QuestionController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync($"{ApiUrl}/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Question questions = JsonSerializer.Deserialize<Question>(data, options);
                return View(questions);
            }
            return NotFound();
        }
        #endregion

        #region Delete Post
        // POST: QuestionController/Delete/5
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
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                List<Question> exam = JsonSerializer.Deserialize<List<Question>>(data, options);
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
