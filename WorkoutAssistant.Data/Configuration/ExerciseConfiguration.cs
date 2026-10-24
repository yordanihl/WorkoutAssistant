using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Shared.Enums;

namespace WorkoutAssistant.Data.Configuration
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public static List<Exercise> Exercises = new()
        {
            new Exercise { Id = 1, Name = "Push-Up", MuscleGroup = MuscleGroup.Chest, Sets = 3, Reps = 10, RestSeconds = 60, WorkoutId = 1 },
            new Exercise { Id = 2, Name = "Bent-Over Row", MuscleGroup = MuscleGroup.Back, Sets = 3, Reps = 10, RestSeconds = 60, WorkoutId = 1 },
            new Exercise { Id = 3, Name = "Overhead Press", MuscleGroup = MuscleGroup.Shoulders, Sets = 3, Reps = 8, RestSeconds = 60, WorkoutId = 1 },
            new Exercise { Id = 4, Name = "Bodyweight Squat", MuscleGroup = MuscleGroup.Legs, Sets = 3, Reps = 12, RestSeconds = 60, WorkoutId = 2 },
            new Exercise { Id = 5, Name = "Glute Bridge", MuscleGroup = MuscleGroup.Legs, Sets = 3, Reps = 12, RestSeconds = 45, WorkoutId = 2 },
            new Exercise { Id = 6, Name = "Calf Raise", MuscleGroup = MuscleGroup.Legs, Sets = 3, Reps = 15, RestSeconds = 45, WorkoutId = 2 },
            new Exercise { Id = 7, Name = "Dumbbell Deadlift", MuscleGroup = MuscleGroup.FullBody, Sets = 3, Reps = 10, RestSeconds = 60, WorkoutId = 3 },
            new Exercise { Id = 8, Name = "Plank", MuscleGroup = MuscleGroup.Core, Sets = 3, Reps = 30, RestSeconds = 45, WorkoutId = 3 },
            new Exercise { Id = 9, Name = "Dumbbell Curl", MuscleGroup = MuscleGroup.Arms, Sets = 3, Reps = 12, RestSeconds = 45, WorkoutId = 3 }
        };

        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasData(Exercises);
        }
    }
}
