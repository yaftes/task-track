
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
        var result1 = await _userManager.ChangePasswordAsync(curruser, model.OldPassword, model.Password);

        
        curruser.ProfilePicture = model.ProfilePicture;
        // updating user Profile
        var result2 = await _userManager.UpdateAsync(curruser);


        

        if (result1.Succeeded && result2.Succeeded) // Check for success
        {
            return RedirectToAction("Login", "Login");
        }
        else
        {
            // Handle errors
            foreach (var error in result1.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
    return View(model);
}
    
}