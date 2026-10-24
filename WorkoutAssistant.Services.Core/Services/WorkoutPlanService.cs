using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Workout;
using WorkoutAssistant.Web.Models.WorkoutPlan;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Services.Core.Services;

public class WorkoutPlanService(AppDbContext dbContext) : IWorkoutPlanService
{
    public IEnumerable<WorkoutPlanViewModel> GetAll()
    {
        return dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .OrderBy(p => p.Title)
            .ThenBy(p => p.Id)
            .ToList();
    }

    public WorkoutPlanDetailsViewModel? GetDetails(int id)
    {
        return dbContext.WorkoutPlans
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
            .SingleOrDefault(p => p.Id == id);
    }

    public WorkoutPlanReorderInputModel? GetForReorder(int id)
    {
        return dbContext.WorkoutPlans
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
            .SingleOrDefault(p => p.Id == id);
    }

    public bool ReorderWorkouts(WorkoutPlanReorderInputModel inputWorkoutPlan)
    {
        if (inputWorkoutPlan.Workouts.Count == 0 ||
            inputWorkoutPlan.Workouts.Count > OrderPositionMax)
        {
            return false;
        }

        List<Workout> workouts = dbContext.Workouts
            .Where(w => w.PlanId == inputWorkoutPlan.Id)
            .ToList();

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

        dbContext.SaveChanges();
        return true;
    }

    public void Add(WorkoutPlanAddInputModel inputWorkoutPlan)
    {
        WorkoutPlan workoutPlan = new()
        {
            Title = inputWorkoutPlan.Title,
            Description = inputWorkoutPlan.Description
        };

        dbContext.WorkoutPlans.Add(workoutPlan);
        dbContext.SaveChanges();
    }

    public WorkoutPlanEditInputModel? GetForEdit(int id)
    {
        return dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanEditInputModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .SingleOrDefault(p => p.Id == id);
    }

    public bool Update(WorkoutPlanEditInputModel inputWorkoutPlan)
    {
        WorkoutPlan? workoutPlan = dbContext.WorkoutPlans.Find(inputWorkoutPlan.Id);
        if (workoutPlan == null)
        {
            return false;
        }

        workoutPlan.Title = inputWorkoutPlan.Title;
        workoutPlan.Description = inputWorkoutPlan.Description;
        dbContext.SaveChanges();
        return true;
    }

    public WorkoutPlanViewModel? GetForDelete(int id)
    {
        return dbContext.WorkoutPlans
            .Select(p => new WorkoutPlanViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description
            })
            .SingleOrDefault(p => p.Id == id);
    }

    public bool Delete(int id)
    {
        WorkoutPlan? workoutPlan = dbContext.WorkoutPlans
            .SingleOrDefault(p => p.Id == id);
        if (workoutPlan == null)
        {
            return false;
        }

        dbContext.Remove(workoutPlan);
        dbContext.SaveChanges();
        return true;
    }
}
