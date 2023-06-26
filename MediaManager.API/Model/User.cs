namespace MediaManager.API.Model
{
    public class User
    {
        public User(string id, string email, string userName)
        {
            Id = id;
            Email = email;
            UserName = userName;
        }

        public String Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
