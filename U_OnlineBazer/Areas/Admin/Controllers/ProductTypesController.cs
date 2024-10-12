using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Super user")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductTypesController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //return View(_dbContext.ProductTypes.ToList());
            List<ProductType> productTypes = _dbContext.ProductTypes.ToList();
            return View(productTypes);
        }

        //GET Create Action Method
        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductTypes.Add(productType);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }



        //GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }
            var productType = _dbContext.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(productType);
                await _dbContext.SaveChangesAsync();
                TempData["Edit"] = "Edit Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }



        //GET Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _dbContext.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //POST Details Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductType productType)
        {
            return RedirectToAction(nameof(Index));
        }

        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete( int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var productType = _dbContext.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Remove(productType);
                await _dbContext.SaveChangesAsync();
                TempData["delete"] = "Delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }
    }
}
