namespace WebApi.Models
{
    public class SubdivisionResponse
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public bool IsActive { get; set; }

        public int? MainId { get; set; }
    }
}
