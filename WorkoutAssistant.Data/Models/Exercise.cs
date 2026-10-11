using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static WorkoutAssistant.Shared.ValidationConstants.Exercise;
using WorkoutAssistant.Shared.Enums;

namespace WorkoutAssistant.Data.Models;

public class Exercise
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

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
    [ForeignKey(nameof(Workout))]
    public int WorkoutId { get; set; }

    public Workout Workout { get; set; } = null!;
}
