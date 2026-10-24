using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Workout;
using WorkoutAssistant.Web.Models.WorkoutPlan;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Services.Core.Services;

public class WorkoutPlanService(AppDbContext dbContext) : IWorkoutPlanService
{
    public async Task<IEnumerable<WorkoutPlanViewModel>> GetAllAsync()
    {
        return await dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .OrderBy(p => p.Title)
            .ThenBy(p => p.Id)
            .ToListAsync();
    }

    public async Task<WorkoutPlanDetailsViewModel?> GetDetailsAsync(int id)
    {
        return await dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanDetailsViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Workouts = p.Workouts
                    .OrderByDescending(w => w.OrderPosition)
                    .Select(w => new WorkoutListViewModel
                    {
                        Id = w.Id,
                        Name = w.Name
                    })
                    .ToList()
            })
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<WorkoutPlanReorderInputModel?> GetForReorderAsync(int id)
    {
        return await dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanReorderInputModel
            {
                Id = p.Id,
                Workouts = p.Workouts
                .OrderByDescending(w => w.OrderPosition)
                .Select(w => new WorkoutOrderInputModel
                {
                    Id = w.Id,
                    Name = w.Name,
                    OrderPosition = w.OrderPosition
                })
                .ToList()
            })
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> ReorderWorkoutsAsync(WorkoutPlanReorderInputModel inputWorkoutPlan)
    {
        if (inputWorkoutPlan.Workouts.Count == 0 ||
            inputWorkoutPlan.Workouts.Count > OrderPositionMax)
        {
            return false;
        }

        List<Workout> workouts = await dbContext.Workouts
            .Where(w => w.PlanId == inputWorkoutPlan.Id)
            .ToListAsync();

        if (workouts.Count != inputWorkoutPlan.Workouts.Count ||
            !workouts.Select(w => w.Id).ToHashSet()
                .SetEquals(inputWorkoutPlan.Workouts.Select(w => w.Id)) ||
            inputWorkoutPlan.Workouts.Select(w => w.OrderPosition).Distinct().Count() != workouts.Count ||
            inputWorkoutPlan.Workouts.Any(w =>
                w.OrderPosition < OrderPositionMin || w.OrderPosition > workouts.Count))
        {
            return false;
        }

        Dictionary<int, Workout> workoutsById = workouts.ToDictionary(w => w.Id);
        foreach (WorkoutOrderInputModel workoutOrder in inputWorkoutPlan.Workouts)
        {
            workoutsById[workoutOrder.Id].OrderPosition = workoutOrder.OrderPosition;
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task AddAsync(WorkoutPlanAddInputModel inputWorkoutPlan)
    {
        WorkoutPlan workoutPlan = new()
        {
            Title = inputWorkoutPlan.Title,
            Description = inputWorkoutPlan.Description
        };

        dbContext.WorkoutPlans.Add(workoutPlan);
        await dbContext.SaveChangesAsync();
    }

    public async Task<WorkoutPlanEditInputModel?> GetForEditAsync(int id)
    {
        return await dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanEditInputModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> UpdateAsync(WorkoutPlanEditInputModel inputWorkoutPlan)
    {
        WorkoutPlan? workoutPlan = await dbContext.WorkoutPlans.FindAsync(inputWorkoutPlan.Id);
        if (workoutPlan == null)
        {
            return false;
        }

        workoutPlan.Title = inputWorkoutPlan.Title;
        workoutPlan.Description = inputWorkoutPlan.Description;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<WorkoutPlanViewModel?> GetForDeleteAsync(int id)
    {
        return await dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        WorkoutPlan? workoutPlan = await dbContext.WorkoutPlans
            .SingleOrDefaultAsync(p => p.Id == id);
        if (workoutPlan == null)
        {
            return false;
        }

        dbContext.Remove(workoutPlan);
        await dbContext.SaveChangesAsync();
        return true;
    }
}
