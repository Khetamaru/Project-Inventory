using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogLibrariesController : ControllerBase
    {
        private readonly LogLibraryContext _context;

        public LogLibrariesController(LogLibraryContext context)
        {
            _context = context;
        }

        // GET: api/LogLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogLibrary>>> GetLogLibraries()
        {
            return await _context.LogLibraries.ToListAsync();
        }

        // GET: api/LogLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogLibrary>> GetLogLibrary(int id)
        {
            var LogLibrary = await _context.LogLibraries.FindAsync(id);

            if (LogLibrary == null)
            {
                return NotFound();
            }

            return LogLibrary;
        }

        // PUT: api/LogLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogLibrary(int id, LogLibrary LogLibrary)
        {
            if (id != LogLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(LogLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogLibraryExists(id))
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

        // POST: api/LogLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LogLibrary>> PostLogLibrary(LogLibrary LogLibrary)
        {
            _context.LogLibraries.Add(LogLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogLibrary), new { id = LogLibrary.Id }, LogLibrary);
        }

        // DELETE: api/LogLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogLibrary(int id)
        {
            var LogLibrary = await _context.LogLibraries.FindAsync(id);
            if (LogLibrary == null)
            {
                return NotFound();
            }

            _context.LogLibraries.Remove(LogLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool LogLibraryExists(long id)
        {
            return _context.LogLibraries.Any(e => e.Id == id);
        }

        // DELETE: api/LogLibraries
        [HttpDelete]
        public async Task<IActionResult> DeleteLogLibrary()
        {
            IEnumerable<LogLibrary> LogLibrary = await _context.LogLibraries.ToListAsync();
            if (LogLibrary == null)
            {
                return NotFound();
            }

            foreach (LogLibrary log in LogLibrary)
            {
                _context.LogLibraries.Remove(log);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
