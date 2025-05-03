using Microsoft.AspNetCore.Mvc;
using SalonBooking.Data;

namespace SalonBooking.Controllers
{
    [Route("{slug}")]
    public class BusinessController : Controller
    {
        private readonly AppDbContext _context;

        public BusinessController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string slug)
        {
            var business = _context.Businesses.FirstOrDefault(b => b.Slug == slug);
            if (business == null)
            {
                return NotFound();
            }
            return View(business);
        }

    }
}
