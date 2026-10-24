using WorkoutAssistant.Data;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.Workout;
using WorkoutAssistant.Web.Models.WorkoutPlan;

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
                    .OrderBy(w => w.OrderPosition)
                    .Select(w => new WorkoutListViewModel
                    {
                        Id = w.Id,
                        Name = w.Name
                    })
                    .ToList()
            })
            .SingleOrDefault(p => p.Id == id);
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
