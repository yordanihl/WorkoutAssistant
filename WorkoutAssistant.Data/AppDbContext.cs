using Microsoft.EntityFrameworkCore;
using WorkoutAssistant.Data.Models;
using WorkoutAssistant.Shared.Enums;

namespace WorkoutAssistant.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<WorkoutPlan> WorkoutPlans { get; set; }
    public virtual DbSet<Workout> Workouts { get; set; }
    public virtual DbSet<Exercise> Exercises { get; set; }
}
