using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Articles;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _service;

        public ArticlesController(IArticleService service)
        {
            _service = service;
        }

        // ✅ GET: /api/articles?isActive=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleReadDto>>> GetAll([FromQuery] bool? isActive = true)
        {
            var articles = await _service.GetAllAsync(isActive);
            return Ok(articles);
        }

        // ✅ GET: /api/articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleReadDto>> GetById(int id)
        {
            var article = await _service.GetByIdAsync(id);
            if (article == null) return NotFound();
            return Ok(article);
        }

        // ✅ POST: /api/articles
        [HttpPost]
        public async Task<ActionResult<ArticleReadDto>> Create([FromBody] ArticleCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        // ✅ PUT: /api/articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticleUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // ✅ PUT: /api/articles/5/set-active?value=false
        [HttpPut("{id}/set-active")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool value)
        {
            var success = await _service.SetActiveStatusAsync(id, value);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
