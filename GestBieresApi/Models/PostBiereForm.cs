using System.ComponentModel.DataAnnotations;


namespace GestBieresApi.Models
{
    public class PostBiereForm
    {
        [Required]
        [MaxLength(80)]
        public string? Nom { get; set; }
        [Required]
        [MaxLength(6)]
        public Decimal Degre { get; set; }
        [Required]      
        [MaxLength(50)]
        public string? Origine{ get; set; }
    }
}
