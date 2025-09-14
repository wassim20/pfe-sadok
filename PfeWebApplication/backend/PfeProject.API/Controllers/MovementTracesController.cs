using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementTracesController : ControllerBase
    {
        private readonly IMovementTraceService _service;

        public MovementTracesController(IMovementTraceService service)
        {
            _service = service;
        }

        // ✅ GET: /api/movementtraces?isActive=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovementTraceReadDto>>> GetAll([FromQuery] bool? isActive = true)
        {
            var traces = await _service.GetAllAsync(isActive);
            return Ok(traces);
        }

        // ✅ GET: /api/movementtraces/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovementTraceReadDto>> GetById(int id)
        {
            var trace = await _service.GetByIdAsync(id);
            if (trace == null) return NotFound();

            return Ok(trace);
        }

        // ✅ POST: /api/movementtraces
        [HttpPost]
        public async Task<ActionResult<MovementTraceReadDto>> Create([FromBody] MovementTraceCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return Ok(created);
        }

        // ✅ PUT: /api/movementtraces/{id}/set-active?value=true
        [HttpPut("{id}/set-active")]
        public async Task<IActionResult> SetActive(int id, [FromQuery] bool value)
        {
            var success = await _service.SetActiveStatusAsync(id, value);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}
