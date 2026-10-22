using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Web.InputModels;
using WorkoutAssistant.Web.ViewModels.Workout;
using WorkoutAssistant.Web.ViewModels.Exercise;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutsController(AppDbContext dbContext) : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            WorkoutDetailsViewModel? workout = dbContext.Workouts
                .Select(w => new WorkoutDetailsViewModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    PlanId = w.PlanId,
                    Exercises = w.Exercises
                    .OrderBy(e  => e.Name)
                    .Select(e => new ExerciseViewModel
                    {
                        Name = e.Name
                    })
                    .ToList()
                })
                .SingleOrDefault(w => w.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            WorkoutEditInputModel? workout = dbContext.Workouts
                .Select(w => new WorkoutEditInputModel
                {
                    Id = w.Id,
                    Name = w.Name
                })
                .SingleOrDefault(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        public IActionResult Edit(WorkoutEditInputModel inputWorkout)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkout);
            }

            Workout? workout = dbContext.Workouts.Find(inputWorkout.Id);
            if (workout == null)
            {
                return BadRequest();
            }

            workout.Name = inputWorkout.Name;
            dbContext.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = workout.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            WorkoutListViewModel? workout = dbContext.Workouts
                .Select(w => new WorkoutListViewModel
                {
                    Id = w.Id,
                    Name = w.Name
                })
                .SingleOrDefault(w => w.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            Workout? workout = dbContext.Workouts
                .SingleOrDefault(w => w.Id == id);

            if (workout == null)
            {
                return BadRequest();
            }

            int planId = workout.PlanId;
            dbContext.Remove(workout);
            dbContext.SaveChanges();

            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = planId });
        }
    }
}
