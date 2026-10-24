namespace WorkoutAssistant.Web.Models.Workout
{
    public class WorkoutListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int OrderPosition { get; set; }
    }
}
