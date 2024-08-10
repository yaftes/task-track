// role of the admin is to add users and skill sets
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller {

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public AdminController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }
    [Authorize(Roles = "Admin")]
    public IActionResult Register() {
        var skills = _dbContext.Skills.ToList();
        RegisterViewModel model = new RegisterViewModel(){
            Skills = skills
        };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register(RegisterViewModel model){
        if (ModelState.IsValid){
            var user = new ApplicationUser(){
                FirstName = model.FirstName,    
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Skills = _dbContext.Skills.ToList()

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

    [HttpGet]
    public IActionResult CreateRole(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateRole(CreateRole model){
        if (ModelState.IsValid){
            var role = new ApplicationRole(){
                Name = model.RoleName,
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded){
                return RedirectToAction("Index","Home");
            }

        }
        return View(model);

    }

}