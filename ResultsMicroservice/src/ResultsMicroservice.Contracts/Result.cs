using System;

namespace ResultsMicroservice.Contracts
{
    public class Result
    {
        public Guid RequestId { get; set; }

        public string Method { get; set; }

        public double Value { get; set; }

        public DateTime Created { get; set; }
    }
}