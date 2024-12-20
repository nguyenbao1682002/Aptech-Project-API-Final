using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsCategoryController : BaseAdminController
    {
        #region Varieble
        private readonly HttpClient _httpClient = null;
        private string NewsCategoryApiUrl = "";
        #endregion

        #region Construct
        public NewsCategoryController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            NewsCategoryApiUrl = "https://localhost:7274/api/NewsCategoryControllerApi";
        }
        #endregion

        #region Index
        // GET: NewsCategoryController
        public async Task<IActionResult> Index(int? page)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(NewsCategoryApiUrl);
            string strData = await res.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<NewsCategory> newsCategoryList = JsonSerializer.Deserialize<List<NewsCategory>>(strData, option);
            int pageNumber = (page ?? 1); // Nếu page là null, sử dụng trang 1.
            int pageSize = 5; // 
            IPagedList<NewsCategory> pagedNewsCategories = newsCategoryList.ToPagedList(pageNumber, pageSize);
            return View(pagedNewsCategories);
        }
        #endregion

        #region Create
        // GET: NewsCategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsCategory n)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                n.CategoryID = random.Next();
                string strData = JsonSerializer.Serialize(n);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PostAsync(NewsCategoryApiUrl, contentData);

                if (res.IsSuccessStatusCode)
                {
                    SetAlert("News category inserted successfully", "success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error while calling Web API");
                }
            }
            return View(n);
        }
        #endregion

        #region Edit
        // GET: NewsCategoryController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{NewsCategoryApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                NewsCategory n = JsonSerializer.Deserialize<NewsCategory>(strData, options);
                return View(n);
            }
            return View();
        }
        // POST: NewsCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NewsCategory n)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(n);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync($"{NewsCategoryApiUrl}/{id}", contentData);
                if (res.IsSuccessStatusCode)
                {
                    SetAlert("Category updated successfully", "warning");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Error while call Web API");
                }
            }
            return View(n);
        }
        #endregion

        #region Delete
        // GET: NewsCategoryController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{NewsCategoryApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                NewsCategory n = JsonSerializer.Deserialize<NewsCategory>(strData, options);
                return View(n);
            }
            return View();
        }

        // POST: NewsCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{NewsCategoryApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                SetAlert("Category deleted successfully", "success");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Error while call Web API");
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion

        // GET: DeleteId
        #region DeleteId
        [HttpPost]
        public async Task<JsonResult> DeleteId(int id)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"{NewsCategoryApiUrl}/{id}");
                HttpResponseMessage responseMessageData = await _httpClient.GetAsync(NewsCategoryApiUrl);
                var data = await responseMessageData.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<NewsCategory> newsCategories = JsonSerializer.Deserialize<List<NewsCategory>>(data, options);
                if (newsCategories == null)
                {
                    return Json(new { success = false, message = "No newscategory found" });
                }
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