using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Data.Models;

namespace WorkoutAssistant.Data.Configuration
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public static List<Workout> Workouts = new()
        {
            new Workout { Id = 1, Name = "Upper Body", OrderPosition = 1, PlanId = 1 },
            new Workout { Id = 2, Name = "Lower Body", OrderPosition = 2, PlanId = 1 },
            new Workout { Id = 3, Name = "Full Body", OrderPosition = 3, PlanId = 1 }
        };

        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasData(Workouts);
        }
    }
}
