using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbcontext;
    public LoginController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        SignInManager<ApplicationUser> _signInManager,
        ApplicationDbContext _dbcontext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbcontext = _dbcontext;
            this._signInManager = _signInManager;
        }
    [HttpGet]
    public IActionResult Login(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model){
        if (ModelState.IsValid){
            var result = await _signInManager.PasswordSignInAsync(model.UserName,model.Password,isPersistent:false,lockoutOnFailure:false);
            if(result.Succeeded){
             var user = await _userManager.GetUserAsync(User);
              if(await _userManager.IsInRoleAsync(user,"Admin")){
                return RedirectToAction("Register","Admin");
              }
              else if(await _userManager.IsInRoleAsync(user,"Super")){
                return RedirectToAction("Register","SuperAdmin");
              }   
              else{
                return RedirectToAction("Index","Home");
              } 
            }
            else{
            ModelState.AddModelError(string.Empty,"Invalid Login Attempt");
            }

        }
        return View(model);

    }

}