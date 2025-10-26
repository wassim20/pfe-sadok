using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Warehouses;
using System.Security.Claims;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // 🏢 Added authorization
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehousesController(IWarehouseService service)
        {
            _service = service;
        }

        // GET: api/warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseReadDto>>> GetAll([FromQuery] bool? isActive = true)
        {
            var companyId = GetCurrentUserCompanyId(); // 🏢 Get company ID
            var warehouses = await _service.GetAllByCompanyAsync(companyId, isActive); // 🏢 Use company-aware method
            return Ok(warehouses);
        }

        // GET: api/warehouses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseReadDto>> GetById(int id)
        {
            var companyId = GetCurrentUserCompanyId(); // 🏢 Get company ID
            var warehouse = await _service.GetByIdAndCompanyAsync(id, companyId); // 🏢 Use company-aware method
            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }

        // POST: api/warehouses
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] WarehouseCreateDto dto)
        {
            var companyId = GetCurrentUserCompanyId(); // 🏢 Get company ID
            await _service.CreateForCompanyAsync(dto, companyId); // 🏢 Use company-aware method
            return Ok(new { message = "Warehouse created successfully." });
        }

        // PUT: api/warehouses/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] WarehouseUpdateDto dto)
        {
            var companyId = GetCurrentUserCompanyId(); // 🏢 Get company ID
            var success = await _service.UpdateForCompanyAsync(id, dto, companyId); // 🏢 Use company-aware method
            if (!success)
                return NotFound();

            return NoContent();
        }

        // PUT: api/warehouses/{id}/set-active?value=true
        [HttpPut("{id}/set-active")]
        public async Task<ActionResult> SetActiveStatus(int id, [FromQuery] bool value)
        {
            var success = await _service.SetActiveStatusAsync(id, value);
            if (!success)
                return NotFound();

            return NoContent();
        }

        private int GetCurrentUserCompanyId() // 🏢 Helper method to get company ID from JWT
        {
            var companyIdClaim = User.FindFirst("CompanyId");
            if (companyIdClaim != null && int.TryParse(companyIdClaim.Value, out int companyId))
            {
                return companyId;
            }
            throw new UnauthorizedAccessException("User company ID not found in token");
        }
    }
}
