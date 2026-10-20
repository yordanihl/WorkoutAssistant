using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Web.InputModels;
using WorkoutAssistant.Web.ViewModels;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutPlansController(AppDbContext dbContext) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<WorkoutPlanViewModel> workoutPlans = dbContext.WorkoutPlans
                .Select(p => new WorkoutPlanViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description
                })
                .OrderBy(p => p.Title)
                .ThenBy(p => p.Id)
                .ToList();

            return View(workoutPlans);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            WorkoutPlanViewModel? workoutPlan = dbContext.WorkoutPlans
                .Select(p => new WorkoutPlanViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description
                })
                .SingleOrDefault(p => p.Id == id);

            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpGet]
        public IActionResult Add()
        {
            WorkoutPlanAddInputModel workoutPlan = new WorkoutPlanAddInputModel();

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult Add(WorkoutPlanAddInputModel inputWorkoutPlan)
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            WorkoutPlanEditInputModel? workoutPlan = dbContext.WorkoutPlans
                .Select(p => new WorkoutPlanEditInputModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description= p.Description
                })
                .SingleOrDefault(p => p.Id == id);

            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult Edit(WorkoutPlanEditInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }
            WorkoutPlan? workoutPlan = dbContext.WorkoutPlans.Find(inputWorkoutPlan.Id);
            if (workoutPlan == null)
            {
                return BadRequest();
            }
            workoutPlan.Title = inputWorkoutPlan.Title;
            workoutPlan.Description = inputWorkoutPlan.Description;

            dbContext.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = workoutPlan.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            WorkoutPlanViewModel? workoutPlan = dbContext.WorkoutPlans
                .Select(p => new WorkoutPlanViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description
                })
                .SingleOrDefault(p => p.Id == id);

            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            WorkoutPlan? workoutPlan = dbContext.WorkoutPlans
                .SingleOrDefault(p => p.Id == id);

            if (workoutPlan == null)
            {
                return BadRequest();
            }

            dbContext.Remove(workoutPlan);
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
