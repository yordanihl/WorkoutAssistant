using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Web.InputModels
{
    public class WorkoutEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
