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
    public class StorageXDataController : ControllerBase
    {
        private readonly StorageXDataContext _context;

        public StorageXDataController(StorageXDataContext context)
        {
            _context = context;
        }

        // GET: api/StorageXData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageXData>>> GetStorageXData()
        {
            return await _context.StorageXData.ToListAsync();
        }

        // GET: api/StorageXData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageXData>> GetStorageXData(long id)
        {
            var StorageXData = await _context.StorageXData.FindAsync(id);

            if (StorageXData == null)
            {
                return NotFound();
            }

            return StorageXData;
        }

        // PUT: api/StorageXData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageXData(long id, StorageXData StorageXData)
        {
            if (id != StorageXData.Id)
            {
                return BadRequest();
            }

            _context.Entry(StorageXData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageXDataExists(id))
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

        // POST: api/StorageXData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageXData>> PostStorageXData(StorageXData StorageXData)
        {
            _context.StorageXData.Add(StorageXData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStorageXData), new { id = StorageXData.Id }, StorageXData);
        }

        // DELETE: api/StorageXData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageXData(long id)
        {
            var StorageXData = await _context.StorageXData.FindAsync(id);
            if (StorageXData == null)
            {
                return NotFound();
            }

            _context.StorageXData.Remove(StorageXData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageXDataExists(long id)
        {
            return _context.StorageXData.Any(e => e.Id == id);
        }
    }
}
