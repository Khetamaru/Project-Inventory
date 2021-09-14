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
    public class ListOptionLibrariesController : ControllerBase
    {
        private readonly ListOptionLibraryContext _context;

        public ListOptionLibrariesController(ListOptionLibraryContext context)
        {
            _context = context;
        }

        // GET: api/ListOptionLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListOptionLibrary>>> GetListOptionLibraries()
        {
            return await _context.ListOptionLibraries.ToListAsync();
        }

        // GET: api/ListOptionLibraries/customList/5
        [HttpGet("customList/{id}")]
        public async Task<ActionResult<IEnumerable<ListOptionLibrary>>> GetListOptionLibraryByCustomListId(int id)
        {
            var listOptionLibrary = await _context.ListOptionLibraries.Where(r => r.CustomListId == id).ToListAsync();

            if (listOptionLibrary == null)
            {
                return NotFound();
            }

            return listOptionLibrary;
        }

        // GET: api/ListOptionLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListOptionLibrary>> GetListOptionLibrary(int id)
        {
            var listOptionLibrary = await _context.ListOptionLibraries.FindAsync(id);

            if (listOptionLibrary == null)
            {
                return NotFound();
            }

            return listOptionLibrary;
        }

        // PUT: api/ListOptionLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListOptionLibrary(int id, ListOptionLibrary listOptionLibrary)
        {
            if (id != listOptionLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(listOptionLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListOptionLibraryExists(id))
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

        // POST: api/ListOptionLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListOptionLibrary>> PostListOptionLibrary(ListOptionLibrary listOptionLibrary)
        {
            _context.ListOptionLibraries.Add(listOptionLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetListOptionLibrary), new { id = listOptionLibrary.Id }, listOptionLibrary);
        }

        // DELETE: api/ListOptionLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListOptionLibrary(int id)
        {
            var listOptionLibrary = await _context.ListOptionLibraries.FindAsync(id);
            if (listOptionLibrary == null)
            {
                return NotFound();
            }

            _context.ListOptionLibraries.Remove(listOptionLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ListOptionLibraryExists(int id)
        {
            return _context.ListOptionLibraries.Any(e => e.Id == id);
        }
    }
}
