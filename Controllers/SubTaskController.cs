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
    public async Task<IActionResult> Create(SubTaskModel model,int Id,List<IFormFile> subtaskfiles){
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
            SubTaskStatus subTaskStatus = new SubTaskStatus(){
                SubTaskId = subTask.Id,
            };
            _dbContext.SubTaskStatus.Add(subTaskStatus);
            _dbContext.SaveChanges();

            // add the subtaskfiles here
            if(subtaskfiles != null){
                foreach(var stf in subtaskfiles){
                    SubTaskFile subtaskfile = new SubTaskFile(){
                        SubTaskId = subTask.Id,
                        FileName = stf.FileName,
                        ContentType = stf.ContentType,
                    };
                    using(var inputstream = new MemoryStream()){
                        await stf.CopyToAsync(inputstream);
                    subtaskfile.Data = inputstream.ToArray();
                    }
                    _dbContext.SubTaskFile.Add(subtaskfile);
                    _dbContext.SaveChanges();   
                    

                }
            }
            var subtasks = _dbContext.SubTask.Where(sb => sb.TaskId == Id).ToList();
            double totalpoint = 0.0;
            foreach(var st in subtasks){
                var stw = _dbContext.SubTaskWeight.FirstOrDefault(sbw => sbw.SubTaskId == st.Id);
                totalpoint += stw.Point;   
            } 

        foreach(var st in subtasks){
            var stw = _dbContext.SubTaskWeight.FirstOrDefault(sbw => sbw.SubTaskId == st.Id);
            stw.Weight = stw.Point / totalpoint * 100;
            _dbContext.SubTaskWeight.Update(stw);
        }
         _dbContext.SaveChanges();
         
        var task = _dbContext.Task.FirstOrDefault(t => t.Id == Id);

        double totalprogress = 0.0;

        foreach(var st in subtasks){
            var sts = _dbContext.SubTaskStatus.FirstOrDefault(sts => sts.SubTaskId == st.Id);
            var stw = _dbContext.SubTaskWeight.FirstOrDefault(sbw => sbw.SubTaskId == st.Id);
            if(sts != null){
                if(sts.Status == "Completed"){
                totalprogress += stw.Weight;
            }
            } 
        }
            task.Progress = totalprogress;
            _dbContext.Task.Update(task);
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
    public IActionResult StatusUpdate(int Id){
        var subtask = _dbContext.SubTask.FirstOrDefault(s => s.Id == Id);
        var subtaskweight = _dbContext.SubTaskWeight.FirstOrDefault(sw => sw.SubTaskId == subtask.Id);
        var subtaskstatus = _dbContext.SubTaskStatus.FirstOrDefault(s => s.SubTaskId == subtask.Id);
        subtaskstatus.Status = "Completed";
        _dbContext.SubTaskStatus.Update(subtaskstatus);
        _dbContext.SaveChanges();

        var task = _dbContext.Task.FirstOrDefault(t => t.Id == subtask.TaskId);
        task.Progress += subtaskweight.Weight;
        _dbContext.Task.Update(task);
        _dbContext.SaveChanges();

        return RedirectToAction("TaskDetail","Task",new {
            Id = task.Id
        });
    }
  
    
}