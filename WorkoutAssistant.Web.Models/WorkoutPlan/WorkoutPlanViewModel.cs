namespace WorkoutAssistant.Web.Models.WorkoutPlan
{
    public class WorkoutPlanViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
}
