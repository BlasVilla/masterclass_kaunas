using System;

namespace RequestsMicroservice.Contracts.Messaging
{
    public class NewRequestMessage
    {
        public Guid RequestId { get; set; }
    }
}