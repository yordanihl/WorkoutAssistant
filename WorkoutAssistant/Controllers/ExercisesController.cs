using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Web.Controllers;

public class ExercisesController(IExerciseService exerciseService) : Controller
{
    [HttpGet]
    public IActionResult Details(int id)
    {
        ExerciseDetailsViewModel? exercise = exerciseService.GetDetails(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpGet]
    public IActionResult Add(int workoutId)
    {
        if (!exerciseService.WorkoutExists(workoutId))
        {
            return NotFound();
        }

        return View(new ExerciseAddInputModel { WorkoutId = workoutId });
    }

    [HttpPost]
    public IActionResult Add(ExerciseAddInputModel inputExercise)
    {
        if (!exerciseService.WorkoutExists(inputExercise.WorkoutId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(inputExercise);
        }

        exerciseService.Add(inputExercise);

        return RedirectToAction(
            nameof(WorkoutsController.Details),
            nameof(WorkoutsController)[..^10],
            new { id = inputExercise.WorkoutId });
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        ExerciseEditInputModel? exercise = exerciseService.GetForEdit(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost]
    public IActionResult Edit(ExerciseEditInputModel inputExercise)
    {
        if (!ModelState.IsValid)
        {
            return View(inputExercise);
        }

        if (!exerciseService.Update(inputExercise))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Details), new { id = inputExercise.Id });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        ExerciseDetailsViewModel? exercise = exerciseService.GetDetails(id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        int? workoutId = exerciseService.Delete(id);
        if (workoutId == null)
        {
            return NotFound();
        }

        return RedirectToAction(
            nameof(WorkoutsController.Details),
            nameof(WorkoutsController)[..^10],
            new { id = workoutId });
    }
}
