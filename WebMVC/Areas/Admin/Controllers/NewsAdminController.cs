using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectBussiness;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsAdminController : BaseAdminController
    {
        #region Variable
        private readonly HttpClient _httpClient = null;
        private string NewsApiUrl = "";
        PetroleumBusinessDBContext db;
        #endregion

        #region Constructor
        public NewsAdminController()
        {
            db = new PetroleumBusinessDBContext();
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            NewsApiUrl = "https://localhost:7274/api/NewsControllerApi";
        }
        #endregion
        // GET: NewsController
        #region Index
        public async Task<IActionResult> Index(int? page)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(NewsApiUrl);
            string strData = await res.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<News> newsList = JsonSerializer.Deserialize<List<News>>(strData, option);

            int pageNumber = page ?? 1;
            int pageSize = 5;

            IPagedList<News> pagedNewsList = newsList.ToPagedList(pageNumber, pageSize);
            return View(pagedNewsList);
        }
        #endregion


        // GET: NewsController/Details/5
        #region Detail
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"{NewsApiUrl}/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var news = JsonSerializer.Deserialize<News>(data, options);

                return View(news);
            }
            else
            {
                return View("Error", new { message = $"Error fetching news details: {responseMessage.StatusCode}" });
            }
        }
        #endregion

        // GET: NewsController/Create
        #region Create
        public async Task<ActionResult> Create()
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("https://localhost:7274/api/NewsControllerApi/GetNewsCategory");
            var data = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            List<NewsCategory> listCategory = JsonSerializer.Deserialize<List<NewsCategory>>(data, options);
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in listCategory)
            {
                selectList.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
            }
            ViewBag.Items = selectList;
            return View();
        }

        // POST: NewsController/Create API//////////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsImage n, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                News news = new News
                {
                    NewsID = random.Next(),
                    Title = n.Title,
                    Contents = n.Contents,
                    ShortContents = n.ShortContents,
                    DateSubmitted = n.DateSubmitted,
                    AccountID = n.AccountID,
                    CategoryID = n.CategoryID,
                };

                if (imageFile != null && imageFile.Length > 0)
                {
                    // Lấy tên tệp từ đường dẫn đầy đủ
                    var fileName = Path.GetFileName(imageFile.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                    using (var stream = System.IO.File.Create(path))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    // Gán chỉ tên tệp (không có đường dẫn đầy đủ)
                    news.Picture = fileName;
                }

                // Send data to API
                string strData = JsonSerializer.Serialize(news);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(NewsApiUrl, contentData);

                if (res.IsSuccessStatusCode)
                {
                    SetAlert("Successfully created new news", "success");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Log error or handle API response
                    ModelState.AddModelError("", "Creating new news failed");
                }
            }

            return View(n);
        }
        #endregion

        // GET: NewsController/Edit/5
        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{NewsApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                News n = JsonSerializer.Deserialize<News>(strData, options);
                HttpResponseMessage responseMessageList = await _httpClient.GetAsync("https://localhost:7274/api/NewsControllerApi/GetNewsCategory");
                var dataList = await responseMessageList.Content.ReadAsStringAsync();
                var optionsList = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<NewsCategory> newsCate = JsonSerializer.Deserialize<List<NewsCategory>>(dataList, optionsList);
                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (var item in newsCate)
                {
                    selectListItems.Add(new SelectListItem { Value = item.CategoryID.ToString(), Text = item.CategoryName });
                }
                ViewBag.Items = selectListItems;
                return View(n);
            }
            return NotFound();
        }


        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News n)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonSerializer.Serialize(n);
                var contentData = new StringContent(strData, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PutAsync($"{NewsApiUrl}/{id}", contentData);
                if (res.IsSuccessStatusCode)
                {
                    SetAlert("News updated successfully", "warning");
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

        // GET: NewsController/Delete/5
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage res = await _httpClient.GetAsync($"{NewsApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                string strData = await res.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                News n = JsonSerializer.Deserialize<News>(strData, options);
                return View(n);
            }
            return View();
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            HttpResponseMessage res = await _httpClient.DeleteAsync($"{NewsApiUrl}/{id}");
            if (res.IsSuccessStatusCode)
            {
                SetAlert("News deleted successfully", "success");
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
                HttpResponseMessage responseMessage = await _httpClient.DeleteAsync($"{NewsApiUrl}/{id}");
                HttpResponseMessage responseMessageData = await _httpClient.GetAsync(NewsApiUrl);
                var data = await responseMessageData.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                List<News> news = JsonSerializer.Deserialize<List<News>>(data, options);
                if (news == null)
                {
                    return Json(new { success = false, message = "No news found" });
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