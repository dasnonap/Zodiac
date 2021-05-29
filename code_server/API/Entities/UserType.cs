namespace API.Entities
{
    public class UserType
    {
        public int UserTypeId { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }
        
        public AppUser AppUser { get; set; }
    }
}