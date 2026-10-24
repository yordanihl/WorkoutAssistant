using WorkoutAssistant.Web.Models.Exercise;

namespace WorkoutAssistant.Web.Models.Workout
{
    public class WorkoutDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int PlanId { get; set; }

        public ICollection<ExerciseViewModel> Exercises { get; set; } = new HashSet<ExerciseViewModel>();
    }
}
