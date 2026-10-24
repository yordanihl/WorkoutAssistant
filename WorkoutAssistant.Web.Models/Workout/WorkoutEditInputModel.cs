using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Web.Models.Workout
{
    public class WorkoutEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
