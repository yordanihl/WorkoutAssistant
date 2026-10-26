using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Web.Controllers;

public class ExercisesController(IExerciseService exerciseService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        ExerciseDetailsViewModel? exercise = await exerciseService.GetDetailsAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpGet]
    public async Task<IActionResult> Add(int workoutId)
    {
        if (!await exerciseService.WorkoutExistsAsync(workoutId))
        {
            return NotFound();
        }

        return View(new ExerciseAddInputModel { WorkoutId = workoutId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(ExerciseAddInputModel inputExercise)
    {
        if (!await exerciseService.WorkoutExistsAsync(inputExercise.WorkoutId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(inputExercise);
        }

        await exerciseService.AddAsync(inputExercise);

        TempData["ToastMessage"] = "Exercise added successfully.";
        TempData["ToastType"] = "success";
        return RedirectToAction(
            nameof(WorkoutsController.Details),
            nameof(WorkoutsController)[..^10],
            new { id = inputExercise.WorkoutId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        ExerciseEditInputModel? exercise = await exerciseService.GetForEditAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ExerciseEditInputModel inputExercise)
    {
        if (!ModelState.IsValid)
        {
            return View(inputExercise);
        }

        if (!await exerciseService.UpdateAsync(inputExercise))
        {
            return NotFound();
        }

        TempData["ToastMessage"] = "Exercise updated successfully.";
        TempData["ToastType"] = "success";
        return RedirectToAction(nameof(Details), new { id = inputExercise.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        ExerciseDetailsViewModel? exercise = await exerciseService.GetDetailsAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        int? workoutId = await exerciseService.DeleteAsync(id);
        if (workoutId == null)
        {
            return NotFound();
        }

        TempData["ToastMessage"] = "Exercise deleted successfully.";
        TempData["ToastType"] = "success";
        return RedirectToAction(
            nameof(WorkoutsController.Details),
            nameof(WorkoutsController)[..^10],
            new { id = workoutId });
    }
}
