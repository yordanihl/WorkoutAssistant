using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Web.InputModels;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutsController(AppDbContext dbContext) : Controller
    {
        [HttpGet]
        public IActionResult Add(int planId)
        {
            WorkoutAddInputModel workout = new WorkoutAddInputModel()
            {
                PlanId = planId
            };

            return View(workout);
        }

        [HttpPost]
        public IActionResult Add(WorkoutAddInputModel inputWorkout)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkout);
            }
            try
            {
                Workout newWorkout = new Workout()
                {
                    Name = inputWorkout.Name,
                    PlanId = inputWorkout.PlanId
                };

                dbContext.Workouts.Add(newWorkout);
                dbContext.SaveChanges();
            }
            catch
            {
                TempData["toast"] = "Something went wrong. Try again later.";
                return RedirectToAction(
                    nameof(WorkoutPlansController.Details),
                    nameof(WorkoutPlansController)[..^10],
                    new { id = inputWorkout.PlanId });
            }
            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = inputWorkout.PlanId });
        }
    }
}
