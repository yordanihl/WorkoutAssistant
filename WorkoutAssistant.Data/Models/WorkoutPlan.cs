using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.WorkoutPlan;

namespace WorkoutAssistant.Data.Models;

public class WorkoutPlan
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }

    public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
}
