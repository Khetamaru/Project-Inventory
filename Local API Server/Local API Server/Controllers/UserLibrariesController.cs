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
    public class UserLibrariesController : ControllerBase
    {
        private readonly UserLibraryContext _context;

        public UserLibrariesController(UserLibraryContext context)
        {
            _context = context;
        }

        // GET: api/UserLibraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLibrary>>> GetUserLibraries()
        {
            return await _context.UserLibraries.Where(user => user.IsActive == true).ToListAsync();
        }

        // GET: api/UserLibraries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLibrary>> GetUserLibrary(int id)
        {
            var UserLibrary = await _context.UserLibraries.FindAsync(id);

            if (UserLibrary == null)
            {
                return NotFound();
            }

            return UserLibrary;
        }

        // PUT: api/UserLibraries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLibrary(int id, UserLibrary UserLibrary)
        {
            if (id != UserLibrary.Id)
            {
                return BadRequest();
            }

            _context.Entry(UserLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLibraryExists(id))
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

        // POST: api/UserLibraries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLibrary>> PostUserLibrary(UserLibrary UserLibrary)
        {
            _context.UserLibraries.Add(UserLibrary);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserLibrary), new { id = UserLibrary.Id }, UserLibrary);
        }

        // DELETE: api/UserLibraries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLibrary(int id)
        {
            var UserLibrary = await _context.UserLibraries.FindAsync(id);
            if (UserLibrary == null)
            {
                return NotFound();
            }

            UserLibrary.IsActive = false;

            _context.Entry(UserLibrary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLibraryExists(id))
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

        private bool UserLibraryExists(long id)
        {
            return _context.UserLibraries.Any(e => e.Id == id);
        }

        // DELETE: api/UserLibraries
        [HttpDelete]
        public async Task<IActionResult> DeleteUserLibrary()
        {
            IEnumerable<UserLibrary> UserLibrary = await _context.UserLibraries.ToListAsync();
            if (UserLibrary == null)
            {
                return NotFound();
            }

            foreach (UserLibrary user in UserLibrary)
            {
                _context.UserLibraries.Remove(user);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
