using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using System.Net.Http.Headers;
using System.Text.Json;
using X.PagedList;

namespace WebMVC.Controllers
{
    public class NewsPageController : Controller
    {
        // GET: NewsPageController
        private readonly HttpClient _httpClient = null;
        private string NewsApiUrl = "";
        public NewsPageController()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            NewsApiUrl = "https://localhost:7274/api/NewsControllerApi";
        }
        // GET: NewsController
        public async Task<IActionResult> Index(int? page)
        {
            HttpResponseMessage res = await _httpClient.GetAsync(NewsApiUrl);
            string strData = await res.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<News> news = JsonSerializer.Deserialize<List<News>>(strData, option);
            int pageNumber = (page ?? 1); // Nếu page là null, sử dụng trang 1.
            int pageSize = 3; // 
            IPagedList<News> pagedNews = news.ToPagedList(pageNumber, pageSize);
            return View(pagedNews);
        }
        // GET: NewsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Gửi yêu cầu GET đến API để lấy thông tin chi tiết theo ID
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
    }
}
