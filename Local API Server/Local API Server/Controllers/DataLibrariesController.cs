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
    public class DataLibrariesController : ControllerBase
    {
        private readonly DataLibraryContext _context;

        public DataLibrariesController(DataLibraryContext context)
        {
            _context = context;
        }

        // GET: api/DataLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataLibrary>>> GetDataLibraries()
        {
            return await _context.DataLibraries.ToListAsync();
        }

        // GET: api/DataLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataLibrary>> GetDataLibrary(long id)
        {
            var DataLibrary = await _context.DataLibraries.FindAsync(id);

            if (DataLibrary == null)
            {
                return NotFound();
            }

            return DataLibrary;
        }

        // PUT: api/DataLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataLibrary(long id, DataLibrary DataLibrary)
        {
            if (id != DataLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(DataLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataLibraryExists(id))
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

        // POST: api/DataLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataLibrary>> PostDataLibrary(DataLibrary DataLibrary)
        {
            _context.DataLibraries.Add(DataLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDataLibrary), new { id = DataLibrary.Id }, DataLibrary);
        }

        // DELETE: api/DataLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataLibrary(long id)
        {
            var DataLibrary = await _context.DataLibraries.FindAsync(id);
            if (DataLibrary == null)
            {
                return NotFound();
            }

            _context.DataLibraries.Remove(DataLibrary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataLibraryExists(long id)
        {
            return _context.DataLibraries.Any(e => e.Id == id);
        }
    }
}
