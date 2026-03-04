namespace EVENT_MANAGEMENT_SYSTEM.DTOs
{
    public class EventDto
    {
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
