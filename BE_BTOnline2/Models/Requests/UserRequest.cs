namespace BE_BTOnline2.Models.Requests
{
    public class UserRequest
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

    }
}
