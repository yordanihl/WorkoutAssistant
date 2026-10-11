using System.ComponentModel.DataAnnotations;

namespace WorkoutAssistant.Shared.Enums;

public enum MuscleGroup
{
    [Display(Name = "Chest")]
    Chest = 0,

    [Display(Name = "Back")]
    Back = 1,

    [Display(Name = "Shoulders")]
    Shoulders = 2,

    [Display(Name = "Arms")]
    Arms = 3,

    [Display(Name = "Legs")]
    Legs = 4,

    [Display(Name = "Core / Abs")]
    Core = 5,

    [Display(Name = "Full Body")]
    FullBody = 6
}