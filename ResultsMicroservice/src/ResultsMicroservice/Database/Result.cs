using System;
using System.ComponentModel.DataAnnotations;

namespace ResultsMicroservice.Database
{
    public class Result
    {
        [Key]
        public Guid RequestId { get; set; }

        [Required]
        [MaxLength(32)]
        public string Method { get; set; }

        public double Value { get; set; }
        
        public DateTime Create { get; set; }
    }
}