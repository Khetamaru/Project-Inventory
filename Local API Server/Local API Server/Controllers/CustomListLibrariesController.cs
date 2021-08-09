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
    public class CustomListLibrariesController : ControllerBase
    {
        private readonly CustomListLibraryContext _context;

        public CustomListLibrariesController(CustomListLibraryContext context)
        {
            _context = context;
        }

        // GET: api/CustomListLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomListLibrary>>> GetCustomListLibraries()
        {
            return await _context.CustomListLibraries.ToListAsync();
        }

        // GET: api/CustomListLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomListLibrary>> GetCustomListLibrary(int id)
        {
            var customListLibrary = await _context.CustomListLibraries.FindAsync(id);

            if (customListLibrary == null)
            {
                return NotFound();
            }

            return customListLibrary;
        }

        // PUT: api/CustomListLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomListLibrary(int id, CustomListLibrary customListLibrary)
        {
            if (id != customListLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(customListLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomListLibraryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/CustomListLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomListLibrary>> PostCustomListLibrary(CustomListLibrary customListLibrary)
        {
            _context.CustomListLibraries.Add(customListLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomListLibrary), new { id = customListLibrary.Id }, customListLibrary);
        }

        // DELETE: api/CustomListLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomListLibrary(int id)
        {
            var customListLibrary = await _context.CustomListLibraries.FindAsync(id);
            if (customListLibrary == null)
            {
                return NotFound();
            }

            _context.CustomListLibraries.Remove(customListLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CustomListLibraryExists(int id)
        {
            return _context.CustomListLibraries.Any(e => e.Id == id);
        }
    }
}
