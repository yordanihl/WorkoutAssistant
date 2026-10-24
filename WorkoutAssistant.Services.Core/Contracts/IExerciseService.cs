using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IExerciseService
{
    Task<ExerciseDetailsViewModel?> GetDetailsAsync(int id);

    Task<bool> WorkoutExistsAsync(int workoutId);

    Task AddAsync(ExerciseAddInputModel inputExercise);

    Task<ExerciseEditInputModel?> GetForEditAsync(int id);

    Task<bool> UpdateAsync(ExerciseEditInputModel inputExercise);

    Task<int?> DeleteAsync(int id);
}
