namespace CelilCavus.Data
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int ApplicationUserRoleId { get; set; }
        public List<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
    public class ApplicationRole
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        public List<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
    public class ApplicationUserRole
    {
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int RoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
    }

}