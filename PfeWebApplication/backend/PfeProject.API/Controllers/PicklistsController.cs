using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Picklists;
using YourProject.Application.Models.Picklists;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PicklistsController : ControllerBase
    {
        private readonly IPicklistService _service;

        public PicklistsController(IPicklistService service)
        {
            _service = service;
        }

        // ✅ GET: /api/picklists?isActive=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PicklistReadDto>>> GetAll([FromQuery] bool? isActive = true)
        {
            var list = await _service.GetAllAsync(isActive);
            return Ok(list);
        }

        // ✅ GET: /api/picklists/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PicklistReadDto>> GetById(int id)
        {
            var picklist = await _service.GetByIdAsync(id);
            if (picklist == null) return NotFound();
            return Ok(picklist);
        }

        // ✅ POST: /api/picklists
        [HttpPost]
        public async Task<ActionResult<PicklistReadDto>> Create([FromBody] PicklistCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        // ✅ PUT: /api/picklists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PicklistUpdateDto dto)
        {
            var success = await _service.UpdateAsync(id, dto);
            if (!success) return NotFound();
            return NoContent();
        }

        // ✅ PUT: /api/picklists/{id}/set-active?value=true
        [HttpPut("{id}/set-active")]
        public async Task<IActionResult> SetActiveStatus(int id, [FromQuery] bool value)
        {
            var result = await _service.SetActiveStatusAsync(id, value);
            if (!result) return NotFound();
            return NoContent();
        }
        

    }
}
