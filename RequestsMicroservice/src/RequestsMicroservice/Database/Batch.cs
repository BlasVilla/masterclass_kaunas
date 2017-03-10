using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestsMicroservice.Database
{
    public class Batch
    {
        [Key]
        public Guid BatchId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        #region Dependencies

        public ICollection<Request> Requests { get; set; }

        #endregion
    }
}