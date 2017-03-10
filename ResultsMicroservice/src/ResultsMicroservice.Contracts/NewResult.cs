using System.ComponentModel.DataAnnotations;

namespace ResultsMicroservice.Contracts
{
    public class NewResult
    {
        [Required]
        [MaxLength(32)]
        public string Method { get; set; }

        [Required]
        public double Value { get; set; }
    }
}