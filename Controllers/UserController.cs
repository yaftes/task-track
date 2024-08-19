
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public  class UserController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public UserController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }

    [HttpGet]
    public async Task<IActionResult> ChangePassword(){
        return View(); 
    }

    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
{
    if (ModelState.IsValid)
    {
        var curruser = await _userManager.GetUserAsync(User);
        if (curruser == null)
        {
            return View();
        }

        // Use await to handle the asynchronous operation
        var result = await _userManager.ChangePasswordAsync(curruser, model.OldPassword, model.Password);

        if (result.Succeeded) // Check for success
        {
            return RedirectToAction("Login", "Login");
        }
        else
        {
            // Handle errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
    return View(model);
}
    
}