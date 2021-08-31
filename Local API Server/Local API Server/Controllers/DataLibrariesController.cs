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
        public async Task<ActionResult<DataLibrary>> GetDataLibrary(int id)
        {
            var DataLibrary = await _context.DataLibraries.FindAsync(id);

            if (DataLibrary == null)
            {
                return NotFound();
            }

            return DataLibrary;
        }

        // GET: api/DataLibraries/storage/5
        [HttpGet("storage/{storageId}")]
        public async Task<ActionResult<IEnumerable<DataLibrary>>> GetDataLibraryByStorage(int storageId)
        {
            var dataLibrary = await _context.DataLibraries.Where(r => r.StorageId == storageId).ToListAsync();

            if (dataLibrary == null)
            {
                return NotFound();
            }

            return dataLibrary;
        }

        // GET: api/DataLibraries/storage/5
        [HttpGet("codeBar/{codeBar}")]
        public async Task<ActionResult<IEnumerable<DataLibrary>>> GetDataLibraryByStorage(string codeBar)
        {
            var dataLibrary = await _context.DataLibraries.Where(r => r.CodeBar == codeBar).ToListAsync();

            if (dataLibrary == null)
            {
                return NotFound();
            }

            return dataLibrary;
        }

        // GET: api/DataLibraries/storage/5
        [HttpGet("storage/{storageId}/{researchString}")]
        public async Task<ActionResult<IEnumerable<DataLibrary>>> GetDataLibraryByStorage(int storageId, string researchString)
        {
            var dataLibrary = await _context.DataLibraries.Where(r => r.StorageId == storageId).ToListAsync();

            if (dataLibrary == null)
            {
                return NotFound();
            }

            var stringTab = researchString.Split(" ");
            bool[] trigger = new bool[dataLibrary.Count()];
            var dataLibraryShorted = new List<DataLibrary>();
            int i = 0;

            foreach (DataLibrary data in dataLibrary)
            {
                if (data.IsHeader != "True")
                {
                    trigger[i] = false;

                    foreach (string str in stringTab)
                    {
                        if (!data.DataText.Contains(str))
                        {
                            trigger[i] = true;
                        }
                    }
                }

                i++;
            }

            for (i = 0; i < trigger.Length; i++)
            {
                if (!trigger[i])
                {
                    dataLibraryShorted.Add(dataLibrary[i]);
                }
            }

            return dataLibraryShorted;
        }

        // PUT: api/DataLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataLibrary(int id, DataLibrary DataLibrary)
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
        public async Task<IActionResult> DeleteDataLibrary(int id)
        {
            var DataLibrary = await _context.DataLibraries.FindAsync(id);
            if (DataLibrary == null)
            {
                return NotFound();
            }

            _context.DataLibraries.Remove(DataLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/DataLibraries/storage/5
        [HttpDelete("storage/{id}")]
        public async Task<IActionResult> DeleteStorageDataLibrary(int id)
        {
            var dataLibrary = await _context.DataLibraries.Where(r => r.StorageId == id).ToListAsync();
            if (dataLibrary == null)
            {
                return NotFound();
            }

            foreach (DataLibrary data in dataLibrary)
            {
                _context.DataLibraries.Remove(data);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool DataLibraryExists(long id)
        {
            return _context.DataLibraries.Any(e => e.Id == id);
        }

        // DELETE: api/DataLibraries
        [HttpDelete]
        public async Task<IActionResult> DeleteDataLibrary()
        {
            IEnumerable<DataLibrary> DataLibrary = await _context.DataLibraries.ToListAsync();
            if (DataLibrary == null)
            {
                return NotFound();
            }

            foreach (DataLibrary data in DataLibrary)
            {
                _context.DataLibraries.Remove(data);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
