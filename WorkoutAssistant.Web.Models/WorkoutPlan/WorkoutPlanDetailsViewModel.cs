using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Web.Models.WorkoutPlan
{
    public class WorkoutPlanDetailsViewModel : WorkoutPlanViewModel
    {
        public ICollection<WorkoutListViewModel> Workouts { get; set; } = new List<WorkoutListViewModel>();
    }
}
