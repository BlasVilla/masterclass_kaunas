using System;

namespace RequestsMicroservice.Contracts.Requests
{
    public class Request
    {
        public Guid RequestId { get; set; }
        
        public int Index { get; set; }
        
        public double X { get; set; }

        public DateTime Created { get; set; }
    }
}