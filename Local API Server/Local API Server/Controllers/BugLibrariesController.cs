using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Local_API_Server.Models;
using MySql.Data.MySqlClient;
using System;

namespace Local_API_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugLibrariesController : ControllerBase
    {
        private readonly BugLibraryContext _context;

        public BugLibrariesController(BugLibraryContext context)
        {
            _context = context;
        }

        // GET: api/BugLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BugLibrary>>> GetBugLibraries()
        {
            return await _context.BugLibraries.OrderByDescending(r => r.Id).ToListAsync();
        }

        // GET: api/BugLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BugLibrary>> GetBugLibrary(int id)
        {
            var BugLibrary = await _context.BugLibraries.FindAsync(id);

            if (BugLibrary == null)
            {
                return NotFound();
            }

            return BugLibrary;
        }

        // PUT: api/BugLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBugLibrary(int id, BugLibrary BugLibrary)
        {
            if (id != BugLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(BugLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugLibraryExists(id))
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

        // POST: api/BugLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BugLibrary>> PostBugLibrary(BugLibrary BugLibrary)
        {
            _context.BugLibraries.Add(BugLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBugLibrary), new { id = BugLibrary.Id }, BugLibrary);
        }

        // DELETE: api/BugLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBugLibrary(int id)
        {
            var BugLibrary = await _context.BugLibraries.FindAsync(id);
            if (BugLibrary == null)
            {
                return NotFound();
            }

            _context.BugLibraries.Remove(BugLibrary);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool BugLibraryExists(long id)
        {
            return _context.BugLibraries.Any(e => e.Id == id);
        }

        // DELETE: api/BugLibraries
        [HttpDelete]
        public async Task<IActionResult> DeleteBugLibrary()
        {
            IEnumerable<BugLibrary> BugLibrary = await _context.BugLibraries.ToListAsync();
            if (BugLibrary == null)
            {
                return NotFound();
            }

            foreach (BugLibrary bug in BugLibrary)
            {
                _context.BugLibraries.Remove(bug);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
