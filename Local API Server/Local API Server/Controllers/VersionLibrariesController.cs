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
    public class VersionLibrariesController : ControllerBase
    {
        private readonly VersionLibraryContext _context;

        public VersionLibrariesController(VersionLibraryContext context)
        {
            _context = context;
        }

        // GET: api/VersionLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersionLibrary>>> GetVersionLibraries()
        {
            return await _context.VersionLibraries.ToListAsync();
        }

        // GET: api/VersionLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionLibrary>> GetVersionLibrary(int id)
        {
            var VersionLibrary = await _context.VersionLibraries.FindAsync(id);

            if (VersionLibrary == null)
            {
                return NotFound();
            }

            return VersionLibrary;
        }

        // GET: api/VersionLibraries/uptodate
        [HttpGet("uptodate")]
        public async Task<ActionResult<VersionLibrary>> GetMostRecentVersionLibrary()
        {
            return (await _context.VersionLibraries.Where(e => e.Version == _context.VersionLibraries.Max(f => f.Version)).ToListAsync()).First();
        }

        // POST: api/VersionLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VersionLibrary>> PostVersionLibrary(VersionLibrary VersionLibrary)
        {
            _context.VersionLibraries.Add(VersionLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVersionLibrary), new { id = VersionLibrary.Id }, VersionLibrary);
        }
    }
}
