using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.IO;

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
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model,IFormFile Image)
    {
        if (ModelState.IsValid)
        {
            var curruser = await _userManager.GetUserAsync(User);
            if (curruser == null)
            {
                return NotFound(); 
            }

            await _userManager.ChangePasswordAsync(curruser,model.OldPassword,model.Password);
 
            if (Image != null && Image.Length > 0) {
                
                // change the file to binary format
                using (var datastream = new MemoryStream())
                {
                    await Image.CopyToAsync(datastream);
                    curruser.ProfilePicture = datastream.ToArray();
                }
                await _userManager.UpdateAsync(curruser);
                return RedirectToAction("Login", "Login"); 
            }

           return View();
        }
        
        return View(); 
    }

    [HttpGet]
    public async Task<IActionResult> GetProfilePicture(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user?.ProfilePicture != null)
        {
            return File(user.ProfilePicture, "image/png"); 
        }
        
        return NotFound();
    }
}
