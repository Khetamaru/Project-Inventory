using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using MySql.Data.MySqlClient;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageLibrariesXCustomListLibrariesController : ControllerBase
    {
        private readonly StorageLibraryXCustomListLibraryContext _context;

        public StorageLibrariesXCustomListLibrariesController(StorageLibraryXCustomListLibraryContext context)
        {
            _context = context;
        }

        // GET: api/StorageLibrariesXCustomListLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageLibraryXCustomListLibrary>>> GetStorageLibrariesXCustomListLibraries()
        {
            return await _context.StorageLibrariesXCustomListLibraries.ToListAsync();
        }

        // GET: api/StorageLibrariesXCustomListLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageLibraryXCustomListLibrary>> GetStorageLibraryXCustomListLibrary(int id)
        {
            var StorageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.FindAsync(id);

            if (StorageLibraryXCustomListLibrary == null)
            {
                return NotFound();
            }

            return StorageLibraryXCustomListLibrary;
        }

        // GET: api/StorageLibrariesXCustomListLibraries/customList/5
        [HttpGet("customList/{customListId}")]
        public async Task<ActionResult<IEnumerable<StorageLibraryXCustomListLibrary>>> GetDataLibraryByCustomList(int customListId)
        {
            var storageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.Where(r => r.CustomListId == customListId).ToListAsync();

            if (storageLibraryXCustomListLibrary == null)
            {
                return NotFound();
            }

            return storageLibraryXCustomListLibrary;
        }

        // GET: api/StorageLibrariesXCustomListLibraries/storage/5
        [HttpGet("storage/{storageId}")]
        public async Task<ActionResult<IEnumerable<StorageLibraryXCustomListLibrary>>> GetDataLibraryByStorage(int storageId)
        {
            var storageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.Where(r => r.StorageId == storageId).ToListAsync();

            if (storageLibraryXCustomListLibrary == null)
            {
                return NotFound();
            }

            return storageLibraryXCustomListLibrary;
        }

        // PUT: api/StorageLibrariesXCustomListLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageLibraryXCustomListLibrary(int id, StorageLibraryXCustomListLibrary StorageLibraryXCustomListLibrary)
        {
            if (id != StorageLibraryXCustomListLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(StorageLibraryXCustomListLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageLibraryXCustomListLibraryExists(id))
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

        // POST: api/StorageLibrariesXCustomListLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageLibraryXCustomListLibrary>> PostStorageLibraryXCustomListLibrary(StorageLibraryXCustomListLibrary StorageLibraryXCustomListLibrary)
        {
            _context.StorageLibrariesXCustomListLibraries.Add(StorageLibraryXCustomListLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStorageLibraryXCustomListLibrary), new { id = StorageLibraryXCustomListLibrary.Id }, StorageLibraryXCustomListLibrary);
        }

        // DELETE: api/StorageLibrariesXCustomListLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageLibraryXCustomListLibrary(int id)
        {
            var StorageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.FindAsync(id);
            if (StorageLibraryXCustomListLibrary == null)
            {
                return NotFound();
            }

            _context.StorageLibrariesXCustomListLibraries.Remove(StorageLibraryXCustomListLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/StorageLibrariesXCustomListLibraries/storage/5
        [HttpDelete("storage/{storageId}")]
        public async Task<ActionResult<IEnumerable<StorageLibraryXCustomListLibrary>>> DeleteCrossByStorageId(int storageId)
        {
            var storageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.Where(r => r.StorageId == storageId).ToListAsync();

            if (storageLibraryXCustomListLibrary == null)
            {
                return Ok();
            }

            foreach (StorageLibraryXCustomListLibrary SLXCLL in storageLibraryXCustomListLibrary)
            {
                _context.StorageLibrariesXCustomListLibraries.Remove(SLXCLL);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool StorageLibraryXCustomListLibraryExists(long id)
        {
            return _context.StorageLibrariesXCustomListLibraries.Any(e => e.Id == id);
        }

        // DELETE: api/StorageLibrariesXCustomListLibraries
        [HttpDelete]
        public async Task<IActionResult> DeleteStorageLibraryXCustomListLibrary()
        {
            IEnumerable<StorageLibraryXCustomListLibrary> StorageLibraryXCustomListLibrary = await _context.StorageLibrariesXCustomListLibraries.ToListAsync();
            if (StorageLibraryXCustomListLibrary == null)
            {
                return NotFound();
            }

            foreach (StorageLibraryXCustomListLibrary storageXList in StorageLibraryXCustomListLibrary)
            {
                _context.StorageLibrariesXCustomListLibraries.Remove(storageXList);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
