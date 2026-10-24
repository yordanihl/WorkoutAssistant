using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Services.Core.Services;

public class ExerciseService(AppDbContext dbContext) : IExerciseService
{
    public async Task<ExerciseDetailsViewModel?> GetDetailsAsync(int id)
    {
        return await dbContext.Exercises
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
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<bool> WorkoutExistsAsync(int workoutId)
    {
        return dbContext.Workouts.AnyAsync(w => w.Id == workoutId);
    }

    public async Task AddAsync(ExerciseAddInputModel inputExercise)
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
        await dbContext.SaveChangesAsync();
    }

    public async Task<ExerciseEditInputModel?> GetForEditAsync(int id)
    {
        return await dbContext.Exercises
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
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> UpdateAsync(ExerciseEditInputModel inputExercise)
    {
        Exercise? exercise = await dbContext.Exercises.FindAsync(inputExercise.Id);
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

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int?> DeleteAsync(int id)
    {
        Exercise? exercise = await dbContext.Exercises.SingleOrDefaultAsync(e => e.Id == id);
        if (exercise == null)
        {
            return null;
        }

        int workoutId = exercise.WorkoutId;
        dbContext.Exercises.Remove(exercise);
        await dbContext.SaveChangesAsync();

        return workoutId;
    }
}
