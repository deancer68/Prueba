using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Database;
using Storage.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PeopleController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = _context.People.FirstOrDefault(x => x.Id == id);
            if (person == null) return BadRequest();
            return Ok(person);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.People.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Put(Person person)
        {
            var existing = await _context.People.FirstOrDefaultAsync(x => x.Id == person.Id);
            if (existing == null) return BadRequest();
            existing.FirstName = person.FirstName;
            existing.LastName = person.LastName;
            existing.DOB = person.DOB;
            await _context.SaveChangesAsync();
            return Ok(existing);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return BadRequest();
            _context.People.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
