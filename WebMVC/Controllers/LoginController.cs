using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace WebMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string ApiUrl = "";
        public LoginController()
        {
            _httpClient = new HttpClient();
            var typeClient = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(typeClient);
            ApiUrl = "https://localhost:7274/api/LoginAPI";
        }
        // GET: LoginController
        public ActionResult Index()
        {
            if (!Request.Cookies.ContainsKey("userName"))
            {
                return View();
            }
            return RedirectToAction("", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string username, string password)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{ApiUrl}/{username}/{password}");
            if (responseMessage.IsSuccessStatusCode)
            {
                Response.Cookies.Append("userName", "Admin");
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role,"Admin")
                    };
                var identity = new ClaimsIdentity(claims, "Admin");
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("Admin", principal, new AuthenticationProperties()
                {
                    IsPersistent = true
                });
                //    var routeValues = new RouteValueDictionary
                //{
                //    {"area","Admin" },
                //    {"returnURL",Request.Query["ReturnURL"] }
                //};
                //return RedirectToAction("Index", "Home", routeValues);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Incorrect password or email or no account.");
            }
            throw new ArgumentException("Incorrect password or email or no account.");
        }
        #region Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Admin");
            Response.Cookies.Delete("userName");
            return RedirectToAction("Index", "Home");
        }
        #endregion
        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
