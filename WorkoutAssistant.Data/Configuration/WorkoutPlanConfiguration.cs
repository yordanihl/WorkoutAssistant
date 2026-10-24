using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutAssistant.Data.Models;

namespace WorkoutAssistant.Data.Configuration
{
    public class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public static List<WorkoutPlan> WorkoutPlans = new()
        {
            new WorkoutPlan
            {
                Id = 1,
                Title = "Balanced Strength Starter",
                Description = "A three-day introductory strength plan covering upper body, lower body, and full-body training."
            }
        };

        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.HasData(WorkoutPlans);
        }
    }
}
