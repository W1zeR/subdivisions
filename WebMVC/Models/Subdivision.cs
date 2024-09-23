namespace WebMVC.Models
{
    public class Subdivision
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public bool IsActive { get; set; }

        public int? MainId { get; set; }
    }
}
