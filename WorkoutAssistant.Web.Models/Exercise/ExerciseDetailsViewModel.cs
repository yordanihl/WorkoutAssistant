using WorkoutAssistant.Shared.Enums;

namespace WorkoutAssistant.Web.Models.Exercise;

public class ExerciseDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public MuscleGroup MuscleGroup { get; set; }

    public int Sets { get; set; }

    public int Reps { get; set; }

    public int RestSeconds { get; set; }

    public string? MediaUrl { get; set; }

    public int WorkoutId { get; set; }

    public string WorkoutName { get; set; } = null!;
}
