using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Web.InputModels;
using WorkoutAssistant.Web.ViewModels.Exercise;

namespace WorkoutAssistant.Web.Controllers;

public class ExercisesController(AppDbContext dbContext) : Controller
{
    [HttpGet]
    public IActionResult Details(int id)
    {
        ExerciseDetailsViewModel? exercise = dbContext.Exercises
            .Select(e => new ExerciseDetailsViewModel
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
                Sets = e.Sets,
                Reps = e.Reps,
                RestSeconds = e.RestSeconds,
                MediaUrl = e.MediaUrl,
                WorkoutId = e.WorkoutId,
                WorkoutName = e.Workout.Name
            })
            .SingleOrDefault(e => e.Id == id);

        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    [HttpGet]
    public IActionResult Add(int workoutId)
    {
        if (!dbContext.Workouts.Any(w => w.Id == workoutId))
        {
            return NotFound();
        }

        return View(new ExerciseAddInputModel { WorkoutId = workoutId });
    }

    [HttpPost]
    public IActionResult Add(ExerciseAddInputModel inputExercise)
    {
        if (!dbContext.Workouts.Any(w => w.Id == inputExercise.WorkoutId))
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(inputExercise);
        }

        Exercise newExercise = new()
        {
            Name = inputExercise.Name,
            MuscleGroup = inputExercise.MuscleGroup,
            Sets = inputExercise.Sets,
            Reps = inputExercise.Reps,
            RestSeconds = inputExercise.RestSeconds,
            MediaUrl = inputExercise.MediaUrl,
            WorkoutId = inputExercise.WorkoutId
        };

        dbContext.Exercises.Add(newExercise);
        dbContext.SaveChanges();

        return RedirectToAction(
            nameof(WorkoutsController.Details),
            nameof(WorkoutsController)[..^10],
            new { id = newExercise.WorkoutId });
    }
}
