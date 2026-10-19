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

        [HttpGet]
        public IActionResult Details(int id)
        {
            WorkoutPlan? workoutPlan = dbContext.WorkoutPlans
                .SingleOrDefault(p => p.Id == id);

            if (workoutPlan == null)
            {
                return BadRequest();
            }

            return View(workoutPlan);
        }

        [HttpGet]
        public IActionResult Add()
        {
            WorkoutPlan workoutPlan = new WorkoutPlan();

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult Add(WorkoutPlan inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }
            try
            {
                WorkoutPlan newWorkoutPlan = new WorkoutPlan()
                {
                    Title = inputWorkoutPlan.Title,
                    Description = inputWorkoutPlan.Description
                };

                dbContext.WorkoutPlans.Add(newWorkoutPlan);
                dbContext.SaveChanges();
            }
            catch
            {
                TempData["toast"] = "Something went wrong. Try again later.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
