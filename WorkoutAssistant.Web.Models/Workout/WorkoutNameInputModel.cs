using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Web.Models.Workout
{
    public abstract class WorkoutNameInputModel
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
