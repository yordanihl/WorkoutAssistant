using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IExerciseService
{
    ExerciseDetailsViewModel? GetDetails(int id);

    bool WorkoutExists(int workoutId);

    void Add(ExerciseAddInputModel inputExercise);

    ExerciseEditInputModel? GetForEdit(int id);

    bool Update(ExerciseEditInputModel inputExercise);

    int? Delete(int id);
}
