namespace Homies.Services.Core.Contracts
{
    using Homies.DataTransferObjects.Event;

    public interface IEventService
    {
        EventFormModel GetEventFormModel();
    }
}