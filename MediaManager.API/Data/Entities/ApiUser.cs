namespace MediaManager.API.Data.Entities
{
    public class ApiUser
    {
        public int UserId { get; set; } = 1;
        public string UserName { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
    }
}
