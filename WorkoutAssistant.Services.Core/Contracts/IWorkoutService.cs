using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IWorkoutService
{
    WorkoutDetailsViewModel? GetDetails(int id);

    bool Add(WorkoutAddInputModel inputWorkout);

    WorkoutEditInputModel? GetForEdit(int id);

    bool Update(WorkoutEditInputModel inputWorkout);

    WorkoutListViewModel? GetForDelete(int id);

    int? Delete(int id);
}
