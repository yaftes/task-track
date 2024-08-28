using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class SubTaskController : Controller {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _dbContext;
    public SubTaskController(
        UserManager<ApplicationUser> _userManager,
        RoleManager<ApplicationRole> _roleManager,
        ApplicationDbContext _dbContext){
            this._userManager = _userManager;
            this._roleManager = _roleManager;
            this._dbContext = _dbContext;
        }
    public IActionResult Create(){
        return View();
    }

    [HttpPost]
    public IActionResult Create(SubTaskModel model,int Id){
        if (ModelState.IsValid){
            SubTask subTask = new SubTask(){
                Title = model.Title,
                Description = model.Description,
                Start_Date = model.Start_Date,
                End_Date = model.End_Date,
                Created_At = DateTime.Now,
                Update_Date = DateTime.Now,
                TaskId = Id,
            };

            _dbContext.SubTask.Add(subTask);
            _dbContext.SaveChanges();
            
            SubTaskWeight sb = new SubTaskWeight(){
                SubTaskId = subTask.Id,
                Point = model.Point,
            };
            _dbContext.SubTaskWeight.Add(sb);
            _dbContext.SaveChanges();
        // Now the proccess of updating the SubTaskWeights based on the 
        // Now we calculate all total point and calculate new weight for each subtask
        var subtasks = _dbContext.SubTask.Where(sb => sb.TaskId == Id).ToList();
        double totalpoint = 0.0;
        foreach(var st in subtasks){
            var stw = _dbContext.SubTaskWeight.FirstOrDefault(sbw => sbw.SubTaskId == st.Id);
            totalpoint += stw.Point;   
        } 
        // calculate new weight for each subtask weight
        foreach(var st in subtasks){
            var stw = _dbContext.SubTaskWeight.FirstOrDefault(sbw => sbw.SubTaskId == st.Id);
            stw.Weight = stw.Point / totalpoint * 100;
            _dbContext.SubTaskWeight.Update(stw);
        }
            _dbContext.SaveChanges();
            
            return RedirectToAction("TaskDetail","Task",new{
                Id = subTask.TaskId,
            });
        }
        return View(model);
    }


    [HttpGet]
    public IActionResult Detail(int Id){
        var subtask = _dbContext.SubTask.FirstOrDefault(s=>s.Id == Id);
        var subtaskweight = _dbContext.SubTaskWeight.FirstOrDefault(stw => stw.SubTaskId == subtask.Id);
        SubTaskDetail sbd = new SubTaskDetail(){
            SubTask = subtask,
            SubTaskWeight = subtaskweight,  
        };
        return View(sbd);
    }
  
    
}