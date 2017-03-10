using System.ComponentModel.DataAnnotations;

namespace RequestsMicroservice.Contracts.Batches
{
    public class NewBatch
    {
        [Required]
        [MaxLength(64)]
        public string Description { get; set; }
    }
}