using Microsoft.AspNetCore.Mvc;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecialTagController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //return View(_dbContext.specialTags.ToList());
            List<SpecialTag> SpecialTag = _dbContext.SpecialTags.ToList();
            return View(SpecialTag);
        }

        //GET Create Action Method
        public ActionResult Create()
        {
            return View();
        }

        //POST Create Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag SpecialTag)
        {
            if (ModelState.IsValid)
            {
                _dbContext.SpecialTags.Add(SpecialTag);
                await _dbContext.SaveChangesAsync();
                TempData["save"] = "Save Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTag);
        }



        //GET Edit Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SpecialTag = _dbContext.SpecialTags.Find(id);
            if (SpecialTag == null)
            {
                return NotFound();
            }
            return View(SpecialTag);
        }

        //POST Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag SpecialTag)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(SpecialTag);
                await _dbContext.SaveChangesAsync();
                TempData["edit"] = "Edit Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(SpecialTag);
        }



        //GET Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SpecialTag = _dbContext.SpecialTags.Find(id);
            if (SpecialTag == null)
            {
                return NotFound();
            }
            return View(SpecialTag);
        }

        //POST Details Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag SpecialTag)
        {
            return RedirectToAction(nameof(Index));
        }


        //POST Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = _dbContext.SpecialTags.Find(id);
            if (specialTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbContext.Remove(specialTag);
                await _dbContext.SaveChangesAsync();
                TempData["delete"] = "Delete Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }
    }
}

