using System.ComponentModel.DataAnnotations;
using WorkoutAssistant.Web.Models.Workout;

namespace WorkoutAssistant.Web.Models.WorkoutPlan;

public class WorkoutPlanReorderInputModel
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    public List<WorkoutOrderInputModel> Workouts { get; set; } = [];
}
