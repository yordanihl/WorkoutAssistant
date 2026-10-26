using System.ComponentModel.DataAnnotations;

namespace WorkoutAssistant.Web.Models.Workout
{
    public class WorkoutAddInputModel : WorkoutNameInputModel
    {
        [Required]
        public int PlanId { get; set; }
    }
}
