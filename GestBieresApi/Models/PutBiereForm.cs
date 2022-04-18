using System.ComponentModel.DataAnnotations;

namespace GestBieresApi.Models
{
    public class PutBiereForm
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string? Nom { get; set; }
        [Required]
        [MaxLength(6)]
        public Decimal Degre { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Origine { get; set; }
    }
}
