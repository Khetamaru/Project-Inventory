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
    public class StorageLibrariesController : ControllerBase
    {
        private readonly StorageLibraryContext _context;

        public StorageLibrariesController(StorageLibraryContext context)
        {
            _context = context;
        }

        // GET: api/StorageLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageLibrary>>> GetStorageLibraries()
        {
            return await _context.StorageLibraries.ToListAsync();
        }

        // GET: api/StorageLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageLibrary>> GetStorageLibrary(long id)
        {
            var storageLibrary = await _context.StorageLibraries.FindAsync(id);

            if (storageLibrary == null)
            {
                return NotFound();
            }

            return storageLibrary;
        }

        // PUT: api/StorageLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageLibrary(long id, StorageLibrary storageLibrary)
        {
            if (id != storageLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(storageLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageLibraryExists(id))
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

        // POST: api/StorageLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageLibrary>> PostStorageLibrary(StorageLibrary storageLibrary)
        {
            _context.StorageLibraries.Add(storageLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStorageLibrary), new { id = storageLibrary.Id }, storageLibrary);
        }

        // DELETE: api/StorageLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageLibrary(long id)
        {
            var storageLibrary = await _context.StorageLibraries.FindAsync(id);
            if (storageLibrary == null)
            {
                return NotFound();
            }

            _context.StorageLibraries.Remove(storageLibrary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageLibraryExists(long id)
        {
            return _context.StorageLibraries.Any(e => e.Id == id);
        }
    }
}
