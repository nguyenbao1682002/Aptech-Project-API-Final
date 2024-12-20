using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ObjectBussiness;
using Repository;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsControllerApi : ControllerBase
    {
        INewsRepository _newsRepository = null;
        private readonly IWebHostEnvironment webHostEnvironment;
        private INewsRepository _repository = new NewsRepository();
        PetroleumBusinessDBContext db;

        public NewsControllerApi(IWebHostEnvironment webHostEnvironment)
        {
            db = new PetroleumBusinessDBContext();
            _newsRepository = new NewsRepository();
            this.webHostEnvironment = webHostEnvironment;
        }
        // GET: api/<NewsController>
        [HttpGet]
        public ActionResult<IEnumerable<News>> GetNewsList() => _repository.GetNewsList();

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public ActionResult<News> GetNewsById(int id)
        {
            var news = _repository.GetNewsById(id);
            if (news == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy sản phẩm
            }

            return news;
        }

        // POST api/<NewsController>
        [HttpPost]
        public IActionResult InsertNews(News n)
        {
            try
            {

                _repository.InsertNews(n);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new News record");
            }
        }
        // upload file
        [HttpPost("uploadfile")]
        public async Task<IActionResult> PostWithImage([FromForm] NewsImage n)
        {
            var news = new News
            {
                NewsID = n.NewsID,
                Title = n.Title,
                Contents = n.Contents,
                ShortContents = n.ShortContents,
                DateSubmitted = n.DateSubmitted,
                AccountID = n.AccountID,
                CategoryID = n.CategoryID
            };
            if (n.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(n.ImageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await n.ImageFile.CopyToAsync(stream);
                }
                news.Picture = fileName;
            }
            else
            {
                news.Picture = "";
            }
            _repository.InsertNews(news);
            return Ok(news);
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public IActionResult EditNews(int id, News n)
        {
            var temp = _repository.GetNewsById(id);
            if (temp == null)
            {
                return NotFound();
            }
            _repository.EditNews(n);
            return NoContent();
        }

        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var temp = _repository.GetNewsById(id);
            if (temp == null)
            {
                return NotFound();
            }
            _repository.DeleteNews(temp);
            return NoContent();
        }
        [Route("GetNewsCategory")]
        [HttpGet]
        public IEnumerable<object> GetNewsCategoryID()
        {
            var rs = db.NewsCategories.Select(n => new { n.CategoryID, n.CategoryName }).ToList();
            return rs;
        }
        
    }
}
