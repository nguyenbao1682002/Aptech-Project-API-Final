using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RegisterAdminController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly string ApiUrl = "";
        public RegisterAdminController()
        {
            httpClient = new HttpClient();
            var type = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(type);
            ApiUrl = "https://localhost:7274/api/ExamRegisterAPI";
        }
        // GET: RegisterAdminController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Register()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync("https://localhost:7274/api/ExamRegisterAPI/GetExam");
            var data = await httpResponse.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Exam> exams = JsonSerializer.Deserialize<List<Exam>>(data, options);
            var selectList = new List<SelectListItem>();
            foreach (var item in exams)
            {
                selectList.Add(new SelectListItem { Value = item.ExamID.ToString(), Text = item.ExamName });
            }
            if (selectList.Count > 0)
            {
                ViewBag.Items = selectList;
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(ExamRegister examRegister, string password)
        {
            try
            {
                Random random = new Random();
                //if (ModelState.IsValid)
                //{

                examRegister.ExamRegisterID = random.Next();

                Account account = new Account();
                account.AccountID = random.Next();
                account.Password = password;
                account.ExamID = examRegister.ExamID;
                account.ExamRegisterID = examRegister.ExamRegisterID;

                var gender = Request.Form["gender"];
                if (gender == "Male")
                {
                    examRegister.Gender = true;
                }
                else
                {
                    examRegister.Gender = false;
                }
                var data = JsonSerializer.Serialize(examRegister);
                var typeData = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await httpClient.PostAsync(ApiUrl, typeData);

                var dataAccount = JsonSerializer.Serialize(account);
                var typeDataAccount = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessageAccount = await httpClient.PostAsync("https://localhost:7274/api/ExamRegisterAPI/PostAccount", typeDataAccount);

                if (responseMessage.IsSuccessStatusCode && responseMessageAccount.IsSuccessStatusCode)
                {
                    TempData["msg"] = "Register successfully.";
                    return Redirect("~/Admin/RegisterAdmin/Register");
                }
                throw new ArgumentException("Register or create account failed!");
                //}
                //return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // GET: RegisterAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
