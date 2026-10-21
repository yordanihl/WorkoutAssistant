using WorkoutAssistant.Web.ViewModels.Workout;

namespace WorkoutAssistant.Web.ViewModels.WorkoutPlan
{
    public class WorkoutPlanDetailsViewModel : WorkoutPlanViewModel
    {
        public ICollection<WorkoutListViewModel> Workouts { get; set; } = new List<WorkoutListViewModel>();
    }
}
