using System.ComponentModel.DataAnnotations;

namespace RequestsMicroservice.Contracts.Requests
{
    public class NewRequest
    {
        [Required]
        public int Index { get; set; }

        [Required]
        public double X { get; set; }
    }
}