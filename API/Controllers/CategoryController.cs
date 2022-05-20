using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Database;
using Storage.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = _context.Category.FirstOrDefault(x => x.Id == id);
            if (category == null) return BadRequest();
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Category.OrderBy(x => x.Name).ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> Put(Category category)
        {
            var existing = await _context.Category.FirstOrDefaultAsync(x => x.Id == category.Id);
            if (existing == null) return BadRequest();
            existing.Name = category.Name;
            await _context.SaveChangesAsync();
            return Ok(existing);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return BadRequest();
            _context.Category.Remove(existing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
