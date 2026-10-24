using WorkoutAssistant.Web.Models.WorkoutPlan;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IWorkoutPlanService
{
    Task<IEnumerable<WorkoutPlanViewModel>> GetAllAsync();

    Task<WorkoutPlanDetailsViewModel?> GetDetailsAsync(int id);

    Task<WorkoutPlanReorderInputModel?> GetForReorderAsync(int id);

    Task<bool> ReorderWorkoutsAsync(WorkoutPlanReorderInputModel inputWorkoutPlan);

    Task AddAsync(WorkoutPlanAddInputModel inputWorkoutPlan);

    Task<WorkoutPlanEditInputModel?> GetForEditAsync(int id);

    Task<bool> UpdateAsync(WorkoutPlanEditInputModel inputWorkoutPlan);

    Task<WorkoutPlanViewModel?> GetForDeleteAsync(int id);

    Task<bool> DeleteAsync(int id);
}
