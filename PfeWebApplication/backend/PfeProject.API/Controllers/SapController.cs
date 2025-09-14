using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Saps;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SapController : ControllerBase
    {
        private readonly ISapService _sapService;

        public SapController(ISapService sapService)
        {
            _sapService = sapService;
        }

        // GET: /api/sap?isActive=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SapReadDto>>> GetAll([FromQuery] bool? isActive = true)
        {
            var result = await _sapService.GetAllAsync(isActive);
            return Ok(result);
        }

        // GET: /api/sap/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SapReadDto>> GetById(int id)
        {
            var sap = await _sapService.GetByIdAsync(id);
            if (sap == null)
                return NotFound();

            return Ok(sap);
        }

        // POST: /api/sap
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SapCreateDto dto)
        {
            await _sapService.CreateAsync(dto);
            return Ok(new { message = "SAP entry created successfully." });
        }

        // PUT: /api/sap/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SapUpdateDto dto)
        {
            var success = await _sapService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // PUT: /api/sap/{id}/set-active?value=true
        [HttpPut("{id}/set-active")]
        public async Task<ActionResult> SetActiveStatus(int id, [FromQuery] bool value)
        {
            var success = await _sapService.SetActiveStatusAsync(id, value);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
