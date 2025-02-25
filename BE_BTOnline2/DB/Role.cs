namespace BE_BTOnline2.DB
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public User User { get; set; }
    }
}
