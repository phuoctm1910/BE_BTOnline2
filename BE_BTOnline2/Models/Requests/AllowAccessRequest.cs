namespace BE_BTOnline2.Models.Requests
{
    public class AllowAccessRequest
    {
        public int RoleId { get; set; }
        public string TableName { get; set; }
        public string AccessProperties { get; set; }
    }
}
