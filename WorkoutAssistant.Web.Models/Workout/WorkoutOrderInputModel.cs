using System.ComponentModel.DataAnnotations;
using static WorkoutAssistant.Shared.ValidationConstants.Workout;

namespace WorkoutAssistant.Web.Models.Workout;

public class WorkoutOrderInputModel
{
    [Required]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [Range(OrderPositionMin, OrderPositionMax)]
    public int OrderPosition { get; set; }
}
