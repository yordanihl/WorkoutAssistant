using Microsoft.AspNetCore.Mvc;
using WorkoutAssistant.Services.Core.Contracts;
using WorkoutAssistant.Web.Models.WorkoutPlan;

namespace WorkoutAssistant.Web.Controllers
{
    public class WorkoutPlansController(IWorkoutPlanService workoutPlanService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<WorkoutPlanViewModel> workoutPlans = workoutPlanService.GetAll();
            return View(workoutPlans);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            WorkoutPlanDetailsViewModel? workoutPlan = workoutPlanService.GetDetails(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new WorkoutPlanAddInputModel());
        }

        [HttpPost]
        public IActionResult Add(WorkoutPlanAddInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }

            workoutPlanService.Add(inputWorkoutPlan);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            WorkoutPlanEditInputModel? workoutPlan = workoutPlanService.GetForEdit(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult Edit(WorkoutPlanEditInputModel inputWorkoutPlan)
        {
            if (!ModelState.IsValid)
            {
                return View(inputWorkoutPlan);
            }

            if (!workoutPlanService.Update(inputWorkoutPlan))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Details), new { id = inputWorkoutPlan.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            WorkoutPlanViewModel? workoutPlan = workoutPlanService.GetForDelete(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (!workoutPlanService.Delete(id))
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
