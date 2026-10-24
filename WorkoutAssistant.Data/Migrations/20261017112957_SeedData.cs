using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkoutAssistant.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkoutPlans",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 1, "A three-day introductory strength plan covering upper body, lower body, and full-body training.", "Balanced Strength Starter" });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Name", "OrderPosition", "PlanId" },
                values: new object[,]
                {
                    { 1, "Upper Body", 1, 1 },
                    { 2, "Lower Body", 2, 1 },
                    { 3, "Full Body", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "MediaUrl", "MuscleGroup", "Name", "Reps", "RestSeconds", "Sets", "WorkoutId" },
                values: new object[,]
                {
                    { 1, null, 0, "Push-Up", 10, 60, 3, 1 },
                    { 2, null, 1, "Bent-Over Row", 10, 60, 3, 1 },
                    { 3, null, 2, "Overhead Press", 8, 60, 3, 1 },
                    { 4, null, 4, "Bodyweight Squat", 12, 60, 3, 2 },
                    { 5, null, 4, "Glute Bridge", 12, 45, 3, 2 },
                    { 6, null, 4, "Calf Raise", 15, 45, 3, 2 },
                    { 7, null, 6, "Dumbbell Deadlift", 10, 60, 3, 3 },
                    { 8, null, 5, "Plank", 30, 45, 3, 3 },
                    { 9, null, 3, "Dumbbell Curl", 12, 45, 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WorkoutPlans",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
