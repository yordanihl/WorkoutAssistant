using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutPlansController(AppDbContext dbContext) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<WorkoutPlan> workoutPlans = dbContext.WorkoutPlans
                .OrderBy(p => p.Title)
                .ThenBy(p => p.Id)
                .ToList();

            return View(workoutPlans);
        }
    }
}
