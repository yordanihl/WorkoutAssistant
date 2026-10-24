using WorkoutAssistant.Web.Models.WorkoutPlan;

namespace WorkoutAssistant.Services.Core.Contracts;

public interface IWorkoutPlanService
{
    IEnumerable<WorkoutPlanViewModel> GetAll();

    WorkoutPlanDetailsViewModel? GetDetails(int id);

    void Add(WorkoutPlanAddInputModel inputWorkoutPlan);

    WorkoutPlanEditInputModel? GetForEdit(int id);

    bool Update(WorkoutPlanEditInputModel inputWorkoutPlan);

    WorkoutPlanViewModel? GetForDelete(int id);

    bool Delete(int id);
}
