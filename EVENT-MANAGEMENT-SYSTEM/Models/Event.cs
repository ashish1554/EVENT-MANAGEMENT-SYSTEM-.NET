namespace EVENT_MANAGEMENT_SYSTEM.Models
{
    public class Event
    {
        public int EventId {  get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;

        public DateOnly EventDate {  get; set; }
        public string Location { get; set; }= string.Empty;


        //public bool IsDeleted {  get; set; }= false;

        public int CreatedBy {  get; set; }

        public int? UpdatedBy { get; set; }
        //navigations
        public User? Creator {  get; set; }
        public User? Updater { get; set; }

        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }

}
