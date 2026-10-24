using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;
using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Services.Core.Services;

public class WorkoutService(AppDbContext dbContext) : IWorkoutService
{
    public WorkoutDetailsViewModel? GetDetails(int id)
    {
        return dbContext.Workouts
            .Select(w => new WorkoutDetailsViewModel
            {
                Id = w.Id,
                Name = w.Name,
                PlanId = w.PlanId,
                Exercises = w.Exercises
                    .OrderBy(e => e.Name)
                    .Select(e => new ExerciseViewModel
                    {
                        Id = e.Id,
                        Name = e.Name
                    })
                    .ToList()
            })
            .SingleOrDefault(w => w.Id == id);
    }

    public bool Add(WorkoutAddInputModel inputWorkout)
    {
        if (!dbContext.WorkoutPlans.Any(p => p.Id == inputWorkout.PlanId))
        {
            return false;
        }

        Workout workout = new()
        {
            Name = inputWorkout.Name,
            PlanId = inputWorkout.PlanId
        };

        dbContext.Workouts.Add(workout);
        dbContext.SaveChanges();
        return true;
    }

    public WorkoutEditInputModel? GetForEdit(int id)
    {
        return dbContext.Workouts
            .Select(w => new WorkoutEditInputModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .SingleOrDefault(w => w.Id == id);
    }

    public bool Update(WorkoutEditInputModel inputWorkout)
    {
        Workout? workout = dbContext.Workouts.Find(inputWorkout.Id);
        if (workout == null)
        {
            return false;
        }

        workout.Name = inputWorkout.Name;
        dbContext.SaveChanges();
        return true;
    }

    public WorkoutListViewModel? GetForDelete(int id)
    {
        return dbContext.Workouts
            .Select(w => new WorkoutListViewModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .SingleOrDefault(w => w.Id == id);
    }

    public int? Delete(int id)
    {
        Workout? workout = dbContext.Workouts.SingleOrDefault(w => w.Id == id);
        if (workout == null)
        {
            return null;
        }

        int planId = workout.PlanId;
        dbContext.Remove(workout);
        dbContext.SaveChanges();

        return planId;
    }
}
