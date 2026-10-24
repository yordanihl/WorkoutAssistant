using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Services.Core.Services;

public class ExerciseService(AppDbContext dbContext) : IExerciseService
{
    public ExerciseDetailsViewModel? GetDetails(int id)
    {
        return dbContext.Exercises
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
    }

    public bool WorkoutExists(int workoutId)
    {
        return dbContext.Workouts.Any(w => w.Id == workoutId);
    }

    public void Add(ExerciseAddInputModel inputExercise)
    {
        Exercise exercise = new()
        {
            Name = inputExercise.Name,
            MuscleGroup = inputExercise.MuscleGroup,
            Sets = inputExercise.Sets,
            Reps = inputExercise.Reps,
            RestSeconds = inputExercise.RestSeconds,
            MediaUrl = inputExercise.MediaUrl,
            WorkoutId = inputExercise.WorkoutId
        };

        dbContext.Exercises.Add(exercise);
        dbContext.SaveChanges();
    }

    public ExerciseEditInputModel? GetForEdit(int id)
    {
        return dbContext.Exercises
            .Select(e => new ExerciseEditInputModel
            {
                Id = e.Id,
                Name = e.Name,
                MuscleGroup = e.MuscleGroup,
                Sets = e.Sets,
                Reps = e.Reps,
                RestSeconds = e.RestSeconds,
                MediaUrl = e.MediaUrl,
                WorkoutId = e.WorkoutId
            })
            .SingleOrDefault(e => e.Id == id);
    }

    public bool Update(ExerciseEditInputModel inputExercise)
    {
        Exercise? exercise = dbContext.Exercises.Find(inputExercise.Id);
        if (exercise == null)
        {
            return false;
        }

        exercise.Name = inputExercise.Name;
        exercise.MuscleGroup = inputExercise.MuscleGroup;
        exercise.Sets = inputExercise.Sets;
        exercise.Reps = inputExercise.Reps;
        exercise.RestSeconds = inputExercise.RestSeconds;
        exercise.MediaUrl = inputExercise.MediaUrl;

        dbContext.SaveChanges();
        return true;
    }

    public int? Delete(int id)
    {
        Exercise? exercise = dbContext.Exercises.SingleOrDefault(e => e.Id == id);
        if (exercise == null)
        {
            return null;
        }

        int workoutId = exercise.WorkoutId;
        dbContext.Exercises.Remove(exercise);
        dbContext.SaveChanges();

        return workoutId;
    }
}
