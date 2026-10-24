using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Web.Models.Workout
{
    public class WorkoutAddInputModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public int PlanId { get; set; }
    }
}
