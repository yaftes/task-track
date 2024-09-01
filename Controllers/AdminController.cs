// role of the admin is to add users and skill sets
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AdminController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AdminController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        SignInManager<ApplicationUser> _signInManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
            this._signInManager = _signInManager;
        }
            [HttpGet]
            public IActionResult Register(){
                var users = _userManager.Users.ToList();
                var model = new RegisterViewModel(){
                    ListofSkill = _dbContext.Skill.ToList(),
                    ApplicationUsers = users,
                };
                
                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register(RegisterViewModel model,string select,List<string> selectedskills)
            {
                if (ModelState.IsValid)
                {

                    ApplicationUser user = new ApplicationUser(){
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.UserName,
                    };
                    
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        foreach(var skill in selectedskills){
                            UserSkill us = new UserSkill(){
                                UserId = user.Id,
                                SkillId = Convert.ToInt16(skill)
                            };
                            _dbContext.UserSkill.Add(us);
                        }
                        _dbContext.SaveChanges();

                        await _userManager.AddToRoleAsync(user,select);

                        return RedirectToAction("Login", "Login");

                    }
                    ModelState.AddModelError(string.Empty, "Failed to register user.");
                }
                return View(model);
            }

        public  IActionResult AddSkill(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSkill(CreateSkill model){
            if (ModelState.IsValid){
                 bool check = _dbContext.Skill.Any(s => s.SkillName == model.skillName); 
                if(!check){
                    Skill skill = new Skill(){
                        SkillName = model.skillName,
                    };
                     _dbContext.Skill.Add(skill);
                    await _dbContext.SaveChangesAsync();
         
                    return View();
                }
                else {
                    ModelState.AddModelError(string.Empty,"Already Exists");
                    return View();
                }

            }
            return View();
        }
        
}
        

