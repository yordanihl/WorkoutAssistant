namespace Homies.Services.Core.Services
{
    using Homies.DataTransferObjects.Event;
    using Homies.Services.Core.Contracts;

    public class EventService : IEventService
    {
        private readonly ITypeService typeService;

        // Injects another service inside this service
        public EventService(ITypeService _typeService)
        {
            typeService = _typeService;
        }

        public EventFormModel GetEventFormModel()
        {
            var eventModel = new EventFormModel()
            {
                Types = typeService.GetTypes()
            };

            return eventModel;
        }
    }
}