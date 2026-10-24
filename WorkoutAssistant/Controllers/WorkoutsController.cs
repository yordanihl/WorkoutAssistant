using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutsController(IWorkoutService workoutService) : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            WorkoutDetailsViewModel? workout = workoutService.GetDetails(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpGet]
        public IActionResult Add(int planId)
        {
            WorkoutAddInputModel workout = new()
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

            if (!workoutService.Add(inputWorkout))
            {
                TempData["toast"] = "Something went wrong. Try again later.";
            }

            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = inputWorkout.PlanId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            WorkoutEditInputModel? workout = workoutService.GetForEdit(id);
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

            if (!workoutService.Update(inputWorkout))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = inputWorkout.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            WorkoutListViewModel? workout = workoutService.GetForDelete(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            int? planId = workoutService.Delete(id);
            if (planId == null)
            {
                return BadRequest();
            }

            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = planId });
        }
    }
}
