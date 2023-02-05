#nullable disable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.NET6.Context;
using API.NET6.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.NET6.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize]
    public class SetorController : ControllerBase
    {
        private readonly DataContext _context;

        public SetorController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Setor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetSetor()
        {
            return await _context.Setor.ToListAsync();
        }

        // GET: api/Setor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetor(int id)
        {
            var setor = await _context.Setor.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            return setor;
        }

        // PUT: api/Setor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetor(int id, Setor setor)
        {
            if (id != setor.IdSetor)
            {
                return BadRequest();
            }

            _context.Entry(setor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Setor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Setor>> PostSetor(Setor setor)
        {
            _context.Setor.Add(setor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetor", new { id = setor.IdSetor }, setor);
        }

        // DELETE: api/Setor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {
            var setor = await _context.Setor.FindAsync(id);
            if (setor == null)
            {
                return NotFound();
            }

            _context.Setor.Remove(setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetorExists(int id)
        {
            return _context.Setor.Any(e => e.IdSetor == id);
        }
    }
}
