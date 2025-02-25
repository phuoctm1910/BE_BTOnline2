namespace BE_BTOnline2.DB
{
    public class AllowAccess
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string TableName { get; set; }
        public string AccessProperties { get; set; }

        public Role Role { get; set; }
    }

}
