using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            List<Product> products = _dbContext.Products.Include(c => c.ProductTypes).Include(f => f.SpecialTags).ToList();
            return View(products);
        }


        //POST Index Action Method
        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
        {
            var products = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags).Where(c =>
            c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if(lowAmount==null || largeAmount == null)
            {
                products = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags).ToList();
            }
            return View(products);
        }


        //GET Create Action Method
       
        public ActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductTypes");
            ViewData["TagId"] = new SelectList(_dbContext.SpecialTags.ToList(), "Id", "Name");
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _dbContext.Products.FirstOrDefault(c=>c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This Product is already exist!";
                    ViewData["ProductId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductTypes");
                    ViewData["TagId"] = new SelectList(_dbContext.SpecialTags.ToList(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    String name = Path.Combine(_webHostEnvironment.WebRootPath + "/Images",
                        Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    product.Image = "Images/hp2.jpeg";
                }
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Edit Action Method

       
        public ActionResult Edit(int? id)
        {
            ViewData["ProductId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductTypes");
            ViewData["TagId"] = new SelectList(_dbContext.SpecialTags.ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).Include
                (c => c.SpecialTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        //POST Create Action Method

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _dbContext.Products.FirstOrDefault(c => c.Name == product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This Product is already exist!";
                    ViewData["ProductId"] = new SelectList(_dbContext.ProductTypes.ToList(), "Id", "ProductTypes");
                    ViewData["TagId"] = new SelectList(_dbContext.SpecialTags.ToList(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_webHostEnvironment.WebRootPath + "Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }
                if (image == null)
                {
                    product.Image = "Images/hp2.jpeg";
                }
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //GET Details Action Method

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var product = _dbContext.Products.Include(c=>c.ProductTypes).Include
                (c=>c.SpecialTags).FirstOrDefault(c=>c.Id==id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }



        //POST Delete Action Method
        
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _dbContext.Products.Include(c => c.ProductTypes).Include(c => c.SpecialTags).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Remove(product);
                await _dbContext.SaveChangesAsync();
                TempData["del"] = "Delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

    }
}
