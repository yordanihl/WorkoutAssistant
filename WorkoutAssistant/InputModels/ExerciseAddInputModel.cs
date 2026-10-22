using System.ComponentModel.DataAnnotations;
using WorkoutAssistant.Shared.Enums;
using static WorkoutAssistant.Shared.ValidationConstants.Exercise;

namespace WorkoutAssistant.Web.InputModels;

public class ExerciseAddInputModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [EnumDataType(typeof(MuscleGroup))]
    public MuscleGroup MuscleGroup { get; set; }

    [Required]
    [Range(SetsMin, SetsMax)]
    public int Sets { get; set; }

    [Required]
    [Range(RepsMin, RepsMax)]
    public int Reps { get; set; }

    [Required]
    [Range(RestSecondsMin, RestSecondsMax)]
    public int RestSeconds { get; set; }

    [MaxLength(MediaUrlMaxLength)]
    public string? MediaUrl { get; set; }

    [Required]
    public int WorkoutId { get; set; }
}
