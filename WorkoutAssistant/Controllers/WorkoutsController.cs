using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutsController(IWorkoutService workoutService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            WorkoutDetailsViewModel? workout = await workoutService.GetDetailsAsync(id);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(WorkoutAddInputModel inputWorkout)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkout);
            }

            if (!await workoutService.AddAsync(inputWorkout))
            {
                TempData["ToastMessage"] = "Something went wrong. Try again later.";
                TempData["ToastType"] = "error";
            }
            else
            {
                TempData["ToastMessage"] = "Workout added successfully.";
                TempData["ToastType"] = "success";
            }

            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = inputWorkout.PlanId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            WorkoutEditInputModel? workout = await workoutService.GetForEditAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkoutEditInputModel inputWorkout)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkout);
            }

            if (!await workoutService.UpdateAsync(inputWorkout))
            {
                return BadRequest();
            }

            TempData["ToastMessage"] = "Workout updated successfully.";
            TempData["ToastType"] = "success";
            return RedirectToAction(nameof(Details), new { id = inputWorkout.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            WorkoutListViewModel? workout = await workoutService.GetForDeleteAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int? planId = await workoutService.DeleteAsync(id);
            if (planId == null)
            {
                return BadRequest();
            }

            TempData["ToastMessage"] = "Workout deleted successfully.";
            TempData["ToastType"] = "success";
            return RedirectToAction(
                nameof(WorkoutPlansController.Details),
                nameof(WorkoutPlansController)[..^10],
                new { id = planId });
        }
    }
}
