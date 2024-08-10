// to add the admin info

using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


public class SuperAdminController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public SuperAdminController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }
    [HttpGet]
     public IActionResult Register(){
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model){
        if (ModelState.IsValid){
            var user = new ApplicationUser(){
                FirstName = model.FirstName,    
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
            };
            var result = await _userManager.CreateAsync(user,model.Password);
            if (result.Succeeded){
                // add the current user to admin role
                var res = await _userManager.AddToRoleAsync(user,"Admin");
                if (res.Succeeded){
                    return RedirectToAction("Index","Home");
                }
                return View(model);
            }
        }
        return View(model);

    }
}