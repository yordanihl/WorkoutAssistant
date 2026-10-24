using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IWorkoutService
{
    Task<WorkoutDetailsViewModel?> GetDetailsAsync(int id);

    Task<bool> AddAsync(WorkoutAddInputModel inputWorkout);

    Task<WorkoutEditInputModel?> GetForEditAsync(int id);

    Task<bool> UpdateAsync(WorkoutEditInputModel inputWorkout);

    Task<WorkoutListViewModel?> GetForDeleteAsync(int id);

    Task<int?> DeleteAsync(int id);
}
