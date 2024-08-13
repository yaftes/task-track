// role of the admin is to add users and skill sets
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    
   [HttpGet]
public IActionResult Register()
{
    RegisterViewModel model = new RegisterViewModel(){
        Skills = _dbContext.Skill.ToList()
    };  

    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Register(RegisterViewModel model,string select)
{
    if (ModelState.IsValid)
    {
        ApplicationUser user = new ApplicationUser()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.UserName,
            Skills = _dbContext.Skill.ToList(),
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            var res = await _userManager.AddToRoleAsync(user, select);
            if (res.Succeeded)
            {
                return RedirectToAction("Login", "Login");
            }
        }
        ModelState.AddModelError(string.Empty, "Failed to register user.");
    }
    return View(model);
}
    [HttpGet]
    public IActionResult AddSkill(){
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddSkill(CreateSkill model){
        var check = _dbContext.Skill.FirstOrDefault(x => x.SkillName == model.skillName);
        if (ModelState.IsValid){
            if(check != null){
            
            return View(model);
            }
           Skill skill  = new Skill(){
            SkillName = model.skillName,
           };
            _dbContext.Skill.Add(skill);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");
        
        }
        return View(model);

    }

}