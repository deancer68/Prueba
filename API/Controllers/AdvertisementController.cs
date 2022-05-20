using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.Database;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AdvertisementController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string searchText, int category = -1)
        {
            if (string.IsNullOrEmpty(searchText)) return BadRequest("Parameters needed");

            var advertisements = _applicationDbContext.Advertisement
                .Include(x => x.Category)
                .Include(x => x.Owner);

            /*
              category == -1 ? advertisements.Where(x=>x.Description.IndexOf(searchText)!=-1).OrderBy(x => x.Category.Name)
                    .Select(x => new
                     {
                         x.Category.Name,
                         x.Category.Id,
                         x.Description,
                         x.Price,
                         x.StartDate,
                         x.Owner.FirstName,
                         x.Owner.LastName
                     }) 
                :
            advertisements.Where(x => x.Category.Id == category && x.Description.IndexOf(searchText) != -1).OrderBy(x => x.Category.Name)
                    .Select(x => new
                    {
                        x.Category.Name,
                        x.Category.Id,
                        x.Description,
                        x.Price,
                        x.StartDate,
                        x.Owner.FirstName,
                        x.Owner.LastName
                    })
              */
            return Ok(category == -1 ?
                    advertisements.Where(x=>x.Description.IndexOf(searchText)!=-1).OrderBy(x => x.Category.Name)
                    .Select(x => new
                    {
                        x.Category.Name,
                        x.Category.Id,
                        x.Description,
                        x.Price,
                        x.StartDate,
                        x.Owner.FirstName,
                        x.Owner.LastName
                    }) 
                    : 
                    advertisements.Where(x => x.Category.Id == category && x.Description.IndexOf(searchText) != -1).OrderBy(x => x.Category.Name)
                    .Select(x => new
                    {
                        x.Category.Name,
                        x.Category.Id,
                        x.Description,
                        x.Price,
                        x.StartDate,
                        x.Owner.FirstName,
                        x.Owner.LastName
                    })
                );
        }


        [HttpPost]
        public async Task<IActionResult> Post(AdvertisementVM advertisement)
        {
            var category = await _applicationDbContext.Category.FirstOrDefaultAsync(x => x.Id.Equals(advertisement.Category));
            if (category == null) return BadRequest("Category not found");
            var person = await _applicationDbContext.People.FirstOrDefaultAsync(x => x.Id.Equals(advertisement.Owner));
            if (person == null) return BadRequest("Owner not found");

            var dbObject = new Storage.Models.Advertisement()
            {
                Description = advertisement.Description,
                Price = advertisement.Price,
                StartDate = advertisement.StartDate,
                Views = 0,
                Category = category,
                Owner = person
            };
            _applicationDbContext.Advertisement.Add(dbObject);
            _applicationDbContext.SaveChanges();
            return Ok();
        }


    }
}
