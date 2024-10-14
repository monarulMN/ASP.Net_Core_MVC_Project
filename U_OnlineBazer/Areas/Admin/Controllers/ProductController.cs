using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = _dbContext.Products.Include(c=>c.ProductTypes).Include(f=>f.SpecialTags).ToList();
            return View(products);
        }


        //GET Create Action Method
        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    var name = Path.Combine(_webHostEnvironment.WebRootPath + "Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}
