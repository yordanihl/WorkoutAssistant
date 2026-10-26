using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.WorkoutPlan;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutPlansController(IWorkoutPlanService workoutPlanService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<WorkoutPlanViewModel> workoutPlans = await workoutPlanService.GetAllAsync();
            return View(workoutPlans);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            WorkoutPlanDetailsViewModel? workoutPlan = await workoutPlanService.GetDetailsAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpGet]
        public async Task<IActionResult> ReorderWorkouts(int id)
        {
            WorkoutPlanReorderInputModel? workoutPlan = await workoutPlanService.GetForReorderAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            if (workoutPlan.Workouts.Count < 2)
            {
                return BadRequest();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReorderWorkouts(WorkoutPlanReorderInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }

            if (!await workoutPlanService.ReorderWorkoutsAsync(inputWorkoutPlan))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = inputWorkoutPlan.Id });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new WorkoutPlanAddInputModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(WorkoutPlanAddInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }

            await workoutPlanService.AddAsync(inputWorkoutPlan);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            WorkoutPlanEditInputModel? workoutPlan = await workoutPlanService.GetForEditAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(WorkoutPlanEditInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }

            if (!await workoutPlanService.UpdateAsync(inputWorkoutPlan))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = inputWorkoutPlan.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            WorkoutPlanViewModel? workoutPlan = await workoutPlanService.GetForDeleteAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!await workoutPlanService.DeleteAsync(id))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
