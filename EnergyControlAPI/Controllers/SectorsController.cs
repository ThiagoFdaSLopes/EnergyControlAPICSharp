// Controller
using EnergyControlAPI.Data.Contexts;
using EnergyControlAPI.DTOs;
using EnergyControlAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorsController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public SectorsController(DatabaseContext db) => _db = db;

        // POST: api/Sectors
        [HttpPost]
        public async Task<ActionResult<SectorDTO>> Create([FromBody] SectorDTO dto)
        {
            var entity = new Sector
            {
                Name = dto.Name,
                FloorNumber = dto.FloorNumber,
                Description = dto.Description
            };

            _db.Sectors.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // GET: api/Sectors/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SectorDTO>> GetById(int id)
        {
            var entity = await _db.Sectors.FindAsync(id);
            if (entity == null)
                return NotFound();

            var dto = new SectorDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                FloorNumber = entity.FloorNumber,
                Description = entity.Description
            };

            return Ok(dto);
        }

        // (Opcional) GET: api/Sectors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SectorDTO>>> GetAll()
        {
            var list = await _db.Sectors
                .Select(e => new SectorDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    FloorNumber = e.FloorNumber,
                    Description = e.Description
                })
                .ToListAsync();

            return Ok(list);
        }

        // PUT: api/Sectors/{id}
        // Apenas Admin pode atualizar
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] SectorDTO dto)
        {
            var entity = await _db.Sectors.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Name = dto.Name;
            entity.FloorNumber = dto.FloorNumber;
            entity.Description = dto.Description;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Sectors/{id}
        // Apenas Admin pode excluir
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Sectors.FindAsync(id);
            if (entity == null)
                return NotFound();

            _db.Sectors.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
