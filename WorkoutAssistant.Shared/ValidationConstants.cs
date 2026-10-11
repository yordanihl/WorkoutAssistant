namespace WorkoutAssistant.Shared;

public static class ValidationConstants
{
    public static class WorkoutPlan
    {
        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 120;
        public const int DescriptionMaxLength = 1200;
    }

    public static class Workout
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 120;
        public const int OrderPositionMin = 1;
        public const int OrderPositionMax = 30;
    }

    public static class Exercise
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 120;
        public const int SetsMin = 1;
        public const int SetsMax = 50;
        public const int RepsMin = 1;
        public const int RepsMax = 500;
        public const int RestSecondsMin = 0;
        public const int RestSecondsMax = 1800;
        public const int MediaUrlMinLength = 12;
        public const int MediaUrlMaxLength = 320;
    }
}