using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //return View();
            return View(_dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags).ToList().ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET Product Details Action Method

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(c => c.ProductTypes).FirstOrDefault(c=>c.Id==id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
