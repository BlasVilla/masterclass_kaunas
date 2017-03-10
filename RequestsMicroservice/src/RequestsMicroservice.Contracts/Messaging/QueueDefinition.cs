namespace RequestsMicroservice.Contracts.Messaging
{
    public static class QueueDefinition
    {
        public const string QueueName = "requests";
        
        public const bool IsDurable = true;
    }
}