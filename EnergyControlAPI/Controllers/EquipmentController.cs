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
    public class EquipmentController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public EquipmentController(DatabaseContext db) => _db = db;

        // POST: api/Equipment
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EquipmentDto>> Create([FromBody] EquipmentDto dto)
        {
            var entity = new EquipmentModel
            {
                Name = dto.Name,
                Type = dto.Type,
                SectorId = dto.SectorId
            };

            _db.Equipments.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        // GET: api/Equipment/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EquipmentDto>> GetById(int id)
        {
            var entity = await _db.Equipments.FindAsync(id);
            if (entity == null)
                return NotFound();

            var dto = new EquipmentDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                SectorId = entity.SectorId,
                // O Sector será opcionalmente carregado em outra chamada se necessário
            };

            return Ok(dto);
        }

        // GET: api/Equipment
        [HttpGet]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
        {
            var list = await _db.Equipments
                .Select(e => new EquipmentDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Type = e.Type,
                    SectorId = e.SectorId
                })
                .ToListAsync();

            return Ok(list);
        }

        // PUT: api/Equipment/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] EquipmentDto dto)
        {
            var entity = await _db.Equipments.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.Name = dto.Name;
            entity.Type = dto.Type;
            entity.SectorId = dto.SectorId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Equipment/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _db.Equipments.FindAsync(id);
            if (entity == null)
                return NotFound();

            _db.Equipments.Remove(entity);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
