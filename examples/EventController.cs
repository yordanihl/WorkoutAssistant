[Authorize]
public class EventController : Controller
{
    private readonly IEventService eventService;

    // Inject the interface, not the DbContext
    public EventController(IEventService _eventService)
    {
        eventService = _eventService;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var eventModel = eventService.GetEventFormModel();

        return View(eventModel);
    }
}