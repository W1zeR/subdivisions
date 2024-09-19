using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entity
{
    public class Subdivision
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public int? MainId { get; set; }
        [ForeignKey(nameof(MainId))]
        public Subdivision? Main { get; set; }
    }
}
