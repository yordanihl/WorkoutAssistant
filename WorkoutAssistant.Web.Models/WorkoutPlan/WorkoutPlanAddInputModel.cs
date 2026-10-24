using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.WorkoutPlan;

namespace WorkoutAssistant.Web.Models.WorkoutPlan
{
    public class WorkoutPlanAddInputModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }
    }
}
