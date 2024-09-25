namespace WebMVC.Models
{
    public class SubdivisionRequest
    {
        public required string Name { get; set; }

        public bool IsActive { get; set; }

        public int? MainId { get; set; }
    }
}
