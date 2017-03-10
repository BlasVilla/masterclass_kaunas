using System;
using System.ComponentModel.DataAnnotations;

namespace RequestsMicroservice.Database
{
    public class Request
    {
        [Key]
        public Guid RequestId { get; set; }
        
        public int Index { get; set; }
        
        public double X { get; set; }

        public DateTime Created { get; set; }
        
        public Guid BatchId { get; set; }

        #region Dependencies

        public Batch Batch { get; set; }

        #endregion
    }
}