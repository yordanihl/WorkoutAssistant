using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Data.Models;

public class Workout
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(OrderPositionMin, OrderPositionMax)]
    public int OrderPosition { get; set; }

    [Required]
    [ForeignKey(nameof(WorkoutPlan))]
    public int PlanId { get; set; }

    public WorkoutPlan WorkoutPlan { get; set; } = null!;

    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
}
