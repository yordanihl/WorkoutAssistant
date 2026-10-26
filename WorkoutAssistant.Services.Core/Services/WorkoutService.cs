using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Exercise;
using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Services.Core.Services;

public class WorkoutService(AppDbContext dbContext) : IWorkoutService
{
    public async Task<WorkoutDetailsViewModel?> GetDetailsAsync(int id)
    {
        return await dbContext.Workouts
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
            .SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> AddAsync(WorkoutAddInputModel inputWorkout)
    {
        if (!await dbContext.WorkoutPlans.AnyAsync(p => p.Id == inputWorkout.PlanId))
        {
            return false;
        }

        int orderPosition = (await dbContext.Workouts
            .Where(w => w.PlanId == inputWorkout.PlanId)
            .Select(w => (int?)w.OrderPosition)
            .MaxAsync()) ?? 0;

        Workout workout = new()
        {
            Name = inputWorkout.Name,
            PlanId = inputWorkout.PlanId,
            OrderPosition = orderPosition + 1
        };

        dbContext.Workouts.Add(workout);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<WorkoutEditInputModel?> GetForEditAsync(int id)
    {
        return await dbContext.Workouts
            .Select(w => new WorkoutEditInputModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> UpdateAsync(WorkoutEditInputModel inputWorkout)
    {
        Workout? workout = await dbContext.Workouts.FindAsync(inputWorkout.Id);
        if (workout == null)
        {
            return false;
        }

        workout.Name = inputWorkout.Name;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<WorkoutListViewModel?> GetForDeleteAsync(int id)
    {
        return await dbContext.Workouts
            .Select(w => new WorkoutListViewModel
            {
                Id = w.Id,
                Name = w.Name
            })
            .SingleOrDefaultAsync(w => w.Id == id);
    }

    public async Task<int?> DeleteAsync(int id)
    {
        Workout? workout = await dbContext.Workouts.SingleOrDefaultAsync(w => w.Id == id);
        if (workout == null)
        {
            return null;
        }

        int planId = workout.PlanId;
        dbContext.Remove(workout);
        await dbContext.SaveChangesAsync();

        List<Workout> remainingWorkouts = await dbContext.Workouts
            .Where(w => w.PlanId == planId)
            .OrderBy(w => w.OrderPosition)
            .ToListAsync();

        for (int i = 0; i < remainingWorkouts.Count; i++)
        {
            remainingWorkouts[i].OrderPosition = i + 1;
        }

        await dbContext.SaveChangesAsync();

        return planId;
    }
}
