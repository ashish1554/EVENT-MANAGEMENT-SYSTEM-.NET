namespace EVENT_MANAGEMENT_SYSTEM.Models
{
    public class Registration
    {
        public int RegistrationId {  get; set; }

        public int UserId {  get; set; }
        public int EventId {  get; set; }
        public string? Status {  get; set; }

        //Navigation

        public User? User { get; set; }
        public Event? Event { get; set; }


    }
}
