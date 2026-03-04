namespace EVENT_MANAGEMENT_SYSTEM.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; }=string.Empty;
        public string PasswordHash { get; set; }= string.Empty;

        public int RoleId { get; set; }

        //navigations
        public Role? Role { get; set; }

        public List<Event> CreatedEvents { get; set; }=new List<Event>(); //organizer creates event
        public List<Event> UpdatedEvents { get; set; }=new List<Event>();
        public List<Registration> Registrations { get; set; } = new List<Registration>();

    }
}
