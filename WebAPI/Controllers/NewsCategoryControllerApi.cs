using Microsoft.AspNetCore.Mvc;
using ObjectBussiness;
using Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsCategoryControllerApi : ControllerBase
    {
        private INewsCategoryRepository _repository = new NewsCategoryRepository();
        // GET: api/<NewsCategoryControllerApi>
        [HttpGet]
        public ActionResult<IEnumerable<NewsCategory>> GetNewsCategoriesList() => _repository.GetNewsCategoriesList();

        // GET api/<NewsCategoryControllerApi>/5
        [HttpGet("{id}")]
        public ActionResult<NewsCategory> GetNewsCategoryById(int id)
        {
            var newscat = _repository.GetNewsCategoryById(id);
            if (newscat == null)
            {
                return NotFound();
            }
            return newscat;
        }

        // POST api/<NewsCategoryControllerApi>
        [HttpPost]
        public IActionResult InsertNewsCategory(NewsCategory n)
        {
            try
            {
                _repository.InsertNewsCategory(n);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new news category record");
            }
        }

        // PUT api/<NewsCategoryControllerApi>/5
        [HttpPut("{id}")]
        public IActionResult EditNewsCategory(int id, NewsCategory n)
        {
            var temp = _repository.GetNewsCategoryById(id);
            if (temp == null)
            {
                return NotFound();
            }
            _repository.EditNewsCategory(n);
            return NoContent();
        }

        // DELETE api/<NewsCategoryControllerApi>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNewsCategory(int id)
        {
            var temp = _repository.GetNewsCategoryById(id);
            if (temp == null) { return NotFound(); }
            _repository.DeleteNewsCategory(temp);
            return NoContent();
        }
    }
}
