﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using U_OnlineBazer.Data;
using U_OnlineBazer.Models;

namespace U_OnlineBazer.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {

        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _dbContext;

        public UserController(UserManager<IdentityUser> userManager,ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.ApplicationUsers.ToList());
        }


        //GET Create action mehtod
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action mehtod

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    TempData["save"] = "User has been created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            
            return View();
        }

        //GET Edit action mehtod
        public IActionResult Edit(string id)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //POST EDit action mehtod

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userInfo = _dbContext.ApplicationUsers.FirstOrDefault(c=>c.Id== user.Id);
            if(userInfo == null)  
            {
                return NotFound();   
            }
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            var result = await _userManager.UpdateAsync(userInfo);
            if (result.Succeeded)
            {
                TempData["edit"] = "User has been updated Successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(userInfo);
        }

        //GET Details action mehtod

        public IActionResult Details(string id)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //GET Lockout action mehtod

        public IActionResult Locout(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //POST Lockout action mehtod

        [HttpPost]
        public IActionResult Locout(ApplicationUser user)
        {
            var userInfo = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();

            }
            userInfo.LockoutEnd = DateTime.Now.AddYears(100);
            int rowAffected = _dbContext.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been lockout successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }


        //GET Active action mehtod
        public IActionResult Active(string id)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //POST Active action mehtod

        [HttpPost]
        public IActionResult Active(ApplicationUser user)
        {
            var userInfo = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();

            }
            userInfo.LockoutEnd = DateTime.Now.AddDays(-1);
            int rowAffected = _dbContext.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been active successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }


        //POST delete action method

        [HttpPost]
        public IActionResult Delete(ApplicationUser user)
        {
            var userInfo = _dbContext.ApplicationUsers.FirstOrDefault(c => c.Id == user.Id);
            if (userInfo == null)
            {
                return NotFound();

            }
            _dbContext.ApplicationUsers.Remove(userInfo);
            int rowAffected = _dbContext.SaveChanges();
            if (rowAffected > 0)
            {
                TempData["save"] = "User has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(userInfo);
        }
    }
}
