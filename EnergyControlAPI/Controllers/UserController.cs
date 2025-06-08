using EnergyControlAPI.Data.Contexts;
using EnergyControlAPI.DTOs;
using EnergyControlAPI.Helpers;
using EnergyControlAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public UserController(DatabaseContext db) => _db = db;

        // GET: api/User?pageNumber=1&pageSize=10
        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<PagedResponse<UserDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _db.Users.AsNoTracking();
            var total = await query.CountAsync();
            var items = await query
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email })
                .ToListAsync();

            return Ok(new PagedResponse<UserDto>
            {
                Items = items,
                TotalItems = total,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
        }

        // GET: api/User/5
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var u = await _db.Users.AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new UserDto { Id = x.Id, Name = x.Name, Email = x.Email })
                .FirstOrDefaultAsync();
            if (u == null) return NotFound();
            return Ok(u);
        }

        // POST: api/User
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            var user = new UserModel
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                Role = dto.Role
            };
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            var result = new UserDto { Id = user.Id, Name = user.Name, Email = user.Email };
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, result);
        }

        // PUT: api/User/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/User/5
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        private string HashPassword(string plain) =>
            BCrypt.Net.BCrypt.HashPassword(plain);
    }
}
