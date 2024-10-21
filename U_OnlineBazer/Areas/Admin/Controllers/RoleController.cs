using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using U_OnlineBazer.Areas.Admin.Models;
using U_OnlineBazer.Data;

namespace U_OnlineBazer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _dbContext;
        public RoleController(RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        //GET Create action method

        public async Task<IActionResult> Create()
        {
            return View();
        }

        //POST Create action method
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole();
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.message = "This role is already exists!!";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been save successfully";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        //GET Edit action method

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        //POST Edit action method

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _roleManager.RoleExistsAsync(role.Name);
            if (isExist)
            {
                ViewBag.mgs = "This role is aldeady exist";
                ViewBag.name = name;
                return View();
            }
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                TempData["save"] = "Role has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        //POST Delete action method

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["delete"] = "Role has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //GET Assign action method

        public async Task<IActionResult> Assign()
        {
            ViewData["UserId"] = new SelectList(_dbContext.ApplicationUsers.ToList(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }


        //POST assign action method
        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVm roleUser)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(c=>c.Id==roleUser.UserId);
           
            var role = await _userManager.AddToRoleAsync(user, roleUser.RoleId);
            if (role.Succeeded)
            {
                TempData["save"] = "User Role Assign Done";
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }


    }
}

