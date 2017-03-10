using System;

namespace RequestsMicroservice.Contracts.Batches
{
    public class Batch
    {
        public Guid BatchId { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}