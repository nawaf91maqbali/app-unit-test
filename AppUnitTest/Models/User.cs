namespace AppUnitTest.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public User() { 
            
        }
        public User(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public static Guid TestUserId = Guid.Parse("0bd7888d-28e0-4f99-be78-bc4987c4ba9c");
        public static User TestUser()
        {
            return new User(TestUserId, "Nawaf", "nawaf.maqbali@rihal.om");
        }
    }
}
