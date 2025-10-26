using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PfeProject.Application.Interfaces;
using PfeProject.Application.Models.Saps;
using System.Security.Claims;

namespace PfeProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var companyId = GetCurrentUserCompanyId();
            var result = await _sapService.GetAllByCompanyAsync(companyId, isActive);
            return Ok(result);
        }

        // GET: /api/sap/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SapReadDto>> GetById(int id)
        {
            var companyId = GetCurrentUserCompanyId();
            var sap = await _sapService.GetByIdAndCompanyAsync(id, companyId);
            if (sap == null)
                return NotFound();

            return Ok(sap);
        }

        // POST: /api/sap
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SapCreateDto dto)
        {
            var companyId = GetCurrentUserCompanyId();
            await _sapService.CreateForCompanyAsync(dto, companyId);
            return Ok(new { message = "SAP entry created successfully." });
        }

        // PUT: /api/sap/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] SapUpdateDto dto)
        {
            var companyId = GetCurrentUserCompanyId();
            var success = await _sapService.UpdateForCompanyAsync(id, dto, companyId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // PUT: /api/sap/{id}/set-active?value=true
        [HttpPut("{id}/set-active")]
        public async Task<ActionResult> SetActiveStatus(int id, [FromQuery] bool value)
        {
            var companyId = GetCurrentUserCompanyId();
            var success = await _sapService.SetActiveStatusForCompanyAsync(id, value, companyId);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: /api/sap/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var companyId = GetCurrentUserCompanyId();
            var success = await _sapService.DeleteForCompanyAsync(id, companyId);
            if (!success)
                return NotFound();

            return Ok(new { message = "SAP entry deleted successfully." });
        }

        private int GetCurrentUserCompanyId()
        {
            var companyIdClaim = User.FindFirst("CompanyId");
            if (companyIdClaim != null && int.TryParse(companyIdClaim.Value, out int companyId))
            {
                return companyId;
            }
            throw new UnauthorizedAccessException("User company ID not found in token");
        }

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] SapAddStockDto dto) // Définir ce DTO
        {
            // Le DTO devrait contenir UsCode et Quantite (à ajouter)
            // public class SapAddStockDto { public string UsCode { get; set; } public int Quantite { get; set; } }

            var success = await _sapService.AddStockAsync(dto.UsCode, dto.Quantite);
            if (!success)
                return NotFound(new { Message = $"Enregistrement SAP pour US '{dto.UsCode}' non trouvé." });

            return NoContent();
        }
        public class SapAddStockDto { public string UsCode { get; set; } public int Quantite { get; set; } }
    }
}
