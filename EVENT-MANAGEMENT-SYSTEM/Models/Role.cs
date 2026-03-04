namespace EVENT_MANAGEMENT_SYSTEM.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;    


        //navigations
        public List<User> Users { get; set; }=new List<User>();
    }
}
